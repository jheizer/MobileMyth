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

'    Copyright 2012-2014,2017 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic

Public Class MythDvr29
    Implements iMythDvr

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(MythDvr25))

    Private DVR As MythDvr_29.DvrClient

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythAPIService.Init
        DVR = New MythDvr_29.DvrClient("BasicHttpBinding_Dvr2", "http://" & Address & ":" & Port & "/Dvr")
        Return True
    End Function

    Public Function GetConflictList(StartIndex As Integer, Count As Integer) As iMythDvr.ProgramList Implements iMythDvr.GetConflictList
        Return ConvertProgramList(DVR.GetConflictList(StartIndex, Count, Nothing))
    End Function

    Public Function GetEncoderList() As iMythDvr.EncoderList Implements iMythDvr.GetEncoderList
        Dim EncList As New iMythDvr.EncoderList
        Dim lst As New List(Of iMythDvr.Encoder)

        For Each prog As MythDvr_29.Encoder In DVR.GetEncoderList.Encoders
            lst.Add(ConvertEncoder(prog))
        Next

        EncList.Encoders = lst.ToArray
        Return EncList
    End Function

    Public Function GetRecorded(ChanId As Integer, StartTime As Date) As iMythDvr.Program Implements iMythDvr.GetRecorded
        Return ConvertProgram(DVR.GetRecorded(Nothing, ChanId, StartTime))
    End Function

    Public Function GetUpcomingList(StartIndex As Integer, Count As Integer, ShowAll As Boolean) As iMythDvr.ProgramList Implements iMythDvr.GetUpcomingList
        Return ConvertProgramList(DVR.GetUpcomingList(StartIndex, Count, ShowAll, Nothing, Nothing))
    End Function

    Public Function RemoveRecorded(ChanId As Integer, StartTs As Date) As Boolean Implements iMythDvr.RemoveRecorded
        Return DVR.RemoveRecorded(Nothing, ChanId, StartTs, False, False)
    End Function

    Public Function GetRecordedList(Descending As Boolean, StartIndex As Integer, Count As Integer, Cache As Boolean) As iMythDvr.ProgramList Implements iMythDvr.GetRecordedList
        Dim Recordings As iMythDvr.ProgramList = Nothing

        Try
            If Cache Then
                Recordings = HttpContext.Current.Cache("GetRecordedList" & Count.ToString)

                If Recordings Is Nothing Then
                    Recordings = ConvertProgramList(DVR.GetRecordedList(Descending, StartIndex, Count, Nothing, Nothing, Nothing))

                    'Cache the list so we don't have to reload it as often
                    HttpContext.Current.Cache.Add("GetRecordedList", Recordings, Nothing, Now.AddMinutes(1), Nothing, CacheItemPriority.Low, Nothing)
                End If
            Else
                Recordings = ConvertProgramList(DVR.GetRecordedList(Descending, StartIndex, Count, Nothing, Nothing, Nothing))
            End If
        Catch ex As Exception
            Logger.Error("Error loading recorded list", ex)
        End Try

        Return Recordings

    End Function

    Public Function GetTitles() As List(Of iMythDvr.RecordingTitle) Implements iMythDvr.GetTitles
        Dim Titles As New List(Of iMythDvr.RecordingTitle)

        Dim Recordings As iMythDvr.ProgramList = Common.MBE.DvrAPI.GetRecordedList(True, 0, 100000, True)
        Dim Programs() As iMythDvr.Program = Array.FindAll(Recordings.Programs, Function(p) p.Recording.StorageGroup <> "Deleted" AndAlso p.Recording.StorageGroup <> "LiveTV")

        Dim Lst = From n In Programs
                  Group n By n.Title Into Count()
                  Select Title, Episodes = Count, InetRef = (From s In Recordings.Programs
                                                             Where s.Title = Title
                                                             Select s.Inetref).First _
                                              , Season = (From s In Recordings.Programs
                                                          Where s.Title = Title
                                                          Select s.Season).First
                  Order By Title

        For Each L In Lst
            Titles.Add(New iMythDvr.RecordingTitle(L.Title, L.InetRef, L.Episodes))
        Next

        Return Titles
    End Function

    Public Sub ClearRecordedListCache() Implements iMythDvr.ClearRecordedListCache
        HttpContext.Current.Cache.Remove("GetRecordedList")
    End Sub


    Private Function ConvertProgramList(WSProgList As MythDvr_29.ProgramList) As iMythDvr.ProgramList
        Dim ProgList As iMythDvr.ProgramList = Common.ConvertTypes(Of MythDvr_29.ProgramList, iMythDvr.ProgramList)(WSProgList)

        Dim lst As New List(Of iMythDvr.Program)

        For Each prog As MythDvr_29.Program In WSProgList.Programs.AsParallel
            lst.Add(ConvertProgram(prog))
        Next

        ProgList.Programs = lst.ToArray
        Return ProgList
    End Function

    Private Function ConvertProgram(WS As MythDvr_29.Program) As iMythDvr.Program
        Dim iObj As iMythDvr.Program = Common.ConvertTypes(Of MythDvr_29.Program, iMythDvr.Program)(WS)

        iObj.Artwork = ConvertArtworkInfoList(WS.Artwork)
        iObj.Recording = Common.ConvertTypes(Of MythDvr_29.RecordingInfo, iMythDvr.RecordingInfo)(WS.Recording)
        iObj.Channel = Common.ConvertTypes(Of MythDvr_29.ChannelInfo, iMythDvr.ChannelInfo)(WS.Channel)

        Return iObj
    End Function

    Private Function ConvertArtworkInfoList(WS As MythDvr_29.ArtworkInfoList) As iMythDvr.ArtworkInfoList
        Dim iObj As iMythDvr.ArtworkInfoList = Common.ConvertTypes(Of MythDvr_29.ArtworkInfoList, iMythDvr.ArtworkInfoList)(WS)

        Dim lst As New List(Of iMythDvr.ArtworkInfo)

        For Each prog As MythDvr_29.ArtworkInfo In WS.ArtworkInfos.AsParallel
            lst.Add(Common.ConvertTypes(Of MythDvr_29.ArtworkInfo, iMythDvr.ArtworkInfo)(prog))
        Next

        iObj.ArtworkInfos = lst.ToArray
        Return iObj
    End Function

    Private Function ConvertEncoder(WS As MythDvr_29.Encoder) As iMythDvr.Encoder
        Dim iObj As iMythDvr.Encoder = Common.ConvertTypes(Of MythDvr_29.Encoder, iMythDvr.Encoder)(WS)
        iObj.Recording = ConvertProgram(WS.Recording)

        Return iObj
    End Function

End Class
