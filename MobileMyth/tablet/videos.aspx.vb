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

Partial Class tablet_videos
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(tablet_videos))

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.PageTitle = "Videos"

        Try
            Dim Folder As String = ""

            If Not Request.QueryString("f") Is Nothing Then
                Folder = HttpUtility.UrlDecode(Request.QueryString("f").TrimStart("/"))
            End If

            Dim Videos As List(Of iMythVideo.VideoMetadataInfo)
            Videos = Common.MBE.VideoAPI.GetVideoList(True, 0, 10000)  'Change the service xml to xs:string for AddDate
            
            Dim Folders As New List(Of String)
            Dim Fold As String
            Dim temp As String
            For Each V As iMythVideo.VideoMetadataInfo In Videos
                Fold = V.FileName
                temp = ""
                If Fold.Contains("/") Then
                    If Fold.StartsWith(Folder) Then
                        If Not String.IsNullOrEmpty(Folder) Then
                            Fold = Fold.Substring(Folder.Length + 1)
                        End If

                        If Fold.Contains("/") Then
                            temp = Fold.Substring(0, Fold.IndexOf("/"))
                            If Not Folders.Contains(temp) Then
                                Folders.Add(temp)
                            End If
                        End If

                    End If
                End If
            Next


            Folders.Sort()

            Dim Sorted As List(Of iMythVideo.VideoMetadataInfo)

            If String.IsNullOrEmpty(Folder) Then
                Sorted = (From v In Videos
                          Where Not v.FileName.Contains("/")
                          Order By v.Title
                          Select v).ToList
            Else
                Sorted = (From v In Videos
                          Where v.FileName.Contains(Folder & "/") AndAlso v.FileName.IndexOf(Folder & "/") = v.FileName.LastIndexOf("/") - Folder.Length
                          Order By v.Title, v.Season, v.Episode
                          Select v).ToList
            End If


            For i As Integer = 0 To Folders.Count - 1
                Dim Li As New VideoPanel(i, Folders(i), "videos.aspx?f=" & HttpUtility.UrlEncode(Folder & "/" & Folders(i)), "../images/blackfolder.png")
                maincontent.Controls.Add(Li)
            Next

            Dim title As String = ""
            Dim Vid As iMythVideo.VideoMetadataInfo
            For i As Integer = 0 To Sorted.Count - 1
                Vid = Sorted(i)
                title = Vid.Title
                If Vid.Episode > 0 Then
                    title &= " " & Vid.Season & "x" & Vid.Episode.ToString("00")
                End If
                Dim Li As New VideoPanel(i, title, "video.aspx?id=" & Vid.Id, Common.ProxyURL("/Content/GetVideoArtwork?Id=" & Vid.Id & "&Type=coverart&Height=225"))
                maincontent.Controls.Add(Li)
            Next

        Catch ex As Exception
            Logger.Error(ex.ToString)
        End Try

    End Sub


End Class