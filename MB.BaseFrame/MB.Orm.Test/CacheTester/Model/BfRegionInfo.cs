using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace MB.Orm.Test
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2010-08-18 01:25 
    /// </summary> 
    public class BfRegionInfo : MB.Orm.Common.BaseModel
    {

        public BfRegionInfo()
        {

        }
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _CODE;

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        private string _NAME;

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        
    } 


}
