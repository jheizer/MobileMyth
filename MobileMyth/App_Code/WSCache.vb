#Region "GPL"
'    This file is part of MobileMyth.

'    MobileMyth is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.

'    MobileMyth is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.

'    You should have received a copy of the GNU General Public License
'    along with MobileMyth.  If not, see <http://www.gnu.org/licenses/>.

'    Copyright 2012, 2013 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic
Imports MythDVR
Imports MythService
Imports System.ServiceModel
Imports System.Net.Sockets
Imports MythVideo

Public Class WSCache
    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(WSCache))

    Public Shared Content As MythContent.ContentClient
    Public Shared DVR As MythDVR.DvrClient
    Public Shared Service As MythService.MythClient
    Public Shared Guide As MythGuide.GuideClient
    Public Shared Video As MythVideo.VideoClient

    Public Shared Content27 As MythContent_27.ContentClient

    Public Shared MythVersion As MythTvVersion = MythTvVersion.v26

    Shared Sub New()
        ReInitServiceReferences()
    End Sub

    Public Shared Function ReInitServiceReferences() As Boolean
        Try
            Logger.Info("Reinitalizing webservice references.")

            Dim Address As String = SiteSettings.Setting("MythServiceAPIAddress")
            Dim Port As String = SiteSettings.Setting("MythServiceAPIPort")

            WSCache.Content = New MythContent.ContentClient("BasicHttpBinding_Content", "http://" & Address & ":" & Port & "/Content")
            WSCache.DVR = New MythDVR.DvrClient("BasicHttpBinding_Dvr", "http://" & Address & ":" & Port & "/Dvr")
            WSCache.Service = New MythService.MythClient("BasicHttpBinding_Myth", "http://" & Address & ":" & Port & "/Myth")
            WSCache.Guide = New MythGuide.GuideClient("BasicHttpBinding_Guide", "http://" & Address & ":" & Port & "/Guide")
            WSCache.Video = New MythVideo.VideoClient("BasicHttpBinding_Video", "http://" & Address & ":" & Port & "/Video")

            WSCache.Content27 = New MythContent_27.ContentClient("BasicHttpBinding_Content1", "http://" & Address & ":" & Port & "/Content")

            Try
                Dim WC As New Net.WebClient
                Dim html As String = WC.DownloadString(Common.GetServiceUrl & "/Status/GetStatus")
                If Not html.Contains("<Status") Then
                    Return False
                End If

                'Detect Mythtv version
                html = WC.DownloadString(Common.GetServiceUrl & "/Dvr/wsdl?raw=1")
                If html.Contains("Interface Version 1.9") Then
                    MythVersion = MythTvVersion.v27
                End If

            Catch ex As Exception
                Return False
            End Try

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Shared Function GetRecordedList() As ProgramList
        Dim Recordings As ProgramList = Nothing

        Try
            Recordings = HttpContext.Current.Cache("GetRecordedList")

            If Recordings Is Nothing Then
                Recordings = DVR.GetRecordedList(True, 0, 10000)
                HttpContext.Current.Cache.Add("GetRecordedList", Recordings, Nothing, Now.AddMinutes(1), Nothing, CacheItemPriority.Low, Nothing)
            End If

        Catch ex As Exception
            Logger.Error("Error loading recorded list" & ControlChars.NewLine & ex.ToString)
        End Try

        Return Recordings

    End Function

    Public Shared Sub ClearRecordedListCache()
        HttpContext.Current.Cache.Remove("GetRecordedList")
    End Sub

    Public Shared Function GetFilteredStreamList(ByVal Filename As String) As MythContent.LiveStreamInfoList
        If WSCache.MythVersion = MythTvVersion.v27 Then
            Dim Streams As MythContent_27.LiveStreamInfoList
            Streams = WSCache.Content27.GetLiveStreamList(Filename)

            Dim Lst As New List(Of MythContent.LiveStreamInfo)
            Dim Str As MythContent.LiveStreamInfo
            For Each Strm As MythContent_27.LiveStreamInfo In Streams.LiveStreamInfos
                Str = New MythContent.LiveStreamInfo

                Dim oldtype As Type = Str.GetType()

                For Each prop As Reflection.PropertyInfo In Strm.GetType.GetProperties
                    Dim propgetter As Reflection.MethodInfo = prop.GetGetMethod
                    Dim propsetter As Reflection.MethodInfo = oldtype.GetProperty(prop.Name).GetSetMethod
                    Dim value As Object = propgetter.Invoke(Strm, Nothing)
                    propsetter.Invoke(Str, New Object() {value})
                Next

                Lst.Add(Str)
            Next

            Dim ret As New MythContent.LiveStreamInfoList
            ret.LiveStreamInfos = Lst.ToArray

            Return ret

        Else
            Return WSCache.Content.GetFilteredLiveStreamList(Filename)
        End If
    End Function

End Class
