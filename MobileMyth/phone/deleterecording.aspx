<%@ Page Title="" Language="VB" MasterPageFile="dialog.master" AutoEventWireup="false" CodeFile="deleterecording.aspx.vb" Inherits="deleterecording" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="DialogHeader" Runat="Server">
    <h1>Delete Recording?</h1>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="DialogContent" Runat="Server">
	<h1>Delete page?</h1>
	<p>This is a regular page, styled as a dialog. To create a dialog, just link to a normal page and include a transition and <code>data-rel="dialog"</code> attribute.</p>
	<a href="dialog/index.html" data-role="button" data-rel="back" data-theme="b">Sounds good</a>       
	<a href="dialog/index.html" data-role="button" data-rel="back" data-theme="c">Cancel</a>    
</asp:Content>

