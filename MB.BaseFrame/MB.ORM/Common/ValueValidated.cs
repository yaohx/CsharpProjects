using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Common {
    /// <summary>
    /// 数据值有效性验证。
    /// </summary>
    public sealed class ValueValidated {
        #region Instance...
        private static Object _Obj = new object();
        private static ValueValidated _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected ValueValidated() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static ValueValidated Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new ValueValidated();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...


        /// <summary>
        /// 判断整行的数据是否都为空。在新增的时候，如果是空行，将忽略不进行处理。
        /// </summary>
        /// <param name="drData"></param>
        /// <returns></returns>
        public bool RowIsNull(DataRow drData) {
            object[] vals = drData.ItemArray;
            foreach (object objVal in vals) {
                if (objVal != System.DBNull.Value && objVal != null) {
                    return false;
                }
            }
            return true;
        }
    }
}
