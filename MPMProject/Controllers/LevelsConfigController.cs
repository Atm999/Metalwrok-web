using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Model;
using System.IdentityModel.Tokens.Jwt;

namespace MPMProject.Controllers
{
    /// <summary>
    /// 公共配置——层级配置
    /// </summary>
    public class LevelsConfigController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";

        public IActionResult Index()
        {
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
                var areaNodeList = joAreaLayer["data"].ToObject<IList<Model.area_node>>();
                //层级
                var datAreaNode = (from p in areaNodeList
                                   where p.upper_id>=0
                                   orderby p.upper_id
                                   select new { p.id,p.name_cn,p.upper_id }).ToList();//若直接将datAreaNode赋值给ViewBag.Layer无法使用获取元素的方法

                for (int i=0;i< datAreaNode.Count;i++)
                {
                    area_layer res = new area_layer();
                    res.id = datAreaNode[i].id;
                    res.name_cn = datAreaNode[i].name_cn;
                    res.upper_id = datAreaNode[i].upper_id;

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
            url = url + "api/v1/configuration/public/area_layer";
            string postData = "{{" +
                "\"name_en\":\"{0}\"," +
                "\"name_cn\":\"{1}\"," +
                "\"name_tw\":\"{2}\"," +
                "\"description\":\"{3}\"," +
                "\"upper_id\":{4}" +
                "}}";
            postData = string.Format(postData, area_Layer.name_en, area_Layer.name_cn, area_Layer.name_tw, area_Layer.description, area_Layer.upper_id);
            string result = PostUrl(url, postData);
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
        #endregion
        #region 群组操作
        //群组查询
        public JsonResult GetGroup()
        {
            url = url + "api/v1/configuration/public/area_node";
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (Convert.ToInt32(jo["code"]) == 200)
            {
                return Json(jo["data"]);
            }
            else
            {
                return Json("Fail");
            }
            
        }
        //群组新增
        public IActionResult AddGroup(area_node area_Node)
        {
            url = url + "api/v1/configuration/public/area_node";
            string postData = "{{" +
                "\"name_en\":\"{0}\"," +
                "\"name_cn\":\"{1}\"," +
                "\"name_tw\":\"{2}\"," +
                "\"description\":\"{3}\"," +
                "\"upper_id\":{4}," +
                "\"area_layer_id\":{5}" +
                "}}";
            //对于群组来说，upper_id和area_layer_id均固定
            postData = string.Format(postData, area_Node.name_en, area_Node.name_cn, area_Node.name_tw, area_Node.description, 0,1);
            string result = PostUrl(url, postData);
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
        //群组修改
        public IActionResult UpdateGroup(area_node area_Node)
        {
            url = url + "api/v1/configuration/public/area_node";
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
            postData = string.Format(postData, area_Node.id, area_Node.name_en, area_Node.name_cn, area_Node.name_tw, area_Node.description,0,1);
            string result = PutUrl(url, postData);
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
        #endregion
        #region 属性设置
        //属性设置查询
        public JsonResult GetSetting(int area_node_id,string type)
        {
            url = url + "api/v1/configuration/public/area_property/"+ type + "";
            area_property area_Property = new area_property();
            string result = GetUrl(url);
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
                    area_property.Day day = new area_property.Day();
                    area_property.Shift shift = new area_property.Shift();
                    area_Property.id = datAreaProperty[0].id;
                    area_Property.name_cn = datAreaProperty[0].name_cn;
                    area_Property.name_en = datAreaProperty[0].name_en;
                    area_Property.name_tw = datAreaProperty[0].name_tw;
                    DateTime dateTime = DateTime.UtcNow;
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
        //属性设置修改/更新
        public IActionResult UpdateSetting()
        {
            string day_start_time = Convert.ToString(HttpContext.Request.Form["day_start_time"]);
            string day_end_time = Convert.ToString(HttpContext.Request.Form["day_end_time"]);
            string night_start_time = Convert.ToString(HttpContext.Request.Form["night_start_time"]);
            string night_end_time = Convert.ToString(HttpContext.Request.Form["night_end_time"]);

            string[] fix_start_time = Convert.ToString(HttpContext.Request.Form["fixStartTime"]).Split(',');
            string[] fix_end_time = Convert.ToString(HttpContext.Request.Form["fixEndTime"]).Split(',');
            string[] unfix_start_time = Convert.ToString(HttpContext.Request.Form["unfixStartTime"]).Split(',');
            string[] unfix_end_time = Convert.ToString(HttpContext.Request.Form["unfixEndTime"]).Split(',');

            for (int i = 0; i < fix_start_time.Length; i++)
            {

            }

            for (int i = 0; i < unfix_start_time.Length; i++)
            {

            }

            url = url + "api/v1/configuration/public/area_node";
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
            postData = string.Format(postData);
            string result = PutUrl(url, postData);
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
        #endregion
        #region 删除操作
        //删除
        public IActionResult Delete(int id,string flag)
        {
            url = url + "api/v1/configuration/public/"+ flag + "?id="+ id + "";
            string result = DeleteUrl(url);
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
        #endregion
    }
}