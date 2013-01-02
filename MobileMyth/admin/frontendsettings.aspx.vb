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

Partial Class admin_frontendsettings
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        uitype.SelectedValue = SiteSettings.FrontendSetting("UIType")

        For Each Name As String In Resolutions.ResolutionNames
            VideoQuality.Items.Add(Name)
        Next

        If String.IsNullOrEmpty(SiteSettings.FrontendSetting("Resolution")) Then
            VideoQuality.SelectedValue = "540p"
        Else
            VideoQuality.SelectedValue = SiteSettings.FrontendSetting("Resolution")
        End If

        ServiceServer.Text = SiteSettings.FrontendSetting("ServiceServer")
        ServicePort.Text = SiteSettings.FrontendSetting("ServicePort")

        ShowTabletSettings()
    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        SiteSettings.FrontendSetting("UIType") = uitype.SelectedValue
        SiteSettings.FrontendSetting("Resolution") = VideoQuality.SelectedValue
        SiteSettings.FrontendSetting("ServiceServer") = ServiceServer.Text
        SiteSettings.FrontendSetting("ServicePort") = ServicePort.Text

        If SiteSettings.FrontendSetting("UIType") = "tablet" Then
            SiteSettings.FrontendSettingBool("ShowRecentRecordings") = RecentRecordings.Checked
            SiteSettings.FrontendSettingBool("ShowRecentVideos") = Recentvideos.Checked
            SiteSettings.FrontendSettingBool("ShowConflicts") = Conflicts.Checked
            SiteSettings.FrontendSettingBool("ShowDiskSpace") = DiskSpace.Checked
            SiteSettings.FrontendSettingBool("ShowEncoders") = Encoders.Checked
            SiteSettings.FrontendSettingBool("ShowUpcoming") = Upcoming.Checked
        End If

        Response.Redirect("frontendsettings.aspx", False) 'Not sure what is going on here...
    End Sub

    Private Sub ShowTabletSettings()
        If SiteSettings.FrontendSetting("UIType") = "tablet" Then
            TabletSettings.Visible = True
            TabletDefaults()

            RecentRecordings.Checked = SiteSettings.FrontendSettingBool("ShowRecentRecordings")
            Recentvideos.Checked = SiteSettings.FrontendSettingBool("ShowRecentVideos")
            Conflicts.Checked = SiteSettings.FrontendSettingBool("ShowConflicts")
            DiskSpace.Checked = SiteSettings.FrontendSettingBool("ShowDiskSpace")
            Encoders.Checked = SiteSettings.FrontendSettingBool("ShowEncoders")
            Upcoming.Checked = SiteSettings.FrontendSettingBool("ShowUpcoming")
        End If
    End Sub


    Private Sub TabletDefaults()
        If String.IsNullOrEmpty(SiteSettings.FrontendSetting("ShowRecentRecordings")) Then
            SiteSettings.FrontendSettingBool("ShowRecentRecordings") = True
            SiteSettings.FrontendSettingBool("ShowRecentVideos") = False
            SiteSettings.FrontendSettingBool("ShowConflicts") = True
            SiteSettings.FrontendSettingBool("ShowDiskSpace") = True
            SiteSettings.FrontendSettingBool("ShowEncoders") = True
            SiteSettings.FrontendSettingBool("ShowUpcoming") = False
        End If
    End Sub


End Class
