using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;
using System.IO;

namespace MB.WinBase.Common {
    /// <summary>
    /// 过滤条件配置元素集合。
    /// </summary>
    [XmlSchemaProvider("GetSchema")]
    public class FilterElementCfgs : Dictionary<string, DataFilterElementCfgInfo>,IXmlSerializable {
        private string _Name;
        private bool _AllowEmptyFilter;
        /// <summary>
        /// 
        /// </summary>
        public FilterElementCfgs() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lstData"></param>
        public FilterElementCfgs(string name,Dictionary<string, DataFilterElementCfgInfo> lstData) {
            _Name = name;
            foreach (string key in lstData.Keys) {
                this.Add(key, lstData[key]);
            }
        }


        /// <summary>
        /// 查询带名称。
        /// </summary>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
        /// 在条件为空的情况下是否允许进行查询。
        /// </summary>
        public bool AllowEmptyFilter {
            get {
                return _AllowEmptyFilter;
            }
            set {
                _AllowEmptyFilter = value;
            }
        }

        /// <summary>
        /// 允许查询的时候不分页,直接查询出所有数据库内容
        /// </summary>
        public bool AllowQueryAll { get; set; }

        /// <summary>
        /// 设置Filter查询框的长度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 设置Filter查询框的宽度
        /// </summary>
        public int Height { get; set; }

        


        public static XmlQualifiedName GetSchema(XmlSchemaSet xs)
        {
            XmlSerializer schemaSerializer = new XmlSerializer(typeof(XmlSchema));

            Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
            Stream sm = asm.GetManifestResourceStream("MB.WinBase.Common.FilterElementCfgs.xsd");
            var reader = new XmlTextReader(sm);
          
            XmlSchema s = (XmlSchema)schemaSerializer.Deserialize(
               reader, null);
            xs.XmlResolver = new XmlUrlResolver();
            xs.Add(s);

            return new XmlQualifiedName("FilterElementCfgs", "http://www.w3.org/2001/XMLSchema");
        }

        #region IXmlSerializable 接口 目前是为了生成Schema,以后需要实现序列化再补上.XiaoMin

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            XmlSerializer schemaSerializer = new XmlSerializer(typeof(XmlSchema));

            Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
            Stream sm = asm.GetManifestResourceStream("MB.WinBase.Common.FilterElementCfgs.xsd");
            var reader = new XmlTextReader(sm);

            XmlSchema s = (XmlSchema)schemaSerializer.Deserialize(
               reader, null);

            return s;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
