using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Model;

namespace MPMProject.Controllers
{
    /// <summary>
    /// 公共配置——层级配置
    /// </summary>
    public class LevelsConfigController : BaseController
    {

        public IActionResult Index()
        {
            //每次访问这个页面都会将账密写进数据库  by CC.X
            if(GlobalVar.user_name != "" && GlobalVar.password != "")
            {
                string myurl = url + "api/v1/configuration/public/user";
                wise_paas_user wise_Paas_User = new wise_paas_user();
                wise_Paas_User.name = GlobalVar.user_name;
                wise_Paas_User.password = GlobalVar.password;
                wise_Paas_User.role = "";
                var postData = JsonConvert.SerializeObject(wise_Paas_User);
                string result = PostUrl(myurl, postData);
            }



            //用于machine tag点绑定后tab页聚焦
            if (!string.IsNullOrEmpty(Request.Query["tab"]))
            {
                ViewBag.tab = 1;
            }

            //获取层级信息(area_layer)
            List<area_layer> area_Layers = new List<area_layer>();
            var areaLayerUrl = url + "api/v1/configuration/public/area_layer";
            var resAreaLayer = GetUrl(areaLayerUrl);
            JObject joAreaLayer = (JObject)JsonConvert.DeserializeObject(resAreaLayer);

            if (Convert.ToInt32(joAreaLayer["code"]) == 200)
            {
                var areaLayerList = joAreaLayer["data"].ToObject<IList<Model.area_layer>>();
                //层级
                var datAreaLayer = (from p in areaLayerList
                                   orderby p.id
                                   select new { p.id,p.name_cn }).ToList();//若直接将datAreaNode赋值给ViewBag.Layer无法使用获取元素的方法

                for (int i=0;i< datAreaLayer.Count;i++)
                {
                    area_layer res = new area_layer();
                    res.id = datAreaLayer[i].id;
                    res.name_cn = datAreaLayer[i].name_cn;

                    area_Layers.Add(res);
                }
                ViewBag.Layer = area_Layers;
                ViewBag.LevelFlag = area_Layers[area_Layers.Count-1].id;//用于在添加层级时给upper_id赋值
            }
            else
            {
                ViewBag.layer = area_Layers;
                ViewBag.LevelFlag = -1;//当服务器挂掉时添加的层级会失效
            }

            return View();
        }
        #region 层级操作
        //添加层级
        public IActionResult AddLevel(area_layer area_Layer)
        {
            string levelUrl = url + "api/v1/configuration/public/area_layer";
            
            if (area_Layer.id == 0)
            {
                string postData = "{{" +
                "\"name_en\":\"{0}\"," +
                "\"name_cn\":\"{1}\"," +
                "\"name_tw\":\"{2}\"," +
                "\"description\":\"{3}\"" +
                "}}";
                postData = string.Format(postData, area_Layer.name_en, area_Layer.name_cn, area_Layer.name_tw, area_Layer.description);
                string result = PostUrl(levelUrl, postData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                if (Convert.ToInt32(jo["code"]) == 200)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else {
                string postData = "{{" +
                "\"id\":{0}," +
                "\"name_en\":\"{1}\"," +
                "\"name_cn\":\"{2}\"," +
                "\"name_tw\":\"{3}\"," +
                "\"description\":\"{4}\"" +
                "}}";
                postData = string.Format(postData, area_Layer.id, area_Layer.name_en, area_Layer.name_cn, area_Layer.name_tw, area_Layer.description);
                string result = PutUrl(levelUrl, postData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                if (Convert.ToInt32(jo["code"]) == 200)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }

        }
        //获取层级
        public IActionResult GetLevel(string urlPara,int area_layer_id)
        {
            var levelUrl = url + "api/v1/configuration/public/"+ urlPara + "";
            var result1 = GetUrl(levelUrl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);

            if (Convert.ToInt32(jo["code"]) == 200)
            {
                if (urlPara == "area_layer") {
                    var levelList = jo["data"].ToObject<IList<Model.area_layer>>();
                    //层级
                    var datLevel = (from p in levelList
                                    orderby p.id
                                    select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description }).ToList();

                    return Json(datLevel);
                }
                else {
                    var levelList = jo["data"].ToObject<IList<Model.area_node>>();
                    //层级
                    var datLevel = (from p in levelList
                                    where p.area_layer_id== area_layer_id
                                    orderby p.id
                                    select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description }).ToList();

                    return Json(datLevel);
                }
                
            }
            else
            {
                return Json("Fail");
            }
        }
        #endregion
        #region 群组操作
        //群组查询，area_layer_id=1表示默认加载群组数据
        public JsonResult GetGroup(int area_layer_id=1)
        {
            if (!string.IsNullOrEmpty(Request.Query["area_layer_id"]))
            {
                area_layer_id = Convert.ToInt32(Request.Query["area_layer_id"]);
            }

            string groupUrl = url + "api/v1/configuration/public/area_node";
            string result = GetUrl(groupUrl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);

            string utcUrl = url + "api/v1/configuration/public/area_property/time_zone";
            string utcResult = GetUrl(utcUrl);
            JObject utcJo = (JObject)JsonConvert.DeserializeObject(utcResult);

            if (Convert.ToInt32(jo["code"]) == 200 && Convert.ToInt32(utcJo["code"]) == 200)
            {
                //当已有数据（area_layer_id > 0）或选择“-请选择-”（area_layer_id==-1）时
                var areaNodeList = jo["data"].ToObject<IList<Model.area_node>>();
                var areaNodeFatherList = jo["data"].ToObject<IList<Model.area_node>>();
                var utcList = utcJo["data"].ToObject<IList<Model.area_property>>();

                var datAreaNode1 = (from p in areaNodeList
                                    where p.area_layer_id==area_layer_id && p.upper_id == 0
                                    join q in utcList
                                    on p.id equals q.area_node_id
                                    orderby p.name_cn
                                    select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description,p.upper_id,p.area_layer_id,q.format, area_node_name = "-" }).ToList();
                var datAreaNode2 = (from p in areaNodeList
                                   join q in areaNodeFatherList
                                   on p.upper_id equals q.id
                                   where p.area_layer_id == area_layer_id
                                   orderby p.name_cn
                                   select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description, p.upper_id, p.area_layer_id,format="-", area_node_name = q.name_cn }).ToList();
                var datAreaNode = datAreaNode1.Union(datAreaNode2);
                return Json(datAreaNode);

                //if (area_layer_id > 0 || area_layer_id == -1)
                //{
                //    var areaNodeList = jo["data"].ToObject<IList<Model.area_node>>();

                //    var datAreaNode = (from p in areaNodeList
                //                       where p.area_layer_id == area_layer_id
                //                       orderby p.name_cn
                //                       select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description }).ToList();

                //    return Json(datAreaNode);
                //}
                //else
                //{
                //    return Json(jo["data"]);
                //}
            }
            else
            {
                return Json("Fail");
            }
            
        }
        //群组新增
        public IActionResult AddGroup(area_node area_Node)
        {
            string utc = Convert.ToString(HttpContext.Request.Form["utc"]).Replace("UTC", "");

            string groupUrl = url + "api/v1/configuration/public/area_node";
            string postData = "{{" +
                "\"name_en\":\"{0}\"," +
                "\"name_cn\":\"{1}\"," +
                "\"name_tw\":\"{2}\"," +
                "\"description\":\"{3}\"," +
                "\"upper_id\":{4}," +
                "\"area_layer_id\":{5}" +
                "}}";
            //对于群组来说，upper_id和area_layer_id均固定
            postData = string.Format(postData, area_Node.name_en, area_Node.name_cn, area_Node.name_tw, area_Node?.description,area_Node.upper_id, area_Node.area_layer_id);
            string result = PostUrl(groupUrl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);

            //若不是群组则直接跳过
            if (Convert.ToInt32(jo["code"]) == 200 && area_Node.upper_id != 0)
            {
                return Json("Success");
            }

            //获取area_node
            string areaNodeUrl = url + "api/v1/configuration/public/area_node";
            string areaNodeResult = GetUrl(areaNodeUrl);
            JObject areaNodeJo = (JObject)JsonConvert.DeserializeObject(areaNodeResult);

            var areaNodeList = areaNodeJo["data"].ToObject<IList<Model.area_node>>();

            var res = (from p in areaNodeList
                       where p.name_en == area_Node.name_en
                       select new { p.id }).ToList();


            string utcUrl = url + "api/v1/configuration/public/area_property";
            string utcPostData = "{{" +
                "\"name_en\":\"{0}\"," +
                "\"name_cn\":\"{1}\"," +
                "\"name_tw\":\"{2}\"," +
                "\"description\":\"{3}\"," +
                "\"format\":\"{4}\"," +
                "\"area_node_id\":{5}" +
                "}}";
            //对于群组来说，upper_id和area_node_id均固定
            utcPostData = string.Format(utcPostData, "time_zone", "时区", "时区", "时区", utc, res[0].id);
            string utcResult = PostUrl(utcUrl, utcPostData);
            JObject joUtc = (JObject)JsonConvert.DeserializeObject(utcResult);


            if (Convert.ToInt32(jo["code"]) == 200 && Convert.ToInt32(joUtc["code"]) == 200)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }
        //群组修改
        public IActionResult UpdateGroup(area_node area_Node)
        {
            string utc = Convert.ToString(HttpContext.Request.Form["utc"]).Replace("UTC","");

            string groupUrl = url + "api/v1/configuration/public/area_node";
            string postData = "{{" +
                "\"id\":{0}," +
                "\"name_en\":\"{1}\"," +
                "\"name_cn\":\"{2}\"," +
                "\"name_tw\":\"{3}\"," +
                "\"description\":\"{4}\"," +
                "\"upper_id\":{5}," +
                "\"area_layer_id\":{6}" +
                "}}";
            //对于群组来说，upper_id和area_node_id均固定
            postData = string.Format(postData, area_Node.id, area_Node.name_en, area_Node.name_cn, area_Node.name_tw, area_Node.description, area_Node.upper_id, area_Node.area_layer_id);
            string result = PutUrl(groupUrl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);

            //若不是群组则直接跳过
            if (Convert.ToInt32(jo["code"]) == 200 && area_Node.upper_id != 0)
            {
                return Json("Success");
            }

            //获取所有时区
            string allUTCUrl = url + "api/v1/configuration/public/area_property/time_zone";
            string AllUTCResult = GetUrl(allUTCUrl);
            JObject allUTCJo = (JObject)JsonConvert.DeserializeObject(AllUTCResult);           

            if (Convert.ToInt32(jo["code"]) == 200 && Convert.ToInt32(allUTCJo["code"]) == 200)
            {
                var areaPropertyList = allUTCJo["data"].ToObject<IList<Model.area_property>>();

                var res = (from p in areaPropertyList
                           where p.area_node_id == area_Node.id
                           select new { p.id}).ToList();

                string utcUrl = url + "api/v1/configuration/public/area_property";
                string utcPostData = "{{" +
                    "\"id\":{0}," +
                    "\"name_en\":\"{1}\"," +
                    "\"name_cn\":\"{2}\"," +
                    "\"name_tw\":\"{3}\"," +
                    "\"description\":\"{4}\"," +
                    "\"format\":\"{5}\"," +
                    "\"area_node_id\":{6}" +
                    "}}";
                //对于群组来说，upper_id和area_node_id均固定
                utcPostData = string.Format(utcPostData, res[0].id, "time_zone", "时区", "时区", "时区", utc, area_Node.id);
                string utcResult = PutUrl(utcUrl, utcPostData);
                JObject joUtc = (JObject)JsonConvert.DeserializeObject(utcResult);
                if (Convert.ToInt32(joUtc["code"]) == 200) {
                    return Json("Success");
                }
                else {
                    return Json("Fail");
                }
            }
            else
            {
                return Json("Fail");
            }
        }
        #endregion
        #region 属性设置
        //属性设置查询
        public JsonResult GetSetting(int area_node_id,string type)
        {
            string settingUrl = url + "api/v1/configuration/public/area_property/"+ type + "";
            area_property area_Property = new area_property();
            string result = GetUrl(settingUrl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            
            if (Convert.ToInt32(jo["code"]) == 200)
            {
                var areaPropertyList = jo["data"].ToObject<IList<Model.area_property>>();
                //层级
                var datAreaProperty = (from p in areaPropertyList
                                   where p.area_node_id == area_node_id
                                   select new {p.id, p.name_cn, p.name_en,p.name_tw,p.format, p.area_node_id }).ToList();
                if (datAreaProperty.Count > 0)
                {
                    area_Property.id = datAreaProperty[0].id;
                    area_Property.name_cn = datAreaProperty[0].name_cn;
                    area_Property.name_en = datAreaProperty[0].name_en;
                    area_Property.name_tw = datAreaProperty[0].name_tw;
                    //解析format
                    area_Property.format = datAreaProperty[0].format;
                    //JObject format = (JObject)JsonConvert.DeserializeObject(datAreaProperty[0].format);
                    //用day和rest作为唯一标识来区别排班和排休，name可能会发生变化
                    if (type== "shift")
                    {
                        area_Property.shift= JsonConvert.DeserializeObject<area_property.Shift>(datAreaProperty[0].format);                      
                    }
                    else
                    {
                        area_Property.fixBreak= JsonConvert.DeserializeObject<area_property.FixBreak>(datAreaProperty[0].format);
                    }

                    area_Property.area_node_id = datAreaProperty[0].area_node_id;
                } 

                return Json(area_Property);
            }
            else
            {
                return Json("Fail");
            }

        }
        //属性设置新增与修改
        public IActionResult UpdateSetting()
        {
            //获取前台传过来的数据
            int area_node_id = Convert.ToInt32(HttpContext.Request.Form["area_node_id"]);
            string day_start_time = Convert.ToString(HttpContext.Request.Form["day_start_time"]);
            string day_end_time = Convert.ToString(HttpContext.Request.Form["day_end_time"]);
            string night_start_time = Convert.ToString(HttpContext.Request.Form["night_start_time"]);
            string night_end_time = Convert.ToString(HttpContext.Request.Form["night_end_time"]);

            string[] fix_start_time = Convert.ToString(HttpContext.Request.Form["fixStartTime"]).Split(',');
            string[] fix_end_time = Convert.ToString(HttpContext.Request.Form["fixEndTime"]).Split(',');
            string[] unfix_start_time = Convert.ToString(HttpContext.Request.Form["unfixStartTime"]).Split(',');
            string[] unfix_end_time = Convert.ToString(HttpContext.Request.Form["unfixEndTime"]).Split(',');



            //获取shift,fixed_break,unfixed_break
            //所有shift
            string urlShift = url + "api/v1/configuration/public/area_property/shift";
            area_property areaPropertyShift = new area_property();
            string resultShift = GetUrl(urlShift);
            JObject joShift = (JObject)JsonConvert.DeserializeObject(resultShift);

            //所有fixed_break
            string urlFixedBreak = url + "api/v1/configuration/public/area_property/fixed_break";
            area_property areaPropertyFixedBreak = new area_property();
            string resultFixedBreak = GetUrl(urlFixedBreak);
            JObject joFixedBreak = (JObject)JsonConvert.DeserializeObject(resultFixedBreak);

            //所有unfixed_break
            string urlUnFixedBreak = url + "api/v1/configuration/public/area_property/unfixed_break";
            area_property areaPropertyUnFixedBreak = new area_property();
            string resultUnFixedBreak = GetUrl(urlUnFixedBreak);
            JObject joUnFixedBreak = (JObject)JsonConvert.DeserializeObject(resultUnFixedBreak);

            if (Convert.ToInt32(joShift["code"]) == 200 && Convert.ToInt32(joFixedBreak["code"]) == 200 && Convert.ToInt32(joUnFixedBreak["code"]) == 200)
            {
                bool flag = true;//用以标识新增/修改动作是否成功
                //Shift新增与修改
                var shiftList = joShift["data"].ToObject<IList<Model.area_property>>();
                var shiftProperty = (from p in shiftList
                                       where p.area_node_id == area_node_id
                                       select new { p.id, p.name_cn, p.name_en, p.name_tw, p.format, p.area_node_id }).ToList();
                if (shiftProperty.Count > 0)
                {//修改
                    areaPropertyShift.id = shiftProperty[0].id;

                    area_property.Shift shift = new area_property.Shift();
                    area_property.Day day = new area_property.Day();
                    area_property.Day night = new area_property.Day();
                    day.start = day_start_time;
                    day.end = day_end_time;
                    night.start = night_start_time;
                    night.end = night_end_time;
                    shift.day = day;
                    shift.night = night;

                    areaPropertyShift.shift = shift;
                    areaPropertyShift.format = JsonConvert.SerializeObject(JsonConvert.SerializeObject(areaPropertyShift.shift));
                    string shiftPutUrl = url + "api/v1/configuration/public/area_property";
                    string shiftPutData = "{{" +
                        "\"id\":{0}," +
                        "\"format\":{1}," +
                        "\"area_node_id\":{2}"+
                        "}}";
                    //对于群组来说，upper_id和area_node_id均固定
                    shiftPutData = string.Format(shiftPutData, areaPropertyShift.id, areaPropertyShift.format,area_node_id);
                    string shiftPutResult = PutUrl(shiftPutUrl, shiftPutData);
                    JObject joShiftPut = (JObject)JsonConvert.DeserializeObject(shiftPutResult);
                    if (Convert.ToInt32(joShiftPut["code"]) != 200)
                    {
                        flag = false;
                    }
                }
                else
                {//新增
                    if (day_start_time!="" || day_end_time!="" || night_start_time != "" || night_end_time != "") {
                        areaPropertyShift.name_cn = "排班";
                        areaPropertyShift.name_en = "shift";
                        areaPropertyShift.name_tw = "排班";
                        areaPropertyShift.description = "排班";
                        areaPropertyShift.area_node_id = area_node_id;

                        area_property.Shift shift = new area_property.Shift();
                        area_property.Day day = new area_property.Day();
                        area_property.Day night = new area_property.Day();
                        day.start = day_start_time;
                        day.end = day_end_time;
                        night.start = night_start_time;
                        night.end = night_end_time;
                        shift.day = day;
                        shift.night = night;

                        areaPropertyShift.shift = shift;

                        areaPropertyShift.format = JsonConvert.SerializeObject(JsonConvert.SerializeObject(areaPropertyShift.shift));
                        string shiftPostUrl = url + "api/v1/configuration/public/area_property";
                        string shiftPostData = "{{" +
                            "\"name_en\":\"{0}\"," +
                            "\"name_cn\":\"{1}\"," +
                            "\"name_tw\":\"{2}\"," +
                            "\"description\":\"{3}\"," +
                            "\"format\":{4}," +
                            "\"area_node_id\":{5}" +
                            "}}";
                        //对于群组来说，upper_id和area_node_id均固定
                        shiftPostData = string.Format(shiftPostData, areaPropertyShift.name_en, areaPropertyShift.name_cn, areaPropertyShift.name_tw,
                            areaPropertyShift.description, areaPropertyShift.format, areaPropertyShift.area_node_id);
                        string shiftPostResult = PostUrl(shiftPostUrl, shiftPostData);
                        JObject joShiftPost = (JObject)JsonConvert.DeserializeObject(shiftPostResult);
                        if (Convert.ToInt32(joShiftPost["code"]) != 200)
                        {
                            flag = false;
                        }
                    }

                }

                //fixed_break新增与修改
                List<area_property.Day> fixedRest = new List<area_property.Day>();     
                var fixedBreakList = joFixedBreak["data"].ToObject<IList<Model.area_property>>();
                var fixedBreakProperty = (from p in fixedBreakList
                                          where p.area_node_id == area_node_id
                                     select new { p.id, p.name_cn, p.name_en, p.name_tw, p.format, p.area_node_id }).ToList();
                if (fixedBreakProperty.Count > 0)
                {//修改
                    areaPropertyFixedBreak.id = fixedBreakProperty[0].id;
                    for (int i = 0; i < fix_start_time.Length; i++)
                    {
                        area_property.Day fixedDay = new area_property.Day();
                        fixedDay.start = fix_start_time[i];
                        fixedDay.end = fix_end_time[i];
                        fixedRest.Add(fixedDay);
                    }
                    area_property.FixBreak fixBreak = new area_property.FixBreak();
                    fixBreak.rest = fixedRest;
                    areaPropertyFixedBreak.fixBreak = fixBreak;
                    areaPropertyFixedBreak.format = JsonConvert.SerializeObject(JsonConvert.SerializeObject(areaPropertyFixedBreak.fixBreak));
                    string fixedBreakPutUrl = url + "api/v1/configuration/public/area_property";
                    string fixedBreakPutData = "{{" +
                        "\"id\":{0}," +
                        "\"format\":{1}," +
                        "\"area_node_id\":{2}" +
                        "}}";
                    //对于群组来说，upper_id和area_node_id均固定
                    fixedBreakPutData = string.Format(fixedBreakPutData, areaPropertyFixedBreak.id, areaPropertyFixedBreak.format, area_node_id);
                    string fixedBreakPutResult = PutUrl(fixedBreakPutUrl, fixedBreakPutData);
                    JObject joFixedBreakPut = (JObject)JsonConvert.DeserializeObject(fixedBreakPutResult);
                    if (Convert.ToInt32(joFixedBreakPut["code"]) != 200)
                    {
                        flag = false;
                    }
                }
                else
                {//新增
                    if (fix_start_time[0]!="" || fix_end_time[0] != "") {
                        areaPropertyFixedBreak.name_cn = "固定排休";
                        areaPropertyFixedBreak.name_en = "fixed_break";
                        areaPropertyFixedBreak.name_tw = "固定排休";
                        areaPropertyFixedBreak.description = "固定排休";
                        areaPropertyFixedBreak.area_node_id = area_node_id;

                        for (int i = 0; i < fix_start_time.Length; i++)
                        {
                            area_property.Day fixedDay = new area_property.Day();
                            fixedDay.start = fix_start_time[i];
                            fixedDay.end = fix_end_time[i];
                            fixedRest.Add(fixedDay);
                        }
                        area_property.FixBreak fixBreak = new area_property.FixBreak();
                        fixBreak.rest = fixedRest;
                        areaPropertyFixedBreak.fixBreak = fixBreak;
                        areaPropertyFixedBreak.format = JsonConvert.SerializeObject(JsonConvert.SerializeObject(areaPropertyFixedBreak.fixBreak));

                        string fixedBreakPostUrl = url + "api/v1/configuration/public/area_property";
                        string fixedBreakPostData = "{{" +
                            "\"name_en\":\"{0}\"," +
                            "\"name_cn\":\"{1}\"," +
                            "\"name_tw\":\"{2}\"," +
                            "\"description\":\"{3}\"," +
                            "\"format\":{4}," +
                            "\"area_node_id\":{5}" +
                            "}}";
                        //对于群组来说，upper_id和area_node_id均固定
                        fixedBreakPostData = string.Format(fixedBreakPostData, areaPropertyFixedBreak.name_en, areaPropertyFixedBreak.name_cn, areaPropertyFixedBreak.name_tw,
                            areaPropertyFixedBreak.description, areaPropertyFixedBreak.format, areaPropertyFixedBreak.area_node_id);
                        string fixedBreakPostResult = PostUrl(fixedBreakPostUrl, fixedBreakPostData);
                        JObject joFixedBreakPost = (JObject)JsonConvert.DeserializeObject(fixedBreakPostResult);
                        if (Convert.ToInt32(joFixedBreakPost["code"]) != 200)
                        {
                            flag = false;
                        }
                    }
                    
                }

                //unfixed_break新增与修改
                List<area_property.Day> unFixedRest = new List<area_property.Day>();
                var unFixedBreakList = joUnFixedBreak["data"].ToObject<IList<Model.area_property>>();
                var unFixedBreakProperty = (from p in unFixedBreakList
                                            where p.area_node_id == area_node_id
                                          select new { p.id, p.name_cn, p.name_en, p.name_tw, p.format, p.area_node_id }).ToList();
                if (unFixedBreakProperty.Count > 0)
                {//修改
                    areaPropertyUnFixedBreak.id = unFixedBreakProperty[0].id;
                    for (int i = 0; i < unfix_start_time.Length; i++)
                    {
                        area_property.Day unFixedDay = new area_property.Day();
                        unFixedDay.start = unfix_start_time[i];
                        unFixedDay.end = unfix_end_time[i];
                        unFixedRest.Add(unFixedDay);
                    }
                    area_property.FixBreak unFixBreak = new area_property.FixBreak();
                    unFixBreak.rest = unFixedRest;
                    areaPropertyUnFixedBreak.fixBreak = unFixBreak;
                    areaPropertyUnFixedBreak.format = JsonConvert.SerializeObject(JsonConvert.SerializeObject(areaPropertyUnFixedBreak.fixBreak));
                    string unFixedBreakPutUrl = url + "api/v1/configuration/public/area_property";
                    string unFixedBreakPutData = "{{" +
                        "\"id\":{0}," +
                        "\"format\":{1}," +
                        "\"area_node_id\":{2}" +
                        "}}";
                    //对于群组来说，upper_id和area_node_id均固定
                    unFixedBreakPutData = string.Format(unFixedBreakPutData, areaPropertyUnFixedBreak.id, areaPropertyUnFixedBreak.format, area_node_id);
                    string unFixedBreakPutResult = PutUrl(unFixedBreakPutUrl, unFixedBreakPutData);
                    JObject joUnFixedBreakPut = (JObject)JsonConvert.DeserializeObject(unFixedBreakPutResult);
                    if (Convert.ToInt32(joUnFixedBreakPut["code"]) != 200)
                    {
                        flag = false;
                    }
                }
                else
                {//新增
                    if (unfix_start_time[0] != "" || unfix_end_time[0] != "") {
                        areaPropertyUnFixedBreak.name_cn = "非固定排休";
                        areaPropertyUnFixedBreak.name_en = "unfixed_break";
                        areaPropertyUnFixedBreak.name_tw = "非固定排休";
                        areaPropertyUnFixedBreak.description = "非固定排休";
                        areaPropertyUnFixedBreak.area_node_id = area_node_id;

                        for (int i = 0; i < unfix_start_time.Length; i++)
                        {
                            area_property.Day unFixedDay = new area_property.Day();
                            unFixedDay.start = unfix_start_time[i];
                            unFixedDay.end = unfix_end_time[i];
                            unFixedRest.Add(unFixedDay);
                        }
                        area_property.FixBreak unFixBreak = new area_property.FixBreak();
                        unFixBreak.rest = unFixedRest;
                        areaPropertyUnFixedBreak.fixBreak = unFixBreak;
                        areaPropertyUnFixedBreak.format = JsonConvert.SerializeObject(JsonConvert.SerializeObject(areaPropertyUnFixedBreak.fixBreak));

                        string unFixedBreakPostUrl = url + "api/v1/configuration/public/area_property";
                        string unFixedBreakPostData = "{{" +
                            "\"name_en\":\"{0}\"," +
                            "\"name_cn\":\"{1}\"," +
                            "\"name_tw\":\"{2}\"," +
                            "\"description\":\"{3}\"," +
                            "\"format\":{4}," +
                            "\"area_node_id\":{5}" +
                            "}}";
                        //对于群组来说，upper_id和area_node_id均固定
                        unFixedBreakPostData = string.Format(unFixedBreakPostData, areaPropertyUnFixedBreak.name_en, areaPropertyUnFixedBreak.name_cn, areaPropertyUnFixedBreak.name_tw,
                            areaPropertyUnFixedBreak.description, areaPropertyUnFixedBreak.format, areaPropertyUnFixedBreak.area_node_id);
                        string unFixedBreakPostResult = PostUrl(unFixedBreakPostUrl, unFixedBreakPostData);
                        JObject joUnFixedBreakPost = (JObject)JsonConvert.DeserializeObject(unFixedBreakPostResult);
                        if (Convert.ToInt32(joUnFixedBreakPost["code"]) != 200)
                        {
                            flag = false;
                        }
                    }
                    
                }

                if (flag)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
                
            }
            else
            {
                return Json("Fail");
            }
           
        }
        #endregion

        //各节点下设备查询
        public JsonResult GetMachineNodeList()
        {
            int area_node_id=Convert.ToInt32(Request.Query["area_node_id"]);
            string groupUrl = url + "api/v1/configuration/public/machine";
            List<machine> machines = new List<machine>();
            string result = GetUrl(groupUrl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);

            if (Convert.ToInt32(jo["code"]) == 200)
            {
                var machineList = jo["data"].ToObject<IList<Model.machine>>();
                //层级
                var datMachine = (from p in machineList
                                  where p.area_node_id == area_node_id
                                       select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description, p.area_node_id }).ToList();
                if (datMachine.Count > 0)
                {
                    for (int i=0;i<datMachine.Count;i++) {
                        machine machineRes = new machine();
                        machineRes.id = datMachine[i].id;
                        machineRes.name_cn = datMachine[i].name_cn;
                        machineRes.name_en = datMachine[i].name_en;
                        machineRes.name_tw = datMachine[i].name_tw;
                        machineRes.description = datMachine[i].description;
                        machineRes.area_node_id = datMachine[i].area_node_id;
                        machines.Add(machineRes);
                    }  
                }

                return Json(machines);
            }
            else
            {
                return Json("Fail");
            }

        }

