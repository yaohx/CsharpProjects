using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.WinBase.Auditing {
    
    /// <summary>
    /// 记录用户模块使用情况
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserActivity {

        public UserActivity() { 
        }

        public UserActivity(string moduleName, string moduleCode) {
            _MODULE_NAME = moduleName;
            _MODULE_CODE = moduleCode;
            _MODULE_ACCESS_COUNT = 1;
        }

        private string _USER_ID;
        [DataMember]
        public string USER_ID {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }

        private string _USER_NAME;
        [DataMember]
        public string USER_NAME {
            get { return _USER_NAME; }
            set { _USER_NAME = value; }
        }

        private string _USER_CODE;
        [DataMember]
        public string USER_CODE {
            get { return _USER_CODE; }
            set { _USER_CODE = value; }
        }

        private string _MODULE_NAME;
        [DataMember]
        public string MODULE_NAME {
            get { return _MODULE_NAME; }
            set { _MODULE_NAME = value; }
        }

        private string _MODULE_CODE;
        [DataMember]
        public string MODULE_CODE {
            get { return _MODULE_CODE; }
            set { _MODULE_CODE = value; }
        }

        private int _MODULE_ACCESS_COUNT;
        [DataMember]
        public int MODULE_ACCESS_COUNT {
            get { return _MODULE_ACCESS_COUNT; }
            set { _MODULE_ACCESS_COUNT = value; }
        }
        private DateTime _ACCESS_DATE;
        [DataMember]
        public DateTime ACCESS_DATE {
            get { return _ACCESS_DATE; }
            set { _ACCESS_DATE = value; }
        }
        private DateTime _FIRST_ACCESS_TIME;
        [DataMember]
        public DateTime FIRST_ACCESS_TIME {
            get { return _FIRST_ACCESS_TIME; }
            set { _FIRST_ACCESS_TIME = value; }
        }

        private DateTime _LAST_ACCESS_TIME;
        [DataMember]
        public DateTime LAST_ACCESS_TIME {
            get { return _LAST_ACCESS_TIME; }
            set { _LAST_ACCESS_TIME = value; }
        } 
    }
}
