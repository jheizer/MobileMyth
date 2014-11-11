Imports System.Net

Partial Class tablet_remotelaunch
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(tablet_remotelaunch))

    Protected Sub loader_Tick(sender As Object, e As System.EventArgs) Handles loader.Tick
        loader.Enabled = False

        Try
            Dim WC As New Net.WebClient
            Dim FE As MythFrontend = Frontends.Frontends(Request.QueryString("fe"))

            Select Case FE.Type
                Case Is = FrontendType.MythTV

                    'Check to see if the box sleeps
                    If Not String.IsNullOrEmpty(FE.MAC) Then

                        'Check to see if it is already awake
                        If Not FEAlive(FE.Address) Then
                            Dim Wake As New WOL
                            Wake.Wake(FE.MAC)

                            'Give it a bit to wake up
                            Dim Count As Integer = 0

                            Do
                                System.Threading.Thread.Sleep(1000)
                                Count += 1
                            Loop While FEAlive(FE.Address) AndAlso Count < 10

                        End If
                    End If

                    'Start Playback
                    Dim ret As String = WC.DownloadString("http://" & FE.Address & ":6547/Frontend/PlayRecording?ChanId=" & Request.QueryString("ChanId") & "&StartTime=" & Request.QueryString("StartTime"))

                    Response.Redirect("frontend.aspx?fe=" & FE.Address, False)

                Case Is = FrontendType.ChromeCast
                    Dim cc As New ChromeCast
                    cc.PlayVideo("10.0.0.233", "http://10.0.0.197:6544/StorageGroup/Streaming/3251_20140828235900.mpg.1280x720_1900kV_192kA.m3u8")


            End Select


        Catch ex As Exception
            Logger.Error("Error Launching remote playback", ex)
        End Try
    End Sub

    Private Function FEAlive(Address As String) As Boolean
        Try
            Dim WC As New QuickWebClient
            Dim ret As String = WC.DownloadString("http://" & Address & ":6547/Frontend/GetStatus")

            If ret.Length > 0 Then
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try

        Return False

    End Function

    'WC that onyl waits 3 seconds for a timeout
    Private Class QuickWebClient
        Inherits Net.WebClient

        Protected Overrides Function GetWebRequest(Uri As Uri) As WebRequest
            Dim Req As WebRequest = MyBase.GetWebRequest(Uri)
            Req.Timeout = 3000
            Return Req
        End Function
    End Class

End Class

