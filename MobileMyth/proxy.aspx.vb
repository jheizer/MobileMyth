Imports System.Net

Partial Class proxy
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(proxy))

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim IsStream As Boolean = False

        Dim url As String = Context.Request.QueryString("url")

        If Page.RouteData.Values.Count > 0 Then
            url = Page.RouteData.Values("url")

            url = "/StorageGroup/" & url
            If url.StartsWith("Streaming") Then
                IsStream = True
            End If
        End If

        If Not IsStream Then
            'This doesn't seem to be working ever for me
            Context.Response.Cache.SetExpires(DateTime.Now.Add(New TimeSpan(0, 10, 0, 0)))
            Context.Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate)
            Context.Response.Cache.VaryByParams("url") = True
        End If

        'Make sure it is a url I am proxying and not some one up to no good
        If Not String.IsNullOrEmpty(url) AndAlso AllowedToProxy(url) Then

            Try
                url = HttpUtility.UrlDecode(url)
                url = Common.GetServiceUrl & url

                Process2(url, Context, Response)

               
                'Proxy with "local" cache
                'url = HttpUtility.UrlDecode(url)
                'Dim FullUrl As String = Common.GetServiceUrl & url
                'Dim Filename As String = url.Replace("/", "_").Replace(":", "_").Replace("=", "_").Replace("?", "_").Replace("&", "_") & ".png"
                'Dim Local As String = IO.Path.Combine(Server.MapPath("~"), "cache", Filename)

                'If Not IO.File.Exists(Local) Then
                '    Dim Cl As New Net.WebClient
                '    Cl.DownloadFile(FullUrl, Local)
                'End If

                'Response.Redirect("~/cache/" & Filename, False)


            Catch ex As Exception
                Logger.Error(ex.ToString)
            End Try

        End If

        Response.Flush()

    End Sub

    Private Sub Process1(ByVal Url As String, ByVal Context As HttpContext, ByVal Response As HttpResponse)
        'Simple version
        Dim Wc As New Net.WebClient
        Dim data As Byte() = Wc.DownloadData(Url)
        Logger.Info("Data Downloaded: " & Url)

        Context.Response.ContentType = Wc.ResponseHeaders("Content-Type")
        Context.Response.BinaryWrite(data)

        Logger.Info("Response Written: " & Url)

    End Sub

    Private Sub Process2(ByVal Url As String, ByVal Context As HttpContext, ByVal Response As HttpResponse)
        Try
            'Streaming but doesn't seem to help
            Dim Req As Net.HttpWebRequest = Net.HttpWebRequest.Create(Url)
            Req.Method = Context.Request.RequestType
            Req.ContentType = Context.Request.ContentType
            Req.Proxy = Net.GlobalProxySelection.GetEmptyWebProxy()

            Dim Resp As Net.HttpWebResponse = Req.GetResponse
            Dim RespStream As IO.Stream = Resp.GetResponseStream

            Context.Response.ContentType = Resp.ContentType
            Context.Response.BufferOutput = False
            Context.Response.Buffer = False

            If Resp.StatusCode.ToString.ToLower = "ok" Then

                Dim Pos As Integer = 0
                Dim Buf(4095) As Byte
                Dim Read As Integer = RespStream.Read(Buf, 0, Buf.Length)

                While Read > 0
                    Response.OutputStream.Write(Buf, 0, Read)
                    Response.Flush()
                    Pos += Read
                    Read = RespStream.Read(Buf, 0, Buf.Length)
                End While
            Else
                Logger.Error(Resp.StatusCode & "  " & Resp.StatusDescription & ": " & Url)
            End If

        Catch ex As HttpException
            If ex.ErrorCode <> -2147023667 Then 'client disconnected, ignore
                Logger.Error(Url, ex)
            End If
        Catch e As Exception
            Logger.Error(Url, e)
        End Try
    End Sub

    Private Sub Process3(ByVal Url As String, ByVal Context As HttpContext, ByVal Response As HttpResponse)
        'Streaming but doesn't seem to help
        Dim Req As Net.HttpWebRequest = CreateScalableHttpWebRequest(Url)
        Req.Method = Context.Request.RequestType
        Req.ContentType = Context.Request.ContentType

        Dim Resp As Net.HttpWebResponse = Req.GetResponse
        Dim RespStream As IO.Stream = Resp.GetResponseStream

        Logger.Info("Got response: " & Url)

        Context.Response.ContentType = Resp.ContentType
        Context.Response.BufferOutput = False
        Context.Response.Buffer = False

        If Resp.StatusCode.ToString.ToLower = "ok" Then
            RespStream.CopyTo(Response.OutputStream)
        End If

        Logger.Info("Response written: " & Url)

    End Sub

    Public Shared Function CreateScalableHttpWebRequest(ByVal url As String) As HttpWebRequest
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(url), HttpWebRequest)
        request.AutomaticDecompression = DecompressionMethods.None
        request.MaximumAutomaticRedirections = 2
        request.ReadWriteTimeout = 5000
        request.Timeout = 3000
        request.Accept = "*/*"
        request.Headers.Add("Accept-Language", "en-US")
        request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.8.1.6) Gecko/20070725 Firefox/2.0.0.6"
        request.Proxy = Net.GlobalProxySelection.GetEmptyWebProxy()

        Return request
    End Function

    Private Function AllowedToProxy(ByVal Url As String) As Boolean
        Dim AcceptableCalls() As String = New String() {"/Content/GetPreviewImage", _
                                                        "/Content/GetRecordingArtwork", _
                                                        "/Content/GetVideoArtwork", _
                                                        "/Content/GetImage", _
                                                        "/StorageGroup/Stream", _
                                                        "/Guide/GetChannelIcon", _
                                                        "/StorageGroup/3rdParty/JW_Player", _
                                                        "/Content/GetFile"}

        For Each Method As String In AcceptableCalls
            If Url.StartsWith(Method) Then
                Return True
            End If
        Next

        Logger.Error("Invalid Proxy URL" & Url)
        Return False
    End Function
End Class
