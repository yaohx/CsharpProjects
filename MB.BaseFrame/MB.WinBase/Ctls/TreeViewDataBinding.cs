//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-10
// Description	:	提供树型控件需要的公共处理方法。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
using System.Windows.Forms;

namespace MB.WinBase.Ctls {
    /// <summary>
    /// 提供树型控件需要的公共处理方法。
    /// </summary>
    public class TreeViewDataBinding<T> where T : class {
        #region 变量定义...
        private TreeView _TreeViewCtl;
        private string _KeyName;
        private string _PrevKeyName;
        private string _TextFieldName;
        private List<T> _CurrentDataEntitys;
        private ITreeViewDataBindingFormate<T> _CurrentFormate;
        private object _RootNodePrevKeyValue;

        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 树型控件数据绑定处理相关。
        /// </summary>
        /// <param name="treeViewCtl"></param>
        /// <param name="textFieldName"></param>
        /// <param name="keyName"></param>
        /// <param name="prevKeyName"></param>
        public TreeViewDataBinding(TreeView treeViewCtl,string textFieldName,string keyName, string prevKeyName) {
            _TreeViewCtl = treeViewCtl;
            _KeyName = keyName;
            _PrevKeyName = prevKeyName;
            _TextFieldName = textFieldName;
        }
        #endregion 构造函数...

        #region public 成员...
        /// <summary>
        /// 刷新数据源
        /// </summary>
        /// <param name="entitys"></param>
        public void RefreshDataSource(List<T> entitys) {
            if (entitys == null || entitys.Count == 0) {
                _TreeViewCtl.Nodes.Clear();
                return;
            }
            _CurrentDataEntitys = entitys;

            dataValidated();

            bindingDataSouce();
        }
        /// <summary>
        /// 当前自定义树型节点格式化器。
        /// </summary>
        public ITreeViewDataBindingFormate<T> CurrentFormate {
            get {
                return _CurrentFormate;
            }
            set {
                _CurrentFormate = value;
            }
        }
        /// <summary>
        /// 最顶层节点上级节点的键值
        /// </summary>
        public object RootNodePrevKeyValue {
            get {
                return _RootNodePrevKeyValue;
            }
            set {
                _RootNodePrevKeyValue = value;
            }
        }
        #endregion public 成员...

        //检查数据是否合法
        private bool dataValidated() {
            if (_CurrentDataEntitys.Count == 0) return true;
            T entity = _CurrentDataEntitys[0];
            if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, _KeyName))
                throw new MB.Util.APPException(string.Format("绑定TreeView 控件时,节点描述字段名称 {0} 配置有误", _KeyName));

            //检查是否包含键值属性

            if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, _TextFieldName))
                    throw new MB.Util.APPException(string.Format("绑定TreeView 控件时,键值字段名称 {0} 配置有误", _TextFieldName));
          
            //检查是否包含上级字段名称
            if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, _PrevKeyName))
                throw new MB.Util.APPException(string.Format("绑定TreeView 控件时,上级键值字段名称 {0} 配置有误", _PrevKeyName));
            return true;
        }
        //
        private void bindingDataSouce() {
            if (_CurrentDataEntitys == null || _CurrentDataEntitys.Count == 0) {
                _TreeViewCtl.Nodes.Clear();
                return;
            }
            try {
                createNode(_TreeViewCtl.Nodes, default(T));

                if (_TreeViewCtl.Nodes.Count > 0)
                    _TreeViewCtl.Nodes[0].Expand(); 
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("自定义集合类绑定树型控件有误:" + ex.Message);
            }
            
        }
        //创建节点。
        private void createNode(TreeNodeCollection treeNodes, T entity) {
            T[] childEntitys = findChildeEntity(entity);
            foreach (T childEntity in childEntitys) {
                //增加一个新的节点
                TreeNode childNode = new TreeNode();
 
                object text = MB.Util.MyReflection.Instance.InvokePropertyForGet(childEntity, _TextFieldName);

                childNode.Text = text.ToString().Trim();
                childNode.Tag = childEntity;
                treeNodes.Add(childNode);
                if (_CurrentFormate != null) {
                    _CurrentFormate.Format(childNode, childEntity);
                }
                //递归调用生成树型节点
                createNode(childNode.Nodes, childEntity);
            }  
         
        }
        //找出当前节点的所有下级节点。
        private T[] findChildeEntity(T entity) {
            object keyValue = null;
            if (entity == null)
                keyValue = _RootNodePrevKeyValue;
            else
               keyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, _KeyName);

            List<T> childs = new List<T>(); 
            foreach (T child in _CurrentDataEntitys) {
                object prevKeyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(child, _PrevKeyName);
                if (keyValue == null) {
                    if (prevKeyValue == null) {
                        childs.Add(child);
                        continue;
                    }
                }
                else {
                    if (prevKeyValue == null) continue;

                    if (string.Compare(keyValue.ToString(), prevKeyValue.ToString(), true) == 0) {
                        childs.Add(child);
                    }
                }
            }
            return childs.ToArray();
        }
    }

    /// <summary>
    /// 数据绑定时 格式化树型节点需要实现的接口。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITreeViewDataBindingFormate<T> {
        /// <summary>
        ///  格式化当前创建的树型节点。
        /// </summary>
        /// <param name="tNode"></param>
        /// <param name="createNodeEntity"></param>
        void Format(TreeNode tNode, T createNodeEntity);
    }
}
