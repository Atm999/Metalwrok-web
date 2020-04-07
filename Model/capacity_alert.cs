using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class capacity_alert
    {

        public int id { get; set; }
        public DateTime? date { get; set; }
        /// <summary>
        /// 月标准产能
        /// </summary>
        public double capacity { get; set; }
        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }
       
    }
    public class capacity_alertDto : capacity_alert
    {
        public notification_group notice_group { get; set; }
    }
}
