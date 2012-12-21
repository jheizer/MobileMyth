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

'    Copyright 2012 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic
Imports MythDVR
Imports MythService
Imports System.ServiceModel
Imports System.Net.Sockets
Imports MythVideo

Public Class WSCache
    Public Shared Content As MythContent.ContentClient
    Public Shared DVR As MythDVR.DvrClient
    Public Shared Service As MythService.MythClient
    Public Shared Guide As MythGuide.GuideClient
    Public Shared Video As MythVideo.VideoClient

    Shared Sub New()
        ReInitServiceReferences()
    End Sub

    Public Shared Function ReInitServiceReferences() As Boolean
        Try

            Dim Address As String = SiteSettings.Setting("MythServiceAPIAddress")
            Dim Port As String = SiteSettings.Setting("MythServiceAPIPort")

            WSCache.Content = New MythContent.ContentClient("BasicHttpBinding_Content", "http://" & Address & ":" & Port & "/Content")
            WSCache.DVR = New MythDVR.DvrClient("BasicHttpBinding_Dvr", "http://" & Address & ":" & Port & "/Dvr")
            WSCache.Service = New MythService.MythClient("BasicHttpBinding_Myth", "http://" & Address & ":" & Port & "/Myth")
            WSCache.Guide = New MythGuide.GuideClient("BasicHttpBinding_Guide", "http://" & Address & ":" & Port & "/Guide")
            WSCache.Video = New MythVideo.VideoClient("BasicHttpBinding_Video", "http://" & Address & ":" & Port & "/Video")

            Dim Con As New TcpClient()
            Dim State As IAsyncResult = Con.BeginConnect(Address, Port, New AsyncCallback(AddressOf Con.EndConnect), Con)
            Dim Connected As Boolean = State.AsyncWaitHandle.WaitOne(1000, True)

            If Not Connected OrElse Not Con.Connected Then
                Return False
            End If

            Try
                Con.Close()
            Catch ex As Exception
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
                HttpContext.Current.Cache.Add("GetRecordedList", Recordings, Nothing, Now.AddMinutes(1), Nothing, Nothing, Nothing)
            End If

        Catch ex As Exception
            'On error return nothing 
        End Try

        Return Recordings

    End Function

    Public Shared Sub ClearRecordedListCache()
        HttpContext.Current.Cache.Remove("GetRecordedList")
    End Sub


End Class
