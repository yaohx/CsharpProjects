using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

 
namespace WinTestProject {
    public partial class testXtraGrid : Form {
        public testXtraGrid() {
            InitializeComponent();

           // DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer.Active = MB.XWinLib.Localization.PivotGridLocal.Create();
        }
        DataTable dt;
        private void button1_Click(object sender, EventArgs e) {
             dt = new DataTable();
            dt.Columns.Add("AAA",typeof(System.Int32) );
            dt.Columns.Add("AAA2",typeof(System.DateTime));
            dt.Columns.Add("AAA3");


            dt.Rows.Add(new object[] {"123","2008-12-09","2342" });
            dt.Rows.Add(new object[] { "44", "2008-12-09", "2342" });
            dt.Rows.Add(new object[] { "55", "2008-12-09", "2342" });
            dt.Rows.Add(new object[] { "66", "2008-12-09", "2342" });
            dt.DefaultView.ListChanged += new ListChangedEventHandler(DefaultView_ListChanged); 

           gridControl1.ShowOptionMenu = true;
            //gridControl1.ReSetContextMenu(MB.XWinLib.XtraGrid.XtraContextMenuType.SaveGridState);
           gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
           gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanging);
            gridControl1.DataSource = dt.DefaultView;

            gridControl1.ReSetContextMenu(MB.XWinLib.XtraGrid.XtraContextMenuType.Copy);


        }

        void DefaultView_ListChanged(object sender, ListChangedEventArgs e) {
            
        }

        void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) {
             
        }

        void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) {
            
        }

        private void button3_Click(object sender, EventArgs e) {
            //ConditionsAdjustment();       
            dt.Rows[0][0] = 999;
            //gridView1.RefreshData(); 

            gridView1.SetRowCellValue(0, "AAA", 99); 
        }
        private void ConditionsAdjustment() {
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;

            //StyleFormatCondition cn;
            //cn = new StyleFormatCondition(FormatConditionEnum.LessOrEqual, gridView1.Columns["AAA"], null, 44);
            //cn.Appearance.BackColor = Color.Yellow;
            //gridView1.FormatConditions.Add(cn);
        }
    }
}
