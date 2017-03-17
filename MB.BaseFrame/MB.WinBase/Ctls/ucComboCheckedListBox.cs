//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-07-26。
// Description	:	下拉多选Combox。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;

namespace MB.WinBase.Ctls {
    /// <summary>
    /// 下拉多选Combox。
    /// </summary>
    [ToolboxItemFilter("System.Windows.Forms")]
    [ToolboxBitmap(typeof(ucComboCheckedListBox))]//"ucCheckedListCombox.bmp"
    public class ucComboCheckedListBox : UserControl {
        private const int MIN_SIZE = 50;
        private const int BEGIN_HEIGHT = 100;

        #region Private Fields
        private Panel _PnlBack;
        private Label _CaptionLabel;
        private Panel _PnlTree;
        private TextBox _TxtInputValue;
        private ButtonEx _BtnSelect;
        private CheckedListBox _CheckListBox;
        private LabelEx _LblSizingGrip;
        private Form _ComboDrowForm;

        private CheckBox _ChkAllowFilter;

        private string _BranchSeparator;
        private System.Drawing.Point _DragOffset;

        private string _ValueMember = "ID";
        private string _DisplayMember = "Name";
        private object _DataSource;
      
        //判断过滤是通过Code 来还是表达的文本来 
        private bool _FilterByCode = true;
        private bool _BeginFilter = true;
        private ToolTip _TipTLib;
        #endregion

        #region 自定义事件处理...
        private ItemCheckEventHandler _ItemCheck;
        private ItemCheckEventHandler _AfterItemChecked;
        public event ItemCheckEventHandler ItemCheck {
            add {
                _ItemCheck += value;
            }
            remove {
                _ItemCheck -= value;
            }
        }
        private void OnItemCheck(ItemCheckEventArgs e) {
            if (_ItemCheck != null) {
                _ItemCheck(this, e);
            }
        }
        public event ItemCheckEventHandler AfterItemChecked {
            add {
                _AfterItemChecked += value;
            }
            remove {
                _AfterItemChecked -= value;
            }
        }
        private void OnAfterItemChecked(ItemCheckEventArgs e) {
            if (_AfterItemChecked != null) {
                _AfterItemChecked(this, e);
            }
        }
        #endregion 自定义事件处理...

        #region 数据绑定和业务操作方法和属性...

        /// <summary>
        /// 数据显示键值
        /// </summary>
        [Browsable(true), Description("得到或者设置数据集中主键字段的名称"), Category("数据")]
        public  string ValueMember {
            get { return _ValueMember; }
            set { _ValueMember = value; }
        }
        /// <summary>
        /// 数据显示名称
        /// </summary>
        [Browsable(true), Description("得到或者设置数据集中显示字段的名称"), Category("数据")]
        public  string DisplayMember {
            get { return _DisplayMember; }
            set { _DisplayMember = value; }
        }
        /// <summary>
        /// 绑定数据源。
        /// </summary>
        [Browsable(true), Description("设置控件的数据源，在绑定的数据源中必须有字段ID->KEY值、Name->显示的文本、PrevID->上级ID"), Category("数据")]
        public object DataSource {
            get {
                return this._DataSource;

            }
            set {
                this._DataSource = value;
                if (value != null)
                    this.setDataSouce();
            }
        }
        #endregion

