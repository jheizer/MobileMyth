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
#End Region

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
