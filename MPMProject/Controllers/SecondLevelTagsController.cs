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
    /// <summary>
    /// 公共配置——二级标签
    /// </summary>
    public class SecondLevelTagsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            var purl = url + "api/v1/configuration/public/tag_type";
            var result1 = GetUrl(purl);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);

            string myurl = url + "api/v1/configuration/public/tag_type_sub";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var typeList = jo1["data"].ToObject<IList<Model.tag_type>>();
            var subList = jo["data"].ToObject<IList<Model.tag_type_sub>>();
            //var dat = (from p in subList
            //          join q in typeList
            //          on p.tag_type_id equals q.id
            //          select new { p.id, p.name_cn, p.name_en, p.name_tw, typeName = q.name_cn }).ToList();
            var jo2 = subList.Join(typeList, p => p.tag_type_id, p => (p as Model.tag_type).id, (p, q) => new {p.id,p.name_cn,p.name_en,p.name_tw,typeName=q.name_cn,p.tag_type_id,p.description }).ToList();
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo2);
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
            return Json(jo2);
        }

        public JsonResult GetTagType()
        {
            var purl = url + "api/v1/configuration/public/tag_type";
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

        public IActionResult Update([FromBody]tag_type_sub sub) 
        {
            string myurl = url + "api/v1/configuration/public/tag_type_sub";
            string postData = "{{\"id\":{0},\"name_en\":\"{1}\",\"name_cn\":\"{2}\",\"name_tw\":\"{3}\",\"description\":\"{4}\",\"tag_type_id\":{5}}}";
            postData = string.Format(postData, sub.id, sub.name_en, sub.name_cn, sub.name_tw, sub.description, sub.tag_type_id);
            string result = PutUrl(myurl, postData);
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
        public IActionResult Add([FromBody]tag_type_sub sub)
        {
            string myurl = url + "api/v1/configuration/public/tag_type_sub";
            string postData = "{{\"id\":{0},\"name_en\":\"{1}\",\"name_cn\":\"{2}\",\"name_tw\":\"{3}\",\"description\":\"{4}\",\"tag_type_id\":{5}}}";
            postData = string.Format(postData, sub.id, sub.name_en, sub.name_cn, sub.name_tw, sub.description,sub.tag_type_id);
            string result = PostUrl(myurl, postData);
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

        public IActionResult Delete([FromBody]tag_type_sub sub)
        {
            string myurl = url + "api/v1/configuration/public/tag_type_sub?id=" + sub.id.ToString();
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