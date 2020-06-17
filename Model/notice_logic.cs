using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class notice_logic
    {
        public int id { set; get; }
        /// <summary>
        /// 逻辑名
        /// </summary>
        public string name { set; get; }
        public int normal_notification_group_id { get; set; }
        /// <summary>
        /// 二级通知人员id
        /// </summary>
        public int abnormal_notification_group_id { get; set; }
        /// <summary>
        /// 三级通知人员id
        /// </summary>
        public int serious_notification_group_id { get; set; }
        /// <summary>
        /// 预警形式，邮件  微信
        /// </summary>
        public int notice_type { get; set; }
    }

    public class notice_logics : notice_logic
    {
        public notification_group normal_notification_group { get; set; } 
        public notification_group abnormal_notification_group { get; set; }
        public notification_group serious_notification_group { get; set; } 

    }
   
}
