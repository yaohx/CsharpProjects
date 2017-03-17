//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	扩展 DevExpress.XtraGrid.GridControl 控件,主要是为了提供给 XtraGridEx 使用的。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Grid.Handler;
using DevExpress.XtraGrid.Menu ;
using MB.WinBase.Common;
using DevExpress.XtraGrid.Columns;
using MB.WinBase;

namespace MB.XWinLib.XtraGrid{
	/// <summary>
	/// 扩展 DevExpress.XtraGrid.GridControl 控件,主要是为了提供给 XtraGridEx 使用的。
	/// </summary>
	[ToolboxItem(true)]
	public class GridControlEx : DevExpress.XtraGrid.GridControl {
        public static Color ODD_ROW_BACK_COLOR = Color.FromArgb(245, 245, 250);   
		#region 变量定义...
		private bool _IsDesign;
		private bool _ValidedDeleteKeyDown;//判断在
		private DevExpress.XtraGrid.Views.Grid.GridView _GridView;
		private int _LocationIndex = 0;
		private int _FirstDockHandle;
		private GridViewOptionsInfo _CurrentInfo;
		private System.Windows.Forms.MenuItem _OptionMenu;
        private System.Windows.Forms.MenuItem _CopyCellMenu;

		private bool _ShowOptionMenu;
		private string _OptionsCFGName = string.Empty;
		private const int FIRST_LOCATION_TOP = 15;
		private const int MAX_WAIT_TIME = 2000;
		private Form _ParentFrm = null;
		private Form _ParentMdiFrm;
		
		private bool _HasInitilizeGridLoad; //这个变量用来避免Gird的初始化被执行无数次
		private  IGridExDataIOControl _DataIOControl;
        private string _XmlCfgFileName;
        private Dictionary<string, ColumnEditCfgInfo> _ColEidtPros; //编辑列的属性

        private System.Guid _InstanceIdentity;
		#endregion 变量定义...

		#region 扩展的自定义事件处理相关...
		private GridControlExMenuEventHandle _BeforeContextMenuClick;
		public event GridControlExMenuEventHandle BeforeContextMenuClick{
			add{
				_BeforeContextMenuClick +=value;
			}
			remove{
				_BeforeContextMenuClick -=value;
			}
		}
		public void OnBeforeContextMenuClick(GridControlExMenuEventArg arg){
			if(_BeforeContextMenuClick!=null)
				_BeforeContextMenuClick(this,arg);
		}

        
        public event EventHandler DefaultViewColumnsCleared;

        public void OnDefaultViewColumnsCleared()
        {
            if(DefaultViewColumnsCleared!=null)
            {
                DefaultViewColumnsCleared(this, new EventArgs());
            }
        }

		#endregion 扩展的自定义事件处理相关...

		#region 构造函数...
		/// <summary>
		/// 构造函数
		/// </summary>
		public GridControlEx(){
          

            _IsDesign = MB.Util.General.IsInDesignMode();
			if(!_IsDesign){
				//在运行阶段设置本地化语言应用
                //DevExpress.XtraGrid.Localization.GridLocalizer.Active = MB.XWinLib.Localization.GridLocal.Create();
			}
			//_IgnoreKeyOnDataImport = true;

            
		}
		#endregion 构造函数...

