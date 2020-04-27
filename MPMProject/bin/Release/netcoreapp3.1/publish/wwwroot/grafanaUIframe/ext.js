(function(global) {

  function extInit() {

    if (jQuery('#app1').length == 0) {
      var node = document.createElement("div");
      node.id = "app1";
      node.setAttribute("ng-app", "app1");
      node.setAttribute("ng-controller", "App1Ctrl");
      document.body.appendChild(node);
      angular.module('app1', ['grafana', 'grafana.core', 'grafana.services']).service('hex', function() {
        this.myFunc = function(x) {
          //alert("123");
        }
      }).controller('App1Ctrl', function($scope, variableSrv, contextSrv, searchSrv, timeSrv, hex) {
        $scope.text = function() {
          //console.info('variable service', variableSrv);
        }
        $scope.text();
      });


    }

    //init
    //toggleSideMenu();


    // var variable = result[0]
    // var option = variable.options[x] //change value
    // angular.element(app1El).injector().get('variableSrv').setOptionAsCurrent(variable, option);
    // angular.element(app1El).injector().get('variableSrv').variableUpdated(variable, true);
  }


  function extEvent(options) {
    var res;
    switch (options.method) {
      case 'toggleSideMenu':
        window[options.method].apply(this, options.params);
        break;
      case 'getDashboardList':
        break;
      case 'setDashboard':
        res = setDashboard(options.params);
        break;
      case 'getTemplatingObjs':
      case 'getVariableObjs':
        if (jQuery('#app1').length == 0) {
          extInit();
        }
        res = getVariableObjs();
        break;
      case 'setTemplatingObjs':
      case 'setVariableObjs':
        if (jQuery('#app1').length == 0) {
          extInit();
        }
        res = setVariableObjs(options.params)
        break;
      case 'getTimeOption':
        break;
      case 'setTimeOption':
        break;
      case 'setOption':
        res = setOption(options.params);
        break;
      case 'defaultOption':
        res = defaultOption(options.params);
        break;
      case 'defaultOption4':
        res = defaultOption4(options.params);
        break;
      case 'toggleMenuLogo4':
        res = toggleMenuLogo4(options.params);
        break;
      case 'variable':
        break;
      case 'time':
        break;
      case 'search':
        break;
      case 'context':
        break;
      default:
        break;
    }
    //parent.formSubmit(res);
    return res;

    //angular.bootstrap(app1El, ['app1', 'grafana.core']);
    // var element = document.querySelector('#app1');
    // var variableSrvRes = angular.element(app1El).injector().get('variableSrv')
    // var contextSrvRes = angular.element(app1El).injector().get('contextSrv')
    // var searchSrvRes = angular.element(app1El).injector().get('searchSrv')
    // var timeSrvRes = angular.element(app1El).injector().get('timeSrv')
    // console.info('service',variableSrvRes,contextSrvRes,searchSrvRes,timeSrvRes);
  }

  function toggleSideMenu(show) {
    if (typeof show != 'undefined') {
      localStorage.setItem('wp-db-sideMenu', show);
    } else {
      show = localStorage.getItem('wp-db-sideMenu') == 'false' ? false : true;
    }

    if (show) {
      $('.sidemenu').show();
    } else {
      $('.sidemenu').hide();
    }
    return true;
  }

  function toggleDashList(show) {
    if (typeof show != 'undefined') {
      localStorage.setItem('wp-db-dashList', show);
    } else {
      show = localStorage.getItem('wp-db-dashList') == 'false' ? false : true;
    }

    if (show) {
      $('.navbar-page-btn').show();
    } else {
      $('.navbar-page-btn').hide();
    }
    return true;
  }

  //{sideMenu:boolean,dashList:boolean}
  function setOption(option) {
    if (typeof option["sideMenu"] == "boolean") {
      toggleSideMenu(option["sideMenu"]);
    }
    if (typeof option["dashList"] == "boolean") {
      toggleDashList(option["dashList"]);
    }
  };

  function defaultOption(gfVer) {
    // append CSS
    var css = '.navbar-page-btn { display: none; } .sidemenu { display: none; }';
    css += '#navbarTitle { font-size: 19px; line-height: 55px; display: block !important; }';

    var head = document.head || document.getElementsByTagName('head')[0],
      style = document.createElement('style');
    style.id = "wp_frame_style";

    style.type = 'text/css';
    if (style.styleSheet) {
      style.styleSheet.cssText = css;
    } else {
      style.appendChild(document.createTextNode(css));
    }
    head.appendChild(style);

    // 

  }

  function defaultOption4(gfVer) {
    var css = '.navbar-inner .navbar-brand-btn { display: none; } ';
    css += '.navbar-inner .navbar-section-wrapper { display: none; } ';
    css += '.navbar-inner .dashnav-action-icons { display: none; } ';
    var head = document.head || document.getElementsByTagName('head')[0],
      style = document.createElement('style');
    style.id = "wp_frame_style";

    style.type = 'text/css';
    if (style.styleSheet) {
      style.styleSheet.cssText = css;
    } else {
      style.appendChild(document.createTextNode(css));
    }
    head.appendChild(style);
  }

  function toggleMenuLogo4(show) {
    if (typeof show != 'undefined') {
      localStorage.setItem('wp-db-4-logo', show);
    } else {
      show = localStorage.getItem('wp-db-4-logo') == 'false' ? false : true;
    }

    if (show) {
      $('.navbar-inner .navbar-brand-btn').show();
      $('.navbar-inner .navbar-section-wrapper').show();
      $('.navbar-inner .dashnav-action-icons').show();
    } else {
      $('.navbar-inner .navbar-brand-btn').hide();
      $('.navbar-inner .navbar-section-wrapper').hide();
      $('.navbar-inner .dashnav-action-icons').hide();
    }
    return true;
  }

  function setDashboard(URI) {
    var a_Href;
    if (!document.getElementById("a_Href")) {
      a_Href = document.createElement("a");
      a_Href.id = "a_Href";
      document.body.appendChild(a_Href);
    } else {
      a_Href = document.getElementById("a_Href");
    }
    a_Href.href = URI;
    a_Href.click();
    a_Href = null;
    delete a_Href;
    document.getElementById("a_Href").parentElement.removeChild(document.getElementById("a_Href"));
    return true;
  };

  function getVariableObjs() {
    var element = document.querySelector('#app1');
    var variableSrvRes = angular.element(element).injector().get('variableSrv');
    var result = [];
    for (var i = 0; i < variableSrvRes.variables.length; i++) {
      result.push({
        name: variableSrvRes.variables[i]["name"],
        options: variableSrvRes.variables[i]["options"]
      });
    }
    console.info('getVariableObjs', result);
    return result;
  }

  //{name,option}
  function setVariableObjs(options) {
    var element = document.querySelector('#app1');
    var variableSrvRes = angular.element(element).injector().get('variableSrv');
    var changeVar = null;
    var newOption = options.option;

    //map variable name
    for (var i = 0; i < variableSrvRes.variables.length; i++) {
      if (variableSrvRes.variables[i]["name"] == options.name) {
        changeVar = variableSrvRes.variables[i];
        break;
      }
    }
    angular.element(element).injector().get('variableSrv').setOptionAsCurrent(changeVar, newOption);
    angular.element(element).injector().get('variableSrv').variableUpdated(changeVar, true); // 設定 true強迫更新頁面
    return true;
  }



  window.addEventListener('message', function(event) {
    if (event.data) {
      var obj = event.data;
      var id = obj.id;
      var res = extEvent(obj);
      console.info('res___', res);

      //can change "post message" as a callback parameter to extEvent
      window.parent.postMessage({
        id: id,
        result: res
      }, "*");
    }

    console.info('data', event.data);
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

})(window);