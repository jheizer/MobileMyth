#Region "GPL"
'    This file is part of MobileMyth.

'    MobileMyth is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.

'    MobileMyth is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.

'    You should have received a copy of the GNU General Public License
'    along with MobileMyth.  If not, see <http://www.gnu.org/licenses/>.

'    Copyright 2012-2014 Jonathan Heizer jheizer@gmail.com
#End Region

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
