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

Imports MythContent
Imports MythService
Imports MythDVR
Imports MythVideo

Partial Class startstream
    Inherits System.Web.UI.Page

    Private Shared Logger As log4net.ILog = log4net.LogManager.GetLogger(GetType(startstream))

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim VidSet As VideoResolution = Resolutions.MyResolution
        Dim type As String = Request.QueryString("type")

        Select Case type
            Case Is = "r"

                Dim ChanId As Integer = Integer.Parse(Request.QueryString("chan"))
                Dim Time As Long = Long.Parse(Request.QueryString("time"))
                Dim StartTime As New DateTime(Time)

                Dim Rec As Program = WSCache.DVR.GetRecorded(ChanId, StartTime)
                Dim Str As LiveStreamInfo = PickAStream(Rec.FileName)

                If Str Is Nothing Then
                    Logger.Info("Starting stream on " & Rec.FileName)
                    Str = WSCache.Content.AddRecordingLiveStream(ChanId, StartTime, 0, 0, VidSet.Height, VidSet.VRate, VidSet.ARate, 48000)

                ElseIf Str.CurrentSegment < 3 Then
                    Message.Text = "Transcode Started.  Waiting for stream to reach 1%."

                Else
                    'Lets wait to forward till after one refresh after the encoding starts to give it time to get going
                    Response.Redirect("viewstream.aspx?type=r&chan=" & Rec.Channel.ChanId & "&time=" & Rec.Recording.StartTs.Value.Ticks & "&url=" & HttpUtility.UrlEncode(Str.RelativeURL))

                End If

            Case Is = "v"

                Dim Vid As String = Request.QueryString("vid")
                Dim Vidinfo As VideoMetadataInfo = WSCache.Video.GetVideo(Vid)
                Dim Str As LiveStreamInfo = PickAStream(Vidinfo.FileName)

                If Str Is Nothing Then
                    Logger.Info("Starting stream on " & Vidinfo.FileName)
                    Str = WSCache.Content.AddVideoLiveStream(Vid, 0, 0, VidSet.Height, VidSet.VRate, VidSet.ARate, 48000)

                ElseIf Str.PercentComplete = 0 Then
                    Message.Text = "Transcode Started.  Waiting for stream to reach 1%."

                Else
                    'Lets wait to forward till after one refresh after the encoding starts to give it time to get going
                    Response.Redirect("viewstream.aspx?type=v&url=" & HttpUtility.UrlEncode(Str.RelativeURL))
                End If

        End Select


    End Sub

    Private Function PickAStream(ByVal Filename As String) As LiveStreamInfo
        Dim str As LiveStreamInfo = Nothing
        Dim Streams As LiveStreamInfoList = WSCache.Content.GetFilteredLiveStreamList(Filename)

        If Streams.LiveStreamInfos.Count > 0 Then
            If Not SiteSettings.FrontendSettingBool("UseAnyStream") Then
                'We want our exact settings all the time
                str = Streams.LiveStreamInfos.ToList.Find(Function(s) s.Height = Resolutions.MyResolution.Height)
            Else
                'See if we can find our settings, if not just pick the first one
                str = Streams.LiveStreamInfos.ToList.Find(Function(s) s.Height = Resolutions.MyResolution.Height)
                If str Is Nothing Then
                    str = Streams.LiveStreamInfos(0)
                End If
            End If
        End If

        Return str
    End Function
End Class