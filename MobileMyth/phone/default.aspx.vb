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

'    Copyright 2012,-2014 Jonathan Heizer jheizer@gmail.com
#End Region

Imports MythService
Imports MythDVR
Imports System.Xml

Partial Class _default
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '    DisplayRecentSlider()
        '    DisplayVideosSlider()
        '    DisplayConflicts()
        '    DisplayEncoders()
        '    DisplayFreeSpace()
        DisplayUpcoming()
    End Sub


    'Private Sub DisplayRecentSlider()


    '        Dim Recordings As ProgramList = Nothing
    '        Recordings = Common.MBE.DvrAPI.GetRecordedList(True, 0, 20, False)

    '        For Each Prog As Program In Array.FindAll(Recordings.Programs, Function(p) p.Recording.StorageGroup <> "Deleted" AndAlso p.Recording.StorageGroup <> "LiveTV")
    '            Dim Slide As New Panel
    '            Slide.CssClass = "slide"

    '            Dim Lnk As New HyperLink
    '            Lnk.NavigateUrl = "recording.aspx?chan=" & Prog.Channel.ChanId & "&time=" & Prog.Recording.StartTs.Value.Ticks
    '            Lnk.Attributes.Add("data-ajax", "false")

    '            Dim img As New Image
    '            img.ImageUrl = "../images/loader.gif"
    '            img.Attributes.Add("data-src", Common.ProxyURL("/Content/GetPreviewImage?ChanId=" & Prog.Channel.ChanId & "&StartTime=" & _
    '                            Prog.Recording.StartTs.Value.ToString("yyyy-MM-ddTHH:mm:ssZ") & _
    '                            "&Height=200"))
    '            Lnk.Controls.Add(img)

    '            Dim EpisodeNumber As String = ""
    '            If Prog.Season > 0 Then
    '                EpisodeNumber = Prog.Season.ToString & "x" & Prog.Episode.ToString("00")
    '            End If
    '            Dim Lit As New LiteralControl
    '            Lit.Text = "<h4 style=""margin: 0px;"">" & Prog.Title & " " & EpisodeNumber & "</h4>"
    '            Lnk.Controls.Add(Lit)

    '            Slide.Controls.Add(Lnk)
    '            RecordingsSlideHolder.Controls.Add(Slide)
    '        Next

    'End Sub

    'Private Sub DisplayVideosSlider()

    '    If SiteSettings.FrontendSettingBool("ShowRecentVideos") Then
    '        RecentVideosPanel.Visible = True

    '        Dim Videos As List(Of iMythVideo.VideoMetadataInfo)
    '        Videos = Common.MBE.VideoAPI.GetVideoList(True, 0, 25)  'Change the service xml to xs:string for AddDate

    '        For Each Vid As iMythVideo.VideoMetadataInfo In Videos
    '            Dim Slide As New Panel
    '            Slide.CssClass = "slide"

    '            Dim Lnk As New HyperLink
    '            Lnk.NavigateUrl = "video.aspx?id=" & Vid.Id
    '            Lnk.Attributes.Add("data-ajax", "false")

    '            Dim img As New Image 'http://BackendServerIP:6544/Content/GetVideoArtwork?Id=100&Type=coverart
    '            img.ImageUrl = Common.ProxyURL("/Content/GetVideoArtwork?Id=" & Vid.Id & "&Type=coverart&Height=200")
    '            img.Height = 200
    '            img.Width = 134
    '            img.AlternateText = "Missing CoverArt"
    '            Lnk.Controls.Add(img)

    '            Dim EpisodeNumber As String = ""
    '            If Vid.Season > 0 Then
    '                EpisodeNumber = Vid.Season.ToString & "x" & Vid.Episode.ToString("00")
    '            End If
    '            Dim Lit As New LiteralControl
    '            Lit.Text = "<h4 style=""margin: 0px;"">" & Vid.Title & " " & EpisodeNumber & "</h4>"
    '            Lnk.Controls.Add(Lit)

    '            Slide.Controls.Add(Lnk)
    '            VideosSliderHolder.Controls.Add(Slide)
    '        Next
    '    End If
    'End Sub

    'Private Sub DisplayConflicts()
    '    If SiteSettings.FrontendSettingBool("ShowConflicts") Then
    '        ConflictsPanel.Visible = True

    '        Dim Cons As ProgramList = Common.MBE.DvrAPI.GetConflictList(0, 500)
    '        Dim Li As New HtmlListItem

    '        'If none, say so
    '        If Cons Is Nothing Then
    '            Li.InnerText = "Error Loading"
    '            Conflicts.Controls.Add(Li)
    '        ElseIf Cons.Count = 0 Then
    '            Li.InnerText = "No Conflicts"
    '            Conflicts.Controls.Add(Li)
    '        Else
    '            For Each Con As Program In Cons.Programs
    '                Li = New HtmlListItem
    '                Li.InnerText = Con.Title
    '                If Not String.IsNullOrEmpty(Con.SubTitle) Then
    '                    Li.InnerText &= " - " & Con.SubTitle
    '                End If
    '                Conflicts.Controls.Add(Li)
    '            Next
    '        End If
    '    End If
    'End Sub

    'Private Sub DisplayEncoders()
    '    If SiteSettings.FrontendSettingBool("ShowEncoders") Then
    '        EncodersPanel.Visible = True
    '        Dim Encods As EncoderList = Common.MBE.DvrAPI.GetEncoderList
    '        Dim Li As New HtmlListItem

    '        If Not Encods Is Nothing Then
    '            For Each Enc As Encoder In Encods.Encoders
    '                Li = New HtmlListItem
    '                If Enc.State = 7 Then
    '                    Li.InnerText = Enc.Id & " is recording " & Enc.Recording.Title & " till " & Enc.Recording.Recording.EndTs.Value.ToLocalTime.ToString("hh:mm tt")
    '                Else
    '                    Li.InnerText = Enc.Id & " is not recording"
    '                End If
    '                Encoders.Controls.Add(Li)
    '            Next
    '        Else
    '            Li.InnerText = "Error Loading"
    '            Encoders.Controls.Add(Li)
    '        End If
    '    End If
    'End Sub

    'Private Sub DisplayFreeSpace()
    '    If SiteSettings.FrontendSettingBool("ShowDiskSpace") Then
    '        DiskSpacePanel.Visible = True
    '        Dim WC As New Net.WebClient
    '        Dim xml As String = WC.DownloadString(Common.GetServiceUrl & "/Status/GetStatus")

    '        Dim Doc As New XmlDocument
    '        Doc.LoadXml(xml)

    '        Dim Nd As XmlNode = Doc.SelectSingleNode("//Group[@dir='TotalDiskSpace']")

    '        Dim Free As Integer = Integer.Parse(Nd.Attributes("free").InnerText)
    '        Dim Total As Integer = Integer.Parse(Nd.Attributes("total").InnerText)
    '        Dim Used As Integer = Integer.Parse(Nd.Attributes("used").InnerText)
    '        Dim Perc As Decimal = Used / Total

    '        Dim Pro As New ProgressBar((Perc * 100).ToString("##") & "% Used &nbsp;&nbsp;&nbsp;" & Common.FormatSizes(Free) & " Free &nbsp;&nbsp;&nbsp;" & Common.FormatSizes(Total) & " Total", Perc * 100)
    '        If Perc > 0.9 Then
    '            pro = New ProgressBar((Perc * 100).ToString("##") & "% Used &nbsp;&nbsp;&nbsp;" & Common.FormatSizes(Free) & " Free &nbsp;&nbsp;&nbsp;" & Common.FormatSizes(Total) & " Total", Perc * 100, "red")
    '        ElseIf Perc > 0.8 Then
    '            pro = New ProgressBar((Perc * 100).ToString("##") & "% Used &nbsp;&nbsp;&nbsp;" & Common.FormatSizes(Free) & " Free &nbsp;&nbsp;&nbsp;" & Common.FormatSizes(Total) & " Total", Perc * 100, "yellow")
    '        End If

    '        DiskSpacePanel.Controls.Add(Pro)
    '    End If

    'End Sub

    Private Sub DisplayUpcoming()
        Dim Recordings As ProgramList = Common.MBE.DvrAPI.GetUpcomingList(0, 10, False)
        '        Dim StartDate As String = DateTime.MaxValue.ToLongDateString

        For Each Rec As Program In Recordings.Programs
            'If Rec.StartTime.Value.ToLongDateString <> StartDate Then
            '    Dim Divider As New HtmlListItem
            '    Divider.Attributes.Add("data-role", "list-divider")
            '    Divider.InnerText = Rec.StartTime.Value.ToLongDateString
            '    List.Controls.Add(Divider)
            '    StartDate = Rec.StartTime.Value.ToLongDateString
            'End If

            'Dim LI As New UpcomingListItem(Rec, False)

            Dim LI As New HtmlListItem
            LI.InnerHtml = "<strong>" & Rec.Title & " - " & Rec.StartTime.Value.ToLocalTime.ToShortDateString & " " & Rec.Recording.StartTs.Value.ToLocalTime.ToString("h:mm tt") & _
                           "</strong><p>" & Rec.SubTitle & "</p>"
            upcomingdetails.Controls.Add(LI)
        Next

    End Sub
End Class
