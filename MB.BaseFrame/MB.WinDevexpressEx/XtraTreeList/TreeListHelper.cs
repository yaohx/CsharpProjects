//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-12-05
// Description	:	TreeListHelper  树型控件绑定处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 

using MB.XWinLib.XtraGrid;
namespace MB.XWinLib.XtraTreeList {
    /// <summary>
    /// 树型控件绑定处理
    /// </summary>
    public class TreeListHelper<T> {
             /// <summary>
        /// 树型控件绑定处理。
        /// </summary>
        /// <param name="trvList"></param>
        /// <param name="dataList"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="allowEdit"></param>
        public virtual void CreateDataBinding(DevExpress.XtraTreeList.TreeList trvList, object dataSource, string xmlFileName, bool allowEdit) {
            var cols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
            var editCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(cols, xmlFileName);
            var treeCfg = MB.WinBase.LayoutXmlConfigHelper.Instance.GetTreeListViewCfgInfo( xmlFileName,null);

            CreateDataBinding(trvList, dataSource, cols, editCols, treeCfg, allowEdit);

        }
        /// <summary>
        ///  树型控件绑定处理。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trvList"></param>
        /// <param name="dataList"></param>
        /// <param name="colPropertys"></param>
        /// <param name="editCols"></param>
        /// <param name="treeCfgInfo"></param>
        /// <param name="allowEdit"></param>
        public virtual void CreateDataBinding(DevExpress.XtraTreeList.TreeList trvList, object dataSource,
                                     Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                     Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols,
                                     MB.WinBase.Common.TreeListViewCfgInfo treeCfgInfo, bool allowEdit) {

            if (treeCfgInfo == null)
                throw new MB.Util.APPException("绑定TreeListView 控件时,对应 TreeListViewCfgInfo 配置的配置不能为空", MB.Util.APPMessageType.SysErrInfo);
            Type entityType = typeof(T);
            if (entityType.GetProperty(treeCfgInfo.KeyFieldName) == null)
                throw new MB.Util.APPException(string.Format("绑定 TreeListView 控件时,实体定义中不包含字段{0}", treeCfgInfo.KeyFieldName), MB.Util.APPMessageType.SysErrInfo);

            trvList.KeyFieldName = treeCfgInfo.KeyFieldName;
            if (entityType.GetProperty(treeCfgInfo.ParentFieldName) == null)
                throw new MB.Util.APPException(string.Format("绑定 TreeListView 控件时,实体定义中不包含字段{0}", treeCfgInfo.ParentFieldName), MB.Util.APPMessageType.SysErrInfo);

            trvList.ParentFieldName = treeCfgInfo.ParentFieldName;
            if (entityType.GetProperty(treeCfgInfo.DisplayFieldName) == null)
                throw new MB.Util.APPException(string.Format("绑定 TreeListView 控件时,实体定义中不包含字段{0}", treeCfgInfo.DisplayFieldName), MB.Util.APPMessageType.SysErrInfo);

            trvList.PreviewFieldName  = treeCfgInfo.DisplayFieldName;
             
            CreateViewColumns(trvList, colPropertys, editCols, allowEdit);

            trvList.DataSource = dataSource;

            TreeListEx treeEx = trvList as TreeListEx;
            if (treeEx != null) {
                treeEx.ReSetContextMenu(XtraContextMenuType.SaveGridState | XtraContextMenuType.Export | XtraContextMenuType.SortChildNode);
                TreeListHelper.RestoreXtraGridState(treeEx);
            }
        }
        /// <summary>
        /// 刷新已经绑定的网格数据。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="lstDatas"></param>
        public void RefreshTreeListData(DevExpress.XtraTreeList.TreeList trvList, IList lstDatas) {
            IBindingList oldList = trvList.DataSource as IBindingList;
            oldList.Clear();
            foreach (object newItem in lstDatas)
                oldList.Add(newItem);

            trvList.RefreshDataSource(); 
        }

 
        /// <summary>
        /// 创建列
        /// </summary>
        /// <param name="trvList"></param>
        /// <param name="colPropertys"></param>
        /// <param name="editCols"></param>
        protected virtual void CreateViewColumns(DevExpress.XtraTreeList.TreeList trvList,
                                     Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                     Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols, bool allowEdit) {

            Type entityType = typeof(T);
            trvList.Columns.Clear();
            List<DevExpress.XtraTreeList.Columns.TreeListColumn> tCols = new List<DevExpress.XtraTreeList.Columns.TreeListColumn>();
            foreach (MB.WinBase.Common.ColumnPropertyInfo colInfo in colPropertys.Values) {
                if (!colInfo.Visibled)
                    continue;
                if (entityType.GetProperty(colInfo.Name) == null)
                    continue;
                DevExpress.XtraTreeList.Columns.TreeListColumn col = new DevExpress.XtraTreeList.Columns.TreeListColumn();
               
                //默认情况下，先把Byte[] 类型

                if (editCols != null && editCols.Count > 0 && editCols.ContainsKey(colInfo.Name)) {
                    MB.WinBase.Common.ColumnEditCfgInfo editIno = editCols[colInfo.Name];
                    if (editIno != null) {
                        DevExpress.XtraEditors.Repository.RepositoryItem rItem = XtraGridEditHelper.Instance.CreateEditItemByEditInfo(editIno, colInfo.DataType);
                        rItem.ReadOnly = !allowEdit;
                        col.ColumnEdit = rItem;
                        trvList.RepositoryItems.Add(rItem);
                    }
                }

                FormatColumn(col, colInfo, colInfo.Name, allowEdit);

                tCols.Add(col);
            }
            trvList.Columns.AddRange(tCols.ToArray());


        }
        /// <summary>
        /// 格式化编辑的列。
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="colPropertyInfo"></param>
        /// <param name="columnName"></param>
        protected virtual void FormatColumn(DevExpress.XtraTreeList.Columns.TreeListColumn gridColumn, 
                                                MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, string columnName, bool allowEdit) {
            gridColumn.Caption = colPropertyInfo.Description;
            if (columnName != null) {
                gridColumn.FieldName = columnName;
            }
            else {
                gridColumn.FieldName = colPropertyInfo.Name;
            }
            gridColumn.Name = "XtCol" + colPropertyInfo.Name;
            gridColumn.Width = colPropertyInfo.VisibleWidth;
            if (colPropertyInfo.Visibled && colPropertyInfo.VisibleWidth > 0) {
                gridColumn.VisibleIndex = colPropertyInfo.OrderIndex;
            }
            else {
                gridColumn.VisibleIndex = -1;
            }
            gridColumn.OptionsColumn.AllowEdit = allowEdit;
            gridColumn.OptionsColumn.ReadOnly = !allowEdit;
            gridColumn.OptionsColumn.AllowFocus = false;
        }

  
    }
    /// <summary>
    /// TreeList 控件通用操作处理相关。
    /// </summary>
    public class TreeListHelper {
        private static readonly string GRID_LAYOUT_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"UserSetting\";

