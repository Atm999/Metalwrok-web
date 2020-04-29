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
    public class Email_ServerController : BaseController
    {
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public  JsonResult GetData()
        {
            string geturl = url + "api/v1/configuration/public/Email_Server";
            return Json(CommonHelper<email_server>.Get(geturl, HttpContext)); 
        }

        public IActionResult Update([FromBody]email_server email) 
        {
            string updateurl=url + "api/v1/configuration/public/Email_Server";
            var postData = JsonConvert.SerializeObject(email);
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
      
    }
}