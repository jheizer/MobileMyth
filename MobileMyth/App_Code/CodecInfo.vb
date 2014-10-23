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

'    Copyright 2014 Jonathan Heizer jheizer@gmail.com
#End Region

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