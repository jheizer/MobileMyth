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

Public Class Common
    Public Shared DateFormat As String = ""

    Public Shared MBE As MythBackendBase = New MythBackend25

    Shared Sub New()
        LoadDateFormat()
        LoadMBE()
    End Sub

    Public Shared Sub LoadMBE()
        Select Case MythBackendBase.FindMythtvVersion
            Case Is = MythTvVersion.v25, MythTvVersion.v26
                MBE = New MythBackend25
            Case Is = MythTvVersion.v27
                MBE = New MythBackend27
            Case Is = MythTvVersion.v29
                MBE = New MythBackend29
        End Select
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

    Public Shared Function ConvertTypes(Of FromType, ToType As {New})(FromObj As FromType) As ToType

        Dim Nw As New ToType

        For Each prop As Reflection.PropertyInfo In GetType(FromType).GetProperties
            Try
                If prop.PropertyType.FullName.StartsWith("System") Then 'Skip sub collections and myth types
                    Dim propgetter As Reflection.MethodInfo = prop.GetGetMethod
                    Dim toprop As System.Reflection.PropertyInfo = GetType(ToType).GetProperty(prop.Name)
                    If Not toprop Is Nothing Then ' Make sure the destination class has the type
                        Dim propsetter As Reflection.MethodInfo = toprop.GetSetMethod
                        Dim value As Object = propgetter.Invoke(FromObj, Nothing)
                        propsetter.Invoke(Nw, New Object() {value})
                    End If
                Else
                    Dim j As Integer = 0
                End If
            Catch ex As Exception
                Dim i As Integer = 0
            End Try
        Next

        Return Nw
    End Function
End Class

Public Enum MythTvVersion As Byte
    v25 = 0
    v26 = 1
    v27 = 2
    v28 = 3
    v29 = 4
End Enum


