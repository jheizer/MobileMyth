<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="video.aspx.vb" Inherits="tablet_video" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Panel runat="server" ID="FanArtImage" 
        style="background-color: rgba(250, 250, 250, 0.85)" 
        meta:resourcekey="FanArtImageResource1">
        <div>
            <div class="ui-grid-a">
	            <div class="ui-block-a" style="padding-left: 10px;">
                    <h1><asp:Label runat="server" ID="VideoTitle" 
                            meta:resourcekey="VideoTitleResource1" ></asp:Label></h1>
                    <h3><asp:Label runat="server" ID="VideoSubTitle" 
                            meta:resourcekey="VideoSubTitleResource1" ></asp:Label></h3>
                    <asp:Label runat="server" ID="VideoDescription" 
                        meta:resourcekey="VideoDescriptionResource1" ></asp:Label><br /><br />
                    <asp:Panel runat="server" ID="TranscodePanel" Visible="False">
                    </asp:Panel>
                </div>
	            <div class="ui-block-b" style="text-align: center;">
                    <asp:Image runat="server" ID="coverimage" Visible="False" 
                        meta:resourcekey="coverimageResource1" />
                </div>
            </div>
            
            <asp:HyperLink runat="server" ID="WatchNowLink" data-role="button" 
                data-icon="arrow-r" data-iconpos="right" data-ajax="false" 
                meta:resourcekey="WatchNowLinkResource1"><asp:literal runat="server" 
                Text="Watch Now" meta:resourcekey="LiteralResource1" />
</asp:HyperLink><asp:HyperLink runat="server" ID="DownloadLink" data-role="button" 
                data-icon="arrow-r" data-iconpos="right" 
                meta:resourcekey="DownloadLinkResource1"><asp:literal runat="server" 
                Text="Download Episode" meta:resourcekey="LiteralResource2" />
</asp:HyperLink><br /><asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>


        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

