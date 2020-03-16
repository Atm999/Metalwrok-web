using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SimpleJson
    {
        /// <summary>
        /// 时间序列类型
        /// </summary>
        public class TimeSeries
        {
            public string target { get; set; }

            public List<List<double>> datapoints { get; set; }

        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public double GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            double timeStamp = ((dt - dateStart).TotalSeconds) * 1000;
            return timeStamp;
        }
    }
}
