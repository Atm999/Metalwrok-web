﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class area_node
    {
        public int id { get; set; }
        public string name_cn { get; set; }
        public string name_en { get; set; }
        public string name_tw { get; set; }
        public string description { get; set; }
        public int upper_id { get; set; }
        public int area_layer_id { get; set; }
    }
}