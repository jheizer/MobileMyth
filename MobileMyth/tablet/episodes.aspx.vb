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


Imports MythService
Imports MythDVR

Partial Class recordings
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim Title As String = "Episodes"
        Dim Recordings As ProgramList = Common.MBE.DvrAPI.GetRecordedList(True, 0, 100000, True)
        Dim Programs As List(Of Program) = Array.FindAll(Recordings.Programs, Function(p) p.Recording.StorageGroup <> "Deleted" AndAlso p.Recording.StorageGroup <> "LiveTV").ToList

        If Not Request.QueryString("title") Is Nothing Then 'If we are only suppose to show a single Title filter that now
            Title = HttpUtility.UrlDecode(Request.QueryString("title"))
            Programs = Programs.FindAll(Function(p) p.Title = Title)
        End If

        Master.PageTitle = Title

        Dim List As New HtmlList
        maincontent.Controls.Add(List)
        List.Attributes.Add("data-role", "listview")

        Dim StartPg As Integer = 0

        If Not Request.QueryString("pg") Is Nothing Then
            StartPg = Integer.Parse(Request.QueryString("pg"))
        End If

        Dim StartDate As String = DateTime.MaxValue.ToLongDateString

        Dim Index As Integer = StartPg * 50
        Dim Rec As Program = Nothing

        'Add the next 50 recordings
        While Index < StartPg * 50 + 50 AndAlso Index < Programs.Count
            Rec = Programs(Index)

            If Rec.StartTime.Value.ToLocalTime.ToLongDateString <> StartDate Then
                Dim Divider As New HtmlListItem
                Divider.Attributes.Add("data-role", "list-divider")
                Divider.InnerText = Rec.StartTime.Value.ToLocalTime.ToLongDateString
                List.Controls.Add(Divider)
                StartDate = Rec.StartTime.Value.ToLocalTime.ToLongDateString
            End If

            Dim LI As New RecordingListItem(Rec)
            List.Controls.Add(LI)

            Index += 1
        End While

        'Add the more link if there are more items
        If Index < Programs.Count Then
            Dim Lit As New LiteralControl
            Lit.Text = "<h3>More</h3>"

            Dim Lnk As New HyperLink
            If Not String.IsNullOrEmpty(Title) Then
                Lnk.NavigateUrl = "episodes.aspx?title=" & HttpUtility.UrlEncode(Title) & "&pg=" & StartPg + 1
            Else
                Lnk.NavigateUrl = "episodes.aspx?pg=" & StartPg + 1
            End If
            Lnk.Controls.Add(Lit)

            Dim LI As New HtmlListItem
            LI.Controls.Add(Lnk)

            List.Controls.Add(LI)
        End If

    End Sub
End Class

