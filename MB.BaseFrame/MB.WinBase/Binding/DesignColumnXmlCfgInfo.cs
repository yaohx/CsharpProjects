using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Binding {
    /// <summary>
    /// 设计阶段列的描述信息。
    /// </summary>
    public class DesignColumnXmlCfgInfo {
        private string _ColumnName;
        private string _ColumnDescription;

        /// <summary>
        /// 
        /// </summary>
        public DesignColumnXmlCfgInfo() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnDescription"></param>
        public DesignColumnXmlCfgInfo(string columnName,string columnDescription) {
            _ColumnName = columnName;
            _ColumnDescription = columnDescription;
        }
        public override string ToString() {
            return _ColumnDescription;
        }
        #region public 属性...
        /// <summary>
        /// 列的名称。
        /// </summary>
        public string ColumnName {
            get {
                return _ColumnName;
            }
            set {
                _ColumnName = value;
            }
        }
        /// <summary>
        /// 列的描述。
        /// </summary>
        public string ColumnDescription {
            get {
                return _ColumnDescription;
            }
            set {
                _ColumnDescription = value;
            }
        }
        #endregion public 属性...
    }
}
