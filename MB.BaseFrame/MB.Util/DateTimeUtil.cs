using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util {
    public class DateTimeUtil {
        
        #region Instance...
        private static object _Object = new object();
        private static DateTimeUtil _Instance;

        protected DateTimeUtil() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static DateTimeUtil Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new DateTimeUtil();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 今天的起始和结束
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public void GetToday(out DateTime begin, out DateTime end) {
            DateTime dtToday = DateTime.Now.Date;
            begin = dtToday;
            end = dtToday.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// 得到某一天的起始和结束
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public void GetDay(DateTime dt, out DateTime begin, out DateTime end) {
            DateTime dtToday = dt.Date;
            begin = dtToday;
            end = dtToday.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// 得到本周的起始和结束
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public void GetWeek(out DateTime begin, out DateTime end) {
            DateTime dtToday = DateTime.Now.Date;
            begin = dtToday.AddDays(Convert.ToDouble((0 - Convert.ToInt16(dtToday.DayOfWeek))));
            end = dtToday.AddDays(Convert.ToDouble((6 - Convert.ToInt16(dtToday.DayOfWeek)))).AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// 得到本月的起始和结束
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public void GetMonth(out DateTime begin, out DateTime end) {
            DateTime dtToday = DateTime.Now.Date;
            begin = dtToday.AddDays(1 - dtToday.Day);
            end = dtToday.AddDays(1 - dtToday.Day).AddMonths(1).AddDays(-1).AddDays(1).AddMilliseconds(-1);
        }

    }
}
