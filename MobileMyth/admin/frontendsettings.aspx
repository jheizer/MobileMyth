<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="frontendsettings.aspx.vb" Inherits="admin_frontendsettings" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <h3>
        <asp:Literal runat="server" Text="General Frontend Settings" meta:resourcekey="LiteralResource1" /></h3>
    <dl>
        <dt>
            <asp:Literal runat="server" Text="UI Style" meta:resourcekey="LiteralResource2" />
        </dt>
        <dd>
            <asp:DropDownList runat="server" ID="uitype" meta:resourcekey="uitypeResource1">
                <asp:ListItem Value="" Text="" meta:resourcekey="ListItemResource1"></asp:ListItem>
                <asp:ListItem Value="phone" Text="Phone (Not Available Yet)" meta:resourcekey="ListItemResource2"></asp:ListItem>
                <asp:ListItem Value="tablet" Text="Tablet" meta:resourcekey="ListItemResource3"></asp:ListItem>
            </asp:DropDownList>
        </dd>
        <dt>
            <asp:Literal runat="server" Text="Video Quality" meta:resourcekey="LiteralResource3" />
        </dt>
        <dd>
            <asp:DropDownList runat="server" ID="VideoQuality" meta:resourcekey="VideoQualityResource1">
            </asp:DropDownList>
        </dd>
        <dt>
            <asp:CheckBox runat="server" ID="UseAnyStream" Text="Use Any Stream" Style="width: 300px; display: inline-block;" meta:resourcekey="UseAnyStreamResource1" />
            <a href="#AnyPopup" data-rel="popup">?</a>
            <div data-role="popup" id="AnyPopup" data-overlay-theme="b">
                <div class="ui-corner-all ui-body-e" style="padding: 5px 15px;">
                    <p>
                        <asp:Literal runat="server" Text="Use an existing stream, if any exist, even if it does not match your resolution settings instead of creating a second transcoding for the same recording." meta:resourcekey="LiteralResource4" /></p>
                </div>
            </div>
        </dt>
        <dt>
            <asp:CheckBox runat="server" ID="ProxyVideo" Text="Proxy Video Stream" Style="width: 300px; display: inline-block;"/>
            <a href="#ProxyPopup" data-rel="popup">?</a>
            <div data-role="popup" id="ProxyPopup" data-overlay-theme="b">
                <div class="ui-corner-all ui-body-e" style="padding: 5px 15px;">
                    <p>
                        <asp:Literal ID="Literal1" runat="server" Text="If you are going to be using your device ourside your network you must check this.  Right now on some devices the player will time out or something and drop the feed.  I have tried numerious methods of proxying the data but on for eaxmple our Kindle Fire HD the video will drop around 30 minutes in usually.  If the transcode is complete or you are on a device that can skip at will it is not a big deal though."  /></p>
                </div>
            </div>
        </dt>
    </dl>
    <asp:Panel runat="server" ID="TabletSettings" Visible="False" meta:resourcekey="TabletSettingsResource1">
        <h3>
            <asp:Literal runat="server" Text="Tablet Home Settings" meta:resourcekey="LiteralResource5" /></h3>
        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:CheckBox runat="server" ID="RecentRecordings" Text="Show Recent Recordings" meta:resourcekey="RecentRecordingsResource1" /><br />
                <asp:CheckBox runat="server" ID="Recentvideos" Text="Show Recent Videos" meta:resourcekey="RecentvideosResource1" /><br />
                <asp:CheckBox runat="server" ID="Conflicts" Text="Show Conflicts" meta:resourcekey="ConflictsResource1" /><br />
            </div>
            <div class="ui-block-b">
                <asp:CheckBox runat="server" ID="DiskSpace" Text="Show Disk Space" meta:resourcekey="DiskSpaceResource1" /><br />
                <asp:CheckBox runat="server" ID="Encoders" Text="Show Encoders" meta:resourcekey="EncodersResource1" /><br />
                <asp:CheckBox runat="server" ID="Upcoming" Text="Show Upcoming Recordings" meta:resourcekey="UpcomingResource1" /><br />
            </div>
        </div>
        <h3>
            <asp:Literal runat="server" Text="Tablet General Settings" meta:resourcekey="LiteralResource6" /></h3>
        <dl>
            <dt>
                <asp:CheckBox runat="server" ID="NoImages" Text="No Extra Images" Style="width: 300px; display: inline-block;" meta:resourcekey="NoImagesResource1" />
                <a href="#AnyPopup" data-rel="popup">?</a>
                <div data-role="popup" id="Div1" data-overlay-theme="b">
                    <div class="ui-corner-all ui-body-e" style="padding: 5px 15px;">
                        <p>
                            <asp:Literal runat="server" Text="Skip loading covert art, previews, fan art, etc" meta:resourcekey="LiteralResource7" /></p>
                    </div>
                </div>
            </dt>
        </dl>
    </asp:Panel>
    <asp:Button runat="server" ID="submit" Text="Save" UseSubmitBehavior="False" CausesValidation="False" data-ajax="false" meta:resourcekey="submitResource1" />
    <br />
    <asp:Panel runat="server" ID="DeviceLog" meta:resourcekey="DeviceLogResource1">
        <iframe src="http://www.mobilemyth.net/index.php/mobiledevicetracker/" style="height: 0px; width: 0px; display: none;" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" runat="Server">
</asp:Content>
