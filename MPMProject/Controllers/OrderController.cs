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
        public static int Count = 0;

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/work_order/wo_config";
            string result = GetUrl(myurl);
            JObject jo= (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<wo_config>>();
            var data = list.Where(p => p.status != 3);
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
       

        public IActionResult Update([FromBody]wo_config wo)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/work_order/wo_config";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.wo_config>>();
            var list = typeList.Where(p => p.id != wo.id);

            var lists = list.Any(p => p.work_order == wo.work_order);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/work_order/wo_config";
                var postData = JsonConvert.SerializeObject(wo);
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
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
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
            string msg = "";
            if (list.Count() == Count)
            {
                work.create_time = DateTime.UtcNow;
                string myurl1 = url + "api/v1/configuration/work_order/wo_config";
                string result1 = GetUrl(myurl1);
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
                var typeList = jo1["data"].ToObject<IList<Model.wo_config>>();

                var lists = typeList.Any(p => p.work_order == work.work_order);
                if (lists == false)//没有重复的
                {
                    string myurl = url + "api/v1/configuration/work_order/wo_config";
                    var postData = JsonConvert.SerializeObject(work);
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
                }
                else
                {
                    msg = "fail";
                }
               
            }
            return Json(msg);
        }

        public IActionResult Delete([FromBody]wo_config wo)
        {
            string myurl = url + "api/v1/configuration/work_order/wo_config?id=" + wo.id.ToString();
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