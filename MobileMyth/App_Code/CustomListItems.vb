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

Imports Microsoft.VisualBasic
Imports MythDVR

Public Class RecordingListItem
    Inherits HtmlListItem

    Public Sub New(ByVal rec As Program)
        Dim lit As New LiteralControl
        lit.Text = "<h3>" & rec.Title & "</h3><p><strong>" & rec.SubTitle & "</strong></p><p>" & _
            rec.Description & "</p><p class=""ui-li-aside""><strong>" & rec.Recording.StartTs.Value.ToShortTimeString & "</strong></p>"

        Dim Link As New HyperLink
        Link.NavigateUrl = "recording.aspx?chan=" & rec.Channel.ChanId & "&time=" & rec.Recording.StartTs.Value.Ticks
        Link.Attributes.Add("data-ajax", "false")
        Link.Controls.Add(lit)

        Me.Controls.Add(Link)
    End Sub
End Class

Public Class UpcomingListItem
    Inherits HtmlListItem

    Public Sub New(ByVal Rec As Program)
        Dim time As String = Convert.ToDateTime(Rec.Recording.StartTs).ToLocalTime.ToShortTimeString & " - " & Convert.ToDateTime(Rec.Recording.EndTs).ToLocalTime.ToShortTimeString
        Dim lit As New LiteralControl
        lit.Text = "<h3>" & Rec.Title & "</h3><p><strong>" & Rec.SubTitle & "</strong></p><p>" & Rec.Description & _
                   "</p><p class=""ui-li-aside""><strong>" & Time & "</strong></p>"

        Dim Epoch As TimeSpan = (Rec.Recording.StartTs.Value - New DateTime(1970, 1, 1))

        Dim Lnk As New HyperLink
        Lnk.NavigateUrl = "mythweb.aspx?url=" & HttpUtility.UrlEncode("tv/detail/" & Rec.Channel.ChanId & "/" & Epoch.TotalSeconds)
        Lnk.Controls.Add(lit)

        Me.Controls.Add(Lnk)
    End Sub
End Class

Public Class ShowListItem
    Inherits HtmlListItem

    Public Sub New(ByVal Title As String, ByVal Count As Integer, ByVal URL As String, ByVal InetRef As String, ByVal Season As String)
        Dim Link As New HyperLink
        Link.NavigateUrl = URL

        If Not Title = "All Programs" AndAlso Not String.IsNullOrEmpty(InetRef) Then
            Dim Img As New Image
            Img.ImageUrl = "http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & "/Content/GetRecordingArtwork?Inetref=" & InetRef & "&Type=banner&Height=82&Season=" & Season
            Img.AlternateText = Title
            Img.Style.Add("max-width", "100%")
            Link.Controls.Add(Img)
        End If

        Dim lit As New LiteralControl
        lit.Text = "<h3>" & Title & "</h3>" & _
                   "<span class=""ui-li-count"">" & Count & "</span>"
        '        lit.Text = Title & "<span class=""ui-li-count"">" & Count & "</span>"
        Link.Controls.Add(lit)

        Me.Controls.Add(Link)
    End Sub
End Class

Public Class VideoPanel
    Inherits Panel

    Public Sub New(ByVal index As Integer, ByVal Title As String, ByVal URL As String, ByVal CoverUrl As String)
        Me.CssClass = GetCss(index)

        Dim InnerPan As New Panel
        InnerPan.CssClass = "ui-bar"
        Me.Controls.Add(InnerPan)

        Dim Link As New HyperLink
        Link.NavigateUrl = URL

        Dim Img As New Image
        Img.ImageUrl = CoverUrl
        Img.AlternateText = Title
        Img.Style.Add("max-width", "100%")
        Img.Style.Add("height", "225px")
        Link.Controls.Add(Img)

        Dim lit As New LiteralControl
        lit.Text = "<br><h3>" & Title & "</h3>"
        Link.Controls.Add(lit)

        InnerPan.Controls.Add(Link)
    End Sub

    Private Function GetCss(ByVal index As Integer) As String
        Select Case index Mod 5
            Case Is = 0
                Return "ui-block-a"
            Case Is = 1
                Return "ui-block-b"
            Case Is = 2
                Return "ui-block-c"
            Case Is = 3
                Return "ui-block-d"
            Case Is = 4
                Return "ui-block-e"
        End Select

        Return "ui-block-a"
    End Function
End Class

