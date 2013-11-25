Imports Microsoft.VisualBasic

Public Interface iMythService
    Inherits iMythAPIService

    Function GetStorageGroupDirs(GroupName As String, HostName As String) As MythService.StorageGroupDirList
    Function GetHosts() As String()

End Interface
