using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
 public class wo_machine_cur_log
    {
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int standard_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 平均C/T
        /// </summary>
        public double cycle_time_average { get; set; }
        /// <summary>
        /// 当前C/T
        /// </summary>
        public double cycle_time { get; set; }
        /// <summary>
        /// 生产率
        /// </summary>
        public double productivity { get; set; }
        /// <summary>
        /// 达成率
        /// </summary>
        public double achieving_rate { get; set; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public int bad_quantity { get; set; }
        /// <summary>
        /// 当前数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime start_time { get; set; }
        /// <summary>
        /// 标准数量
        /// </summary>
        public double standard_time { get; set; }
        /// <summary>
        /// 工单id
        /// </summary>
        public int wo_config_id { get; set; }

        public int work_order_id { get; set; }

    }
}
