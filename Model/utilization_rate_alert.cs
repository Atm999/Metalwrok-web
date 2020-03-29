using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class utilization_rate_alert
    {

        public int id { get; set; }
        public int machine_id { get; set; }
        /// <summary>
        /// 稼动率类别 0:日稼动率 1:班稼动率 2:工单稼动率
        /// </summary>
        public int utilization_rate_type { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int maximum { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public int minimum { get; set; }

        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }
       
    }
    public class utilization_rate_alertDto : utilization_rate_alert
    {
        public machine machine { get; set; }
        public notification_group notice_group { get; set; }
    }
}
