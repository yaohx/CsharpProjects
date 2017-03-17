using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary>
    /// 动态聚组聚合字段
    /// </summary>
    [DataContract]
    public class DataAreaField {
        private string _EntityName;
        private string _EntityDescription;
        private string _Name;
        private string _Description; //只在客户端用，不需要序列化
        private string _DataType; //只在客户端用，不需要序列化
        private SummaryItemType _SummaryItemType;
        private DynamicGroupConditionOperator _ConditionOperator;
        private string _ConditionValue;

        [DataMember]
        public string EntityName {
            get { return _EntityName; }
            set { _EntityName = value; }
        }
        [DataMember]
        public string EntityDescription {
            get { return _EntityDescription; }
            set { _EntityDescription = value; }
        }

        [DataMember]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        [DataMember]
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }
        [DataMember]
        public string DataType {
            get { return _DataType; }
            set { _DataType = value; }
        }

        [DataMember]
        public SummaryItemType SummaryItemType {
            get { return _SummaryItemType; }
            set { _SummaryItemType = value; }
        }

        [DataMember]
        public DynamicGroupConditionOperator ConditionOperator {
            get { return _ConditionOperator; }
            set { _ConditionOperator = value; }
        }

        [DataMember]
        public string ConditionValue {
            get { return _ConditionValue; }
            set { _ConditionValue = value; }
        }

    }
}
