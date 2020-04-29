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
    public class ErrorMaintenanceController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {//
            string myurl = url + "api/v1/configuration/andon/error_log?status=3";
            return Json(CommonHelper<error_log>.Get(myurl, HttpContext));
        }

        public IActionResult Update([FromBody]error_log log)
        {
            string myurl = url + "api/v1/configuration/andon/error_log" ;
            var postData = JsonConvert.SerializeObject(log);
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
        public IActionResult Delete([FromBody]machinelease lease)
        {
            string myurl = url + "api/v1/configuration/andon/error_log?id=" + lease.id.ToString();
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
            string myurl = url + "api/v1/configuration/public/machine";
            return Json(CommonHelper<machine>.Get(myurl, HttpContext));
        }
        public JsonResult Geterror()
        {
            string myurl = url + "api/v1/configuration/andon/error_type";
            return Json(CommonHelper<error_type>.Get(myurl, HttpContext));
        }
        public JsonResult Geterrordetail()
        {
            string myurl = url + "api/v1/configuration/andon/error_type_detail";
            return Json(CommonHelper<error_type_detail>.Get(myurl, HttpContext));
        }

        public JsonResult Getdetail(int typeid)
        {
            var purl = url + "api/v1/configuration/andon/error_type_detail";
            var list = CommonHelper<error_type_detail>.Get(purl, HttpContext); 
            var data = list.Where(p => p.error_type_id == typeid);

            return Json(data);
        }
    }
}