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
    public class CapacityConfigController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string geturl = url + "api/v1/configuration/work_order/capacity_config";
            return Json(CommonHelper<capacityconfig>.Get(geturl, HttpContext)); 
        }
     
        public IActionResult Update([FromBody]capacityconfig ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/work_order/capacity_config";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);

            var typeList = CommonHelper<capacityconfig>.Get(myurl1, HttpContext); 
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.date == ec.date);
            if (lists == false)
            {
                string updateurl = url + "api/v1/configuration/work_order/capacity_config";
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
        public IActionResult Add([FromBody]capacityconfig ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/work_order/capacity_config";
            var typeList = CommonHelper<capacityconfig>.Get(myurl1, HttpContext);

            var list = typeList.Any(p => p.date == ec.date);
            if (list == false)//没有重复的
            {
                string addurl = url + "api/v1/configuration/work_order/capacity_config";
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

        public IActionResult Delete([FromBody]capacityconfig ec)
        {
            string deleteurl = url + "api/v1/configuration/work_order/capacity_config?id=" + ec.id.ToString();
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
       
    }
}