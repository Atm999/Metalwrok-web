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
    public class DeptController : Controller
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public  JsonResult GetData()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "api/v1/configuration/public/dept");
            //GET请求
            request.Method = "GET";
            request.ReadWriteTimeout = 5000;
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            //返回内容
            string result = myStreamReader.ReadToEnd();
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

        public JsonResult Update(int id, string name_en, string name_tw, string name_cn) 
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "api/v1/configuration/public/dept");
            //GET请求
            request.Method = "PUT";
            request.ReadWriteTimeout = 5000;
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            //返回内容
            string result = myStreamReader.ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                   
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