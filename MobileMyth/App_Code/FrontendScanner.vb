Imports Microsoft.VisualBasic

Public Class FrontendScanner
    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(FrontendScanner))

    Public Shared Frontends As New Dictionary(Of String, String)

    Private Shared WithEvents Zero As New Mono.Zeroconf.ServiceBrowser

    Public Shared Sub StartFindFrontends()
        Try
            Zero.Browse("_mythfrontend._tcp", "local")
        Catch ex As Exception
            Logger.Error(ex.ToString)
        End Try
    End Sub

    Public Shared Sub StopFindFrontends()
        Zero.Dispose()
    End Sub

    Private Shared Sub Zero_ServiceAdded(ByVal o As Object, ByVal args As Mono.Zeroconf.ServiceBrowseEventArgs) Handles Zero.ServiceAdded
        args.Service.Resolve()
        Frontends.Add(args.Service.Name, args.Service.NetworkInterface)

        Logger.Info("ZeroConf Added: " & args.Service.Name)
    End Sub

    Private Shared Sub Zero_ServiceRemoved(ByVal o As Object, ByVal args As Mono.Zeroconf.ServiceBrowseEventArgs) Handles Zero.ServiceRemoved
        args.Service.Resolve()
        If Frontends.ContainsKey(args.Service.Name) Then
            Frontends.Remove(args.Service.Name)
            Logger.Info("ZeroConf Removed: " & args.Service.Name)
        End If
    End Sub
End Class