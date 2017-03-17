using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model.Chart
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2012-12-26 02:50 
    /// </summary> 
    [DataContract]
    public class ChartLayoutItemInfo
    {
        private int _ID;
        [DataMember]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _LT_ID;
        [DataMember]
        public int LT_ID
        {
            get { return _LT_ID; }
            set { _LT_ID = value; }
        }
        private int _CT_ID;
        [DataMember]
        public int CT_ID
        {
            get { return _CT_ID; }
            set { _CT_ID = value; }
        }
        private string _NAME;
        [DataMember]
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        private string _TEXT;
        [DataMember]
        public string TEXT
        {
            get { return _TEXT; }
            set { _TEXT = value; }
        }
        private string _FORM_TEXT;
        [DataMember]
        public string FORM_TEXT
        {
            get { return _FORM_TEXT; }
            set { _FORM_TEXT = value; }
        }
        private string _ITEM_TYPE;
        [DataMember]
        public string ITEM_TYPE
        {
            get { return _ITEM_TYPE; }
            set { _ITEM_TYPE = value; }
        }
    } 

}
