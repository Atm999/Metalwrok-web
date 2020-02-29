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
    public class QualityCallController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取下拉选虚拟线的数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMachine()
        {
            url = url + "api/v1/configuration/public/machine";
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

        /// <summary>
        /// 根据machinename查找error_log数据，显示画面
        /// </summary>
        /// <returns></returns>
        public JsonResult GetIndex() {
           string machinename = "S3";
            url = url + "api/v1/configuration/andon/error_log/1?status=1";
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<error_log>>();
            var data = list.FirstOrDefault(p => p.machine_name == machinename);
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
            return Json(data);
        }
    }
}