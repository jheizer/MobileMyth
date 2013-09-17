<%@ WebHandler Language="VB" Class="transcode" %>

Imports System
Imports System.Web
Imports MythContent
Imports System.Globalization

Public Class transcode : Implements IHttpHandler
    
    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(transcode))
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim test As Boolean = False
        
        test = (Not String.IsNullOrEmpty(context.Request.QueryString("test")) AndAlso context.Request.QueryString("test") = "true")
        
        Try
            
            Dim Path As String = IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data", "transcode.xml")
            Dim Data As XElement = XElement.Load(Path)
                
            Dim Recordings As MythDVR.ProgramList = WSCache.GetRecordedList
            Dim Videos As MythVideo.VideoMetadataInfoList = WSCache.Video.GetVideoList(True, 0, 10000)
            
            Dim Streams As LiveStreamInfoList
            Dim nd As XElement
            
            For Each nd In (From s In Data.Descendants("rule") Select s).ToList
                '<rule title="test" resolution="720p" />    
                Dim StartDate As DateTime = DateTime.Parse(nd.Attribute("after").Value, CultureInfo.GetCultureInfo("en-US")).ToUniversalTime()
            
                'recordings
                For Each rec As MythDVR.Program In Recordings.Programs.ToList.FindAll(Function(r) r.Title.ToLower.Contains(nd.Attribute("title").Value.ToLower) AndAlso r.Recording.StartTs.Value > StartDate)
                    Streams = WSCache.GetFilteredStreamList(rec.FileName)

                    If Streams.LiveStreamInfos.Count = 0 Then
                        If test Then
                            context.Response.Write("Test: Creating stream for: " & rec.Title & "  " & rec.SubTitle & "<br>")
                        Else
                            context.Response.Write("Creating stream for: " & rec.Title & "  " & rec.SubTitle & "<br>")
                            
                            Dim VidSet As VideoResolution = Resolutions.Resolution(nd.Attribute("resolution").Value)
                        
                            Dim Str As LiveStreamInfo = WSCache.Content.AddRecordingLiveStream(rec.Channel.ChanId, rec.Recording.StartTs, 0, 0, VidSet.Height, VidSet.VRate, VidSet.ARate, 48000)
                        
                            'now we wait
                            While Str.PercentComplete < 100
                                Threading.Thread.Sleep(60000)
                                
                                Streams = WSCache.GetFilteredStreamList(rec.FileName)
                                Str = Streams.LiveStreamInfos(0)
                            End While
                            
                        End If
                    End If
                    
                Next
                
                
                Dim Vid As MythVideo.VideoMetadataInfo
                
                For Each Vid In Videos.VideoMetadataInfos.ToList.FindAll(Function(v) v.FileName.ToLower.Contains(nd.Attribute("title").Value.ToLower) AndAlso v.AddDate > StartDate)
                    Streams = WSCache.GetFilteredStreamList(Vid.FileName)

                    If Streams.LiveStreamInfos.Count = 0 Then
                        If test Then
                            context.Response.Write("Test: Creating stream for: " & Vid.FileName & "<br>")
                        Else
                            context.Response.Write("Creating stream for: " & Vid.FileName & "<br>")
                            
                            Dim VidSet As VideoResolution = Resolutions.Resolution(nd.Attribute("resolution").Value)
                        
                            Dim Str As LiveStreamInfo = WSCache.Content.AddVideoLiveStream(Vid.Id, 0, 0, VidSet.Height, VidSet.VRate, VidSet.ARate, 48000)

                        
                            'now we wait
                            While Str.PercentComplete < 100
                                Threading.Thread.Sleep(60000)
                                
                                Streams = WSCache.GetFilteredStreamList(Vid.FileName)
                                Str = Streams.LiveStreamInfos(0)
                            End While
                            
                        End If
                    End If
                
                Next
            Next

            
        Catch ex As Exception
            context.Response.Write(ex.ToString)
        End Try
        

    End Sub
        
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class