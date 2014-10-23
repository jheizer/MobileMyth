Imports Microsoft.VisualBasic
Imports System.Diagnostics

Public Class LinuxProcess
    Public Shared Function Run(Executable As String, Arguements As String) As String
        Return Run(Executable, Arguements, False)
    End Function

    Public Shared Function Run(Executable As String, Arguements As String, ReturnError As Boolean) As String
        Dim proc As New Process
        Dim startinfo As New ProcessStartInfo
        startinfo.RedirectStandardOutput = True
        startinfo.RedirectStandardInput = True
        startinfo.RedirectStandardError = True
        startinfo.UseShellExecute = False
        startinfo.Verb = " "
        startinfo.CreateNoWindow = True
        startinfo.UseShellExecute = False

        startinfo.FileName = Executable
        startinfo.Arguments = " " & Arguements

        proc.StartInfo = startinfo
        proc.Start()

        proc.WaitForExit()

        If ReturnError Then
            Return proc.StandardError.ReadToEnd
        End If

        Return proc.StandardOutput.ReadToEnd

    End Function
End Class
