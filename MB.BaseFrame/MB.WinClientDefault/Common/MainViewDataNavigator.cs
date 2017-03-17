//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-12-05
// Description	:	基于 IViewGridForm 主体列表编辑数据操作行为。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList.Nodes;
using MB.WinBase.Binding;

namespace MB.WinClientDefault.Common {
    /// <summary>
    /// 基于 IViewGridForm 主体列表编辑数据操作行为。
    /// </summary>
    public class MainViewDataNavigator {
        /// <summary>
        /// 判断是否为叶子行。
        /// </summary>
        /// <returns></returns>
        public bool CheckIsLeafDataRow() {
            if (_DataViewType == DataViewType.GridView)
                return true;
            else {
                return _TreeList.FocusedNode.Nodes.Count == 0; 
            }
        }
        private DevExpress.XtraGrid.Views.Grid.GridView _GridView;
     /// <summary>
     /// GridView浏览控件
     /// </summary>
        public DevExpress.XtraGrid.Views.Grid.GridView GridView {
            get { return _GridView; }
            set { _GridView = value; }
        }
        private DevExpress.XtraTreeList.TreeList _TreeList;
        /// <summary>
        /// TreeListView 浏览控件
        /// </summary>
        public DevExpress.XtraTreeList.TreeList TreeList {
            get { return _TreeList; }
            set { _TreeList = value; }
        }

        private DataViewType _DataViewType;
        /// <summary>
        /// 基于GridView的主数据浏览
        /// </summary>
        /// <param name="gridView"></param>
        public MainViewDataNavigator(DevExpress.XtraGrid.Views.Grid.GridView gridView) {
            _GridView = gridView;
            _DataViewType = DataViewType.GridView; 
        }
        /// <summary>
        /// 基于树型浏览样式的主窗口。
        /// </summary>
        /// <param name="treeList"></param>
        public MainViewDataNavigator(DevExpress.XtraTreeList.TreeList treeList) {
            _TreeList = treeList;
            _DataViewType = DataViewType.TreeListView; 
        }

      

        /// <summary>
        /// 通过Index 移动行。
        /// TreeListView 不支持
        /// </summary>
        /// <param name="index"></param>
        public void MoveBy(int index) {
            if (_DataViewType == DataViewType.GridView)
                _GridView.MoveBy(index);
            
        }
        /// <summary>
        /// MoveFirst
        /// </summary>
        public void MoveFirst() {
            if (_DataViewType == DataViewType.GridView)
                _GridView.MoveFirst();
            else
                _TreeList.MoveFirst();
        }
        /// <summary>
        /// MoveNext
        /// </summary>
        public void MoveNext() {
            if (_DataViewType == DataViewType.GridView)
                _GridView.MoveNext(); 
            else
                _TreeList.MoveNextVisible();  
        }
        /// <summary>
        /// MovePrev
        /// </summary>
        public void MovePrev() {
            if (_DataViewType == DataViewType.GridView)
                _GridView.MovePrev();
            else
                _TreeList.MovePrevVisible();  
        }
        /// <summary>
        /// MoveLast
        /// </summary>
        public void MoveLast() {
            if (_DataViewType == DataViewType.GridView)
                _GridView.MoveLast();
            else
                _TreeList.MoveLastVisible();  
        }
        /// <summary>
        /// ActiveFilterEnabled
        /// </summary>
        public bool ActiveFilterEnabled {
            get {
                if (_DataViewType == DataViewType.GridView)
                    return _GridView.ActiveFilterEnabled;
                else
                    return false;
            }
            set {
                if (_DataViewType == DataViewType.GridView)
                    _GridView.ActiveFilterEnabled = value;
            }
        }
        /// <summary>
        /// IsFirstRow
        /// </summary>
        public bool IsFirstRow {
            get {
                if (_DataViewType == DataViewType.GridView)
                    return _GridView.IsFirstRow;
                else
                    return _TreeList.FocusedNode.PrevVisibleNode == null; 
            }
        }
        /// <summary>
        /// IsLastRow
        /// </summary>
        public bool IsLastRow {
            get {
                if (_DataViewType == DataViewType.GridView)
                    return _GridView.IsLastRow;
                else
                    return _TreeList.FocusedNode.NextVisibleNode == null;
            }
        }
        /// <summary>
        /// FocusedRowHandle
        /// 不支持TreeListView
        /// </summary>
        public int FocusedRowHandle {
            get {
                if (_DataViewType == DataViewType.GridView)
                    return _GridView.FocusedRowHandle;
                else
                    return _TreeList.VisibleNodesCount;
            }
        }

