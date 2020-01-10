if(typeof(window.configLang) == 'undefined'){
	window.configLang = {};
}

configLang.en = {
  title:{
      description: 'IOT system'
  },
  menu:{
    list:[
        {
            title: 'Public configuration', id: 'm2i_User0', list: [
                { title: 'Area List', type: 'dashboard', dashbaord: '/Area/Index' },
                { title: 'City List', type: 'dashboard', dashbaord: '/City/Index' },
                { title: 'Plant List', type: 'dashboard', dashbaord: '/Plant/Index' },
                { title: 'Unit List', type: 'dashboard', dashbaord: '/Unit/Index' },
                { title: 'Dept List', type: 'dashboard', dashbaord: '/Dept/Index' },
                { title: 'Line List', type: 'dashboard', dashbaord: '/Line/Index' },
                { title: 'Station List', type: 'dashboard', dashbaord: '/Station/Index' },
                { title: 'Machine List', type: 'dashboard', dashbaord: '/Machine/Index' },
                { title: 'Utilization Formula List', type: 'dashboard', dashbaord: '/UtilizationRateFormula/Index' }]
        },
        {
            title: 'Material configuration', id: 'm2i_User1', list: [
                { title: 'MaterialCategory List', type: 'dashboard', dashbaord: '/MaterialCategory/Index' },
                { title: 'MaterialInfo List', type: 'dashboard', dashbaord: '/Materialinfo/Index' }]
        },
        {
            title: 'Personnel and work-rest configuration', id: 'm2i_User2', list: [
                { title: 'Class List', type: 'dashboard', dashbaord: '/Class/Index' },
                { title: 'Person List', type: 'dashboard', dashbaord: '/Person/Index' },
                { title: 'Attendance List', type: 'dashboard', dashbaord: '/Attendance/Index' },
                { title: 'RestTime List', type: 'dashboard', dashbaord: '/RestTime/Index' },
                { title: 'Rest Time(Row Rest)', type: 'dashboard', dashbaord: '/RestTimeRow/Index' }]
        },
        {
            title: 'Error configuration', id: 'm2i_User3', list: [
                { title: 'ErrorType List', type: 'dashboard', dashbaord: '/ErrorType/Index' },
                { title: 'ErrorTypeDetails List', type: 'dashboard', dashbaord: '/ErrorTypeDetails/Index' },
                { title: 'ErrorConfig List', type: 'dashboard', dashbaord: '/ErrorConfig/Index' },
                { title: 'ErrorLog List', type: 'dashboard', dashbaord: '/ErrorLog/Index' },
                { title: 'ErrorLog Report', type: 'dashboard', dashbaord: '/ErrorLog/Report' }]
         },
        {
              title: 'Machine Error Code configuration', id: 'm2i_User3', list: [
                  { title: 'Errorcode Config', type: 'dashboard', dashbaord: '/MachineErrorCode/Index' },
                  { title: 'Errorcode Report', type: 'dashboard', dashbaord: '/MachineErrorCode/Report' }]
        },
        {
            title: 'WorkingTime configuration', id: 'm2i_User3', list: [
                { title: 'MachineWorkingTime List', type: 'dashboard', dashbaord: '/MachineWorkingTime/Index' }]
        },
        {
            title: 'Work order management', id: 'm2i_Lamp3', list: [
               
                { title: 'Proschedule List', type: 'dashboard', dashbaord: '/Proschedule/Index' } ]
        },
        {
            title: 'Tag configuration', id: 'm2i_Lamp2', list: [
                { title: 'TagType List', type: 'dashboard', dashbaord: '/TagType/Index' },
                { title: 'SystemTag List', type: 'dashboard', dashbaord: '/SystemTag/Index' },
                { title: 'TagInfo List', type: 'dashboard', dashbaord: '/TagInfo/Index' }]
        },
      {title: 'User Management', id: 'm2i_User', list: [
            { title: 'User List', type: 'dashboard', dashbaord: '/User/Index' },
            { title: 'Edit Password', type: 'dashboard', dashbaord: '/User/EditPassWord' },
            { title: 'Logout', type: 'dashboard', dashbaord: '/User/Logout' }]
      }
    ]
  }
}