<%@ WebHandler Language="VB" Class="fecontrol" %>

Imports System
Imports System.Web

Public Class fecontrol : Implements IHttpHandler

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(fecontrol))
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim FE As String = context.Request.QueryString("fe")
            Dim Command As String = context.Request.QueryString("cmd")
            Dim Params As String = HttpUtility.UrlDecode(context.Request.QueryString("p"))
        
            Dim Wc As New Net.WebClient
            Dim ret As String = Wc.DownloadString("http://" & FE & ":6547/Frontend/" & Command & "?" & Params)
        
            If Command = "GetStatus" Then
                ret = ParseStatus(ret)
            End If
            
            context.Response.Write(ret)
        
        Catch ex As Exception
            'Not the end of the world
        End Try
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Function ParseStatus(ByVal Xml As String) As String
        Dim El As XElement = XElement.Parse(Xml)
        
        Dim FEStatus As New Status
        
        Try
            
        FEStatus.State = (From n In El.Descendants("String")
                           Where n.Descendants("Key")(0).Value = "state"
                           Select n.Descendants("Value")(0).Value).FirstOrDefault
        
        If Not FEStatus.State = "idle" Then
                FEStatus.TotalSeconds = (From n In El.Descendants("String")
                                   Where n.Descendants("Key")(0).Value = "totalseconds"
                                   Select n.Descendants("Value")(0).Value).FirstOrDefault
        
                FEStatus.SecondsPlayed = (From n In El.Descendants("String")
                        Where n.Descendants("Key")(0).Value = "secondsplayed"
                        Select n.Descendants("Value")(0).Value).FirstOrDefault
        
                FEStatus.ChanId = (From n In El.Descendants("String")
                        Where n.Descendants("Key")(0).Value = "chanid"
                        Select n.Descendants("Value")(0).Value).FirstOrDefault
        
                FEStatus.StartTime = (From n In El.Descendants("String")
                        Where n.Descendants("Key")(0).Value = "starttime"
                        Select n.Descendants("Value")(0).Value).FirstOrDefault
        
                FEStatus.Title = (From n In El.Descendants("String")
                        Where n.Descendants("Key")(0).Value = "title"
                        Select n.Descendants("Value")(0).Value).FirstOrDefault
        
                FEStatus.SubTitle = (From n In El.Descendants("String")
                            Where n.Descendants("Key")(0).Value = "subtitle"
                            Select n.Descendants("Value")(0).Value).FirstOrDefault
        
            End If
        
        Catch ex As Exception
            FEStatus = New Status
        End Try
        
        Dim Ser As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return Ser.Serialize(FEStatus)
        
    End Function
    
    Private Structure Status
        Public State As String
        Public SecondsPlayed As Integer
        Public TotalSeconds As Integer
        Public Title As String
        Public SubTitle As String
        Public StartTime As String
        Public ChanId As String
    End Structure
    
    
End Class