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

Imports Microsoft.VisualBasic

Public Interface iMythVideo
    Inherits iMythAPIService

    Function GetVideo(Id As Integer) As VideoMetadataInfo
    Function GetVideoList(Descending As Boolean, StartIndex As Integer, Count As Integer) As List(Of VideoMetadataInfo)

    Class VideoMetadataInfo
        Public Property AddDate As String
        Public Property Artwork As List(Of ArtworkInfo)
        Public Property Banner As String
        Public Property Certification As String
        Public Property Collectionref As Integer
        Public Property ContentType As String
        Public Property Converart As String
        Public Property Description As String
        Public Property Director As String
        Public Property Episode As Integer
        Public Property Fanart As String
        Public Property FileName As String
        Public Property Hash As String
        Public Property HomePage As String
        Public Property HostName As String
        Public Property Id As Integer
        Public Property Inetref As String
        Public Property Length As Integer
        Public Property ParentalLevel As Integer
        Public Property PlayCount As Integer
        Public Property Processed As Boolean
        Public Property ReleaseDate As String
        Public Property Screenshot As String
        Public Property Season As Integer
        Public Property Studio As String
        Public Property SubTitle As String
        Public Property Tagline As String
        Public Property Title As String
        Public Property Trailer As String
        Public Property UserRating As Single
        Public Property Visible As Boolean
        Public Property Watched As Boolean

    End Class

    Class ArtworkInfo
        Public Property FileName As String
        Public Property StorageGroup As String
        Public Property Type As String
        Public Property URL As String
    End Class

End Interface

