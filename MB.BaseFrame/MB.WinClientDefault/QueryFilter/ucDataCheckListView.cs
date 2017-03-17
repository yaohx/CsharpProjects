//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-03
// Description	:	浏览数据选择。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.IFace;
using MB.WinBase.Atts;
using MB.WinBase.Common;
using MB.Util.Model;
using System.Collections;
using MB.WinBase.Binding;
using MB.XWinLib.XtraGrid;
using DevExpress.XtraGrid.Columns;
namespace MB.WinClientDefault.QueryFilter
{
    /// <summary>
    /// 浏览数据选择。
    /// </summary>
    public partial class ucDataCheckListView : UserControl, IDataAssistantListControl
    {
        private static readonly string ENTITY_SELECTED_PROPERTY = "Selected";
        private bool _MultiSelect;

        private MB.WinBase.Common.ColumnEditCfgInfo _ColumnEditCfgInfo;

        #region 自定义事件相关...
        private GetObjectDataAssistantEventHandle _AfterSelectData;
        public event GetObjectDataAssistantEventHandle AfterSelectData
        {
            add
            {
                _AfterSelectData += value;
            }
            remove
            {
                _AfterSelectData -= value;
            }
        }
        private void onAfterSelectData(GetObjectDataAssistantEventArgs arg)
        {
            if (_AfterSelectData != null)
                _AfterSelectData(this, arg);
        }
        #endregion 自定义事件相关...

        public ucDataCheckListView()
        {
            InitializeComponent();

            grdCtlMain.Dock = DockStyle.Fill;

            gridViewMain.CustomFilterDialog += new DevExpress.XtraGrid.Views.Grid.CustomFilterDialogEventHandler(_GridView_CustomFilterDialog);
        }

        private void _GridView_CustomFilterDialog(object sender, DevExpress.XtraGrid.Views.Grid.CustomFilterDialogEventArgs e) {
            MyXtraGridFilterDialog dialog = new MyXtraGridFilterDialog(e.Column);
            dialog.ShowDialog();
            e.FilterInfo = new ColumnFilterInfo(dialog.GetCustomFiltersCriterion());
            e.Handled = true;
        }

