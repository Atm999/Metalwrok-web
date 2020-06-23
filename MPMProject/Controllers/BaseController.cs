using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Model;
using Wise_Paas;

namespace MPMProject.Controllers
{
    public class BaseController : Controller
    {
        public static string url = "";
        

        public wise_paas_user GetLoginUser() {
            wise_paas_user user = new wise_paas_user();
            HttpContext.Request.Cookies.TryGetValue("userName", out string user_name);
            HttpContext.Request.Cookies.TryGetValue("role", out string role);
            user.name = user_name;
            user.role = role;
            return user;
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
                string token =  Cookies.GetEIToken(HttpContext);
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

        public string PostUrl(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            string token = Cookies.GetEIToken(HttpContext);
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
                string token = Cookies.GetEIToken(HttpContext);
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
        /// http put方法
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="postData">抛送数据</param>
        /// <returns></returns>
        public string PutUrl(string url)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "Put";
                req.Timeout = 80000;
                req.ContentType = "application/json";
                req.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string token = Cookies.GetEIToken(HttpContext);
                if (!string.IsNullOrEmpty(token))
                {
                    string authorization = "{0} {1}";
                    authorization = string.Format(authorization, "Bearer", token);
                    req.Headers.Add("Authorization", authorization);
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
                string token = Cookies.GetEIToken(HttpContext);
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
        public string DeleteUrl(string Url)
        {
            string result = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                req.Method = "Delete";
                req.ContentType = "text/html;charset=UTF-8";
                req.Timeout = 80000;
                string token = Cookies.GetEIToken(HttpContext);
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