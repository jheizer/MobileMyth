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

Public Class SiteSettings
    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(SiteSettings))

    Private Shared Path As String = ""
    Private Shared Data As XElement

    Shared Sub New()
        Try
            Logger.Info("Loading settings file")

            Path = IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data", "settings.xml")

            If Not IO.File.Exists(Path) Then
                Dim NwFile As New IO.StreamWriter(Path)
                NwFile.Write("<settings><Frontends></Frontends></settings>")
                NwFile.Close()
            End If

            Data = XElement.Load(Path)
        Catch ex As Exception
            Logger.Error("Error loading settings file" & ControlChars.NewLine & ex.ToString)
        End Try
    End Sub

    Public Shared Property Setting(ByVal Name As String) As String
        Get
            Dim Ret As String = HttpContext.Current.Cache("Setting:" & Name)

            If Ret Is Nothing Then
                SyncLock (Data)
                    Try
                        Ret = (From s In Data.Descendants(Name)
                               Select s).First().Value

                        HttpContext.Current.Cache.Insert("Setting:" & Name, Ret, New System.Web.Caching.CacheDependency(Path))

                    Catch ex As Exception
                        Ret = ""
                    End Try

                End SyncLock
            End If

            Return Ret

        End Get
        Set(ByVal value As String)
            Dim St As XElement = (From s In Data.Descendants(Name)
                                      Select s).FirstOrDefault
            SyncLock (Data)
                If St Is Nothing Then
                    St = New XElement(Name)
                    Data.Add(St)
                End If
                St.Value = value
                Data.Save(Path)
            End SyncLock
        End Set
    End Property

    Public Shared Property SettingBool(ByVal Name As String) As Boolean
        Get
            Dim ret As String = Setting(Name)
            If Not String.IsNullOrEmpty(ret) Then
                Dim tmp As Boolean = False
                If Boolean.TryParse(ret, tmp) Then
                    Return tmp
                End If
            End If
            Return False
        End Get
        Set(ByVal value As Boolean)
            Setting(Name) = value.ToString
        End Set
    End Property

    Public Shared ReadOnly Property SettingBool(ByVal Name As String, ByVal DefaultValue As Boolean) As Boolean
        Get
            Dim ret As String = Setting(Name)
            If Not String.IsNullOrEmpty(ret) Then
                Dim tmp As Boolean = False
                If Boolean.TryParse(ret, tmp) Then
                    Return tmp
                Else
                    Return DefaultValue
                End If
            Else
                Return DefaultValue
            End If
            Return False
        End Get
    End Property

    Public Shared Property FrontendSettingBool(ByVal Name As String) As Boolean
        Get
            Dim ret As String = FrontendSetting(Name)
            If Not String.IsNullOrEmpty(ret) Then
                Dim tmp As Boolean = False
                If Boolean.TryParse(ret, tmp) Then
                    Return tmp
                End If
            End If
            Return False

        End Get
        Set(ByVal value As Boolean)
            FrontendSetting(Name) = value.ToString
        End Set
    End Property

    Public Shared ReadOnly Property FrontendSettingBool(ByVal Name As String, ByVal DefaultValue As Boolean) As Boolean
        Get
            Dim ret As String = FrontendSetting(Name)
            If Not String.IsNullOrEmpty(ret) Then
                Dim tmp As Boolean = False
                If Boolean.TryParse(ret, tmp) Then
                    Return tmp
                Else
                    Return DefaultValue
                End If
            End If
            Return False

        End Get
    End Property

    Public Shared Property FrontendSetting(ByVal Name As String) As String
        Get
            Dim FE As String = HttpContext.Current.Request.UserHostName
            Dim Ret As String = HttpContext.Current.Cache(FE & Name)

            If Ret Is Nothing Then

                SyncLock (Data)
                    Try
                        Dim Nd As XElement = (From s In Data.Descendants("Frontend")
                                                  Where s.Attribute("name") = FE
                                                  Select s).FirstOrDefault()
                        If Nd Is Nothing Then
                            Return ""
                        End If

                        Ret = Nd.Element(Name).Value

                        HttpContext.Current.Cache.Insert(FE & Name, Ret, New System.Web.Caching.CacheDependency(Path))

                    Catch ex As Exception
                        Ret = ""
                    End Try

                End SyncLock

            End If

            Return Ret

        End Get
        Set(ByVal value As String)
                Dim FE As String = HttpContext.Current.Request.UserHostName

                'Look for the FE
                Dim Nd As XElement = (From s In Data.Descendants("Frontend")
                                          Where s.Attribute("name") = FE
                                          Select s).FirstOrDefault()

            SyncLock (Data)
                If Nd Is Nothing Then
                    Nd = New XElement("Frontend")
                    Nd.SetAttributeValue("name", FE)
                    Data.Descendants("Frontends").First.Add(Nd)
                End If

                'Find the value
                Dim ValNd As XElement = Nd.Descendants(Name).FirstOrDefault

                If ValNd Is Nothing Then
                    ValNd = New XElement(Name)
                    Nd.Add(ValNd)
                End If

                ValNd.Value = value
                Data.Save(Path)
            End SyncLock
        End Set
    End Property


End Class