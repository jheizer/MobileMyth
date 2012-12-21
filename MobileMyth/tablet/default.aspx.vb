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

Imports MythContent
Imports MythService
Imports MythDVR
Imports System.Xml
Imports MythVideo

Partial Class _default
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        DisplayRecentSlider()
        DisplayVideosSlider()
        DisplayConflicts()
        DisplayEncoders()
        DisplayFreeSpace()
        DisplayUpcoming()
    End Sub

    Private Function FormatSizes(ByVal MB As Integer) As String
        Dim Sz As Integer = Integer.Parse(MB)
        Dim Out As Decimal = 0
        Dim Unit As String = "MB"

        If Sz > 1000000 Then
            Out = Sz / 1000000
            Unit = "TB"
        ElseIf Sz > 1000 Then
            Out = Sz / 1000
            Unit = "GB"
        End If

        Return Out.ToString("#.##") & " " & Unit
    End Function

    Private Sub DisplayRecentSlider()

        If SiteSettings.FrontendSettingBool("ShowRecentRecordings") Then
            RecentRecordingsPanel.Visible = True

            Dim Recordings As ProgramList = Nothing
            Recordings = WSCache.DVR.GetRecordedList(True, 0, 20)

            For Each Prog As Program In Recordings.Programs
                Dim Slide As New Panel
                Slide.CssClass = "slide"

                Dim Lnk As New HyperLink
                Lnk.NavigateUrl = "recording.aspx?chan=" & Prog.Channel.ChanId & "&time=" & Prog.Recording.StartTs.Value.Ticks
                Lnk.Attributes.Add("data-ajax", "false")

                Dim img As New Image
                img.ImageUrl = "http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & _
                                "/Content/GetPreviewImage?ChanId=" & Prog.Channel.ChanId & "&StartTime=" & _
                                Convert.ToDateTime(Prog.Recording.StartTs).ToString("yyyy-MM-ddTHH:mm:ssZ") & _
                                "&Height=200"
                Lnk.Controls.Add(img)

                Dim EpisodeNumber As String = ""
                If Prog.Season > 0 Then
                    EpisodeNumber = Prog.Season.ToString & "x" & Prog.Episode.ToString("00")
                End If
                Dim Lit As New LiteralControl
                Lit.Text = "<h4 style=""margin: 0px;"">" & Prog.Title & " " & EpisodeNumber & "</h4>"
                Lnk.Controls.Add(Lit)

                Slide.Controls.Add(Lnk)
                RecordingsSlideHolder.Controls.Add(Slide)
            Next
        End If
    End Sub

    Private Sub DisplayVideosSlider()

        If SiteSettings.FrontendSettingBool("ShowRecentVideos") Then
            RecentVideosPanel.Visible = True

            Dim Videos As MythVideo.VideoMetadataInfoList
            Videos = WSCache.Video.GetVideoList(True, 0, 25)  'Change the service xml to xs:string for AddDate

            For Each Vid As VideoMetadataInfo In Videos.VideoMetadataInfos
                Dim Slide As New Panel
                Slide.CssClass = "slide"

                Dim Lnk As New HyperLink
                Lnk.NavigateUrl = "video.aspx?id=" & Vid.Id
                Lnk.Attributes.Add("data-ajax", "false")

                Dim img As New Image 'http://BackendServerIP:6544/Content/GetVideoArtwork?Id=100&Type=coverart
                img.ImageUrl = "http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & _
                               "/Content/GetVideoArtwork?Id=" & Vid.Id & "&Type=coverart&Height=200"
                img.Height = 200
                img.Width = 134
                img.AlternateText = "Missing CoverArt"
                Lnk.Controls.Add(img)

                Dim EpisodeNumber As String = ""
                If Vid.Season > 0 Then
                    EpisodeNumber = Vid.Season.ToString & "x" & Vid.Episode.ToString("00")
                End If
                Dim Lit As New LiteralControl
                Lit.Text = "<h4 style=""margin: 0px;"">" & Vid.Title & " " & EpisodeNumber & "</h4>"
                Lnk.Controls.Add(Lit)

                Slide.Controls.Add(Lnk)
                VideosSliderHolder.Controls.Add(Slide)
            Next
        End If
    End Sub

    Private Sub DisplayConflicts()
        If SiteSettings.FrontendSettingBool("ShowConflicts") Then
            ConflictsPanel.Visible = True

            Dim Cons As ProgramList = WSCache.DVR.GetConflictList(0, 500)
            Dim Li As New HtmlListItem

            'If none, say so
            If Cons Is Nothing Then
                Li.InnerText = "Error Loading"
                Conflicts.Controls.Add(Li)
            ElseIf Cons.Count = 0 Then
                Li.InnerText = "No Conflicts"
                Conflicts.Controls.Add(Li)
            Else
                For Each Con As Program In Cons.Programs
                    Li = New HtmlListItem
                    Li.InnerText = Con.Title
                    If Not String.IsNullOrEmpty(Con.SubTitle) Then
                        Li.InnerText &= " - " & Con.SubTitle
                    End If
                    Conflicts.Controls.Add(Li)
                Next
            End If
        End If
    End Sub

    Private Sub DisplayEncoders()
        If SiteSettings.FrontendSettingBool("ShowEncoders") Then
            EncodersPanel.Visible = True
            Dim Encods As EncoderList = WSCache.DVR.GetEncoderList
            Dim Li As New HtmlListItem

            If Not Encods Is Nothing Then
                For Each Enc As Encoder In Encods.Encoders
                    Li = New HtmlListItem
                    If Enc.State = 7 Then
                        Li.InnerText = Enc.Id & " is recording " & Enc.Recording.Title & " till " & Enc.Recording.Recording.EndTs.Value.ToLocalTime.ToShortTimeString
                    Else
                        Li.InnerText = Enc.Id & " is not recording"
                    End If
                    Encoders.Controls.Add(Li)
                Next
            Else
                Li.InnerText = "Error Loading"
                Encoders.Controls.Add(Li)
            End If
        End If
    End Sub

    Private Sub DisplayFreeSpace()
        If SiteSettings.FrontendSettingBool("ShowDiskSpace") Then
            DiskSpacePanel.Visible = True
            Dim WC As New Net.WebClient
            Dim xml As String = WC.DownloadString("http://" & SiteSettings.Setting("MythServiceAPIAddress") & ":" & SiteSettings.Setting("MythServiceAPIPort") & "/Status/GetStatus")

            Dim Doc As New XmlDocument
            Doc.LoadXml(xml)

            Dim Nd As XmlNode = Doc.SelectSingleNode("//Group[@dir='TotalDiskSpace']")

            Dim Free As Integer = Integer.Parse(Nd.Attributes("free").InnerText)
            Dim Total As Integer = Integer.Parse(Nd.Attributes("total").InnerText)
            Dim Used As Integer = Integer.Parse(Nd.Attributes("used").InnerText)
            Dim Perc As Decimal = Used / Total

            diskinfo.Text = (Perc * 100).ToString("##") & "% Used &nbsp;&nbsp;&nbsp;" & FormatSizes(Free) & " Free &nbsp;&nbsp;&nbsp;" & FormatSizes(Total) & " Total"
            If Perc > 0.9 Then
                progressbar.CssClass = "meter red"
            ElseIf Perc > 0.8 Then
                progressbar.CssClass = "meter yellow"
            End If

            progressbarvalue.Style.Add("width", (Perc * 100).ToString("##") & "%")
        End If

    End Sub

    Private Sub DisplayUpcoming()
        If SiteSettings.FrontendSettingBool("ShowUpcoming") Then
            UpcomingPanel.Visible = True
            Dim UpcomingList As ProgramList = WSCache.DVR.GetUpcomingList(0, 10, False)

            For Each Up As Program In UpcomingList.Programs
                Dim Li As New HtmlListItem
                Li.InnerText = Up.StartTime.Value.ToShortTimeString & " - " & Up.Title
                Upcoming.Controls.Add(Li)
            Next
        End If
    End Sub
End Class
