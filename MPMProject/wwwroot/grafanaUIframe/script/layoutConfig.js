function screenSize(){
  var size = {
    width: window.innerWidth || document.body.clientWidth,
    height: window.innerHeight || document.body.clientHeight
  }
  // size.width = size.width < 1024?1024:size.width;
  // size.height = size.height < 768?768:size.height;

  size.width = size.width < 0?0:size.width;
  size.height = size.height < 0?0:size.height;

  return size;
}

var layoutConfig ={
  'vw':{
    defaults: {
      maskIframesOnResize: true,
      fxName: "slide",
      fxSpeed: "slow",
      spacing_closed: 0,
      spacing_open:0,
      resizable:false,
      closable:false,
      slidable:true
    },
    center:{
      onresize_end: function(){
        frameUI.resizeFrame();
      },
      onresize:function(){
        frameUI.resizeFrame();
      }
    },
    north: {
      size: screenSize().width/100*2.86,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    },
    west: {
      size: screenSize().width/100*15.6,
      fxName: "none",
      spacing_closed: 0,
      spacing_open:1,
      togglerLength_closed: "100%",
      resizable: true,
      closable: true,
      initClosed: true,
      minSize: screenSize().width/100*10,
      onopen:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerClose');
        $('#wp-menuToggler').addClass('wp-menuTogglerOpen');
      },
      onclose:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerOpen');
        $('#wp-menuToggler').addClass('wp-menuTogglerClose');
      }
    },
    south: {
      size: screenSize().width/100*2.08,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    }
  },
  '1024':{
    defaults: {
      maskIframesOnResize: true,
      fxName: "slide",
      fxSpeed: "slow",
      spacing_closed: 0,
      spacing_open:0,
      resizable:false,
      closable:false,
      slidable:true
    },
    center:{
      onresize_end: function(){
        frameUI.resizeFrame();
      },
      onresize:function(){
        frameUI.resizeFrame();
      }
    },
    north: {
      size: 33,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    },
    west: {
      size: 240,
      fxName: "none",
      spacing_closed: 0,
      spacing_open:1,
      togglerLength_closed: "100%",
      resizable: true,
      closable: true,
      initClosed: true,
      minSize: 200,
      onopen:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerClose');
        $('#wp-menuToggler').addClass('wp-menuTogglerOpen');
      },
      onclose:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerOpen');
        $('#wp-menuToggler').addClass('wp-menuTogglerClose');
      }
    },
    south: {
      size: 20,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    }
  },
  '1440':{
    defaults: {
      maskIframesOnResize: true,
      fxName: "slide",
      fxSpeed: "slow",
      spacing_closed: 0,
      spacing_open:0,
      resizable:false,
      closable:false,
      slidable:true
    },
    center:{
      onresize_end: function(){
        frameUI.resizeFrame();
      },
      onresize:function(){
        frameUI.resizeFrame();
      }
    },
    north: {
      size: 44,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    },
    west: {
      size: 240,
      fxName: "none",
      spacing_closed: 0,
      spacing_open:1,
      togglerLength_closed: "100%",
      resizable: true,
      closable: true,
      initClosed: true,
      minSize: 200,
      onopen:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerClose');
        $('#wp-menuToggler').addClass('wp-menuTogglerOpen');
      },
      onclose:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerOpen');
        $('#wp-menuToggler').addClass('wp-menuTogglerClose');
      }
    },
    south: {
      size: 32,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    }
  },
  '1920':{
    defaults: {
      maskIframesOnResize: true,
      fxName: "slide",
      fxSpeed: "slow",
      spacing_closed: 0,
      spacing_open:0,
      resizable:false,
      closable:false,
      slidable:true
    },
    center:{
      onresize_end: function(){
        frameUI.resizeFrame();
      },
      onresize:function(){
        frameUI.resizeFrame();
      }
    },
    north: {
      size: 55,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    },
    west: {
      size: 240,
      fxName: "none",
      spacing_closed: 0,
      spacing_open:1,
      togglerLength_closed: "100%",
      resizable: true,
      closable: true,
      initClosed: true,
      minSize: 200,
      onopen:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerClose');
        $('#wp-menuToggler').addClass('wp-menuTogglerOpen');
      },
      onclose:function(){
        $('#wp-menuToggler').removeClass('wp-menuTogglerOpen');
        $('#wp-menuToggler').addClass('wp-menuTogglerClose');
      }
    },
    south: {
      size: 39,
      fxName: "none",
      spacing_closed: 0,
      togglerLength_closed: "100%",
      resizable:false
    }
  }
}