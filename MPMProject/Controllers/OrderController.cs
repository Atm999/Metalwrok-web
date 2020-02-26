using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Model.virtual_line;

namespace MPMProject.Controllers
{
    public class OrderController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        public static int Count = 0;

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
             url = url + "api/v1/configuration/work_order/wo_config";
            string result = GetUrl(url);
            JObject jo= (JObject)JsonConvert.DeserializeObject(result);
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
       

        public IActionResult Update([FromBody]wo_config wo)
        {
            url = url + "api/v1/configuration/work_order/wo_config";
            var postData = JsonConvert.SerializeObject(wo);
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
        /// <summary>
        /// 确保数据类型一致
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public IActionResult Add([FromBody]wo_config work)
        {
            var standard_time = work.standard_time;
            var list = standard_time.Split(";");
            if (list.Count() == Count)
            {
                work.create_time = DateTime.UtcNow;
                url = url + "api/v1/configuration/work_order/wo_config";
                var postData = JsonConvert.SerializeObject(work);
                string result = PostUrl(url, postData);
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
            else {
                return Json("Fail");
            }
        }

        public IActionResult Delete([FromBody]wo_config wo)
        {
            url = url + "api/v1/configuration/work_order/wo_config?id=" + wo.id.ToString();
            string result = DeleteUrl(url);
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

        public JsonResult Getline()
        {
            var purl = url + "api/v1/configuration/work_order/virtual_line";
            var result1 = GetUrl(purl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);

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

        public JsonResult GetmachineCount(int lineid)
        {
            var purl = url + "api/v1/configuration/work_order/virtual_line";
            var result1 = GetUrl(purl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);
            var list = jo["data"].ToObject<IList<virtual_lineMachine>>();
            var data = list.FirstOrDefault(p => p.id == lineid).Machines;
            Count = data.Count();
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
            return Json(data.Count());
        }

    }
}