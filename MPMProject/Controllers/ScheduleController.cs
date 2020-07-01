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
    public class ScheduleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/lpm/schedule";
            var ne = CommonHelper<schedule>.Get(myurl, HttpContext);
            return Json(ne);
        }
        public JsonResult GetList(int schedule_id)
        {
            string myurl = url + "api/v1/configuration/lpm/person_shift";
            var jo = CommonHelper<person_shift>.Get(myurl, HttpContext);
            var typeList = jo.Where(p => p.schedule_id == schedule_id).ToList();

            var purl = url + "api/v1/configuration/public/person";
            var subList = CommonHelper<Person>.Get(purl, HttpContext);

            var murl = url + "api/v1/configuration/public/machine";
            var mList = CommonHelper<machine>.Get(murl, HttpContext);
          
            var jo2 = typeList.Join(subList, p => p.person_id, p => (p as Model.Person).id, (p, q) => new { p.id, p.person_id, p.machine_id, p.shift, p.schedule_id, q.id_num, q.user_name, q.dept_id }).ToList();
            var jo3 = jo2.Join(mList, p => p.machine_id, p => (p as Model.machine).id, (p, q) => new { p.id, p.person_id, p.machine_id, p.shift, p.schedule_id, p.id_num, p.user_name, p.dept_id,q.name_cn }).ToList();

            return Json(jo3);
        }
        public IActionResult Update(schedule ec)
        {
            string myurl = url + "api/v1/configuration/lpm/schedule";
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
        public IActionResult Add(schedule ec)
        {
            string myurl = url + "api/v1/configuration/lpm/schedule";
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
        public IActionResult addpshift(person_shift ec)
        {
            string myurl = url + "api/v1/configuration/lpm/person_shift";
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
        
        public IActionResult Delete([FromBody]schedule ec)
        {
            string myurl = url + "api/v1/configuration/lpm/schedule?id=" + ec.id.ToString();
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