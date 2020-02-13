using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class area_property
    {
        public int id { get; set; }
        public string name_cn { get; set; }
        public string name_en { get; set; }
        public string name_tw { get; set; }
        public string description { get; set; }
        public string format { get; set; }
        public int area_node_id { get; set; }

        public Shift shift { get; set; } //排班格式
        public FixBreak fixBreak { get; set; }//固定排休和非固定排休格式

        public class Day {
            public string start { get; set; }
            public string end { get; set; }
        }
        public class Shift
        {
            public Day day { get; set; }
            public Day night { get; set; }
        }
        public class FixBreak
        {
            public List<Day> rest { get; set; }
        }
    }
}
