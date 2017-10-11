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

'    Copyright 2012-2014,2017 Jonathan Heizer jheizer@gmail.com
#End Region


Imports MythService

Partial Class recording
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(recording))

    Private Rec As iMythDvr.Program

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try

            Dim ChanId As Integer = Integer.Parse(Request.QueryString("chan"))
            Dim Time As Long = Long.Parse(Request.QueryString("time"))
            Dim StartTime As New DateTime(Time)

            Rec = Common.MBE.DvrAPI.GetRecorded(ChanId, StartTime)

            If Rec Is Nothing Then
                EpisodeTitle.Text = "Error loading recording"
                Logger.Error("Error loading recording: ChanId - " & ChanId & " Starttime - " & Time)
            Else
                ShowRecordingDetails()
            End If

        Catch ex As Exception
            EpisodeTitle.Text = "Error loading recording"
            Logger.Error("Error loading recording" & ControlChars.NewLine & ex.ToString)
        End Try
    End Sub

    Private Sub ShowRecordingDetails()
        If Not Rec Is Nothing Then

            'If we have a number lets try to display a banner
            If Not String.IsNullOrEmpty(Rec.Inetref) AndAlso Not SiteSettings.FrontendSettingBool("NoImages") Then
                coverimage.ImageUrl = Common.ProxyURL("/Content/GetRecordingArtwork?Inetref=" & Rec.Inetref & "&Type=cover&Height=300&Season=" & Rec.Season)
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

            RecordingDate.Text = "Recorded: " & Rec.Recording.StartTs.Value.ToLocalTime.ToString(Common.DateFormat & " H:mm tt")
            OriginalDate.Text = "Originally Aired: " & Rec.Airdate
            FileSize.Text = "File Size: " & Common.FormatSizes(Rec.FileSize / 1048576)

            If Rec.Season > 0 OrElse Rec.Episode > 0 Then
                Episode.Text = "Season " & Rec.Season & " Episode " & Rec.Episode
            End If


            'Display stream info if transcoding has already been started
            Dim Streams As List(Of iMythContent.LiveStreamInfo) = Common.MBE.ContentAPI.GetFilteredStreamList(Rec.FileName)

            For Each Str As iMythContent.LiveStreamInfo In Streams
                TranscodePanel.Visible = True
                Dim Pro As New ProgressBar("Transcoding Progress (" & Resolutions.ResolutionByHeight(Str.Height).Name & "): " & Str.PercentComplete & "%", Str.PercentComplete)
                TranscodePanel.Controls.Add(Pro)
            Next

            WatchNowLink.NavigateUrl = "startstream.aspx?type=r&chan=" & Rec.Channel.ChanId & "&time=" & Rec.Recording.StartTs.Value.Ticks

            'FE Links
            Dim Fe As ListItem
            For Each ky As String In Frontends.Frontends.Keys
                Fe = New ListItem(ky, ky)
                FEList.Items.Add(Fe)
            Next

            PlayFE.Attributes.Add("onclick", "PlayOnFrontend($('#" & FEList.ClientID & "').val(), '" & Rec.Channel.ChanId & "', '" & Rec.Recording.StartTs.Value.ToString("yyyy-MM-ddTHH:mm:ssZ") & "')")

            DownloadLink.NavigateUrl = Common.ProxyURL("/Content/GetRecording?ChanId=" & Rec.Channel.ChanId & "&StartTime=" & Rec.Recording.StartTs.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"))

            'Info for delete popup
            DeleteTitle.Text = "Delete Recording?"
            DeleteDetails.Text = Rec.Title & "<br>" & Rec.SubTitle

            Master.PageTitle = Rec.Title

        End If
    End Sub

    Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        If Not Rec Is Nothing Then
            If Common.MBE.DvrAPI.RemoveRecorded(Rec.Channel.ChanId, Rec.Recording.StartTs) Then
                Logger.Info("Recording Deleted: Chan-" & Rec.Channel.ChanId & " StartTs-" & Rec.Recording.StartTs.ToString)
                Response.Redirect("confirmation.aspx?msg=1", False)
            End If
        End If
    End Sub
End Class


