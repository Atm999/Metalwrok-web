using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class machine_status_alert
    {

        public int id { get; set; }
        public int machine_id { get; set; }
        public string machine_status { get; set; }
        public int notice_type { get; set; }
        public int notice_group_id { get; set; }
        public bool enable { get; set; }

    }
    public class machine_status_alertDto : machine_status_alert
    {
        public machine machine { get; set; }
        public notification_group notice_group { get; set; }
    }
}
