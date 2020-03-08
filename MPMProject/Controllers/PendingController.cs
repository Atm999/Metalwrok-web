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
        {//
            string myurl = url + "api/v1/configuration/andon/error_log/0?status=1";
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

        public IActionResult Update([FromBody]error_log log)
        {
            string id_num = log.name;
            int id = log.id;
            var purl = url + "api/v1/configuration/public/person";
            string result1 = GetUrl(purl);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var personList = jo1["data"].ToObject<IList<Model.Person>>();
            var entity=personList.FirstOrDefault(p => p.id_num == id_num);

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