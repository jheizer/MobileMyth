Imports Microsoft.VisualBasic

Public Class CodecInfo
    ' mpegts
    'mpeg2video
    'ac3 
    ' h264
    'aac
    'matroska
    'webm
    'vorbis
    'vp8
    Public ContainerType As VideoContainer = VideoContainer.Other
    Public VideoType As VideoCodec = VideoCodec.other
    Public AudioType As AudioCodec = AudioCodec.other

    Public Sub New()
    End Sub

    Public Sub New(Filename As String)
        Dim VidInfo As String = LinuxProcess.Run("mythffmpeg", "-i " & Filename, True)
        VidInfo = VidInfo.ToLower

        'Container Type
        If VidInfo.Contains("mpegts") Then
            ContainerType = VideoContainer.Mpegts
        ElseIf VidInfo.Contains("webm") Then
            ContainerType = VideoContainer.WebM
        ElseIf VidInfo.Contains("avi") Then
            ContainerType = VideoContainer.AVI
        ElseIf VidInfo.Contains("mp4") Then
            ContainerType = VideoContainer.MP4
        ElseIf VidInfo.Contains("matroska") Then
            ContainerType = VideoContainer.MKV
        End If

        'Video Type
        If VidInfo.Contains("h264") Then
            VideoType = VideoCodec.h264
        ElseIf VidInfo.Contains("vp8") Then
            VideoType = VideoCodec.VP8
        ElseIf VidInfo.Contains("mpeg2video") Then
            VideoType = VideoCodec.mpeg2
        End If

        'Audio Type
        If VidInfo.Contains("aac") Then
            AudioType = AudioCodec.aac
        ElseIf VidInfo.Contains("ac3") Then
            AudioType = AudioCodec.ac3
        ElseIf VidInfo.Contains("mp3") Then
            AudioType = AudioCodec.mp3
        ElseIf VidInfo.Contains("vorbis") Then
            AudioType = AudioCodec.vorbis
        End If

    End Sub
End Class

Public Enum VideoContainer As Byte
    MP4 = 0
    WebM = 1
    Mpegts = 2
    AVI = 3
    MKV = 4
    Other = 9 ' only 0 and 1 really matter
End Enum

Public Enum VideoCodec As Byte
    h264 = 0
    mpeg2 = 1
    VP8 = 2
    other = 9
End Enum

Public Enum AudioCodec As Byte
    mp3 = 0
    aac = 1
    ac3 = 2
    vorbis = 3
    other = 9
End Enum