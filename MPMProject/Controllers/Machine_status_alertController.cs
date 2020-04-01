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
    public class Machine_status_alertController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/machine_status_alert_detail";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var machineList = jo["data"].ToObject<IList<Model.machine_status_alertDto>>();

            var purl = url + "api/v1/configuration/public/tag_extra";
            var result1 = GetUrl(purl);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var tag_info_extraList = jo1["data"].ToObject<IList<Model.tag_info_extra>>();

            var dat =
                from p in
                machineList
                join
                y in tag_info_extraList.Where(n => n.tag_type_sub_id == 13)
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
                    p.enable,
                    p.machine.name_cn,
                    nname = p.notice_group.name_cn,
                    o?.name,//o!=null?o.name:null
                    o?.description,
                    extraid = o?.id
                };

            //var dat = (from p in machine_status_alertList
            //           join q in machineList
            //           on p.machine_id equals q.id
            //           join a in notice_groupList
            //           on p.notice_group_id equals a.id
            //           join t in tag_info_extraList.Where(n => n.tag_type_sub_id == 13)
            //           on p.machine_id equals t.target_id   
            //           into g
            //           from z in g.DefaultIfEmpty()

            //           select new { p.id,p.machine_id,p.machine_status,p.notice_group_id,p.notice_type,p.enable, q.name_cn,nname=a.name_cn, z?.name, z?.description, extraid = z?.id }).ToList();


            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
                    break;
                case 400:
                    break;
            }
            return Json(dat);
        }
        public IActionResult Update([FromBody]machine_status_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/machine_status_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.machine_status_alert>>();
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.machine_id == ec.machine_id && p.machine_status == ec.machine_status);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/machine_status_alert";
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
        public IActionResult Add([FromBody]machine_status_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/machine_status_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.machine_status_alert>>();

            var list = typeList.Any(p => p.machine_id == ec.machine_id && p.machine_status == ec.machine_status);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/machine_status_alert";
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
            return Json(msg);
        }
        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info_extra tag_Info)
        {
            tag_Info.tag_type_sub_id = 13;
            tag_Info.target_type = 0;
            string tagInfoUrl = url + "api/v1/configuration/public/tag_extra";
            int id = tag_Info.id;
            //新增
            if (id == 0)
            {
                //string tagInfoPostData = "{{" +
                //                "\"id\":{0}," +
                //                "\"description\":\"{1}\"," +
                //                "\"tag_type_sub_id\":{2}," +
                //                "\"target_id\":{3}," +
                //                "\"name\":\"{4}\"," +
                //                 "\"target_type\":{5}," +
                //                "}}";
                //tagInfoPostData = string.Format(tagInfoPostData, id, tag_Info.description, tag_Info.tag_type_sub_id, tag_Info.target_id, tag_Info.name, 0);
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
                string tagInfoPutData = "{{" +
                                "\"id\":{0}," +
                                "\"description\":\"{1}\"," +
                                "\"tag_type_sub_id\":{2}," +
                                "\"target_id\":{3}," +
                                "\"name\":\"{4}\"," +
                                 "\"target_type\":{5}," +
                                "}}";
                tagInfoPutData = string.Format(tagInfoPutData, id, tag_Info.description, tag_Info.tag_type_sub_id, tag_Info.target_id, tag_Info.name, 0);
                string tagInfoPutResult = PutUrl(tagInfoUrl, tagInfoPutData);
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
        public IActionResult Delete([FromBody]machine_status_alert ec)
        {
            string myurl = url + "api/v1/configuration/andon/machine_status_alert?id=" + ec.id.ToString();
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

        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";
            string result = GetUrl(myurl);
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
        
        public JsonResult Getmachinestatus()
        {
            string myurl = url + "api/v1/configuration/oee/status_setting";
            string result = GetUrl(myurl);
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

    }
}