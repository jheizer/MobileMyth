

<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="frontendsettings.aspx.vb" Inherits="admin_frontendsettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <h3>General Frontend Settings</h3>
  <dl>
        <dt>UI Style</dt>
        <dd>
            <asp:DropDownList runat="server" ID="uitype">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem value="phone" text="Phone (Not Available Yet)"></asp:ListItem>
                <asp:ListItem value="tablet" text="Tablet"></asp:ListItem>
            </asp:DropDownList>
        </dd>
        <dt>Video Quality</dt>
        <dd>
            <asp:DropDownList runat="server" ID="VideoQuality"></asp:DropDownList>
        </dd>
        <dt>Override Myth Service Server (NOT TESTED YET, DON'T USE) <a href="#ServerPopup" data-rel="popup">?</a>
            <div data-role="popup" id="ServerPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p>Ex: If you are connecting remotely via ssh put the port forward server here and only this frontend will use this setting.</p>
				</div>
		    </div>
        </dt>
        <dd>
            <asp:TextBox runat="server" ID="ServiceServer"></asp:TextBox>
        </dd>
        <dt>Override Myth Service Port (NOT TESTED YET, DON'T USE) <a href="#PortPopup" data-rel="popup">?</a>
            <div data-role="popup" id="PortPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p>If you are connecting remotely via ssh put the forwarded port here and only this frontend will use this setting.</p>
				</div>
		    </div>    
        </dt>
        <dd>
            <asp:TextBox runat="server" ID="ServicePort"></asp:TextBox>
        </dd>
        <dt><asp:CheckBox runat="server" ID="UseAnyStream" Text="Use Any Stream" style="width: 300px; display: inline-block;"/> <a href="#AnyPopup" data-rel="popup">?</a>
            <div data-role="popup" id="AnyPopup" data-overlay-theme="b">
				<div class="ui-corner-all ui-body-e" style="padding:5px 15px;">
		          <p>Use an existing stream, if any exist, even if it does not match your resolution settings instead of creating a second transcoding for the same recording.</p>
				</div>
		    </div>
        
        </dt>
    </dl>

    <asp:Panel runat="server" ID="TabletSettings" Visible="false">
        <h3>Tablet Home Settings</h3>
        <div class="ui-grid-a">
	        <div class="ui-block-a">
                <asp:CheckBox runat="server" ID="RecentRecordings" Text="Show Recent Recordings"/><br />
                <asp:CheckBox runat="server" ID="Recentvideos" Text="Show Recent Videos"/><br />
                <asp:CheckBox runat="server" ID="Conflicts" Text="Show Conflicts"/><br />
            </div>
            <div class="ui-block-b">
                <asp:CheckBox runat="server" ID="DiskSpace" Text="Show Disk Space"/><br />
                <asp:CheckBox runat="server" ID="Encoders" Text="Show Encoders"/><br />
                <asp:CheckBox runat="server" ID="Upcoming" Text="Show Upcoming Recordings"/><br />      
            </div>
        </div>
          
    </asp:Panel>

    <asp:Button runat="server" ID="submit" text="Save" UseSubmitBehavior="false" CausesValidation="false" data-ajax="false" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

