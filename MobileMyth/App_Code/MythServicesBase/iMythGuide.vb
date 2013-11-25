Imports Microsoft.VisualBasic

Public Interface iMythGuide
    Inherits iMythAPIService

    Function GetProgramGuide(StartTime As Date, EndTime As Date, StartChanId As Integer, NumChannels As Integer, Details As Boolean) As MythGuide.ProgramGuide

End Interface
