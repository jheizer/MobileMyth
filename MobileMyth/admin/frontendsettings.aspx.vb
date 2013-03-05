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

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(admin_frontendsettings))
    'http://10.0.0.197:6544/3rdParty/jwplayer.qsp
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

        UseAnyStream.Checked = SiteSettings.FrontendSettingBool("UseAnyStream")
        ProxyVideo.Checked = SiteSettings.FrontendSettingBool("ProxyVideo")
        NoImages.Checked = SiteSettings.FrontendSettingBool("NoImages")

        'Let the device be reported
        DeviceLog.Visible = SiteSettings.SettingBool("DeviceList", True)

        ShowTabletSettings()
    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        Try

            SiteSettings.FrontendSetting("UIType") = uitype.SelectedValue
            SiteSettings.FrontendSetting("Resolution") = VideoQuality.SelectedValue
            SiteSettings.FrontendSettingBool("UseAnyStream") = UseAnyStream.Checked
            SiteSettings.FrontendSettingBool("ProxyVideo") = ProxyVideo.Checked

            If SiteSettings.FrontendSetting("UIType") = "tablet" Then
                SiteSettings.FrontendSettingBool("ShowRecentRecordings") = RecentRecordings.Checked
                SiteSettings.FrontendSettingBool("ShowRecentVideos") = Recentvideos.Checked
                SiteSettings.FrontendSettingBool("ShowConflicts") = Conflicts.Checked
                SiteSettings.FrontendSettingBool("ShowDiskSpace") = DiskSpace.Checked
                SiteSettings.FrontendSettingBool("ShowEncoders") = Encoders.Checked
                SiteSettings.FrontendSettingBool("ShowUpcoming") = Upcoming.Checked

                SiteSettings.FrontendSettingBool("NoImages") = NoImages.Checked
            End If

            Response.Redirect("frontendsettings.aspx", False)

        Catch ex As Exception
            Logger.Error(ex.ToString)
        End Try
    End Sub

    Private Sub ShowTabletSettings()
        If SiteSettings.FrontendSetting("UIType") = "tablet" Then
            TabletSettings.Visible = True

            RecentRecordings.Checked = SiteSettings.FrontendSettingBool("ShowRecentRecordings", True)
            Recentvideos.Checked = SiteSettings.FrontendSettingBool("ShowRecentVideos", False)
            Conflicts.Checked = SiteSettings.FrontendSettingBool("ShowConflicts", True)
            DiskSpace.Checked = SiteSettings.FrontendSettingBool("ShowDiskSpace", True)
            Encoders.Checked = SiteSettings.FrontendSettingBool("ShowEncoders", True)
            Upcoming.Checked = SiteSettings.FrontendSettingBool("ShowUpcoming", False)
        End If
    End Sub

End Class
