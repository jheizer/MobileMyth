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

    Public Function AddRecordingLiveStream(ChanId As Integer, StartTime As Date, Maxsegments As Integer, Width As Integer, Height As Integer, BitRate As Integer, AudioBitrate As Integer, SampleRate As Integer) As iMythContent.LiveStreamInfo Implements iMythContent.AddRecordingLiveStream
        Dim Str As MythContent.LiveStreamInfo = Content.AddRecordingLiveStream(ChanId, StartTime, Maxsegments, Width, Height, BitRate, AudioBitrate, SampleRate)
        Return Common.ConvertTypes(Of MythContent.LiveStreamInfo, iMythContent.LiveStreamInfo)(Str)
    End Function

    Public Function GetFilteredStreamList(FileName As String) As List(Of iMythContent.LiveStreamInfo) Implements iMythContent.GetFilteredStreamList
        Dim ret As New List(Of iMythContent.LiveStreamInfo)

        For Each live As MythContent.LiveStreamInfo In Content.GetFilteredLiveStreamList(FileName).LiveStreamInfos
            ret.Add(Common.ConvertTypes(Of MythContent.LiveStreamInfo, iMythContent.LiveStreamInfo)(live))
        Next

        Return ret
    End Function

    Public Function RemoveLiveStream(Id As Integer) As Boolean Implements iMythContent.RemoveLiveStream
        Return Content.RemoveLiveStream(Id)
    End Function

    Public Function AddVideoLiveStream(Id As Integer, MaxSegments As Integer, Width As Integer, Height As Integer, Bitrate As Integer, AudioBitrate As Integer, SampleRate As Integer) As iMythContent.LiveStreamInfo Implements iMythContent.AddVideoLiveStream
        Dim Str As MythContent.LiveStreamInfo = Content.AddVideoLiveStream(Id, MaxSegments, Width, Height, Bitrate, AudioBitrate, SampleRate)
        Return Common.ConvertTypes(Of MythContent.LiveStreamInfo, iMythContent.LiveStreamInfo)(Str)
    End Function
End Class
