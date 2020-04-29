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
    public class Machine_fault_alertController : BaseController 
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/machine_fault_alert_detail";
            var machineList = CommonHelper<machine_fault_alertDto>.Get(myurl, HttpContext); 

            var purl = url + "api/v1/configuration/public/tag_extra";
            var tag_info_extraList = CommonHelper<tag_info_extra>.Get(purl, HttpContext); 

            var dat =
                from p in
                machineList
                join
                y in tag_info_extraList.Where(n => n.tag_type_sub_id == 24)
                on p.error_type_detail_id equals y.target_id
                into g
                from o in g.DefaultIfEmpty()
                select new
                {
                    p.id,
                    p.error_type.name_cn,
                    pdname=p.error_type_detail.name_cn,
                    p.fault_times,
                    p.notice_group_id,
                    p.notice_type,
                    p.enable,
                    p.error_type_detail_id,
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
            tag_Info.tag_type_sub_id = 24;
            tag_Info.target_type = 3;
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
        public IActionResult Update([FromBody]machine_fault_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/machine_fault_alert";
            var typeList = CommonHelper<machine_fault_alert>.Get(myurl1, HttpContext);
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.error_type_detail_id == ec.error_type_detail_id );
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/machine_fault_alert";
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
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }
        public IActionResult Add([FromBody]machine_fault_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/machine_fault_alert";
            var typeList = CommonHelper<machine_fault_alert>.Get(myurl1, HttpContext); 

            var list = typeList.Any(p => p.error_type_detail_id == ec.error_type_detail_id );
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/machine_fault_alert";
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
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }

        public IActionResult Delete([FromBody]machine_fault_alert ec)
        {
            string myurl = url + "api/v1/configuration/andon/machine_fault_alert?id=" + ec.id.ToString();
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

        public JsonResult GetError()
        {
            var purl = url + "api/v1/configuration/andon/error_type";
            
            return Json(CommonHelper<error_type>.Get(purl, HttpContext));
        }
        public JsonResult GetErrorDetail()
        {
            var purl = url + "api/v1/configuration/andon/error_type_detail";
           
            return Json(CommonHelper<error_type_detail>.Get(purl, HttpContext));
        }
        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";
           
            return Json(CommonHelper<notification_group>.Get(myurl, HttpContext));
        }
    }
}