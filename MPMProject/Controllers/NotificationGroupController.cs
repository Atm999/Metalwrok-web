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
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";
            return Json(CommonHelper<notification_groupPerson>.Get(myurl, HttpContext));
        }

        public IActionResult Update([FromBody]notification_group group)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_type";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = CommonHelper<notification_group>.Get(myurl1, HttpContext);
            var list = typeList.Where(p => p.id != group.id);

            var lists = list.Any(p => p.name_cn == group.name_cn || p.name_en == group.name_en || p.name_tw == group.name_tw);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/notification_group";
                var postData = JsonConvert.SerializeObject(group);
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
            else {
                msg = "fail";
            }
               
            return Json(msg);
        }
        public IActionResult Add([FromBody]notification_group group)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/notification_group";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = CommonHelper<notification_group>.Get(myurl1, HttpContext);

            var list = typeList.Any(p => p.name_cn == group.name_cn || p.name_en == group.name_en || p.name_tw == group.name_tw);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/notification_group";
                var postData = JsonConvert.SerializeObject(group);
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
            else {
                msg = "fail";
            }
            return Json(msg);
        }

        public IActionResult Delete([FromBody]notification_group group)
        {
            string myurl = url + "api/v1/configuration/andon/notification_group?id=" + group.id.ToString();
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

        public JsonResult Getperson(int group_id)
        {
            List<Person> machines = new List<Person>();
            var purl = url + "api/v1/configuration/public/person";
           

            var list = CommonHelper<Person>.Get(purl, HttpContext);
            string myurl = url + "api/v1/configuration/andon/notification_group";
          
            var mylist = CommonHelper<notification_groupPerson>.Get(myurl, HttpContext); 
            var data = mylist.FirstOrDefault(p => p.id == group_id).person;
            var otherPersons=list.Where(p=>!data.Select(q=>q.person_id).Contains(p.id));
            return Json(otherPersons);
        }

        public IActionResult UpdatePerson([FromBody]notification_person person)
        {
            string myurl = url + "api/v1/configuration/andon/notification_group/";
            var postData = JsonConvert.SerializeObject(person);
            string result = PutUrl(myurl, postData);
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
            string myurl = url + "api/v1/configuration/andon/notification_group";
            var list = CommonHelper<notification_groupPerson>.Get(myurl, HttpContext);
            var data = list.FirstOrDefault(p => p.id == group_id).person;
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
            string myurl = url + "api/v1/configuration/andon/notification_group/" + group_id + "?person_id=" + id;
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