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

Public Interface iMythContent
    Inherits iMythAPIService

    Function GetFileList(StorageGroup As String) As String()
    Function AddRecordingLiveStream(ChanId As Integer, StartTime As Date, Maxsegments As Integer, Width As Integer, Height As Integer, BitRate As Integer, AudioBitrate As Integer, SampleRate As Integer) As LiveStreamInfo
    Function AddVideoLiveStream(Id As Integer, MaxSegments As Integer, Width As Integer, Height As Integer, Bitrate As Integer, AudioBitrate As Integer, SampleRate As Integer) As LiveStreamInfo
    Function RemoveLiveStream(Id As Integer) As Boolean
    Function GetFilteredStreamList(FileName As String) As List(Of LiveStreamInfo)

    Class LiveStreamInfo
        Public Property Id As Integer
        Public Property Width As Integer
        Public Property Height As Integer
        Public Property Bitrate As Integer
        Public Property AudioBitrate As Integer
        Public Property SegmentSize As Integer
        Public Property MaxSegments As Integer
        Public Property StartSegment As Integer
        Public Property CurrentSegment As Integer
        Public Property SegmentCount As Integer
        Public Property PercentComplete As Integer
        Public Property Created As DateTime
        Public Property LastModified As DateTime
        Public Property RelativeURL As String
        Public Property FullURL As String
        Public Property StatusStr As String
        Public Property Statusinteger As Integer
        Public Property StatusMessage As String
        Public Property SourceFile As String
        Public Property SourceHost As String
        Public Property SourceWidth As Integer
        Public Property SourceHeight As Integer
        Public Property AudioOnlyBitrate As Integer
    End Class

End Interface
