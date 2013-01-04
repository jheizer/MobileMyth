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

Partial Class admin_general
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(admin_general))

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ServiceIP.Text = SiteSettings.Setting("MythServiceAPIAddress")
        ServicePort.Text = SiteSettings.Setting("MythServiceAPIPort")
        MythWebUrl.Text = SiteSettings.Setting("MythWebUrl")
    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        SiteSettings.Setting("MythServiceAPIAddress") = ServiceIP.Text
        SiteSettings.Setting("MythServiceAPIPort") = ServicePort.Text
        SiteSettings.Setting("MythWebUrl") = MythWebUrl.Text

        If Not SiteSettings.Setting("MythWebUrl").ToLower.StartsWith("http") Then
            SiteSettings.Setting("MythWebUrl") = "http://" & SiteSettings.Setting("MythWebUrl")
        End If

        If Not SiteSettings.Setting("MythWebUrl").EndsWith("/") Then
            SiteSettings.Setting("MythWebUrl") &= "/"
        End If

        If Not WSCache.ReInitServiceReferences() Then
            Logger.Error("Error Connecting to the Backend")
        End If
    End Sub
End Class
