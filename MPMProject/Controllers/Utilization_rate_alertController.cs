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
    public class Utilization_rate_alertController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/utilization_rate_alert_detail";
            var machineList = CommonHelper<utilization_rate_alertDto>.Get(myurl, HttpContext);

            var purl = url + "api/v1/configuration/public/tag_extra";
            var tag_info_extraList = CommonHelper<tag_info_extra>.Get(purl, HttpContext);

            List<object> list = new List<object>();
            foreach (var obj in machineList)
            {
                tag_info_extra tag_info = new tag_info_extra();
                if (obj.utilization_rate_type == 0)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 0 && x.target_id == obj.machine_id && x.tag_type_sub_id == 15)
                        .FirstOrDefault();
                }
                else if (obj.utilization_rate_type == 1)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 0 && x.target_id == obj.machine_id && x.tag_type_sub_id == 16)
                        .FirstOrDefault();
                }
                else if (obj.utilization_rate_type == 2)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 0 && x.target_id == obj.machine_id && x.tag_type_sub_id == 17)
                        .FirstOrDefault();
                }
                object ob = new
                {
                    obj.id,
                    obj.machine_id,
                    obj.utilization_rate_type,
                    notice_group_id = obj.notice_group,
                    obj.notice_type,
                    obj.maximum,
                    obj.minimum,
                    obj.enable,
                    obj.machine.name_cn,
                    nname = obj.notice_group.name_cn,
                    tag_info?.name,
                    tag_info?.description,
                    extraid = tag_info?.id,
                    tag_info?.tag_type_sub_id
                };
                list.Add(ob);
            }
            return Json(list);
        }
        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info_extra tag_Info)
        {
            if (tag_Info.utilization_rate_types == 0)
            {
                tag_Info.tag_type_sub_id = 15;
            }
            else if (tag_Info.utilization_rate_types == 1)
            {
                tag_Info.tag_type_sub_id = 16;
            }
            else if (tag_Info.utilization_rate_types == 2)
            {
                tag_Info.tag_type_sub_id = 17;
            }
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
        public IActionResult Update([FromBody]utilization_rate_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/utilization_rate_alert";
            var typeList = CommonHelper<utilization_rate_alert>.Get(myurl1, HttpContext); 
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.machine_id == ec.machine_id && p.utilization_rate_type == ec.utilization_rate_type);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/utilization_rate_alert";
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
        public IActionResult Add([FromBody]utilization_rate_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/utilization_rate_alert";
            var typeList = CommonHelper<utilization_rate_alert>.Get(myurl1, HttpContext); 

            var list = typeList.Any(p => p.machine_id == ec.machine_id && p.utilization_rate_type == ec.utilization_rate_type);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/utilization_rate_alert";
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

        public IActionResult Delete([FromBody]utilization_rate_alert ec)
        {
            string myurl = url + "api/v1/configuration/andon/utilization_rate_alert?id=" + ec.id.ToString();
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

    }
}