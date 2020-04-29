using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class error_config
    {
        public int id { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 标签编码id
        /// </summary>
        public int tag_type_sub_id { get; set; }
        /// <summary>
        /// 责任人员id
        /// </summary>
        public int response_person_id { get; set; }
        /// <summary>
        /// 功能是否激活
        /// </summary>
        public bool? alert_active { get; set; }
        /// <summary>
        /// 异常灯颜色
        /// </summary>
        public int? trigger_out_color { get; set; }

        /// <summary>
        /// 逻辑类型 0:安灯逻辑 1:自定义逻辑
        /// </summary>
        public int logic_type { get; set; }
        /// <summary>
        /// 安灯逻辑id
        /// </summary>
        public int andon_logic_id { get; set; }

    }

    public class config : error_config
    {
        public tag_type_sub type { get; set; }
        public Person response_person { get; set; }

        public machine machine { get; set; }

        public andon_logic andon_logic { get; set; }

        public tag_info tag { get; set; }
    }
}
