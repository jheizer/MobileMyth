

<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="frontendsettings.aspx.vb" Inherits="admin_frontendsettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
  <dl title="Frontend Settings">
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
    </dl>

    <asp:Panel runat="server" ID="TabletSettings" Visible="false">
        <h3>Tablet Home Settings</h3>
        <asp:CheckBox runat="server" ID="RecentRecordings" Text="Show Recent Recordings"/><br />
        <asp:CheckBox runat="server" ID="Recentvideos" Text="Show Recent Videos"/><br />
        <asp:CheckBox runat="server" ID="Conflicts" Text="Show Conflicts"/><br />
        <asp:CheckBox runat="server" ID="DiskSpace" Text="Show Disk Space"/><br />
        <asp:CheckBox runat="server" ID="Encoders" Text="Show Encoders"/><br />
        <asp:CheckBox runat="server" ID="Upcoming" Text="Show Upcoming Recordings"/><br />
    </asp:Panel>

    <asp:Button runat="server" ID="submit" text="Save" UseSubmitBehavior="false" CausesValidation="false" data-ajax="false" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

