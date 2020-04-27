
var dateUtil = {}

dateUtil.dateFormat = function (date, format) {
        // Calculate date parts and replace instances in format string accordingly
        format = format.replace("ss", (date.getSeconds() < 10 ? '0' : '') + date.getSeconds()); // Pad with '0' if needed
        format = format.replace("mm", (date.getMinutes() < 10 ? '0' : '') + date.getMinutes()); // Pad with '0' if needed
        format = format.replace("hh", (date.getHours() < 10 ? '0' : '') + date.getHours()); // Pad with '0' if needed
        format = format.replace("DD", (date.getDate() < 10 ? '0' : '') + date.getDate()); // Pad with '0' if needed
        format = format.replace("MM", (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1)); // Months are zero-based
        format = format.replace("YYYY", date.getFullYear());
        return format;
    };


dateUtil.dateString = function(date){
	return dateUtil.dateFormat(date, "YYYY/MM/DD");
};

dateUtil.timeString = function(date){
	return dateUtil.dateFormat(date, "hh:mm:ss");
};

dateUtil.updatDate = function(){
  var dateObj = new Date();
  $('#wp-date').text(dateUtil.dateString(dateObj));
  $('#wp-time').text(dateUtil.timeString(dateObj));
};

//-----------------------------------------------------------------------------
var frameUI = {};

frameUI.windowResize = function(layout, config){
  $(window).on('resize',function(){
    frameUI.outerFrame(layout, config);  
    // clearTimeout(window.resizeEndFlag);
    // window.resizeEndFlag = setTimeout(function(){
    //   frameUI.outerFrame(layout, config);  
    // }, 400);
  });
};

