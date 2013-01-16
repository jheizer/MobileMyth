Imports Microsoft.VisualBasic

Public Class Common
    Public Shared DateFormat As String = ""

    Shared Sub New()
       LoadDateFormat
    End Sub

    Public Shared Sub LoadDateFormat()
        Select Case SiteSettings.Setting("DateFormat")
            Case Is = "DD/MM/YYYY"
                DateFormat = "dd/MM/yyyy"
            Case Else
                DateFormat = "MM/dd/yyyy"
        End Select
    End Sub

    Public Shared Function GetServiceUrl() As String
        Dim Server As String = SiteSettings.Setting("MythServiceAPIAddress")
        Dim Port As String = SiteSettings.Setting("MythServiceAPIPort")
        Return "http://" & Server & ":" & Port
    End Function

    Public Shared Function ProxyURL(ByVal Url As String) As String
        Return "../proxy.aspx?url=" & HttpUtility.UrlEncode(Url)
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
