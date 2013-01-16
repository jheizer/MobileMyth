<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="admin_general" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ MasterType VirtualPath="admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <dl title="Backend Settings">
        <dt><asp:literal runat="server" Text="Backend IP/Hostname" 
                meta:resourcekey="LiteralResource1" /><a href="#ServerPopup" data-rel="popup">?</a>
            <div data-role="popup" id="ServerPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p><asp:literal runat="server" Text="Name or IP to connect to your backend." 
                          meta:resourcekey="LiteralResource2" /></p>
				</div>
		    </div>  
        </dt>
        <dd>
            <asp:TextBox runat="server" ID="ServiceIP" 
                meta:resourcekey="ServiceIPResource1"></asp:TextBox>
        </dd>
        <dt><asp:literal runat="server" Text="Service Port" 
                meta:resourcekey="LiteralResource3" /> <a href="#PortPopup" data-rel="popup">?</a>
            <div data-role="popup" id="PortPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p><asp:literal runat="server" Text="Default: 6544" 
                          meta:resourcekey="LiteralResource4" /></p>
				</div>
		    </div>  
        </dt>
        <dd>
            <asp:TextBox runat="server" ID="ServicePort" 
                meta:resourcekey="ServicePortResource1"></asp:TextBox>
        </dd>
        <dt><asp:literal runat="server" Text="MythWeb URL" 
                meta:resourcekey="LiteralResource5" /> <a href="#MythwebPopup" data-rel="popup">?</a>
            <div data-role="popup" id="MythwebPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p><asp:literal runat="server" Text="Ex: http://MyBackend/mythweb/" 
                          meta:resourcekey="LiteralResource6" /> <br />
                  <asp:literal runat="server" 
                          Text="Put your Mythweb url so we can link to it for incomplete portions on MobileMyth." 
                          meta:resourcekey="LiteralResource7" /></p>
				</div>
		    </div>    
        </dt>
        <dd>
            <asp:TextBox runat="server" ID="MythWebUrl" 
                meta:resourcekey="MythWebUrlResource1"></asp:TextBox>
        </dd>
        <dt><asp:literal runat="server" Text="Date Format" 
                meta:resourcekey="LiteralResource8" /></dt>
        <dd>
            <asp:DropDownList runat="server" ID="dateformat" 
                meta:resourcekey="dateformatResource1">
                <asp:ListItem value="MM/DD/YYYY" text="MM/DD/YYYY" 
                    meta:resourcekey="ListItemResource1"></asp:ListItem>
                <asp:ListItem value="DD/MM/YYYY" text="DD/MM/YYYY" 
                    meta:resourcekey="ListItemResource2"></asp:ListItem>
            </asp:DropDownList>
        </dd>
        <dt><asp:CheckBox runat="server" ID="DeviceList" Text="Allow Device Reporting" 
                style="width: 300px; display: inline-block;" 
                meta:resourcekey="DeviceListResource1"/> <a href="#ReportingPopup" data-rel="popup">?</a>
                <div data-role="popup" id="ReportingPopup" data-overlay-theme="b">
    				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		            <p><asp:literal runat="server" 
                            Text="Loads a remote page in an iframe on the Frontend Settings page only so I can get a list of mobile devices most used to know where to focus my time." 
                            meta:resourcekey="LiteralResource9" /></p>
				    </div>
		        </div>
            </dt>
    </dl>
    <asp:Button runat="server" ID="submit" text="Save" data-ajax="false" 
        rel="external" meta:resourcekey="submitResource1" />

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