        //各节点下设备绑定与解绑
        public IActionResult UpdateMachineNode(int id,int area_node_id)
        {
            string machinePutUrl = url + "api/v1/configuration/public/machine";
            //设备解绑
            if (area_node_id == 0)
            {
                string machinePutData = "{{" +
                                "\"id\":{0}," +
                                "\"area_node_id\":{1}" +
                                "}}";
                machinePutData = string.Format(machinePutData, id, area_node_id);
                string machinePutResult = PutUrl(machinePutUrl, machinePutData);
                JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                if (Convert.ToInt32(joMachinePut["code"]) == 200)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {//设备绑定
                string[] Machine = Convert.ToString(HttpContext.Request.Form["Machine"]).Split(',');
                if (Machine.Length > 0 && Machine[0]!="")
                {
                    bool flag = true;
                    for (int i=0;i< Machine.Length; i++) {
                        string machinePutData = "{{" +
                                "\"id\":{0}," +
                                "\"area_node_id\":{1}" +
                                "}}";
                        machinePutData = string.Format(machinePutData, Convert.ToInt32(Machine[i]), area_node_id);
                        string machinePutResult = PutUrl(machinePutUrl, machinePutData);
                        JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                        if (Convert.ToInt32(joMachinePut["code"]) != 200)
                        {
                            flag=false;
                        }
                    }
                    if (flag)
                    {
                        return Json("Success");
                    }
                    else
                    {
                        return Json("Fail");
                    }
                }
                else  
                {
                    return Json("Success");
                }
                
            }
     
        }
        //查询所有未绑定的设备
        public JsonResult GetUnBindMachine()
        {
            List<machine> machines = new List<machine>();
            var UnBindUrl = url + "api/v1/configuration/public/machine";
            var result1 = GetUrl(UnBindUrl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);

            if (Convert.ToInt32(jo["code"]) == 200)
            {
                var machineList = jo["data"].ToObject<IList<Model.machine>>();
                //层级
                var datMachine = (from p in machineList
                                  where p.area_node_id == 0
                                  orderby p.name_cn
                                  select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description, p.area_node_id }).ToList();
                if (datMachine.Count > 0)
                {
                    for (int i = 0; i < datMachine.Count; i++)
                    {
                        machine machineRes = new machine();
                        machineRes.id = datMachine[i].id;
                        machineRes.name_cn = datMachine[i].name_cn;
                        machineRes.name_en = datMachine[i].name_en;
                        machineRes.name_tw = datMachine[i].name_tw;
                        machineRes.description = datMachine[i].description;
                        machineRes.area_node_id = datMachine[i].area_node_id;
                        machines.Add(machineRes);
                    }
                }
                return Json(machines);
            }
            else
            {
                return Json("Fail");
            }
            
        }
        //获取所有设备查询
        public JsonResult GetMachineListForSearch()
        {
            string machineUrl = url + "api/v1/configuration/public/machine";
            string machineResult = GetUrl(machineUrl);
            JObject joMachine = (JObject)JsonConvert.DeserializeObject(machineResult);


            if (Convert.ToInt32(joMachine["code"]) == 200)
            {
                var machineList = joMachine["data"].ToObject<IList<Model.machine>>();

                return Json(machineList);
            }
            else
            {
                return Json("Fail");
            }

        }
        //获取所有设备查询
        public JsonResult GetMachineList()
        {
            string machineUrl = url + "api/v1/configuration/public/machine";
            string machineResult = GetUrl(machineUrl);
            JObject joMachine = (JObject)JsonConvert.DeserializeObject(machineResult);

            string areaNodeUrl = url + "api/v1/configuration/public/area_node";
            string areaNodeResult = GetUrl(areaNodeUrl);
            JObject joAreaNode = (JObject)JsonConvert.DeserializeObject(areaNodeResult);

            string tagTypeSubUrl = url + "api/v1/configuration/public/tag_type_sub";
            string tagTypeSubResult = GetUrl(tagTypeSubUrl);
            JObject joTagTypeSub = (JObject)JsonConvert.DeserializeObject(tagTypeSubResult);

            string tagInfoUrl = url + "api/v1/configuration/public/tag";
            string tagInfoResult = GetUrl(tagInfoUrl);
            JObject joTagInfo = (JObject)JsonConvert.DeserializeObject(tagInfoResult);

            if (Convert.ToInt32(joMachine["code"]) == 200 && Convert.ToInt32(joAreaNode["code"]) == 200 
                && Convert.ToInt32(joTagTypeSub["code"]) == 200 && Convert.ToInt32(joTagInfo["code"]) == 200)
            {
                var machineList = joMachine["data"].ToObject<IList<Model.machine>>();
                var areaNodeList = joAreaNode["data"].ToObject<IList<Model.area_node>>();
                var tagTypeSubList = joTagTypeSub["data"].ToObject<IList<Model.tag_type_sub>>();
                var tagInfoList = joTagInfo["data"].ToObject<IList<Model.tag_info>>();

                
                var datMachine = (from p in machineList
                                  join q in areaNodeList
                                  on p.area_node_id equals q.id
                                  select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description, p.area_node_id,area_node_name=q.name_cn,q.area_layer_id}).ToList();

                var datUnBindMachine = (from p in machineList
                                  where p.area_node_id==0
                                  select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description, p.area_node_id, area_node_name="-", area_layer_id =-1}).ToList();


