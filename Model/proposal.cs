using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class proposal
    {
        public int id { set; get; }
        /// <summary>
        /// 逻辑名
        /// </summary>
        public int person_id { set; get; }
       
        public string title { get; set; }
        public string content { get; set; }
    }

   
}
