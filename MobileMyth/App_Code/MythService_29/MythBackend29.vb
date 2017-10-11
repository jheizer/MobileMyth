﻿#Region "GPL"
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

'    Copyright 2012-2017 Jonathan Heizer jheizer@gmail.com
#End Region

Imports Microsoft.VisualBasic

Public Class MythBackend29
    Inherits MythBackendBase

    Public Sub New()
        MyBase.New()

        ContentAPI = New MythContent27
        ContentAPI.Init(Address, Port)

        GuideAPI = New MythGuide25
        GuideAPI.Init(Address, Port)

        DvrAPI = New MythDvr29
        DvrAPI.Init(Address, Port)

        ServiceAPI = New MythService25
        ServiceAPI.Init(Address, Port)

        VideoAPI = New MythVideo27
        VideoAPI.Init(Address, Port)

        MyBase.Init()
    End Sub
End Class