        #region Public 一般处理属性...
        private bool _OnlyItemChecked = false;
        /// <summary>
        /// 判断是否只有一个商品是选择的
        /// </summary>
        [Browsable(true), Description("判断是否只能选择Item")]
        public bool OnlyItemChecked {
            get {
                return _OnlyItemChecked;
            }
            set {

                _OnlyItemChecked = value;
            }
        }
        /// <summary>
        /// 判断是否只有一个商品是选择的
        /// </summary>
        [Browsable(true), Description("判断输入的字符选择是匹配Code还是匹配显示的文本。")]
        public bool FilterByCode {
            get {
                return _FilterByCode;
            }
            set {

                _FilterByCode = value;
            }
        }
        /// <summary>
        ///得到或者编辑一个树的节点集合 
        /// </summary>
        [Browsable(true), Description("得到或者编辑一个树的节点集合"), Category("TreeView"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor(typeof(TreeNodeCollection), typeof(TreeNodeCollection))]
        public CheckedListBox ListObject {
            get {
                return this._CheckListBox;
            }
        }
       /// <summary>
        /// 得到或者设置这个控件的文本
       /// </summary>
        [Browsable(true), Description("得到或者设置这个控件的文本")]
        public override string Text {
            get { return this._TxtInputValue.Text; }
            set {
                this._TxtInputValue.Text = value;
            }
        }
        /// <summary>
        /// 设置控件的文本是否为只读
        /// </summary>
        [Browsable(true), Description("设置控件的文本是否为只读")]
        public bool TextReadOnly {
            set {
                this._TxtInputValue.ReadOnly = value;
                if (value == true) {
                    this._TxtInputValue.Cursor = System.Windows.Forms.Cursors.Arrow;
                    this._TxtInputValue.DoubleClick += new EventHandler(toggleComboListView);

                }
                else {
                    this._TxtInputValue.Cursor = System.Windows.Forms.Cursors.IBeam;
                    this._TxtInputValue.DoubleClick -= new EventHandler(toggleComboListView);

                }
            }
        }
        /// <summary>
        /// 得到或者设置节点之间的分割符
        /// </summary>
        [Browsable(true), Description("得到或者设置节点之间的分割符"), Category("Appearance")]
        public string BranchSeparator {
            get { return this._BranchSeparator; }
            set {
                if (value != null && value.Length > 0)
                    this._BranchSeparator = value.Substring(0, 1);
            }
        }
        /// <summary>
        ///  标题描述
        /// </summary>
        public string CaptionText {
            get {
                return _CaptionLabel.Text;
            }
            set {
                _CaptionLabel.Text = value;
            }
        }
        /// <summary>
        ///  是否显示控件标题
        /// </summary>
        public bool CaptionVisible {
            get {
                return _CaptionLabel.Visible;
            }
            set {
                _CaptionLabel.Visible = value;
            }
        }
        #endregion

        #region 构造函数...
        public ucComboCheckedListBox() {
            this.InitializeComponent();

            this._BranchSeparator = @"\";
            this.TextReadOnly = false;
            this._TxtInputValue.TabIndex = 0;

            this._CaptionLabel.Visible = false;
        }
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            _TipTLib = new ToolTip();
            this.Name = "ComboBoxTree";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.comboBoxTree_Layout);
            // Initializing Controls
            this._PnlBack = new Panel();
            this._PnlBack.BorderStyle = BorderStyle.Fixed3D;
            this._PnlBack.BackColor = Color.White;
            this._PnlBack.AutoScroll = false;

            this._CaptionLabel = new Label();
            this._CaptionLabel.Size = new Size(100, 14);
            this._CaptionLabel.ForeColor = System.Drawing.Color.White;
            this._CaptionLabel.Font = new Font("Arial", 9F);
            this._CaptionLabel.BackColor = System.Drawing.Color.LightSteelBlue;  //Label的颜色设定

            this._ChkAllowFilter = new CheckBox();
            this._ChkAllowFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._ChkAllowFilter.Size = new Size(16, 16);
            this.Location = new Point(0, 0);
            this._ChkAllowFilter.Checked = true;
            this._ChkAllowFilter.CheckedChanged += new EventHandler(chkFilter_CheckedChanged);

            this._TipTLib.SetToolTip(_ChkAllowFilter, "选择它表示输入的时候进行数据过滤，否则不进行数据过滤操作。");

            this._TxtInputValue = new TextBox();
            this.Location = new Point(16, 0);

            this._TxtInputValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._TxtInputValue.KeyUp += new KeyEventHandler(tbSelectedValue_KeyUp);
            this._TxtInputValue.LostFocus += new EventHandler(tbSelectedValue_LostFocus);

            this._BtnSelect = new ButtonEx();
            this._BtnSelect.Click += new EventHandler(toggleComboListView);
            this._BtnSelect.TabIndex = 2;
            this._BtnSelect.FlatStyle = FlatStyle.Flat;

            this._LblSizingGrip = new LabelEx();
            this._LblSizingGrip.Size = new Size(9, 9);
            this._LblSizingGrip.BackColor = Color.Transparent;
            this._LblSizingGrip.Cursor = Cursors.SizeNWSE;
            this._LblSizingGrip.MouseMove += new MouseEventHandler(sizingGripMouseMove);
            this._LblSizingGrip.MouseDown += new MouseEventHandler(sizingGripMouseDown);

