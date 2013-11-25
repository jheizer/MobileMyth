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
        Public Title As String
        Public Inetref As String
        Public Count As String

        Sub New()
        End Sub

        Sub New(Title As String, Inetref As String, Count As String)
            Me.Title = Title
            Me.Inetref = Inetref
            Me.Count = Count
        End Sub
    End Class
End Interface
