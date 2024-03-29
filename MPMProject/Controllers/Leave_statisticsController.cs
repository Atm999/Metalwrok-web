﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    public class Leave_statisticsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/lpm/leave_statistics";
            var typeList = CommonHelper<leave_statistics>.Get(myurl, HttpContext);

            var purl = url + "api/v1/configuration/public/person";
            var subList = CommonHelper<Person>.Get(purl, HttpContext);

            var durl = url + "api/v1/configuration/public/dept";
            var deptList = CommonHelper<dept>.Get(durl, HttpContext);

            var jo2 = typeList.Join(subList, p => p.person_id, p => (p as Model.Person).id, (p, q) => new { p.id, p.start_time, p.end_time, p.duration, p.person_id, q.id_num, q.user_name, q.dept_id }).ToList();

            var jo3 = jo2.Join(deptList, p => p.dept_id, p => (p as Model.dept).id, (p, q) => new { p.id, p.start_time, p.end_time, p.duration, p.person_id, p.id_num, p.user_name, p.dept_id, q.name_cn }).ToList();
            return Json(jo3);
        }

        public IActionResult Update(leave_statistics ec)
        {
            string myurl = url + "api/v1/configuration/lpm/leave_statistics";
            var postData = JsonConvert.SerializeObject(ec);
            string result = PutUrl(myurl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    return Json("Success");
                case 400:
                    return Json("fail");

            }
            return Json("fail");
        }
        public IActionResult Add(leave_statistics ec)
        {
            string myurl = url + "api/v1/configuration/lpm/leave_statistics";
            var postData = JsonConvert.SerializeObject(ec);
            string result = PostUrl(myurl, postData);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    return Json("Success");
                case 400:
                    return Json("fail");
            }
            return Json("fail");
        }

        public IActionResult Delete([FromBody]leave_statistics ec)
        {
            string myurl = url + "api/v1/configuration/lpm/leave_statistics?id=" + ec.id.ToString();
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

        public JsonResult Getperson()
        {
            string myurl = url + "api/v1/configuration/public/person";

            return Json(CommonHelper<Person>.Get(myurl, HttpContext));
        }

        public JsonResult Getpersonsubstitute()
        {
            string purl = url + "api/v1/configuration/public/person";

            var plist = (CommonHelper<Person>.Get(purl, HttpContext));//所有人员

            string myurl = url + "api/v1/configuration/lpm/person_shift";
            var typeList = CommonHelper<person_shift>.Get(myurl, HttpContext);//计划里面的人员

          
            var lists = plist.Where(p => !typeList.Any(p2 => p2.person_id == p.id)).ToList();
            return Json(lists);


        }
    }
}