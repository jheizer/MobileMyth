Imports Microsoft.VisualBasic

Public Class Common

    Public Shared Function GetServiceUrl() As String
        Dim Server As String = SiteSettings.FrontendSetting("ServiceServer")
        Dim Port As String = SiteSettings.FrontendSetting("ServicePort")

        If String.IsNullOrEmpty(Server) Then
            Server = SiteSettings.Setting("MythServiceAPIAddress")
        End If

        If String.IsNullOrEmpty(Port) Then
            Port = SiteSettings.Setting("MythServiceAPIPort")
        End If

        Return "http://" & Server & ":" & Port
    End Function

    Public Shared Function FormatSizes(ByVal MB As Integer) As String
        Dim Sz As Integer = Integer.Parse(MB)
        Dim Out As Decimal = 0
        Dim Unit As String = "MB"

        If Sz > 1000000 Then
            Out = Sz / 1000000
            Unit = "TB"
        ElseIf Sz > 1000 Then
            Out = Sz / 1000
            Unit = "GB"
        End If

        Return Out.ToString("#.##") & " " & Unit
    End Function
End Class
