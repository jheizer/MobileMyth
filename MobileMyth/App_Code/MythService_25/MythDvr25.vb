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

'    Copyright 2012-2014 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic
Imports MythDVR

Public Class MythDvr25
    Implements iMythDvr

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(MythDvr25))

    Private DVR As MythDVR.DvrClient

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythAPIService.Init
        DVR = New MythDVR.DvrClient("BasicHttpBinding_Dvr", "http://" & Address & ":" & Port & "/Dvr")
        Return True
    End Function

    Public Function GetConflictList(StartIndex As Integer, Count As Integer) As MythDVR.ProgramList Implements iMythDvr.GetConflictList
        Return DVR.GetConflictList(StartIndex, Count)
    End Function

    Public Function GetEncoderList() As MythDVR.EncoderList Implements iMythDvr.GetEncoderList
        Return DVR.GetEncoderList
    End Function

    Public Function GetRecorded(ChanId As Integer, StartTime As Date) As MythDVR.Program Implements iMythDvr.GetRecorded
        Return DVR.GetRecorded(ChanId, StartTime)
    End Function

    Public Function GetUpcomingList(StartIndex As Integer, Count As Integer, ShowAll As Boolean) As MythDVR.ProgramList Implements iMythDvr.GetUpcomingList
        Return DVR.GetUpcomingList(StartIndex, Count, ShowAll)
    End Function

    Public Function RemoveRecorded(ChanId As Integer, StartTs As Date) As Boolean Implements iMythDvr.RemoveRecorded
        Return DVR.RemoveRecorded(ChanId, StartTs)
    End Function

    Public Function GetRecordedList(Descending As Boolean, StartIndex As Integer, Count As Integer, Cache As Boolean) As MythDVR.ProgramList Implements iMythDvr.GetRecordedList
        Dim Recordings As MythDVR.ProgramList = Nothing

        Try
            If Cache Then
                Recordings = HttpContext.Current.Cache("GetRecordedList" & Count.ToString)

                If Recordings Is Nothing Then
                    Recordings = DVR.GetRecordedList(Descending, StartIndex, Count)

                    'Cache the list so we don't have to reload it as often
                    HttpContext.Current.Cache.Add("GetRecordedList", Recordings, Nothing, Now.AddMinutes(1), Nothing, CacheItemPriority.Low, Nothing)
                End If
            Else
                Recordings = DVR.GetRecordedList(Descending, StartIndex, Count)
            End If
        Catch ex As Exception
            Logger.Error("Error loading recorded list", ex)
        End Try

        Return Recordings

    End Function

    Public Function GetTitles() As List(Of iMythDvr.RecordingTitle) Implements iMythDvr.GetTitles
        Dim Titles As New List(Of iMythDvr.RecordingTitle)

        Dim Recordings As ProgramList = Common.MBE.DvrAPI.GetRecordedList(True, 0, 100000, True)
        Dim Programs() As Program = Array.FindAll(Recordings.Programs, Function(p) p.Recording.StorageGroup <> "Deleted" AndAlso p.Recording.StorageGroup <> "LiveTV")

        Dim Lst = From n In Programs _
                  Group n By n.Title Into Count() _
                  Select Title, Episodes = Count, InetRef = (From s In Recordings.Programs _
                                                  Where s.Title = Title _
                                                  Select s.Inetref).First _
                                              , Season = (From s In Recordings.Programs _
                                                  Where s.Title = Title _
                                                  Select s.Season).First _
                  Order By Title

        For Each L In Lst
            Titles.Add(New iMythDvr.RecordingTitle(L.Title, L.InetRef, L.Episodes))
        Next

        Return Titles
    End Function

    Public Sub ClearRecordedListCache() Implements iMythDvr.ClearRecordedListCache
        HttpContext.Current.Cache.Remove("GetRecordedList")
    End Sub

End Class
