using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
  public  class wechart_server
    {
        public int id { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string apply_name { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string corp_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apply_agentid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apply_secret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? create_time { get; set; }
    }
}
