Imports Microsoft.VisualBasic

Public Class MythContent25
    Implements iMythContent

    Private Content As MythContent.ContentClient

    Public Sub New()
    End Sub

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythContent.Init
        Content = New MythContent.ContentClient("BasicHttpBinding_Content", "http://" & Address & ":" & Port & "/Content")
        Return True
    End Function

    Public Function GetFileList(StorageGroup As String) As String() Implements iMythContent.GetFileList
        Return Content.GetFileList(StorageGroup).ToArray
    End Function

    Public Function AddRecordingLiveStream(ChanId As Integer, StartTime As Date, Maxsegments As Integer, Width As Integer, Height As Integer, BitRate As Integer, AudioBitrate As Integer, SampleRate As Integer) As MythContent_27.LiveStreamInfo Implements iMythContent.AddRecordingLiveStream
        Dim Str As MythContent.LiveStreamInfo = Content.AddRecordingLiveStream(ChanId, StartTime, Maxsegments, Width, Height, BitRate, AudioBitrate, SampleRate)
        Return Common.ConvertTypes(Of MythContent.LiveStreamInfo, MythContent_27.LiveStreamInfo)(Str)
    End Function

    Public Function GetFilteredStreamList(FileName As String) As MythContent_27.LiveStreamInfoList Implements iMythContent.GetFilteredStreamList
        Dim Lst As New MythContent_27.LiveStreamInfoList
        Dim Ary As New List(Of MythContent_27.LiveStreamInfo)

        For Each Stream As MythContent.LiveStreamInfo In Content.GetFilteredLiveStreamList(FileName).LiveStreamInfos
            Ary.Add(Common.ConvertTypes(Of MythContent.LiveStreamInfo, MythContent_27.LiveStreamInfo)(Stream))
        Next

        Lst.LiveStreamInfos = Ary.ToArray
        Return Lst
    End Function

    Public Function RemoveLiveStream(Id As Integer) As Boolean Implements iMythContent.RemoveLiveStream
        Return Content.RemoveLiveStream(Id)
    End Function

    Public Function AddVideoLiveStream(Id As Integer, MaxSegments As Integer, Width As Integer, Height As Integer, Bitrate As Integer, AudioBitrate As Integer, SampleRate As Integer) As MythContent_27.LiveStreamInfo Implements iMythContent.AddVideoLiveStream
        Dim Str As MythContent.LiveStreamInfo = Content.AddVideoLiveStream(Id, MaxSegments, Width, Height, Bitrate, AudioBitrate, SampleRate)
        Return Common.ConvertTypes(Of MythContent.LiveStreamInfo, MythContent_27.LiveStreamInfo)(Str)
    End Function
End Class
