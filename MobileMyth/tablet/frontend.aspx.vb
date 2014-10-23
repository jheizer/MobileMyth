
Partial Class tablet_frontend
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim wo As New WOL
        wo.Wake("48:5b:39:a7:9d:9b")

        Dim fe As String = Request.QueryString("fe")

        up.Attributes.Add("onclick", "SendCmd('" & fe & "', 'UP')")
        down.Attributes.Add("onclick", "SendCmd('" & fe & "', 'DOWN')")
        left.Attributes.Add("onclick", "SendCmd('" & fe & "', 'LEFT')")
        right.Attributes.Add("onclick", "SendCmd('" & fe & "', 'RIGHT')")
        selectbtn.Attributes.Add("onclick", "SendCmd('" & fe & "', 'SELECT')")

        menu.Attributes.Add("onclick", "SendCmd('" & fe & "', 'MENU')")
        exitbtn.Attributes.Add("onclick", "SendCmd('" & fe & "', 'ESCAPE')")
        guide.Attributes.Add("onclick", "SendCmd('" & fe & "', 'GUIDE')")
        info.Attributes.Add("onclick", "SendCmd('" & fe & "', 'INFO')")

        stopbtn.Attributes.Add("onclick", "SendCmd('" & fe & "', 'STOP')")
        skipback.Attributes.Add("onclick", "SendCmd('" & fe & "', 'JUMPRWND')")
        skipforward.Attributes.Add("onclick", "SendCmd('" & fe & "', 'JUMPFFWD')")
        play.Attributes.Add("onclick", "SendCmd('" & fe & "', 'PLAY')")

        record.Attributes.Add("onclick", "SendCmd('" & fe & "', 'TOGGLERECORD')")
        rewind.Attributes.Add("onclick", "SendCmd('" & fe & "', 'SEEKRWND')")
        fastforward.Attributes.Add("onclick", "SendCmd('" & fe & "', 'SEEKFFWD')")
        pause.Attributes.Add("onclick", "SendCmd('" & fe & "', 'PAUSE')")

    End Sub
End Class
