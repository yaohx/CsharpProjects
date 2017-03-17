using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MB.Util;
using MB.Util.Model;
using MB.WinBase.Common.DynamicGroup;

namespace MB.WinClientDefault.DynamicGroup {
    /// <summary>
    /// 用于显示当前聚组条件的实体对象
    /// </summary>
    public class DynamicGroupUIColumns : System.ComponentModel.INotifyPropertyChanged {
        private string _COLUMN_NAME;  //聚组条件的列名，用于匹配聚合条件的KEY
        private string _COLUMN_DESCRIPTION;//描述
        private string _COLUMN_FIELD_TYPE;//列的类型
        private string _ENTITY_NAME;//所属对象
        private string _ENTITY_DESCRIPTION;//所属对象描述
        private DynamicGroupColArea _COL_AREA; //列领域
        private string _AGG_TYPE;//聚合类型
        private string _AGG_CONDITION_OPERATOR;//操作符号例如“>,=”，这个由用户设置
        private string _AGG_VALUE;//聚合值，这个由用户设置
        private bool _SELECTED; //是否被选中参数动态聚组


        

        public string COLUMN_NAME {
            get { return _COLUMN_NAME; }
            set { _COLUMN_NAME = value; this.RaisePropertyChanged("COLUMN_NAME"); }
        }

        public string COLUMN_DESCRIPTION {
            get { return _COLUMN_DESCRIPTION; }
            set { _COLUMN_DESCRIPTION = value; this.RaisePropertyChanged("COLUMN_DESCRIPTION"); }
        }

        public string COLUMN_FIELD_TYPE {
            get { return _COLUMN_FIELD_TYPE; }
            set { _COLUMN_FIELD_TYPE = value; this.RaisePropertyChanged("COLUMN_FIELD_TYPE"); }
        }

        public string ENTITY_NAME {
            get { return _ENTITY_NAME; }
            set { _ENTITY_NAME = value; this.RaisePropertyChanged("ENTITY_NAME"); }
        }

        public string ENTITY_DESCRIPTION {
            get { return _ENTITY_DESCRIPTION; }
            set { _ENTITY_DESCRIPTION = value; this.RaisePropertyChanged("ENTITY_DESCRIPTION"); }
        }

        public DynamicGroupColArea COL_AREA {
            get { return _COL_AREA; }
            set { _COL_AREA = value; this.RaisePropertyChanged("COL_AREA"); }
        }

        public string AGG_TYPE {
            get { return _AGG_TYPE; }
            set { _AGG_TYPE = value; this.RaisePropertyChanged("AGG_TYPE"); }
        }

        public string AGG_CONDITION_OPERATOR {
            get { return _AGG_CONDITION_OPERATOR; }
            set { _AGG_CONDITION_OPERATOR = value; this.RaisePropertyChanged("AGG_CONDITION_OPERATOR"); }
        }


        public string AGG_VALUE {
            get { return _AGG_VALUE; }
            set { _AGG_VALUE = value; this.RaisePropertyChanged("AGG_VALUE"); }
        }


        public bool SELECTED {
            get { return _SELECTED; }
            set { _SELECTED = value; this.RaisePropertyChanged("SELECTED"); }
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
