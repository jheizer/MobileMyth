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

Public Class MythVideo27
    Implements iMythVideo

    Private Video As MythVideo_27.VideoClient

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythAPIService.Init
        Video = New MythVideo_27.VideoClient("BasicHttpBinding_Video1", "http://" & Address & ":" & Port & "/Video")
        Return True
    End Function

    Public Function GetVideo(Id As Integer) As iMythVideo.VideoMetadataInfo Implements iMythVideo.GetVideo
        Dim Vid As MythVideo_27.VideoMetadataInfo = Video.GetVideo(Id)

        Dim retvid As iMythVideo.VideoMetadataInfo = Common.ConvertTypes(Of MythVideo_27.VideoMetadataInfo, iMythVideo.VideoMetadataInfo)(vid)
        retvid.Artwork = New List(Of iMythVideo.ArtworkInfo)

        For Each art As MythVideo_27.ArtworkInfo In vid.Artwork.ArtworkInfos
            retvid.Artwork.Add(Common.ConvertTypes(Of MythVideo_27.ArtworkInfo, iMythVideo.ArtworkInfo)(art))
        Next

        Return retvid
    End Function

    Public Function GetVideoList(Descending As Boolean, StartIndex As Integer, Count As Integer) As List(Of iMythVideo.VideoMetadataInfo) Implements iMythVideo.GetVideoList
        Dim ret As New List(Of iMythVideo.VideoMetadataInfo)

        For Each vid As MythVideo_27.VideoMetadataInfo In Video.GetVideoList(Descending, StartIndex, Count).VideoMetadataInfos
            Dim retvid As iMythVideo.VideoMetadataInfo = Common.ConvertTypes(Of MythVideo_27.VideoMetadataInfo, iMythVideo.VideoMetadataInfo)(vid)

            retvid.Artwork = New List(Of iMythVideo.ArtworkInfo)

            For Each art As MythVideo_27.ArtworkInfo In vid.Artwork.ArtworkInfos
                retvid.Artwork.Add(Common.ConvertTypes(Of MythVideo_27.ArtworkInfo, iMythVideo.ArtworkInfo)(art))
            Next

            ret.Add(retvid)
        Next

        Return ret

    End Function
End Class
