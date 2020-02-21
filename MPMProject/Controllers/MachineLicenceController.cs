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
        private IHostingEnvironment hostingEnv;
        public MachineLicenceController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }
        public IActionResult Index()
        {
            string data1=DESCode.Read(hostingEnv.WebRootPath+ $@"\Files\Files\licence.txt");
            string data = "{\"space_id\":\"0e8d66cd-55e6-4853-9835-2b24e6a95022\",\"machineNum\":100}";
            string key = "WisePaaS";
            string iv = "AKTCKM30";
            ViewBag.Encrypt = DESCode.DESEncrypt(data, key, iv);

            string licence = DESCode.DESDecrypt(data1, key, iv);
            JObject jo = (JObject)JsonConvert.DeserializeObject(licence);
            ViewBag.licenceNum = jo["machineNum"].ToString();
            return View();
        }

        public IActionResult Decode()
        {
            return View();
        }
    }
}