using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class machine_fault_alert
    {

        public int id { get; set; }
        public int error_type_detail_id { get; set; }
        /// <summary>
        /// 故障次数
        /// </summary>
        public int fault_times { get; set; }
        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }
       
    }
}
