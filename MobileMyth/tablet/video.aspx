<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="video.aspx.vb" Inherits="tablet_video" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Panel runat="server" ID="FanArtImage" style="background-color: rgba(250, 250, 250, 0.85)">
        <div>
            <div class="ui-grid-a">
	            <div class="ui-block-a" style="padding-left: 10px;">
                    <h1><asp:Label runat="server" ID="VideoTitle" ></asp:Label></h1>
                    <h3><asp:Label runat="server" ID="VideoSubTitle" ></asp:Label></h3>
                    <asp:Label runat="server" ID="VideoDescription" ></asp:Label>
                </div>
	            <div class="ui-block-b" style="text-align: center;">
                    <asp:Image runat="server" ID="coverimage" Visible="false" />
                </div>
            </div>
            
            <asp:HyperLink runat="server" ID="WatchNowLink" data-role="button" data-icon="arrow-r" data-iconpos="right" data-ajax="false">Watch Now</asp:HyperLink>
            <asp:HyperLink runat="server" ID="DownloadLink" data-role="button" data-icon="arrow-r" data-iconpos="right">Download Episode</asp:HyperLink>
            <br />

            <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>


        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

