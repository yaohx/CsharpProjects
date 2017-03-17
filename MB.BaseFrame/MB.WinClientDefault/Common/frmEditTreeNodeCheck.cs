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
    /// 添加树型节点时的选择项。
    /// </summary>
    public partial class frmEditTreeNodeCheck : AbstractBaseForm {
        private EditTreeNodeCheckParam _CheckParam;
        public frmEditTreeNodeCheck(EditTreeNodeCheckParam checkParam) {
            InitializeComponent();
            _CheckParam = checkParam;
        }
      

        private void butAddAsChild_Click(object sender, EventArgs e) {
            _CheckParam.AddAdChild = true;
            _CheckParam.AllIsSame = chkAllIsSame.Checked;
            this.Close();
        }

        private void butAddAsRoot_Click(object sender, EventArgs e) {
            _CheckParam.AddAdChild = false;
            _CheckParam.AllIsSame = chkAllIsSame.Checked;
            this.Close();
        }
    }
    /// <summary>
    /// 节点选择行为参数。
    /// </summary>
    public class EditTreeNodeCheckParam  {
        private bool _AddAsChild;
        private bool _AllIsSame;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addAsChild"></param>
        /// <param name="allIsSame"></param>
        public EditTreeNodeCheckParam() {
        }
        /// <summary>
        /// 作为子节点进行添加
        /// </summary>
        public bool AddAdChild {
            get {
                return _AddAsChild;
            }
            set {
                _AddAsChild = value;
            }
        }
        /// <summary>
        /// 所有按同一方式进行处理
        /// </summary>
        public bool AllIsSame {
            get {
                return _AllIsSame;
            }
            set {
                _AllIsSame = value;
            }
        }
    }
}
