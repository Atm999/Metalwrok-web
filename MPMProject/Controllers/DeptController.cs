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
    public class DeptController : BaseController
    {
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public  JsonResult GetData()
        {
            string  geturl = url + "api/v1/configuration/public/dept";
            string result = GetUrl(geturl);
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

        public IActionResult Update([FromBody]dept dept) 
        {
            string updateurl=url + "api/v1/configuration/public/dept";
            string postData = "{{\"id\":{0},\"name_en\":\"{1}\",\"name_cn\":\"{2}\",\"name_tw\":\"{3}\",\"description\":\"{4}\"}}";
            postData = string.Format(postData, dept.id, dept.name_en, dept.name_cn, dept.name_tw, dept.description);
            string result = PutUrl(updateurl, postData);
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
        public IActionResult Add([FromBody]dept dept)
        {
            string addurl = url + "api/v1/configuration/public/dept";
            string postData = "{{\"id\":{0},\"name_en\":\"{1}\",\"name_cn\":\"{2}\",\"name_tw\":\"{3}\",\"description\":\"{4}\"}}";
            postData = string.Format(postData, dept.id, dept.name_en, dept.name_cn, dept.name_tw, dept.description);
            string result = PostUrl(addurl, postData);
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

        public IActionResult Delete([FromBody]dept dept)
        {
            string deleteurl = url + "api/v1/configuration/public/dept?id="+dept.id.ToString();
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