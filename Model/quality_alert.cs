using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class quality_alert
    {

        public int id { get; set; }
        public int work_order_id { get; set; }
        /// <summary>
        /// 不良数量预警值
        /// </summary>
        public int defective_number { get; set; }
        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }
       
    }
}
