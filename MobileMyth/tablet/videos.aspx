<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="videos.aspx.vb" Inherits="tablet_videos" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    div .titlelabel {
        position: absolute;
        float: left;
        z-index: 1000;
        left: 22px;
        top: 100px;
        width: 215px;
        background-color: white;
        padding: 3px;
        color: black;
        opacity: .75;
        text-shadow: 0 /*{a-body-shadow-x} 1px /*{a-body-shadow-y}*/ 1px /*{a-body-shadow-radius}*/ #d1fb53 /*{a-body-shadow-color}*/;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Panel runat="server" ID="FanArtImage" Style="background-color: rgba(250, 250, 250, 0.1)" meta:resourcekey="FanArtImageResource1">
        <div class="ui-grid-d">
            <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
        </div> 
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

