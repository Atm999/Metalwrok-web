using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class person_shift
    {
        public int id { get; set; }
        public int person_id { get; set; }
       
        public int schedule_id { get; set; }
        public int machine_id { get; set; }
        public int shift { get; set; }


    }
}
