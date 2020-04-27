if(typeof(window.configLang) == 'undefined'){
	window.configLang = {};
}

configLang.zh_tw = {
  title:{
      description: '物聯網系統'
  },
  menu:{
    list:[
    
        {
            title: '公共配置', id: 'm2i_User0', list: [
                { title: '區域列表', type: 'dashboard', dashbaord: '/Area/Index' },
                { title: '城市列表', type: 'dashboard', dashbaord: '/City/Index' },
                { title: '廠區列表', type: 'dashboard', dashbaord: '/Plant/Index' },
                { title: '製程列表', type: 'dashboard', dashbaord: '/Unit/Index' },
                { title: '部門列表', type: 'dashboard', dashbaord: '/Dept/Index' },
                { title: '綫別列表', type: 'dashboard', dashbaord: '/Line/Index' },
                { title: '站別列表', type: 'dashboard', dashbaord: '/Station/Index' },
                { title: '設備列表', type: 'dashboard', dashbaord: '/Machine/Index' },
                { title: '稼動率公式', type: 'dashboard', dashbaord: '/UtilizationRateFormula/Index' }]
        },
        {
            title: '物料配置', id: 'm2i_User1', list: [
                { title: '物料类别', type: 'dashboard', dashbaord: '/MaterialCategory/Index' },
                { title: '物料清单', type: 'dashboard', dashbaord: '/Materialinfo/Index' }]
        },
        {
            title: '人員及作息配置', id: 'm2i_User2', list: [
                { title: '班別列表', type: 'dashboard', dashbaord: '/Class/Index' },
                { title: '人員列表', type: 'dashboard', dashbaord: '/Person/Index' },
                { title: '人員出勤資訊', type: 'dashboard', dashbaord: '/Attendance/Index' },
                { title: '休息時間(日常)', type: 'dashboard', dashbaord: '/RestTime/Index' },
                { title: '休息時間(排休)', type: 'dashboard', dashbaord: '/RestTimeRow/Index' }]
        },
        {
            title: '異常管理', id: 'm2i_User3', list: [
                { title: '異常類型列表', type: 'dashboard', dashbaord: '/ErrorType/Index' },
                { title: '異常詳細列表', type: 'dashboard', dashbaord: '/ErrorTypeDetails/Index' },
                { title: '異常配置', type: 'dashboard', dashbaord: '/ErrorConfig/Index' },
                { title: '異常信息維護', type: 'dashboard', dashbaord: '/ErrorLog/Index' },
                { title: '異常信息查詢', type: 'dashboard', dashbaord: '/ErrorLog/Report' }]
         },
        {
              title: '機器人異常編碼管理', id: 'm2i_User3', list: [
                  { title: '異常编码設置', type: 'dashboard', dashbaord: '/MachineErrorCode/Index' },
                  { title: '異常記錄查詢', type: 'dashboard', dashbaord: '/MachineErrorCode/Report' }]
        },
        {
            title: '工時配置', id: 'm2i_User3', list: [
                { title: '標準工時列表', type: 'dashboard', dashbaord: '/MachineWorkingTime/Index' }]
        },
        {
            title: '工單管理', id: 'm2i_Lamp3', list: [
               
                { title: '未完成工單信息', type: 'dashboard', dashbaord: '/Proschedule/Index' }
                { title: '歷史工單信息', type: 'dashboard', dashbaord: '/Proschedule/Report' }
            ]
        },
        {
            title: '標簽配置', id: 'm2i_Lamp2', list: [
                { title: '標簽類型列表', type: 'dashboard', dashbaord: '/TagType/Index' },
                { title: '系統標簽列表', type: 'dashboard', dashbaord: '/SystemTag/Index' },
                { title: '標簽列表', type: 'dashboard', dashbaord: '/TagInfo/Index' }]
        },
      {
          title: '用戶管理', id: 'm2i_User', list: [
              { title: '用戶列表', type: 'dashboard', dashbaord: '/User/Index' },
              { title: '變更密碼', type: 'dashboard', dashbaord: '/User/EditPassWord' },
              { title: '用戶註銷', type: 'dashboard', dashbaord: '/User/Logout' }]
      }
    ]
  }
}