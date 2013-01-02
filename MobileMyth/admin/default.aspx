<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="admin_general" %>
<%@ MasterType VirtualPath="admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <dl title="Backend Settings">
        <dt>Backend IP/Hostname <a href="#ServerPopup" data-rel="popup">?</a>
            <div data-role="popup" id="ServerPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p>DNS Name or IP to connect to your backend.</p>
				</div>
		    </div>  
        </dt>
        <dd>
            <asp:TextBox runat="server" ID="ServiceIP"></asp:TextBox>
        </dd>
        <dt>Backend Service Port <a href="#PortPopup" data-rel="popup">?</a>
            <div data-role="popup" id="PortPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p>Default: 6544</p>
				</div>
		    </div>  
        </dt>
        <dd>
            <asp:TextBox runat="server" ID="ServicePort"></asp:TextBox>
        </dd>
        <dt>MythWeb URL <a href="#MythwebPopup" data-rel="popup">?</a>
            <div data-role="popup" id="MythwebPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p>Ex: http://MyBackend/mythweb/ <br />Put your Mythweb url so we can link to it for incomplete portions on MobileMyth.</p>
				</div>
		    </div>    
        </dt>
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
    Copyright 2012, 2013 Jonathan Heizer jheizer@gmail.com
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

