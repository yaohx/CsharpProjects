using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.Util;
using System.Collections;

namespace MB.WinBase.Ctls
{
    [ToolboxItem(true)]
    public partial class ucPopupRegionEdit : UserControl
    {
        private const int BEGIN_LEVEL = 1; //起始级别
        private RegionEditInfo _CurRegionEditInfo;

        private ColumnEditCfgInfo _ColumnEditCfgInfo;
        private Dictionary<int, RegionType> _dicRegion = new Dictionary<int, RegionType>();
        private RegionType _CurRegionType;

        private RegionEditInfo _CurRegionEdit = new RegionEditInfo();
        System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        DevExpress.XtraEditors.PopupContainerControl popupContainerControl;

        private Action<RegionEditInfo> _AfterLinkIsClicked;
        /// <summary>
        /// 当点击区域控件的链接以后触发
        /// </summary>
        public event Action<RegionEditInfo> AfterLinkIsClicked {
            add {
                _AfterLinkIsClicked += value;
            }
            remove {
                _AfterLinkIsClicked -= value;
            }
        }




        public ucPopupRegionEdit()
        {
            InitializeComponent();

            popupContainerControl = new DevExpress.XtraEditors.PopupContainerControl();
            popupContainerControl.Size = new Size(336, 208);

            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;

            popupContainerControl.Controls.Add(flowLayoutPanel);

            flowLayoutPanel.AutoScroll = true;

            this.popupContainerEdit1.Properties.PopupControl = popupContainerControl;

            this.popupContainerEdit1.QueryPopUp += new CancelEventHandler(popupContainerEdit1_QueryPopUp);

            this.Resize += new EventHandler(ucPopupRegionEdit_Resize);
            this.Layout += new LayoutEventHandler(ucPopupRegionEdit_Layout);
            //this.Load += new EventHandler(ucPopupRegionEdit_Load);
        }

        void ucPopupRegionEdit_Layout(object sender, LayoutEventArgs e)
        {
            this.Height = popupContainerEdit1.Height;
        }

        void ucPopupRegionEdit_Resize(object sender, EventArgs e)
        {
            this.Height = popupContainerEdit1.Height;
        }

        void ucPopupRegionEdit_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 获取或者设置Click Button 调用的方法描述。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MB.WinBase.Common.ColumnEditCfgInfo ColumnEditCfgInfo
        {
            get
            {
                return _ColumnEditCfgInfo;
            }
            set
            {
                _ColumnEditCfgInfo = value;
            }
        }

        [System.Diagnostics.DebuggerHidden()]
        public override string Text
        {
            get
            {               
                return this.popupContainerEdit1.Text;
            }
            set
            {
                this.popupContainerEdit1.Text = value;
            }
        }

