<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="mythweb.aspx.vb" Inherits="tablet_mythweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .ui-content {
            padding: 0px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

