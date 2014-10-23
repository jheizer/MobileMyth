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

'    Copyright 2012-2014 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic
Imports System.ServiceModel
Imports System.Net.Sockets

Public MustInherit Class MythBackendBase

    Friend Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(MythBackendBase))

    Public MythVersion As MythTvVersion = MythTvVersion.v26
    Public GalleryEnabled As Boolean = False

    Public ContentAPI As iMythContent
    Public GuideAPI As iMythGuide
    Public ServiceAPI As iMythService
    Public VideoAPI As iMythVideo
    Public DvrAPI As iMythDvr


    Friend Address As String
    Friend Port As String

    Public Sub New()
        Try
            Logger.Info("Reinitalizing webservice references.")

            Address = SiteSettings.Setting("MythServiceAPIAddress")
            Port = SiteSettings.Setting("MythServiceAPIPort")

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Public Function TestConnection() As Boolean
        Dim WC As New Net.WebClient
        Dim html As String = WC.DownloadString(Common.GetServiceUrl & "/Status/GetStatus")
        If Not html.Contains("<Status") Then
            Return False
        End If
        Return True
    End Function

    Friend Function Init() As Boolean
        Try
            MythVersion = FindMythtvVersion()
            GalleryEnabled = CheckForGallery()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Shared Function FindMythtvVersion() As MythTvVersion
        Dim WC As New Net.WebClient
        Dim html As String

        html = WC.DownloadString(Common.GetServiceUrl & "/Dvr/wsdl?raw=1")
        If html.Contains("Interface Version 1.9") Then
            Return MythTvVersion.v27
        End If

        html = WC.DownloadString(Common.GetServiceUrl & "/Content/wsdl?raw=1")
        If html.Contains("Interface Version 1.34") Then
            Return MythTvVersion.v28
        End If

        Return MythTvVersion.v26
    End Function

    Public Function CheckForGallery() As Boolean
        For Each Host As String In ServiceAPI.GetHosts
            Dim Dirs As MythService.StorageGroupDirList = ServiceAPI.GetStorageGroupDirs("Photographs", Host)
            If Dirs.StorageGroupDirs.Count > 0 Then
                Return True
            End If
        Next
        Return False
    End Function

End Class