        [System.Diagnostics.DebuggerHidden()]
        public bool Enabled
        {
            get {
                return this.popupContainerEdit1.Enabled;
            }
            set {
                this.popupContainerEdit1.Enabled = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RegionEditInfo CurRegionEdit
        {
            get { return _CurRegionEdit; }
            set { _CurRegionEdit = value;

                SetTextValue();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region 初始化数据.....
        public void InitControlData()
        {
            _BaseDataBindingEdit = getDirectnessBaseEdit(this);

            if (_BaseDataBindingEdit != null)
            {
                RegionEditInfo regionEditInfo = this._CurRegionEdit;
                object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;
                if (currentEditEntity == null) return;

                foreach (var data in _ColumnEditCfgInfo.EditCtlDataMappings)
                {
                    object obj = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, data.ColumnName);
                    MyReflection.Instance.InvokePropertyForSet(regionEditInfo, data.SourceColumnName, obj);
                }

                this._CurRegionEdit = regionEditInfo;
            }
        }

        private void SetTextValue()
        {
            string txt = string.Empty;
            if (!string.IsNullOrEmpty(_CurRegionEdit.Country)) txt = _CurRegionEdit.Country;
            if (!string.IsNullOrEmpty(_CurRegionEdit.Province)) txt += " " + _CurRegionEdit.Province;
            if (!string.IsNullOrEmpty(_CurRegionEdit.City)) txt += " " + _CurRegionEdit.City;
            if (!string.IsNullOrEmpty(_CurRegionEdit.District)) txt += " " + _CurRegionEdit.District;

            this.popupContainerEdit1.Text = txt;
        }
        private void SetCtrlReadOnly()
        {
            if (MB.Util.General.IsInDesignMode() || _BaseDataBindingEdit.CurrentEditType == ObjectEditType.DesignDemo) return;

            //判断是否存在单据状态属性
            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(_BaseDataBindingEdit.CurrentEditEntity);
            if (exists)
            {
                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(_BaseDataBindingEdit.CurrentEditEntity);

                this.popupContainerEdit1.Enabled = (docState == MB.Util.Model.DocState.Progress);
            }
            if (_BaseDataBindingEdit.CurrentEditType == ObjectEditType.OpenReadOnly)
            {
                this.popupContainerEdit1.Enabled = false;
            }
        }
        #endregion 初始化数据.....

        void popupContainerEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (e.Cancel) return;

            InitControlData();

            InitPopupControl();

            SetLinkBinding(RegionSource);
        }

        private void InitPopupControl()
        {
            if (_dicRegion.Count <= 0)
            {
                //初始化选择级别
                for (int i = BEGIN_LEVEL; i <= _ColumnEditCfgInfo.Level; i++)
                {
                    RegionType regionType = new RegionType();
                    regionType.ParentRegionLevel = i - 1;
                    regionType.RegionLevel = i;
                    _dicRegion.Add(i, regionType);
                }
            }

            _CurRegionType = _dicRegion[BEGIN_LEVEL];

            if (_CurRegionEdit != null) initCurRegionType();          
        }

        #region 初始化区域对应级别值
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
        #endregion

        private IBaseEditForm _BaseDataBindingEdit;
        private IBaseEditForm getDirectnessBaseEdit(Control ctl)
        {
            if (ctl.Parent == null)
            {
                return null;
            }
            IBaseEditForm parent = ctl.Parent as IBaseEditForm;
            if (parent != null)
            {
                return parent;
            }
            return this.getDirectnessBaseEdit(ctl.Parent);
        }

        #region private Event

        private void SetLinkBinding(InvokeDataSourceDescInfo invokeDataSourceDescInfo)
        {
            DataSet ds = getRegionSource(invokeDataSourceDescInfo);

            this.flowLayoutPanel.Controls.Clear();
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
                this.flowLayoutPanel.Controls.Add(lnkRegion);
            }

            if (!_CurRegionType.RegionLevel.Equals(BEGIN_LEVEL))
            {
                LinkLabel lnkReturn = new LinkLabel();
                lnkReturn.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
                lnkReturn.TextAlign = ContentAlignment.MiddleLeft;
                lnkReturn.Width = 105;

                lnkReturn.Name = "lnkReturn";
                lnkReturn.Text = "返回上一级";

                lnkReturn.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkReturn_LinkClicked);
                this.flowLayoutPanel.Controls.Add(lnkReturn);
            }
        }

        void lnkRegion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            LinkLabel lnkRegion = (LinkLabel)sender;

            string code = ((DataRow)lnkRegion.Tag)["CODE"].ToString();
            ResetCurRegionEditInfo(code);

            _CurRegionType.RegionId = MB.Util.MyConvert.Instance.ToInt(((DataRow)lnkRegion.Tag)["ID"]);
            _CurRegionType.Region = code;

            _dicRegion[_CurRegionType.RegionLevel] = _CurRegionType;

            if (_dicRegion.ContainsKey(_CurRegionType.RegionLevel + 1)) {
                _CurRegionType = _dicRegion[_CurRegionType.RegionLevel + 1];
                _CurRegionType.ParentRegionLevel = _CurRegionType.RegionLevel - 1;

                SetLinkBinding(this.RegionSource);
            }
            else this.popupContainerEdit1.ClosePopup();

            onAfterLinkIsClicked(this._CurRegionEdit);
        }

        void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_dicRegion.ContainsKey(_CurRegionType.RegionLevel - 1))
            {
                _CurRegionType.RegionId = 0;
                _CurRegionType.Region = "";

                ResetCurRegionEditInfo(string.Empty);

                _CurRegionType = _dicRegion[_CurRegionType.RegionLevel - 1];

                SetLinkBinding(this.RegionSource);
            }
            onAfterLinkIsClicked(this._CurRegionEdit);
        }

        private void onAfterLinkIsClicked(RegionEditInfo curRegionEdit) {
            if (_AfterLinkIsClicked != null) {
                _AfterLinkIsClicked(curRegionEdit);
            }
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
            if (!string.IsNullOrEmpty(code))
                _CurRegionEdit.Level = _CurRegionType.RegionLevel.ToString();
            else
                _CurRegionEdit.Level = (_CurRegionType.RegionLevel - 1).ToString();
            setSelectedValue();
        }

        private void setSelectedValue()
        {
            if (_BaseDataBindingEdit != null)
            {
                object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;

                foreach (var data in _ColumnEditCfgInfo.EditCtlDataMappings)
                {
                    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(_CurRegionEdit, data.SourceColumnName);
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(currentEditEntity, data.ColumnName, val);
                }

            }

            SetTextValue();
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
}
