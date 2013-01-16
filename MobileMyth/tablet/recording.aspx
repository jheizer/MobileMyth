<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="recording.aspx.vb" Inherits="recording" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .meter
        {
            height: 15px; /* Can be anything */
            position: relative;
            background: #444;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            border-radius: 25px;
            padding: 7px;
            -webkit-box-shadow: inset 0 -1px 1px rgba(255,255,255,0.3);
            -moz-box-shadow: inset 0 -1px 1px rgba(255,255,255,0.3);
            box-shadow: inset 0 -1px 1px rgba(255,255,255,0.3);
        }
        
        .meter > span
        {
            display: block;
            height: 100%;
            -webkit-border-top-right-radius: 8px;
            -webkit-border-bottom-right-radius: 8px;
            -moz-border-radius-topright: 8px;
            -moz-border-radius-bottomright: 8px;
            border-top-right-radius: 8px;
            border-bottom-right-radius: 8px;
            -webkit-border-top-left-radius: 20px;
            -webkit-border-bottom-left-radius: 20px;
            -moz-border-radius-topleft: 20px;
            -moz-border-radius-bottomleft: 20px;
            border-top-left-radius: 20px;
            border-bottom-left-radius: 20px;
            background-color: rgb(43,194,83);
            background-image: -webkit-gradient(
	          linear,
	          left bottom,
	          left top,
	          color-stop(0, rgb(43,194,83)),
	          color-stop(1, rgb(84,240,84))
	         );
            background-image: -webkit-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
            background-image: -moz-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
            background-image: -ms-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
            background-image: -o-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
            -webkit-box-shadow: inset 0 2px 9px rgba(255,255,255,0.3), inset 0 -2px 6px rgba(0,0,0,0.4);
            -moz-box-shadow: inset 0 2px 9px rgba(255,255,255,0.3), inset 0 -2px 6px rgba(0,0,0,0.4);
            position: relative;
            overflow: hidden;
        }
    </style>
    <script type="text/javascript">
        function PlayOnFrontend(fe, chan, st) {
            $.get('../fecontrol.ashx?cmd=PlayRecording&fe=' + fe + '&p=' + encodeURIComponent('ChanId=' + chan + '&StartTime=' + st), function (data) {
                window.location = 'frontend.aspx?fe=' + fe;
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="Server">
    <asp:Panel runat="server" ID="FanArtImage" Style="background-color: rgba(250, 250, 250, 0.85)" meta:resourcekey="FanArtImageResource1">
        <div>
            <div class="ui-grid-a">
                <div class="ui-block-a" style="padding-left: 10px;">
                    <h1>
                        <asp:Label runat="server" ID="EpisodeTitle" meta:resourcekey="EpisodeTitleResource1"></asp:Label></h1>
                    <h3>
                        <asp:Label runat="server" ID="EpisodeSubTitle" meta:resourcekey="EpisodeSubTitleResource1"></asp:Label></h3>
                    <asp:Label runat="server" ID="EpisodeDescription" meta:resourcekey="EpisodeDescriptionResource1"></asp:Label><br />
                    <br />
                    <asp:Label runat="server" ID="RecordingDate" meta:resourcekey="RecordingDateResource1"></asp:Label><br />
                    <asp:Label runat="server" ID="OriginalDate" meta:resourcekey="OriginalDateResource1"></asp:Label><br />
                    <asp:Label runat="server" ID="FileSize" meta:resourcekey="FileSizeResource1"></asp:Label><br />
                    <asp:Label runat="server" ID="Episode" meta:resourcekey="EpisodeResource1"></asp:Label><br />
                    <asp:Panel runat="server" ID="TranscodePanel" Visible="False" meta:resourcekey="TranscodePanelResource1">
                    </asp:Panel>
                </div>
                <div class="ui-block-b" style="text-align: center;">
                    <asp:Image runat="server" ID="coverimage" Visible="False" meta:resourcekey="coverimageResource1" />
                </div>
            </div>
            <asp:HyperLink runat="server" ID="WatchNowLink" data-role="button" data-icon="arrow-r" data-iconpos="right" data-ajax="false" Text="Watch Now" meta:resourcekey="WatchNowLinkResource1" />
            <asp:HyperLink runat="server" ID="DownloadLink" data-role="button" data-icon="arrow-r" data-iconpos="right" Visible="False" Text="Download Episode" meta:resourcekey="DownloadLinkResource1" />

            <table>
                <tr>
                    <td style="width: 100%"><asp:DropDownList runat="server" ID="FEList" meta:resourcekey="FEListResource1"></asp:DropDownList></td>
                    <td style="width: 250px;"><asp:HyperLink runat="server" ID="PlayFE" data-role="button" data-icon="arrow-r" data-iconpos="right" Text="Play On Frontend" meta:resourcekey="PlayFEResource1" /></td>
                </tr>
            </table>

            <a href="#popupDialog" data-rel="popup" data-position-to="window" data-role="button" data-transition="pop">
                <asp:Literal runat="server" Text="Delete" meta:resourcekey="LiteralResource3" /></a>
            <br />
            <div data-role="popup" id="popupDialog" data-overlay-theme="a" data-theme="c" style="max-width: 400px;" class="ui-corner-all">
                <div data-role="header" data-theme="a" class="ui-corner-top">
                    <h1>
                        <asp:Literal runat="server" Text="Delete?" meta:resourcekey="LiteralResource4" /></h1>
                </div>
                <div data-role="content" data-theme="d" class="ui-corner-bottom ui-content">
                    <h3 class="ui-title">
                        <asp:Label runat="server" ID="DeleteTitle" meta:resourcekey="DeleteTitleResource1"></asp:Label></h3>
                    <p>
                        <asp:Label runat="server" ID="DeleteDetails" meta:resourcekey="DeleteDetailsResource1"></asp:Label></p>
                    <a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c">
                        <asp:Literal runat="server" Text="Cancel" meta:resourcekey="LiteralResource5" /></a>
                    <asp:LinkButton runat="server" ID="DeleteButton" data-role="button" data-inline="true" data-transition="flow" data-theme="b" data-ajax="false" meta:resourcekey="DeleteButtonResource1"><asp:literal runat="server" 
                        Text="Delete" meta:resourcekey="LiteralResource6" />
                    </asp:LinkButton></div></div><asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="AfterFirstPage" runat="Server">
</asp:Content>
