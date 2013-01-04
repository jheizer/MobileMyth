<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="guide.aspx.vb" Inherits="tablet_guide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="maincontent"></asp:PlaceHolder>

        <asp:Panel runat="server" ID="GuideList" class="ui-grid-b">
            <div class="ui-block-a">
	    		<div class="ui-bar ui-bar-e" >
                    &nbsp;
                </div>
            </div>
            <div class="ui-block-b">
	    		<div class="ui-bar ui-bar-e" >
			    	On Now
    			</div>
		    </div>
		</asp:Panel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

