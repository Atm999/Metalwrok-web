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
        public JsonResult GetmachineList(int group_id)
        {
            url = url + "api/v1/configuration/work_order/virtual_line";
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<virtual_lineMachine>>();
            var data = list.FirstOrDefault(p => p.id == group_id).Machines;
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

        public IActionResult AddMachine(int id, int virtualLine_id)
        {//"https://api-mpm.wise-paas.cn/api/v1/configuration/work_order/virtual_line/444?machine_id=444"
            
          //设备绑定
                string[] Machine = Convert.ToString(HttpContext.Request.Form["Machine"]).Split(',');
                if (Machine.Length > 0 && Machine[0] != "")
                {
                    bool flag = true;
                    for (int i = 0; i < Machine.Length; i++)
                    {
                        string machinePutUrl = url + "api/v1/configuration/work_order/virtual_line/" + virtualLine_id + "?machine_id=" + Convert.ToInt32(Machine[i]);
                        string machinePutData = "{{" +
                                "\"virtualLine_id\":{0}," +
                                "\"machine_id\":{1}" +
                                "}}";
                        machinePutData = string.Format(machinePutData, virtualLine_id, Convert.ToInt32(Machine[i]) );
                        string machinePutResult = PostUrl(machinePutUrl, machinePutData);
                        JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                        if (Convert.ToInt32(joMachinePut["code"]) != 200)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        return Json("Success");
                    }
                    else
                    {
                        return Json("Fail");
                    }
                }
                else
                {
                    return Json("Success");
                }
        }

        public IActionResult DeleteMachine(int id, int group_id)
        {
            url = url + "api/v1/configuration/work_order/virtual_line/" + group_id + "?machine_id=" + id;
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
    }
}