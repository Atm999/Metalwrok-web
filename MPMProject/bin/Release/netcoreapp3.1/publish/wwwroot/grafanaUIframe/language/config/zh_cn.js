if(typeof(window.configLang) == 'undefined'){
	window.configLang = {};
}

configLang.zh_cn =
    {
  title:{
      description: '物联网系统'
  },
  menu:{
    list:[
           {
            title: '公共配置', id: 'm2i_User0', list: [
                { title: '区域列表', type: 'dashboard', dashbaord: '/Area/Index' },
                { title: '城市列表', type: 'dashboard', dashbaord: '/City/Index' },
                { title: '厂区列表', type: 'dashboard', dashbaord: '/Plant/Index' },
                { title: '制程列表', type: 'dashboard', dashbaord: '/Unit/Index' },
                { title: '部门列表', type: 'dashboard', dashbaord: '/Dept/Index' },
                { title: '线别列表', type: 'dashboard', dashbaord: '/Line/Index' },
                { title: '站別列表', type: 'dashboard', dashbaord: '/Station/Index' },
                { title: '设备列表', type: 'dashboard', dashbaord: '/Machine/Index' },
                { title: '稼动率公式', type: 'dashboard', dashbaord: '/UtilizationRateFormula/Index' }]
            },
           {
            title: '物料配置', id: 'm2i_User1', list: [
                { title: '物料类别', type: 'dashboard', dashbaord: '/MaterialCategory/Index' },
                { title: '物料清单', type: 'dashboard', dashbaord: '/Materialinfo/Index' }]
           },
           {
            title: '人员及作息配置', id: 'm2i_User2', list: [
                { title: '班别列表', type: 'dashboard', dashbaord: '/Class/Index' },
                { title: '人员列表', type: 'dashboard', dashbaord: '/Person/Index' },
                { title: '人员出勤信息', type: 'dashboard', dashbaord: '/Attendance/Index' },
                { title: '休息时间(日常)', type: 'dashboard', dashbaord: '/RestTime/Index' },
                { title: '休息时间(排休)', type: 'dashboard', dashbaord: '/RestTimeRow/Index' }]
           },
           {
            title: '安灯异常管理', id: 'm2i_User3', list: [
                { title: '异常类型列表', type: 'dashboard', dashbaord: '/ErrorType/Index' },
                { title: '异常详细列表', type: 'dashboard', dashbaord: '/ErrorTypeDetails/Index' },
                { title: '异常配置', type: 'dashboard', dashbaord: '/ErrorConfig/Index' },
                { title: '异常信息维护', type: 'dashboard', dashbaord: '/ErrorLog/Index' },
                { title: '异常信息查询', type: 'dashboard', dashbaord: '/ErrorLog/Report' }]
          },
          {
              title: '机器人异常管理', id: 'm2i_User3', list: [
                  { title: '编码配置', type: 'dashboard', dashbaord: '/MachineErrorCode/Index' },
                { title: '异常编码记录', type: 'dashboard', dashbaord: '/MachineErrorCode/Report' }]
          },
          {
            title: '工时配置', id: 'm2i_User3', list: [
                { title: '标准工时列表', type: 'dashboard', dashbaord: '/MachineWorkingTime/Index' }]
          },
           {
            title: '工单管理', id: 'm2i_Lamp3', list: [
              
                { title: '未完成工单信息', type: 'dashboard', dashbaord: '/Proschedule/Index' },
                { title: '历史工单查询', type: 'dashboard', dashbaord: '/Proschedule/Report' }

            ]
        },
           { 
            title: '标签配置', id: 'm2i_Lamp2', list: [
                { title: '标签类型列表', type: 'dashboard', dashbaord: '/TagType/Index' },
                { title: '系统标签列表', type: 'dashboard', dashbaord: '/SystemTag/Index' },
                { title: '标签列表', type: 'dashboard', dashbaord: '/TagInfo/Index' }]
          },
           {
          title: '用户管理', id: 'm2i_User', list: [
               { title: '用户列表', type: 'dashboard', dashbaord: '/User/Index' },
               { title: '变更密码', type: 'dashboard', dashbaord: '/User/EditPassWord' },
               { title: '用户注销', type: 'dashboard', dashbaord: '/User/Logout' }]
        }
    ]
  }
}