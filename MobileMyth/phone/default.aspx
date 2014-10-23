<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_default" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">

        <div data-role="content">   
			<div data-nativedroid-plugin="homescreen" data-nativedroid-background="http://farm9.staticflickr.com/8457/8064313210_507341f269_z.jpg">
				<div data-nativedroid-role='screenslide'>  

					<div data-nativedroid-role='widget' 
						 data-nativedroid-widget-type="clock"
						 data-nativedroid-widget-clock-lang="en"
						 data-nativedroid-widget-clock-format="short"
						 data-nativedroid-widget-width='2'
						 data-nativedroid-widget-height='1' 
						 data-nativedroid-widget-offset-top='0' 
						 data-nativedroid-widget-offset-left='1'></div>

					<div data-nativedroid-role='widget'
						 data-nativedroid-widget-type='icon' 
						 data-nativedroid-widget-icon-type='text'
						 data-nativedroid-widget-icon-class='icon-home'
						 data-nativedroid-widget-icon-title='Home'
						 data-nativedroid-widget-icon-link='index.html'
						 data-nativedroid-widget-width='1'
						 data-nativedroid-widget-height='1'
						 data-nativedroid-widget-offset-top='4'
						 data-nativedroid-widget-offset-left='0'>
					</div>

					
					<div data-nativedroid-role='widget'
						 data-nativedroid-widget-type='icon' 
						 data-nativedroid-widget-icon-type='text'
						 data-nativedroid-widget-icon-class='icon-picture'
						 data-nativedroid-widget-icon-title='Gallery'
						 data-nativedroid-widget-icon-link='gallery.html'
						 data-nativedroid-widget-width='1'
						 data-nativedroid-widget-height='1'
						 data-nativedroid-widget-offset-top='4'
						 data-nativedroid-widget-offset-left='1'>
					</div>
					
					<div data-nativedroid-role='widget'
						 data-nativedroid-widget-type='icon' 
						 data-nativedroid-widget-icon-type='text'
						 data-nativedroid-widget-icon-class='icon-twitter'
						 data-nativedroid-widget-icon-title='Twitter'
						 data-nativedroid-widget-icon-link='tweetfeed.html'
						 data-nativedroid-widget-width='1'
						 data-nativedroid-widget-height='1'
						 data-nativedroid-widget-offset-top='4'
						 data-nativedroid-widget-offset-left='2'>
					</div>

					<div data-nativedroid-role='widget'
						 data-nativedroid-widget-type='icon' 
						 data-nativedroid-widget-icon-type='text'
						 data-nativedroid-widget-icon-class='icon-font'
						 data-nativedroid-widget-icon-title='Text'
						 data-nativedroid-widget-icon-link='text.html'
						 data-nativedroid-widget-width='1'
						 data-nativedroid-widget-height='1'
						 data-nativedroid-widget-offset-top='4'
						 data-nativedroid-widget-offset-left='3'>
					</div>


				</div>
				<div data-nativedroid-role='screenslide'>

						 
					<div data-nativedroid-role='widget'
						 data-nativedroid-widget-type='reader' 
						 data-nativedroid-widget-reader-type='rss' 
						 data-nativedroid-widget-reader-source="http://jquerymobile.com/feed" 
						 data-nativedroid-widget-width='4'
						 data-nativedroid-widget-height='5'
						 data-nativedroid-widget-offset-top='0'
						 data-nativedroid-widget-offset-left='0'>
					</div>
					

				</div>
				<div data-nativedroid-role='screenslide'>
					
                    <div class="nativeDroidHomescreenWidget nativeDroidWidgetReader"                     
                         data-nativedroid-widget-width='4'
						 data-nativedroid-widget-height='5'
						 data-nativedroid-widget-offset-top='0'
						 data-nativedroid-widget-offset-left='0'>
                        
                        <ul data-role="listview">
                            <li class="widgetTitleBar"><h3>Upcoming Recordings</h3></li>
                            <asp:PlaceHolder runat="server" ID="upcomingdetails"></asp:PlaceHolder>
                            <li><div class="feedActionBox"><a href="upcoming.aspx"><i class="icon-share-alt"></i></a></div><strong>All Upcoming Recordings</strong></li>
                        </ul>
                    </div>
            </div>
				</div>
				
         </div>
</asp:Content>
