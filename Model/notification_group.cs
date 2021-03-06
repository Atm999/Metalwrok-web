﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class notification_group
    {
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_en { get; set; }
        /// <summary>
        /// 
        /// </summary> 
        public string name_tw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_cn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        public int notification_group_id { get; set; }

        public class notification_groupPerson : notification_group
        {
            public IList<Person> person { get; set; }
        } 
    }
}
