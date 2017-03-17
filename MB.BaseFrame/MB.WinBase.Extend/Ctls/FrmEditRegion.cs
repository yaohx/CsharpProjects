using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using MB.Util;
using MB.WinBase.IFace;

namespace MB.WinBase.Extend.Ctls
{
    public partial class FrmEditRegion : Form
    {
        private const int BEGIN_LEVEL = 1; //起始级别

        private ucEditRegion _UcRegion;
        private Dictionary<int, RegionType> _dicRegion = new Dictionary<int, RegionType>();
        private RegionType _CurRegionType;

        private RegionEditInfo _CurRegionEdit;

        public FrmEditRegion(ucEditRegion ucRegion)
        {
            InitializeComponent();

            _UcRegion = ucRegion;
            _CurRegionEdit = ucRegion.CurRegionEditInfo;

            this.Load += new EventHandler(FrmEditRegion_Load);
            this.btnReturn.Click += new EventHandler(btnReturn_Click);
            this.btnClose.Click += new EventHandler(btnClose_Click);
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        void btnReturn_Click(object sender, EventArgs e)
        {
            if (_dicRegion.ContainsKey(_CurRegionType.RegionLevel - 1))
            {
                _CurRegionType.RegionId = 0;
                _CurRegionType.Region = "";

                ResetCurRegionEditInfo(string.Empty);

                _CurRegionType = _dicRegion[_CurRegionType.RegionLevel - 1];

                SetLinkBinding(this.RegionSource);
            }
        }

        void FrmEditRegion_Load(object sender, EventArgs e)
        {
            //初始化选择级别
            for (int i = BEGIN_LEVEL; i <= _UcRegion.SelectRegionLevel; i++)
            {
                RegionType regionType = new RegionType();
                regionType.ParentRegionLevel = i - 1;
                regionType.RegionLevel = i;
                _dicRegion.Add(i,regionType);
            }

            _CurRegionType = _dicRegion[BEGIN_LEVEL];

            initCurRegionType();

            this.btnReturn.Visible = _CurRegionType.RegionLevel != BEGIN_LEVEL;
        }

        private void initCurRegionType()
        {
            SetRegion(_CurRegionEdit.Country, BEGIN_LEVEL);
            SetRegion(_CurRegionEdit.Province, BEGIN_LEVEL + 1);
            SetRegion(_CurRegionEdit.City, BEGIN_LEVEL + 2);
            SetRegion(_CurRegionEdit.District, BEGIN_LEVEL + 3);
        }

        private void SetRegion(string code, int level)
        {
            if (!string.IsNullOrEmpty(code))
            {
                if (!_dicRegion.ContainsKey(level)) return;

                _CurRegionType = _dicRegion[level];

                DataSet ds = getRegionSource(RegionSource);
                DataRow[] drs = ds.Tables[0].Select("CODE = '" + code + "'");
                if (drs.Length > 0)
                {
                    _dicRegion[level].RegionId = MB.Util.MyConvert.Instance.ToInt(drs[0]["ID"]);
                    _dicRegion[level].Region = drs[0]["CODE"].ToString();
                }
            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="parent"></param>
        public void ShowFilterForm(IWin32Window parent)
        {
            this.Show(parent);

            SetLinkBinding(RegionSource);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region private Event

        private void SetLinkBinding(InvokeDataSourceDescInfo invokeDataSourceDescInfo)
        {
            DataSet ds = getRegionSource(invokeDataSourceDescInfo);

            this.flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                LinkLabel lnkRegion = new LinkLabel();
                lnkRegion.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
                lnkRegion.TextAlign = ContentAlignment.MiddleLeft;
                lnkRegion.Width = 105;

                lnkRegion.Name = "lnkRegion" + ds.Tables[0].Rows[i]["CODE"].ToString();
                lnkRegion.Text = ds.Tables[0].Rows[i]["Name"].ToString();
                lnkRegion.Tag = ds.Tables[0].Rows[i];

                lnkRegion.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkRegion_LinkClicked);
                this.flowLayoutPanel1.Controls.Add(lnkRegion);
            }

            this.btnReturn.Visible = _CurRegionType.RegionLevel != BEGIN_LEVEL;
        }

        private DataSet getRegionSource(InvokeDataSourceDescInfo invokeDataSourceDescInfo)
        {
            string[] arrType = invokeDataSourceDescInfo.Type.Split(new char[] { ',' });
            string[] arrMethod = invokeDataSourceDescInfo.Method.Split(new char[] { ',' });
            string[] arrParams = null;
            if (arrMethod.Length > 1) arrParams = arrMethod[1].Split(new char[] { ';' });
            IClientRuleQueryBase clientRule = (IClientRuleQueryBase)MB.Util.DllFactory.Instance.LoadObject(arrType[0], arrType[1]);
            var data = MyReflection.Instance.InvokeMethod(clientRule, arrMethod[0], arrParams);
            DataSet ds = ((DataSet)data).Copy();
            return ds;
        }

        private void ResetCurRegionEditInfo(string code)
        {
            if (_CurRegionType.RegionLevel == BEGIN_LEVEL)
            {
                _CurRegionEdit.Country = code;
            }
            else if (_CurRegionType.RegionLevel == BEGIN_LEVEL + 1)
            {
                _CurRegionEdit.Province = code;
            }
            else if (_CurRegionType.RegionLevel == BEGIN_LEVEL + 2)
            {
                _CurRegionEdit.City = code;
            }
            else if (_CurRegionType.RegionLevel == BEGIN_LEVEL + 3)
            {
                _CurRegionEdit.District = code;
            }
            else throw new MB.Util.APPException(string.Format("无法识别的区域级别 {0} ，目前只支持1-4级设置，请检查!", _CurRegionType.RegionLevel), APPMessageType.DisplayToUser);

            _UcRegion.CurRegionEditInfo = _CurRegionEdit;
        }

        void lnkRegion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnkRegion = (LinkLabel)sender;

            string code = ((DataRow)lnkRegion.Tag)["CODE"].ToString();
            ResetCurRegionEditInfo(code);

            _CurRegionType.RegionId = MB.Util.MyConvert.Instance.ToInt(((DataRow)lnkRegion.Tag)["ID"]);
            _CurRegionType.Region = code;

            _dicRegion[_CurRegionType.RegionLevel] = _CurRegionType;

            if (_dicRegion.ContainsKey(_CurRegionType.RegionLevel + 1))
            {
                _CurRegionType = _dicRegion[_CurRegionType.RegionLevel + 1];
                _CurRegionType.ParentRegionLevel = _CurRegionType.RegionLevel - 1;

                SetLinkBinding(this.RegionSource);
            }
            else this.Visible = false;
        }

        #endregion

        #region private property

        private InvokeDataSourceDescInfo _RegionSource = null;
        private InvokeDataSourceDescInfo RegionSource
        {
            get
            {
                if (_RegionSource == null)
                {
                    InvokeDataSourceDescInfo s = new InvokeDataSourceDescInfo();
                    s.Type = "MB.ERP.BaseLibrary.CSysSetting.UIRule.BfRegionUIRule,MB.ERP.BaseLibrary.CSysSetting.dll";
                    _RegionSource = s;
                }

                int regionId = -1;
                if (_dicRegion.ContainsKey(_CurRegionType.ParentRegionLevel)) regionId = _dicRegion[_CurRegionType.ParentRegionLevel].RegionId;
                _RegionSource.Method = "GetRegionInfos," + regionId + ";" + _CurRegionType.RegionLevel;

                return _RegionSource;
            }
        }


        #endregion

    }
    public class RegionType
    {
        private int _RegionId;
        public int RegionId
        {
            get { return _RegionId; }
            set { _RegionId = value; }
        }

        private int _RegionLevel;
        public int RegionLevel
        {
            get { return _RegionLevel; }
            set { _RegionLevel = value; }
        }

        private string _Region;
        public string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }

        private int _ParentRegionLevel;
        public int ParentRegionLevel
        {
            get { return _ParentRegionLevel; }
            set { _ParentRegionLevel = value; }
        }
    }
}
