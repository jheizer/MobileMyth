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

'    Copyright 2012-2014,2017 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic

Public Interface iMythDvr
    Inherits iMythAPIService

    Function GetRecorded(ChanId As Integer, StartTime As DateTime) As Program
    Function GetEncoderList() As EncoderList
    Function GetConflictList(StartIndex As Integer, Count As Integer) As ProgramList
    Function GetUpcomingList(StartIndex As Integer, Count As Integer, ShowAll As Boolean) As ProgramList
    Function RemoveRecorded(ChanId As Integer, StartTs As DateTime) As Boolean
    Function GetRecordedList(Descending As Boolean, StartIndex As Integer, Count As Integer, Cache As Boolean) As ProgramList
    Sub ClearRecordedListCache()
    Function GetTitles() As List(Of iMythDvr.RecordingTitle)

    Class RecordingTitle
        Public Property Title As String
        Public Property Inetref As String
        Public Property Count As String

        Sub New()
        End Sub

        Sub New(Title As String, Inetref As String, Count As String)
            Me.Title = Title
            Me.Inetref = Inetref
            Me.Count = Count
        End Sub
    End Class

    Class Encoder
        Public Sub New()
        End Sub

        Public Property Id As Integer
        Public Property HostName As String
        Public Property Local As Boolean
        Public Property Connected As Boolean
        Public Property State As Integer
        Public Property SleepStatus As Integer
        Public Property LowOnFreeSpace As Boolean
        Public Property Recording As Program
    End Class

    Class Program

        Public Sub New()
        End Sub

        Public Property ProgramId As String
        Public Property Episode As Integer
        Public Property Season As Integer
        Public Property Inetref As String
        Public Property Description As String
        Public Property Airdate As String
        Public Property HostName As String
        Public Property FileName As String
        Public Property ProgramFlags As Integer
        Public Property LastModified As Date?
        Public Property FileSize As Long
        Public Property Stars As Double
        Public Property Artwork As ArtworkInfoList
        Public Property SeriesId As String
        Public Property SubProps As Integer
        Public Property AudioProps As Integer
        Public Property VideoProps As Integer
        Public Property Repeat As Boolean
        Public Property CatType As String
        Public Property Category As String
        Public Property SubTitle As String
        Public Property Title As String
        Public Property EndTime As Date?
        Public Property StartTime As Date?
        Public Property Channel As ChannelInfo
        Public Property Recording As RecordingInfo
    End Class

    Class ArtworkInfoList

        Public Sub New()
        End Sub

        Public Property ArtworkInfos As ArtworkInfo()
    End Class


    Class ArtworkInfo
        Public Sub New()
        End Sub

        Public Property URL As String
        Public Property FileName As String
        Public Property StorageGroup As String
        Public Property Type As String

    End Class

    Class ChannelInfo
        Public Sub New()
        End Sub

        Public Property Modulation As String
        Public Property Visible As Boolean
        Public Property UseEIT As Boolean
        'Public Property CommFree As Integer
        Public Property InputId As Integer
        Public Property SourceId As Integer
        Public Property ChanFilters As String
        Public Property SIStandard As String
        Public Property FineTune As Integer
        Public Property FrequencyTable As String
        Public Property FrequencyId As String
        Public Property Frequency As Long
        Public Property Programs As Program()
        Public Property Format As String
        Public Property ATSCMinorChan As UInteger
        Public Property ATSCMajorChan As UInteger
        Public Property NetworkId As UInteger
        Public Property ServiceId As UInteger
        Public Property TransportId As UInteger
        Public Property MplexId As UInteger
        Public Property ChannelName As String
        Public Property IconURL As String
        Public Property CallSign As String
        Public Property ChanNum As String
        Public Property ChanId As UInteger
        Public Property XMLTVID As String
        Public Property DefaultAuth As String
    End Class

    Class RecordingInfo

        Public Sub New()
        End Sub

        Public Property RecGroup As String
        Public Property Status As Integer
        Public Property Priority As Integer
        Public Property StartTs As Date?
        Public Property EndTs As Date?
        Public Property RecordId As Integer
        Public Property Profile As String
        Public Property PlayGroup As String
        Public Property StorageGroup As String
        Public Property RecType As Integer
        Public Property DupInType As Integer
        Public Property DupMethod As Integer
        Public Property EncoderId As Integer

    End Class

    Class ProgramList
        Public Sub New()
        End Sub

        Public Property StartIndex As Integer
        Public Property Count As Integer
        Public Property TotalAvailable As Integer
        Public Property AsOf As Date?
        Public Property Version As String
        Public Property ProtoVer As String
        Public Property Programs As Program()
    End Class

    Class EncoderList
        Public Sub New()
        End Sub

        Public Property Encoders As Encoder()
    End Class
End Interface
