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

Imports MythDVR
Imports MythContent

Partial Class viewstream
    Inherits System.Web.UI.Page

    Private Rec As Program

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim Url As String = HttpUtility.UrlDecode(Request.QueryString("url"))
        Dim lit As New LiteralControl
        lit.Text = "<video width=""100%"" height=""" & Resolutions.MyResolution.Height & """ controls=""controls""><source src=""" & Url & """></video>"
        maincontent.Controls.Add(lit)

        Dim brs As New LiteralControl
        brs.Text = "<br><br>"
        maincontent.Controls.Add(brs)

        Dim Lnk As New HyperLink
        Lnk.Text = "External Player URL"
        Lnk.NavigateUrl = Url
        Lnk.Attributes.Add("data-role", "button")
        maincontent.Controls.Add(Lnk)


        If Request.QueryString("type") = "r" Then
            Dim ChanId As Integer = Integer.Parse(Request.QueryString("chan"))
            Dim Time As Long = Long.Parse(Request.QueryString("time"))
            Dim StartTime As New DateTime(Time)

            Rec = WSCache.DVR.GetRecorded(ChanId, StartTime)

            DeleteTitle.Text = "Delete Recording?"
            DeleteDetails.Text = Rec.Title & "<br>" & Rec.SubTitle
        End If
    End Sub

    Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        If Not Rec Is Nothing Then
            Dim Streams As LiveStreamInfoList = WSCache.Content.GetFilteredLiveStreamList(Rec.FileName)

            For Each Str As LiveStreamInfo In Streams.LiveStreamInfos
                WSCache.Content.RemoveLiveStream(Str.Id)
            Next

            If WSCache.DVR.RemoveRecorded(Rec.Channel.ChanId, Rec.Recording.StartTs) Then
                Response.Redirect("confirmation.aspx?msg=1", False)
            End If
        End If
    End Sub

End Class

