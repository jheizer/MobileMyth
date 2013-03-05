<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="viewstream.aspx.vb" Inherits="viewstream" EnableViewState="false" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" type="text/javascript" src="../proxy.aspx?url=%2FStorageGroup%2F3rdParty%2FJW_Player%2Fjwplayer.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js"></script>

    <script type="text/javascript">
        function playStreamLink(url, width, height) {
            var jwpcall = 'playStreamInJWPlayer("' + url + '", ' + width + ', ' +
                          height + ');';
            return "<a href='javascript: " + jwpcall + "'><i18n>Play</i18n></a>" +
                " - <a href='" + url + "'>m3u8</a>";
        }

        var jwIsSetup = 0;
        function playStreamInJWPlayer(playlist, width, height) {
            $("#playerwrapper").show();

            if (jwIsSetup) {
                jwplayer('player').load({file: playlist, provider: "../StorageGroup/3rdParty/JW_Player/adaptiveProvider.swf"});
                //jwplayer('player').load({file: playlist, provider: "../proxy.aspx?url=%2FStorageGroup%2F3rdParty%2FJW_Player%2FadaptiveProvider.swf"});
                jwplayer('player').play();
                return;
            }

            jwplayer('player').setup({
                file: playlist,
                width: width,
                height: height,
                modes: [
                  { type: "flash", 
                    src: "../StorageGroup/3rdParty/JW_Player/player.swf", 
                    //src: "../proxy.aspx?url=%2FStorageGroup%2F3rdParty%2FJW_Player%2Fplayer.swf", 
                    config: {
                      file: playlist,
                      provider: "../StorageGroup/3rdParty/JW_Player/adaptiveProvider.swf"
                      //provider: "../proxy.aspx?url=%2FStorageGroup%2F3rdParty%2FJW_Player%2FadaptiveProvider.swf"
                    }
                  },
                  { type: "html5",
                    config: {
                      file: playlist,
                    }
                  },
                  { type: "download" }
                ]
          });
          jwplayer('player').play();
          jwIsSetup = 1;
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>
    <a href="#popupDialog" data-rel="popup" data-position-to="window" data-role="button" data-transition="pop">Delete</a>
    <br />

    <div data-role="popup" id="popupDialog" data-overlay-theme="a" data-theme="c" style="max-width:400px;" class="ui-corner-all">
		<div data-role="header" data-theme="a" class="ui-corner-top">
			<h1><asp:literal runat="server" Text="Delete?" /></h1>
		</div>
		<div data-role="content" data-theme="d" class="ui-corner-bottom ui-content">
			<h3 class="ui-title"><asp:Label runat="server" ID="DeleteTitle"></asp:Label></h3>
			<p><asp:Label runat="server" ID="DeleteDetails"></asp:Label></p>
			<a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c"><asp:literal runat="server" Text="Cancel" /></a>  
            <asp:LinkButton runat="server" ID="DeleteButton" data-role="button" data-inline="true" data-transition="flow" data-theme="b" data-ajax="false"><asp:literal runat="server" Text="Delete" /></asp:LinkButton>
            
		</div>
	</div>
</asp:Content>


