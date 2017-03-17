using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MB.Util {
    /// <summary>
    /// 正则表达式的帮助类
    /// </summary>
    public class RegularExpressionsHelper {
        private static Object _Obj = new object();
        private static RegularExpressionsHelper _Instance;

        public static RegularExpressionsHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new RegularExpressionsHelper();
                    }
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 验证是不是纯数字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool IsDigital(string source) {
            string pattern = @"^[0-9]+$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(source);
        }
    }
}
