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
    public class NotificationController : BaseController
    {


        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/ehs/notice_logic";
            List<notice_logics> list = CommonHelper<notice_logics>.Get(myurl, HttpContext);
            list = list.OrderByDescending(x => x.id).ToList();
            return Json(list);
        }


        public IActionResult Update([FromBody]notice_logic ec)
        {
            string msg = "";
            string myurl = url + "api/v1/configuration/ehs/notice_logic";
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
            return Json(msg);
        }
        public IActionResult Add([FromBody]notice_logic ec)
        {
            string msg = "";
            string myurl = url + "api/v1/configuration/ehs/notice_logic";
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

            return Json(msg);
        }

        public IActionResult Delete([FromBody]notice_logic ec)
        {
            string myurl = url + "api/v1/configuration/ehs/notice_logic?id=" + ec.id.ToString();
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

        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";

            return Json(CommonHelper<notification_group>.Get(myurl, HttpContext));
        }
    }
}