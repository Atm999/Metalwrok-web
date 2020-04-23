using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class tag_info
    {
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 二级标签的id
        /// </summary>
        public int tag_type_sub_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// SCADAID:Name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 临时变量，用来获取页面上的异常名称
        /// </summary>
        public string namecn { get; set; }
    }
}
