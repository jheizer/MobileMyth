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
Imports Microsoft.VisualBasic

Public Class Gallery

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(Gallery))

    Private Shared ExcludeFiles() As String = New String() {"thumbs.db", ".ini"}

    Public Shared Sub GetImages(Folder As String, ByRef Folders As List(Of String), ByRef Images As List(Of String))
        Dim Photos() As String = Common.MBE.ContentAPI.GetFileList("Photographs")

        Folders = New List(Of String)

        Dim Fold As String
        Dim temp As String
        For Each Fold In Photos
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

        'Decide which images should be shown based on the current folder
        Dim Sorted As List(Of String)
        If String.IsNullOrEmpty(Folder) Then
            Sorted = (From v In Photos
                      Where Not v.Contains("/")
                      Order By v
                      Select v).ToList
        Else
            Sorted = (From v In Photos
                      Where v.Contains(Folder & "/") AndAlso v.IndexOf(Folder & "/") = v.LastIndexOf("/") - Folder.Length
                      Order By v
                      Select v).ToList
        End If


        Images = New List(Of String)

        For Each img As String In Sorted
            If CheckFile(img) Then
                Images.Add(img)
            End If
        Next

    End Sub

    Private Shared Function CheckFile(Img As String) As Boolean
        Dim Ext As String = IO.Path.GetExtension(Img)

        'Check to make sure we are not trying to load a cached thumbnail
        If Not Img.IndexOf(Ext) = Img.LastIndexOf(Ext) Then 'If the extension is only at the end
            Return False
        End If

        'Check to make sure we are not trying to display a thumb.db or other system file
        Dim File As String = IO.Path.GetFileName(Img).ToLower

        For Each f As String In ExcludeFiles
            If File = f Then
                Return False
            End If
        Next

        Return True
    End Function

End Class
