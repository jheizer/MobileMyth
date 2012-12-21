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

Partial Class startstream
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim Id As String = Request.QueryString("id")

        If Not Id Is Nothing Then
            Dim Recordings As ProgramList = WSCache.GetRecordedList

            Dim rec As Program = Recordings.Programs.ToList.Find(Function(r) r.ProgramId = Id)

            Dim Str As LiveStreamInfo
            Dim Streams As LiveStreamInfoList = WSCache.Content.GetLiveStreamList()

            Str = Streams.LiveStreamInfos.ToList.Find(Function(s) IO.Path.GetFileName(s.SourceFile) = rec.FileName)

            If Str Is Nothing Then
                Str = WSCache.Content.AddLiveStream("Default", rec.FileName, "", 0, SiteSettings.Setting("VideoWidth"), 0, 800000, 64000, 44100)
            End If

            If Request.QueryString("done") = "t" Then

                Response.Redirect("viewstream.aspx?url=" & HttpUtility.UrlEncode(Str.FullURL))

            End If
        End If
    End Sub
End Class