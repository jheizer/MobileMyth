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

Imports Microsoft.VisualBasic

Public Class HtmlList
    Inherits HtmlGenericControl

    Public Sub New()
        Me.TagName = "ul"
    End Sub

End Class

Public Class HtmlListItem
    Inherits HtmlGenericControl

    Public Sub New()
        Me.TagName = "li"
    End Sub

End Class

Public Class HtmlSpan
    Inherits HtmlGenericControl

    Public Sub New()
        Me.TagName = "span"
    End Sub
End Class


Public Class ProgressBar
    Inherits Panel

    Public Sub New(ByVal Text As String, ByVal Percent As Integer)
        Me.New(Text, Percent, "")
    End Sub

    Public Sub New(ByVal Text As String, ByVal Percent As Integer, ByVal Color As String)
        If Not String.IsNullOrEmpty(Text) Then
            Dim Txt As New Panel
            Txt.Style.Add("text-align", "center")
            Dim Lbl As New Label
            Lbl.Text = Text
            Txt.Controls.Add(Lbl)
            Me.Controls.Add(Txt)
        End If

        Dim Meter As New Panel
        Meter.CssClass = "meter " & Color
        Dim Value As New HtmlSpan
        Value.Style.Add("width", Percent.ToString("##") & "%")
        Meter.Controls.Add(Value)
        Me.Controls.Add(Meter)

    End Sub
End Class