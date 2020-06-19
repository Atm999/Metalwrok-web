using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Model
{
    public class standard 
    {
        public int id { get; set; }
        /// <summary>
        /// 标签id
        /// </summary>
        public int? tag_id { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public int?  tag_type_sub_id { get; set; }
        public int? notice_logic_id { get; set; }
        /// <summary>
        /// 正常范围最小值
        /// </summary>
        public double? normal_min { get; set; }
        /// <summary>
        /// 正常范围最大值
        /// </summary>
        public double? normal_max { get; set; }
        /// <summary>
        /// 异常范围最小值
        /// </summary>
        public double? abnormal_min { get; set; }
        /// <summary>
        /// 异常范围最大值
        /// </summary>
        public double? abnormal_max { get; set; }
        /// <summary>
        /// 严重范围最小值
        /// </summary>
        public double? serious_min { get; set; }
        /// <summary>
        /// 严重范围最大值
        /// </summary>
        public double? serious_max { get; set; }
    }

    public class env_standard : standard
    {
        public tag_type_sub tag_type_sub { get; set; }
        public area_node area_node { get; set; }
        public area_layer area_layer { get; set; }
        public machine machine { get; set; }
        public tag_info tag_info { get; set; }
        public notice_logic notice_logic { get; set; }
    }
}