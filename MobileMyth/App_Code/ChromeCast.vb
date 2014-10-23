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

'    Copyright 2014 Jonathan Heizer jheizer@gmail.com

'Create based on https://github.com/entertailion/Fling
#End Region


Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Web.Script.Serialization

Public Class ChromeCast
    Private AppName As String = "96be16b6-cf7e-4f6e-8ebd-593a0a9fdb5d"

    Public Function PlayVideo(ChromeCastIP As String, Url As String) As String
        If AppRunning(ChromeCastIP) Then
            StopApp(ChromeCastIP)
        End If

        Check4App(ChromeCastIP, AppName)

        StartApp(ChromeCastIP)

        Threading.Thread.Sleep(2000)

        If AppRunning(ChromeCastIP) Then

            Threading.Thread.Sleep(1000)

            Dim WSUrl As String = GetWSAddress(ChromeCastIP)

            Threading.Thread.Sleep(2000)

            Return WSUrl
        End If

        Return ""
    End Function

    Private Function AppRunning(ChromeCastIP As String) As Boolean

        Dim ResponseCode As String

        Dim request As HttpWebRequest = HttpWebRequest.Create("http://" & ChromeCastIP & ":8008/apps/")
        Dim response As HttpWebResponse = request.GetResponse()

        ResponseCode = response.StatusCode

        response.Close()

        Return (ResponseCode = 200)
    End Function


    Private Sub StopApp(ChromeCastIP As String)
        Dim ResponseCode As String
        Dim App As Uri

        Dim request As HttpWebRequest = HttpWebRequest.Create("http://" & ChromeCastIP & ":8008/apps/")
        Dim response As HttpWebResponse = request.GetResponse()

        ResponseCode = response.StatusCode
        App = response.ResponseUri

        response.Close()

        If ResponseCode = 200 Then

            request = HttpWebRequest.Create(App)
            request.Method = "DELETE"
            response = request.GetResponse()

            ResponseCode = response.StatusCode

            response.Close()
        End If
    End Sub


    Private Function Check4App(ChromeCastIP As String, Name As String) As Boolean
        Dim ResponseCode As String

        Dim request As HttpWebRequest = HttpWebRequest.Create("http://" & ChromeCastIP & ":8008/apps/" & Name)
        request.ContentType = "application/x-www-form-urlencoded"
        request.Headers.Add("Origin", "chrome-extension://boadgeojelhgndaghljhdicfkmllpafd")
        request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.71 Safari/537.36"
        request.Headers.Add("DNT", "1")
        request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch")
        request.Accept = "*/*"
        request.Headers.Add("Accept-Language", "en-US,en;q=0.8")

        Dim dataStream As Stream
        Dim response As HttpWebResponse = request.GetResponse()

        ResponseCode = response.StatusCode

        dataStream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()

        reader.Close()
        dataStream.Close()
        response.Close()

        Return (ResponseCode <> 204)
    End Function

    Private Sub StartApp(ChromeCastIP As String)
        Dim request As HttpWebRequest = HttpWebRequest.Create("http://" & ChromeCastIP & ":8008/apps/" & AppName) 'YouTube") '?v=release-96be16b6cf7e4f6e8ebd593a0a9fdb5d&id=local%3A1&idle=windowclose")
        request.Method = "POST"
        Dim dataStream As Stream '= request.GetRequestStream()
        Dim response As WebResponse = request.GetResponse()
        dataStream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        response.Close()
    End Sub

    Private Function GetWSAddress(ChromeCastIP As String) As String

        Dim request As HttpWebRequest = HttpWebRequest.Create("http://" & ChromeCastIP & ":8008/connection/" & AppName)
        request.Method = "POST"
        Dim postData As String = "{""channel"":0,""senderId"":{""appName"":""" & AppName & """, ""senderId"":""" & AppName & """}}"
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
        request.ContentType = "application/json"
        request.Headers.Add("Origin", "chrome-extension://boadgeojelhgndaghljhdicfkmllpafd")
        request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.71 Safari/537.36"
        request.Headers.Add("DNT", "1")
        request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch")
        request.Accept = "*/*"
        request.Headers.Add("Accept-Language", "en-US,en;q=0.8")
        request.KeepAlive = True

        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
        Dim response As WebResponse = request.GetResponse()
        dataStream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        response.Close()

        Dim Des As New JavaScriptSerializer
        Dim Obj As Generic.Dictionary(Of String, Object) = Des.DeserializeObject(responseFromServer)

        Return Obj("URL")
    End Function

End Class
