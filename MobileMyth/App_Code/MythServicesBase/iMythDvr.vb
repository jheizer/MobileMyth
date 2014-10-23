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

Public Interface iMythDvr
    Inherits iMythAPIService

    Function GetRecorded(ChanId As Integer, StartTime As DateTime) As MythDVR.Program
    Function GetEncoderList() As MythDVR.EncoderList
    Function GetConflictList(StartIndex As Integer, Count As Integer) As MythDVR.ProgramList
    Function GetUpcomingList(StartIndex As Integer, Count As Integer, ShowAll As Boolean) As MythDVR.ProgramList
    Function RemoveRecorded(ChanId As Integer, StartTs As DateTime) As Boolean
    Function GetRecordedList(Descending As Boolean, StartIndex As Integer, Count As Integer, Cache As Boolean) As MythDVR.ProgramList
    Sub ClearRecordedListCache()
    Function GetTitles() As List(Of iMythDvr.RecordingTitle)

    Class RecordingTitle
        Public Property Title As String
        Public Property Inetref As String
        Public Property Count As String

        Sub New()
        End Sub

        Sub New(Title As String, Inetref As String, Count As String)
            Me.Title = Title
            Me.Inetref = Inetref
            Me.Count = Count
        End Sub
    End Class
End Interface
