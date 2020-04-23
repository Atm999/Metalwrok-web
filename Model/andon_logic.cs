using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class andon_logic
    {
        public int id { set; get; }
        /// <summary>
        /// 逻辑名
        /// </summary>
        public string name { set; get; }
        public int level1_notification_group_id { get; set; }
        /// <summary>
        /// 二级通知人员id
        /// </summary>
        public int level2_notification_group_id { get; set; }
        /// <summary>
        /// 三级通知人员id
        /// </summary>
        public int level3_notification_group_id { get; set; }
        /// <summary>
        /// 超时设置
        /// </summary>
        public int timeout_setting { get; set; }
        /// <summary>
        /// 预警形式，邮件  微信
        /// </summary>
        public int notice_type { get; set; }
    }
}
