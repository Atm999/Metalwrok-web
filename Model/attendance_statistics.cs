using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class attendance_statistics
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int shift { get; set; }
        public int person_id { get; set; }
        public Boolean is_attend { get; set; }
    }
}
