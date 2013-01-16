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

Partial Class tablet_video
    Inherits System.Web.UI.Page

    Private vid As VideoMetadataInfo

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim Id As String = Request.QueryString("id")

        If Not Id Is Nothing Then

            vid = WSCache.Video.GetVideo(Id)

            'If we have a number lets try to display a banner
            If Not String.IsNullOrEmpty(vid.Inetref) Then
                coverimage.ImageUrl = Common.ProxyURL("/Content/GetVideoArtwork?Id=" & vid.Id & "&Type=coverart&Height=300")
                coverimage.Visible = True
                'ui-content
                'background-color: rgba(255,255,255,0.5);
                Master.ContentDiv.Style.Add("background", "url('" & Common.ProxyURL("/Content/GetRecordingArtwork?Inetref=" & vid.Inetref & "&Type=fanart&Season=" & vid.Season) & "') no-repeat center top;")
                Master.ContentDiv.Style.Add("background-color", " rgba(255,255,255,0.5)")
            End If

            Dim TotalMinutes As String = ""
            Try
                TotalMinutes = "  (" & vid.Length.ToString & " Minutes)"
            Catch ex As Exception
            End Try

            VideoTitle.Text = vid.Title
            VideoSubTitle.Text = vid.SubTitle & TotalMinutes
            VideoDescription.Text = vid.Description

            WatchNowLink.NavigateUrl = "startstream.aspx?type=v&vid=" & vid.Id
            DownloadLink.NavigateUrl = Common.GetServiceUrl & "/Content/GetRecording?ChanId=34736&StartTime=2011-08-29T18:59:00"
            DownloadLink.Visible = False

            Master.PageTitle = vid.Title
        End If

    End Sub

End Class


