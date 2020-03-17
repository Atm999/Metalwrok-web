using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace MPMProject.Models
{
    public static class HttpHelper
    {
            /// <summary>
            /// 超时时间设置
            /// </summary>
            public static Int32 HttpRequestTimeOut = 5000;

            /// <summary>
            /// GET
            /// </summary>
            /// <param name="url">接口地址</param>
            /// <param name="accessToken">token 认证</param>
            /// <returns></returns>
            public static HttpResponseData Get(String url, string accessToken)
            {
                return Invoke<Object>("GET", url, null, null, accessToken);
            }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="Id">URL参数</param>
        /// <param name="accessToken">token 认证</param>
        /// <returns></returns>
        public static HttpResponseData Get(String url, Object Id,string accessToken)
            {
                return Invoke<Object>("GET", url, Id, null, accessToken);
            }

            /// <summary>
            /// POST
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="url">接口地址</param>
            /// <param name="Id">URL参数（可选）</param>
            /// <param name="Data">提交数据</param>
            /// <returns></returns>
            public static HttpResponseData Post<T>(String url, Object Id, T Data)
            {
                return Invoke("POST", url, Id, Data, "");
            }

            /// <summary>
            /// POST
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="url">接口地址</param>
            /// <param name="Id">URL参数（可选）</param>
            /// <param name="Data">提交数据</param>
            /// <returns></returns>
            public static HttpResponseData Post<T>(String url, T Data)
            {
                return Invoke("POST", url, null, Data, "");
            }

        /// <summary>
        /// PUT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">接口地址</param>
        /// <param name="Data">提交数据</param>
        /// <returns></returns>
        public static HttpResponseData Put<T>(String url, T Data)
            {
                return Invoke("PUT", url, null, Data, "");
            }
            /// <summary>
            /// PUT
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="url">接口地址</param>
            /// <param name="Id">URL参数（可选）</param>
            /// <param name="Data">提交数据（可选）</param>
            /// <returns></returns>
            public static HttpResponseData Put<T>(String url, Object Id, T Data)
            {
                return Invoke("PUT", url, Id, Data, "");
            }

            /// <summary>
            /// DELETE
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="url">接口地址</param>
            /// <param name="Id">url参数（可选）</param>
            /// <param name="Data">提交数据（可选）</param>
            /// <returns></returns>
            public static HttpResponseData Delete<T>(String url, Object Id, T Data, string accessToken)
            {
                return Invoke<Object>("DELETE", url, Id, Data, accessToken);
            }

            /// <summary>
            /// DELETE
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="url">接口地址</param>
            /// <param name="Id">url参数（可选）</param>
            /// <param name="Data">提交数据（可选）</param>
            /// <returns></returns>
            public static HttpResponseData Delete<T>(String url, T Data)
            {
                return Invoke<Object>("DELETE", url, null, Data, "");
            }



        /// <summary>
        /// 操作调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Method"></param>
        /// <param name="url"></param>
        /// <param name="Id"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        private static HttpResponseData Invoke<T>(String Method, String url, Object Id, T Data, string accessToken)
            {
                HttpResponseData Response = new HttpResponseData()
                {
                    Code = HttpStatusCode.RequestTimeout,
                    Data = String.Empty,
                    Message = String.Empty,
                };
                try
                {
                    String PostParam = String.Empty;
                    if (Data != null)
                    {
                        PostParam = JsonConvert.SerializeObject(Data);
                    }
                    byte[] postData = Encoding.UTF8.GetBytes(PostParam);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url + (Id == null ? "" : '/' + Id.ToString())));
                    request.Method = Method;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        string authorization = "{0} {1}";
                        authorization = string.Format(authorization, "Bearer", accessToken);
                        request.Headers.Add("Authorization", authorization);
                    }

                request.ServicePoint.Expect100Continue = false;
                    request.Timeout = HttpRequestTimeOut;
                    request.Accept = "application/json";
                    request.ContentType = "application/json";
                    request.ContentLength = postData.Length;

                if (postData.Length > 0)
                    {
                    request.GetRequestStream().Write(postData, 0, postData.Length);
                    }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Response.Code = response.StatusCode;

                    using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.Default))
                    {
                        Response.Data = stream.ReadToEnd();
                    }
                }
            }
                catch (Exception ex)
                {
                    Response.Message = ex.Message;
                }
                return Response;
            }


            /// <summary>
            /// Http 响应数据
            /// </summary>
            public class HttpResponseData
            {
                /// <summary>
                /// Http StatusCode
                /// </summary>
                public HttpStatusCode Code { get; set; }

                /// <summary>
                /// Response Data
                /// </summary>
                public String Data { get; set; }

                /// <summary>
                /// Error Message
                /// </summary>
                public String Message { get; set; }

            }
        }
    }
