using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace WinTestProject {
    public partial class testBandGridView : DevExpress.XtraEditors.XtraForm {
        private List<ProductInfo> _LstEntitys;
        private MB.WinClientDefault.Common.UIDynamicColumnBinding<ProductInfo> _DataBinding;
        private MB.WinBase.Data.HViewDataConvert<ProductInfo> _DataConvert;
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;
        private string xmlFileName = "testHViewDataConvert";

        public testBandGridView() {
            InitializeComponent();
            _LstEntitys = getOrgDataSource();

            _DataBinding = new MB.WinClientDefault.Common.UIDynamicColumnBinding<ProductInfo>(gridControlEx1, xmlFileName);
            DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView(gridControlEx1);
            gridControlEx1.MainView = advBandedView;
            _DataBinding = new MB.WinClientDefault.Common.UIDynamicColumnBinding<ProductInfo>(gridControlEx1, xmlFileName);
            _DataBinding.ReadOnly = false;


            MB.WinBase.Data.HViewConvertCfgParam cfgPars = MB.WinBase.LayoutXmlConfigHelper.Instance.GetHViewConvertCfgParam(xmlFileName, string.Empty);
            cfgPars.DynamicColumnCaption = false;
            MB.WinBase.Data.HViewDataConvert<ProductInfo> convert = new MB.WinBase.Data.HViewDataConvert<ProductInfo>(cfgPars);
            _DataBinding.CreateDataBinding(convert, _LstEntitys, "SDynamicColumn");



            //_DataBinding = new MB.WinClientDefault.Common.UIDynamicColumnBinding<ProductInfo>(gridControlEx1, "testHViewDataConvert");
            //MB.WinBase.Data.HViewConvertCfgParam cfgPars = MB.WinBase.LayoutXmlConfigHelper.Instance.GetHViewConvertCfgParam("testHViewDataConvert", string.Empty);
            //_DataConvert = new MB.WinBase.Data.HViewDataConvert<ProductInfo>(cfgPars);
            //_DataBinding.CreateDataBinding(_DataConvert, _LstEntitys, "Default");


            //MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(gridControlEx1, _LstEntitys, "testAdvBandGridView");
            
            //List<ProductInfo> lstData = _LstEntitys;
            //_DataBinding.CreateDataBinding(_DataConvert, lstData, "Default");  

            DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandGridView = gridControlEx1.MainView as
                                                DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
            bandGridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(bandGridView_RowClick);
            bandGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(bandGridView_CellValueChanged);
        }

        void bandGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) {
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandGridView = gridControlEx1.MainView as
                                                DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
            var row = bandGridView.GetRow(e.RowHandle) as ProductInfo;
        }

        void bandGridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e) {
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandGridView = gridControlEx1.MainView as
                                                DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
            var row = bandGridView.GetRow(e.RowHandle) as ProductInfo;
            
        }



        private List<ProductInfo> getOrgDataSource() {
            List<ProductInfo> lstData = new List<ProductInfo>();
            lstData.Add(new ProductInfo("红玫瑰", DateTime.Parse("2009-11-09"), "0001", "01", "S", 9, 354));
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

    }
}