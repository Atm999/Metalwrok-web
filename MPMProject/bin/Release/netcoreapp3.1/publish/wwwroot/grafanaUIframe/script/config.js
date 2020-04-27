//url: 'https://wat2.wise-paas.com/?orgId=1',
//autoSize.html
var config = {
  dashboard:{
    // url: 'http://localhost:3000',
    // westUrl: 'http://localhost:3000',
    // eastUrl: 'http://localhost:3000'
    url: 'autoSize.html',
    westUrl: 'autoSize.html',
    eastUrl: 'autoSize.html'
  },
  language:[
    {text:'繁體中文',abbr:'zh_tw'},
    {text:'简体中文',abbr:'zh_cn'},
    {text:'English',abbr:'en'}
  ],
  title:{
    description:'雲端監控與運維平台系統'
  },
  frame:{
    main:{

    },
    west:{
      isGrafana:false
    },
    east:{
      isGrafana:false
    }
  },
  menu:{
    icon:{

    },
    swipeMenu:{
      initClosed: false
      },
     
    list:[
        {title: 'Andon Config', id: 'm2i_EMS', list:[]},
          ]

  }
}

// url: 'https://dashboard-grafana-wise-paas-stage-grafana.wise-paas.com',
// westUrl: 'http://172.16.12.4:3000',
//     eastUrl: 'http://172.16.12.4:3000'
