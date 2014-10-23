<%@ WebHandler Language="VB" Class="cleanup" %>

Imports System
Imports System.Web

Imports MythService

Public Class cleanup : Implements IHttpHandler

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(cleanup))
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
       
        Dim RecordingFiles As New List(Of String)
        RecordingFiles.AddRange(Common.MBE.ContentAPI.GetFileList("Default"))
        RecordingFiles.AddRange(Common.MBE.ContentAPI.GetFileList("Videos"))
       
        Dim StorageDirs As New List(Of String)
        
        For Each StrDir As MythService.StorageGroupDir In Common.MBE.ServiceAPI.GetStorageGroupDirs("Default", "").StorageGroupDirs
            StorageDirs.Add(StrDir.DirName)
        Next
        For Each StrDir As MythService.StorageGroupDir In Common.MBE.ServiceAPI.GetStorageGroupDirs("Videos", "").StorageGroupDirs
            StorageDirs.Add(StrDir.DirName)
        Next
                                       
        Dim HLSs As List(Of iMythContent.LiveStreamInfo) = Common.MBE.ContentAPI.GetFilteredStreamList("")
        Dim Source As String
        
        For Each hls As iMythContent.LiveStreamInfo In HLSs
            Source = hls.SourceFile
            For Each Dir As String In StorageDirs
                Source = Source.Replace(Dir, "")
            Next
            
            If Not RecordingFiles.Contains(Source) Then
                Common.MBE.ContentAPI.RemoveLiveStream(hls.Id)
                context.Response.Write(hls.SourceFile & ControlChars.NewLine)
            End If
        Next
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class