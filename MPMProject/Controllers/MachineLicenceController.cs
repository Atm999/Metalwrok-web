using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.FileEncode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    public class MachineLicenceController : BaseController
    {
        private IWebHostEnvironment hostingEnv;
        public MachineLicenceController(IWebHostEnvironment env)
        {
            this.hostingEnv = env;
        }
        public IActionResult Index()
        {
            var licenceUrl = url + "api/v1/configuration/public/licence";
            var resLicence = GetUrl(licenceUrl);
            JObject joLicence = (JObject)JsonConvert.DeserializeObject(resLicence);
            if (Convert.ToInt32(joLicence["code"]) == 200)
            {
                ViewBag.used_number = joLicence["data"][0]["used_number"];
                ViewBag.authorized_number = joLicence["data"][0]["authorized_number"];
            }
            else {
                ViewBag.used_number = "请求失败";
                ViewBag.authorized_number = "请求失败";
            }

            return View();
        }

    }
}