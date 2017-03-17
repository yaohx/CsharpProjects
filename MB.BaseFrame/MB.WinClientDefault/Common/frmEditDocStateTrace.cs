//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	单据状态改变对应处理的事件。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault.Common {
    /// <summary>
    /// 单据状态改变对应处理的事件。
    /// </summary>
    public partial class frmEditDocStateTrace : AbstractBaseForm {
        private MB.Util.Model.DocOperateType _DocOperateType;

        #region 自定义事件处理相关...
        private System.EventHandler<EditDocStateTraceEventArgs> _AfterEditSure;
        public event System.EventHandler<EditDocStateTraceEventArgs> AfterEditSure {
            add {
                _AfterEditSure += value; 
            }
            remove {
                _AfterEditSure -= value;
            }
        }
        private void onAfterEditSure(EditDocStateTraceEventArgs arg) {
            if (_AfterEditSure != null)
                _AfterEditSure(this, arg);
        }
        #endregion 自定义事件处理相关...

        /// <summary>
        /// 
        /// </summary>
        public frmEditDocStateTrace(MB.Util.Model.DocOperateType docOperateType) {
            InitializeComponent();

            _DocOperateType = docOperateType;
            this.Load += new EventHandler(frmEditDocStateTrace_Load);

        }

        void frmEditDocStateTrace_Load(object sender, EventArgs e) {
            txtUserName.Text = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_NAME;
            labTitle.Text = MB.Util.MyCustomAttributeLib.Instance.GetFieldDesc(typeof(MB.Util.Model.DocOperateType), _DocOperateType.ToString(), false);
        }

        private void butQuit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void butSure_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtOpRemark.Text)) {
                MB.WinBase.MessageBoxEx.Show("操作描述不能为空,请输入");
                txtOpRemark.Focus();  
                return;
            }
            onAfterEditSure(new EditDocStateTraceEventArgs(txtOpRemark.Text , _DocOperateType));

            this.Close();
        }
    }    


    /// <summary>
    /// 单据状态编辑对应事件参数。
    /// </summary>
    public class EditDocStateTraceEventArgs : System.EventArgs {
        /// <summary>
        /// 单据状态编辑对应事件参数。
        /// </summary>
        /// <param name="remark">操作输入的备注信息</param>
        public EditDocStateTraceEventArgs(string remark, MB.Util.Model.DocOperateType docOperateType) {
            _Remark = remark;
            _DocOperateType = docOperateType;
        }

        private MB.Util.Model.DocOperateType _DocOperateType;
        /// <summary>
        /// 单据操作类型。
        /// </summary>
        public MB.Util.Model.DocOperateType DocOperateType {
            get { return _DocOperateType; }
            set { _DocOperateType = value; }
        }
        private string _Remark;
        /// <summary>
        /// 操作输入的备注信息。
        /// </summary>
        public string Remark {
            get { return _Remark; }
            set { _Remark = value; }
        }

    }
}
