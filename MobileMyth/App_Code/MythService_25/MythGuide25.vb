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

Public Class MythGuide25
    Implements iMythGuide

    Private Guide As MythGuide.GuideClient

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythAPIService.Init
        Guide = New MythGuide.GuideClient("BasicHttpBinding_Guide", "http://" & Address & ":" & Port & "/Guide")
        Return True
    End Function

    Public Function GetProgramGuide(StartTime As Date, EndTime As Date, StartChanId As Integer, NumChannels As Integer, Details As Boolean) As MythGuide.ProgramGuide Implements iMythGuide.GetProgramGuide
        Return Guide.GetProgramGuide(StartTime, EndTime, StartChanId, NumChannels, Details)
    End Function
End Class
