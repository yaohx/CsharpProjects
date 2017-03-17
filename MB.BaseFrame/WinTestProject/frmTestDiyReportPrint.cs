using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestDiyReportPrint : Form {
        public frmTestDiyReportPrint() {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e) {
            Dictionary<string,object> pars = new Dictionary<string,object>();
            pars.Add("UserID","HQ01U4282");
            pars.Add("UserName","陈迪臣");
            DataSet ds = getReportData();
            System.Guid gid = new Guid("60215F32-1467-46a5-A5A7-D38E6ECC9CB2");

            MB.WinPrintReport.DefaultReportData reportData = new MB.WinPrintReport.DefaultReportData("StockOut", ds, pars);
            MB.WinPrintReport.FrmEditPrintTemplete frm = new MB.WinPrintReport.FrmEditPrintTemplete(reportData,gid);
            frm.ShowDialog(); 
        }
        //
        private DataSet getReportData() {
            DataSet dsData = new DataSet();
            DataTable dtMain = new DataTable("Main");
            dtMain.Columns.Add("UserID");
            dtMain.Rows.Add(new object[] { "猜测上的用户"});
 
            DataTable dt = new DataTable("Product");
            dt.Columns.Add("商品码");
            dt.Columns.Add("批次日期",typeof(System.DateTime));
            dt.Columns.Add("数量",typeof(System.Int32));

            //批次日期	汇总
            DataTable dtSum = new DataTable("Summary");
            dtSum.Columns.Add("批次日期");
            dtSum.Columns.Add("汇总",typeof(System.Int32));

            dsData.Tables.Add(dtMain);
            dsData.Tables.Add(dt);
            dsData.Tables.Add(dtSum);

            //初始化数据
            for (int i = 0; i < 100; i++) {
                dt.Rows.Add(new object[] { "ProductID:" + i,System.DateTime.Now.AddDays(i),i / 2 });
            }

            dtSum.Rows.Add(new object[] { "第一批", 3454 });
            dtSum.Rows.Add(new object[] { "第二批", 1000 });
            dtSum.Rows.Add(new object[] { "第三批", 500 });

            return dsData;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Dictionary<string, object> pars = new Dictionary<string, object>();
            pars.Add("UserID", "XXXXX");
            pars.Add("UserName", "XXXXX");
            DataSet ds = getReportData();
            System.Guid gid = new Guid("60215f32-1467-46a5-a5a7-d38e6ecc9cb2");
            MB.WinPrintReport.DefaultReportData reportData = new MB.WinPrintReport.DefaultReportData("StockOut", ds, pars);
            MB.WinPrintReport.PrintContextMenu.ShowMenu(linkLabel1, reportData);  
        }

        private void button2_Click(object sender, EventArgs e) {
            _SubReportS = new Hashtable();


            DataTable dtMain = new DataTable();
            dtMain.Columns.Add("CompanyID");
            dtMain.Columns.Add("CompanyName");
            dtMain.Rows.Add(new object[] { "122", "南极" });


            _DsData.Tables.Add(dtMain);
            DataTable dtChild = new DataTable();

            dtChild.Columns.Add("CompanyID");
            dtChild.Columns.Add("CompanyName");
            _DsData.Tables.Add(dtChild);
            //_DsData.Tables[0].Columns["CompanyCode"].Caption = "公司编码";
            _DsData.Relations.Add("Mytest", _DsData.Tables[0].Columns["CompanyID"], _DsData.Tables[1].Columns["CompanyID"]);

          //  DIYReport.Extend.EndUserDesigner.MainDesignForm frm = new DIYReport.Extend.EndUserDesigner.MainDesignForm(_DsData, null, null);
            //frm.BeginXReportIOProcess += new DIYReport.ReportModel.XReportIOEventHandler(frm_BeginXReportIOProcess);
           // frm.ShowDialog();
        }
        private DataSet _DsData = new DataSet();
        private Hashtable _SubReportS;
  
    }
}
