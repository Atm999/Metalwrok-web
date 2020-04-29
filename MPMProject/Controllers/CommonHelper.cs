using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wise_Paas;

namespace MPMProject.Controllers
{
    public static class CommonHelper<T> where T :  new()
    {
        public static List<T> Get(string url, HttpContext httpContext)
        {
            JObject fjo = (JObject)JsonConvert.DeserializeObject(GetUrl(url,httpContext));
            var flist = fjo["data"].ToObject<IList<T>>().ToList();
            return flist;
        }


        public static bool Post(string url, string postData, HttpContext httpContext)
        {
            JObject fjo = (JObject)JsonConvert.DeserializeObject(PostUrl(url, postData,httpContext));
            switch (Convert.ToInt32(fjo["code"]))
            {
                case 200:
                    return true;
                case 400:
                    break;
                case 410:
                    break;
                case 411:
                    break;
                default:
                    break;
            }
            return false;
        }

        public static bool Post(string url, HttpContext httpContext)
        {
            JObject fjo = (JObject)JsonConvert.DeserializeObject(PostUrl(url,httpContext));
            switch (Convert.ToInt32(fjo["code"]))
            {
                case 200:
                    return true;
                case 400:
                    break;
                case 410:
                    break;
                case 411:
                    break;
                default:
                    break;
            }
            return false;
        }

        public static bool Put(string url, string postData, HttpContext httpContext)
        {
            JObject fjo = (JObject)JsonConvert.DeserializeObject(PutUrl(url, postData,httpContext));
            switch (Convert.ToInt32(fjo["code"]))
            {
                case 200:
                    return true;
                case 400:
                    break;
                case 410:
                    break;
                case 411:
                    break;
                default:
                    break;
            }
            return false;
        }

        public static bool Delete(string url, string postData, HttpContext httpContext)
        {
            JObject fjo = (JObject)JsonConvert.DeserializeObject(DeleteUrl(url,httpContext));
            switch (Convert.ToInt32(fjo["code"]))
            {
                case 200:
                    return true;
                case 400:
                    break;
                case 410:
                    break;
                case 411:
                    break;
                default:
                    break;
            }
            return false;
        }



        /// <summary>
        /// http post方法
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="postData">抛送数据</param>
        /// <returns></returns>
        public static string PostUrl(string url, string postData, HttpContext httpContext)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.Timeout = 80000;
                req.ContentType = "application/json";
                string token = Cookies.GetEIToken(httpContext);
                if (!string.IsNullOrEmpty(token))
                {
                    string authorization = "{0} {1}";
                    authorization = string.Format(authorization, "Bearer", token);
                    req.Headers.Add("Authorization", authorization);
                }
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

        public static string PostUrl(string url, HttpContext httpContext)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            string token = Cookies.GetEIToken(httpContext);
            if (!string.IsNullOrEmpty(token))
            {
                string authorization = "{0} {1}";
                authorization = string.Format(authorization, "Bearer", token);
                req.Headers.Add("Authorization", authorization);
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// http put方法
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="postData">抛送数据</param>
        /// <returns></returns>
        public static string PutUrl(string url, string postData,HttpContext httpContext)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "Put";
                req.Timeout = 80000;
                req.ContentType = "application/json";
                req.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string token = Cookies.GetEIToken(httpContext);
                if (!string.IsNullOrEmpty(token))
                {
                    string authorization = "{0} {1}";
                    authorization = string.Format(authorization, "Bearer", token);
                    req.Headers.Add("Authorization", authorization);
                }
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
        public static string GetUrl(string Url, HttpContext httpContext)
        {
            string result = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                req.Method = "GET";
                req.ContentType = "text/html;charset=UTF-8";
                req.Timeout = 80000;
                string token = Cookies.GetEIToken(httpContext);
                if (!string.IsNullOrEmpty(token))
                {
                    string authorization = "{0} {1}";
                    authorization = string.Format(authorization, "Bearer", token);
                    req.Headers.Add("Authorization", authorization);
                }
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
        public static string DeleteUrl(string Url, HttpContext httpContext)
        {
            string result = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                req.Method = "Delete";
                req.ContentType = "text/html;charset=UTF-8";
                req.Timeout = 80000;
                string token = Cookies.GetEIToken(httpContext);
                if (!string.IsNullOrEmpty(token))
                {
                    string authorization = "{0} {1}";
                    authorization = string.Format(authorization, "Bearer", token);
                    req.Headers.Add("Authorization", authorization);
                }
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
