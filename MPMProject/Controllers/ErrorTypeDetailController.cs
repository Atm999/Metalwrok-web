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
    public class ErrorTypeDetailController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            var purl = url + "api/v1/configuration/andon/error_type";
            string myurl = url + "api/v1/configuration/andon/error_type_detail";
            var typeList = CommonHelper<error_type>.Get(purl, HttpContext); 
            var subList = CommonHelper<error_type_detail>.Get(myurl, HttpContext); 
            //var dat = (from p in subList
            //          join q in typeList
            //          on p.tag_type_id equals q.id
            //          select new { p.id, p.name_cn, p.name_en, p.name_tw, typeName = q.name_cn }).ToList();
            var jo2 = subList.Join(typeList, p => p.error_type_id, p => (p as Model.error_type).id, (p, q) => new { p.id, p.name_cn, p.name_en, p.name_tw, typeName = q.name_cn, p.error_type_id, p.description,p.code }).ToList();
            return Json(jo2);
        }

        public JsonResult GetTagType()
        {
            var purl = url + "api/v1/configuration/andon/error_type";
           
            return Json(CommonHelper<error_type>.Get(purl, HttpContext));
        }

        public IActionResult Update([FromBody]error_type_detail sub)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_type_detail";
            var typeList = CommonHelper<error_type_detail>.Get(myurl1, HttpContext); 
            var list = typeList.Where(p => p.id != sub.id);

            var lists = list.Any(p => p.name_cn == sub.name_cn || p.name_en == sub.name_en || p.name_tw == sub.name_tw ||p.code==sub.code);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/error_type_detail";
                var postData = JsonConvert.SerializeObject(sub);
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
        public IActionResult Add([FromBody]error_type_detail sub)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_type_detail";
            var typeList = CommonHelper<error_type_detail>.Get(myurl1, HttpContext);
            var list = typeList.Any(p => p.name_cn == sub.name_cn || p.name_en == sub.name_en || p.name_tw == sub.name_tw||p.code==sub.code);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/error_type_detail";
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
            }
            else {
                msg = "fail";
            }
               
            return Json(msg);
        }

        public IActionResult Delete([FromBody]error_type_detail sub)
        {
            string myurl = url + "api/v1/configuration/andon/error_type_detail?id=" + sub.id.ToString();
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