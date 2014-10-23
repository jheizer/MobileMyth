<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="recordings.aspx.vb" Inherits="shows" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../scripts/jquery.mobile.multiview.js" type="text/javascript"></script>
    <script src="../scripts/jquery.mobile.scrollview.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
</asp:Content>


