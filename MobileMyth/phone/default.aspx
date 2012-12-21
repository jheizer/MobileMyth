<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_default" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderHolder" Runat="Server">
<h1>MobileMyth.Net</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">

<a href="shows.aspx" data-role="button" data-icon="arrow-r" data-iconpos="right">Recordings</a>
<a href="upcoming.aspx" data-role="button" data-icon="arrow-r" data-iconpos="right">Upcoming Recordings</a>
<a href="status.aspx" data-role="button" data-icon="arrow-r" data-iconpos="right">Backend Status</a>
	
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="FooterHolder" Runat="Server">
</asp:Content>


