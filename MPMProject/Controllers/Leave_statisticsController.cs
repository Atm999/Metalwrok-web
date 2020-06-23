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
    public class Leave_statisticsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/andon_logic_detail";
            var  ne = CommonHelper<andon_logicgroup>.Get(myurl, HttpContext);
            return Json(ne);
        }

        public IActionResult Update([FromBody]andon_logic ec)
        {
            string myurl = url + "api/v1/configuration/andon/andon_logic";
            var postData = JsonConvert.SerializeObject(ec);
            string result = PutUrl(myurl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    return Json("Success");
                case 400:
                    return Json("fail");

            }
            return Json("fail");
        }
        public IActionResult Add([FromBody]andon_logic ec)
        {
            string myurl = url + "api/v1/configuration/andon/andon_logic";
            var postData = JsonConvert.SerializeObject(ec);
            string result = PostUrl(myurl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    return Json("Success");
                case 400:
                    return Json("fail");
            }
            return Json("fail");
        }

        public IActionResult Delete([FromBody]andon_logic ec)
        {
            string myurl = url + "api/v1/configuration/andon/andon_logic?id=" + ec.id.ToString();
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