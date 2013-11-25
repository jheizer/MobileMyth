Imports Microsoft.VisualBasic

Public Class MythBackend25
    Inherits MythBackendBase

    Public Sub New()
        MyBase.New()

        ContentAPI = New MythContent25
        ContentAPI.Init(Address, Port)

        GuideAPI = New MythGuide25
        GuideAPI.Init(Address, Port)

        DvrAPI = New MythDvr25
        DvrAPI.Init(Address, Port)

        ServiceAPI = New MythService25
        ServiceAPI.Init(Address, Port)

        VideoAPI = New MythVideo25
        VideoAPI.Init(Address, Port)

        MyBase.Init()
    End Sub
End Class
