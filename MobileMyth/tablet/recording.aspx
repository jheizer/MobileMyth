<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="recording.aspx.vb" Inherits="recording" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .meter { 
	        height: 15px;  /* Can be anything */
	        position: relative;
	        background: #444;
	        -moz-border-radius: 25px;
	        -webkit-border-radius: 25px;
	        border-radius: 25px;
	        padding: 7px;
	        -webkit-box-shadow: inset 0 -1px 1px rgba(255,255,255,0.3);
	        -moz-box-shadow   : inset 0 -1px 1px rgba(255,255,255,0.3);
	        box-shadow        : inset 0 -1px 1px rgba(255,255,255,0.3);
        }

        .meter > span {
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
	        -webkit-box-shadow: 
	          inset 0 2px 9px  rgba(255,255,255,0.3),
	          inset 0 -2px 6px rgba(0,0,0,0.4);
	        -moz-box-shadow: 
	          inset 0 2px 
	          9px  rgba(255,255,255,0.3),
	          inset 0 -2px 6px rgba(0,0,0,0.4);
	        position: relative;
	        overflow: hidden;
        }
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Panel runat="server" ID="FanArtImage" style="background-color: rgba(250, 250, 250, 0.85)">
        <div>
            <div class="ui-grid-a">
	            <div class="ui-block-a" style="padding-left: 10px;">
                    <h1><asp:Label runat="server" ID="EpisodeTitle" ></asp:Label></h1>
                    <h3><asp:Label runat="server" ID="EpisodeSubTitle" ></asp:Label></h3>
                    <asp:Label runat="server" ID="EpisodeDescription" ></asp:Label><br /><br />
                    <asp:Label runat="server" ID="RecordingDate" ></asp:Label><br />
                    <asp:Label runat="server" ID="OriginalDate" ></asp:Label><br />
                    <asp:Label runat="server" ID="Episode" ></asp:Label><br />
                    
                    <asp:Panel runat="server" ID="TranscodePanel" Visible="false">
                        <div style="text-align: center;">
                            <asp:Label runat="server" id="transcodinginfo"></asp:Label>
                        </div>
                        <asp:Panel class="meter" runat="server" id="progressbar">
	                        <span runat="server" id="progressbarvalue"></span>
                        </asp:Panel>
                    </asp:Panel>
                </div>
	            <div class="ui-block-b" style="text-align: center;">
                    <asp:Image runat="server" ID="coverimage" Visible="false" />
                </div>
            </div>
            
            <asp:HyperLink runat="server" ID="WatchNowLink" data-role="button" data-icon="arrow-r" data-iconpos="right" data-ajax="false">Watch Now</asp:HyperLink>
            <asp:HyperLink runat="server" ID="DownloadLink" data-role="button" data-icon="arrow-r" data-iconpos="right" Visible="false">Download Episode</asp:HyperLink>
            <a href="#popupDialog" data-rel="popup" data-position-to="window" data-role="button" data-transition="pop">Delete</a>
            <br />

            <div data-role="popup" id="popupDialog" data-overlay-theme="a" data-theme="c" style="max-width:400px;" class="ui-corner-all">
		        <div data-role="header" data-theme="a" class="ui-corner-top">
			        <h1>Delete?</h1>
		        </div>
		        <div data-role="content" data-theme="d" class="ui-corner-bottom ui-content">
			        <h3 class="ui-title"><asp:Label runat="server" ID="DeleteTitle"></asp:Label></h3>
			        <p><asp:Label runat="server" ID="DeleteDetails"></asp:Label></p>
			        <a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c">Cancel</a>  
                    <asp:LinkButton runat="server" ID="DeleteButton" data-role="button" data-inline="true" data-transition="flow" data-theme="b" data-ajax="false">Delete</asp:LinkButton>
            
		        </div>
	        </div>
            <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>


        </div>
    </asp:Panel>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

