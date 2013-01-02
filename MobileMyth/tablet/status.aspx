<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="status.aspx.vb" Inherits="status" EnableViewState="false" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        h1.status
        {
            display: none;
        }
        h2.status
        {
            margin-bottom: 3px;
        }
        h2.status .first
        {
            margin-top: 0px;
        }
        div.schedule a 
        {
            pointer-events: none;
            cursor: default;      
            text-decoration: none;
        }
        div.schedule a span 
        {
            display: none;
        }
        hr
        {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
</asp:Content>


