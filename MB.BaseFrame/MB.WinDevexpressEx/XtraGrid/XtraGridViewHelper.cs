//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	XtraGridViewHelper 设置XTraGrid显示的样式处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Export;

using MB.BaseFrame;
using MB.XWinLib.Share;
using MB.XWinLib.DesignEditor;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
namespace MB.XWinLib.XtraGrid
{
    /// <summary>
    /// XtraGridViewHelper 设置XTraGrid显示的样式处理相关。
    /// </summary>
    public class XtraGridViewHelper
    {
        public static readonly string BAND_TYPE_NAME = "Band";

        #region 网格显示样式...
        public static readonly string NORMAL_SKIN_NAME = "Stardust";
        public static readonly string EDIT_SKIN_NAME = "Caramel";
        public static readonly string SEARCH_SKIN_NAME = "The Asphalt World";//"Glass Oceans";
        #endregion 网格显示样式...
        private static readonly string SGV_GROUP_PANEL_TITLE = CLL.Convert("把列拖到这里可以对它进行分组");

        private const int CARD_VIEW_WIDTH = 240;

        public static bool NotSetGridSkin;
        private static readonly string GRID_LAYOUT_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"UserSetting\";

        #region Instance...
        private static Object _Obj = new object();
        private static XtraGridViewHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected XtraGridViewHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static XtraGridViewHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new XtraGridViewHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...


        /// <summary>
        /// 设置指定的列是否显示列
        /// </summary>
        /// <param name="visibled"></param>
        /// <param name="columns"></param>
        public void SetGridViewColumnVisible(DevExpress.XtraGrid.Views.Grid.GridView gridView, bool visibled, params string[] columns) {
            if (columns == null || columns.Length == 0) return;
            foreach (DevExpress.XtraGrid.Columns.GridColumn dc in gridView.Columns) {
                if (Array.IndexOf(columns, dc.FieldName) < 0) continue;
                dc.Visible = visibled;
            }
        }

        #region public function...
        /// <summary>
        /// 设置GridView 控件的IndicatorWidth 。
        /// </summary>
        /// <param name="xtraGCtl"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public void SetGridIndicatorWidth(DevExpress.XtraGrid.GridControl xtraGrid, int maxIndex) {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null)
                return;
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(xtraGrid.Handle);
            System.Drawing.SizeF size = g.MeasureString(maxIndex.ToString(), gridView.PaintAppearance.Row.Font);

