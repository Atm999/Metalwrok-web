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
    public class Capacity_alertController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string geturl = url + "api/v1/configuration/andon/capacity_alert_detail";
            string result = GetUrl(geturl);
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
        public IActionResult Update([FromBody]capacity_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/capacity_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.capacity_alert>>();
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.date == ec.date);
            if (lists == false)
            {
                string updateurl = url + "api/v1/configuration/andon/capacity_alert";
                var postData = JsonConvert.SerializeObject(ec);
                string result = PutUrl(updateurl, postData);
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
        public IActionResult Add([FromBody]capacity_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/capacity_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.capacity_alert>>();

            var list = typeList.Any(p => p.date == ec.date);
            if (list == false)//没有重复的
            {
                string addurl = url + "api/v1/configuration/andon/capacity_alert";
                var postData = JsonConvert.SerializeObject(ec);
                string result = PostUrl(addurl, postData);
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

        public IActionResult Delete([FromBody]capacity_alert ec)
        {
            string deleteurl = url + "api/v1/configuration/andon/capacity_alert?id=" + ec.id.ToString();
            string result = DeleteUrl(deleteurl);
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
            string groupurl = url + "api/v1/configuration/andon/notification_group";
            string result = GetUrl(groupurl);
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