		#region 覆盖基类的方法...
        protected override void OnLoaded() {
            base.OnLoaded();
            if (_IsDesign) return;
            if (base.ContextMenu == null) {
                XtraContextMenu xMenu = new XtraContextMenu(this,XtraContextMenuType.Copy | XtraContextMenuType.Export | XtraContextMenuType.ExportAsTemplet |
                    XtraContextMenuType.DataImport | XtraContextMenuType.SaveGridState | XtraContextMenuType.ColumnsAllowSort |
                    XtraContextMenuType.Chart | XtraContextMenuType.ChartDesign);
                base.ContextMenu = xMenu.GridContextMenu;
            }

            if (_HasInitilizeGridLoad || !_ShowOptionMenu) return;

            _HasInitilizeGridLoad = true;
            _ParentFrm = MB.WinBase.ShareLib.Instance.GetControlParentForm(this);
            if (_ParentFrm == null)
                return;

            MB.WinBase.IFace.IForm viewFrm = _ParentFrm as MB.WinBase.IFace.IForm;
            if (viewFrm != null && viewFrm.ClientRuleObject != null) {
                _XmlCfgFileName = viewFrm.ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile;
                _OptionsCFGName = viewFrm.GetType().FullName + "~" + viewFrm.ClientRuleObject.GetType().FullName + "~" + this.Name + ".xml";
                var layOutXmlHelper = LayoutXmlConfigHelper.Instance;
                var colPros = layOutXmlHelper.GetColumnPropertys(_XmlCfgFileName);
                var colEditPros = layOutXmlHelper.GetColumnEdits(colPros, _XmlCfgFileName);
            }
            else
                _OptionsCFGName = _ParentFrm.GetType().FullName + "~" + this.Name + ".xml";

            _CurrentInfo = GridViewOptionsHelper.Instance.CreateFromXMLToInfo(_OptionsCFGName);

            _GridView = this.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

            if (_GridView != null) {
                _GridView.OptionsView.EnableAppearanceEvenRow = true;

                _GridView.Appearance.EvenRow.BackColor = ODD_ROW_BACK_COLOR;
                // _GridView.OptionsView.EnableAppearanceEvenRow = true;
                //  _GridView.OptionsView.EnableAppearanceOddRow = true; 
                _GridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(_GridView_RowCellStyle);
                _GridView.CustomFilterDialog += new DevExpress.XtraGrid.Views.Grid.CustomFilterDialogEventHandler(_GridView_CustomFilterDialog);
            }

            setGridViewByOptions();
            createOptionsMenu(ContextMenu);
            createCopyCellMenu(ContextMenu);

        }


        void _GridView_CustomFilterDialog(object sender, DevExpress.XtraGrid.Views.Grid.CustomFilterDialogEventArgs e) {
            MyXtraGridFilterDialog dialog = new MyXtraGridFilterDialog(e.Column);
            dialog.ShowDialog(this);
            e.FilterInfo = new ColumnFilterInfo(dialog.GetCustomFiltersCriterion());
            e.Handled = true;
        }


        void _GridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e) {
            //if (e.RowHandle < 0) return;
            //if ((e.RowHandle % 2) != 0 && _GridView.FocusedRowHandle != e.RowHandle)
            //    e.Appearance.BackColor = ODD_ROW_BACK_COLOR;   
        }

