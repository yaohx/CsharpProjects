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
    public partial class testHViewDataConvert : Form {
        public testHViewDataConvert() {
            InitializeComponent();

            _DataBinding = new MB.WinClientDefault.Common.UIDynamicColumnBinding<ProductInfo>(gridControl1, "testHViewDataConvert");
            _DataBinding.ReadOnly = true;
            _DataBinding.AfterDataConvert += new EventHandler(_DataBinding_AfterDataConvert);
            _LstEntitys = getOrgDataSource();
        }
        private List<ProductInfo> _LstEntitys;
        private MB.WinClientDefault.Common.UIDynamicColumnBinding<ProductInfo> _DataBinding;
        private MB.WinBase.Data.HViewDataConvert<ProductInfo> _DataConvert;
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;
        private string xmlFileName = "testHViewDataConvert";
        private void button1_Click(object sender, EventArgs e) {

            List<ProductInfo> lstData = _LstEntitys;


            MB.WinBase.Data.HViewConvertCfgParam cfgPars = MB.WinBase.LayoutXmlConfigHelper.Instance.GetHViewConvertCfgParam("testHViewDataConvert", string.Empty);
            cfgPars.DynamicColumnCaption = false;

            MB.WinBase.Data.HViewDataConvert<ProductInfo> convert = new MB.WinBase.Data.HViewDataConvert<ProductInfo>(cfgPars);
 
          
            _DataBinding.CreateDataBinding(convert, lstData, "DynamicGroup");  
        }

        void _DataBinding_AfterDataConvert(object sender, EventArgs e) {
              //_DataBinding.CurrentEditTable
              //追加汇总列
             //string sumString = string.Empty;
             //for(int index = 0; index < _DataBinding.HViewConvertObject.DynamicColumnCount){

             //}
            string totalCountName = "TotalCount";
            if (!_DataBinding.CurrentEditTable.Columns.Contains(totalCountName)) {
                string express = getSumExpress(_DataBinding.HViewConvertObject.DynamicColumnCount,false);
                _DataBinding.CurrentEditTable.Columns.Add(totalCountName, typeof(System.Int32), express); 
            }

            string totalAmountName = "TotalAmount";
            if (!_DataBinding.CurrentEditTable.Columns.Contains(totalAmountName)) {
                string express = getSumExpress(_DataBinding.HViewConvertObject.DynamicColumnCount, true);
                _DataBinding.CurrentEditTable.Columns.Add(totalAmountName, typeof(System.Int32), express);
            }
        }
        private string getSumExpress(int maxCount, bool isAmount) {
            string sumString = string.Empty;
            for (int i = 0; i < maxCount; i++) {
                string fieldName = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName("Count", i);
                if (isAmount)
                    fieldName = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName("Amount", i);

                sumString += "ISNULL([" + fieldName + "],0) + ";
            }
            if (sumString.Length > 0) {
                //if (isAmount)
                //    sumString = "(" + sumString.Remove(sumString.Length - 2, 2) + ") * PRICE * DISC_RATE/100";
                //else
                sumString = sumString.Remove(sumString.Length - 2, 2);
            }

            return sumString;
        }
        //常规列表
        private void button2_Click(object sender, EventArgs e) {

 
            List<ProductInfo> lstData = _LstEntitys;
            var detailBindingParams = new MB.XWinLib.GridDataBindingParam(gridControl1, lstData, false);
            
            _ColPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
            var editCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(_ColPropertys, xmlFileName);
            var gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(xmlFileName,string.Empty);
            MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.CreateEditXtraGrid(detailBindingParams, _ColPropertys, editCols, gridViewLayoutInfo);
        }

        private  List<ProductInfo> getOrgDataSource() {
            List<ProductInfo> lstData = new List<ProductInfo>();
            lstData.Add(new ProductInfo("红玫瑰",DateTime.Parse("2009-11-09"), "0001", "01", "S", 9, 354));
            lstData.Add(new ProductInfo("红玫瑰", DateTime.Parse("2009-11-09"), "0001", "02", "M", 2, 888));
            lstData.Add(new ProductInfo("红玫瑰", DateTime.Parse("2009-11-09"), "0001", "03", "L", 3, 86566));
            lstData.Add(new ProductInfo("蓝玫瑰", DateTime.Parse("2009-11-09"), "0002", "02", "M", 5, 6664));
            lstData.Add(new ProductInfo("蓝玫瑰", DateTime.Parse("2009-11-09"), "0002", "03", "L", 6, 775));
            lstData.Add(new ProductInfo("黑玫瑰", DateTime.Parse("2009-11-09"), "0003", "01", "S", 2, 344));
            lstData.Add(new ProductInfo("黑玫瑰", DateTime.Parse("2009-11-09"), "0003", "02", "M", 1, 999));
            lstData.Add(new ProductInfo("黑玫瑰", DateTime.Parse("2009-11-09"), "0003", "03", "L", 0, 34));
            lstData.Add(new ProductInfo("黑玫瑰", DateTime.Parse("2009-11-09"), "0003", "04", "XL", 8, 454));

            lstData.OrderBy(o => o.Size);
            return lstData;
        }

        private void button3_Click(object sender, EventArgs e) {
            List<ProductInfo> lstData = _LstEntitys;

            MB.WinBase.Data.HViewConvertCfgParam cfgPars = new MB.WinBase.Data.HViewConvertCfgParam();
            cfgPars.DynamicColumnCaption = false;
            cfgPars.RowAreaColumns = new string[] { "Name", "Code" };
            cfgPars.ConvertKeyColumns = new string[] { "Code" };
            cfgPars.ColumnAreaCfgInfo.ValueColumnName = "Size";
            cfgPars.ColumnAreaCfgInfo.CaptionColumnName = "SizeName";
            cfgPars.ColumnAreaCfgInfo.OrderColumnName = "Size";
            cfgPars.ColumnAreaCfgInfo.MappingColumnName = new string[] { "Count", "Amount" };

            //MB.WinBase.Data.HViewConvertCfgParam cfgPars = MB.WinBase.LayoutXmlConfigHelper.Instance.GetHViewConvertCfgParam("testHViewDataConvert",string.Empty);
            //cfgPars.DynamicColumnCaption = false;

            _DataConvert = new MB.WinBase.Data.HViewDataConvert<ProductInfo>(cfgPars);
     

            _DataBinding.CreateDataBinding(_DataConvert, lstData, "SDynamicColumn");  
        }

        private void button4_Click(object sender, EventArgs e) {
            List<ProductInfo> lstData = _LstEntitys;

            //MB.WinBase.Data.HViewConvertCfgParam cfgPars = new MB.WinBase.Data.HViewConvertCfgParam();
            //cfgPars.DynamicColumnCaption = true;
            //cfgPars.RowAreaColumns = new string[] { "Name", "Code" };
            //cfgPars.ConvertKeyColumns = new string[] { "Code" };
            //cfgPars.ColumnAreaCfgInfo.ValueColumnName = "Size";
            //cfgPars.ColumnAreaCfgInfo.CaptionColumnName = "SizeName";
            //cfgPars.ColumnAreaCfgInfo.OrderColumnName = "Size";
            //cfgPars.ColumnAreaCfgInfo.MappingColumnName = new string[] { "Count" };
            MB.WinBase.Data.HViewConvertCfgParam cfgPars = MB.WinBase.LayoutXmlConfigHelper.Instance.GetHViewConvertCfgParam("testHViewDataConvert", string.Empty);
            cfgPars.DynamicColumnCaption = true;

            _DataConvert = new MB.WinBase.Data.HViewDataConvert<ProductInfo>(cfgPars);

         

            _DataBinding.CreateDataBinding(_DataConvert, lstData, "DynamicColumn");

         
        }

        private void advBandedGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
           
        }

        private void button5_Click(object sender, EventArgs e) {
            ProductInfo[] lstData = _DataBinding.GetDetailEntitysByDataRow(1); 
            string colName = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName("Count",1);
            ProductInfo data = _DataBinding.GetDetailEntityByDataRowAndColumn(1, colName);

            // 
           
        }

        private void button6_Click(object sender, EventArgs e) {
           
            DataTable dt = MB.Util.MyConvert.Instance.ToDataTable(gridControl1.DataSource, "");
            MB.WinClientDefault.Common.FrmQuickCellDataInput frm = new MB.WinClientDefault.Common.FrmQuickCellDataInput(
                                                gridControl1, dt, _DataConvert.DynamicColumns,xmlFileName);
            frm.RowCellEditForEditing += new EventHandler<MB.WinClientDefault.Common.GridRowCellEditEventArgs>(frm_RowCellEditForEditing);
            frm.BeforeDataImport += new EventHandler<MB.WinClientDefault.Common.QuickInputDataImportEventArgs>(frm_AfterDataImport);
            frm.ShowDialog(); 

        }

        void frm_RowCellEditForEditing(object sender, MB.WinClientDefault.Common.GridRowCellEditEventArgs e) {
           // DataRow dr = getEditRowByQuickEditRow(QuickEditDataRow);
           e.AllowEdit = _DataBinding.CheckRowCellAllowEdit(e.QuickEditDataRow, e.ColumnName.FieldName);    
        }

        void frm_AfterDataImport(object sender, MB.WinClientDefault.Common.QuickInputDataImportEventArgs e) {
            e.Handled = true;

        }

    }
    


    public class ProductInfo {

        public ProductInfo(string name,DateTime createDate, string code,string size,string sizeName,int count,int amount) {
            _Name = name;
            _Code = code;
            _Size = size;
            _SizeName = sizeName;
            _Count = count;
            _Amount = amount;
            _CreateDate = createDate;
        }
        private DateTime _CreateDate;
        public DateTime CreateDate {
            get {
                return _CreateDate;
            }
            set {
                _CreateDate = value;
            }
        }
        private string _Name;

        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Code;

        public string Code {
            get { return _Code; }
            set { _Code = value; }
        }
        private string _Size;

        public string Size {
            get { return _Size; }
            set { _Size = value; }
        }
        private string _SizeName;

        public string SizeName {
            get { return _SizeName; }
            set { _SizeName = value; }
        }
        private int _Count;

        public int Count {
            get { return _Count; }
            set { _Count = value; }
        }
        private int _Amount;

        public int Amount {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
}
