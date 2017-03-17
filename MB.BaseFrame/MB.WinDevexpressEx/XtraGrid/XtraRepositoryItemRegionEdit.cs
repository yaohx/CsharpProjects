using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Repository;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.Util;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.Accessibility;

namespace MB.XWinLib.XtraGrid
{
    [UserRepositoryItem("Register")]
    public class XtraRepositoryItemRegionEdit : RepositoryItemPopupContainerEdit
    {
        internal const string EditorName = "XtraRegionEdit";
        private MB.WinBase.Common.ColumnEditCfgInfo _ColumnEditCfgInfo;

        static XtraRepositoryItemRegionEdit()
        {
            Register();
        }

        public XtraRepositoryItemRegionEdit()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnEditCfgInfo"></param>
        public XtraRepositoryItemRegionEdit(MB.WinBase.Common.ColumnEditCfgInfo columnEditCfgInfo)
        {
            _ColumnEditCfgInfo = columnEditCfgInfo;
        }

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(XtraRegionEdit), typeof(RepositoryItemPopupBase), 
                typeof(PopupBaseEditViewInfo), new ButtonEditPainter(), false, EditImageIndexes.TextEdit, typeof(BaseAccessible)));
        }
        public override string EditorTypeName
        {
            get
            {
                return EditorName;
            }
        }

        public override BaseEdit CreateEditor()
        {
            BaseEdit edit = base.CreateEditor();
            (edit as XtraRegionEdit).ColumnEditCfgInfo = _ColumnEditCfgInfo;

            //注册XtraRegionEdit点击链接以后的时间，动态创建代理
            if (_ColumnEditCfgInfo.InvokeDataSourceDesc != null) {
                try {
                    InvokeDataSourceDescInfo invokeDataSourceDescInfo = _ColumnEditCfgInfo.InvokeDataSourceDesc;
                    string[] arrType = invokeDataSourceDescInfo.Type.Split(new char[] { ',' });
                    string[] arrMethod = invokeDataSourceDescInfo.Method.Split(new char[] { ',' });
                    string[] arrParams = null;
                    if (arrMethod.Length > 1) arrParams = arrMethod[1].Split(new char[] { ';' });
                    object obj = MB.Util.DllFactory.Instance.LoadObject(arrType[0], arrType[1]);
                    System.Reflection.MethodInfo methodInfo = obj.GetType().GetMethod(arrMethod[0]);

                    Action<RegionEditInfo> del = Delegate.CreateDelegate(typeof(Action<RegionEditInfo>), methodInfo) as Action<RegionEditInfo>;
                    (edit as XtraRegionEdit).AfterLinkIsClicked += del;
                }
                catch (Exception ex) {
                    MB.Util.TraceEx.Write(string.Format("XtraRepositoryItemRegionEdit 动态加载事件出错, {0}", ex.ToString()));
                }
            }
            return edit;
        }
        //主要通过反射的方式创建一个新的 RepositoryItem（如 在该控件对应的查询窗口中）
        //通过覆盖它避免反射创建不成功 以及需要的参数为空
        protected override RepositoryItem CreateRepositoryItem()
        {
            return new XtraRepositoryItemRegionEdit(_ColumnEditCfgInfo);
        }

    }

    [ToolboxItem(false)] 
    public class XtraRegionEdit : PopupContainerEdit
    {
        #region 自定义变量...
        System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        DevExpress.XtraEditors.PopupContainerControl popupContainerControl; 
        private const int BEGIN_LEVEL = 1; //起始级别
        private Dictionary<int, RegionType> _dicRegion = new Dictionary<int, RegionType>();
        private RegionType _CurRegionType;
        private RegionEditInfo _CurRegionEdit = new RegionEditInfo();
        private int _SelectRegionLevel;

        private Dictionary<int, RegionEditInfo> _HasLoad = new Dictionary<int, RegionEditInfo>();

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

        private MB.WinBase.Common.ColumnEditCfgInfo _ColumnEditCfgInfo;
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
        #endregion 自定义变量...

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="editInfo"></param>
        public XtraRegionEdit()
        {
            this.Properties.AutoHeight = false;
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Properties.ShowPopupCloseButton = true;
            this.Properties.CloseOnOuterMouseClick = true;
            this.Name = "XtraRegionEdit";
        }

        #endregion 构造函数...

        #region ShowPopup...
        protected virtual void InitPopupForm()
        {
            popupContainerControl = new DevExpress.XtraEditors.PopupContainerControl();
            popupContainerControl.Size = new Size(336, 208);

            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;

            popupContainerControl.Controls.Add(flowLayoutPanel);

            flowLayoutPanel.AutoScroll = true;

            this.Properties.PopupControl = popupContainerControl;
        }

        /// <summary>
        /// 显示弹出界面
        /// </summary>
        public override void ShowPopup()
        {
            Control ctl = this.Parent;
            DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
            if (grdCtl != null)
            {
                IBindingList bindingList = grdCtl.DataSource as IBindingList;
                if (bindingList != null && !bindingList.AllowEdit)
                {
                    return;
                }
                DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (gridView.GetFocusedRow() == null)
                {
                    //通过这种变态的方法自动增加一个空行，同时保证不重复增加
                    bindingList.AddNew();
                }
            }

            InitPopupForm();
            InitPopupControl();

            SetLinkBinding(RegionSource);

            base.ShowPopup();
        }

        private void InitPopupControl()
        {
            if (_dicRegion.Count <= 0)
            {
                //初始化选择级别
                _SelectRegionLevel = _ColumnEditCfgInfo.Level;

                for (int i = BEGIN_LEVEL; i <= _SelectRegionLevel; i++)
                {
                    RegionType regionType = new RegionType();
                    regionType.ParentRegionLevel = i - 1;
                    regionType.RegionLevel = i;
                    _dicRegion.Add(i, regionType);
                }
            }

            _CurRegionType = _dicRegion[BEGIN_LEVEL];

            if (this.Properties.OwnerEdit != null)
            {
                Control ctl = this.Properties.OwnerEdit.Parent;
                DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
                if (grdCtl != null)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

                    _CurRegionEdit = new RegionEditInfo();

                    object mainEntity = gridView.GetFocusedRow();
                    if (mainEntity != null)
                    {
                        foreach (var data in _ColumnEditCfgInfo.EditCtlDataMappings)
                        {
                            object obj = MB.Util.MyReflection.Instance.InvokePropertyForGet(mainEntity, data.ColumnName);
                            MB.Util.MyReflection.Instance.InvokePropertyForSet(_CurRegionEdit, data.SourceColumnName, obj);
                        }
                    }
                    initCurRegionType();
                }
            }
        }
        #endregion

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
            else {
                if (this.Properties.OwnerEdit != null) this.Properties.OwnerEdit.ClosePopup();
            }
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
            if (this.Properties.OwnerEdit != null)
            {
                Control ctl = this.Properties.OwnerEdit.Parent;
                DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
                if (grdCtl != null)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                    object mainEntity = gridView.GetFocusedRow();
                    if (mainEntity != null)
                    {
                        foreach (var data in _ColumnEditCfgInfo.EditCtlDataMappings)
                        {
                            object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(_CurRegionEdit, data.SourceColumnName);
                            MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, data.ColumnName, val);
                        }

                        SetTextValue();

                        MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, _ColumnEditCfgInfo.Name, this.Text);
                    }
                }
            }
        }

        private void SetTextValue()
        {
            string txt = string.Empty;
            if (!string.IsNullOrEmpty(_CurRegionEdit.Country)) txt = _CurRegionEdit.Country;
            if (!string.IsNullOrEmpty(_CurRegionEdit.Province)) txt += " " + _CurRegionEdit.Province;
            if (!string.IsNullOrEmpty(_CurRegionEdit.City)) txt += " " + _CurRegionEdit.City;
            if (!string.IsNullOrEmpty(_CurRegionEdit.District)) txt += " " + _CurRegionEdit.District;

            this.Text = txt;
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