		/// <summary>
		///  内置菜单应用..
		/// </summary>
		public override ContextMenu ContextMenu {
			get {
				return base.ContextMenu;
			}
			set {
				base.ContextMenu = value;
			}
		}
		/// <summary>
		/// 判断并处理Delete 键的情况。
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e) {
			if(_ValidedDeleteKeyDown && (e.KeyCode == System.Windows.Forms.Keys.Delete)){
               // MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.DeleteFocusedRow(this);    
			}
			base.OnKeyDown (e);
		}
		#endregion 覆盖基类的方法...

		#region 扩展的Public 方法...
		/// <summary>
		/// 重新设置弹出窗口的菜单项。
		/// </summary>
		/// <param name="createMenus"></param>
		public virtual void ReSetContextMenu(XtraContextMenuType   createMenus){
			XtraContextMenu xMenu = new XtraContextMenu(this,createMenus);
			base.ContextMenu = xMenu.GridContextMenu;
			createOptionsMenu(ContextMenu);
            createCopyCellMenu(ContextMenu);
		}
		#endregion 扩展的Public 方法...

		#region 扩展的属性...
        /// <summary>
        /// 当前实例的身份标记。
        /// 必须是一个固定的身份，不可以通过 System.Guid.NewGuid() 产生。
        /// </summary>
        [Browsable(false)]
        public System.Guid InstanceIdentity {
            get {
                
                return _InstanceIdentity;
            }
            set {
                _InstanceIdentity =  value;
            }

        }
		/// <summary>
		/// 获取或设置网格数据IO操作时控制的对象。
		/// </summary>
		[Description("获取或设置网格数据IO操作时控制的对象。")] 
		public IGridExDataIOControl  DataIOControl{
			get{
				return _DataIOControl;
			}
			set{
				_DataIOControl = value;
			}
		}

		/// <summary>
		/// 获取控件所在的窗口。
		/// </summary>
		[Browsable(false)]
		public virtual System.Windows.Forms.Form ParentForm{
			get{
				if(!_IsDesign){
					return MB.WinBase.ShareLib.Instance.GetControlParentForm(this);
				}
				else
					return null;
			}
		}
        /// <summary>
        /// 设置或获取当用户按下Deleted 键时是否删除Grid 上的记录.
        /// </summary>
		[Description("设置或获取当用户按下Deleted 键时是否删除Grid 上的记录。")] 
		public bool ValidedDeleteKeyDown{
			get{
				return _ValidedDeleteKeyDown;
			}
			set{
				_ValidedDeleteKeyDown = value;
			}
		}
        /// <summary>
        /// 设置或获取是否显示选项控制菜单
        /// </summary>
		[Description("设置或获取是否显示选项控制菜单")] 
		public bool ShowOptionMenu{
			get{
				return _ShowOptionMenu;
			}
			set{
				_ShowOptionMenu = value;
				
			}
		}
		#endregion 扩展的属性...

		#region 内部函数和事件处理...
		//创建固定的选项目菜单
		private void createOptionsMenu(ContextMenu contextMenu){
			if(!_ShowOptionMenu ||  contextMenu==null)
				return;
			if(_OptionMenu==null){
				_OptionMenu = new MenuItem();
				_OptionMenu.Text = "选项";
				_OptionMenu.Click +=new EventHandler(_OptionMenu_Click);
			}
			if(!contextMenu.MenuItems.Contains(_OptionMenu)){
				contextMenu.MenuItems.Add("-");
				contextMenu.MenuItems.Add(_OptionMenu);
			}
		}
 
		private void _OptionMenu_Click(object sender, EventArgs e) {
			if(_OptionsCFGName==null || _OptionsCFGName.Length ==0)
				return;

            FrmXtraGridViewOptions frm = new FrmXtraGridViewOptions(_GridView,_CurrentInfo, _OptionsCFGName);
			
            var result=frm.ShowDialog();

            // 确定,应用条件样式
            if (result == DialogResult.OK)
            {
                setGridViewByOptions();
            }

            // 取消,重新加载配置文件
            if (result == DialogResult.Cancel)
            {
                _CurrentInfo = GridViewOptionsHelper.Instance.CreateFromXMLToInfo(_OptionsCFGName);
            }
 
		}

        #region 复制单元格右键菜单处理
        /// <summary>
        ///  对每一个Grid都添加复制单元格的功能
        /// </summary>
        /// <param name="contextMenu">网格控件的context menu</param>
        private void createCopyCellMenu(ContextMenu contextMenu) {
            if (contextMenu == null)
                return;
            if (_CopyCellMenu == null) {
                _CopyCellMenu = new MenuItem();
                _CopyCellMenu.Text = "复制单元格";
                _CopyCellMenu.Click += new EventHandler(_CopyCellMenu_Click);
            }
            if (!contextMenu.MenuItems.Contains(_CopyCellMenu)) {
                contextMenu.MenuItems.Add("-");
                contextMenu.MenuItems.Add(_CopyCellMenu);
            }
        }

        /// <summary>
        /// 赋值单元格右键菜单事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _CopyCellMenu_Click(object sender, EventArgs e) {
            try {
                ContextMenu menu = sender as ContextMenu;
                object value = getCellValueFromGrid();
                if (value != null)
                    Clipboard.SetText(value.ToString());
                //menu.
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("复制单元格失败," + ex.ToString());
                MB.WinBase.MessageBoxEx.Show(this, "复制单元格失败");
            }


        }

        /// <summary>
        /// 根据当前鼠标点击的单元格得到单元格的内容
        /// </summary>
        /// <returns></returns>
        private object getCellValueFromGrid() {
            return getCellValueFromGrid(string.Empty);
        }

        /// <summary>
        /// 根据属性名得到单元格的内容
        /// 可以显示的指定属性名字
        /// </summary>
        /// <param name="cfgPropertyName">单元格属性名，如果为空，则根据当前鼠标点击的单元格</param>
        /// <returns></returns>
        private object getCellValueFromGrid(string cfgPropertyName) {
            string propertyName = cfgPropertyName;
            object value = null;
            DevExpress.XtraGrid.GridControl grid = this;
            if (grid != null) {
                var view = grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view.FocusedRowHandle < 0) return null;

                object row = view.GetFocusedRow();
                if (string.IsNullOrEmpty(propertyName))
                    propertyName = view.FocusedColumn.FieldName;

                #region 对于ClickInputButton去实现的列，COPY的列的名字不是在GIRD中显示的名字。所以需要处理
                if (_ColEidtPros != null && _ColEidtPros.Count > 0) {
                    if (_ColEidtPros.ContainsKey(propertyName)) { 
                        string textFieldNameFromSource = _ColEidtPros[propertyName].TextFieldName;
                        var dataMappings = _ColEidtPros[propertyName].EditCtlDataMappings;
                        if (dataMappings != null && dataMappings.Count > 0) {
                            foreach (var dataMapping in dataMappings) {
                                if (dataMapping.ColumnName.CompareTo(textFieldNameFromSource) == 0) {
                                    propertyName = dataMapping.ColumnName;
                                }
                            }
                        }
                    }
                }
                #endregion

                if (string.Compare(row.GetType().Name, "DataRowView", true) == 0) {
                    System.Data.DataRowView drRow = row as System.Data.DataRowView;
                    value = drRow[propertyName];
                }
                else {
                    if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(row, propertyName))
                        value = MB.Util.MyReflection.Instance.InvokePropertyForGet(row, propertyName);
                }
            }
            return value;
        }
        #endregion

        private void setGridViewByOptions(){
			if(_GridView!=null && _CurrentInfo!=null){
				_GridView.OptionsView.ShowHorzLines =  _CurrentInfo.ShowHorzLines;
				_GridView.OptionsView.ShowVertLines = _CurrentInfo.ShowVertLines;
				_GridView.OptionsView.AllowCellMerge = _CurrentInfo.AllowCellMerge;
                //根据条件数值样式
                if (_CurrentInfo.StyleConditions != null && _CurrentInfo.StyleConditions.Count > 0) {
                    MB.XWinLib.XtraGrid.XtraGridViewHelper.Instance.AddStylesForConditions(this, _CurrentInfo.StyleConditions);     
                }
			}
		}
        private DevExpress.XtraGrid.Columns.GridColumn getColumnByStyleName(string name) {
           var col = _GridView.Columns.ColumnByFieldName(name);
           if (col != null)
               return col;
           else
               return null;
        }
		#endregion 内部函数和事件处理...
	}

	#region 自定义事件处理相关...
	public delegate void GridControlExMenuEventHandle(object sender,GridControlExMenuEventArg arg);
	public class GridControlExMenuEventArg{
		private bool _Handled;
        private DevExpress.XtraGrid.Columns.GridColumn _Column;  
		private XtraContextMenuType _MenuType ;
		public GridControlExMenuEventArg(XtraContextMenuType menuType){
			_MenuType = menuType;
		}
		public bool Handled{
			get{
				return _Handled;
			}
			set{
				_Handled = value;
			}
		}
		public XtraContextMenuType MenuType{
			get{
				return _MenuType;
			}
			set{
				_MenuType = value;
			}
		}
        /// <summary>
        /// 当前响应鼠标的列
        /// </summary>
        public DevExpress.XtraGrid.Columns.GridColumn Column {
            get {
                return _Column;
            }
            set {
                _Column = value;
            }
        }
	}
	#endregion 自定义事件处理相关...

	#region 数据导入控制相关...
	/// <summary>
	/// 实现网格数据导入导出的控制类。
	/// </summary>
	public interface IGridExDataIOControl{
		bool IgnoreKeyOnDataImport{get;set;}
		bool BeforeIOData();
		void AfterIOData();
	}
	public class GridExDataIOControl : IGridExDataIOControl{
		private bool _IgnoreKeyOnDataImport;
		public GridExDataIOControl() : this(true)
		{

		}
		public GridExDataIOControl(bool ignoreKeyOnDataImport){
			_IgnoreKeyOnDataImport = ignoreKeyOnDataImport;
		}
		public virtual bool BeforeIOData(){
			return true;
		}
		public virtual void AfterIOData(){
		}
		public bool IgnoreKeyOnDataImport{
			get{
				return _IgnoreKeyOnDataImport;
			}set{
				 _IgnoreKeyOnDataImport = value;
			 }
		}
	}
	#endregion 数据导入控制相关...
}

