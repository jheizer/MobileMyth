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

'    Copyright 2012-2014 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic
Imports MythDVR

Public Class RecordingListItem
    Inherits HtmlListItem

    Public Sub New(ByVal rec As Program)

        Dim Link As New HyperLink
        Link.NavigateUrl = "recording.aspx?chan=" & rec.Channel.ChanId & "&time=" & rec.Recording.StartTs.Value.Ticks
        Link.Attributes.Add("data-ajax", "false")

        'Add the thumbnail preview image
        If Not SiteSettings.FrontendSettingBool("NoImages") Then
            Dim img As New Image
            img.ImageUrl = "../images/loader.gif"
            img.Attributes.Add("data-src", Common.ProxyURL("/Content/GetPreviewImage?ChanId=" & rec.Channel.ChanId & "&StartTime=" & _
                            rec.Recording.StartTs.Value.ToString("yyyy-MM-ddTHH:mm:ssZ") & _
                            "&Height=80"))
            Link.Controls.Add(img)
        End If


        Dim lit As New LiteralControl
        Dim data As New Text.StringBuilder

        data.Append("<h3>")
        data.Append(rec.Title)
        data.Append("</h3><p><strong>")
        data.Append(rec.SubTitle)
        data.Append("</strong></p><p>")
        data.Append(rec.Description)
        data.Append("</p><p class=""ui-li-aside""><strong>")
        data.Append(rec.Recording.StartTs.Value.ToLocalTime.ToString("h:mm tt"))
        data.Append("</strong></p>")

        lit.Text = data.ToString

        Link.Controls.Add(lit)

        Me.Controls.Add(Link)
    End Sub
End Class

Public Class UpcomingListItem
    Inherits HtmlListItem

    Public Sub New(ByVal Rec As Program)
        Me.New(Rec, True)
    End Sub

    Public Sub New(ByVal Rec As Program, ShowDescription As Boolean)
        Dim Builder As New Text.StringBuilder

        Builder.Append("<h3>")
        Builder.Append(Rec.Title)
        Builder.Append("</h3><p>")
        Builder.Append(Rec.SubTitle)
        Builder.Append("</p>")
        If ShowDescription Then
            Builder.Append("<p>")
            Builder.Append(Rec.Description)
            Builder.Append("</p>")
        End If
        Builder.Append("<p class=""ui-li-aside"">")
        Builder.Append(Rec.Recording.StartTs.Value.ToLocalTime.ToString("h:mm tt"))
        Builder.Append(" - ")
        Builder.Append(Rec.Recording.EndTs.Value.ToLocalTime.ToString("h:mm tt"))
        Builder.Append("</p>")

        Dim lit As New LiteralControl
        lit.Text = Builder.ToString

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
            If Not SiteSettings.FrontendSettingBool("NoImages") Then
                Dim Img As New Image
                Img.ImageUrl = Common.ProxyURL("/Content/GetRecordingArtwork?Inetref=" & InetRef & "&Type=banner&Height=82&Season=" & Season)
                Img.AlternateText = Title
                Img.Style.Add("max-width", "100%")
                Link.Controls.Add(Img)
            End If
        End If

        Dim lit As New LiteralControl
        lit.Text = "<h3>" & Title & "</h3>" & _
                   "<span class=""ui-li-count"">" & Count & "</span>"
        '        lit.Text = Title & "<span class=""ui-li-count"">" & Count & "</span>"
        Link.Controls.Add(lit)

        Me.Controls.Add(Link)
    End Sub
End Class

Public Class TiledPanel
    Inherits Panel

    Public Sub New(ByVal index As Integer, ByVal Title As String, ByVal URL As String, ByVal CoverUrl As String)
        Me.CssClass = GetCss(index)

        Dim InnerPan As New Panel
        InnerPan.Style.Add("text-align", "center")
        InnerPan.Style.Add("position", "relative")
        Me.Controls.Add(InnerPan)

        Dim Link As New HyperLink
        Link.NavigateUrl = URL

        Dim Img As New Image
        Img.ImageUrl = "../images/loader.gif"
        Img.Attributes.Add("data-src", CoverUrl)
        Img.AlternateText = Title
        Img.Style.Add("max-width", "100%")
        Img.Style.Add("height", "225px")
        Link.Controls.Add(Img)

        If Not String.IsNullOrEmpty(Title) Then
            Dim lit As New Label
            lit.Text = Title
            lit.CssClass = "titlelabel"
            Link.Controls.Add(lit)
        End If

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


'In case these need to be changed later
Public Class VideoPanel
    Inherits TiledPanel

    Public Sub New(ByVal index As Integer, ByVal Title As String, ByVal URL As String, ByVal CoverUrl As String)
        MyBase.New(index, Title, URL, CoverUrl)
    End Sub

End Class

Public Class GalleryPanel
    Inherits TiledPanel

    Public Sub New(ByVal index As Integer, ByVal URL As String, ByVal CoverUrl As String)
        MyBase.New(index, "", URL, CoverUrl)
    End Sub

    Public Sub New(ByVal index As Integer, ByVal Title As String, ByVal URL As String, ByVal CoverUrl As String)
        MyBase.New(index, Title, URL, CoverUrl)
    End Sub
End Class
