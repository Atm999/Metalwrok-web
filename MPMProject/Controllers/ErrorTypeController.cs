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
    public class ErrorTypeController : BaseController
    {
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/error_type";
         
            return Json(CommonHelper<error_type>.Get(myurl, HttpContext));
        }

        public IActionResult Update([FromBody]error_type type)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_type";
            var typeList = CommonHelper<error_type>.Get(myurl1, HttpContext); 
            var list = typeList.Where(p => p.id != type.id);

            var lists = list.Any(p => p.name_cn == type.name_cn || p.name_en == type.name_en || p.name_tw == type.name_tw);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/error_type";
                var postData = JsonConvert.SerializeObject(type);
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
        public IActionResult Add([FromBody]error_type type)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_type";
            var typeList = CommonHelper<error_type>.Get(myurl1, HttpContext); 

            var list = typeList.Any(p => p.name_cn == type.name_cn || p.name_en == type.name_en || p.name_tw == type.name_tw);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/error_type";
                var postData = JsonConvert.SerializeObject(type);
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

        public IActionResult Delete([FromBody]error_type type)
        {
            string myurl = url + "api/v1/configuration/andon/error_type?id=" + type.id.ToString();
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
    }
}