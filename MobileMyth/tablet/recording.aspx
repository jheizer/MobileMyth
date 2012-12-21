<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="recording.aspx.vb" Inherits="recording" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Panel runat="server" ID="FanArtImage" style="background-color: rgba(250, 250, 250, 0.85)">
        <div>
            <div class="ui-grid-a">
	            <div class="ui-block-a" style="padding-left: 10px;">
                    <h1><asp:Label runat="server" ID="EpisodeTitle" ></asp:Label></h1>
                    <h3><asp:Label runat="server" ID="EpisodeSubTitle" ></asp:Label></h3>
                    <asp:Label runat="server" ID="EpisodeDescription" ></asp:Label>
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

