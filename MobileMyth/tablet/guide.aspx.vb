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

Imports MythGuide

Partial Class tablet_guide
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim Link As New HyperLink
        Link.NavigateUrl = SiteSettings.Setting("MythWebUrl") & "tv/list"
        Link.Text = "Full Guide on MythWeb"
        maincontent.Controls.Add(Link)

        Dim Gd As ProgramGuide = WSCache.Guide.GetProgramGuide(DateTime.Now.ToUniversalTime.ToString("yyyy-MM-ddTHH:mm:ssZ"), _
                                                                        DateTime.Now.ToUniversalTime.ToString("yyyy-MM-ddTHH:mm:ssZ"), _
                                                                        0, 1000, False)
        Dim Chan As ChannelInfo
        For i As Integer = 0 To Gd.Channels.Count - 1
            Chan = Gd.Channels(i)

            Dim block As New Panel
            block.CssClass = "ui-block-a"

            Dim Bar As New Panel
            Bar.CssClass = "ui-bar ui-bar-e"
            Bar.Style.Add("height", "75px")

            Dim Lit As New LiteralControl
            Lit.Text = Chan.CallSign & "<br>" & Chan.ChanNum

            Bar.Controls.Add(Lit)
            block.Controls.Add(Bar)
            GuideList.Controls.Add(block)

            block = New Panel
            block.CssClass = "ui-block-b"

            Bar = New Panel
            Bar.CssClass = "ui-bar ui-bar-e"
            Bar.Style.Add("height", "75px")

            Dim Lnk As New HyperLink
            Lnk.Text = Chan.Programs(0).Title & "<br>" & Chan.Programs(0).SubTitle
            Dim Epoch As TimeSpan = (Chan.Programs(0).StartTime.Value - New DateTime(1970, 1, 1))
            Lnk.NavigateUrl = SiteSettings.Setting("MythWebUrl") & "tv/detail/" & Chan.ChanId & "/" & Epoch.TotalSeconds
            Lnk.Attributes.Add("rel", "external")

            Bar.Controls.Add(Lnk)
            block.Controls.Add(Bar)
            GuideList.Controls.Add(block)
        Next

    End Sub
End Class
