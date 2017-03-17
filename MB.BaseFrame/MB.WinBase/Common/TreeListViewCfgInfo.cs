using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.XmlConfig; 

namespace MB.WinBase.Common {
    /// <summary>
    /// 树型控件绑定配置处理类。
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = false)] 
    public class TreeListViewCfgInfo {
        private string _KeyFieldName;
        /// <summary>
        /// 创建树型控件的键值名称。
        /// </summary>
        [PropertyXmlConfig]  
        public string KeyFieldName {
            get { return _KeyFieldName; }
            set { _KeyFieldName = value; }
        }
        private string _DisplayFieldName;
        /// <summary>
        /// 显示节点的字段名称。
        /// </summary>
        [PropertyXmlConfig]  
        public string DisplayFieldName {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        private string _ParentFieldName;
        /// <summary>
        /// 创建树型控件关联父节点的字段名称。
        /// </summary>
        [PropertyXmlConfig]  
        public string ParentFieldName {
            get { return _ParentFieldName; }
            set { _ParentFieldName = value; }
        }
        private string _OrderFieldName;
         /// <summary>
         /// 节点的排序字段名称。
         /// </summary>
        [PropertyXmlConfig]  
        public string OrderFieldName {
            get { return _OrderFieldName; }
            set { _OrderFieldName = value; }
        }
        private string _IcoFieldName;
        /// <summary>
        /// 图标的字段名称。
        /// </summary>
        [PropertyXmlConfig]  
        public string IcoFieldName {
            get {
                return _IcoFieldName;
            }
            set {
                _IcoFieldName = value;
            }
        }
        private bool _OnlyLeafNodeSelectable;
        /// <summary>
        /// 是否只有叶子节点才能被选中
        /// </summary>
        [PropertyXmlConfig]
        public bool OnlyLeafNodeSelectable {
            get { return _OnlyLeafNodeSelectable; }
            set { _OnlyLeafNodeSelectable = value; }
        }



    }
}