            this._CheckListBox = new CheckedListBox();
            this._CheckListBox.BorderStyle = BorderStyle.None;

            this._CheckListBox.Location = new Point(16, 0);
            this._CheckListBox.LostFocus += new EventHandler(comboListViewLostFocus);
            this._CheckListBox.ItemCheck += new ItemCheckEventHandler(clstComboList_ItemCheck);
            this._CheckListBox.MouseUp += new MouseEventHandler(clstComboList_MouseUp);

            this._CheckListBox.KeyPress += new KeyPressEventHandler(clstComboList_KeyPress);

            this._CheckListBox.Dock = DockStyle.Fill;
            this._CheckListBox.CheckOnClick = true;
            this._CheckListBox.TabIndex = 1;


            this._ComboDrowForm = new Form();
            this._ComboDrowForm.FormBorderStyle = FormBorderStyle.None;
            this._ComboDrowForm.StartPosition = FormStartPosition.Manual;
            this._ComboDrowForm.ShowInTaskbar = false;
            this._ComboDrowForm.BackColor = System.Drawing.Color.White;// System.Drawing.SystemColors.Control;

            this._PnlTree = new Panel();
            this._PnlTree.BorderStyle = BorderStyle.FixedSingle;
            this._PnlTree.BackColor = Color.White;
            this._PnlTree.Dock = DockStyle.Fill;

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            // Adding Controls to UserControl
            this._PnlTree.Controls.Add(this._LblSizingGrip);
            this._PnlTree.Controls.Add(this._CheckListBox);

            this._ComboDrowForm.Controls.Add(this._PnlTree);
            this._PnlBack.Controls.AddRange(new Control[] { this._ChkAllowFilter, _TxtInputValue, _BtnSelect, _CaptionLabel });
            this.Controls.Add(this._PnlBack);

            this.ResumeLayout(false);

        }
        #endregion

        #region 界面处理...
        private void relocateGrip() {
            this._LblSizingGrip.Top = this._ComboDrowForm.Height - _LblSizingGrip.Height - 1;
            this._LblSizingGrip.Left = this._ComboDrowForm.Width - _LblSizingGrip.Width - 1;
        }

        private void toggleComboListView(object sender, EventArgs e) {
            if (!this._ComboDrowForm.Visible) {
                Rectangle CBRect = this.RectangleToScreen(this.ClientRectangle);
                this._ComboDrowForm.Location = new System.Drawing.Point(CBRect.X, CBRect.Y + this._PnlBack.Height);
                
                try {
                    this._ComboDrowForm.Show();
                    this._ComboDrowForm.BringToFront();

                    this.relocateGrip();
                }
                catch (Exception ee) {
                    MB.Util.TraceEx.Write("显示ComboListView 的项出错!" + ee.Message, MB.Util.APPMessageType.SysErrInfo);
                }
            }
            else {
                this._ComboDrowForm.Hide();
            }
        }

        #endregion

        #region 事件处理...
        private void sizingGripMouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                int tvWidth, tvHeight;
                tvWidth = Cursor.Position.X - this._ComboDrowForm.Location.X;
                tvWidth = tvWidth + this._DragOffset.X;
                tvHeight = Cursor.Position.Y - this._ComboDrowForm.Location.Y;
                tvHeight = tvHeight + this._DragOffset.Y;

                if (tvWidth < MIN_SIZE)
                    tvWidth = MIN_SIZE;
                if (tvHeight < MIN_SIZE)
                    tvHeight = MIN_SIZE;

