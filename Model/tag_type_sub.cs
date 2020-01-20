using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public  class tag_type_sub
    {
        public int id { get; set; }
        public string name_cn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_en { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string name_tw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tag_type_id { get; set; }
    }
}
