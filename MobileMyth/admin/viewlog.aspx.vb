
Partial Class admin_viewlog
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(admin_viewlog))

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim Lit As New Literal
        Dim Text As New Text.StringBuilder
        Try
            For Each Ln As String In IO.File.ReadAllLines(IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data", "logs", "mobilemyth.log"))
                Text.Append(Ln.Replace(ControlChars.Tab, "&nbsp;&nbsp;&nbsp;").Replace("   ", "&nbsp;&nbsp;&nbsp;"))
                Text.Append("<br/>")
            Next

            Lit.Text = Text.ToString

        Catch ex As Exception
            Lit.Text = "Error Loading Log File"
        End Try

        maincontent.Controls.Add(Lit)
    End Sub
End Class
