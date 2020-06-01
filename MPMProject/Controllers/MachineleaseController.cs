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
    public class MachineleaseController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/oee/machine_lease_detail";
            return Json(CommonHelper<machineleases>.Get(myurl, HttpContext));
        }

        public JsonResult Getmachine()
        {
            string myurl = url + "api/v1/configuration/public/machine";
            return Json(CommonHelper<machine>.Get(myurl, HttpContext));
        }

        public IActionResult Update([FromBody]machinelease lease)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/oee/machine_lease";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.machinelease>>();
            var list = typeList.Where(p => p.id != lease.id);

            var lists = list.Any(p => p.machine_id == lease.machine_id );
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/oee/machine_lease";
                var postData = JsonConvert.SerializeObject(lease);
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
        public IActionResult Add([FromBody]machinelease lease)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/oee/machine_lease";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.machinelease>>();

            var list = typeList.Any(p => p.machine_id == lease.machine_id );

            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/oee/machine_lease";
                var time = DateTime.Now.AddHours(GlobalVar.time_zone);
                lease.start_time = time;
                var postData = JsonConvert.SerializeObject(lease);
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
                return Json("fail");
            }
            return Json(msg);
        }


            public IActionResult Delete([FromBody]machinelease lease)
        {
            string myurl = url + "api/v1/configuration/oee/machine_lease?id=" + lease.id.ToString();
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
    }
}