using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class tag_info_extra
    {
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tag_type_sub_id { get; set; }
        /// <summary>
        /// 目标类型 0:machine_id 1:node_id 2:virtual_line_id
        /// </summary>
        public int target_type { get; set; }
        /// <summary>
        /// 目标id
        /// </summary>
        public int target_id { get; set; } 

        /// <summary>
        /// SCADAID:Name
        /// </summary>
        public string name { get; set; }
         
        public int utilization_rate_types { get; set; }
    }
}
