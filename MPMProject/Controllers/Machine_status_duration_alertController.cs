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
    public class Machine_status_duration_alertController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/machine_status_duration_alert_detail";
            var machineList = CommonHelper<machine_status_duration_alertDto>.Get(myurl, HttpContext);

            var purl = url + "api/v1/configuration/public/tag_extra";
            var tag_info_extraList = CommonHelper<tag_info_extra>.Get(purl, HttpContext);  

            var dat =
                from p in
                machineList
                join
                y in tag_info_extraList.Where(n => n.tag_type_sub_id == 14)
                on p.machine_id equals y.target_id
                into g
                from o in g.DefaultIfEmpty()
                select new
                {
                    p.id,
                    p.machine_id,
                    p.machine_status,
                    p.notice_group_id,
                    p.notice_type,
                    p.duration,
                    p.enable,
                    p.machine.name_cn,
                    nname = p.notice_group.name_cn,
                    o?.name,//o!=null?o.name:null
                    o?.description,
                    extraid = o?.id
                };
            return Json(dat);
        }
        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info_extra tag_Info)
        {
            tag_Info.tag_type_sub_id = 14;
            tag_Info.target_type = 0;
            string tagInfoUrl = url + "api/v1/configuration/public/tag_extra";
            int id = tag_Info.id;
            //新增
            if (id == 0)
            {
                var tagInfoPostData = JsonConvert.SerializeObject(tag_Info);
                string tagInfoPostResult = PostUrl(tagInfoUrl, tagInfoPostData);
                JObject joMachinePost = (JObject)JsonConvert.DeserializeObject(tagInfoPostResult);
                if (Convert.ToInt32(joMachinePost["code"]) == 200)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {//修改
                var tagInfoPostData = JsonConvert.SerializeObject(tag_Info);
                string tagInfoPutResult = PutUrl(tagInfoUrl, tagInfoPostData);
                JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(tagInfoPutResult);
                if (Convert.ToInt32(joMachinePut["code"]) == 200)
                {
                    return Json("Success");
                }

                else
                {
                    return Json("Fail");
                }

            }
        }
        public IActionResult Update([FromBody]machine_status_duration_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/machine_status_duration_alert";
            var typeList = CommonHelper<machine_status_duration_alert>.Get(myurl1, HttpContext); 
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.machine_id == ec.machine_id && p.machine_status == ec.machine_status);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/machine_status_duration_alert";
                var postData = JsonConvert.SerializeObject(ec);
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
        public IActionResult Add([FromBody]machine_status_duration_alert ec)
        {

            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/machine_status_duration_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = CommonHelper<machine_status_duration_alert>.Get(myurl1, HttpContext); 

            var list = typeList.Any(p => p.machine_id == ec.machine_id && p.machine_status == ec.machine_status);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/machine_status_duration_alert";
                var postData = JsonConvert.SerializeObject(ec);
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

        public IActionResult Delete([FromBody]machine_status_duration_alert ec)
        {
            string myurl = url + "api/v1/configuration/andon/machine_status_duration_alert?id="+ ec.id.ToString();
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

        public JsonResult Getmachine()
        {
            var purl = url + "api/v1/configuration/public/machine";

            return Json(CommonHelper<machine>.Get(purl, HttpContext));
        }

        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";

            return Json(CommonHelper<notification_group>.Get(myurl, HttpContext));
        }


        public JsonResult Getmachinestatus()
        {
            string myurl = url + "api/v1/configuration/oee/status_setting";

            return Json(CommonHelper<status_setting>.Get(myurl, HttpContext));
        }
    }
}