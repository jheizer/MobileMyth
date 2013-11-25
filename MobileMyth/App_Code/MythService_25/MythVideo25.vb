Imports Microsoft.VisualBasic

Public Class MythVideo25
    Implements iMythVideo

    Private Video As MythVideo.VideoClient

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythAPIService.Init
        Video = New MythVideo.VideoClient("BasicHttpBinding_Video", "http://" & Address & ":" & Port & "/Video")
        Return True
    End Function

    Public Function GetVideo(Id As Integer) As MythVideo.VideoMetadataInfo Implements iMythVideo.GetVideo
        Return Video.GetVideo(Id)
    End Function

    Public Function GetVideoList(Descending As Boolean, StartIndex As Integer, Count As Integer) As MythVideo.VideoMetadataInfoList Implements iMythVideo.GetVideoList
        Return Video.GetVideoList(Descending, StartIndex, Count)
    End Function
End Class
