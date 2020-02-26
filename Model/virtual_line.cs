using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class virtual_line
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

        public class virtual_lineMachine : virtual_line
        {
            public IList<machine> Machines { get; set; }
        }
    }
}
