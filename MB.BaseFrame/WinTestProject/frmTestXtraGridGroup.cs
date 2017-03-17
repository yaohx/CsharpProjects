using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;

namespace WinTestProject {
    public partial class frmTestXtraGridGroup : Form {
        public frmTestXtraGridGroup() {
            InitializeComponent();


            this.Load += new EventHandler(frmTestXtraGridGroup_Load);
            gridView1.OptionsView.ShowFooter = true;
            gridView1.OptionsView.ShowGroupPanel = true;
            gridView1.OptionsView.AllowCellMerge = true;


            bandedGridView1.OptionsView.AllowCellMerge = true; 
            
        }

        void advBandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
            if (e.Column.FieldName.Equals("Name")) {
                e.DisplayText = string.Empty;
                e.Graphics.DrawRectangle(Pens.White,new Rectangle(e.Bounds.X,e.Bounds.Y,e.Bounds.Width,e.Bounds.Height + 1)  );   
            }
        }

        void frmTestXtraGridGroup_Load(object sender, EventArgs e) {
          //  (testXtraGrid as MB.XWinLib.XtraGrid.GridControlEx).BeforeContextMenuClick += new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(frmTestXtraGridGroup_BeforeContextMenuClick);
         
          //  gridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded ;
            gridView1.OptionsCustomization.AllowGroup = true;
          
            gridView1.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(gridView1_CellMerge);
            
         //   gridView1.OptionsView.ShowGroupedColumns = true; 
           // MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(gridControlEx1,sour, "testHViewDataConvert");

            //webBrowser1.Url = new Uri(@"E:\Nick\工作相关\MB.NET 应用系统开发相关\MB.ERP.NET\0305.swf");
        }

        //void frmTestXtraGridGroup_BeforeContextMenuClick(object sender, MB.XWinLib.XtraGrid.GridControlExMenuEventArg arg)
        //{
        //    if(arg.MenuType== MB.XWinLib.XtraGrid.XtraContextMenuType.
        //}

        void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e) {
            if (e.Column.FieldName.Equals("Name")) {
                e.Merge = object.Equals(e.CellValue1, e.CellValue2);

            }
            else {
                e.Merge = false;
                
            }
            e.Handled = true;
        }

        //创建分析数据
        private DataSet createData() {
            DataTable dt = new DataTable();
            
            dt.Columns.Add("Name",typeof(string));
            dt.Columns.Add("Code",typeof(string));
            dt.Columns.Add("CreateDate",typeof(DateTime));
            dt.Columns.Add("Size",typeof(string));
            dt.Columns.Add("SizeName",typeof(string));
            dt.Columns.Add("Count",typeof(Int32));
            dt.Columns.Add("Amount",typeof(decimal));

            for (int i = 0; i < 10; i++) {
                dt.Rows.Add(new object[] {"AAA","AAA" + i,System.DateTime.Now, "S","小号",i,i *  10 });
            }
            for (int i = 0; i < 10; i++) {
                dt.Rows.Add(new object[] { "BBB", "BBB" + i, System.DateTime.Now, "M", "小号", i, i * 10 });
            }
            for (int i = 0; i < 10; i++) {
                dt.Rows.Add(new object[] { "CCC", "CCC" + i, System.DateTime.Now, "X", "小号", i, i * 10 });
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        private void button1_Click(object sender, EventArgs e) {
            DataSet ds = createData();
            // MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(gridControlEx1, ds, "testHViewDataConvert");     
             gridControlEx1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //var dc = gridView1.Columns[5];

             var s = new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "SUM(Count)", null, "总数={0}");
             gridView1.GroupSummary.Add(s); 
            //s = new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Count", dc, "Count={0}");
            //gridView1.GroupSummary.Add(s); 
        }

    }
}
