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
    public class Attendance_statisticsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/lpm/attendance_statistics";
            var typeList = CommonHelper<attendance_statistics>.Get(myurl, HttpContext);
            var purl = url + "api/v1/configuration/public/person";
            var subList = CommonHelper<Person>.Get(purl, HttpContext);
            var jo2 = typeList.Join(subList, p => p.person_id, p => (p as Model.Person).id, (p, q) => new { p.id, p.person_id,p.is_attend, p.shift, p.date, q.id_num, q.user_name, q.dept_id }).ToList();
            return Json(jo2);
        }

        public IActionResult Update([FromBody]attendance_statistics ec)
        {
            string myurl = url + "api/v1/configuration/lpm/attendance_statistics";
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
        public IActionResult Add([FromBody]attendance_statistics ec)
        {
            string myurl = url + "api/v1/configuration/lpm/attendance_statistics";
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

        public IActionResult Delete([FromBody]attendance_statistics ec)
        {
            string myurl = url + "api/v1/configuration/lpm/attendance_statistics?id=" + ec.id.ToString();
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
    }
}