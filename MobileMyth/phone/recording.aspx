<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="recording.aspx.vb" Inherits="recording" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderHolder" Runat="Server">
    <span class="ui-title">&nbsp;</span> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Image runat="server" ID="bannerimage" Visible="false" />
    <h1><asp:Label runat="server" ID="EpisodeTitle" ></asp:Label></h1>
    <h3><asp:Label runat="server" ID="EpisodeSubTitle" ></asp:Label></h3>
    <asp:Label runat="server" ID="EpisodeDescription" ></asp:Label>

    <asp:HyperLink runat="server" ID="WatchNowLink" data-role="button" data-icon="arrow-r" data-iconpos="right" data-ajax="false">Watch Now</asp:HyperLink>
    <asp:HyperLink runat="server" ID="DownloadLink" data-role="button" data-icon="arrow-r" data-iconpos="right" Visible="false">Download Episode</asp:HyperLink>
    <asp:HyperLink runat="server" ID="DeleteLink" data-role="button" data-rel="dialog" data-transition="slidedown" href="#deletedialog">Delete</asp:HyperLink>
    <a href="#popupDialog" data-rel="popup" data-position-to="window" data-role="button" data-inline="true" data-transition="pop">Dialog</a>


    <div data-role="popup" id="popupDialog" data-overlay-theme="a" data-theme="c" style="max-width:400px;" class="ui-corner-all">
		<div data-role="header" data-theme="a" class="ui-corner-top">
			<h1>Delete Page?</h1>
		</div>
		<div data-role="content" data-theme="d" class="ui-corner-bottom ui-content">
			<h3 class="ui-title">Are you sure you want to delete this page?</h3>
			<p>This action cannot be undone.</p>
			<a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c">Cancel</a>  
            <asp:LinkButton runat="server" ID="DeleteButton" data-role="button" data-inline="true" data-rel="back" data-transition="flow" data-theme="b" data-ajax="false" >Delete</asp:LinkButton>
            
		</div>
	</div>
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="FooterHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AfterFirstPage" Runat="Server">


</asp:Content>

