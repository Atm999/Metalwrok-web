using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    public class Wechart_ServerController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public  JsonResult GetData()
        {
            url = url + "api/v1/configuration/public/wechart_server";
            string result = GetUrl(url);
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

        public IActionResult Update([FromBody]wechart_server wechart) 
        {
            url=url + "api/v1/configuration/public/wechart_server";
            var postData = JsonConvert.SerializeObject(wechart);
            string result = PutUrl(url, postData);
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