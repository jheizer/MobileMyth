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

'    Copyright 2013-2014,2017 Jonathan Heizer jheizer@gmail.com
#End Region


Imports MythService

Partial Class tablet_gallery
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(tablet_gallery))

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.PageTitle = "Gallery"

        Try
            Dim Folder As String = ""

            If Not Request.QueryString("f") Is Nothing Then
                Folder = HttpUtility.UrlDecode(Request.QueryString("f").TrimStart("/"))
            End If

            Dim Images As List(Of String)
            Dim Folders As List(Of String)
            Gallery.GetImages(Folder, Folders, Images)

            'Do we need to add a .. link
            Dim FolderStartIndex As Integer = 0
            If Not String.IsNullOrEmpty(Folder) Then
                Dim url As String = "gallery.aspx"
                If Folder.Contains("/") Then
                    url &= "?f=" & HttpUtility.UrlEncode(Folder.Substring(0, Folder.LastIndexOf("/")))
                End If
                Dim Li As New GalleryPanel(0, "..", url, "../images/blackfolder.png")
                maincontent.Controls.Add(Li)
                FolderStartIndex = 1
            End If

            'Add the folders
            For i As Integer = FolderStartIndex To Folders.Count - 1 + FolderStartIndex
                Dim Li As New GalleryPanel(i, Folders(i - FolderStartIndex), "gallery.aspx?f=" & HttpUtility.UrlEncode(Folder & "/" & Folders(i - FolderStartIndex)), "../images/blackfolder.png")
                maincontent.Controls.Add(Li)
            Next

            'Add the images
            For i As Integer = 0 To Images.Count - 1
                Dim Li As New GalleryPanel(i + Folders.Count + FolderStartIndex, "", "gallery_slideshow.aspx?f=" & HttpUtility.UrlEncode(Folder) & "#" & i + 1, Common.ProxyURL("/Content/GetImageFile?StorageGroup=Photographs&FileName=" & Images(i) & "&Height=175"))
                maincontent.Controls.Add(Li)
            Next

        Catch ex As Exception
            Logger.Error(ex.ToString)
        End Try

    End Sub


End Class