                var tagInfo = (from p in tagInfoList
                           join q in tagTypeSubList
                           on p.tag_type_sub_id equals q.id
                           select new { p.machine_id, q.name_cn }).ToList();
                var machineInfo = datMachine.Union(datUnBindMachine).ToList();//该list只能读不可写


                //存储machine_id和mame_cn(标签名)
                List<tag_info> tag_Infos = new List<tag_info>();
               
                //存储machine_id
                for (int i=0; i < machineInfo.Count; i++) {
                    tag_info tag = new tag_info();
                    tag.machine_id = machineInfo[i].id;
                    tag_Infos.Add(tag);
                }
                //存储machine_id下的所有tag名称
                for (int i=0;i< tag_Infos.Count;i++) {
                    for (int j = 0; j < tagInfo.Count; j++)
                    {
                        if (tag_Infos[i].machine_id == tagInfo[j].machine_id) {
                            tag_Infos[i].name += tagInfo[j].name_cn + ";";
                        }
                    }
                }


                
                var result = (from a in machineInfo
                              join b in tag_Infos
                              on a.id equals b.machine_id into temp
                              from res in temp.DefaultIfEmpty()
                              orderby a.area_node_name, a.name_cn
                              select new { a.id,a.name_cn,a.name_en,a.name_tw,a.description,a.area_layer_id,a.area_node_id,a.area_node_name,
                                  tag = temp.Select(T => T.name).FirstOrDefault()
                              }).ToList();

