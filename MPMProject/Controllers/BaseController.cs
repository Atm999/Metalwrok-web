using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MPMProject.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }
        /// <summary>
        /// http post方法
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="postData">抛送数据</param>
        /// <returns></returns>
        public string PostUrl(string url, string postData)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.Timeout = 80000;
                req.ContentType = "application/json";
                //req.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] data = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);

                    reqStream.Close();
                }
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }
        /// <summary>
        /// http put方法
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="postData">抛送数据</param>
        /// <returns></returns>
        public string PutUrl(string url, string postData)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "Put";
                req.Timeout = 80000;
                req.ContentType = "application/json";
                req.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] data = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);

                    reqStream.Close();
                }
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }

        /// <summary>
        /// http get方法
        /// </summary>
        /// <param name="Url">url地址</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="postDataStr">抛送参数</param>
        /// <returns></returns>
        public string GetUrl(string Url)
        {
            string result = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                req.Method = "GET";
                req.ContentType = "text/html;charset=UTF-8";
                req.Timeout = 80000;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream myResponseStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                result = reader.ReadToEnd();
                reader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
        /// <summary>
        /// http delete方法
        /// </summary>
        /// <param name="Url">url地址</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="postDataStr">抛送参数</param>
        /// <returns></returns>
        public string DeleteUrl(string Url)
        {
            string result = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                req.Method = "Delete";
                req.ContentType = "text/html;charset=UTF-8";
                req.Timeout = 80000;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream myResponseStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                result = reader.ReadToEnd();
                reader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
    }
}