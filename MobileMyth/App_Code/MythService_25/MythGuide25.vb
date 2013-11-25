Imports Microsoft.VisualBasic

Public Class MythGuide25
    Implements iMythGuide

    Private Guide As MythGuide.GuideClient

    Public Function Init(Address As String, Port As String) As Boolean Implements iMythAPIService.Init
        Guide = New MythGuide.GuideClient("BasicHttpBinding_Guide", "http://" & Address & ":" & Port & "/Guide")
        Return True
    End Function

    Public Function GetProgramGuide(StartTime As Date, EndTime As Date, StartChanId As Integer, NumChannels As Integer, Details As Boolean) As MythGuide.ProgramGuide Implements iMythGuide.GetProgramGuide
        Return Guide.GetProgramGuide(StartTime, EndTime, StartChanId, NumChannels, Details)
    End Function
End Class
