<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="gallery.aspx.vb" Inherits="tablet_gallery" %>
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
    *Warning this is still early.  The previews in .27 are cached in the image folders making a mess so use at your own risk.
    <div class="ui-grid-d">
        <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
    </div> 
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

