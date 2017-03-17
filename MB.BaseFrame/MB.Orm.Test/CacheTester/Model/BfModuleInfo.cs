using System.Runtime.Serialization;
using System;

namespace MB.Orm.Test
{
    /// <summary> 
    /// 系统模块类
    /// </summary> ]
    public class BfModuleInfo : MB.Orm.Common.BaseModel
    {

        public BfModuleInfo()
        {

        }
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _NAME;

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        private string _CODE;

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
       

    }
}