                return Json(result);
            }
            else
            {
                return Json("Fail");
            }

        }
        //设备新增与修改
        public IActionResult UpdateMachine(int id,int area_node_id,string name_cn, string name_tw, string name_en, string description) {
            string machineUrl = url + "api/v1/configuration/public/machine";
            //新增
            if (id == 0)
            {
                if (area_node_id == -1) {
                    area_node_id = 0;
                }
                string machinePostData = "{{" +
                                "\"id\":{0}," +
                                "\"area_node_id\":{1}," +
                                "\"name_cn\":\"{2}\"," +
                                "\"name_tw\":\"{3}\"," +
                                "\"name_en\":\"{4}\"," +
                                "\"description\":\"{5}\"" +
                                "}}";
                machinePostData = string.Format(machinePostData, id, area_node_id, name_cn, name_tw, name_en, description);
                string machinePostResult = PostUrl(machineUrl, machinePostData);
                JObject joMachinePost = (JObject)JsonConvert.DeserializeObject(machinePostResult);
                if (Convert.ToInt32(joMachinePost["code"]) == 200)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {//修改
                if (area_node_id == -1)
                {
                    area_node_id = 0;
                }
                string machinePutData = "{{" +
                                "\"id\":{0}," +
                                "\"area_node_id\":{1}," +
                                "\"name_cn\":\"{2}\"," +
                                "\"name_tw\":\"{3}\"," +
                                "\"name_en\":\"{4}\"," +
                                "\"description\":\"{5}\"" +
                                "}}";
                machinePutData = string.Format(machinePutData, id, area_node_id, name_cn, name_tw, name_en, description);
                string machinePutResult = PutUrl(machineUrl, machinePutData);
                JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                if (Convert.ToInt32(joMachinePut["code"]) == 200)
                {
                    return Json("Success");
                }

                else
                {
                    return Json("Fail");
                }

            }
        }

        #region 层级中数据删除
        //层级中数据删除
        public IActionResult Delete(int id,string flag)
        {
            bool actionFlag = true;
            //当层级删除时，则要把其下的节点和绑定的属性设备也同时删除
            if (flag == "area_layer")
            {
                //获取层级
                string areaNodeSearchUrl = url + "api/v1/configuration/public/area_node";
                string areaNodeSearchResult = GetUrl(areaNodeSearchUrl);
                JObject areaNodeSearchjo = (JObject)JsonConvert.DeserializeObject(areaNodeSearchResult);
                if (Convert.ToInt32(areaNodeSearchjo["code"]) == 200)
                {
                    var areaNodeList = areaNodeSearchjo["data"].ToObject<IList<Model.area_node>>();
                    //存储area_layer_id为id的id值
                    List<int> nodeIdList = new List<int>();
                    for (int a = 0; a < areaNodeList.Count; a++) {
                        if (id== areaNodeList[a].area_layer_id) {
                            nodeIdList.Add(areaNodeList[a].id);
                        }
                    }
                    //存储所有和id相关的id
                    List<int> idList = new List<int>();
                    
                    for (int b=0;b< nodeIdList.Count;b++) {
                        int idFlag = nodeIdList[b];//id标识,判断id和upper_id是否相等，若相等则将upper_id 赋值给idFlag
                        idList.Add(idFlag);
                        for (int i = 0; i < areaNodeList.Count; i++)
                        {
                            if (idFlag == areaNodeList[i].upper_id)
                            {
                                idFlag = areaNodeList[i].id;
                                idList.Add(idFlag);
                            }
                        }
                    }          

                    //删除绑定的所有属性
                    List<string> typeList = new List<string> { "shift", "unfixed_break", "fixed_break", "time_zone" };
                    for (int n = 0; n < typeList.Count; n++)
                    {
                        string areaPropertySearchUrl = url + "api/v1/configuration/public/area_property/" + typeList[n] + "";
                        string areaPropertySearchResult = GetUrl(areaPropertySearchUrl);
                        JObject areaPropertySearchjo = (JObject)JsonConvert.DeserializeObject(areaPropertySearchResult);
                        if (Convert.ToInt32(areaPropertySearchjo["code"]) == 200)
                        {
                            var areaPropertyList = areaPropertySearchjo["data"].ToObject<IList<Model.area_property>>();
                            //idList存储area_node_id
                            for (int i = 0; i < idList.Count; i++)
                            {
                                var datAreaNode = (from p in areaPropertyList
                                                   where p.area_node_id == idList[i]
                                                   select new { p.id }).ToList();
                                //datAreaNode存储 id
                                for (int j = 0; j < datAreaNode.Count; j++)
                                {
                                    string areaPropertyDeleteUrl = url + "api/v1/configuration/public/area_property?id=" + datAreaNode[j].id + "";
                                    string areaPropertyDeleteResult = DeleteUrl(areaPropertyDeleteUrl);
                                    JObject areaPropertyDeletejo = (JObject)JsonConvert.DeserializeObject(areaPropertyDeleteResult);
                                    if (Convert.ToInt32(areaPropertyDeletejo["code"]) != 200)
                                    {
                                        actionFlag = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            actionFlag = false;
                        }
                    }


                    //所有关联的设备解绑
                    string machineSearchUrl = url + "api/v1/configuration/public/machine";
                    string machineSearchResult = GetUrl(machineSearchUrl);
                    JObject machineSearchjo = (JObject)JsonConvert.DeserializeObject(machineSearchResult);
                    if (Convert.ToInt32(machineSearchjo["code"]) == 200)
                    {
                        var machineSearchjoList = machineSearchjo["data"].ToObject<IList<Model.machine>>();
                        //idList存储area_node_id
                        for (int i = 0; i < idList.Count; i++)
                        {
                            var datMachine = (from p in machineSearchjoList
                                              where p.area_node_id == idList[i]
                                              select new { p.id }).ToList();
                            //datAreaNode存储 id
                            for (int j = 0; j < datMachine.Count; j++)
                            {
                                string machinePutUrl = url + "api/v1/configuration/public/machine";
                                string machinePutData = "{{" +
                                    "\"id\":{0}," +
                                    "\"area_node_id\":{1}" +
                                    "}}";
                                //对于群组来说，upper_id和area_node_id均固定
                                machinePutData = string.Format(machinePutData, datMachine[j].id, 0);
                                string machinePutResult = PutUrl(machinePutUrl, machinePutData);
                                JObject machinePutjo = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                                if (Convert.ToInt32(machinePutjo["code"]) != 200)
                                {
                                    actionFlag = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        actionFlag = false;
                    }

                    //删除节点内容
                    for (int i = 0; i < idList.Count; i++)
                    {
                        string areaNodeDeteteUrl = url + "api/v1/configuration/public/area_node?id=" + idList[i] + "";
                        string areaNodeDeteteResult = DeleteUrl(areaNodeDeteteUrl);
                        JObject areaNodeDetetejo = (JObject)JsonConvert.DeserializeObject(areaNodeDeteteResult);
                        if (Convert.ToInt32(areaNodeDetetejo["code"]) != 200)
                        {
                            actionFlag = false;
                        }
                    }
                }
                else
                {
                    actionFlag = false;
                }
                //找到层级相关的所有节点


                //删除节点下的属性

                //所有节点关联的设备解绑

                //删除层级
                string areaLayerDeleteUrl = url + "api/v1/configuration/public/area_layer?id=" + id + "";
                string areaLayerDeleteResult = DeleteUrl(areaLayerDeleteUrl);
                JObject areaLayerDeletejo = (JObject)JsonConvert.DeserializeObject(areaLayerDeleteResult);
                if (Convert.ToInt32(areaLayerDeletejo["code"]) != 200)
                {
                    actionFlag=false;
                }

                if (actionFlag) {
                    return Json("Success");
                }
                else {
                    return Json("Fail");
                }
            }//当层级删除时，则要把其下的节点和绑定的属性设备也同时删除
            else if (flag == "area_node")
            {
                
                string areaNodeGetUrl = url + "api/v1/configuration/public/area_node";
                string areaNodeGetResult = GetUrl(areaNodeGetUrl);
                JObject areaNodeGetjo = (JObject)JsonConvert.DeserializeObject(areaNodeGetResult);
                if (Convert.ToInt32(areaNodeGetjo["code"]) == 200)
                {
                    var areaNodeList = areaNodeGetjo["data"].ToObject<IList<Model.area_node>>();
                    //存储所有和id相关的id
                    List<int> idList = new List<int>();
                    int idFlag = id;//id标识,判断id和upper_id是否相等，若相等则将upper_id 赋值给idFlag
                    idList.Add(idFlag);
                    for (int i = 0; i < areaNodeList.Count; i++)
                    {
                        if (idFlag == areaNodeList[i].upper_id)
                        {
                            idFlag = areaNodeList[i].id;
                            idList.Add(idFlag);
                        }
                    }

                    //删除绑定的所有属性
                    List<string> typeList = new List<string> { "shift", "unfixed_break", "fixed_break", "time_zone" };
                    for (int n=0;n< typeList.Count;n++) {
                        string areaPropertySearchUrl = url + "api/v1/configuration/public/area_property/"+ typeList[n]+ "";
                        string areaPropertySearchResult = GetUrl(areaPropertySearchUrl);
                        JObject areaPropertySearchjo = (JObject)JsonConvert.DeserializeObject(areaPropertySearchResult);
                        if (Convert.ToInt32(areaPropertySearchjo["code"]) == 200)
                        {
                            var areaPropertyList = areaPropertySearchjo["data"].ToObject<IList<Model.area_property>>();
                            //idList存储area_node_id
                            for (int i = 0; i < idList.Count; i++)
                            {
                                var datAreaNode = (from p in areaPropertyList
                                                   where p.area_node_id == idList[i]
                                                   select new { p.id }).ToList();
                                //datAreaNode存储 id
                                for (int j = 0; j < datAreaNode.Count; j++)
                                {
                                    string areaPropertyDeleteUrl = url + "api/v1/configuration/public/area_property?id=" + datAreaNode[j].id + "";
                                    string areaPropertyDeleteResult = DeleteUrl(areaPropertyDeleteUrl);
                                    JObject areaPropertyDeletejo = (JObject)JsonConvert.DeserializeObject(areaPropertyDeleteResult);
                                    if (Convert.ToInt32(areaPropertyDeletejo["code"]) != 200)
                                    {
                                        actionFlag = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            actionFlag = false;
                        }
                    }
                   

                    //删除绑定的所有设备
                    string machineSearchUrl = url + "api/v1/configuration/public/machine";
                    string machineSearchResult = GetUrl(machineSearchUrl);
                    JObject machineSearchjo = (JObject)JsonConvert.DeserializeObject(machineSearchResult);
                    if (Convert.ToInt32(machineSearchjo["code"]) == 200)
                    {
                        var machineSearchjoList = machineSearchjo["data"].ToObject<IList<Model.machine>>();
                        //idList存储area_node_id
                        for (int i = 0; i < idList.Count; i++)
                        {
                            var datMachine = (from p in machineSearchjoList
                                               where p.area_node_id == idList[i]
                                               select new { p.id }).ToList();
                            //datAreaNode存储 id
                            for (int j = 0; j < datMachine.Count; j++)
                            {
                                string machinePutUrl = url + "api/v1/configuration/public/machine";
                                string machinePutData = "{{" +
                                    "\"id\":{0}," +
                                    "\"area_node_id\":{1}" +
                                    "}}";
                                //对于群组来说，upper_id和area_node_id均固定
                                machinePutData = string.Format(machinePutData, datMachine[j].id, 0);
                                string machinePutResult = PutUrl(machinePutUrl, machinePutData);
                                JObject machinePutjo = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                                if (Convert.ToInt32(machinePutjo["code"]) != 200)
                                {
                                    actionFlag = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        actionFlag = false;
                    }

                    //删除节点内容
                    for (int i=0;i<idList.Count;i++)
                    {
                        string areaNodeDeteteUrl = url + "api/v1/configuration/public/area_node?id="+ idList[i]+ "";
                        string areaNodeDeteteResult = DeleteUrl(areaNodeDeteteUrl);
                        JObject areaNodeDetetejo = (JObject)JsonConvert.DeserializeObject(areaNodeDeteteResult);
                        if (Convert.ToInt32(areaNodeDetetejo["code"]) != 200)
                        {
                            actionFlag = false;
                        }
                    }   
                }
                else {
                    actionFlag = false;
                }
                
            }
            else {
                string publicUrl = url + "api/v1/configuration/public/" + flag + "?id=" + id + "";
                string result = DeleteUrl(publicUrl);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                if (Convert.ToInt32(jo["code"]) != 200)
                {
                    actionFlag = false;
                }

            }

            if (actionFlag) {
                return Json("Success");
            }
            else {
                return Json("Fail");
            }

            
        }
        #endregion
    }
}