﻿#Region "GPL"
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



Partial Class viewstream
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(viewstream))

    Private Rec As iMythDvr.Program

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim Url As String = Request.QueryString("url")

        If SiteSettings.FrontendSettingBool("ProxyVideo") Then
            Url = Common.ProxyURL(Url)
        Else
            Url = Common.GetServiceUrl & Url
        End If

        Dim PlayerHeight As String = Resolutions.MyResolution.Height
        If CInt(PlayerHeight) > 720 Then
            PlayerHeight = "720"
        End If

        Dim lit As New LiteralControl

        If SiteSettings.FrontendSetting("UIType") = "desktop" Then
            lit.Text = "<div id=""playerwrapper""><div id='player'></div></div><script type=""text/javascript"">playStreamInJWPlayer(""" & Url & """, 1000 ," & PlayerHeight & ");</script>"
        Else
            lit.Text = "<video width=""100%"" height=""" & PlayerHeight & """ controls=""controls""><source src=""" & Url & """></video>"
        End If

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

            Rec = Common.MBE.DvrAPI.GetRecorded(ChanId, StartTime)

            DeleteTitle.Text = "Delete Recording?"
            DeleteDetails.Text = Rec.Title & "<br>" & Rec.SubTitle
        End If
    End Sub

    Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        If Not Rec Is Nothing Then
            Dim Streams As List(Of iMythContent.LiveStreamInfo) = Common.MBE.ContentAPI.GetFilteredStreamList(Rec.FileName)

            Logger.Info("Deleting Livestreams: " & Rec.FileName)
            For Each Str As iMythContent.LiveStreamInfo In Streams
                Common.MBE.ContentAPI.RemoveLiveStream(Str.Id)
            Next

            If Common.MBE.DvrAPI.RemoveRecorded(Rec.Channel.ChanId, Rec.Recording.StartTs) Then
                Logger.Info("Recording Deleted: Chan-" & Rec.Channel.ChanId & "StartTs-" & Rec.Recording.StartTs.ToString)
                Response.Redirect("confirmation.aspx?msg=1", False)
            End If
        End If
    End Sub

End Class

