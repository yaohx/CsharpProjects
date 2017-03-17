using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MB.Util;
using MB.Util.Model;

namespace MB.WinClientDefault.DynamicGroup {
    /// <summary>
    /// 用于显示当前聚组条件的实体对象
    /// </summary>
    public class DynamicGroupCondition : System.ComponentModel.INotifyPropertyChanged {
        private string _GROUP_NAME;  //聚组条件的列名，用于匹配聚合条件的KEY
        private string _GROUP_DESCRIPTION;//描述
        private string _GROUP_FIELD_TYPE;//列的类型
        private string _ENTITY_NAME;//所属对象
        private string _ENTITY_DESCRIPTION;//所属对象描述
        private string _SUMMARY_TYPE;//聚合类型
        private string _GROUP_CONDITION_OPERATOR;//操作符号例如“>,=”，这个由用户设置
        private string _GROUP_VALUE;//聚合值，这个由用户设置
        

        public string GROUP_NAME {
            get { return _GROUP_NAME; }
            set { _GROUP_NAME = value; this.RaisePropertyChanged("GROUP_NAME"); }
        }

        public string GROUP_DESCRIPTION {
            get { return _GROUP_DESCRIPTION; }
            set { _GROUP_DESCRIPTION = value; this.RaisePropertyChanged("GROUP_DESCRIPTION"); }
        }

        public string GROUP_FIELD_TYPE {
            get { return _GROUP_FIELD_TYPE; }
            set { _GROUP_FIELD_TYPE = value; this.RaisePropertyChanged("GROUP_FIELD_TYPE"); }
        }

        public string ENTITY_NAME {
            get { return _ENTITY_NAME; }
            set { _ENTITY_NAME = value; this.RaisePropertyChanged("ENTITY_NAME"); }
        }

        public string ENTITY_DESCRIPTION {
            get { return _ENTITY_DESCRIPTION; }
            set { _ENTITY_DESCRIPTION = value; this.RaisePropertyChanged("ENTITY_DESCRIPTION"); }
        }

        public string SUMMARY_TYPE {
            get { return _SUMMARY_TYPE; }
            set { _SUMMARY_TYPE = value; this.RaisePropertyChanged("SUMMARY_TYPE"); }
        }

        public string GROUP_CONDITION_OPERATOR {
            get { return _GROUP_CONDITION_OPERATOR; }
            set { _GROUP_CONDITION_OPERATOR = value; this.RaisePropertyChanged("GROUP_CONDITION_OPERATOR"); }
        }


        public string GROUP_VALUE {
            get { return _GROUP_VALUE; }
            set { _GROUP_VALUE = value; this.RaisePropertyChanged("GROUP_VALUE"); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 得到聚组条件操作符
        /// </summary>
        /// <param name="itemType">参数是表明当前聚合字段的数据类型，如String,Int等</param>
        /// <returns></returns>
        public DataSet GetOperators(string itemType) {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            foreach (DynamicGroupConditionOperator condition in Enum.GetValues(typeof(DynamicGroupConditionOperator))) {
                DataRow dr = dt.NewRow();
                dr["ID"] = condition.ToString();
                dr["NAME"] = MyCustomAttributeLib.Instance.GetFieldDesc(typeof(DynamicGroupConditionOperator), condition.ToString(), false);
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }


        /// <summary>
        /// 得到可选的聚合类型
        /// </summary>
        /// <param name="itemType">参数是表明当前聚合字段的数据类型，如String,Int等</param>
        /// <returns></returns>
        public DataSet GetSummaryItems(string itemType) {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            foreach (Util.Model.SummaryItemType summaryItemType in Enum.GetValues(typeof(Util.Model.SummaryItemType))) {
                if (summaryItemType == Util.Model.SummaryItemType.None)
                    continue;

                DataRow dr = dt.NewRow();
                string summaryValue = summaryItemType.ToString();
                string summaryName = MyCustomAttributeLib.Instance.GetFieldDesc(typeof(Util.Model.SummaryItemType), summaryItemType.ToString(), false);
                dr["ID"] = summaryValue;
                dr["NAME"] = summaryValue + string.Format("({0})", summaryName);
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }
    
    }
}
