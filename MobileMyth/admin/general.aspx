<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="general.aspx.vb" Inherits="admin_general" %>
<%@ MasterType VirtualPath="admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <dl title="Backend Settings">
        <dt>Backend IP/Hostname</dt>
        <dd>
            <asp:TextBox runat="server" ID="ServiceIP"></asp:TextBox>
        </dd>
        <dt>Backend Service Port</dt>
        <dd>
            <asp:TextBox runat="server" ID="ServicePort"></asp:TextBox>
        </dd>
        <dt>MythWeb URL  (Ex: http://MyBackend/mythweb/)</dt>
        <dd>
            <asp:TextBox runat="server" ID="MythWebUrl"></asp:TextBox>
        </dd>
    </dl>
    <asp:Button runat="server" ID="submit" text="Save" data-ajax="false" rel="external" />

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <a href="http://mobilemyth.net">MobileMyth Home Page</a><br />
    GNU General Public License.<br />
    Copyright 2012 Jonathan Heizer jheizer@gmail.com
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

