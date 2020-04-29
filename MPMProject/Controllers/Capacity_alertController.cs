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
    public class Capacity_alertController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string geturl = url + "api/v1/configuration/andon/capacity_alert_detail";
            var machineList = CommonHelper<capacity_alertDto>.Get(geturl, HttpContext);

            var purl = url + "api/v1/configuration/public/tag_extra";
            var tag_info_extraList = CommonHelper<tag_info_extra>.Get(purl, HttpContext); 

            //var nurl = url + "api/v1/configuration/public/area_node";
            //var nresult = GetUrl(nurl);
            //JObject njo = (JObject)JsonConvert.DeserializeObject(nresult);
            //var nodeList = njo["data"].ToObject<IList<Model.area_node>>();

            //var datAreaNode = (from p in nodeList
            //                   orderby p.id 
            //                   select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description,p.upper_id ,p.area_layer_id }).ToList();
            //var dat =
            //      from p in
            //      machineList
            //      join
            //      y in tag_info_extraList.Where(n => n.tag_type_sub_id == 23 && n.target_type == 1)
            //      on p.capacity equals y.target_id///????有问题的
            //      into g
            //      from o in g.DefaultIfEmpty()
            //      select new
            //      {
            //          p.id,
            //          p.date,
            //          p.capacity,
            //          p.notice_group_id,
            //          p.notice_type,
            //          p.enable,
            //          nname = p.notice_group.name_cn,
            //          o?.name,//o!=null?o.name:null
            //          o?.description,
            //          extraid = o?.id
            //      };
            List<object> list = new List<object>();

            tag_info_extra o = new tag_info_extra();
            o = tag_info_extraList
                .Where(x => x.target_type == 1 && x.target_id == 0 && x.tag_type_sub_id == 23)
                .FirstOrDefault();
            foreach (var p in machineList)
            {
                object ob = new
                {
                    p.id,
                    p.date,
                    p.capacity,
                    p.notice_group_id,
                    p.notice_type,
                    p.enable,
                    nname = p.notice_group.name_cn,
                    o?.name,//o!=null?o.name:null
                    o?.description,
                    extraid = o?.id
                };
                list.Add(ob);
            }
            return Json(list);
        }
        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info_extra tag_Info)
        {
            tag_Info.tag_type_sub_id = 23;
            tag_Info.target_type = 1;
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
        public IActionResult Update([FromBody]capacity_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/capacity_alert";
            var typeList = CommonHelper<capacity_alert>.Get(myurl1, HttpContext);
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.date == ec.date);
            if (lists == false)
            {
                string updateurl = url + "api/v1/configuration/andon/capacity_alert";
                var postData = JsonConvert.SerializeObject(ec);
                string result = PutUrl(updateurl, postData);
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
        public IActionResult Add([FromBody]capacity_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/capacity_alert";
            var typeList = CommonHelper<capacity_alert>.Get(myurl1, HttpContext);

            var list = typeList.Any(p => p.date == ec.date);
            if (list == false)//没有重复的
            {
                string addurl = url + "api/v1/configuration/andon/capacity_alert";
                var postData = JsonConvert.SerializeObject(ec);
                string result = PostUrl(addurl, postData);
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

        public IActionResult Delete([FromBody]capacity_alert ec)
        {
            string deleteurl = url + "api/v1/configuration/andon/capacity_alert?id=" + ec.id.ToString();
            string result = DeleteUrl(deleteurl);
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

        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";

            return Json(CommonHelper<notification_group>.Get(myurl, HttpContext));
        }
    }
}