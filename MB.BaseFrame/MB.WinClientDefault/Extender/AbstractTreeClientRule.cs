//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-12-17
// Description	:	AbstractTreeClientRule: 基于树型浏览列表显示的主浏览窗口。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MB.WinClientDefault.Extender {
    /// <summary>
    /// 基于树型浏览列表显示的主浏览窗口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TChannel"></typeparam>
    public abstract class AbstractTreeClientRule<T, TChannel> : MB.WinBase.AbstractClientRule<T, TChannel>,
        MB.WinBase.IFace.ITreeListViewHoster where TChannel : class, IDisposable {
        private MB.XWinLib.XtraTreeList.TreeListHelper<T> _TreeListDataBinding;

        private MB.XWinLib.XtraTreeList.TreeListEx _TreeListCtl;
        private MB.WinBase.Common.TreeListViewCfgInfo _TreeViewCfg = null;
        private MB.WinClientDefault.Common.EditTreeNodeCheckParam _TreeNodeEditType = null; 
      
        /// <summary>
        /// 创建基于树型列表的主业务类。
        /// </summary>
        /// <param name="mainDataTypeInDoc"></param>
        public AbstractTreeClientRule(object mainDataTypeInDoc) : base(mainDataTypeInDoc){

            _TreeListDataBinding = new MB.XWinLib.XtraTreeList.TreeListHelper<T>();

        }


        public override object CreateNewEntity(int dataInDocType) {
            object obj = base.CreateNewEntity(dataInDocType);
            if (dataInDocType == (int)this.MainDataTypeInDoc) {
                int parentID = 0;
                if (_TreeListCtl.FocusedNode != null) {
                    if (_TreeNodeEditType == null) {
                        _TreeNodeEditType = new MB.WinClientDefault.Common.EditTreeNodeCheckParam();
                    }
                    if (!_TreeNodeEditType.AllIsSame) {
                        MB.WinClientDefault.Common.frmEditTreeNodeCheck frm = new
                                                   MB.WinClientDefault.Common.frmEditTreeNodeCheck(_TreeNodeEditType);
                        frm.ShowDialog();
                    }
                    if (_TreeNodeEditType.AddAdChild)
                        parentID = (int)_TreeListCtl.FocusedNode.GetValue(_TreeViewCfg.KeyFieldName);
                    else {
                        if (_TreeListCtl.FocusedNode.ParentNode != null) {
                            parentID = (int)_TreeListCtl.FocusedNode.ParentNode.GetValue(_TreeViewCfg.KeyFieldName);
                        }
                    }
                }

                MB.Util.MyReflection.Instance.InvokePropertyForSet(obj, _TreeViewCfg.ParentFieldName, parentID);
            }
            return obj;
        }

        #region ITreeListViewHoster 成员
        /// <summary>
        /// 提供树型列表控件的绑定。
        /// </summary>
        /// <param name="treeListCtl"></param>
        /// <param name="bindingSource"></param>
        public virtual void CreateTreeListViewDataBinding(MB.XWinLib.XtraTreeList.TreeListEx treeListCtl, MB.WinBase.Binding.BindingSourceEx bindingSource) {
            _TreeListCtl = treeListCtl;
            _TreeListDataBinding.CreateDataBinding(treeListCtl, bindingSource, this.UIRuleXmlConfigInfo.GetDefaultColumns(),
                                                    this.UIRuleXmlConfigInfo.ColumnsCfgEdit, TreeViewCfg, false);
     
        }
        /// <summary>
        /// 刷新控件的数据。
        /// </summary>
        /// <param name="treeListCtl"></param>
        /// <param name="lstData"></param>
        public void RefreshTreeListData(MB.XWinLib.XtraTreeList.TreeListEx treeListCtl, IList lstData) {
           _TreeListDataBinding.RefreshTreeListData(treeListCtl, lstData);
        
        }

        //public virtual  void SetTreeNodeStateImage(DevExpress.XtraTreeList.GetStateImageEventArgs e) {
        //   // e.NodeImageIndex = e.Node.Selected ? 1 : 0;
        //}
        /// <summary>
        /// 格式化树型节点。
        /// </summary>
        /// <param name="treeListCtl"></param>
        /// <param name="treeNode"></param>
        public virtual void FormatTreeListNode(MB.XWinLib.XtraTreeList.TreeListEx treeListCtl, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs args) {
           //   
        }
        /// <summary>
        /// 树型控件的配置信息。
        /// </summary>
        public virtual MB.WinBase.Common.TreeListViewCfgInfo TreeViewCfg {
            get {
                if (_TreeViewCfg == null)
                    _TreeViewCfg = MB.WinBase.LayoutXmlConfigHelper.Instance.GetTreeListViewCfgInfo(this.ClientLayoutAttribute.UIXmlConfigFile, null);

                
                return _TreeViewCfg;
            }
        }

        #endregion

    }
}
