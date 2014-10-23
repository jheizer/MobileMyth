<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="music.aspx.vb" Inherits="tablet_music" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">

    <div class="ui-grid-c">
        <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
    </div> 

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

