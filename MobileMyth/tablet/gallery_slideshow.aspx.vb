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

'    Copyright 2013 Jonathan Heizer jheizer@gmail.com
#End Region


Imports MythService
Imports MythDVR

Partial Class tablet_gallery_slideshow
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(tablet_gallery_slideshow))

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.PageTitle = "Gallery"

        Try
            Dim Folder As String = ""

            If Not Request.QueryString("f") Is Nothing Then
                Folder = Request.QueryString("f")
            End If

            Dim Images As List(Of String)
            Dim Folders As List(Of String)
            Gallery.GetImages(Folder, Folders, Images)

            'Add the images
            For Each ImgPath As String In Images
                Dim Slide As New Panel
                Slide.CssClass = "slide"

                Dim img As New Image
                img.ImageUrl = "../images/loader-large.gif"
                img.Attributes.Add("data-src", Common.ProxyURL("/Content/GetImageFile?StorageGroup=Photographs&FileName=" & ImgPath))
                Slide.Controls.Add(img)

                GallerySlideHolder.Controls.Add(Slide)
            Next

        Catch ex As Exception
            Logger.Error(ex.ToString)
        End Try

    End Sub


End Class