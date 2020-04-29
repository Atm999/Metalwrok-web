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
    public class PersonController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
          var  purl = url + "api/v1/configuration/public/person";
            string myurl = url + "api/v1/configuration/public/dept";

            var personList = CommonHelper<Person>.Get(purl, HttpContext); 
            var deptList = CommonHelper<dept>.Get(myurl, HttpContext); 
            //var dat = (from p in subList
            //          join q in typeList
            //          on p.tag_type_id equals q.id
            //          select new { p.id, p.name_cn, p.name_en, p.name_tw, typeName = q.name_cn }).ToList();
            var jo2 = personList.Join(deptList, p => p.dept_id, p => (p as Model.dept).id, (p, q) => new { p.id, p.user_name, p.id_num, p.user_level, deptName = q.name_cn, p.dept_id, p.email,p.wechart,p.mobile_phone }).ToList();
            return Json(jo2);
        }
        public JsonResult Getdept()
        {
            var purl = url + "api/v1/configuration/public/dept";
          
            return Json(CommonHelper<dept>.Get(purl, HttpContext)); 
        }

        public IActionResult Update([FromBody]Person person)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/public/person";
         
            var typeList = CommonHelper<Person>.Get(myurl1, HttpContext);
            var list = typeList.Where(p => p.id_num != person.id_num);

            var lists = list.Any(p => p.id_num == person.id_num);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/public/person";
                string postData = "{{\"id\":{0},\"user_name\":\"{1}\",\"id_num\":\"{2}\",\"user_level\":\"{3}\",\"email\":\"{4}\",\"wechart\":\"{5}\",\"mobile_phone\":\"{6}\",\"user_position\":\"{7}\",\"dept_id\":{8}}}";
                postData = string.Format(postData, person.id, person.user_name, person.id_num, person.user_level, person.email, person.wechart, person.mobile_phone, person.user_position, person.dept_id);
                string result = PutUrl(myurl, postData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                switch (Convert.ToInt32(jo["code"]))
                {
                    case 200:
                        msg = "Success";
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
            }
            else {
                msg = "fail";
            }
            return Json(msg);
        }
        public IActionResult Add([FromBody]Person person)
        {
            person.user_position = "";
            string msg = "";
            string myurl1 = url + "api/v1/configuration/public/person";
          
            var typeList = CommonHelper<Person>.Get(myurl1, HttpContext); 
            var list = typeList.Any(p => p.id_num == person.id_num);
            if (list == false)
            {
                string myurl = url + "api/v1/configuration/public/person";
                var postData = JsonConvert.SerializeObject(person);
                string result = PostUrl(myurl, postData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                switch (Convert.ToInt32(jo["code"]))
                {
                    case 200:
                        msg = "Success";
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
            }
            else {
                msg = "fail";
            }
            
            return Json(msg);
        }

        public IActionResult Delete([FromBody]Person person)
        {
            string myurl = url + "api/v1/configuration/public/person?id=" + person.id.ToString();
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