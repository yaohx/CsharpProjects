//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MB.Util.Comparer {
//    /// <summary>
//    /// 字符窜比较。
//    /// </summary>
//    public class StringEqualityComparer : IEqualityComparer<string> {
//        private bool _IgnoreCase;
//        /// <summary>
//        /// 创建一个字符窜比较器。
//        /// </summary>
//        public StringEqualityComparer() {
//        }
//        /// <summary>
//        /// 创建一个字符窜比较器。
//        /// </summary>
//        /// <param name="ignoreCase">判断是否在比较时忽略大小写</param>
//        public StringEqualityComparer(bool ignoreCase) {
//            _IgnoreCase = ignoreCase;
//        }

//        #region IEqualityComparer<string> 成员

//        public bool Equals(string x, string y) {
//            return string.Compare(x, y, _IgnoreCase) == 0;
//        }

//        public int GetHashCode(string obj) {
//            return obj.GetHashCode();
//        }

//        #endregion
//    }
//}


