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
    public class VirtualLineController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            url = url + "api/v1/configuration/work_order/virtual_line";
            string result = GetUrl(url);
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

        public IActionResult Update([FromBody]virtual_line line)
        {
            url = url + "api/v1/configuration/work_order/virtual_line";
            var postData = JsonConvert.SerializeObject(line);
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
        public IActionResult Add([FromBody]virtual_line line)
        {
            url = url + "api/v1/configuration/work_order/virtual_line";
            var postData = JsonConvert.SerializeObject(line);
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

        public IActionResult Delete([FromBody]virtual_line line)
        {
            url = url + "api/v1/configuration/work_order/virtual_line?id=" + line.id.ToString();
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

        public JsonResult Getmachine()
        {
            var purl = url + "api/v1/configuration/public/machine";
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


        //虚拟线下设备查询
        public JsonResult GetMachineNodeList()
        {
            int area_node_id = Convert.ToInt32(Request.Query["area_node_id"]);
            url = url + "api/v1/configuration/public/machine";
            List<machine> machines = new List<machine>();
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);

            if (Convert.ToInt32(jo["code"]) == 200)
            {
                var machineList = jo["data"].ToObject<IList<Model.machine>>();
                //层级
                var datMachine = (from p in machineList
                                  where p.area_node_id == area_node_id
                                  select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description, p.area_node_id }).ToList();
                if (datMachine.Count > 0)
                {
                    for (int i = 0; i < datMachine.Count; i++)
                    {
                        machine machineRes = new machine();
                        machineRes.id = datMachine[i].id;
                        machineRes.name_cn = datMachine[i].name_cn;
                        machineRes.name_en = datMachine[i].name_en;
                        machineRes.name_tw = datMachine[i].name_tw;
                        machineRes.description = datMachine[i].description;
                        machineRes.area_node_id = datMachine[i].area_node_id;
                        machines.Add(machineRes);
                    }
                }

                return Json(machines);
            }
            else
            {
                return Json("Fail");
            }

        }
    }
}