using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    /// <summary>
    ///环境
    /// </summary>
    public class StandardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            var purl = url + "api/v1/configuration/ehs/env_standard";

            var subList = CommonHelper<env_standard>.Get(purl, HttpContext);

            return Json(subList);
        }
        /// <summary>
        /// 类别
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTagType()
        {
            var purl = url + "api/v1/configuration/public/tag_type";
            string myurl = url + "api/v1/configuration/public/tag_type_sub";

            var typeList = CommonHelper<tag_type>.Get(purl, HttpContext);
            var subList = CommonHelper<tag_type_sub>.Get(myurl, HttpContext);
            var list = typeList.Where(p => p.name_cn == "环境");
            var jo2 = subList.Join(list, p => p.tag_type_id, p => (p as Model.tag_type).id, (p, q) => new { p.id, p.name_cn, p.name_en, p.name_tw, typeName = q.name_cn, p.tag_type_id, p.description }).ToList();
            return Json(jo2);
        }
        /// <summary>
        /// 标签名
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTaginfo()
        {
            var purl = url + "api/v1/configuration/public/tag";
            return Json(CommonHelper<tag_info>.Get(purl, HttpContext));
        }
        public JsonResult Getarea()
        {
            var purl = url + "api/v1/configuration/public/area_node";
            return Json(CommonHelper<area_node>.Get(purl, HttpContext));
        }
        public IActionResult Update([FromBody]standard sub)
        {
            string msg = "";
            string myurl = url + "api/v1/configuration/ehs/env_standard";
            string postData = JsonConvert.SerializeObject(sub);
            string result = PutUrl(myurl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    msg = "Success";
                    break;
                case 400:
                    msg = "fail";
                    break;
            }
            return Json(msg);
        }
        public IActionResult Add([FromBody]standard sub)
        {
            string msg = "";

            string myurl = url + "api/v1/configuration/ehs/env_standard";
            var postData = JsonConvert.SerializeObject(sub);
            string result = PostUrl(myurl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    msg = "Success";
                    break;
                case 400:
                    msg = "fail";
                    break;
            }
            return Json(msg);

        }

        public IActionResult Delete([FromBody]standard sta)
        {
            string msg = "";
            string myurl = url + "api/v1/configuration/ehs/env_standard?id=" + sta.id.ToString();
            string result = DeleteUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    msg = "Success";
                    break;
                case 400:
                    msg = "fail";
                    break;
            }
            return Json(msg);
        }
    }
}