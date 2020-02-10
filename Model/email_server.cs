using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class email_server
    {
        public int id { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_name { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }
    }
}
