<%@ Page Title="" Language="VB" MasterPageFile="~/tablet/MasterPage.master" AutoEventWireup="false" CodeFile="gallery_slideshow.aspx.vb" Inherits="tablet_gallery_slideshow" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript" src="../scripts/jquery.iosslider.js"></script>

    <style type="text/css">
            .iosSlider {
				width: 100%;
				height: 100%;
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
				height: 100%;
				background: #fff;
				margin: 0 0 0 0;
			}
			
            #ctl00_ContentPanel .slide 
			{
			    width: 750px;
			}
    </style>

    <script>
   
      $(document).ready(function () {
        var hash = window.location.hash || '1';
        var startSlide = hash.replace("#", ""); 

        $('.iosSlider').iosSlider({
            snapToChildren: false,
            desktopClickDrag: true,
            infiniteSlider: false,
            snapSlideCenter: false,
            stageCSS: {
                overflow: 'visible'
            },
            scrollbar: true,
            responsiveSlideContainer: true,
            responsiveSlides: true,
            scrollbarLocation: 'bottom',
            frictionCoefficient: 0.98,
            startAtSlide: startSlide,
            onSliderUpdate: updateHeight
        });

    updateHeight();
    
    function updateHeight() {

        var setHeight = $('.iosSlider .item:eq(0)').outerHeight(true);

        $('.iosSlider').css({
            height: (window.innerHeight - 125) + 'px'
        });
        $('.slider').css({
            height: (window.innerHeight - 125) + 'px'
        });
        $('.slide').css({
            height: (window.innerHeight - 125) + 'px'
        });
        $('.slide img').css({
            height: (window.innerHeight - 125) + 'px'
        });
        $('#ctl00_ContentPanel .slide ').css({
            width: (window.innerHeight - 125) * 1.5 + 'px'  //Not working yet
        });
    }

});

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" Runat="Server">
    <div class='iosSlider'>
        <div class='slider'>
            <asp:PlaceHolder runat="server" ID="GallerySlideHolder"></asp:PlaceHolder>
	    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AfterFirstPage" Runat="Server">
</asp:Content>

