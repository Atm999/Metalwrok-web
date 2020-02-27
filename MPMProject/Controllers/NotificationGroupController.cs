using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Model.notification_group;

namespace MPMProject.Controllers
{
    public class NotificationGroupController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            url = url + "api/v1/configuration/andon/notification_group";
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

        public IActionResult Update([FromBody]notification_group group)
        {
            url = url + "api/v1/configuration/andon/notification_group";
            var postData = JsonConvert.SerializeObject(group);
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
        public IActionResult Add([FromBody]notification_group group)
        {
            url = url + "api/v1/configuration/andon/notification_group";
            var postData = JsonConvert.SerializeObject(group);
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

        public IActionResult Delete([FromBody]notification_group group)
        {
            url = url + "api/v1/configuration/andon/notification_group?id=" + group.id.ToString();
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

        public JsonResult Getperson()
        {
            var purl = url + "api/v1/configuration/public/person";
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

        public IActionResult UpdatePerson([FromBody]notification_person person)
        {
            url = url + "api/v1/configuration/andon/notification_group/";
            var postData = JsonConvert.SerializeObject(person);
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


        //群组下人员查询
        public JsonResult GetmachineList(int group_id)
        {
            url = url + "api/v1/configuration/andon/notification_group";
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<notification_groupPerson>>();
            var data = list.FirstOrDefault(p => p.id == group_id).person;
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

        public IActionResult AddPerson(int id, int group_id)
        {
            //人员绑定
            string[] Machine = Convert.ToString(HttpContext.Request.Form["Machine"]).Split(',');
            if (Machine.Length > 0 && Machine[0] != "")
            {
                bool flag = true;
                for (int i = 0; i < Machine.Length; i++)
                {
                    string machinePutUrl = url + "api/v1/configuration/andon/notification_group/" + group_id + "?person_id=" + Convert.ToInt32(Machine[i]);
                    string machinePutData = "{{" +
                            "\"group_id\":{0}," +
                            "\"person_id\":{1}" +
                            "}}";
                    machinePutData = string.Format(machinePutData, group_id, Convert.ToInt32(Machine[i]));
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
            url = url + "api/v1/configuration/andon/notification_group/" + group_id + "?person_id=" + id;
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