<%@ Application Language="VB" %>
<%@ Import Namespace="System.ServiceModel" %>

<script runat="server">
 
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        log4net.Config.XmlConfigurator.Configure()
        Dim logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(global_asax))

        Try
            logger.Info("Site Started")
        
            Routing.RouteTable.Routes.MapPageRoute("Streaming", "StorageGroup/{*url}", "~/proxy.aspx")
      
            FrontendScanner.StartFindFrontends()
            
            Common.LoadMBE()
            
        Catch ex As Exception
            logger.Error(ex.ToString)
        End Try
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        Try
            FrontendScanner.StopFindFrontends()
        Catch ex As Exception
        End Try
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex As Exception = Server.GetLastError().GetBaseException()
        
        Dim logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(global_asax))
        logger.Error(ex.ToString)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        
        'Dim Req As String = HttpContext.Current.Request.RawUrl.ToString
        'Req = Req.Replace("MobileMyth/", "").Replace("mobilemyth/", "")
        'Req = HttpUtility.HtmlEncode(Req)
        
        'If Req.Contains("/StorageGroup/") AndAlso Not Req.Contains("proxy.ashx") Then
        '    Context.RewritePath("~/proxy.ashx?url=" & Req)
        '    'Response.Redirect("~/proxy.ashx?url=" & Req)
        'End If
    End Sub
</script>