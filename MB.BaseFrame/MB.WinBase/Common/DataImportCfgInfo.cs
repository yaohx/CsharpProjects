using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Util.XmlConfig;
using System.Xml.Serialization; 
namespace MB.WinBase.Common {
    /// <summary>
    /// 数据导入相应的配置信息。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = false)] 
    public class DataImportCfgInfo {
        private DataImportOperate _Operate;
        private string _OverideKeys = string.Empty;
        private string _OverideFields = string.Empty;

        /// <summary>
        /// 数据导入处理操作方式。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public DataImportOperate Operate {
            get {
                return _Operate;
            }
            set {
                _Operate = value;
            }
        }
        /// <summary>
        ///  在进行数据覆盖导入时的键值。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string OverideKeys {
            get {
                return _OverideKeys;
            }
            set {
                _OverideKeys = value;
            }
        }
        /// <summary>
        /// 进行覆盖的字段。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string OverideFields {
            get {
                return _OverideFields;
            }
            set {
                _OverideFields = value;
            }
        }
    }

    #region DataImportOperate...
    /// <summary>
    /// 数据导入处理操作方式。
    /// </summary>
    public enum DataImportOperate {
        /// <summary>
        /// 以新增的方式进行导入。
        /// </summary>
        AddNew,
        /// <summary>
        /// 以覆盖的方式进行导入
        /// </summary>
        Overide,
        /// <summary>
        /// 允许新增和覆盖的方式进行导入。
        /// </summary>
        AddAndOveride
    }
    #endregion DataImportOperate...
}
