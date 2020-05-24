using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class wo_config
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string work_order { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string part_num { get; set; }
        /// <summary>
        /// 班别
        /// </summary>
        public int shift { get; set; }
        /// <summary>
        /// 标准数量
        /// </summary>
        public int standard_num { get; set; }
        /// <summary>
        /// 是否自动完结
        /// </summary>
        public bool auto { get; set; }
        /// <summary>
        /// 执行序号
        /// </summary>
        public int order_index { get; set; }
        /// <summary>
        /// 状态 0:创建 1:排产 2:执行中 3:完成
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 标准时间 
        /// </summary>
        public string standard_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int virtual_line_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
       public DateTime? create_time { get; set; }
        public string lbr_formula { get; set; }
        public class woVirtualLine : wo_config
        {
            public IList<virtual_line> virtual_Line { get; set; }
        } 
        public class woMachinecurlog: wo_config
        {
            public IList<wo_machine_cur_log> Wo_machine_cur_log { get; set; } 
        }

        public class woMachine : wo_config
        {
            public IList<machine> Machines { get; set; }
        }

    }

    public class wo_config_detail : wo_config
    {
        /// <summary>
        /// 虚拟线信息
        /// </summary>
        public virtual_line virtual_Line { set; get; }

    }

    public class wo_config_excute : wo_config_detail
    {
        /// <summary>
        /// 虚拟线日志信息
        /// </summary>
        public virtual_line_log virtual_Line_log { set; get; }
    }
}
