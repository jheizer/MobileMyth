Imports Microsoft.VisualBasic
Imports System.Net.Sockets
Imports System.Net

Public Class WOL

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(WOL))

    Public Sub Wake(MAC As String)
        Try
            MAC = MAC.Replace(":", "").Replace("-", "").Trim

            Dim Udp As New UdpClient()
            Udp.EnableBroadcast = True
            Udp.Connect(IPAddress.Broadcast, 7)

            Dim Packet(101) As Byte

            '0xff the first 6 bytes
            For i As Integer = 0 To 6
                Packet(i) = &HFF
            Next

            'Repeate the mac address 16 times
            For i As Integer = 1 To 16
                For j As Integer = 0 To 10 Step 2
                    Packet((6 * i) + (j / 2)) = Convert.ToByte(MAC.Substring(j, 2), 16)
                Next
            Next

            Udp.Send(Packet, Packet.Length)

        Catch ex As Exception
            Logger.Error("Error During WoL ", ex)
        End Try
    End Sub

End Class
