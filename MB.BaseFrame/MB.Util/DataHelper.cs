using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// 数据加工处理帮助类
    /// </summary>
    public class DataHelper {

        #region Instance...
        private static object _Object = new object();
        private static DataHelper _Instance;

        protected DataHelper() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static DataHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new DataHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 获取多个字段的值。
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public string[] GetMultiFieldValue(DataRow drData,string[] fields) {
            List<string> vals = new List<string>();
            for (int i = 0; i < fields.Length; i++) {
                if (!drData.Table.Columns.Contains(fields[i]))
                    throw new MB.Util.APPException(string.Format("GetMultiFieldValue 时，数据结构中不包含字段{0}", fields[i]));
                if (drData[fields[i]] == System.DBNull.Value)
                    vals.Add(string.Empty);
                else
                    vals.Add(drData[fields[i]].ToString());
            }
            return vals.ToArray(); 
        }
    }
}
