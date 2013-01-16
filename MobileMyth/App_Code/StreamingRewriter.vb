Imports Microsoft.VisualBasic

Public Class StreamingRewriter
    Implements IHttpModule

    Public Sub Init(ByVal application As HttpApplication) Implements IHttpModule.Init
        AddHandler application.BeginRequest, AddressOf Me.Application_BeginRequest
    End Sub

    Private Sub Application_BeginRequest(ByVal source As Object, ByVal e As EventArgs)


    End Sub

    Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
    End Sub

End Class
