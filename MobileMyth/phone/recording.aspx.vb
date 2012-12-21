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

'    Copyright 2012 Jonathan Heizer jheizer@gmail.com
#End Region

Imports MythContent
Imports MythService
Imports MythDVR

Partial Class recording
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim Id As String = Request.QueryString("id")

        If Not Id Is Nothing Then

            Dim Recordings As ProgramList = WSCache.GetRecordedList
            Dim rec As Program = Recordings.Programs.ToList.Find(Function(r) r.ProgramId = Id)

            'If we have a number lets try to display a banner
            If Not String.IsNullOrEmpty(rec.Inetref) Then
                bannerimage.ImageUrl = "http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & "/Content/GetRecordingArtwork?Inetref=" & rec.Inetref & "&Type=banner&Season=" & rec.Season
                bannerimage.Visible = True
            End If

            Dim TotalMinutes As String = ""
            Try
                TotalMinutes = "   (" & Convert.ToDateTime(rec.EndTime).Subtract(Convert.ToDateTime(rec.StartTime)).TotalMinutes & " Minutes)"
            Catch ex As Exception
            End Try

            EpisodeTitle.Text = rec.Title
            EpisodeSubTitle.Text = rec.SubTitle & TotalMinutes
            EpisodeDescription.Text = rec.Description

            WatchNowLink.NavigateUrl = "startstream.aspx?id=" & rec.ProgramId
            DownloadLink.NavigateUrl = "http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & "/Content/GetRecording?ChanId=34736&StartTime=2011-08-29T18:59:00"
            DeleteLink.NavigateUrl = "http://google.com"
            DeleteButton.PostBackUrl = "recording.aspx?id=" & Id

        End If

    End Sub

    Protected Sub DeleteButton_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        'delete code here then
        Response.Redirect("confirmation.aspx?msg=1", False)
    End Sub
End Class

