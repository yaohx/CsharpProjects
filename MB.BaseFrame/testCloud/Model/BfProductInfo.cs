using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace testCloud.Model
{
   	/// <summary> 
	/// 文件生成时间 2009-12-24 04:06 
	/// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("BF_PRODUCT", "BfProduct", new string[] { "ID" })]
    [KnownType(typeof(BfProductInfo))]
    public class BfProductInfo : MB.Orm.Common.BaseModel
    {
        public BfProductInfo() {

        }
        private int _ID;
        [DataMember]
        [Description("ID")]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _BF_PROD_CLS_ID;
        [DataMember]
        [Description("商品类ID")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_CLS_ID", System.Data.DbType.Int32)]
        public int BF_PROD_CLS_ID {
            get { return _BF_PROD_CLS_ID; }
            set { _BF_PROD_CLS_ID = value; }
        }
        private string _PROD_CLS_NUM;
        [DataMember]
        [Description("商品类编码")]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_CLS_NUM", System.Data.DbType.String)]
        public string PROD_CLS_NUM {
            get { return _PROD_CLS_NUM; }
            set { _PROD_CLS_NUM = value; }
        }
        private string _BF_PROD_CLS_NAME;
        [DataMember]
        [Description("商品类名称")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_CLS_NAME", System.Data.DbType.String)]
        public string BF_PROD_CLS_NAME {
            get { return _BF_PROD_CLS_NAME; }
            set { _BF_PROD_CLS_NAME = value; }
        }
        private int _BF_PROD_COLOR_ID;
        [DataMember]
        [Description("商品颜色")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_COLOR_ID", System.Data.DbType.Int32)]
        public int BF_PROD_COLOR_ID {
            get { return _BF_PROD_COLOR_ID; }
            set { _BF_PROD_COLOR_ID = value; }
        }
        private string _BF_PROD_COLOR_CODE;
        [DataMember]
        [Description("商品颜色编码")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_COLOR_CODE", System.Data.DbType.String)]
        public string BF_PROD_COLOR_CODE {
            get { return _BF_PROD_COLOR_CODE; }
            set { _BF_PROD_COLOR_CODE = value; }
        }
        private string _BF_PROD_COLOR_NAME;
        [DataMember]
        [Description("商品颜色名称")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_COLOR_NAME", System.Data.DbType.String)]
        public string BF_PROD_COLOR_NAME {
            get { return _BF_PROD_COLOR_NAME; }
            set { _BF_PROD_COLOR_NAME = value; }
        }
        private int _BF_PROD_COLOR_SERIES_ID;
        [DataMember]
        [Description("商品颜色系")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_COLOR_SERIES_ID", System.Data.DbType.Int32)]
        public int BF_PROD_COLOR_SERIES_ID {
            get { return _BF_PROD_COLOR_SERIES_ID; }
            set { _BF_PROD_COLOR_SERIES_ID = value; }
        }
        private string _BF_PROD_COLOR_SERIES_CODE;
        [DataMember]
        [Description("商品颜色系编码")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_COLOR_SERIES_CODE", System.Data.DbType.String)]
        public string BF_PROD_COLOR_SERIES_CODE {
            get { return _BF_PROD_COLOR_SERIES_CODE; }
            set { _BF_PROD_COLOR_SERIES_CODE = value; }
        }
        private string _BF_PROD_COLOR_SERIES_NAME;
        [DataMember]
        [Description("商品颜色系名称")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_COLOR_SERIES_NAME", System.Data.DbType.String)]
        public string BF_PROD_COLOR_SERIES_NAME {
            get { return _BF_PROD_COLOR_SERIES_NAME; }
            set { _BF_PROD_COLOR_SERIES_NAME = value; }
        }
        private int _BF_PROD_EDTN_ID;
        [DataMember]
        [Description("版型")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_EDTN_ID", System.Data.DbType.Int32)]
        public int BF_PROD_EDTN_ID {
            get { return _BF_PROD_EDTN_ID; }
            set { _BF_PROD_EDTN_ID = value; }
        }
        private string _BF_PROD_EDTN_CODE;
        [DataMember]
        [Description("版型编码")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_EDTN_CODE", System.Data.DbType.String)]
        public string BF_PROD_EDTN_CODE {
            get { return _BF_PROD_EDTN_CODE; }
            set { _BF_PROD_EDTN_CODE = value; }
        }
        private string _BF_PROD_EDTN_NAME;
        [DataMember]
        [Description("版型名称")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_EDTN_NAME", System.Data.DbType.String)]
        public string BF_PROD_EDTN_NAME {
            get { return _BF_PROD_EDTN_NAME; }
            set { _BF_PROD_EDTN_NAME = value; }
        }
        private int _BF_PROD_EDTN_CLS_ID;
        [DataMember]
        [Description("版型类")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_EDTN_CLS_ID", System.Data.DbType.Int32)]
        public int BF_PROD_EDTN_CLS_ID {
            get { return _BF_PROD_EDTN_CLS_ID; }
            set { _BF_PROD_EDTN_CLS_ID = value; }
        }
        private string _BF_PROD_EDTN_CLS_CODE;
        [DataMember]
        [Description("版型类编码")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_EDTN_CLS_CODE", System.Data.DbType.String)]
        public string BF_PROD_EDTN_CLS_CODE {
            get { return _BF_PROD_EDTN_CLS_CODE; }
            set { _BF_PROD_EDTN_CLS_CODE = value; }
        }
        private string _BF_PROD_EDTN_CLS_NAME;
        [DataMember]
        [Description("版型类名称")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_EDTN_CLS_NAME", System.Data.DbType.String)]
        public string BF_PROD_EDTN_CLS_NAME {
            get { return _BF_PROD_EDTN_CLS_NAME; }
            set { _BF_PROD_EDTN_CLS_NAME = value; }
        }
        private int _BF_PROD_SPEC_ID;
        [DataMember]
        [Description("规格")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_SPEC_ID", System.Data.DbType.Int32)]
        public int BF_PROD_SPEC_ID {
            get { return _BF_PROD_SPEC_ID; }
            set { _BF_PROD_SPEC_ID = value; }
        }
        private string _BF_PROD_SPEC_CODE;
        [DataMember]
        [Description("规格编码")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_SPEC_CODE", System.Data.DbType.String)]
        public string BF_PROD_SPEC_CODE {
            get { return _BF_PROD_SPEC_CODE; }
            set { _BF_PROD_SPEC_CODE = value; }
        }
        private string _BF_PROD_SPEC_NAME;
        [DataMember]
        [Description("规格名称")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_SPEC_NAME", System.Data.DbType.String)]
        public string BF_PROD_SPEC_NAME {
            get { return _BF_PROD_SPEC_NAME; }
            set { _BF_PROD_SPEC_NAME = value; }
        }
        private int _BF_PROD_SPEC_CLS_ID;
        [DataMember]
        [Description("规格类")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_SPEC_CLS_ID", System.Data.DbType.Int32)]
        public int BF_PROD_SPEC_CLS_ID {
            get { return _BF_PROD_SPEC_CLS_ID; }
            set { _BF_PROD_SPEC_CLS_ID = value; }
        }
        private string _BF_PROD_SPEC_CLS_CODE;
        [DataMember]
        [Description("规格类编码")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_SPEC_CLS_CODE", System.Data.DbType.String)]
        public string BF_PROD_SPEC_CLS_CODE {
            get { return _BF_PROD_SPEC_CLS_CODE; }
            set { _BF_PROD_SPEC_CLS_CODE = value; }
        }
        private string _BF_PROD_SPEC_CLS_NAME;
        [DataMember]
        [Description("规格类名称")]
        [MB.Orm.Mapping.Att.ColumnMap("BF_PROD_SPEC_CLS_NAME", System.Data.DbType.String)]
        public string BF_PROD_SPEC_CLS_NAME {
            get { return _BF_PROD_SPEC_CLS_NAME; }
            set { _BF_PROD_SPEC_CLS_NAME = value; }
        }
        private string _PROD_NUM;
        [DataMember]
        [Description("商品编码")]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_NUM", System.Data.DbType.String)]
        public string PROD_NUM {
            get { return _PROD_NUM; }
            set { _PROD_NUM = value; }
        }
        private string _ADDIT_DESC;
        [DataMember]
        [Description("补充描述")]
        [MB.Orm.Mapping.Att.ColumnMap("ADDIT_DESC", System.Data.DbType.String)]
        public string ADDIT_DESC {
            get { return _ADDIT_DESC; }
            set { _ADDIT_DESC = value; }
        }
        private string _INNER_BC;
        [DataMember]
        [Description("店内码")]
        [MB.Orm.Mapping.Att.ColumnMap("INNER_BC", System.Data.DbType.String)]
        public string INNER_BC {
            get { return _INNER_BC; }
            set { _INNER_BC = value; }
        }
        private string _INTNL_BC;
        [DataMember]
        [Description("国际码")]
        [MB.Orm.Mapping.Att.ColumnMap("INTNL_BC", System.Data.DbType.String)]
        public string INTNL_BC {
            get { return _INTNL_BC; }
            set { _INTNL_BC = value; }
        }
        private string _PROD_STATUS;
        [DataMember]
        [Description("商品状态")]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_STATUS", System.Data.DbType.String)]
        public string PROD_STATUS {
            get { return _PROD_STATUS; }
            set { _PROD_STATUS = value; }
        }
        private string _CANCEL_REASON;
        [DataMember]
        [Description("取消原因")]
        [MB.Orm.Mapping.Att.ColumnMap("CANCEL_REASON", System.Data.DbType.String)]
        public string CANCEL_REASON {
            get { return _CANCEL_REASON; }
            set { _CANCEL_REASON = value; }
        }
        private string _PROD_GRID;
        [DataMember]
        [Description("网格值")]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_GRID", System.Data.DbType.String)]
        public string PROD_GRID {
            get { return _PROD_GRID; }
            set { _PROD_GRID = value; }
        }
        private string _REMARK;
        [DataMember]
        [Description("备注")]
        [MB.Orm.Mapping.Att.ColumnMap("REMARK", System.Data.DbType.String)]
        public string REMARK {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        private bool _IS_TRANS_B2C;
        [DataMember]
        [Description("是否传入B2C")]
        [MB.Orm.Mapping.Att.ColumnMap("IS_TRANS_B2C", System.Data.DbType.Boolean)]
        public bool IS_TRANS_B2C {
            get { return _IS_TRANS_B2C; }
            set { _IS_TRANS_B2C = value; }
        }
        private string _LAST_MODIFIED_USER;
        [DataMember]
        [Description("最后更新人")]
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_USER", System.Data.DbType.String)]
        public string LAST_MODIFIED_USER {
            get { return _LAST_MODIFIED_USER; }
            set { _LAST_MODIFIED_USER = value; }
        }
    }
}
