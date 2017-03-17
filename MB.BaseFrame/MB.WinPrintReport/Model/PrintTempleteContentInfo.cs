using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;  

namespace MB.WinPrintReport.Model {
    /// <summary>
    /// 打印模板描述信息。
    /// </summary>
    public class PrintTempleteContentInfo  {

        private System.Guid _GID;
        private string _Name;
        private string _DataSource;
        private string _Remark;
        private string _TempleteXmlContent;
        private List<PrintTempleteContentInfo> _Childs;

        #region 构造函数...
        /// <summary>
        /// 打印模板描述信息。
        /// </summary>
        public PrintTempleteContentInfo() {
            _Childs = new List<PrintTempleteContentInfo>();
        }
        /// <summary>
        ///  打印模板描述信息。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="templeteXml"></param>
        public PrintTempleteContentInfo(System.Guid id,string name,string templeteXml) : this() {
            _GID = id;
            _Name = name;
            _TempleteXmlContent = templeteXml;
        }
        #endregion 构造函数...

        #region public 属性...
        /// <summary>
        /// 模板ID.
        /// </summary>
        [DataMember]   
        public System.Guid GID {
            get { return _GID; }
            set { _GID = value; }
        }
        /// <summary>
        /// 模板名称。
        /// </summary>
        [DataMember]   
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        /// <summary>
        /// 报表需要的数据源名称
        /// </summary>
        [DataMember]   
        public string DataSource {
            get {
                return _DataSource;
            }
            set {
                _DataSource = value;
            }
        }
        /// <summary>
        /// 备注。
        /// </summary>
        [DataMember]   
        public string Remark {
            get { return _Remark; }
            set { _Remark = value; }
        }
        /// <summary>
        /// 打印模板内容系列化的XML 字符窜。
        /// </summary>
        [DataMember]   
        public string TempleteXmlContent {
            get { return _TempleteXmlContent; }
            set {
                  _TempleteXmlContent = value;
            }
        }                            
        /// <summary>
        /// 子报表。
        /// </summary>
        [MB.Util.Serializer.ValueXmlSerializer(ValueType = "MB.WinPrintReport.Model.PrintTempleteContentInfo,MB.WinPrintReport")]   
        [DataMember]  
        public List<PrintTempleteContentInfo> Childs {
            get {
                return _Childs;
            }
            set {
                _Childs = value;
            }
        }

        #endregion public 属性...
    }
}
