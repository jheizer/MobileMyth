<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="video.aspx.vb" Inherits="tablet_video" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Panel runat="server" ID="FanArtImage" 
        style="background-color: rgba(250, 250, 250, 0.85)" 
        meta:resourcekey="FanArtImageResource1">
        <div>
            <div class="ui-grid-a">
	            <div class="ui-block-a" style="padding-left: 10px;">
                    <h1><asp:Label runat="server" ID="VideoTitle" 
                            meta:resourcekey="VideoTitleResource1" ></asp:Label></h1>
                    <h3><asp:Label runat="server" ID="VideoSubTitle" 
                            meta:resourcekey="VideoSubTitleResource1" ></asp:Label></h3>
                    <asp:Label runat="server" ID="VideoDescription" 
                        meta:resourcekey="VideoDescriptionResource1" ></asp:Label>
                </div>
	            <div class="ui-block-b" style="text-align: center;">
                    <asp:Image runat="server" ID="coverimage" Visible="False" 
                        meta:resourcekey="coverimageResource1" />
                </div>
            </div>
            
            <asp:HyperLink runat="server" ID="WatchNowLink" data-role="button" 
                data-icon="arrow-r" data-iconpos="right" data-ajax="false" 
                meta:resourcekey="WatchNowLinkResource1"><asp:literal runat="server" 
                Text="Watch Now" meta:resourcekey="LiteralResource1" />
</asp:HyperLink><asp:HyperLink runat="server" ID="DownloadLink" data-role="button" 
                data-icon="arrow-r" data-iconpos="right" 
                meta:resourcekey="DownloadLinkResource1"><asp:literal runat="server" 
                Text="Download Episode" meta:resourcekey="LiteralResource2" />
</asp:HyperLink><br /><asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>


        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