frameUI.screenSize = function(){
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

frameUI.resizeFrame = function(){
  var h = $("#wp-mainContent").height();
  var w = $("#wp-mainContent").width();
  $("#wp-mainContent").children('iframe').height(h - 20);
  $("#wp-mainContent").children('iframe').width(w - 20);

  h = $(".wp-leftFrame").first().height();
  w = $(".wp-leftFrame").first().width();
  $(".wp-leftFrame").first().children('iframe').height(h);
  $(".wp-leftFrame").first().children('iframe').width(w);

  h = $(".wp-rightFrame").first().height();
  w = $(".wp-rightFrame").first().width();
  $(".wp-rightFrame").first().children('iframe').height(h);
  $(".wp-rightFrame").first().children('iframe').width(w);
}

frameUI.layoutSSZoomOut = function(config){
  var rightW, rightH, mainW, mainH, bottomW, bottomH, bottomTop;

  // if(frameUI.screenSize().width <= 1030){
  //   rightW = 256;
  //   rightH = 456;
  //   mainW = frameUI.screenSize().width - rightW - 6;
  //   mainH = rightH;
  //   bottomW = frameUI.screenSize().width;
  //   bottomH = ((frameUI.screenSize().height - rightH -6) < 100)?100:(frameUI.screenSize().height - rightH -6);
  //   bottomTop = rightH + 6;
  // }else if(frameUI.screenSize().width > 1030 && frameUI.screenSize().width <= 1450){
  //   rightW = 360;
  //   rightH = 750;
  //   mainW = frameUI.screenSize().width - rightW - 14;
  //   mainH = rightH;
  //   bottomW = frameUI.screenSize().width;
  //   bottomH = ((frameUI.screenSize().height - rightH -10) < 150)?150:(frameUI.screenSize().height - rightH -10);
  //   bottomTop = rightH + 10;
  // }else if(frameUI.screenSize().width > 1450 && frameUI.screenSize().width <= 1930){
  //   rightW = frameUI.screenSize().width * (540 / 1920);
  //   rightH = frameUI.screenSize().height * (960 / 1080);
  //   mainW = frameUI.screenSize().width - rightW - 14;
  //   mainH = rightH;
  //   bottomW = frameUI.screenSize().width;
  //   bottomH = ((frameUI.screenSize().height - rightH -10) < 100)?100:(frameUI.screenSize().height - rightH -10);
  //   bottomTop = rightH + 10;
  // }else{
  //   rightW = frameUI.screenSize().width * (540 / 1920);
  //   rightH = frameUI.screenSize().height * (960 / 1080);
  //   mainW = frameUI.screenSize().width - rightW - 14;
  //   mainH = rightH;
  //   bottomW = frameUI.screenSize().width;
  //   bottomH = ((frameUI.screenSize().height - rightH -10) < 100)?100:(frameUI.screenSize().height - rightH -10);
  //   bottomTop = rightH + 10;
  // }
  //xxxxxxxxxxxxxxxxxxxxx    temp    xxxxxxxxxxxxxxxxxxxxxxx
  rightW = frameUI.screenSize().width/100 * 25;
  rightH = frameUI.screenSize().height - frameUI.screenSize().width/100 * 5.6;
  mainW = frameUI.screenSize().width - rightW - 14;
  mainH = rightH;
  bottomW = frameUI.screenSize().width;
  bottomH = frameUI.screenSize().width/100 * 5.6;
  bottomTop = rightH + 10;
    

      $('html,body').css({'background-color':frameSetting.layoutSs.backgroundColor});
      //right 
      $('.wp-leftFrame').show();
      $('.wp-leftFrame').css({
          'position':'absolute',
          'right':'0px',
          'top':'0px',
          'width': rightW + 'px',
          'height': rightH + 'px'
      });
      if(config){
        $('#westDashboard').attr('src',config.dashboard.westUrl);
        $('#westDashboard').on('load', function() {
          if(config.frame && config.frame.west && config.frame.west.isGrafana){
            interactDash.toggleNavbar(false,'westDashboard');
            interactDash.toggleSideMenu(false,'westDashboard');  
          }
          
        });
      }
      //bottom
      $('.wp-rightFrame').show();
      $('.wp-rightFrame').css({
          'position':'absolute',
          'left': '0px',
          'top': bottomTop + 'px',
          'width': bottomW + 'px',
          'height': bottomH + 'px'
      });
      if(config){
        $('#eastDashboard').attr('src',config.dashboard.eastUrl);
        $('#eastDashboard').on('load', function() {
          if(config.frame && config.frame.east && config.frame.east.isGrafana){
            interactDash.toggleNavbar(false,'eastDashboard');
            interactDash.toggleSideMenu(false,'eastDashboard');  
          }
        });
      }
      //main
      $('.wp-Lframe').css({
        'position':'absolute',
        'left': '0px',
        'top': '0px',
        'width': mainW + 'px',
        'height': mainH + 'px'
      });
}

frameUI.layoutSSZoomIn = function(config){
      var mainW = frameUI.screenSize().width;
      var mainH = frameUI.screenSize().height;


      $('html,body').css({'background-color':frameSetting.layoutSs.backgroundColor});
      //right 
      $('.wp-leftFrame').hide();

      //bottom
      $('.wp-rightFrame').hide();

      //main
      $('.wp-Lframe').css({
        'position':'absolute',
        'left': '0px',
        'top': '0px',
        'width': mainW + 'px',
        'height': mainH + 'px'
      });
}

frameUI.outerFrame = function(layoutType, config) {

  //change for jquery.layout
  if (frameUI.layout) {
    // if (frameUI.screenSize().width <= 1030) {
    //   frameUI.layout.options.north.size = 33;
    //   frameUI.layout.options.south.size = 20;
    // } else if (frameUI.screenSize().width > 1030 && frameUI.screenSize().width <= 1450) {
    //   frameUI.layout.options.north.size = 44;
    //   frameUI.layout.options.south.size = 32;
    // } else if (frameUI.screenSize().width > 1450 && frameUI.screenSize().width <= 1930) {
    //   frameUI.layout.options.north.size = 55;
    //   frameUI.layout.options.south.size = 39;
    // } else {
    //   frameUI.layout.options.north.size = 55;
    //   frameUI.layout.options.south.size = 39;
    // }

    frameUI.layout.options.north.size = frameUI.screenSize().width/100*15.6;
    frameUI.layout.options.north.size = frameUI.screenSize().width/100*2.86;
    frameUI.layout.options.south.size = frameUI.screenSize().width/100*2.08;
    
    frameUI.layout.resizeAll();
  }

  if (layoutType && frameSetting) {
    //each layout style
    if (layoutType.toLowerCase() == 'ss') {
      if ($('#wp-zoom').hasClass('zoom_in')) {
        frameUI.layoutSSZoomIn();
        frameUI.layout.resizeAll();
      } else if ($('#wp-zoom').hasClass('zoom_out')) {
        frameUI.layoutSSZoomOut();
        frameUI.layout.resizeAll();
      } else {
        $('#wp-zoom').show();
        frameUI.layoutSSZoomOut(config);
      }
      $('#wp-zoom').off('click');
      $('#wp-zoom').on('click', function() {
        if ($('#wp-zoom').hasClass('zoom_in')) {
          $('#wp-zoom').removeClass('zoom_in').addClass('zoom_out');
          frameUI.layoutSSZoomOut();
          frameUI.layout.resizeAll();
        } else {
          $('#wp-zoom').removeClass('zoom_out').addClass('zoom_in');
          frameUI.layoutSSZoomIn();
          frameUI.layout.resizeAll();
        }
      });
    } else {

    }


  } else {
    //$('.wp-Lframe')
    $('.wp-leftFrame').hide();
    $('.wp-rightFrame').hide();
  }
};

frameUI.main = function(config){
  //$('body > .wp-Lframe').layout({ applyDefaultStyles: true });
  var layoutCfg;
  layoutCfg = layoutConfig['vw'];
  // if(frameUI.screenSize().width < 1030){
  //   layoutCfg = layoutConfig['1024'];
  // }else if(frameUI.screenSize().width > 1030 && frameUI.screenSize().width <= 1450){
  //   layoutCfg = layoutConfig['1440'];
  // }else if(frameUI.screenSize().width > 1450 && frameUI.screenSize().width <= 1930){
  //   layoutCfg = layoutConfig['1920'];
  // }else{
  //   layoutCfg = layoutConfig['1920'];
  // }
  frameUI.layout = $('body > .wp-Lframe').layout(layoutCfg);

  frameUI.resizeFrame();

  $('#wp-menuToggler').on('click',function(){
    frameUI.layout.toggle('west');
  });
  
  if(config){
    if(config['menu'] && config['menu']['swipeMenu'] && config['menu']['swipeMenu']['initClosed'] == false){
      frameUI.layout.open('west');
    }
  }



};

frameUI.runConfig = function(ListId,config){
  var iconHtml = '', itemsHtml = '';
  var css = '';
  
  if(config && config['language'] && configLang && configLang[localStorage.getItem('langJs')]){
    frameUI.insertMainMenuItems('items-list',configLang[localStorage.getItem('langJs')]);
  }else{
    frameUI.insertMainMenuItems(ListId,config);  
  }

  if(config['language'] && config['language'].length > 0){
    itemsHtml = '';
    for(var i=0;i<config['language'].length;i++){
      itemsHtml += '      <li class="lang-list-file" data-lang="'+config['language'][i]['abbr']+'">'+config['language'][i]['text']+'</li>';
    }
    $('#lang-list .lang-list-content > ul').html(itemsHtml);
  }else{
    $('#lang-list').hide();
  }
  

  //use URL for default open dashboard  
  var openDashboard = interactDash.getParamFromURL('dashboard');
  var openUrl = interactDash.getParamFromURL('url');
  if(openDashboard && openDashboard.length > 0){
    openDashboard = '/dashboard/db/'+openDashboard;
    var existOpenDash = false;
    $('.item-list-file').each(function(){
      if($(this).attr('data-dashboard') == openDashboard && existOpenDash == false){
        existOpenDash = true;
        $('.item-list-file').removeClass('onclick');
        $(this).addClass('onclick');
      }
    });
  }else if(openUrl && openUrl.length > 0){
    var existOpenDash = false;
    $('.item-list-file').each(function(){
      if($(this).attr('data-url') == openUrl && existOpenDash == false){
        existOpenDash = true;
        $('.item-list-file').removeClass('onclick');
        $(this).addClass('onclick');
      }
    });
  }

  //select default menu item
  if($('#'+ListId).find('.onclick').length > 0){
    $('#'+ListId).find('.onclick').parents('.item-list-content').addClass('item-active');
  }
};

frameUI.insertMainMenuItems = function(ListId,config){
  console.log('config',config);
  $('#wp-srp-title').text(config.title.description);

  var iconHtml = '', itemsHtml = '';
  var css = '';
  for(var i=0;i<config['menu']['list'].length;i++){

    itemsHtml += '<li class="item-list-title" id="item-list-'+config['menu']['list'][i]['id']+'">';
    // itemsHtml += '    <div class="icon-block"  id="item-img-'+config['menu']['list'][i]['id']+'">';
    // itemsHtml += '    </div>';
    itemsHtml += '    <div class="icon-text">';
    itemsHtml +=   config['menu']['list'][i]['title'];
    itemsHtml += '    </div>';
    // itemsHtml += '    <div class="icon-arrow">';
    // itemsHtml += '    </div>';
    itemsHtml += '</li>';
    itemsHtml += '<li class="item-list-content">';
    if(Array.isArray(config['menu']['list'][i]['list'])){
      itemsHtml += '  <ul class="item-list-layer2">';
      var selected = '';
      for(var j=0;j<config['menu']['list'][i]['list'].length;j++){
        if(config['menu']['list'][i]['list'][j]['type'] == 'folder'){
          itemsHtml += '<li class="item-list-folder">';
          itemsHtml += '  <div>'+config['menu']['list'][i]['list'][j]['title']+'</div>';
          itemsHtml += '  <ul class="item-list-layer3">';
          for(var k=0;k<config['menu']['list'][i]['list'][j]['list'].length;k++){
            selected = config['menu']['list'][i]['list'][j]['list'][k]['selected']?'onclick':'';
            config['menu']['list'][i]['list'][j]['list'][k]['selected'] = false;
            if(config['menu']['list'][i]['list'][j]['list'][k]['type'] == 'dashboard'){
              itemsHtml += '    <li class="item-list-file '+selected+'" data-dashboard="'+config['menu']['list'][i]['list'][j]['list'][k]['dashbaord']+'">';
              itemsHtml += config['menu']['list'][i]['list'][j]['list'][k]['title'];
              itemsHtml += '    </li>';
            }else if(config['menu']['list'][i]['list'][j]['list'][k]['type'] == 'url'){
              itemsHtml += '    <li class="item-list-file '+selected+'" data-url="'+config['menu']['list'][i]['list'][j]['list'][k]['url']+'">';
              itemsHtml += config['menu']['list'][i]['list'][j]['list'][k]['title'];
              itemsHtml += '    </li>';
            }else {

            }
          }
          itemsHtml += '  </ul>';
          itemsHtml += '';
          itemsHtml += '</li>';
        }else if(config['menu']['list'][i]['list'][j]['type'] == 'dashboard'){
          selected = config['menu']['list'][i]['list'][j]['selected']?'onclick':'';
          config['menu']['list'][i]['list'][j]['selected'] = false;
          itemsHtml += '    <li class="item-list-file '+selected+'" data-dashboard="'+config['menu']['list'][i]['list'][j]['dashbaord']+'">';
          itemsHtml += config['menu']['list'][i]['list'][j]['title'];
          itemsHtml += '    </li>';
        }else if(config['menu']['list'][i]['list'][j]['type'] == 'url'){
          selected = config['menu']['list'][i]['list'][j]['selected']?'onclick':'';
          config['menu']['list'][i]['list'][j]['selected'] = false;
          itemsHtml += '    <li class="item-list-file '+selected+'" data-url="'+config['menu']['list'][i]['list'][j]['url']+'">';
          itemsHtml += config['menu']['list'][i]['list'][j]['title'];
          itemsHtml += '    </li>';
        }else{

        }
      }
      itemsHtml += '  </ul>';
    }
    itemsHtml += '</li>';

  }

  $('#'+ListId).html(itemsHtml);  
}

frameUI.bindMenuEvent = function(){

  //icon-up-arrow
  $('#items-list').on("click", function(e) {
    if ($(e.target).is('li.item-list-title') ||
      $(e.target).parents('li.item-list-title').length > 0) {
      var that = $(e.target).parents('li.item-list-title').length > 0 ? $(e.target).parents('li.item-list-title').get(0) : e.target;
      var index = $(that).prevAll('.item-list-title').length;
      // console.log(index);
      if ($(that).hasClass('item-active')) {
        if ($(that).find('.icon-arrow').first().hasClass('icon-up-arrow')) {
          $(that).find('.icon-arrow').first().removeClass('icon-up-arrow');
          $('.item-list-content:eq(' + index + ')').removeClass("item-active");
        } else {
          $(that).find('.icon-arrow').first().addClass('icon-up-arrow');
          $('.item-list-content:eq(' + index + ')').addClass("item-active");
        }
      } else {
        $('.item-list-title,.item-list-content').removeClass("item-active");
        $('.item-list-title:eq(' + index + ')').addClass("item-active");
        $('.item-list-content:eq(' + index + ')').addClass("item-active");
        //add arrow
        $('icon-arrow').removeClass('icon-up-arrow');
        $('.item-list-title:eq(' + index + ')').find('.icon-arrow').addClass('icon-up-arrow');
      }
    }else{
    }
  });

  //language click
  $('#lang-list').on("click", function(e) {
    if ($(e.target).is('li.lang-list-title') ||
      $(e.target).parents('li.lang-list-title').length > 0) {
      var that = $(e.target).parents('li.lang-list-title').length > 0 ? $(e.target).parents('li.lang-list-title').get(0) : e.target;
      var index = $(that).prevAll('.lang-list-title').length;
      if ($(that).hasClass('lang-active')) {
        $(that).removeClass("lang-active");
        $(that).next('.lang-list-content').removeClass("lang-active");
      }else{
        $(that).addClass("lang-active");
        $(that).next('.lang-list-content').addClass("lang-active");
      }
    }
    //change language 
    if ($(e.target).is('li.lang-list-file')){
      var that = e.target;
      if($(that).hasClass('onclick')){
      }else{
        $('li.lang-list-file').removeClass('onclick');
        $(that).addClass('onclick'); 
        interactDash.onChangeLang($(that).attr('data-lang'));
      }
      
      
    }
  });

  


  //open dashboard
  $('.item-list-file').off('click');
  $('.item-list-file').on('click',function(){
    $('.item-list-file').removeClass('onclick');
    $(this).addClass('onclick');
    $('#grafanaDashboard').attr('src', $(this).attr('data-dashboard'));
    //interactDash.main('setDashboard', '/dashboard/db/f_save1');
    //if($(this).attr('data-dashboard')){
    //  if($('#grafanaDashboard').attr('src').indexOf(config.dashboard.url) == 0){
    //    interactDash.main('setDashboard', $(this).attr('data-dashboard'));
    //  }else{
    //    interactDash.openDashWithDef(config,$(this).attr('data-dashboard'));
    //    //$('#grafanaDashboard').attr('src',config.dashboard.url + $(this).attr('data-dashboard'));
    //  }
    //}else if($(this).attr('data-url')){
    //  $('#grafanaDashboard').attr('src',$(this).attr('data-url'));
    //}else{

    //}
    
  });
};

frameUI.updateMarquee = function(text){
  $('#wp-marquee').find('marquee').first().html(text);
};

//-----------------------------------------------------------------------------


var interactDash = {}

interactDash.RUNNING_CB = [];//{id,callback}

interactDash.generateCallId = function(){
    var count = 0;
    var dateString = new Date().getTime().toString();
    var tempAry = [];
    for(var i=0;i<interactDash.RUNNING_CB.length;i++){
        if(interactDash.RUNNING_CB[i]["id"].indexOf(dateString) > -1){
            tempAry.push(interactDash.RUNNING_CB[i]["id"]);
        }
    }
    if(tempAry.length > 0){
        tempAry = tempAry.sort(function(x,y){return (x.split("_")[1] * 1 - y.split("_")[1] * 1);});
        count = tempAry[tempAry.length -1].split("_")[1] * 1 + 1;    
    }
    var newId = dateString + '_' + count.toString();
    tempAry = null;
    return newId;
}

interactDash.applyCallback = function(id,res){
    var index = -1;
    for(var i=0;i<interactDash.RUNNING_CB.length;i++){
        if(interactDash.RUNNING_CB[i]["id"] == id){
            index = i;
            if(interactDash.RUNNING_CB[i]["callback"]){
                interactDash.RUNNING_CB[i]["callback"].apply(this,[res]);    
            }
            break;
        }
    }
    if (index > -1) {
        interactDash.RUNNING_CB.splice(index, 1);
    }
}

interactDash.main = function(method,params,callback){
    var id = interactDash.generateCallId();
    interactDash.RUNNING_CB.push({id:id,callback:callback});

    var frame = document.getElementById('grafanaDashboard'); 
    frame.contentWindow.postMessage({id:id,method:method,params:params}, '*'); 

    dashboardMui.initLanguage();
    dashboardMui.muiFunctions.run();
}

interactDash.toggleSideMenu = function(show,iframeId){
    var params = [];
    params.push(show);
    if(!iframeId){
      iframeId = 'grafanaDashboard';
    }
    var frame = document.getElementById(iframeId); 
    frame.contentWindow.postMessage({method:"toggleSideMenu",params:show}, '*'); 
}

interactDash.toggleNavbar = function(show,iframeId){
    var params = [];
    params.push(show);
    if(!iframeId){
      iframeId = 'grafanaDashboard';
    }
    var frame = document.getElementById(iframeId); 
    frame.contentWindow.postMessage({method:"toggleNavbar",params:show}, '*'); 
}

interactDash.getParamFromURL = function(param) {
  var url = window.location.toString();
  var str = "";
  var str_value = "";
  if (url.indexOf("?") != -1) {
    var ary = url.split("?")[1].split("&");
    for (var i = 0; i < ary.length; i++) {
      str = ary[i].split("=")[0];
      if (str == param) {
        str_value = decodeURI(ary[i].split("=")[1]);
        break;
      }
    }
  }
  return str_value;

};

interactDash.onChangeLang = function(lang) {
  localStorage.setItem('langJs', lang);
  dashboardMui.muiFunctions.change(lang);
  if(config && config['language'] && configLang && configLang[lang]){
    frameUI.insertMainMenuItems('items-list',configLang[lang]);
  }
  
  frameUI.bindMenuEvent();
};

if(window && window.addEventListener){
  window.addEventListener('message', function (event) {
    if (event.data) {
      var obj = event.data;
      //extEvent(obj);
      console.info('parent if', event.data);
      interactDash.applyCallback(obj.id,obj.result);
    }else{
      console.info('parent else', event);
    }


    // // IMPORTANT: Check the origin of the data! 
    // if (~event.origin.indexOf('http://yoursite.com')) {
    //   // The data has been sent from your site 

    //   // The data sent with postMessage is stored in event.data 
    //   console.info('data', event.data);
    // } else {
    //   // The data hasn't been sent from your site! 
    //   // Be careful! Do not use it. 
    //   return;
    // }
  });  
}

interactDash.openDashWithDef = function (config, openDashboardPath){
  var grafanaUtl = config.dashboard.url;
  if(openDashboardPath && openDashboardPath.length > 0){

  }else{
    openDashboardPath = '';
  }
  if(grafanaUtl){
    grafanaUtl = grafanaUtl + openDashboardPath;
  }else{
    var protocol = window.location.hostname;//"http:"
    var hostname = window.location.hostname;//"localhost"
    var port = 3000;
    grafanaUtl = protocol+'//'+hostname+':'+port+openDashboardPath;
  }
  $('#grafanaDashboard').attr('src',grafanaUtl);
};


/*
interactDash.toggleSideMenu(true)
interactDash.main('toggleSideMenu',[false])
interactDash.main('getVariableObjs',undefined)
interactDash.main('setVariableObjs',{name:'AAA',option:{text:"2",value:"2"}})
interactDash.main('setDashboard','/dashboard/db/f_save1'); //from， to， refresh
interactDash.main('defaultOption',undefined);
interactDash.main('setOption',{sideMenu:false,dashList:false});
*/