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

'    Copyright 2012, 2013 Jonathan Heizer jheizer@gmail.com
#End Region

Imports MythContent
Imports MythService
Imports MythDVR
Imports MythVideo

Partial Class tablet_videos
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.PageTitle = "Videos"

        Try
            Dim Videos As MythVideo.VideoMetadataInfoList
            Videos = WSCache.Video.GetVideoList(True, 0, 10000)  'Change the service xml to xs:string for AddDate
            Dim Sorted As List(Of VideoMetadataInfo) = (From v In Videos.VideoMetadataInfos
                                               Order By v.Title
                                               Select v).ToList
            Dim Vid As VideoMetadataInfo
            For i As Integer = 0 To Sorted.Count - 1
                Vid = Sorted(i)
                Dim Li As New VideoPanel(i, Vid.Title, "video.aspx?id=" & Vid.Id, Common.GetServiceUrl & "/Content/GetVideoArtwork?Id=" & Vid.Id & "&Type=coverart&Height=225")
                maincontent.Controls.Add(Li)
            Next

        Catch ex As Exception
        End Try

        'This was needed before I changes teh service references to treat the dates for videos as strings
        'Dim index As Integer = 0
        'Dim RealCount As Integer = 0
        'Dim Count As Integer = 1

        'While index < Count
        '    Try
        '        Videos = WSCache.Video.GetVideoList(True, index, 1)
        '        Dim Li As New VideoPanel(GetCss(RealCount), Videos.VideoMetadataInfos(0).Title, "videos.aspx?id=" & Videos.VideoMetadataInfos(0).Id, "http://10.0.0.197:6544/Content/GetVideoArtwork?Id=" & Videos.VideoMetadataInfos(0).Id & "&Type=coverart&Height=225")
        '        maincontent.Controls.Add(Li)

        '        Count = Videos.TotalAvailable - 1
        '        RealCount += 1
        '    Catch ex As Exception
        '    End Try
        '    index += 1

        'End While

    End Sub


End Class