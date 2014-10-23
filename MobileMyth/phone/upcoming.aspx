<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="upcoming.aspx.vb" Inherits="upcoming" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderHolder" Runat="Server">
<h1>Upcoming Recordings</h1>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    
    
    <div data-role="content">   
        <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
    </div>

</asp:Content>


