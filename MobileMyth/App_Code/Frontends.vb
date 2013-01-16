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

Public Class Frontends
    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(Frontends))

    Public Shared Frontends As New Generic.Dictionary(Of String, String)

    Shared Sub New()
        Try
            Logger.Info("Loading Frontends file")

            Dim Path As String = IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data", "frontends.xml")
            Dim Data As XElement = XElement.Load(Path)

            For Each nd As XElement In (From s In Data.Descendants("frontend") Select s).ToList
                Frontends.Add(nd.Attribute("name").Value, nd.Attribute("address").Value)
            Next

        Catch ex As Exception
            Logger.Error("Error loading frontends list" & ControlChars.NewLine & ex.ToString)
        End Try
    End Sub

End Class