            gridView.IndicatorWidth = MB.Util.MyConvert.Instance.ToInt(size.Width) + 10; //在配置的基础上在加上10
            g.Dispose();
        }
        /// <summary>
        /// 设置grid的编辑方式。
        /// </summary>
        /// <param name="xtraGCtl"></param>
        /// <param name="autoNewRow"></param>
        /// <param name="enterMoveNext"></param>
        public void SetEditGridAutoInfo(DevExpress.XtraGrid.GridControl xtraGrid) {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null)
                return;
            gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridView.OptionsNavigation.AutoFocusNewRow = true;
            gridView.OptionsNavigation.AutoMoveRowFocus = true;
            gridView.OptionsNavigation.EnterMoveNextColumn = true;


        }
        /// <summary>
        /// 设置grid的编辑方式。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="autoNewRow"></param>
        public void SetGridViewNewItem(DevExpress.XtraGrid.GridControl xtraGrid, bool autoNewRow) {
            XtraContextMenuType viewPopuMenus = XtraContextMenuType.SaveGridState | XtraContextMenuType.Export | XtraContextMenuType.ColumnsAllowSort;
            if (autoNewRow)
                viewPopuMenus = viewPopuMenus | XtraContextMenuType.BatchAdd;

            SetGridViewNewItem(xtraGrid, autoNewRow, viewPopuMenus);
        }
        /// <summary>
        /// 设置grid的编辑方式。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="autoNewRow"></param>
        public void SetGridViewNewItem(DevExpress.XtraGrid.GridControl xtraGrid, bool autoNewRow, XtraContextMenuType appendMenus) {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null)
                return;
            gridView.OptionsView.NewItemRowPosition = autoNewRow ? DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom : DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridView.OptionsNavigation.AutoFocusNewRow = autoNewRow;
            gridView.OptionsNavigation.AutoMoveRowFocus = true;
            gridView.OptionsNavigation.EnterMoveNextColumn = true;


            //重新设置弹出菜单的操作项。
            GridControlEx grdEx = xtraGrid as GridControlEx;
            if (grdEx != null) {
                grdEx.ReSetContextMenu(appendMenus);
            }
        }
        /// <summary>
        /// 格式化动态创建列的样式。
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="previousCol">动态创建的字段在什么列之后。</param>
        /// <param name="activeLeftName"></param>
        /// <param name="canEdit"></param>
        public void FormatActiveColumns(DevExpress.XtraGrid.Views.Grid.GridView gridView, string previousCol, string activeLeftName, bool canEdit) {
            DevExpress.XtraGrid.Columns.GridColumn preCol = gridView.Columns[previousCol];
            int preIndex = preCol.VisibleIndex;
            foreach (DevExpress.XtraGrid.Columns.GridColumn aCol in gridView.Columns) {
                if (aCol.FieldName.IndexOf(activeLeftName) < 0) continue;
                aCol.VisibleIndex = ++preIndex;
                if (!canEdit) {
                    aCol.OptionsColumn.ReadOnly = true;
                    aCol.OptionsColumn.AllowFocus = false;
                    aCol.AppearanceHeader.ForeColor = Color.Black;
                    aCol.AppearanceCell.Font = new Font(aCol.AppearanceCell.Font, System.Drawing.FontStyle.Bold);
                    aCol.AppearanceCell.BackColor = Color.WhiteSmoke;
                }
            }
        }
        #endregion public function...

        #region XtraGrid UI 操作状态保存...
        /// <summary>
        /// 保存XtraGrid 控件的UI 操作状态。
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public void SaveXtraGridState(DevExpress.XtraGrid.GridControl xtraGrid) {
            GridLayoutManager.SaveXtraGridState(xtraGrid);
        }
        /// <summary>
        /// 恢复XtraGrid 控件的UI 操作保存状态。 
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public void RestoreXtraGridState(DevExpress.XtraGrid.GridControl xtraGrid) {
            GridLayoutManager.RestoreXtraGridState(xtraGrid);
        }
        //通过字段的Caption 找相应的列
        private DevExpress.XtraGrid.Columns.GridColumn findGridColumnByCaption(DevExpress.XtraGrid.Views.Grid.GridView gridView, string caption) {
            foreach (DevExpress.XtraGrid.Columns.GridColumn gCol in gridView.Columns) {
                if (string.Compare(gCol.Caption, caption, true) == 0)
                    return gCol;
            }
            return null;

        }

        #endregion XtraGrid UI 操作状态保存...

        #region CreateViewColumns...
        /// <summary>
        ///  创建 Xtra Grid 的column
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="dataSource"></param>
        /// <param name="colPropertys"></param>
        /// <param name="repositoryPicEdit"></param>
        /// <param name="editCols"></param>
        public void CreateViewColumns(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                      object repositoryPicEdit, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols) {

            CreateViewColumns(xtraGrid, dataSource, colPropertys, repositoryPicEdit, editCols, null);
        }
        /// <summary>
        /// 创建 Xtra Grid 的column。
        /// 
        /// </summary>
        /// <param name="grdCtl"></param>
        /// <param name="dataSource"></param>
        /// <param name="colPropertys"></param>
        /// <param name="repositoryPicEdit"></param>
        /// <param name="editCols"></param>
        public void CreateViewColumns(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                        object repositoryPicEdit, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols, MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo) {

            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            bool isPic = (repositoryPicEdit != null && string.Compare(repositoryPicEdit.GetType().Name, "RepositoryItemPictureEdit", true) == 0);
            if (gridView.Columns.Count > 0) {
                gridView.Columns.Clear();

                if(xtraGrid is GridControlEx)
                {
                   var gridControlEx=xtraGrid as GridControlEx;
                    gridControlEx.OnDefaultViewColumnsCleared();
                }
            }

            SetGridView(gridView, colPropertys);

            //先清空原先的设置信息,第一个为默认创建的
            if (xtraGrid.RepositoryItems.Count > 1) {
                for (int i = 1; i < xtraGrid.RepositoryItems.Count; i++) {
                    xtraGrid.RepositoryItems.RemoveAt(i);
                }
            }

            foreach (MB.WinBase.Common.ColumnPropertyInfo colInfo in colPropertys.Values) {
                if (!colInfo.Visibled)
                    continue;
                if (dataSource != null) {
                    IList lstData = dataSource as IList;
                    if (lstData != null) {
                        if (lstData.Count > 0) {
                            object entity = lstData[0];
                            //虽然是IList但是这个LIST里面其实是DataSet
                            if (entity is DataRowView)
                            {
                                DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(dataSource, string.Empty);
                                if (dtData != null)
                                {
                                    if (!dtData.Columns.Contains(colInfo.Name)) continue;
                                }
                            }
                            else if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, colInfo.Name))
                                continue;
                        }
                    }
                    else {
                        DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(dataSource, string.Empty);
                        if (dtData != null) {
                            if (!dtData.Columns.Contains(colInfo.Name)) continue;
                        }
                    }
                }
                DevExpress.XtraGrid.Columns.GridColumn col = gridView.Columns.Add();

                //默认情况下，先把Byte[] 类型
                if (string.Compare(colInfo.DataType, "System.Byte[]", true) == 0) {
                    col.ColumnEdit = repositoryPicEdit as DevExpress.XtraEditors.Repository.RepositoryItem;
                }
                else if (string.Compare(colInfo.DataType, "System.DateTime", true) == 0 ||
                    string.Compare(colInfo.DataType, "System.DateTime?", true) == 0)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateItem = 
                        new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                    dateItem.NullDate = DateTime.MinValue;
                    dateItem.NullText = string.Empty;
                    col.ColumnEdit = dateItem as DevExpress.XtraEditors.Repository.RepositoryItem;
                }
                else {
                    if (editCols != null && editCols.Count > 0 && editCols.ContainsKey(colInfo.Name)) {
                        MB.WinBase.Common.ColumnEditCfgInfo editIno = editCols[colInfo.Name];
                        if (editIno != null) {
                            DevExpress.XtraEditors.Repository.RepositoryItem rItem = XtraGridEditHelper.Instance.CreateEditItemByEditInfo(editIno, colInfo.DataType);
                            //设置它们的编辑项为只读的状态。这点很重要，因为创建可编辑的网格控件是通过  XtraGridEditHelper 来创建的。
                            //这里它只能是只读的。
                            rItem.ReadOnly = true;
                            // rItem.AllowFocused = true;
                            col.ColumnEdit = rItem;
                            xtraGrid.RepositoryItems.Add(rItem);
                        }
                    }
                }
                SetColumn(col, colInfo);
                if (gridViewLayoutInfo != null && gridViewLayoutInfo.GridLayoutColumns != null) {
                    var layoutInfo = gridViewLayoutInfo.GridLayoutColumns.FirstOrDefault<MB.WinBase.Common.GridColumnLayoutInfo>(o => o.Name == colInfo.Name);
                    if (layoutInfo != null) {
                        XtraGridViewHelper.Instance.SetColumnDisplayFormat(col, colInfo, layoutInfo);
                    }
                }
            }
            if (colPropertys != null) {
                SetGroupSummary(gridView, colPropertys);
            }
        }
        #endregion CreateViewColumns...

        #region XtraGrid Skin...
        /// <summary>
        /// 设置XtrGrid 的Skin....
        /// </summary>
        /// <param name="xtraGrd"></param>
        /// <param name="skin"></param>
        public void SetXtraGridSkin(DevExpress.XtraGrid.GridControl xtraGrid, XtraGridSkin skin) {
            //if (NotSetGridSkin)
            //    return;
            //xtraGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //xtraGrid.LookAndFeel.UseDefaultLookAndFeel = false;
            //xtraGrid.LookAndFeel.UseWindowsXPTheme = false;
            //string skinName = string.Empty;
            //switch (skin) {
            //    case XtraGridSkin.Normal:
            //        skinName = NORMAL_SKIN_NAME;
            //        break;
            //    case XtraGridSkin.Edit:
            //        skinName = EDIT_SKIN_NAME;
            //        break;
            //    case XtraGridSkin.Query:
            //        skinName = SEARCH_SKIN_NAME;
            //        break;
            //    default:
            //        skinName = NORMAL_SKIN_NAME;
            //        break;
            //}
            //xtraGrid.LookAndFeel.SkinName = skinName;
        }
        #endregion XtraGrid Skin...


        #region Styles...

        /// <summary>
        /// 增加网格的条件样式。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="styleConditions"></param>
        public void AddStylesForConditions(DevExpress.XtraGrid.GridControl xtraGrid, List<MB.XWinLib.DesignEditor.XtraStyleConditionInfo> styleConditions)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (styleConditions == null || styleConditions.Count == 0) return;

            //清空已有的格式条件
            gridView.FormatConditions.Clear();

            foreach (MB.XWinLib.DesignEditor.XtraStyleConditionInfo info in styleConditions)
            {
                //如果存在DispColName 的话，需要也是处理，在本应该程序中是在GridView的RowCellStyle事件中进行处理。  
                //该条件用于动态Caption.By XiaoMin  (info.ColumnCaption != null && info.ColumnCaption.Length > 0 && info.ColumnCaption != info.ColumnName))
                if (info.IsByEvent)
                    continue;

                if (gridView.FormatConditions[info.Name] == null)
                {
                    var formatCondition= CreateFormatCondition(info, gridView);
                    if(formatCondition!=null)
                        gridView.FormatConditions.Add(formatCondition);
                }
            }
        }

        /// <summary>
        /// 生成条件样式
        /// </summary>
        /// <param name="info">XtraStyleConditionInfo</param>
        /// <param name="gridView">GridView</param>
        /// <returns></returns>
        public static StyleFormatCondition CreateFormatCondition(XtraStyleConditionInfo info, GridView gridView)
        {
            var col = gridView.Columns.ColumnByFieldName(info.ColumnName);

            if (col == null)
            {
                MB.Util.TraceEx.Write("Style Format condition 列名称的配置，在数据源中找不到对应的列，请检查。", MB.Util.APPMessageType.SysWarning);
                return null;
            }

            col.AppearanceCell.Options.UseTextOptions = true;
            DevExpress.XtraGrid.StyleFormatCondition formatCondition = new StyleFormatCondition(info.Condition,
            col, info.StyleName, info.Value, info.Value2, info.ApplyToRow);

            if (!string.IsNullOrEmpty(info.Expression))
            {
                formatCondition.Condition = FormatConditionEnum.Expression;
                formatCondition.Expression = info.Expression;
            }
            formatCondition.Tag = info.Name;
            formatCondition.Appearance.Font = info.ForeFont;
            formatCondition.Appearance.ForeColor = info.ForeColor;
            formatCondition.Appearance.BackColor = info.BackColor;
            
            return formatCondition;
        }

        /// <summary>
        /// 根据用户值的设置设置显示列的样式。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="arg"></param>
        /// <param name="styleConditions"></param>
        public void SetRowCellStyle(DevExpress.XtraGrid.GridControl xtraGrid, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs arg, Dictionary<string, MB.WinBase.Common.StyleConditionInfo> styleConditions) {


        }
        #endregion Styles...

        #region 扩展的Public 方法...

        /// <summary>
        /// 设置网格属性。
        /// </summary>
        /// <param name="pView"></param>
        /// <param name="pPropertys"></param>
        public void SetGridView(DevExpress.XtraGrid.Views.Grid.GridView gridView, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys) {
            gridView.GroupPanelText = SGV_GROUP_PANEL_TITLE;
            gridView.OptionsBehavior.KeepGroupExpandedOnSorting = true;
            //gridView.OptionsView.ShowFooter = true;
            gridView.OptionsView.ColumnAutoWidth = false;

        }
        /// <summary>
        /// 设置Card View 的属性
        /// </summary>
        /// <param name="pView"></param>
        public void SetCardView(DevExpress.XtraGrid.GridControl xtraGrid) {
            DevExpress.XtraGrid.Views.Card.CardView cardView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Card.CardView;
            cardView.CardWidth = CARD_VIEW_WIDTH;
            cardView.OptionsBehavior.FieldAutoHeight = true;
            cardView.OptionsView.ShowCardCaption = false;
            //cardView.DetailHeight = 160;

            // xtraGrid.Styles.AddReplace("FieldCaption", new DevExpress.Utils.ViewStyleEx("FieldCaption", "CardView", new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134))), "", true, false, false, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Top, null, System.Drawing.SystemColors.HotTrack, System.Drawing.SystemColors.WindowText, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal));
        }

        //设置汇总显示的信息
        public void SetGroupSummary(DevExpress.XtraGrid.Views.Grid.GridView gridView, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys) {
            ArrayList list = new ArrayList();

            foreach (DevExpress.XtraGrid.Columns.GridColumn dc in gridView.Columns) {
                int num;
                string name = string.Empty;
                if (!string.IsNullOrEmpty(colPropertys.Keys.FirstOrDefault(o => string.Compare(o, dc.FieldName, true) == 0))) {
                    name = dc.FieldName;
                }
                else {
                    name = interceptRightNumber(dc.FieldName, out num);
                }
                MB.WinBase.Common.ColumnPropertyInfo info = null;
                if (colPropertys.ContainsKey(name))
                    info = colPropertys[name];
                if (info != null && info.SummaryItem) {
                    //设置Total 汇总的信息
                    dc.SummaryItem.SummaryType = (DevExpress.Data.SummaryItemType)Enum.Parse(typeof(DevExpress.Data.SummaryItemType), info.SummaryItemType.ToString());
                    dc.SummaryItem.DisplayFormat = getSummmaryDisplayCaption(dc.SummaryItem.SummaryType) + "={0}";
                    //为了设置小计的显示而存储
                    list.Add(dc);
                }
                // 处理动态创建的列.
                if (info == null && dc.Tag != null) {
                    switch (dc.Tag.ToString()) {
                        case "System.Int32":
                        case "System.Decimal":
                            dc.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                            dc.SummaryItem.DisplayFormat = getSummmaryDisplayCaption(dc.SummaryItem.SummaryType) + "={0}";
                            list.Add(dc);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (list.Count > 0) {
                List<DevExpress.XtraGrid.GridSummaryItem> summaryItems = new List<GridSummaryItem>();
                for (int i = 0; i < list.Count; i++) {
                    DevExpress.XtraGrid.Columns.GridColumn dc = list[i] as DevExpress.XtraGrid.Columns.GridColumn;
                    if (dc != null) {
                        var sumItem = new DevExpress.XtraGrid.GridGroupSummaryItem(dc.SummaryItem.SummaryType, dc.FieldName, dc, getSummmaryDisplayCaption(dc.SummaryItem.SummaryType) + "={0}");
                        var gsumItem = new DevExpress.XtraGrid.GridGroupSummaryItem(dc.SummaryItem.SummaryType, dc.FieldName, null, getSummmaryDisplayCaption(dc.SummaryItem.SummaryType) + "={0}");
                        summaryItems.Add(sumItem);
                        summaryItems.Add(gsumItem);
                    }
                }
                gridView.GroupSummary.AddRange(summaryItems.ToArray());
            }
        }
        //根据统计汇总的类型获取对应的中文描述。
        private string getSummmaryDisplayCaption(DevExpress.Data.SummaryItemType type) {
            switch (type) {
                case DevExpress.Data.SummaryItemType.Sum:
                    return "总计";
                case DevExpress.Data.SummaryItemType.Count:
                    return "总数";
                case DevExpress.Data.SummaryItemType.Max:
                    return "最大";
                case DevExpress.Data.SummaryItemType.Min:
                    return "最小";
                case DevExpress.Data.SummaryItemType.Average:
                    return "平均";
                default:
                    return "小计";
            }
        }
        /// <summary>
        /// 设置显示列的样式
        /// </summary>
        /// <param name="pColumn"></param>
        /// <param name="pColInfo"></param>
        public void SetColumn(DevExpress.XtraGrid.Columns.GridColumn gridColumn, DataColumn dataColumn) {
            gridColumn.Caption = dataColumn.Caption;
            gridColumn.FieldName = dataColumn.ColumnName;
            gridColumn.Name = "XtCol" + dataColumn.ColumnName;
            gridColumn.OptionsColumn.ReadOnly = true;
            gridColumn.OptionsColumn.AllowEdit = false;
            //pColumn.OptionsFilter.AllowFilter = false;
            gridColumn.Width = 80;
            gridColumn.VisibleIndex = gridColumn.AbsoluteIndex;// _OrderIndex ++;
        }
        /// <summary>
        ///  设置显示列的样式 通过配置的XML文件
        /// </summary>
        /// <param name="pColumn"></param>
        /// <param name="pFieldInfo"></param>
        public void SetColumn(DevExpress.XtraGrid.Columns.GridColumn gridColumn, MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo) {
            SetColumn(gridColumn, colPropertyInfo, null);
        }
        public void SetColumn(DevExpress.XtraGrid.Columns.GridColumn gridColumn, MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, string columnName) {
            gridColumn.Caption = colPropertyInfo.Description;
            if (columnName != null) {
                gridColumn.FieldName = columnName;
            }
            else {
                gridColumn.FieldName = colPropertyInfo.Name;
            }
            //SetColumnDisplayFormat(gridColumn, colPropertyInfo);
            gridColumn.Name = "XtCol" + colPropertyInfo.Name;
            gridColumn.Width = colPropertyInfo.VisibleWidth;
            if (colPropertyInfo.Visibled && colPropertyInfo.VisibleWidth > 0) {
                gridColumn.VisibleIndex = colPropertyInfo.OrderIndex;
            }
            else {
                gridColumn.VisibleIndex = -1;
            }
            //pColumn.OptionsFilter.AllowFilter = false;
            gridColumn.OptionsColumn.ReadOnly = string.Compare(colPropertyInfo.DataType, "System.Byte[]", true) == 0;
            gridColumn.OptionsColumn.AllowEdit = string.Compare(colPropertyInfo.DataType, "System.Byte[]", true) == 0;
        }
        /// <summary>
        /// 设置列显示格式
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="colLayoutInfo"></param>
        public void SetColumnDisplayFormat(DevExpress.XtraGrid.Columns.GridColumn gridColumn, MB.WinBase.Common.ColumnPropertyInfo columnPropertyInfo,
            MB.WinBase.Common.GridColumnLayoutInfo colLayoutInfo) {
             if (colLayoutInfo != null)
                gridColumn.Caption = string.IsNullOrEmpty(colLayoutInfo.Text) ? columnPropertyInfo.Description : colLayoutInfo.Text;

             if (colLayoutInfo.DisplayFormat == null) return;

             MB.WinBase.Common.FormatType fType = colLayoutInfo.DisplayFormat.FormatType;
             DevExpress.Utils.FormatType formatType = DevExpress.Utils.FormatType.None; //add by aifang
             switch (fType) {
                 case MB.WinBase.Common.FormatType.Numeric:
                     formatType = DevExpress.Utils.FormatType.Numeric; //add by aifang
                     gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                     gridColumn.DisplayFormat.FormatString = colLayoutInfo.DisplayFormat.FormatString;
                     break;
                 case MB.WinBase.Common.FormatType.DateTime:
                     formatType = DevExpress.Utils.FormatType.DateTime; //add by aifang
                     gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                     if (!string.IsNullOrEmpty(colLayoutInfo.DisplayFormat.FormatString))
                         gridColumn.DisplayFormat.FormatString = colLayoutInfo.DisplayFormat.FormatString;
                     break;
                 case MB.WinBase.Common.FormatType.Custom:
                     formatType = DevExpress.Utils.FormatType.Custom; //add by aifang
                     gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                     if (!string.IsNullOrEmpty(colLayoutInfo.DisplayFormat.FormatString))
                     {
                         gridColumn.DisplayFormat.FormatString = colLayoutInfo.DisplayFormat.FormatString;
                     }
                     break;
                 default:
                     break;
             }

            //add by aifang 支持明细中日期格式显示
             if (gridColumn.ColumnEdit != null)
             {
                 gridColumn.ColumnEdit.EditFormat.FormatType = formatType;
                 gridColumn.ColumnEdit.DisplayFormat.FormatType = formatType;

                 if (!string.IsNullOrEmpty(colLayoutInfo.DisplayFormat.FormatString))
                 {
                     gridColumn.ColumnEdit.DisplayFormat.FormatString = colLayoutInfo.DisplayFormat.FormatString;
                     gridColumn.ColumnEdit.EditFormat.FormatString = colLayoutInfo.DisplayFormat.FormatString;

                     if (fType == MB.WinBase.Common.FormatType.DateTime) {
                         //只有在日期类型的格式中，格式化日期
                         DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateItem = (DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)gridColumn.ColumnEdit;
                         if (dateItem != null) dateItem.EditMask = colLayoutInfo.DisplayFormat.FormatString;
                     }
                 }
             }
            //end

             //背景颜色处理
             if (!string.IsNullOrEmpty(colLayoutInfo.BackColor)) {
                 Color bc = MB.Util.MyConvert.Instance.ToColor(colLayoutInfo.BackColor);
                 if (bc != Color.Empty) {
                     gridColumn.AppearanceHeader.BackColor = bc;
                 }
             }
             //字体颜色
             if (!string.IsNullOrEmpty(colLayoutInfo.ForeColor)) {
                 Color fc = MB.Util.MyConvert.Instance.ToColor(colLayoutInfo.ForeColor);
                 if (fc != Color.Empty) {
                     gridColumn.AppearanceHeader.ForeColor = fc;
                     gridColumn.AppearanceCell.ForeColor = fc;
                 }
             }
          
        }
        /// <summary>
        /// 设置控件显示的样式
        /// </summary>
        /// <param name="pDg"></param>
        public void SetStyles(DevExpress.XtraGrid.GridControl xtraGrid, bool defaultStyle) {

        }
        //
        /// <summary>
        /// 根据商品ID 甚至对应行的焦点.
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        public void SetGridViewFocusedRowHandle(DevExpress.XtraGrid.Views.Grid.GridView gridView, string columnName, string columnValue) {
            int count = gridView.RowCount;
            for (int index = 0; index < count; index++) {
                DataRow dr = gridView.GetDataRow(index);
                if (dr[columnName] == null || dr[columnName] == System.DBNull.Value)
                    continue;

                string val = dr[columnName].ToString();
                if (string.Compare(val, columnValue, true) == 0) {
                    gridView.FocusedRowHandle = index;
                    break;
                }
            }
        }
        #endregion 扩展的Public 方法...

        #region 列冻结处理相关...
        /// <summary>
        ///  冻结列
        /// </summary>
        /// <param name="girdView"></param>
        public void FixedFocusColumn(DevExpress.XtraGrid.GridControl xtraGrid) {
            DevExpress.XtraGrid.Views.Base.ColumnView girdView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
            if (girdView.FocusedColumn == null)
                return;

            int index = girdView.FocusedColumn.VisibleIndex;
            foreach (DevExpress.XtraGrid.Columns.GridColumn dc in girdView.Columns) {
                if (dc.VisibleIndex > index)
                    continue;
                dc.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
        }
        /// <summary>
        /// 取消冻结。
        /// </summary>
        /// <param name="xtraGrid"></param>
        public void UnSixedColumn(DevExpress.XtraGrid.GridControl xtraGrid) {
            DevExpress.XtraGrid.Views.Base.ColumnView girdView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
            if (girdView.FocusedColumn == null)
                return;
            for (int i = girdView.Columns.Count - 1; i > -1; i--) {
                girdView.Columns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            }
        }
        #endregion 列冻结处理相关...

        #region 内部函数处理...
        //根据devexpress 的列sort order 获取up.data.model下的SortOrderType.
        private MB.Util.DataOrderType getOrderTypeBy(DevExpress.Data.ColumnSortOrder order) {
            switch (order) {
                case DevExpress.Data.ColumnSortOrder.Ascending:
                    return MB.Util.DataOrderType.Ascending;
                case DevExpress.Data.ColumnSortOrder.Descending:
                    return MB.Util.DataOrderType.Descending;
                case DevExpress.Data.ColumnSortOrder.None:
                    return MB.Util.DataOrderType.None;
                default:
                    return MB.Util.DataOrderType.None;
            }
        }
        //根据 up.data.model下的SortOrderType.转换为devexpress 的列sort order
        private DevExpress.Data.ColumnSortOrder getOrderTypeBy(MB.Util.DataOrderType order) {
            switch (order) {
                case MB.Util.DataOrderType.Ascending:
                    return DevExpress.Data.ColumnSortOrder.Ascending;
                case MB.Util.DataOrderType.Descending:
                    return DevExpress.Data.ColumnSortOrder.Descending;
                case MB.Util.DataOrderType.None:
                    return DevExpress.Data.ColumnSortOrder.None;
                default:
                    return DevExpress.Data.ColumnSortOrder.None;
            }
        }
        //截取字符窜最右边的数字
        private string interceptRightNumber(string colName, out int number) {
            string str = colName;
            //判断是否为数据库中的动态字一段 ，动态字段以Field 开始
            if (str.Length > 5 && str.Substring(0, 5) == "Field") {
                number = 0;
                return str;
            }
            string sNum = "";
            for (int i = 0; i < colName.Length; i++) {
                char c = char.Parse(str.Substring(str.Length - 1, 1));
                int ascCode = (int)c;
                if (c < 48 | c > 57) { //数字的Asc 码
                    number = string.IsNullOrEmpty(sNum) ? 0 : int.Parse(sNum);
                    return str;
                }
                else {
                    sNum = c.ToString() + sNum;
                    str = str.Remove(str.Length - 1, 1);
                }
            }
            number = string.IsNullOrEmpty(sNum) ? 0 : int.Parse(sNum);
            return string.Empty;
        }
        #endregion 内部函数处理...
    }

    #region enum XtraGridSkin...
    public enum XtraGridSkin
    {
        Normal,
        Edit,
        Query
    }
    #endregion enum XtraGridSkin...
}
