Imports Microsoft.VisualBasic

Public Class MythService25
    Implements iMythService

    Private Service As MythService.MythClient

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythAPIService.Init
        Service = New MythService.MythClient("BasicHttpBinding_Myth", "http://" & Address & ":" & Port & "/Myth")
        Return True
    End Function

    Public Function GetStorageGroupDirs(GroupName As String, HostName As String) As MythService.StorageGroupDirList Implements iMythService.GetStorageGroupDirs
        Return Service.GetStorageGroupDirs(GroupName, HostName)
    End Function

    Public Function GetHosts() As String() Implements iMythService.GetHosts
        Return Service.GetHosts().ToArray
    End Function
End Class
