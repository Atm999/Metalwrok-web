using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Model.virtual_line;
using static Model.wo_config;

namespace MPMProject.Controllers
{
    public class SiteclientController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取下拉选虚拟线的数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetVirtualLine()
        {
            string myurl = url + "api/v1/configuration/work_order/virtual_line";
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
        /// <summary>
        /// 联动获取设备的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetVirtualLineMachine(int id)
        {
            if (id == 0)
                return Json(new List<machine>());
            string myurl = url + "api/v1/configuration/work_order/virtual_line";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<virtual_lineMachine>>();
            var data = list.FirstOrDefault(p => p.id == id).Machines;
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(data);
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
            return Json(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtual_line_id"></param>
        /// <param name="machineid"></param>
        /// <returns></returns>
        public JsonResult GetWo(int virtual_line_id, int machineid)
        {
            var wurl = url + "api/v1/client/work_order/onsite?virtual_line_id=" + virtual_line_id;
            string result = GetUrl(wurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);

            //var list = jo["data"].ToObject<IList<wo_config>>();
            //var murl = url + "api/v1/client/work_order/machine_current_log";
            //string mresult = GetUrl(murl);
            //JObject mjo = (JObject)JsonConvert.DeserializeObject(mresult);
            //var mlist = mjo["data"].ToObject<IList<wo_machine_cur_log>>();
            //var data = from p in list
            //           join q in mlist.Where(p => p.machine_id == machineid)
            //           on p.id equals q.wo_config_id
            //           into g
            //           from z in g.DefaultIfEmpty()
            //           select new { wo_config = p, wo_machine_cur_log = z };

            //var data= mlist.Where(p => p.machine_id == machineid)
            //    .Join(list,
            //    p => p.wo_config_id, 
            //    q => ((wo_config)q).id, 
            //    (p, q) => new { wo_config= q, wo_machine_cur_log=p });

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

        //public JsonResult GetWork_order(int id)
        //{
        //    url = url + "api/v1/configuration/work_order/wo_config";
        //    string result = GetUrl(url);
        //    JObject jo = (JObject)JsonConvert.DeserializeObject(result);

        //    var list = jo["data"].ToObject<IList<wo_config>>();
        //    var data = list.First(p => p.id == id);
        //    return Json(data);
        //}
        /// <summary>
        /// 加载页面时判断  当前设备是否已开始工单
        /// </summary> //https://api-mpm.wise-paas.cn/api/v1/client/work_order/onsite/0?machine_id=122&work_order_id=12
        /// <param name="machineid"></param>
        ///  //转化为正常的时间类型
        //CultureInfo culture = new CultureInfo("en-us");
        //DateTime dt = Convert.ToDateTime("2/28/20 2:53:00 PM", culture);

        /// <returns></returns>
        public JsonResult GetIndex(int machineid)
        {
           

            //第一步：先根据machineid查machine_current_log是否有数据，有->显示页面；
            //第二步：查不到数据的情况下，查当前这一站是否是第一站
            var furl = url + "api/v1/client/work_order/machine_current_log";
            string fresult = GetUrl(furl);
            JObject fjo = (JObject)JsonConvert.DeserializeObject(fresult);
            var flist = fjo["data"].ToObject<IList<wo_machine_cur_log>>();
            var fdata = flist.FirstOrDefault(p => p.machine_id == machineid);
            if (fdata!=null&&fdata.wo_config_id != 0)
            {
                var wurl = url + "api/v1/configuration/work_order/wo_config";
                string wresult = GetUrl(wurl);
                JObject wjo = (JObject)JsonConvert.DeserializeObject(wresult);
                var wlist = wjo["data"].ToObject<IList<wo_config>>();
                var wdata = wlist.FirstOrDefault(p => p.id == fdata.wo_config_id);
                return Json(new { wo_config = wdata, wo_machine_cur_log = fdata }); 
            }
            //根据machineid查当前这一站是否是第一站
            var vurl = url + "api/v1/configuration/work_order/virtual_line";
            string vresult = GetUrl(vurl);
            JObject vjo = (JObject)JsonConvert.DeserializeObject(vresult);
            var vlist = vjo["data"].ToObject<IList<virtual_lineMachine>>();
            var entity = vlist.FirstOrDefault(p => p.Machines.Any(q => q.machine_id == machineid));//根据设备id找到所在的线
            if (entity == null)
            {
                return Json(null);
            }
            var index = entity.Machines.ToList().FindIndex(p => p.machine_id == machineid);
            var id = 0;
            bool isFirst = false;
            //如果是第一站则取当前的id,如果不是第一站，则取上一站的设备id
            if (index == 0)//第一站
            {
                id = machineid;//machine 表的machine_id(主键id)
                isFirst = true;
            }
            else if (index > 0)//不是第一站
            {
                var preEntity = entity.Machines[index - 1];//前一站是否开始工单
                id = preEntity.machine_id;
            }
            var murl = url + "api/v1/client/work_order/machine_current_log";
            string mresult = GetUrl(murl);
            JObject mjo = (JObject)JsonConvert.DeserializeObject(mresult);
            var mlist = mjo["data"].ToObject<IList<wo_machine_cur_log>>();
            if (mlist.Count == 0)
            {
                return Json(new { IsFirst=isFirst,wo_config=new List<wo_config>(), wo_machine_cur_log=new List<wo_machine_cur_log>()});
            }
            var data = mlist.FirstOrDefault(p => p.machine_id == id);
            if (data==null)
            {
                return Json(new { IsFirst = isFirst, wo_config = new List<wo_config>(), wo_machine_cur_log = new List<wo_machine_cur_log>() });
            }
            var durl = url + "api/v1/configuration/work_order/wo_config";
                string dresult = GetUrl(durl);
                JObject djo = (JObject)JsonConvert.DeserializeObject(dresult);
                var dlist = djo["data"].ToObject<IList<wo_config>>();
                var ddata = dlist.FirstOrDefault(p => p.id == data.wo_config_id);
                return Json(new { IsFirst = isFirst, wo_config = ddata, wo_machine_cur_log = data });
        }

        public JsonResult onsite(int type,int machine_id,int work_order_id)
        {
            string myurl = url + "api/v1/client/work_order/onsite/"+type+"?machine_id="+machine_id+"&work_order_id="+work_order_id;
            string postData = "{{" +
                               "\"type\":{0}," +
                               "\"machine_id\":{1}," +
                                "\"work_order_id\":{2}," +
                               "}}";
            postData = string.Format(postData, type, machine_id,work_order_id);
            string result = PostUrl(myurl, postData);
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

        //public JsonResult Getwork_order_id( int machineid)
        //{
        //    var furl = url + "api/v1/client/work_order/machine_current_log";
        //    string fresult = GetUrl(furl);
        //    JObject fjo = (JObject)JsonConvert.DeserializeObject(fresult);
        //    var flist = fjo["data"].ToObject<IList<wo_machine_cur_log>>();
        //    var fdata = flist.FirstOrDefault(p => p.machine_id == machineid);
        //    if (fdata != null && fdata.wo_config_id != 0)
        //    {
        //        var wurl = url + "api/v1/configuration/work_order/wo_config";
        //        string wresult = GetUrl(wurl);
        //        JObject wjo = (JObject)JsonConvert.DeserializeObject(wresult);
        //        var wlist = wjo["data"].ToObject<IList<wo_config>>();
        //        var wdata = wlist.FirstOrDefault(p => p.id == fdata.wo_config_id);
        //        return Json(new { wo_config = wdata, wo_machine_cur_log = fdata });
        //    }
        //    return Json("success");
        //}

    }
}