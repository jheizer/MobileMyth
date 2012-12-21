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

Partial Class shows
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.PageTitle = "Recordings"

        Dim Recordings As ProgramList = WSCache.GetRecordedList

        Dim Lst = From n In Recordings.Programs _
                  Group n By n.Title Into Count() _
                  Select Title, Episodes = Count, InetRef = (From s In Recordings.Programs _
                                                  Where s.Title = Title _
                                                  Select s.Inetref).First _
                                              , Season = (From s In Recordings.Programs _
                                                  Where s.Title = Title _
                                                  Select s.Season).First _
                  Order By Title

        Dim List As New HtmlList
        maincontent.Controls.Add(List)
        List.Attributes.Add("data-role", "listview")

        Dim li As New ShowListItem("All Programs", Recordings.Programs.Count, "recordings.aspx", "", "")
        List.Controls.Add(li)

        For Each Rec In Lst
            li = New ShowListItem(Rec.Title, Rec.Episodes, "episodes.aspx?title=" & Rec.Title, Rec.InetRef, Rec.Season)
            List.Controls.Add(li)
        Next

    End Sub
End Class