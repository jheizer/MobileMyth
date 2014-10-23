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

Public Class Frontends
    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(Frontends))

    Public Shared Frontends As New Generic.Dictionary(Of String, MythFrontend)

    Shared Sub New()
        Try
            Logger.Info("Loading Frontends file")

            Dim Path As String = IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data", "frontends.xml")
            Dim Data As XElement = XElement.Load(Path)

            Dim FE As MythFrontend
            For Each nd As XElement In (From s In Data.Descendants("frontend") Select s).ToList
                FE = New MythFrontend(nd.Attribute("address").Value)

                If Not nd.Attribute("type") Is Nothing Then
                    Select Case nd.Attribute("type").Value
                        Case Is = "chromecast"
                            FE.Type = FrontendType.ChromeCast
                    End Select
                End If

                If Not nd.Attribute("mac") Is Nothing Then
                    FE.MAC = nd.Attribute("mac").Value
                End If

                Frontends.Add(nd.Attribute("name").Value, FE)
            Next

        Catch ex As Exception
            Logger.Error("Error loading frontends list" & ControlChars.NewLine & ex.ToString)
        End Try
    End Sub

End Class

Public Class MythFrontend
    Public Address As String
    Public Type As FrontendType
    Public MAC As String

    Public Sub New(Address As String)
        Me.Address = Address
        Me.Type = FrontendType.MythTV
    End Sub
End Class

Public Enum FrontendType As Byte
    MythTV = 0
    ChromeCast = 1
End Enum

