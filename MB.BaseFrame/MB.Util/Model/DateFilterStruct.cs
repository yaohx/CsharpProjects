using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Model {
    /// <summary>
    /// 日期类型。
    /// </summary>
    public struct DateFilterStruct {
        private DateTime _BeginDate;
        private DateTime _EndDate;


        public DateFilterStruct(DateTime beginDate, DateTime endDate) {
            _BeginDate = beginDate;
            _EndDate = endDate;
        }
        /// <summary>
        /// 开始时间。
        /// </summary>
        public DateTime BeginDate {
            get {
                return _BeginDate;
            }
            set {
                _BeginDate = value;
            }
        }
        /// <summary>
        /// 结束日期。
        /// </summary>
        public DateTime EndDate {
            get {
                return _EndDate;
            }
            set {
                _EndDate = value;
            }
        }

    }
}
