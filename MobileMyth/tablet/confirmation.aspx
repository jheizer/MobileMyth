<%@ Page Title="" Language="VB" MasterPageFile="dialog.master" AutoEventWireup="false" CodeFile="confirmation.aspx.vb" Inherits="confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="DialogHeader" Runat="Server">
    <h1>Success</h1>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="DialogContent" Runat="Server">
    <h2><asp:Label runat="server" ID="Message"></asp:Label></h2>
    <a href="recordings.aspx" data-role="button" data-theme="c" data-ajax="false">OK</a>   
</asp:Content>