        /// <summary>
        /// 获取选中行对应的实体类集合(允许多选),如果没有选中行则返回空
        /// </summary>
        /// <param name="bindingSource">绑定数据源</param>
        /// <returns>选中行对应的实体类集合</returns>
        public object[] GetSelectedObjects(BindingSourceEx bindingSource)
        {
            if(_DataViewType == DataViewType.GridView)
            {
                var rowHandles= _GridView.GetSelectedRows();

                List<object> objects=new List<object>();

                foreach (var rowHandle in rowHandles)
	            {
		            objects.Add(bindingSource[this._GridView.GetDataSourceRowIndex(rowHandle)]);
	            }

                return objects.ToArray();
            }
            else if (_DataViewType == DataViewType.TreeListView)
            {
                List<object> objects = new List<object>();

                foreach (TreeListNode node in _TreeList.Selection)
                {
                    var data = _TreeList.GetDataRecordByNode(node);
                    if (!objects.Contains(data))
                    {
                        objects.Add(data);
                    }

                    var descendantNodes = GetDescendantNodes(node);
                    //有子节点
                    if (descendantNodes != null && descendantNodes.Count() > 0)
                    {
                        foreach (var descendantNode in descendantNodes)
                        {
                            var data2 = _TreeList.GetDataRecordByNode(descendantNode);
                            if (!objects.Contains(data2))
                            {
                                objects.Add(data2);
                            }
                        }
                    }
                }

                return objects.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取一个TreeListNode所有的子孙节点,如果不存在则返回空
        /// </summary>
        /// <param name="node">父节点</param>
        /// <returns>所有的子孙节点</returns>
        TreeListNode[] GetDescendantNodes(TreeListNode node)
        {
            List<TreeListNode> nodes = new List<TreeListNode>();

            if (node.HasChildren)
            {
                foreach (TreeListNode childNode in node.Nodes)
                {
                    nodes.Add(childNode);
                    var descendantNodes = GetDescendantNodes(childNode);

                    if(descendantNodes!=null)
                        nodes.AddRange(descendantNodes);
                }

                return nodes.ToArray();
            }
            else
                return null;
        }

        /// <summary>
        /// RowCount
        /// 不支持TreeListView
        /// </summary>
        public int RowCount {
            get {
                if (_DataViewType == DataViewType.GridView)
                    return _GridView.RowCount;
                else
                    return _TreeList.VisibleNodesCount;
            }
        }
        /// <summary>
        /// ActiveFilterString
        /// 不支持TreeListView
        /// </summary>
        public string ActiveFilterString {
            get{
                if (_DataViewType == DataViewType.GridView)
                    return _GridView.ActiveFilterString;
                else
                    return string.Empty;
               }
            
        }
       /// <summary>
       /// 删除焦点行。
       /// </summary>
       /// <param name="bindingSource"></param>
        public void RemoveFocusedRow(MB.WinBase.Binding.BindingSourceEx bindingSource) {
            if (_DataViewType == DataViewType.GridView)
                bindingSource.RemoveCurrent();
            else {
                _TreeList.DeleteNode(_TreeList.FocusedNode); 
            }
        }

        public void RemoveSelectedRow(MB.WinBase.Binding.BindingSourceEx bindingSource)
        {
            if (_DataViewType == DataViewType.GridView)
            {
                var rowIndexs=_GridView.GetSelectedRows();

                if (rowIndexs != null && rowIndexs.Count() > 0)
                {
                    List<object> toBeDeletedObjects = new List<object>();

                    foreach (var rowIndex in rowIndexs)
                    {
                        toBeDeletedObjects.Add(bindingSource[rowIndex]);
                    }

                    foreach (var item in toBeDeletedObjects)
                    {
                        bindingSource.Remove(item);
                    }
                }     
            }
            else if (_DataViewType == DataViewType.TreeListView)
            {
                foreach (var node in _TreeList.Selection)
	            {
                    _TreeList.DeleteNode(node as TreeListNode);
	            }
            }
        }
    }
    /// <summary>
    /// 数据浏览样式
    /// </summary>
    public enum DataViewType {
        /// <summary>
        /// GridView 控件。
        /// </summary>
        GridView,
        /// <summary>
        /// TreeListView 控件。
        /// </summary>
        TreeListView,
    }
}
