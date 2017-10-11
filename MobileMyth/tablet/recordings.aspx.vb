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

'    Copyright 2012-2014,2017 Jonathan Heizer jheizer@gmail.com
#End Region


Imports MythService

Partial Class shows
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.PageTitle = "Recordings"

        Dim Titles As List(Of iMythDvr.RecordingTitle) = Common.MBE.DvrAPI.GetTitles

        Dim Sum As Integer = 0
        For Each t As iMythDvr.RecordingTitle In Titles
            Sum += t.Count
        Next

        Dim List As New HtmlList
        maincontent.Controls.Add(List)
        List.Attributes.Add("data-role", "listview")

        Dim li As New ShowListItem("All Programs", Sum, "episodes.aspx", "", "")
        List.Controls.Add(li)

        For Each Rec In Titles
            li = New ShowListItem(Rec.Title, Rec.Count, "episodes.aspx?title=" & HttpUtility.UrlEncode(Rec.Title), Rec.Inetref, 0)
            List.Controls.Add(li)
        Next

    End Sub
End Class