        #region XtraGrid UI 操作状态保存...
        /// <summary>
        /// 保存XtraGrid 控件的UI 操作状态。
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public static void SaveXtraGridState(TreeListEx treeList) {
            if (treeList == null) return;

            string sectionName = string.Empty;

            if (treeList != null && treeList.InstanceIdentity != Guid.Empty) {
                sectionName = treeList.InstanceIdentity.ToString();
            }
            else {
                System.Windows.Forms.Form frm = MB.WinBase.ShareLib.Instance.GetControlParentForm(treeList);
                if (frm == null) {
                    return;
                }
                else {
                    sectionName = frm.GetType().FullName + " " + treeList.Name;
                    MB.WinBase.IFace.IForm busFrm = frm as MB.WinBase.IFace.IForm;
                    if (busFrm != null && busFrm.ClientRuleObject != null) {
                        sectionName = frm.GetType().FullName + " " + busFrm.ClientRuleObject.GetType().FullName + " " +
                                      busFrm.ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile + " " + treeList.Name;
                    }
                }
            }
            treeList.SaveLayoutToXml(GRID_LAYOUT_FILE_PATH + sectionName + ".xml");
        }

        /// <summary>
        /// 恢复XtraGrid 控件的UI 操作保存状态。 
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public static void RestoreXtraGridState(TreeListEx treeList) {
            if (treeList == null) return;

            string sectionName = string.Empty;

            if (treeList != null && treeList.InstanceIdentity != Guid.Empty) {
                sectionName = treeList.InstanceIdentity.ToString();
            }
            else {
                System.Windows.Forms.Form frm = MB.WinBase.ShareLib.Instance.GetControlParentForm(treeList);
                if (frm == null) {
                    return;
                }
                else {
                    sectionName = frm.GetType().FullName + " " + treeList.Name;

                    MB.WinBase.IFace.IForm busFrm = frm as MB.WinBase.IFace.IForm;
                    if (busFrm != null && busFrm.ClientRuleObject != null) {
                        sectionName = frm.GetType().FullName + " " + busFrm.ClientRuleObject.GetType().FullName + " " + busFrm.ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile + " " + treeList.Name;
                    }
                }
            }

            if (System.IO.File.Exists(GRID_LAYOUT_FILE_PATH + sectionName + ".xml")) {
                treeList.RestoreLayoutFromXml(GRID_LAYOUT_FILE_PATH + sectionName + ".xml");
            }

        }

        #endregion XtraGrid UI 操作状态保存...

        /// <summary>
        /// 根据指定的值设置树型控件中的焦点节点。
        /// </summary>
        /// <param name="treeList"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public static void SetFocusedNode(DevExpress.XtraTreeList.TreeList treeList, int nodeID) {
            var node = treeList.FindNodeByID(nodeID);
            if (node != null)
                treeList.SetFocusedNode(node);
        }
        /// <summary>
        /// 根据指定的值设置树型控件中的焦点节点。
        /// </summary>
        /// <param name="treeList"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetFocusedNode(DevExpress.XtraTreeList.TreeList treeList, string fieldName, object value) {
            var node = treeList.FindNodeByFieldValue(fieldName, value);
            if (node != null)
                treeList.SetFocusedNode(node);
        }
    }

    
}
