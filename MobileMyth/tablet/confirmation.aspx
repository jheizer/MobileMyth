<%@ Page Title="" Language="VB" MasterPageFile="dialog.master" AutoEventWireup="false" CodeFile="confirmation.aspx.vb" Inherits="confirmation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="DialogHeader" Runat="Server">
    <h1><asp:literal runat="server" Text="Success" 
            meta:resourcekey="LiteralResource1" /></h1>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="DialogContent" Runat="Server">
    <h2><asp:Label runat="server" ID="Message" meta:resourcekey="MessageResource1"></asp:Label></h2>
    <a href="recordings.aspx" data-role="button" data-theme="c" data-ajax="false">
    <asp:literal runat="server" Text="Ok" meta:resourcekey="LiteralResource2" /></a>   
</asp:Content>

