using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestEditColor : Form 
    {
        private List<MyTestColorInfo> _LstEntitys;
 
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;
        private string xmlFileName = "TestColor";

        public frmTestEditColor() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {

            List<MyTestColorInfo> lstData = getOrgDataSource();
            var detailBindingParams = new MB.XWinLib.GridDataBindingParam(gridControlEx1, lstData, false);

            _ColPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
            var editCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(_ColPropertys, xmlFileName);
            var gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(xmlFileName, string.Empty);
            MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.CreateEditXtraGrid(detailBindingParams, _ColPropertys, editCols, gridViewLayoutInfo);
        }

        private void frmTestEditColor_Load(object sender, EventArgs e) {

        }
        private List<MyTestColorInfo> getOrgDataSource() {
            List<MyTestColorInfo> lstData = new List<MyTestColorInfo>();
            lstData.Add(new MyTestColorInfo(1, 0));
            lstData.Add(new MyTestColorInfo(2, 0));
            lstData.Add(new MyTestColorInfo(3, 0));
            lstData.Add(new MyTestColorInfo(4, 0));
       
            return lstData;
        }
    }

    class MyTestColorInfo {
        public MyTestColorInfo(int id, int val) {
            _ID = id;
            _Color = val;
        }
        private int _ID;

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private decimal _Color;

        public decimal Color {
            get { return _Color; }
            set { _Color = value; }
        }

    }

    public class myForm : MB.WinBase.IFace.IForm
    {
        public MB.WinBase.IFace.IClientRuleQueryBase ClientRuleObject {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
   
        public MB.WinBase.Common.ClientUIType ActiveUIType {
            get {
                return MB.WinBase.Common.ClientUIType.ShowModelForm;
                }
        }

        public void Close() {
            throw new NotImplementedException();
        }
    }
}
