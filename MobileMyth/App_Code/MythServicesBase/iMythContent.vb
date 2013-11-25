Imports Microsoft.VisualBasic

Public Interface iMythContent
    Inherits iMythAPIService

    Function GetFileList(StorageGroup As String) As String()
    Function AddRecordingLiveStream(ChanId As Integer, StartTime As Date, Maxsegments As Integer, Width As Integer, Height As Integer, BitRate As Integer, AudioBitrate As Integer, SampleRate As Integer) As MythContent_27.LiveStreamInfo
    Function AddVideoLiveStream(Id As Integer, MaxSegments As Integer, Width As Integer, Height As Integer, Bitrate As Integer, AudioBitrate As Integer, SampleRate As Integer) As MythContent_27.LiveStreamInfo
    Function RemoveLiveStream(Id As Integer) As Boolean
    Function GetFilteredStreamList(FileName As String) As MythContent_27.LiveStreamInfoList

End Interface
