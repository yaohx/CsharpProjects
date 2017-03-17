using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Orm.Test
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2009-12-18 09:24 
    /// </summary> 
    public class BfCodeInfo : MB.Orm.Common.BaseModel
    {

        public BfCodeInfo() {

        }
        private int _ID;

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _CODE;

        public string CODE {
            get { return _CODE; }
            set { _CODE = value; }
        }
        private string _DESCRIPTION;

        public string DESCRIPTION {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
       
    } 

}
