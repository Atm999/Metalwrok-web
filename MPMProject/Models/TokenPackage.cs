using System;
using System.Collections.Generic;
using System.Text;

namespace MPMProject.Models
{
    public class TokenPackage
    {
        //请求此post_https://portal-sso.wise-paas.com/v2.0/tokenvalidation  
        //同时传递的参数为token后返回以下字段（token验证）
        public string tokenType { get; set; }
        public string accessToken { get; set; }
        public Int64 expiresIn { get; set; }
        public string refreshToken { get; set; }
    }
}
