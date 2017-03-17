using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace WinTestProject {
    public partial class testEmit : Form {
        public testEmit() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            //
            DataSet ds = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("MbfsFuc", "SelectObject");

   
        }

        private void button2_Click(object sender, EventArgs e) {
            List<MbfsFucInfo> lst = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<MbfsFucInfo>("MbfsFuc", "SelectObject");
        }
    }

    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2009-09-24 10:57 
    /// </summary> 
    [MB.Orm.Mapping.Att.ModelMap("MBFS_FUC", "MbfsFuc", new string[] { "ID" })]
    [KnownType(typeof(MbfsFucInfo))]
    public class MbfsFucInfo : MB.Orm.Common.BaseModel{

        public MbfsFucInfo() {

        }
        private int _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _FUC_NUM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FUC_NUM", System.Data.DbType.String)]
        public string FUC_NUM {
            get { return _FUC_NUM; }
            set { _FUC_NUM = value; }
        }
        private int _MBFS_SUBJECT_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MBFS_SUBJECT_ID", System.Data.DbType.Int32)]
        public int MBFS_SUBJECT_ID {
            get { return _MBFS_SUBJECT_ID; }
            set { _MBFS_SUBJECT_ID = value; }
        }
        private string _MBFS_SUBJECT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MBFS_SUBJECT_CODE", System.Data.DbType.String)]
        public string MBFS_SUBJECT_CODE {
            get { return _MBFS_SUBJECT_CODE; }
            set { _MBFS_SUBJECT_CODE = value; }
        }
        private string _MBFS_SUBJECT_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MBFS_SUBJECT_NAME", System.Data.DbType.String)]
        public string MBFS_SUBJECT_NAME {
            get { return _MBFS_SUBJECT_NAME; }
            set { _MBFS_SUBJECT_NAME = value; }
        }
        private string _VENDER_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDER_ID", System.Data.DbType.String)]
        public string VENDER_ID {
            get { return _VENDER_ID; }
            set { _VENDER_ID = value; }
        }
        private string _VENDER_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDER_NAME", System.Data.DbType.String)]
        public string VENDER_NAME {
            get { return _VENDER_NAME; }
            set { _VENDER_NAME = value; }
        }
        private string _VENDEE_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDEE_ID", System.Data.DbType.String)]
        public string VENDEE_ID {
            get { return _VENDEE_ID; }
            set { _VENDEE_ID = value; }
        }
        private string _VENDEE_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDEE_NAME", System.Data.DbType.String)]
        public string VENDEE_NAME {
            get { return _VENDEE_NAME; }
            set { _VENDEE_NAME = value; }
        }
        private string _AGENT_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AGENT_ID", System.Data.DbType.String)]
        public string AGENT_ID {
            get { return _AGENT_ID; }
            set { _AGENT_ID = value; }
        }
        private string _AGENT_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AGENT_NAME", System.Data.DbType.String)]
        public string AGENT_NAME {
            get { return _AGENT_NAME; }
            set { _AGENT_NAME = value; }
        }
        private string _OPR_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("OPR_ID", System.Data.DbType.String)]
        public string OPR_ID {
            get { return _OPR_ID; }
            set { _OPR_ID = value; }
        }
        private string _PROGRESS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROGRESS", System.Data.DbType.String)]
        public string PROGRESS {
            get { return _PROGRESS; }
            set { _PROGRESS = value; }
        }
        private bool _CANCELLED;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CANCELLED", System.Data.DbType.Boolean)]
        public bool CANCELLED {
            get { return _CANCELLED; }
            set { _CANCELLED = value; }
        }
        private string _REMARK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REMARK", System.Data.DbType.String)]
        public string REMARK {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        private string _REMARK2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REMARK2", System.Data.DbType.String)]
        public string REMARK2 {
            get { return _REMARK2; }
            set { _REMARK2 = value; }
        }
        private string _RWAREH_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RWAREH_NAME", System.Data.DbType.String)]
        public string RWAREH_NAME {
            get { return _RWAREH_NAME; }
            set { _RWAREH_NAME = value; }
        }
        private string _REMARK3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REMARK3", System.Data.DbType.String)]
        public string REMARK3 {
            get { return _REMARK3; }
            set { _REMARK3 = value; }
        }
        private string _DISP_WAREH_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DISP_WAREH_ID", System.Data.DbType.String)]
        public string DISP_WAREH_ID {
            get { return _DISP_WAREH_ID; }
            set { _DISP_WAREH_ID = value; }
        }
        private string _DISP_WAREH_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DISP_WAREH_NAME", System.Data.DbType.String)]
        public string DISP_WAREH_NAME {
            get { return _DISP_WAREH_NAME; }
            set { _DISP_WAREH_NAME = value; }
        }
        private bool _TO_CONTRACT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("TO_CONTRACT", System.Data.DbType.String)]
        public bool TO_CONTRACT {
            get { return _TO_CONTRACT; }
            set { _TO_CONTRACT = value; }
        }

      

    } 

}

