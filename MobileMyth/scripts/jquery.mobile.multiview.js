(function($,window,undefined) {
	$( window.document ).bind('mobileinit', function(){
    	
		if($.support.touch){
			$('html').addClass('touch');
			}						
	
		var replaceBackBtn,
			$popover = $("div:jqmData(panel='popover')"),
			$main = $("div:jqmData(panel='main')"),
			$menu = $("div:jqmData(panel='menu')"),
			$panel = $("div:jqmData(role='panel')");
			
		// active and viewport classes
		$('html').addClass('multiview'); 
		$main.addClass('ui-panel-active');		
		$panel.addClass('ui-mobile-viewport');
	  
		$(function() {
			// unbind
			$(document).unbind('.toolbar');
			$('.ui-page').die('.toolbar');
		
			// why this?
			if( !$.mobile.hashListeningEnabled || !$.mobile.path.stripHash( location.hash ) ){										 			
				var firstPage=$('div:jqmData(role="panel") > div:jqmData(show="first")').page().addClass($.mobile.activePageClass);				
				firstPage.children('div:jqmData(role="content")').attr('data-scroll', 'y');
				// splitView on init
				if ( $('div:jqmData(panel="main") div:jqmData(show="first"):jqmData(splitview="true")').length == 1 ) {					
					splitScreen ( "init" );
					checkWidth ();
					gulliver();
					}										
			} else {								
				// dead end on some backbuttons, deeplinks... 
				// TODO do something
				alert ("dead end, clear url or refresh, otherwise browser may crash after you click OK");				
				} 		
						
			}); 					
		
		$popover.each(function(index) {			
			// panel type - not used - for Photoswipe
			if ( $(this).attr("data-plain") ) {	
				$(this).addClass('ui-popover-embedded');
				} else {
					$(this).addClass('ui-popover');											
					
					if($(this).hasClass('ui-triangle-top')) {
						$(this).prepend('<div class="popover_triangle"></div>');
						}												
					}
					
				// all panels' first pages' close-button
				var $closeFirstPage = ( $(this).hasClass('pop_fullscreen') ) ? 'back' : 'close',
					$backButton = '<a href="#" data-role="button" data-icon="back" data-inline="true" data-iconpos="left" data-theme="a" class="back_popover ui-btn-left closePanel">'+$closeFirstPage+'</a>';
						
				$(this).find('div:jqmData(show="first")').each(function(index) {					
					$(this).find('div:jqmData(role="header") h1').before($backButton);
					$(this).find('.back_popover').buttonMarkup();
				});
			});
		
		// close button func;
		$('.closePanel').live('click', function () {			
			$(this).closest('div:jqmData(role="panel")').fadeToggle('fast').removeClass('ui-panel-active');						
			$('.toggle_popover').removeClass('ui-btn-active');
			});							
								
		// toggle popovers - made with http://www.cagintranet.com/archive/create-an-ipad-like-dropdown-popover/								
		$('.toggle_popover').live('click', function(event) {
			// splitview -switchable- check
			if($(this).jqmData('panel') == "menu" && $('html').hasClass('switchable')) {					
				if ($(this).hasClass('ui-btn-active') ) {
					$('div:jqmData(panel="main") div:jqmData(role="header"), div:jqmData(panel="main") div:jqmData(role="content")').css({'margin-left':'25%','width':'75%'});					
					} else {
					
						$('div:jqmData(panel="main") div:jqmData(role="header"), div:jqmData(panel="main") div:jqmData(role="content")').css({'margin-left':'0px','width':'auto'});
						}				
					}
			if($(this).hasClass('ui-btn-active')) {
				// hide panel
				$(this).removeClass('ui-btn-active');
				var $hide = $(this).jqmData('panel'),
					$hidePanel = $("div:jqmData(id='"+$hide+"')");
					$hidePanel.fadeToggle('fast').removeClass('ui-panel-active');						
				} else {					
					// hide all panels THEN show new
					hideAllPanels();					
					$(this).addClass('ui-btn-active');
					var $show = $(this).jqmData('panel'),
						$showPanel = $("div:jqmData(id='"+$show+"')");																	
					
					$showPanel.fadeToggle('fast').addClass('ui-panel-active');
					$showPanel.find('div:jqmData(show="first")').addClass('ui-page-active');											
					
					// panel scrollview
					if ( $showPanel.data('scrollable', 'Off') ) {
						$showPanel.data('scrollable', 'On');
						scrollMe( $showPanel );
						}
					}
			});
									
			// hide on body click
			$('body').live('click tap', function(event) {
				if (!$(event.target).closest('.ui-popover').length && !$(event.target).closest('.toggle_popover').length) {
					$(".ui-popover").stop(true, true).hide();
					$('.toggle_popover').removeClass($.mobile.activeBtnClass);
				};
			  });
									
			// hide on pageChange
			$('div:jqmData(role="page")').live('pagebeforehide', function(event) {
				hideAllPanels();
				});
														
			function hideAllPanels () {						
				$('.toggle_popover').each ( function ( index ) {
					var $hide = $(this).jqmData('panel'),
						$hidePanel = $("div:jqmData(id='"+$hide+"')");
					
					$hidePanel.find("div:jqmData(role='page')")
							  .not(":jqmData(show='first')")
							  .removeClass('ui-page-active');
					$hidePanel.find("div:jqmData(role='page') .ui-btn-active")
							  .removeClass('ui-btn-active');
							   
				
					if ( $(this).hasClass('ui-btn-active') ) {
						$(this).removeClass('ui-btn-active');
						
						var $hide = $(this).jqmData('panel'),
							$hidePanel = $("div:jqmData(id='"+$hide+"')");
						$hidePanel.fadeToggle('fast')
								  .removeClass('ui-panel-active')
								  
						}
					
					
					});
				};
			

		//existing base tag?
		var $window = $( window ),
			$html = $( 'html' ),
			$head = $( 'head' ),
			$base = $head.children( "base" ),
          
			//tuck away the original document URL minus any fragment.
			documentUrl = $.mobile.path.parseUrl( location.href ),

			//if the document has an embedded base tag, documentBase is set to its
			//initial value. If a base tag does not exist, then we default to the documentUrl.
			documentBase = $base.length ? $.mobile.path.parseUrl( $.mobile.path.makeUrlAbsolute( $base.attr( "href" ), documentUrl.href ) ) : documentUrl;

		function findClosestLink(ele) {
			while (ele){
				if (ele.nodeName.toLowerCase() == "a"){
					break;
					}
				ele = ele.parentNode;
				}
				return ele;
			}

		// The base URL for any given element depends on the page it resides in.
		function getClosestBaseUrl( ele ) {
			// Find the closest page and extract out its url.
			var url = $( ele ).closest( ".ui-page" ).jqmData( "url" ),
				base = documentBase.hrefNoHash;
				if ( !url || !$.mobile.path.isPath( url ) ) {
					url = base;
					}
			return $.mobile.path.makeUrlAbsolute( url, base);
			}

		//simply set the active page's minimum height to screen height, depending on orientation
		function getScreenHeight(){
			var orientation = jQuery.event.special.orientationchange.orientation(),
				port = orientation === "portrait",
				winMin = port ? 480 : 320,
				screenHeight = port ? screen.availHeight : screen.availWidth,
				winHeight = Math.max( winMin, $( window ).height() ),
				pageMin = Math.min( screenHeight, winHeight );

				return pageMin;
			}
	
		// override getScreenHeight for popovers
		function newResetActivePageHeight(){
			var page=$( "." + $.mobile.activePageClass );
				
			page.each(function() {
				var $panelType = $(this).closest('div:jqmData(role="panel")').jqmData('panel');
				var $panelType = $(this).closest('div:jqmData(role="panel")').jqmData('panel');
				if ( $panelType == 'popover' || $panelType == 'menu' ) {
					$(this).css("min-height", "inherit");  		
					} else {
						$(this).css("min-height", getScreenHeight());
						}
				});          
			}

		//override _registerInternalEvents to bind to new methods below
		$.mobile._registerInternalEvents = function(){
			//DONE: bind form submit with this plugin
			// $("form").die('submit');
			$("form").live('submit', function(event){
				var $this = $( this );
				if( !$.mobile.ajaxEnabled || $this.is( ":jqmData(ajax='false')" ) ){ return; }

				var type = $this.attr("method"),
					target = $this.attr("target"),
					url = $this.attr( "action" ),
					$currPanel=$this.parents('div:jqmData(role="panel")'),
					$currPanelActivePage=$currPanel.children('div.'+$.mobile.activePageClass);

				// If no action is specified, browsers default to using the
				// URL of the document containing the form. Since we dynamically
				// pull in pages from external documents, the form should submit
				// to the URL for the source document of the page containing
				// the form.
				if ( !url ) {
					// Get the @data-url for the page containing the form.
					url = getClosestBaseUrl( $this );
					if ( url === documentBase.hrefNoHash ) {
						// The url we got back matches the document base,
						// which means the page must be an internal/embedded page,
						// so default to using the actual document url as a browser
						// would.
						url = documentUrl.hrefNoSearch;
						}
					}

				url = $.mobile.path.makeUrlAbsolute( url, getClosestBaseUrl($this) );

				//external submits use regular HTTP
				if( $.mobile.path.isExternal( url ) || target ) {
					return;
					}

				//temporarily put this here- eventually shud just set it immediately instead of an interim var.
				$.mobile.activePage=$currPanelActivePage;
				// $.mobile.pageContainer=$currPanel;
				$.mobile.changePage(
					url, {
						type: type && type.length && type.toLowerCase() || "get",
						data: $this.serialize(),
						transition: $this.jqmData("transition"),
						direction: $this.jqmData("direction"),
						reloadPage: true,
						pageContainer:$currPanel
						});
				event.preventDefault();
			});

			//add active state on vclick
			$( document ).bind( "vclick", function( event ) {
				var link = findClosestLink( event.target );
					if ( link ) {
						if ( $.mobile.path.parseUrl( link.getAttribute( "href" ) || "#" ).hash !== "#" ) {
							$( link ).closest( ".ui-btn" ).not( ".ui-disabled" ).addClass( $.mobile.activeBtnClass );
							$( "." + $.mobile.activePageClass + " .ui-btn" ).not( link ).blur();
							}
						}
				});

			//DONE: link click event binding for changePage
			//click routing - direct to HTTP or Ajax, accordingly
			$(document).bind( "click", function(event) {
				var link = findClosestLink(event.target);
				if (!link) {
					return;
					}				
																
				var $link = $(link),
				//remove active link class if external (then it won't be there if you come back)
				httpCleanup = function(){
					window.setTimeout( function() { removeActiveLinkClass( true ); }, 200 );
					};				  				
				  				  
				// check for splitview in link or destination page									
				if ( $link.jqmData('splitview') == true || $( $link.attr("href") ).jqmData('splitview') == true ) {					
					splitScreen ( "click" );
					checkWidth ();				
					} else {	
						// splitview should close if 1) destination is in main panel AND 2) menu panel is not active
						if ($link.closest('div:jqmData(id="menu")').length == 0 && $($link.attr('href')).closest('div:jqmData(panel)').jqmData('panel') == "main") {								
							// no-close on clicking collapsibles								
							if ($link.hasClass('ui-collapsible-heading-toggle')) {									
								return;
								}
								
							// clear splitview formatting						
							$('div:jqmData(panel="main")').removeClass( 'ui-panel-left ui-panel-right ui-border-right');								
							$('div:jqmData(id="menu")').addClass('menuHide').removeClass('ui-panel-active');																														
							$('html').removeClass('ui-splitview-active ui-splitview-mode ui-popover-mode switchable');
									
							// clear menu buttons from popover mode								
							replaceBackBtn();
									
							// clear checkWidth 								
							$('div:jqmData(panel="main") div:jqmData(role="header"), div:jqmData(panel="main") div:jqmData(role="content")').css({'margin-left':'', 'width':'auto', 'right':'0'});
																									
							} else {										
								
								// IMPROVE - block href="#" to keep splitview intact if popovers are opened
								if ($link.attr('href') == '#') {									
									event.preventDefault();
									return;
									}								
								}
							}

				  //if there's a data-rel=back attr, go back in history
				  if( $link.is( ":jqmData(rel='back')" ) ) {					
					window.history.back();
					return false;
				  }

				  //if ajax is disabled, exit early
				  if( !$.mobile.ajaxEnabled ){				  					
					httpCleanup();
					//use default click handling
					return;
				  }

				  var baseUrl = getClosestBaseUrl( $link ),

					  //get href, if defined, otherwise fall to null #
					  href = $.mobile.path.makeUrlAbsolute( $link.attr( "href" ) || "#", baseUrl );

				  // XXX_jblas: Ideally links to application pages should be specified as
				  // an url to the application document with a hash that is either
				  // the site relative path or id to the page. But some of the
				  // internal code that dynamically generates sub-pages for nested
				  // lists and select dialogs, just write a hash in the link they
				  // create. This means the actual URL path is based on whatever
				  // the current value of the base tag is at the time this code
				  // is called. For now we are just assuming that any url with a
				  // hash in it is an application page reference.
				  if ( href.search( "#" ) != -1 ) {
					href = href.replace( /[^#]*#/, "" );
					if ( !href ) {
					  //link was an empty hash meant purely
					  //for interaction, so we ignore it.
					  event.preventDefault();
					  return;
					} else if ( $.mobile.path.isPath( href ) ) {
					  //we have apath so make it the href we want to load.
					  href = $.mobile.path.makeUrlAbsolute( href, baseUrl );
					} else {
					  //we have a simple id so use the documentUrl as its base.
					  href = $.mobile.path.makeUrlAbsolute( "#" + href, documentUrl.hrefNoHash );
					}
				  }
			  
				  // Should we handle this link, or let the browser deal with it?
				  var useDefaultUrlHandling = $link.is( "[rel='external']" ) || $link.is( ":jqmData(ajax='false')" ) || $link.is( "[target]" ),
		  
					  // Some embedded browsers, like the web view in Phone Gap, allow cross-domain XHR
					  // requests if the document doing the request was loaded via the file:// protocol.
					  // This is usually to allow the application to "phone home" and fetch app specific
					  // data. We normally let the browser handle external/cross-domain urls, but if the
					  // allowCrossDomainPages option is true, we will allow cross-domain http/https
					  // requests to go through our page loading logic.
					  isCrossDomainPageLoad = ( $.mobile.allowCrossDomainPages && documentUrl.protocol === "file:" && href.search( /^https?:/ ) != -1 ),

					  //check for protocol or rel and its not an embedded page
					  //TODO overlap in logic from isExternal, rel=external check should be
					  // moved into more comprehensive isExternalLink
					  isExternal = useDefaultUrlHandling || ( $.mobile.path.isExternal( href ) && !isCrossDomainPageLoad ),

					  isRefresh=$link.jqmData('refresh'),
					  $targetPanel=$link.jqmData('panel'),
					  $targetContainer=$('div:jqmData(id="'+$targetPanel+'")'),
					  $targetPanelActivePage=$targetContainer.children('div.'+$.mobile.activePageClass),
					  $currPanel=$link.parents('div:jqmData(role="panel")'),
					  //not sure we need this. if you want the container of the element that triggered this event, $currPanel
					  $currContainer=$.mobile.pageContainer,
					  $currPanelActivePage=$currPanel.children('div.'+$.mobile.activePageClass),
					  url=$.mobile.path.stripHash($link.attr("href")),
					  from = null;

					  
						
					  
				  //still need this hack apparently:
				  $('.ui-btn.'+$.mobile.activeBtnClass).removeClass($.mobile.activeBtnClass);
				  $activeClickedLink = $link.closest( ".ui-btn" ).addClass($.mobile.activeBtnClass);

				  if( isExternal ) {
					// block photoSwipe
					if ( $link.hasClass("swipeMe") == true ) {
						return;
						} else {
							httpCleanup();
							//use default click handling
							return;
							}
					}
				  
				  

				  // TODO: [splitview] if this isn't used remove it
				  //prevent # urls from bubbling
				  //path.get() is replaced to combat abs url prefixing in IE
				  // var replaceRegex = new RegExp($.mobile.path.get()+"(?=#)");
				  // if( url.replace(replaceRegex, "") == "#" ){
				  // //for links created purely for interaction - ignore
				  // event.preventDefault();
				  // return;
				  // }

				  // TODO: [splitview] if this isn't used remove it
				  // if( isExternal || hasAjaxDisabled || hasTarget || !$.mobile.ajaxEnabled){
				  // //remove active link class if external (then it won't be there if you come back)
				  // window.setTimeout(function() {removeActiveLinkClass(true);}, 200);

				  // //use default click handling
				  // return;
				  // }

				  //use ajax
				  var transitionVal = $link.jqmData( "transition" ),
					  direction = $link.jqmData("direction"),
					  reverseVal = (direction && direction === "reverse") ||
								// deprecated - remove by 1.0
								$link.jqmData( "back" ),
					  //this may need to be more specific as we use data-rel more
					  role = $link.attr( "data-" + $.mobile.ns + "rel" ) || undefined,
					  hash = $currPanel.jqmData('hash');
					  					  

				  // TODO: [splitview] if this isn't used remove it
				  // //if it's a relative href, prefix href with base url
				  // if( $.mobile.path.isRelativeUrl( url ) && !hadProtocol ){
				  // url = $.mobile.path.makeUrlAbsolute( url );
				  // }

				//url = $.mobile.path.stripHash( url );  
				
				
				//if link refers to an already active panel, stop default action and return
				if ($targetPanelActivePage.attr('data-url') == url || $currPanelActivePage.attr('data-url') == url) {
					if (isRefresh) { //then changePage below because it's a pageRefresh request					
						$.mobile.changePage(href, options={fromPage:from, transition:'fade', reverse:reverseVal, changeHash:false, pageContainer:$targetContainer, reloadPage:isRefresh});																														
						} else { //else preventDefault and return
							event.preventDefault();
							return;
							}
					}
					//if link refers to a page on another panel, changePage on that panel
					else if ($targetPanel && $targetPanel!=$link.parents('div:jqmData(role="panel")')) {						
						var from=$targetPanelActivePage;
						$.mobile.pageContainer=$targetContainer;
						$.mobile.changePage(href, options={fromPage:from, transition:transitionVal, reverse:reverseVal, pageContainer:$targetContainer});												
						
					}
					//if link refers to a page inside the same panel, changePage on that panel
					else {					
						var from=$currPanelActivePage;
						$.mobile.pageContainer=$currPanel;
						var hashChange= (hash == 'false' || hash == 'crumbs')? false : true;
						$.mobile.changePage(href, options={fromPage:from, transition:transitionVal, reverse:reverseVal, changeHash:hashChange, pageContainer:$currPanel});												
						
						//active page must always point to the active page in main - for history purposes.
						$.mobile.activePage=$('div:jqmData(id="main") > div.'+$.mobile.activePageClass);
						}										
					event.preventDefault();
				});

		
        //prefetch pages when anchors with data-prefetch are encountered
        $( ".ui-page" ).live( "pageshow.prefetch", function(){
          var urls = [];
          $( this ).find( "a:jqmData(prefetch)" ).each(function(){
            var url = $( this ).attr( "href" );
            if ( url && $.inArray( url, urls ) === -1 ) {
              urls.push( url );
              $.mobile.loadPage( url );
            }
          });
        } );
      
	  
	  
	  
	  
        //DONE: bind hashchange with this plugin
        //hashchanges are defined only for the main panel - other panels should not support hashchanges to avoid ambiguity
        // $(window).unbind("hashchange");
        $(window).bind( "hashchange", function( e, triggered ) {										  
			
          var to = $.mobile.path.stripHash( location.hash ),
              transitionVal = $.mobile.urlHistory.stack.length === 0 ? "none" : undefined,
              $mainPanel=$('div:jqmData(id="main")'),
              $mainPanelFirstPage=$mainPanel.children('div:jqmData(role="page")').first(),
              $mainPanelActivePage=$mainPanel.children('div.ui-page-active'),
              $menuPanel=$('div:jqmData(id="menu")'),
              $menuPanelFirstPage=$menuPanel.children('div:jqmData(role="page")').first(),
              $menuPanelActivePage=$menuPanel.children('div.ui-page-active'),
              //FIX: temp var for dialogHashKey
              dialogHashKey = "&ui-state=dialog";						  		  		 
			  
          if( !$.mobile.hashListeningEnabled || $.mobile.urlHistory.ignoreNextHashChange ){		    
            $.mobile.urlHistory.ignoreNextHashChange = false;
            return;
          }		  		  
		  
          // special case for dialogs		  		  		  
          if( $.mobile.urlHistory.stack.length > 1 &&
              to.indexOf( dialogHashKey ) > -1 ) {					
            // If current active page is not a dialog skip the dialog and continue
            // in the same direction
            if(!$.mobile.activePage.is( ".ui-dialog" )) {						
              //determine if we're heading forward or backward and continue accordingly past
              //the current dialog			  			
              $.mobile.urlHistory.directHashChange({
                currentUrl: to,
                isBack: function() { window.history.back(); },
                isForward: function() { window.history.forward(); }
              });
              // prevent changepage
              return;
            } else {						
              var setTo = function() { to = $.mobile.urlHistory.getActive().pageUrl; };
              // if the current active page is a dialog and we're navigating
              // to a dialog use the dialog objected saved in the stack
              urlHistory.directHashChange({ currentUrl: to, isBack: setTo, isForward: setTo });
            }
          } else if ( to == dialogHashKey ) { 						
			// photoswipe dialog, fires without .ui-dialig or rel="dialog"			
			e.preventDefault();
			
			
			
			} 

          //if to is defined, load it
          if ( to ){		 				  
            to = ( typeof to === "string" && !$.mobile.path.isPath( to ) ) ? ( '#' + to ) : to;
            $.mobile.pageContainer=$menuPanel;
            //if this is initial deep-linked page setup, then changePage sidemenu as well
            if (!$('div.ui-page-active').length) {
              $.mobile.changePage($menuPanelFirstPage, options={transition:transitionVal, reverse:true, changeHash:false});
            }
            $.mobile.pageContainer=$mainPanel;
            $.mobile.activePage=$mainPanelActivePage.length ? $mainPanelActivePage : undefined;
            $.mobile.changePage(to, options={transition:transitionVal, changeHash:false, pageContainer:$mainPanel});
          }
          //there's no hash, go to the first page in the main panel.
          else {				 
            $.mobile.pageContainer=$mainPanel;
            $.mobile.activePage=$mainPanelActivePage? $mainPanelActivePage : undefined;
            $.mobile.changePage($mainPanelFirstPage, options={transition:transitionVal, changeHash:false, pageContainer:$mainPanel} );
          }
        });

        

        //set page min-heights to be device specific
        $( document ).bind( "pageshow.resetPageHeight", newResetActivePageHeight );
        $( window ).bind( "throttledresize.resetPageHeight", newResetActivePageHeight );

      }; //end _registerInternalEvents

      //DONE: bind orientationchange and resize - the popover
      $(window).bind('orientationchange resize throttledresize', function(event){	  				
				
				if ( $('html').hasClass('ui-splitview-active') ) {
					// call splitview to reformat - I guess only useful for desktop, browser window resize
					// IMPROVE - "OR" is very bad for "orientation & resize"
					splitScreen ( "OR" )
					checkWidth ();
					gulliver ();
					}
	  });	  	  
	  
	  
	  function splitScreen ( event ) {					
			
			var $window=$(window),
				$menu=$('div:jqmData(id="menu")'),
				$main=$('div:jqmData(id="main")'),
				$mainHeader=$main.find('div.'+$.mobile.activePageClass+'> div:jqmData(role="header")'),
				$window=$(window);
        
			function popoverBtn(buttonType) {							
				// loop through all main pages
				$('div:jqmData(panel="main") div:jqmData(role="page")').each(function(index) {
					var $header = $(this).find(':jqmData(role=header)'),
						$button = $header.children('a.ui-btn-left:first-child');
					
					// merge existing left button into controlgroup
					if ( $button.length && $header.children('.menuToggle').length == 0 ) {
						var $buttonTarget = $button.attr('href'),
							$buttonIcon = $button.jqmData('icon'),
							$buttonTitle = $button.attr('title'),
							$buttonText = $button.text(),
							$buttonRel = $button.attr('rel');
																										
						$button.replaceWith('<div class="ui-home popover-btn iconposSwitcher-a toggle_popover menuToggle ui-btn ui-btn-inline ui-btn-icon-left ui-btn-corner-all ui-shadow ui-btn-up-a" data-type="horizontal" data-role="controlgroup"><a class="ui-controlgroup-btn-notext ui-btn ui-btn-up-a ui-btn-inline ui-btn-icon-notext ui-corner-left" data-iconpos="notext" data-inline="true" data-icon="'+$buttonIcon+'" data-role="button" href="'+$buttonTarget+'" rel="'+$buttonRel+'" title="'+$buttonTitle+'" data-theme="a"><span class="ui-btn-inner ui-corner-left"><span class="ui-btn-text">'+$buttonText+'</span><span class="ui-icon ui-icon-stokkers ui-icon-shadow"></span></span></a><a class="popover-btn toggle_popover ui-controlgroup-btn-left ui-btn ui-btn-up-a ui-btn-inline ui-btn-icon-left ui-corner-right ui-controlgroup-last" data-iconpos="left" data-inline="true" data-panel="menu" data-icon="filter" data-role="button" href="#" data-theme="a"><span class="ui-btn-inner ui-corner-right ui-controlgroup-last"><span class="ui-btn-text">Filter</span><span class="ui-icon ui-icon-filter ui-icon-shadow"></span></span></a></div>');	
						
						// or insert plain menu button
						} else {
							
							if ( $header.children('.menuToggle').length == 0 ) {
								//$header.prepend('<a data-iconpos="left" data-inline="true" data-icon="filter" data-role="button" href="#" data-theme="a" class="ui-home popover-btn iconposSwitcher-a toggle_popover menuToggle" data-panel="menu"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Filter</span><span class="ui-icon ui-icon-filter ui-icon-shadow"></span></span></a>');																
								$header.prepend('<a data-iconpos="left" data-inline="true" data-icon="filter" data-role="button" href="#" data-theme="a" class="ui-btn-up-a ui-btn ui-btn-icon-left ui-btn-corner-all ui-shadow ui-home popover-btn iconposSwitcher-a toggle_popover menuToggle" data-panel="menu"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-corner-all"><span class="ui-btn-text">Filter</span><span class="ui-icon ui-icon-filter ui-icon-shadow"></span></span></span></a>');												
								}						
							}
						});
						
					if (buttonType == "switchable") {
						$('html').addClass('switchable');						
					}
				}

			replaceBackBtn = function replaceBackBtn() {											
				
				//if($.mobile.urlstack.length > 1 && !header.children('a:jqmData(rel="back")').length && header.jqmData('backbtn')!=false){
				
				// run through all links in menu with data-panel="main" and replace the menu button with the previous button
				$('div:jqmData(panel="main") div:jqmData(role="page")').each(function(index) {
				
						var $header = $(this).find(':jqmData(role=header)'),
							$button = $header.children('a.ui-btn-left:first-child'),
							$oldButton = '<a data-iconpos="notext" data-theme="a" class="ui-home ui-btn-left ui-btn ui-btn-up-a ui-btn-icon-notext ui-btn-corner-all ui-shadow" data-direction="reverse" data-transition="slide" data-icon="stokkers" data-role="button" href="#" title="Stokkers"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Stokkers</span><span class="ui-icon ui-icon-stokkers ui-icon-shadow"></span></span></a>';					
						
						// if it's a controlgroup
						if ( $header.find('div:first-child').hasClass('ui-controlgroup') ) {	
							// make it a single button
							$header.children('.menuToggle').replaceWith( $oldButton );						
							} else {							
								// there is only a menu toggle button, this should be removed
								$header.children('.menuToggle').remove();
								}
						});	
					
					}

			function popover(){

				// local vars
				var $menu=$('div:jqmData(id="menu")'),
					$main=$('div:jqmData(id="main")');						
					
				$menu.addClass('pop_height pop_menuBox ui-triangle-top ui-panel-hidden ui-panel-active')
					.removeClass('menuHide ui-panel-left ui-border-right')
					.css({'width':'25%', 'min-width':'250px', 'display':'none'});				
				$('html').addClass('ui-splitview-active ui-popover-mode').removeClass('ui-splitview-mode');
												
				if(!$menu.children('.popover_triangle').length){
					$menu.prepend('<div class="popover_triangle"></div>');
					}
				$main.removeClass('ui-panel-right')
						.css('width', ''); 	
		
				// only add ui-popover, if we are not in fullscreen mode, otherwise conflicting CSS
				if( !$('html').hasClass('ui-fullscreen') ) {
					$menu.addClass('ui-popover').removeClass('pop_fullscreen');
					} else {
						$menu.addClass('pop_fullscreen').removeClass('ui-popover');
						}					
				popoverBtn("plain");
				}
		
			function splitView () {					
				// local vars
				var $menu=$('div:jqmData(id="menu")'),
					$main=$('div:jqmData(id="main")');
										
				$menu.removeClass('ui-popover menuHide pop_height pop_menuBox ui-triangle-top ui-panel-visible')
					.addClass('ui-panel-left ui-border-right ui-panel-active')
					.css({'width':'25%', 'min-width':'250px', 'display':''});							
				$menu.children('.popover_triangle').remove();
				$main.addClass('ui-panel-right');
				$('html').addClass('ui-splitview-active ui-splitview-mode').removeClass('ui-popover-mode');						
				
				// override this
					//	.width(function() {
						//	return $(window).width()-$('div[data-id="menu"]').width();
						//  });		

				// init scrollview on menu pages, ensure only fired once, 
				// especially since splitview function fires on resize and or-change, too.
				$menu.find('$div:jqmData(role="page")').each(function(index) {
						var $page = $(this);
						if ( $page.data('scrollable', 'Off') ) {
							$page.data('scrollable', 'On');
							scrollMe( $page );
							}
						});
						
				replaceBackBtn();	
				//popoverBtn("switchable");
			}																
		
		
		
			// orientationchange - original version does not fire when binding to 2+ events
			if(event) {				
				// portrait
				if (window.orientation == 0 || window.orientation == 180 ){
					if($window.width() > 768)  {
						splitView();
						} else {
							// preferred
							popover();
							}					 
					}
					// landscape
					else if (window.orientation == 90 || window.orientation == -90 ) {
					if($window.width() > 768) {	
						//preferred
						splitView();						
						} else {
							popover();
							}
						// click, resize, init events
						} else if ($window.width() < 768){
							popover();
							}
							else if ($window.width() > 768) {
								splitView();
								}		
				}
				
		} // end splitScreen
		
		function checkWidth () {									
				
					//  on click/resize/orientationChange adapt main panel content and header
					// IMPROVE - make '250px' config option
					var $tweakContent = $('div:jqmData(panel="main") div:jqmData(role="content")'),
						$tweakHeader =  $('div:jqmData(panel="main") div:jqmData(role="header")');																								
					
					if ( $('div:jqmData(panel="menu")').width() == '250' ) {						
						// IMPROVE - override content 15px JQM-CSS
						var $newWidth = $(window).width()-250,
							$oldWidth = $(window).width(),
							$oddPaddingWidth = 30;
						
						// if splitview is active
						if ( $('html').hasClass('ui-splitview-active') ) {
							// and SPLITVIEW MODE, tweak content, make scrollbars visible
							if ( $('html').hasClass('ui-splitview-mode') ) {								
								$tweakContent.css({'margin-left':'250px'}).width( $newWidth-$oddPaddingWidth );
								$tweakHeader.css({'margin-left':'250px'}).width( $newWidth );															
								} else {
								// in POPOVER, undo, make scrollbars visible
								$tweakContent.css({'margin-left':''}).width( $oldWidth-$oddPaddingWidth );
								$tweakHeader.css({ 'margin-left':''}).width( $oldWidth );
								} 						
							} 																					
						} else {						
							// if menu > 250px, it's either splitview on a big display or SMALL mode = fullscreen menu
							if ( $('html').hasClass('ui-splitview-active') ) {	
								if ( $('html').hasClass('ui-splitview-mode') ) {
									// SPLITVIEW MODE, tweak content to make scollbars visible
									var $padding = 30,
									$oddWidth = parseInt( Math.round( $(window).width()*0.75 ) ),
									$odd = $oddWidth-$padding;
									$('div:jqmData(panel="main") div:jqmData(role="content")').css({ 'margin-left':'25%'}).width( $odd );
									$('div:jqmData(panel="main") div:jqmData(role="header")').css({ 'margin-left':'25%', 'width': '75%'});
									} else {
										// POPOVER MODE, fullscreen, undo
										$tweakContent.css({'margin-left':''}).width( $oldWidth-$oddPaddingWidth );
										$tweakHeader.css({ 'margin-left':''}).width( $oldWidth );
										}
							
									}				
							}
					}
		
		function gulliver () {			
			
			console.log( "gulliver fired");
			
			//layout mode - add supersize
			if ($.mobile.media("screen and (max-width:320px)")||($.mobile.browser.ie && $(this).width() < 320)) {
			var framed = "small";
			} else if ($.mobile.media("screen and (min-width:768px)")||($.mobile.browser.ie && $(this).width() >= 768)) {
				var framed = "large";
				} else {
					var framed = "medium";
					}		
		
			console.log( framed );
			
			// small = fullscreen
			if (framed == "small") {	

				console.log( "in small");
				
				$('html').addClass('ui-fullscreen');
				$popover.addClass('pop_fullscreen').removeClass('ui-popover ui-popover-embedded');												
				$popover.find('.popover_triangle').remove();
										
				// .iconposSwitcher notext
				$(".iconposSwitcher-div a").attr('data-iconpos','notext').removeClass('ui-btn-icon-left ui-btn-icon-right').addClass('ui-btn-icon-notext');																																												
				$(".iconposSwitcher-a").attr('data-iconpos','notext').removeClass('ui-btn-icon-left ui-btn-icon-right').addClass('ui-btn-icon-notext');																																												
										
				// hide headers
				$('.hideSmallScreen').css('visibility','hidden');						
																									
				//$popover.css('z-index', '997 !important');
																																									
				} else {
				
					console.log( "in mediumlarge");
					
					// "middle" and "large" together for now - curl back changes, take this segment out so it can also be called on resize and ORchange									
					$('html').removeClass('ui-fullscreen');
					$popover.removeClass('pop_fullscreen').addClass('ui-popover ui-popover-embedded');
					$('.ui-triangle-top').prepend('<div class="popover_triangle"></div>');
					
					$(".iconposSwitcher-div a").attr('data-iconpos','notext').addClass('ui-btn-icon-left ui-btn-icon-right').removeClass('ui-btn-icon-notext');																																												
					$(".iconposSwitcher-a").attr('data-iconpos','notext').addClass('ui-btn-icon-left ui-btn-icon-right').removeClass('ui-btn-icon-notext');	
				
					$('.hideSmallScreen').css('visibility','visible');
					}
			
		}      




//----------------------------------------------------------------------------------
//Other event bindings: scrollview, crumbs, data-context and content height adjustments
//----------------------------------------------------------------------------------

      // scrollview for main-pages, data used to ensure scroll init only fires once
				$('div[data-role="page"]').live('pagebeforeshow.scroll', function(event){						
							var $page = $(this);
							if ( $page.data('scrollable', 'Off') ) {
								$page.data('scrollable', 'On');
								scrollMe( $page );
								}
					});				
				
				// IMPROVE - this is not nice, scrollview for menu-pages in splitview mode
				//if ( $('div:jqmData(panel="menu")').hasClass('ui-panel-left') ) {
				//	var $page = $('div:jqmData(panel="menu" .ui-page-active');
				//	scrollMe ( $page );
				//	}
					
				scrollMe = function scrollMe( pageToBeScrolled ) {	
					var $page = pageToBeScrolled
					if ($.support.touch && $page.data('scrollable', 'Off')) {
						$page.data('scrollable', 'On');
						$page.find('div[data-role="content"]').attr('data-scroll', 'y');
						$page.find("[data-scroll]:not(.ui-scrollview-clip)").each(function() {
							var $this = $(this);						
							// XXX: Remove this check for ui-scrolllistview once we've
							//      integrated list divider support into the main scrollview class.
							if ($this.hasClass("ui-scrolllistview")) {
								$this.scrolllistview();
							} else {
								var st = $this.data("scroll") + "";
								var paging = st && st.search(/^[xy]p$/) != -1;
								var dir = st && st.search(/^[xy]/) != -1 ? st.charAt(0) : null;
								
								console.log("dir= "+dir+" paging= "+paging+" st= "+st);
								
								var opts = {};
								if (dir) {
									opts.direction = dir;
									}
								if (paging) {
									opts.pagingEnabled = true;
									}

								var method = $this.data("scroll-method");								
								if (method) {
									opts.scrollMethod = method;
										}				
									
									$this.scrollview(opts);
									}
							});
						}
					}
	  
	  
	  
      //data-hash 'crumbs' handler
      //now that data-backbtn is no longer defaulting to true, lets set crumbs to create itself even when backbtn is not available
      $('div:jqmData(role="page")').live('pagebeforeshow.crumbs', function(event, data){
        var $this = $(this);
        if($this.jqmData('hash') == 'crumbs' || $this.parents('div:jqmData(role="panel")').data('hash') == 'crumbs'){
          if($this.jqmData('hash')!=false && $this.find('.ui-crumbs').length < 1){
            var $header=$this.find('div:jqmData(role="header")');
              backBtn = $this.find('a:jqmData(rel="back")');
			
			
			if (data.prevPage.jqmData('show') == "first" && $this.jqmData('url') != '' )  {										
				if(data.prevPage.jqmData('url') == $this.jqmData('url')){ //if it's a page refresh			  
				  var prevCrumb = data.prevPage.find('.ui-crumbs');
				  crumbify(backBtn, prevCrumb.attr('href'), prevCrumb.find('.ui-btn-text').html());
				}
				else if($.mobile.urlstack.length > 0) {				 								
				  var text = data.prevPage.find('div:jqmData(role="header") .ui-title').html();
				  crumbify(backBtn, '#'+data.prevPage.jqmData('url'), text);
				}
				else if(backBtn.length && $.mobile.urlstack.length <= 1) {
				  backBtn.remove();
				}
			  }
		  }
        }
          
          function crumbify(button, href, text){							
					if(!button.length) {
						$this.find('div:jqmData(role="header")').prepend('<a class="ui-crumbs ui-btn-left" data-icon="arrow-l"></a>');
						button=$header.children('.ui-crumbs').buttonMarkup();
						}
					button.removeAttr('data-rel')
						  .jqmData('direction','reverse')
						  .addClass('ui-crumbs')
						  .attr('href',href);
					button.find('.ui-btn-text').html(text);
				
		  }
      });

      //data-context handler - a page with a link that has a data-context attribute will load that page after this page loads
      //this still needs work - pageTransitionQueue messes everything up.
      $('div:jqmData(role="panel")').live('changepage.context', function(){
        var $this=$(this),
            $currPanelActivePage = $this.children('.' + $.mobile.activePageClass),
            panelContextSelector = $this.jqmData('context'),
            pageContextSelector = $currPanelActivePage.jqmData('context'),
            contextSelector= pageContextSelector ? pageContextSelector : panelContextSelector;
        //if you pass a hash into data-context, you need to specify panel, url and a boolean value for refresh
        if($.type(contextSelector) === 'object') {
          var $targetContainer=$('div:jqmData(id="'+contextSelector.panel+'")'),
              $targetPanelActivePage=$targetContainer.children('div.'+$.mobile.activePageClass),
              isRefresh = contextSelector.refresh === undefined ? false : contextSelector.refresh;
          if(($targetPanelActivePage.jqmData('url') == contextSelector.url && contextSelector.refresh)||(!contextSelector.refresh && $targetPanelActivePage.jqmData('url') != contextSelector.url)){
              $.mobile.changePage(contextSelector.url, options={transition:'fade', changeHash:false, pageContainer:$targetContainer, reloadPage:isRefresh});
          }
        }
        else if(contextSelector && $currPanelActivePage.find(contextSelector).length){
          $currPanelActivePage.find(contextSelector).trigger('click');
        }
      });

      //this measures the height of header and footer and sets content to the appropriate height so
      // that no content is concealed behind header and footer
      $('div:jqmData(role="page")').live('pageshow.contentHeight', function(){	    

        var $this=$(this),
            $header=$this.children(':jqmData(role="header")'),
            $footer=$this.children(':jqmData(role="footer")'),
            thisHeaderHeight=$header.css('display') == 'none' ? 0 : $header.outerHeight(),
            thisFooterHeight=$footer.css('display') == 'none' ? 0 : $footer.outerHeight();
        $this.children(':jqmData(role="content")').css({'top':thisHeaderHeight, 'bottom':thisFooterHeight});				
      })

	 $('div:jqmData(role="page")').live('pagecreate', function () {		
		//window.scrollTo(0, 1);
	 });
  });
})(jQuery,window);


/**
  * Klass.js - copyright @dedfat
  * version 1.0
  * https://github.com/ded/klass
  * Follow our software http://twitter.com/dedfat :)
  * MIT License
  */
!function(a,b){function j(a,b){function c(){}c[e]=this[e];var d=this,g=new c,h=f(a),j=h?a:this,k=h?{}:a,l=function(){this.initialize?this.initialize.apply(this,arguments):(b||h&&d.apply(this,arguments),j.apply(this,arguments))};l.methods=function(a){i(g,a,d),l[e]=g;return this},l.methods.call(l,k).prototype.constructor=l,l.extend=arguments.callee,l[e].implement=l.statics=function(a,b){a=typeof a=="string"?function(){var c={};c[a]=b;return c}():a,i(this,a,d);return this};return l}function i(a,b,d){for(var g in b)b.hasOwnProperty(g)&&(a[g]=f(b[g])&&f(d[e][g])&&c.test(b[g])?h(g,b[g],d):b[g])}function h(a,b,c){return function(){var d=this.supr;this.supr=c[e][a];var f=b.apply(this,arguments);this.supr=d;return f}}function g(a){return j.call(f(a)?a:d,a,1)}var c=/xyz/.test(function(){xyz})?/\bsupr\b/:/.*/,d=function(){},e="prototype",f=function(a){return typeof a===b};if(typeof module!="undefined"&&module.exports)module.exports=g;else{var k=a.klass;g.noConflict=function(){a.klass=k;return this},a.klass=g}}(this,"function")


