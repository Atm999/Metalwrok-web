﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public  class schedule
    {
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        public Boolean enable { get; set; }

        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}