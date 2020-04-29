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
    public class StatusSettingController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/oee/status_setting";
            return Json(CommonHelper<status_setting>.Get(myurl, HttpContext));
        }
        public IActionResult Update([FromBody]status_setting set)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/oee/status_setting";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.status_setting>>();
            var list = typeList.Where(p => p.id != set.id);

            var lists = list.Any(p => p.status_name == set.status_name || p.value == set.value);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/oee/status_setting";
                var postData = JsonConvert.SerializeObject(set);
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
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }
        public IActionResult Add([FromBody]status_setting set)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/oee/status_setting";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.status_setting>>();

            var list = typeList.Any(p => p.status_name == set.status_name||p.value==set.value);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/oee/status_setting";
                var postData = JsonConvert.SerializeObject(set);
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
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }

        public IActionResult Delete([FromBody]status_setting set)
        {
            string myurl = url + "api/v1/configuration/oee/status_setting?id=" + set.id.ToString();
            string result = DeleteUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json("Success");
                    break;
                case 400:
                    break;
                case 410:
                    break;
                case 411:
                    break;
                default:
                    break;
            }
            return Json("Success");
        }

        public IActionResult Updateformula([FromBody]utilization_rate_formula set)
        {
            string myurl = url + "api/v1/configuration/oee/utilization_formula";
            var postData = JsonConvert.SerializeObject(set);
            string result = PutUrl(myurl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json("Success");
                    break;
                case 400:
                    break;
                case 410:
                    break;
                case 411:
                    break;
                default:
                    break;
            }
            return Json("Success");
        }

        public JsonResult Getdateformula() 
        {
            string myurl = url + "api/v1/configuration/oee/utilization_formula";
            return Json(CommonHelper<utilization_rate_formula>.Get(myurl, HttpContext));
        }
    }
}