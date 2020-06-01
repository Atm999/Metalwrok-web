using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class virtual_line_log
    {
        /// <summary>
        /// 虚拟线id
        /// </summary>
        public int virtual_line_id { get; set; }
        /// <summary>
        /// 工单配置id
        /// </summary>
        public int wo_config_id { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? start_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? end_time { get; set; }
        /// <summary>
        /// 平衡率
        /// </summary>
        public decimal balance_rate { get; set; }
        /// <summary>
        /// 生产率
        /// </summary>
        public decimal productivity { get; set; }

        /// <summary>
        /// 标准数量
        /// </summary>
        public decimal quantity { set; get; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal bad_quantity { set; get; }
    }
}
