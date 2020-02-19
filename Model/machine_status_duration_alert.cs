using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class machine_status_duration_alert
    {

        public int id { get; set; }
        public int machine_id { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public string machine_status { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public double duration { get; set; }
      
        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }
       
    }
}
