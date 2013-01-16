<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="frontend.aspx.vb" Inherits="tablet_frontend" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function getParameterByName(name) {
            var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
            return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
        }

        function SendCmd(fe, cmd) {
            $.ajax({ url: "../fecontrol.ashx?fe=" + fe + "&cmd=SendAction&p=Action%3D" + cmd });
        }

        
        function UpdateStatus() {
            var fe = getParameterByName("fe");

            $.ajax({ url: "../fecontrol.ashx?fe=" + fe + "&cmd=GetStatus&p=",
                type: 'GET',
                success: function (data) {
                    $("#status").text(data);
                }
            });

            setTimeout('UpdateStatus()', 2000);
        }
        
        $(document).ready(function () {
            UpdateStatus();
        });

    </script>
               
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent">
        <div class="ui-grid-a">
            <div class="ui-block-a" id="status">
                

            </div>
            <div class="ui-block-b">
                <div style="float: right;">
                    <table style="margin-left: 35px;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Image runat="server" ID="up" ImageUrl="../images/105.png" AlternateText="up" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image runat="server" ID="left" ImageUrl="../images/102.png" AlternateText="left" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="selectbtn" ImageUrl="../images/101.png" AlternateText="select" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="right" ImageUrl="../images/104.png" AlternateText="right" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Image runat="server" ID="down" ImageUrl="../images/103.png" AlternateText="down" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Image runat="server" ID="menu" ImageUrl="../images/132.png" AlternateText="Menu" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="exitbtn" ImageUrl="../images/72.png" AlternateText="Exit" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="guide" ImageUrl="../images/133.png" AlternateText="Guide" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="info" ImageUrl="../images/65.png" AlternateText="Info" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image runat="server" ID="stopbtn" ImageUrl="../images/91.png" AlternateText="Stop" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="skipback" ImageUrl="../images/92.png" AlternateText="|<<" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="skipforward" ImageUrl="../images/94.png" AlternateText=">>|" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="play" ImageUrl="../images/98.png" AlternateText="Play" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image runat="server" ID="record" ImageUrl="../images/134.png" AlternateText="Rec" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="rewind" ImageUrl="../images/96.png" AlternateText="<<" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="fastforward" ImageUrl="../images/97.png" AlternateText=">>" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="pause" ImageUrl="../images/93.png" AlternateText="Pause" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" runat="Server">
</asp:Content>
