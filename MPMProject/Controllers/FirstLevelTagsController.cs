using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
   
    /// <summary>
    /// 公共配置——一级标签
    /// </summary>
    public class FirstLevelTagsController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            url = url + "api/v1/configuration/public/tag_type";
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
    }
}