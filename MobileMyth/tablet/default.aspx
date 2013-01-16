<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_default" EnableViewState="false" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../scripts/jquery.iosslider.js"></script>

    <style type="text/css">
            .iosSlider {
				width: 100%;
				height: 225px;
			}
			
			.iosSlider .slider {
				width: 100%;
				height: 100%;
			}
			
			.iosSlider .slider .item {
				position: relative;
				top: 0;
				left: 0;
				
				width: 100%;
				height: 200px;
				background: #fff;
				margin: 0 0 0 0;
			}
			
			#ctl00_ContentHolder_RecentRecordingsPanel .slide 
			{
			    width: 400px;
			}
			
			#ctl00_ContentHolder_RecentVideosPanel .slide 
			{
			    width: 200px;
			}
    </style>

    <style type="text/css">
        .meter { 
	        height: 15px;  /* Can be anything */
	        position: relative;
	        background: #444;
	        -moz-border-radius: 25px;
	        -webkit-border-radius: 25px;
	        border-radius: 25px;
	        padding: 7px;
	        -webkit-box-shadow: inset 0 -1px 1px rgba(255,255,255,0.3);
	        -moz-box-shadow   : inset 0 -1px 1px rgba(255,255,255,0.3);
	        box-shadow        : inset 0 -1px 1px rgba(255,255,255,0.3);
        }

        .meter > span {
	        display: block;
	        height: 100%;
	           -webkit-border-top-right-radius: 8px;
	        -webkit-border-bottom-right-radius: 8px;
	               -moz-border-radius-topright: 8px;
	            -moz-border-radius-bottomright: 8px;
	                   border-top-right-radius: 8px;
	                border-bottom-right-radius: 8px;
	            -webkit-border-top-left-radius: 20px;
	         -webkit-border-bottom-left-radius: 20px;
	                -moz-border-radius-topleft: 20px;
	             -moz-border-radius-bottomleft: 20px;
	                    border-top-left-radius: 20px;
	                 border-bottom-left-radius: 20px;
	        background-color: rgb(43,194,83);
	        background-image: -webkit-gradient(
	          linear,
	          left bottom,
	          left top,
	          color-stop(0, rgb(43,194,83)),
	          color-stop(1, rgb(84,240,84))
	         );
	        background-image: -webkit-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
	        background-image: -moz-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
	        background-image: -ms-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
	        background-image: -o-linear-gradient(
	          center bottom,
	          rgb(43,194,83) 37%,
	          rgb(84,240,84) 69%
	         );
	        -webkit-box-shadow: 
	          inset 0 2px 9px  rgba(255,255,255,0.3),
	          inset 0 -2px 6px rgba(0,0,0,0.4);
	        -moz-box-shadow: 
	          inset 0 2px 
	          9px  rgba(255,255,255,0.3),
	          inset 0 -2px 6px rgba(0,0,0,0.4);
	        position: relative;
	        overflow: hidden;
        }

        .red > span {
	        background-color: #f0a3a3;
	        background-image: -webkit-gradient(linear,left top,left bottom,color-stop(0, #f0a3a3),color-stop(1, #f42323));
	        background-image: -webkit-linear-gradient(top, #f0a3a3, #f42323);
                background-image: -moz-linear-gradient(top, #f0a3a3, #f42323);
                background-image: -ms-linear-gradient(top, #f0a3a3, #f42323);
                background-image: -o-linear-gradient(top, #f0a3a3, #f42323);
        }
        
        .yellow > span {
	        background-color: #dfc48c;
	        background-image: -webkit-gradient(linear,left top,left bottom,color-stop(0, #dfc48c),color-stop(1, #dfce8e));
	        background-image: -webkit-linear-gradient(top, #dfc48c, #dfc88e);
                background-image: -moz-linear-gradient(top, #dfc48c, #dfc88e);
                background-image: -ms-linear-gradient(top, #dfc48c, #dfc88e);
         
    </style>

    <script type="text/javascript">
        $(document).ready(function () {

            $('.iosSlider').iosSlider({
                desktopClickDrag: true
            });

        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:Panel runat="server" ID="RecentRecordingsPanel" Visible="False" 
    meta:resourcekey="RecentRecordingsPanelResource1">
        <h3 style="margin-top: 0px; margin-bottom: 3px;"><asp:literal runat="server" 
                Text="Recent Recordings" meta:resourcekey="LiteralResource1" /></h3>
        <div class='iosSlider'>
    	    <div class='slider'>
                <asp:PlaceHolder runat="server" ID="RecordingsSlideHolder"></asp:PlaceHolder>
		    </div>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="RecentVideosPanel" Visible="False" 
    meta:resourcekey="RecentVideosPanelResource1">
        <h3 style="margin-bottom: 3px;"><asp:literal runat="server" Text="Recent Videos" 
                meta:resourcekey="LiteralResource2" /></h3>
        <div class='iosSlider'>
    	    <div class='slider'>
                <asp:PlaceHolder runat="server" ID="VideosSliderHolder"></asp:PlaceHolder>
		    </div>
        </div>
    </asp:Panel>

    <div class="ui-grid-a">
	    <div class="ui-block-a">
            <div style="padding-right: 10px;">
             

                <asp:Panel runat="server" ID="ConflictsPanel" Visible="False" 
                    meta:resourcekey="ConflictsPanelResource1">
                    <h3><asp:literal runat="server" Text="Conflicts" 
                            meta:resourcekey="LiteralResource3" /></h3>
                    <ul data-role="listview" data-inset="true" runat="server" id="Conflicts" style="margin-right: 10px;">   
                    </ul>
                </asp:Panel>

                <asp:Panel runat="server" ID="DiskSpacePanel" Visible="False" 
                    meta:resourcekey="DiskSpacePanelResource1">
                    <h3><asp:literal runat="server" Text="Disk Space" 
                            meta:resourcekey="LiteralResource4" /></h3>
                    
                </asp:Panel>
            </div>
        </div>
	    <div class="ui-block-b">
            <asp:Panel runat="server" ID="UpcomingPanel" Visible="False" 
                meta:resourcekey="UpcomingPanelResource1">
                <h3><asp:literal runat="server" Text="Upcoming Recordings" 
                        meta:resourcekey="LiteralResource5" /></h3>
                <ul data-role="listview" data-inset="true" runat="server" id="Upcoming">   
                </ul>
            </asp:Panel>

            <asp:Panel runat="server" ID="EncodersPanel" Visible="False" 
                meta:resourcekey="EncodersPanelResource1">
                <h3><asp:literal runat="server" Text="Encoders" 
                        meta:resourcekey="LiteralResource6" /></h3>
                <ul data-role="listview" data-inset="true" runat="server" id="Encoders">   
                </ul>
            </asp:Panel>

        </div>	   
    </div>

    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>

</asp:Content>



