Imports Microsoft.VisualBasic

Public Interface iMythVideo
    Inherits iMythAPIService

    Function GetVideo(Id As Integer) As MythVideo.VideoMetadataInfo
    Function GetVideoList(Descending As Boolean, StartIndex As Integer, Count As Integer) As MythVideo.VideoMetadataInfoList


End Interface
