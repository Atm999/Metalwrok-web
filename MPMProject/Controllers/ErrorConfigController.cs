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
    public class ErrorConfigController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Indexs()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/error_config_detail";
            string result = GetUrl(myurl);
            JObject jo= (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
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
            return Json(jo["data"]);
        }
        public IActionResult Update([FromBody]error_config ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_config";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.error_config>>();
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.machine_id == ec.machine_id && p.tag_type_sub_id == ec.tag_type_sub_id);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/error_config";
                var postData = JsonConvert.SerializeObject(ec);
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
            else {
                msg = "fail";
            }
            return Json(msg);
        }
        public IActionResult Add([FromBody]error_config ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_config";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.error_config>>();

            var list = typeList.Any(p => p.machine_id == ec.machine_id && p.tag_type_sub_id == ec.tag_type_sub_id );
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/error_config";
                var postData = JsonConvert.SerializeObject(ec);
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
            else {
                msg = "fail";
            }
               
            return Json(msg);
        }

        public IActionResult Delete([FromBody]error_config ec)
        {
            string myurl = url + "api/v1/configuration/andon/error_config?id=" + ec.id.ToString();
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

        public JsonResult Getmachine()
        {
            var purl = url + "api/v1/configuration/public/machine";
            var result1 = GetUrl(purl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);

            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
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
            return Json(jo["data"]);
        }

        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]); 
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
            return Json(jo["data"]);
        }

        public JsonResult Gettagsub()
        {
            string myurl = url + "api/v1/configuration/public/tag_type_sub";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var flist = jo["data"].ToObject<IList<tag_type_sub>>();
            var fdata = flist.Where(p => p.tag_type_id == 3);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
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
            return Json(fdata);
        }

        public JsonResult Getperson()
        {
            string myurl = url + "api/v1/configuration/public/person";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
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
            return Json(jo["data"]);
        }
    }
}