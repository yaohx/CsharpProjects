using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.WinClientDefault.DataImport {
    /// <summary>
    /// 需要导入的数据描述信息。
    /// </summary>
    public class DataImportInfo {
        private DataSet _ImportData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsImportData"></param>
        public DataImportInfo(DataSet dsImportData) {
            _ImportData = dsImportData;
        }
        /// <summary>
        /// 需要导入的数据。
        /// </summary>
        public DataSet ImportData {
            get {
                return _ImportData;
            }
            set {
                _ImportData = value;
            }
        }
    }
}
