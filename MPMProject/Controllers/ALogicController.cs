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
    public class ALogicController : BaseController
    {

      
        public IActionResult Indexs()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/error_config_detail";
            List<config> list = CommonHelper<config>.Get(myurl, HttpContext);
            list = list.OrderByDescending(x => x.id).ToList();
            return Json(list); 
        }

        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info tag_Info)
        {
            string myurls = url + "api/v1/configuration/public/tag_type_sub";
            var typeList = CommonHelper<tag_type_sub>.Get(myurls, HttpContext);
            var list = typeList.FirstOrDefault(p => p.name_cn == tag_Info.namecn);
            tag_Info.tag_type_sub_id = list.id;
            string tagInfoUrl = url + "api/v1/configuration/public/tag";
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
                var tagInfoPutData = JsonConvert.SerializeObject(tag_Info); 
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
        public IActionResult Update([FromBody]error_config ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_config";
            var typeList = CommonHelper<error_config>.Get(myurl1, HttpContext); 
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.machine_id == ec.machine_id && p.tag_type_sub_id == ec.tag_type_sub_id);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/error_config";
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
        public IActionResult Add([FromBody]error_config ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/error_config";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = CommonHelper<error_config>.Get(myurl1, HttpContext); 

            var list = typeList.Any(p => p.machine_id == ec.machine_id && p.tag_type_sub_id == ec.tag_type_sub_id );
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/error_config";
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

        public IActionResult Delete([FromBody]error_config ec)
        {
            string myurl = url + "api/v1/configuration/andon/error_config?id=" + ec.id.ToString();
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
          
            return Json(CommonHelper<machine>.Get(purl, HttpContext) );
        }

        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";
          
            return Json(CommonHelper<notification_group>.Get(myurl, HttpContext));
        }

        public JsonResult Gettagsub()
        {
            string myurl = url + "api/v1/configuration/public/tag_type_sub";
            var flist = CommonHelper<tag_type_sub>.Get(myurl, HttpContext);
            var fdata = flist.Where(p => p.tag_type_id == 3);
           
            return Json(fdata);
        }

        public JsonResult Getperson()
        {
            string myurl = url + "api/v1/configuration/public/person";
           
            return Json(CommonHelper<Person>.Get(myurl, HttpContext));
        }

        public JsonResult GetAndonLogic()
        {
            string myurl = url + "api/v1/configuration/andon/andon_logic";

            return Json(CommonHelper<andon_logic>.Get(myurl, HttpContext));
        }

    }
}