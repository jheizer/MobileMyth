Imports Microsoft.VisualBasic

Public Class MythContent27
    Implements iMythContent

    Private Content As MythContent_27.ContentClient

    Public Sub New()
    End Sub

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythContent.Init
        Content = New MythContent_27.ContentClient("BasicHttpBinding_Content", "http://" & Address & ":" & Port & "/Content")
        Return True
    End Function

    Public Function GetFileList(StorageGroup As String) As String() Implements iMythContent.GetFileList
        Return Content.GetFileList(StorageGroup).ToArray
    End Function

    Public Function AddRecordingLiveStream(ChanId As Integer, StartTime As Date, Maxsegments As Integer, Width As Integer, Height As Integer, BitRate As Integer, AudioBitrate As Integer, SampleRate As Integer) As MythContent_27.LiveStreamInfo Implements iMythContent.AddRecordingLiveStream
        Return Content.AddRecordingLiveStream(ChanId, StartTime, Maxsegments, Width, Height, BitRate, AudioBitrate, SampleRate)
    End Function

    Public Function GetFilteredStreamList(FileName As String) As MythContent_27.LiveStreamInfoList Implements iMythContent.GetFilteredStreamList
        Return Content.GetLiveStreamList(FileName)
    End Function

    Public Function RemoveLiveStream(Id As Integer) As Boolean Implements iMythContent.RemoveLiveStream
        Return Content.RemoveLiveStream(Id)
    End Function

    Public Function AddVideoLiveStream(Id As Integer, MaxSegments As Integer, Width As Integer, Height As Integer, Bitrate As Integer, AudioBitrate As Integer, SampleRate As Integer) As MythContent_27.LiveStreamInfo Implements iMythContent.AddVideoLiveStream
        Return Content.AddVideoLiveStream(Id, MaxSegments, Width, Height, Bitrate, AudioBitrate, SampleRate)
    End Function
End Class
