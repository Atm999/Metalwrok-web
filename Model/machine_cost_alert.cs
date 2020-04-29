using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class machine_cost_alert
    {

        public int id { get; set; }
        public int machine_id { get; set; }
        /// <summary>
        /// 在月费用超支预警模式下为最大月费用 在余额不足预警模式下为最小余额数
        /// </summary>
        public double cost { get; set; }
        /// <summary>
        /// 预警模式 0:月费用超支预警 1:余额不足预警
        /// </summary>
        public int alert_mode { get; set; }
        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }
       
    }
    public class machine_cost_alertDto : machine_cost_alert
    {
        public machine machine { get; set; }
        public notification_group notice_group { get; set; }
    }
}
