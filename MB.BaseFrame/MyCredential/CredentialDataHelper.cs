using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyCredential {
    /// <summary>
    /// 证书数据提供类
    /// </summary>
    class CredentialDataHelper {
        private DataSet _CurrentBinding;
        private static readonly string TEMPLATE_STRING = "CfgName={0},URL={1},Domain={2},LoginName={3},LoginPassword={4}";
        public CredentialDataHelper() {
            _CurrentBinding = createBindingData();
        }
        private DataSet createBindingData() {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            dt.Columns.Add("CfgName", typeof(string)).Caption = "应用编码";
            dt.Columns.Add("URL", typeof(string)).Caption = "URL格式化字符窜";
            dt.Columns.Add("Domain", typeof(string)).Caption = "域";
            dt.Columns.Add("LoginName", typeof(string)).Caption = "登录用户";
            dt.Columns.Add("LoginPassword", typeof(string)).Caption = "登录密码";
            return ds;
        }
        public DataSet CurrentBinding {
            get {
                return _CurrentBinding;
            }
        }
        public string CredentialToString() {
            StringBuilder sb = new StringBuilder();
            if (_CurrentBinding.Tables[0].Rows.Count == 0) return string.Empty;
            DataRow[] drs = _CurrentBinding.Tables[0].Select();
            foreach (var dr in drs) {
                var line = string.Format(TEMPLATE_STRING, dr["CfgName"], dr["URL"], dr["Domain"], dr["LoginName"], dr["LoginPassword"]);
                if (sb.Length > 0)
                    sb.Append(";");
                sb.Append(line);
            }
            return sb.ToString();
        }
    }
}
