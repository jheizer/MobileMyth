<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="viewstream.aspx.vb" Inherits="viewstream" EnableViewState="false" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
    <a href="#popupDialog" data-rel="popup" data-position-to="window" data-role="button" data-transition="pop">Delete</a>
    <br />

    <div data-role="popup" id="popupDialog" data-overlay-theme="a" data-theme="c" style="max-width:400px;" class="ui-corner-all">
		<div data-role="header" data-theme="a" class="ui-corner-top">
			<h1><asp:literal runat="server" Text="Delete?" /></h1>
		</div>
		<div data-role="content" data-theme="d" class="ui-corner-bottom ui-content">
			<h3 class="ui-title"><asp:Label runat="server" ID="DeleteTitle"></asp:Label></h3>
			<p><asp:Label runat="server" ID="DeleteDetails"></asp:Label></p>
			<a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c"><asp:literal runat="server" Text="Cancel" /></a>  
            <asp:LinkButton runat="server" ID="DeleteButton" data-role="button" data-inline="true" data-transition="flow" data-theme="b" data-ajax="false"><asp:literal runat="server" Text="Delete" /></asp:LinkButton>
            
		</div>
	</div>
</asp:Content>


