using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class overtime_statistics
    {
        public int id { set; get; }
       
        /// <summary>
        ///
        /// </summary>
        public int person_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double duration { get; set; }

        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}