                this._ComboDrowForm.Size = new System.Drawing.Size(tvWidth, tvHeight);
                relocateGrip();
            }
        }

        private void sizingGripMouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                int OffsetX = System.Math.Abs(Cursor.Position.X - this._ComboDrowForm.RectangleToScreen(this._ComboDrowForm.ClientRectangle).Right);
                int OffsetY = System.Math.Abs(Cursor.Position.Y - this._ComboDrowForm.RectangleToScreen(this._ComboDrowForm.ClientRectangle).Bottom);

                this._DragOffset = new Point(OffsetX, OffsetY);
            }
        }

        private void comboListViewLostFocus(object sender, EventArgs e) {
            if (!this._BtnSelect.RectangleToScreen(this._BtnSelect.ClientRectangle).Contains(Cursor.Position)) {
                if (!_TxtInputValue.Focused) {
                    this._ComboDrowForm.Hide();
                }
            }
        }

        private void clstComboList_ItemCheck(object sender, ItemCheckEventArgs e) {
            OnItemCheck(e);
            if (e.CurrentValue != CheckState.Indeterminate) {
                string str = "";
                int count = _CheckListBox.Items.Count;
                for (int i = 0; i < count; i++) {
                    if (_OnlyItemChecked) {
                        if (i != e.Index) {
                            _CheckListBox.SetItemChecked(i, false);
                        }
                    }
                    bool b = _CheckListBox.GetItemChecked(i);
                    if ((b && i != e.Index) || (!b && i == e.Index)) {
                        str += "{" + _CheckListBox.Items[i].ToString() + "}";
                    }
                }
                _TxtInputValue.Text = str;
            }


        }
        private void clstComboList_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (_CheckListBox.SelectedIndex > -1) {
                    OnAfterItemChecked(null);
                }
            }
        }
  
        private void clstComboList_KeyPress(object sender, KeyPressEventArgs e) {
            string str = "";
            int count = _CheckListBox.Items.Count;
            if (e.KeyChar == (char)1) {
                for (int i = 0; i < count; i++) {
                    _CheckListBox.SetItemChecked(i, true);
                    str += "{" + _CheckListBox.Items[i].ToString() + "}";
                }
                _TxtInputValue.Text = str;
            }
            else if (e.KeyChar == (char)3) {
                for (int i = 0; i < count; i++) {
                    _CheckListBox.SetItemChecked(i, false);
                }
                _TxtInputValue.Text = str;
            }
        }



        private void comboBoxTree_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
            if (_CaptionLabel.Visible) {
                this.Height = _CaptionLabel.Height + this._TxtInputValue.Height + 8;
            }
            else {
                this.Height = this._TxtInputValue.Height + 8;
            }
            this._PnlBack.Size = new Size(this.Width, this.Height - 2);

            this._BtnSelect.Size = new Size(16, this._TxtInputValue.Height + 2);

            this._TxtInputValue.Width = this.Width - this._BtnSelect.Width - _ChkAllowFilter.Width - 6;

            if (!_CaptionLabel.Visible) {
                this._ChkAllowFilter.Location = new Point(2, 2);
                this._TxtInputValue.Location = new Point(2 + _ChkAllowFilter.Width, 2);
                this._BtnSelect.Location = new Point(this.Width - this._BtnSelect.Width - 4, 0);
            }
            else {
                this._ChkAllowFilter.Location = new Point(2, _CaptionLabel.Height + 2);
                this._TxtInputValue.Location = new Point(_ChkAllowFilter.Width + 2, _CaptionLabel.Height + 2);

                this._BtnSelect.Location = new Point(this.Width - this._BtnSelect.Width - 4, _CaptionLabel.Height);

            }

            _CaptionLabel.Location = new Point(0, 0);
            _CaptionLabel.Width = this.Width;
            this._ComboDrowForm.Size = new Size(this.Width, BEGIN_HEIGHT);
           
            this.relocateGrip();
        }
        private void chkFilter_CheckedChanged(object sender, EventArgs e) {
            bool b = MB.Util.General.IsInDesignMode();
            if (!b) {
                if (_ChkAllowFilter.Checked) {
                    filterItems();
                }
            }
        }

        #endregion

        #region 扩展的public 方法...

        #region Items Checked 相关...
        /// <summary>
        /// 通过Keys值设置items 的Checked 
        /// </summary>
        /// <param name="pKeys"></param>
        public void SetItemsChecked(IList keyValues) {
            int count = _CheckListBox.Items.Count;
            for (int i = 0; i < count; i++) {
                CheckListItem item = (CheckListItem)_CheckListBox.Items[i];
                bool b = keyValues.Contains(item._KeyValue);
                _CheckListBox.SetItemChecked(i, b);
            }
        }
        /// <summary>
        /// 让所有的Items 在Checked 或者UnChecked 状态
        /// </summary>
        /// <param name="pChecked"></param>
        public void SetItemsAllChecked(bool bChecked) {
            int count = _CheckListBox.Items.Count;
            for (int i = 0; i < count; i++) {
                _CheckListBox.SetItemChecked(i, bChecked);
            }
        }
        /// <summary>
        /// 通过Index 来设置Item 的Checked 状态
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pCheched"></param>
        public void SetItemsChecked(int index, bool bChecked) {
            _CheckListBox.SetItemChecked(index, bChecked);
        }
        /// <summary>
        /// 得到checked 的item 的key值
        /// </summary>
        /// <returns></returns>
        public string[] GetAllCheckedItemsKey() {
            List<string> list = new List<string>();
            foreach (CheckListItem item in _CheckListBox.CheckedItems) {
                list.Add(item._KeyValue);
            }
            return list.ToArray() ;
        }
        /// <summary>
        /// 得到所有的checked 的item
        /// </summary>
        /// <returns></returns>
        public IList GetAllCheckedItems() {
            return _CheckListBox.CheckedItems;
        }
        #endregion Items Checked 相关...
        /// <summary>
        /// 得到CheckedListItem的所有项 
        /// </summary>
        public IList Items {
            get {
                return _CheckListBox.Items;
            }
        }
        #endregion 扩展的public 方法...

        #region 内部业务方法处理...
        private DataTable innerDataSource() {
            if (_DataSource == null) {
                return null;
            }
            string typeName = _DataSource.GetType().Name;
            switch (typeName) {
                case "DataSet":
                    DataSet ds = _DataSource as DataSet;
                    return ds.Tables[0];
                case "DataTable":
                    return _DataSource as DataTable;
                case "DataView":
                    DataView dv = _DataSource as DataView;
                    return dv.Table;
                default:
                    throw new MB.Util.APPException(string.Format("ucCheckedListCombox 控件目前不支持 {0} 类型的 数据源绑定操作",typeName));
            }
        }
        private void setDataSouce() {
            DataTable dt = innerDataSource();
            if (dt != null) {
                if (!dt.Columns.Contains(_ValueMember))
                    throw new MB.Util.APPException(string.Format("当前绑定的数据源中不包含配置字段名称{0}",_ValueMember));
                if (!dt.Columns.Contains(_DisplayMember))
                    throw new MB.Util.APPException(string.Format("当前绑定的数据源中不包含配置字段名称{0}", _DisplayMember));

                //判断该树型控件上是否已经存在节点
                _CheckListBox.Items.Clear();
                DataRow[] drsData = dt.Select();
                foreach (DataRow dr in drsData) {
                    string key = dr[_ValueMember].ToString();
                    string txt = dr[_DisplayMember].ToString();
                    CheckListItem item = new CheckListItem(key, txt);

                    _CheckListBox.Items.Add(item, false);
             
                }
            }
        }

        #endregion 内部方法处理...

        #region Label控件扩展...
        private class LabelEx : Label {
            /// <summary>
            /// 
            /// </summary>
            public LabelEx() {
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.DoubleBuffer, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="e"></param>
            protected override void OnPaint(PaintEventArgs e) {
                base.OnPaint(e);
                System.Windows.Forms.ControlPaint.DrawSizeGrip(e.Graphics, System.Drawing.Color.Black, 1, 0, this.Size.Width, this.Size.Height);
            }
        }
        #endregion

        #region Button控件扩展...
        private class ButtonEx : Button {
            ButtonState state;

            /// <summary>
            /// 
            /// </summary>
            public ButtonEx() {
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.DoubleBuffer, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMouseDown(MouseEventArgs e) {
                state = ButtonState.Pushed;
                base.OnMouseDown(e);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMouseUp(MouseEventArgs e) {
                state = ButtonState.Normal;
                base.OnMouseUp(e);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="e"></param>
            protected override void OnPaint(PaintEventArgs e) {
                base.OnPaint(e);
                System.Windows.Forms.ControlPaint.DrawComboButton(e.Graphics, 0, 0, this.Width, this.Height, state);
            }
        }

        #region 创建下拉列表框的键和值的结构...
        /// <summary>
        /// CheckListItem  下拉列表框增加需要的结构
        /// </summary>
        private struct CheckListItem {
            public string _Description;
            public string _KeyValue;
            public CheckListItem(int keyValue, string description) {
                _Description = description;
                _KeyValue = keyValue.ToString();
            }
            public CheckListItem(string keyValue, string description) {
                _Description = description;
                _KeyValue = keyValue;
            }
            /// <summary>
            /// 覆盖该方法是为了使该对象的值在下拉列表框中显示出来
            /// </summary>
            /// <returns></returns>
            public override string ToString() {
                return _Description;
            }
        }
        #endregion 创建下拉列表框的键和值的结构...
        #endregion

        #region 输入过滤查询选择处理...
        private void tbSelectedValue_LostFocus(object sender, EventArgs e) {
            bool b = this._BtnSelect.RectangleToScreen(this._BtnSelect.ClientRectangle).Contains(Cursor.Position);
            if (this._ComboDrowForm.Visible) {
                b = b || this._CheckListBox.RectangleToScreen(this._CheckListBox.ClientRectangle).Contains(Cursor.Position);
            }
            if (!b) {
                if (!_BeginFilter) {
                    this._ComboDrowForm.Hide();
                }
            }
        }
        private void tbSelectedValue_KeyUp(object sender, KeyEventArgs e) {

            DataTable dataDt = innerDataSource();
            if (dataDt == null) {
                return;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up
                || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.ShiftKey
                || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Escape) {
                return;
            }
            if (e.KeyCode == Keys.Enter) {
                if (!_ChkAllowFilter.Checked)
                    filterItems();
                int count = _CheckListBox.Items.Count;
                for (int i = 0; i < count; i++) {
                    _CheckListBox.SetItemChecked(i, true);
                }
                OnAfterItemChecked(null);
                SendKeys.Send("{TAB}");
            }
            if (!_ChkAllowFilter.Checked) {
                return;
            }
            filterItems();

        }
        private void filterItems() {
            DataTable dataDt = innerDataSource();
            if (dataDt == null) {
                return;
            }
            if (_TxtInputValue.Text.Trim().IndexOf('{') > -1) {
                //表示正在显示状态
                return;
            }
            _CheckListBox.Items.Clear();
            if (string.IsNullOrEmpty(_TxtInputValue.Text)) {
                setDataSouce();
                return;
            }

            _BeginFilter = true;
            foreach (DataRow dr in dataDt.Rows) {
                string key = dr[_ValueMember].ToString();
                string txt = dr[_DisplayMember].ToString();
                string filterText = getFilterStr(txt);
                bool b = matchString(filterText, _TxtInputValue.Text);
                if (b) {
                    CheckListItem item = new CheckListItem(key, txt);
                    _CheckListBox.Items.Add(item, false);
                    if (!this._ComboDrowForm.Visible) {
                        Rectangle CBRect = this.RectangleToScreen(this.ClientRectangle);
                        this._ComboDrowForm.Location = new System.Drawing.Point(CBRect.X, CBRect.Y + this._PnlBack.Height);
                        this._ComboDrowForm.Owner = this.ParentForm;
                        this._ComboDrowForm.Show();
                        //this._ComboDrowForm.BringToFront();

                        this.relocateGrip();
                        this._TxtInputValue.Focus();
                    }

                }
            }
            _BeginFilter = false;

        }
        private bool matchString(string matchStr, string patternStr) {
            string match = getFilterStr(matchStr);
            patternStr = patternStr.Replace('*', '.');//如果输入是*号当做一个要查询的字符
            patternStr = patternStr.Replace('_', '.');//如果输入是_号当做一个要查询的字符
            patternStr = patternStr.Replace("%", ".+");//如果输入是%号当做一个要查询的字符
            patternStr = patternStr.Replace(";", "|^");
            patternStr = patternStr.Replace(",", "|^");
            Regex check = new Regex(@"D", RegexOptions.Compiled);
            string pattern = @"^" + patternStr;
            try {
                Regex re = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                return re.IsMatch(match, 0);
            }
            catch {
                return false;
            }

        }
        private string getFilterStr(string itemText) {
            string tem = itemText;
            if (_FilterByCode) {
                //目前处理假设 CODE都存储在文本最后的括号之间
                int left = itemText.LastIndexOf('(');
                if (left > -1) {
                    tem = itemText.Substring(left + 1, itemText.Length - left - 2);
                }
            }
            return tem;
        }//
        #endregion 输入过滤查询选择处理...
 
    }
}
