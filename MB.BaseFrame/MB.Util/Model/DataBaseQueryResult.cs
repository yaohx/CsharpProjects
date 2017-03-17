using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Model {
    /// <summary>
    /// 数据库查询的结果
    /// </summary>
    public class DataBaseQueryResult {
        [ThreadStatic]
        private static DataBaseQueryResult _Instance;

        public static DataBaseQueryResult Instance {
            get {
                if (_Instance == null) {
                    _Instance = new DataBaseQueryResult();
                }
                return _Instance; }
        }

        private DataBaseQueryResult() { }

        private int _TotalRows;
        /// <summary>
        /// 数据库查询结果的总记录数
        /// </summary>
        public int TotalRows {
            get { return _TotalRows; }
        }

        /// <summary>
        /// 设定数据库返回的总记录数
        /// </summary>
        /// <param name="rows"></param>
        public void SetTotalRows(int rows) {
            _TotalRows = rows;
        }
    }
}
