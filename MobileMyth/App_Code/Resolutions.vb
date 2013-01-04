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

'    Copyright 2012, 2013 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic

Public Class Resolutions
    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(Resolutions))

    Private Shared Resolutions As New Generic.Dictionary(Of String, VideoResolution)

    Shared Sub New()
        Try
            Logger.Info("Loading resolutions file")

            Dim Path As String = IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data", "resolutions.xml")
            Dim Data As XElement = XElement.Load(Path)

            For Each nd As XElement In (From s In Data.Descendants("Resolution") Select s).ToList
                Resolutions.Add(nd.Attribute("Name").Value, New VideoResolution(nd))
            Next

        Catch ex As Exception
            Logger.Error("Error loading resolution settings" & ControlChars.NewLine & ex.ToString)
        End Try
    End Sub

    Public Shared Function Resolution(ByVal Name As String) As VideoResolution
        If Resolutions.ContainsKey(Name) Then
            Return Resolutions(Name)
        End If
        Return Nothing
    End Function

    Public Shared Function MyResolution() As VideoResolution
        Return Resolution(SiteSettings.FrontendSetting("Resolution"))
    End Function

    Public Shared Function ResolutionNames() As List(Of String)
        Return (From r In Resolutions.Values
                Select r.Name).ToList
    End Function

End Class

'  <Resolution Name="224p" Width="224" VRate="225" ARate="96000"></Resolution>
Public Class VideoResolution
    Public Name As String
    Public Height As String
    Public VRate As String
    Public ARate As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal Res As XElement)
        Name = Res.Attribute("Name").Value
        Height = Res.Attribute("Height").Value
        VRate = Res.Attribute("VRate").Value
        ARate = Res.Attribute("ARate").Value
    End Sub

End Class