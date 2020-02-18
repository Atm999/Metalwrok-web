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
    public class MachineBindController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";

        public IActionResult Index()
        {
            ViewBag.machine_id = Convert.ToInt32(Request.Query["machine_id"]);
            ViewBag.name_cn = Convert.ToString(Request.Query["machine_name_cn"]);
            ViewBag.description = Convert.ToString(Request.Query["machine_description"]);


            return View();
        }

        //各节点下设备查询
        public JsonResult GetMachineTagList()
        {
            int machine_id = Convert.ToInt32(Request.Query["machine_id"]);
            //tag_info
            string tagInfoUrl = url + "api/v1/configuration/public/tag";
            string tagInfoResult = GetUrl(tagInfoUrl);
            JObject tagInfoJo = (JObject)JsonConvert.DeserializeObject(tagInfoResult);
            //tag_type_sub
            string tagTypeSubUrl = url + "api/v1/configuration/public/tag_type_sub";
            string tagTypeSubResult = GetUrl(tagTypeSubUrl);
            JObject tagTypeSubJo = (JObject)JsonConvert.DeserializeObject(tagTypeSubResult);
            //tag_type
            string tagTypeUrl = url + "api/v1/configuration/public/tag_type";
            string tagTypeResult = GetUrl(tagTypeUrl);
            JObject tagTypeJo = (JObject)JsonConvert.DeserializeObject(tagTypeResult);

            if (Convert.ToInt32(tagInfoJo["code"]) == 200 && Convert.ToInt32(tagTypeSubJo["code"])==200 && Convert.ToInt32(tagTypeJo["code"]) == 200)
            {
                var tagInfoList = tagInfoJo["data"].ToObject<IList<Model.tag_info>>();
                var tagTypeSubList = tagTypeSubJo["data"].ToObject<IList<Model.tag_type_sub>>();
                var tagTypeList = tagTypeJo["data"].ToObject<IList<Model.tag_type>>();

                //层级
                var datTagInfo = (from m in tagInfoList
                                  where m.machine_id == machine_id
                                  join n in tagTypeSubList
                                  on m.tag_type_sub_id equals n.id
                                  join p in tagTypeList
                                  on n.tag_type_id equals p.id
                                  select new { m.id,m.name,m.description, tag_type_sub_id=n.id,tag_type_sub_name=n.name_cn,tag_type_id=p.id, tag_type_name=p.name_cn }).ToList();


                return Json(datTagInfo);
            }
            else
            {
                return Json("Fail");
            }

        }

        //查询TagType
        public JsonResult GetTagTypeList(string urlPara,int tag_type_id)
        {
            //tag_type
            string tagTypeUrl = url + "api/v1/configuration/public/"+ urlPara + "";
            string tagTypeResult = GetUrl(tagTypeUrl);
            JObject tagTypeJo = (JObject)JsonConvert.DeserializeObject(tagTypeResult);

            if (Convert.ToInt32(tagTypeJo["code"]) == 200)
            {
                if (urlPara == "tag_type")
                {
                    var tagTypeList = tagTypeJo["data"].ToObject<IList<Model.tag_type>>();

                    var datTagType = (from p in tagTypeList
                                      orderby p.name_cn
                                      select new { p.id, p.name_cn }).ToList();
                    return Json(datTagType);
                }
                else {
                    var tagTypeList = tagTypeJo["data"].ToObject<IList<Model.tag_type_sub>>();

                    var datTagType = (from p in tagTypeList
                                      where p.tag_type_id== tag_type_id
                                      orderby p.name_cn
                                      select new { p.id, p.name_cn }).ToList();
                    return Json(datTagType);
                }
                
            }
            else
            {
                return Json("Fail");
            }

        }

        //群组查询
        public JsonResult GetTagTypeSub(int tag_type_id=0)
        {
            url = url + "api/v1/configuration/public/tag_type_sub";
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (Convert.ToInt32(jo["code"]) == 200)
            {
                if (tag_type_id > 0 || tag_type_id == -1)
                {
                    var tagTypeSubList = jo["data"].ToObject<IList<Model.tag_type_sub>>();

                    var datTagTypeSub = (from p in tagTypeSubList
                                       where p.tag_type_id == tag_type_id
                                       orderby p.name_cn
                                       select new { p.id, p.name_cn, p.name_en, p.name_tw, p.description }).ToList();

                    return Json(datTagTypeSub);
                }
                else
                {
                    return Json(jo["data"]);
                }
            }
            else
            {
                return Json("Fail");
            }

        }

        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info tag_Info)
        {
            string tagInfoUrl = url + "api/v1/configuration/public/tag";
            int id = tag_Info.id;
            //新增
            if (id == 0)
            {
                string tagInfoPostData = "{{" +
                                "\"id\":{0}," +
                                "\"description\":\"{1}\"," +
                                "\"tag_type_sub_id\":{2}," +
                                "\"machine_id\":{3}," +
                                "\"name\":\"{4}\""+
                                "}}";
                tagInfoPostData = string.Format(tagInfoPostData, id, tag_Info.description, tag_Info.tag_type_sub_id, tag_Info.machine_id,tag_Info.name);
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
                                "\"machine_id\":{3}," +
                                "\"name\":\"{4}\"" +
                                "}}";
                tagInfoPutData = string.Format(tagInfoPutData, id, tag_Info.description, tag_Info.tag_type_sub_id, tag_Info.machine_id, tag_Info.name);
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

        #region 层级中数据删除
        //层级中数据删除
        public IActionResult Delete(int id, string flag)
        {
            url = url + "api/v1/configuration/public/" + flag + "?id=" + id + "";
            string result = DeleteUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (Convert.ToInt32(jo["code"]) == 200)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }

        }
        #endregion
    }
}