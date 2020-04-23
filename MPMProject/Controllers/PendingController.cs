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
    public class PendingController : BaseController
    {
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            // 1. 选择设备类的，待处理的异常
            //2.已接单的不显示
            //3.超时不显示 从error_config获取  


            //查出异常配置的所有数据
            string myurls = url + "api/v1/configuration/andon/error_config";
            string results = GetUrl(myurls);
            JObject jos = (JObject)JsonConvert.DeserializeObject(results);
            var lists = jos["data"].ToObject<IList<error_config>>();
            
            //  选择设备类的，待处理的异常
            string myurl = url + "api/v1/configuration/andon/error_log/0?status=1";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<error_log>>();
            var date = DateTime.Now;
            var data = list.Where(p => p.substitutes == null).ToList();//已接单不显示
            var  etities=data.Join(lists, p => p.error_config_id, n => ((error_config)n).id, (p, n) =>new {p,n })
                              .Where(p=> Convert.ToDateTime(p.p.start_time).AddMinutes(Convert.ToDouble(p.n.timeout_setting))>=date)
                              .Select(q=>q.p);
           
            return Json(etities);
        }

        public IActionResult Update([FromBody]error_log log)
        {
            string msg = "";
            string resname = log.responsible_name;
            string id_num = log.name;
            int id = log.id;
            var purl = url + "api/v1/configuration/public/person";
            string result1 = GetUrl(purl);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var personList = jo1["data"].ToObject<IList<Model.Person>>();
            var entity=personList.FirstOrDefault(p => p.id_num == id_num);
            if (resname != entity.user_name)
            {
                string myurl = url + "api/v1/configuration/andon/error_log/" + id + "?name=" + entity.user_name;
                string machinePutData = "{{" +
                                   "\"id\":{0}," +
                                   "\"name\":\"{1}\"," +
                                   "}}";
                machinePutData = string.Format(machinePutData, id, entity.user_name);
                string machinePutResult = PutUrl(myurl, machinePutData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(machinePutResult);
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
            else {
                msg = "fail";
            }
            
            return Json(msg);
        }
       
        
        public JsonResult Getmachine()
        {
            string myurl = url + "api/v1/configuration/public/machine";
            string result = GetUrl(myurl);
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