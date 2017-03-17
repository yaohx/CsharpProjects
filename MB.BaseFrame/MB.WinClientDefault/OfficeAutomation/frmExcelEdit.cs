using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace MB.WinClientDefault.OfficeAutomation {
    /// <summary>
    /// 打开excel编辑窗口
    /// </summary>
    public partial class frmExcelEdit : DevExpress.XtraEditors.XtraForm {

        private IExcelEditor _ExcelEdit;
        private IList _SumittedList;

        /// <summary>
        /// 当excel提交以后得到的所有excel的值
        /// </summary>
        public IList SumittedList {
            get { return _SumittedList; }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="excelEdit">实现excel编辑的实体</param>
        public frmExcelEdit(IExcelEditor excelEdit) {
            InitializeComponent();
            this.Load += new EventHandler(frmExcelEdit_Load);
            _ExcelEdit = excelEdit;
        }

        private void frmExcelEdit_Load(object sender, EventArgs e) {
            try {
                string excelFilePath = _ExcelEdit.OpenExcel();
                this.excelViewer.OpenFile(excelFilePath);

            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("打开excel编辑窗口出错了，" + ex.ToString());
                MB.WinBase.MessageBoxEx.Show("打开excel编辑窗口出错了");
            }
        }


        private void btnSave_Click(object sender, EventArgs e) {
            try {
                MB.WinBase.InvokeMethodWithWaitCursor.InvokeWithWait(() => {
                    this.excelViewer.SaveActiveWorkbook();
                    this.excelViewer.CloseExcel();
                    _SumittedList = _ExcelEdit.Submit();
                }, "正在提交execel...");

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("提交excel编辑窗口出错了，" + ex.ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

    }
}