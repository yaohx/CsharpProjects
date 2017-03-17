using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmExcelViewTest : Form {
        public frmExcelViewTest() {
            InitializeComponent();
            this.Load += new EventHandler(frmExcelViewTest_Load);
        }

        void frmExcelViewTest_Load(object sender, EventArgs e) {
            string fileName = @"D:\test.xls";
            this.excelViewer1.OpenFile(fileName);
        }

        private void button1_Click(object sender, EventArgs e) {
            MB.WinBase.InvokeMethodWithWaitCursor.InvokeWithWait(() => {
                System.Threading.Thread.Sleep(2000);
                                        this.excelViewer1.CloseExcel();
                                    }, "正在努力提交");

            this.Close();
           
        }


    }
}
