<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="recordings.aspx.vb" Inherits="recordings" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderHolder" Runat="Server">
    <a href="shows.aspx" data-icon="back">Shows</a>
    <h1 runat="server" id="headertitle">Recordings</h1>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="FooterHolder" Runat="Server">
</asp:Content>

