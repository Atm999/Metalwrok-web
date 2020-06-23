using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class leave_statistics
    {
        public int id { get; set; }
        public int person_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public double duration { get; set; }
    }
}
