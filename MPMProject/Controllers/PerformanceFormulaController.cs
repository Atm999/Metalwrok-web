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
    public class PerformanceFormulaController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/lpm/performance_formula";
            var typeList = CommonHelper<leave_statistics>.Get(myurl, HttpContext);
            return Json(typeList); 
        }

        public IActionResult Update(performance_formula ec)
        {
            string myurl = url + "api/v1/configuration/lpm/performance_formula";
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
        public IActionResult Add([FromBody]performance_formula ec)
        {
            string myurl = url + "api/v1/configuration/lpm/performance_formula";
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

        public IActionResult Delete([FromBody]performance_formula ec)
        {
            string myurl = url + "api/v1/configuration/lpm/performance_formula?id=" + ec.id.ToString();
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