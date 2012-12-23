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

    Private Rec As Program

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim ChanId As Integer = Integer.Parse(Request.QueryString("chan"))
        Dim Time As Long = Long.Parse(Request.QueryString("time"))
        Dim StartTime As New DateTime(Time)

        Rec = WSCache.DVR.GetRecorded(ChanId, StartTime)

        'If we have a number lets try to display a banner
        If Not String.IsNullOrEmpty(Rec.Inetref) Then
            coverimage.ImageUrl = "http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & "/Content/GetRecordingArtwork?Inetref=" & Rec.Inetref & "&Type=cover&Height=300&Season=" & Rec.Season
            coverimage.Visible = True
            'ui-content
            'background-color: rgba(255,255,255,0.5);
            Master.ContentDiv.Style.Add("background", "url('http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & "/Content/GetRecordingArtwork?Inetref=" & Rec.Inetref & "&Type=fanart&Season=" & Rec.Season & "') no-repeat center top;")
            Master.ContentDiv.Style.Add("background-color", " rgba(255,255,255,0.5)")
        End If

        Dim TotalMinutes As String = ""
        Try
            TotalMinutes = "   (" & Rec.EndTime.Value.Subtract(Rec.StartTime.Value).TotalMinutes & " Minutes)"
        Catch ex As Exception
        End Try

        EpisodeTitle.Text = Rec.Title
        EpisodeSubTitle.Text = Rec.SubTitle & TotalMinutes
        EpisodeDescription.Text = Rec.Description

        RecordingDate.Text = "Recorded: " & Rec.Recording.StartTs.Value.ToString
        OriginalDate.Text = "Originally Aired: " & Rec.Airdate

        If Rec.Season > 0 OrElse Rec.Episode > 0 Then
            Episode.Text = "Season " & Rec.Season & " Episode " & Rec.Episode
        End If

        'Display stream info if transcoding has already been started
        Dim Streams As LiveStreamInfoList = WSCache.Content.GetFilteredLiveStreamList(Rec.FileName)
        If Streams.LiveStreamInfos.Count > 0 Then
            TranscodePanel.Visible = True
            transcodinginfo.Text = "Transcoding Progress: " & Streams.LiveStreamInfos(0).PercentComplete & "%"
            progressbarvalue.Style.Add("width", Streams.LiveStreamInfos(0).PercentComplete & "%")
        End If

        WatchNowLink.NavigateUrl = "startstream.aspx?type=r&chan=" & Rec.Channel.ChanId & "&time=" & Rec.Recording.StartTs.Value.Ticks
        DownloadLink.NavigateUrl = "http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & "/Content/GetRecording?ChanId=34736&StartTime=2011-08-29T18:59:00"

        DeleteTitle.Text = "Delete Recording?"
        DeleteDetails.Text = Rec.Title & "<br>" & Rec.SubTitle

        Master.PageTitle = Rec.Title

    End Sub

    Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        If Not Rec Is Nothing Then
            If WSCache.DVR.RemoveRecorded(Rec.Channel.ChanId, Rec.Recording.StartTs) Then
                Response.Redirect("confirmation.aspx?msg=1", False)
            End If
        End If
    End Sub
End Class


