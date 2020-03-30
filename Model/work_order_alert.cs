using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class work_order_alert
    {

        public int id { get; set; }
        public int virtual_line_id { get; set; }
        /// <summary>
        /// 异常类型 0 瓶颈站 1工单完成 2逾期未完成 3 超过标准工时
        /// </summary>
        public int alert_type { get; set; }
        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }
       
    }
    public class work_order_alertDto : work_order_alert
    {
        public virtual_line virtual_line { get; set; }
        public notification_group notice_group { get; set; }
    }
}
