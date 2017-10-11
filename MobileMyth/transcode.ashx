<%@ WebHandler Language="VB" Class="transcode" %>

Imports System
Imports System.Web

Imports System.Globalization

Public Class transcode : Implements IHttpHandler

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(transcode))

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim test As Boolean = False

        test = (Not String.IsNullOrEmpty(context.Request.QueryString("test")) AndAlso context.Request.QueryString("test") = "true")

        Try

            Dim Path As String = IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data", "transcode.xml")
            Dim Data As XElement = XElement.Load(Path)

            Dim Recordings As iMythDvr.ProgramList = Common.MBE.DvrAPI.GetRecordedList(True, 0, 100000, False)
            Dim Videos As List(Of iMythVideo.VideoMetadataInfo) = Common.MBE.VideoAPI.GetVideoList(True, 0, 10000)

            Dim Streams As List(Of iMythContent.LiveStreamInfo)
            Dim nd As XElement

            For Each nd In (From s In Data.Descendants("rule") Select s).ToList
                '<rule title="test" resolution="720p" />    
                Dim StartDate As DateTime = DateTime.Parse(nd.Attribute("after").Value, CultureInfo.GetCultureInfo("en-US")).ToUniversalTime()

                'recordings
                For Each rec As iMythDvr.Program In Recordings.Programs.ToList.FindAll(Function(r) r.Title.ToLower.Contains(nd.Attribute("title").Value.ToLower) AndAlso r.Recording.StartTs.Value > StartDate)
                    Streams = Common.MBE.ContentAPI.GetFilteredStreamList(rec.FileName)

                    If Streams.Count = 0 Then
                        If test Then
                            context.Response.Write("Test: Creating stream for: " & rec.Title & "  " & rec.SubTitle & "<br>")
                        Else
                            context.Response.Write("Creating stream for: " & rec.Title & "  " & rec.SubTitle & "<br>")

                            Dim VidSet As VideoResolution = Resolutions.Resolution(nd.Attribute("resolution").Value)

                            Dim Str As iMythContent.LiveStreamInfo = Common.MBE.ContentAPI.AddRecordingLiveStream(rec.Channel.ChanId, rec.Recording.StartTs, 0, 0, VidSet.Height, VidSet.VRate, VidSet.ARate, 48000)

                            'now we wait
                            While Str.PercentComplete < 100
                                Threading.Thread.Sleep(60000)

                                Streams = Common.MBE.ContentAPI.GetFilteredStreamList(rec.FileName)
                                Str = Streams(0)
                            End While

                        End If
                    End If

                Next


                Dim Vid As iMythVideo.VideoMetadataInfo

                For Each Vid In Videos.ToList.FindAll(Function(v) v.FileName.ToLower.Contains(nd.Attribute("title").Value.ToLower) AndAlso v.AddDate > StartDate)
                    Streams = Common.MBE.ContentAPI.GetFilteredStreamList(Vid.FileName)

                    If Streams.Count = 0 Then
                        If test Then
                            context.Response.Write("Test: Creating stream for: " & Vid.FileName & "<br>")
                        Else
                            context.Response.Write("Creating stream for: " & Vid.FileName & "<br>")

                            Dim VidSet As VideoResolution = Resolutions.Resolution(nd.Attribute("resolution").Value)

                            Dim Str As iMythContent.LiveStreamInfo = Common.MBE.ContentAPI.AddVideoLiveStream(Vid.Id, 0, 0, VidSet.Height, VidSet.VRate, VidSet.ARate, 48000)


                            'now we wait
                            While Str.PercentComplete < 100
                                Threading.Thread.Sleep(60000)

                                Streams = Common.MBE.ContentAPI.GetFilteredStreamList(Vid.FileName)
                                Str = Streams(0)
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