        /// <summary>
        /// 设置数据浏览
        /// </summary>
        /// <param name="clientRule"></param>
        /// <param name="dataSource"></param>
        public void SetDataSource(MB.WinBase.IFace.IClientRuleQueryBase clientRule, object dataSource)
        {
            //在数据小助手中实现动态列的设置，原来是取Default, clientRule.UIRuleXmlConfigInfo.GetDefaultColumns()
            Dictionary<string, ColumnPropertyInfo> bindingPropertys = MB.XWinLib.XtraGrid.XtraGridDynamicHelper.Instance.GetDynamicColumns(clientRule);

            MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(grdCtlMain, dataSource,
                                                    bindingPropertys, clientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit,
                                                    clientRule.ClientLayoutAttribute.UIXmlConfigFile, true);

            

            // gridViewMain.OptionsSelection.MultiSelect = _MultiSelect;
            if (_MultiSelect)
            {
                MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.AppendEditSelectedColumn(grdCtlMain);
            }

            //绑定右键菜单项 add by aifang 2012-08-22 begin
            if (!_ColumnEditCfgInfo.HideContextMenu)
            {
                Menu.MyContextMenuStrip strip = null;
                if (_ColumnEditCfgInfo.DataAssistantContextMenu == null || string.IsNullOrEmpty(_ColumnEditCfgInfo.DataAssistantContextMenu.Type))
                {
                    strip = new Menu.MyContextMenuStrip();
                    strip.ColumnEditCfgInfo = _ColumnEditCfgInfo;
                    strip.ParentControl = this.grdCtlMain;
                    this.grdCtlMain.ContextMenuStrip = strip;
                }
                else
                {
                    string type = _ColumnEditCfgInfo.DataAssistantContextMenu.Type;
                    string[] typePars = type.Split(',');

                    string param = _ColumnEditCfgInfo.DataAssistantContextMenu.TypeConstructParams;
                    string[] conPars = string.IsNullOrEmpty(param) ? null : param.Split(',');

                    object obj = MB.Util.DllFactory.Instance.LoadObject(typePars[0], conPars, typePars[1]);
                    if ((obj as Menu.MyContextMenuStrip) == null)
                        throw new MB.Util.APPException(" ClickButtonInput 弹出的自定义菜单必须继承自 MB.WinClientDefault.Menu.MyContextMenuStrip 控件", Util.APPMessageType.DisplayToUser);

                    strip = obj as Menu.MyContextMenuStrip;
                    strip.ColumnEditCfgInfo = _ColumnEditCfgInfo;
                    strip.ParentControl = this.grdCtlMain;
                    //为自定义的ContextMenuStrip添加复制单元格的命令
                    ToolStripMenuItem itemCopyCell = new ToolStripMenuItem();
                    itemCopyCell.Name = "tsmiCopyCell";
                    itemCopyCell.Text = "复制单元格";
                    strip.Items.Add(itemCopyCell);

                    this.grdCtlMain.ContextMenuStrip = strip;
                }
            }
            //绑定右键菜单项 add by aifang 2012-08-22 end
        }
        /// <summary>
        /// 是否可以多选。
        /// </summary>
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect;
            }
            set
            {
                _MultiSelect = value;
            }
        }

        /// <summary>
        /// XML配置信息
        /// </summary>
        public ColumnEditCfgInfo ColumnEditCfgInfo
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
        /// <summary>
        ///  获取当前选择的行。
        /// </summary>
        /// <returns></returns>
        public object[] GetSelectRows()
        {
            var rows = GetSelectRowsWithIndex();
            return rows == null ? null : rows.Values.ToArray();
        }
        /// <summary>
        ///  获取当前选择的行。
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, object> GetSelectRowsWithIndex()
        {
            if (gridViewMain.RowCount == 0) return null;

            IDictionary<int, object> rows = new Dictionary<int, object>();
            if (_MultiSelect)
            {
                object obj = gridViewMain.GetRow(0);
                if (string.Compare(obj.GetType().Name, "DataRowView", true) == 0)
                {
                    for (int i = 0; i < gridViewMain.RowCount; i++)
                    {
                        object entity = gridViewMain.GetRow(i);
                        System.Data.DataRowView drRow = entity as System.Data.DataRowView;
                        if (!drRow.Row.Table.Columns.Contains(ENTITY_SELECTED_PROPERTY))
                            return null;
                        if (MB.Util.MyConvert.Instance.ToBool(drRow[ENTITY_SELECTED_PROPERTY]))
                            rows.Add(i, entity);
                    }
                }
                else
                {
                    for (int i = 0; i < gridViewMain.RowCount; i++)
                    {
                        object entity = gridViewMain.GetRow(i);
                        if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, ENTITY_SELECTED_PROPERTY))
                            return null;
                        object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, ENTITY_SELECTED_PROPERTY);
                        if (MB.Util.MyConvert.Instance.ToBool(val))
                            rows.Add(i, entity);
                    }
                }
            }
            else
            {
                int[] handles = gridViewMain.GetSelectedRows();
                //for (int i = 0; i < handles.Length; i++)
                //    rows.Add(gridViewMain.GetRow(handles[i]));
                if (handles.Length > 0)
                    rows.Add(0, gridViewMain.GetRow(handles[0]));
            }
            return rows;
        }

        public void CheckListViewItems(IEnumerable<int> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                return;
            }
            bool check = true;
            foreach (int index in selectedIds)
            {
                object row = gridViewMain.GetRow(index);
                if (string.Compare(row.GetType().Name, "DataRowView", true) == 0)
                {
                    System.Data.DataRowView drRow = row as System.Data.DataRowView;
                    if (drRow.Row.Table.Columns.Contains(ENTITY_SELECTED_PROPERTY))
                        drRow[ENTITY_SELECTED_PROPERTY] = check;
                }
                else
                {
                    if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(row, ENTITY_SELECTED_PROPERTY))
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(row, ENTITY_SELECTED_PROPERTY, check);

                }

            }

            gridViewMain.RefreshData();
        }

        private DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo _HitInfo;
        private void grdCtlMain_DoubleClick(object sender, EventArgs e)
        {
            if (_HitInfo != null && _HitInfo.InRow && _HitInfo.RowHandle >= 0)
            {
                object[] rows = GetSelectRows();
                onAfterSelectData(new GetObjectDataAssistantEventArgs(rows));
            }
        }

        private void grdCtlMain_MouseMove(object sender, MouseEventArgs e)
        {
            _HitInfo = gridViewMain.CalcHitInfo(new Point(e.X, e.Y));
        }

        private void gridViewMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                int index = e.RowHandle + 1;
                e.Info.DisplayText = index.ToString();
            }
        }
        /// <summary>
        /// 批量选择当前网格上的数据。
        /// </summary>
        /// <param name="check"></param>
        public void CheckListViewItem(bool check)
        {
            int count = gridViewMain.RowCount;
            for (int index = 0; index < count; index++)
            {
                object row = gridViewMain.GetRow(index);
                if (string.Compare(row.GetType().Name, "DataRowView", true) == 0)
                {
                    System.Data.DataRowView drRow = row as System.Data.DataRowView;
                    if (drRow.Row.Table.Columns.Contains(ENTITY_SELECTED_PROPERTY))
                        drRow[ENTITY_SELECTED_PROPERTY] = check;
                }
                else
                {
                    if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(row, ENTITY_SELECTED_PROPERTY))
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(row, ENTITY_SELECTED_PROPERTY, check);

                }

            }

            gridViewMain.RefreshData();
        }

        /// <summary>
        /// 在多选状态下，保证选中的条数直接生效
        /// 而不是在移开焦点的时候才生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewMain_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_MultiSelect && string.Compare(e.Column.FieldName, "Selected") == 0)
            {
                ((DevExpress.XtraGrid.Views.Grid.GridView)sender).SetRowCellValue(e.RowHandle, e.Column, e.Value);
            }

        }
    }
}
