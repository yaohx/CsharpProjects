using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject
{
    public partial class frmTestExcelImport : Form
    {
        public frmTestExcelImport() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            MB.WinEIDrive.Import.XlsImport import = new MB.WinEIDrive.Import.XlsImport(null,@"F:\log\myExcel.xls");
            import.Commit();
            dataGridView1.DataSource = import.ImportData;
        }
    }
}
