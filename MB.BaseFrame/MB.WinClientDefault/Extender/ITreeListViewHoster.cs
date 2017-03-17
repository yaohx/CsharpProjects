using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 实现树型列表浏览窗口必须实现的接口。
    /// 特别说明： 这里为了让所有业务类的数据处理接口都来自  MB.WinBase.IFace
    /// 这里特别把它的命名空间修改为这里。
    /// </summary>
    public interface ITreeListViewHoster {
        /// <summary>
        /// 创建树型控件数据绑定。
        /// </summary>
        /// <param name="treeListCtl"></param>
        /// <param name="bindingSource"></param>
        void CreateTreeListViewDataBinding(MB.XWinLib.XtraTreeList.TreeListEx treeListCtl, MB.WinBase.Binding.BindingSourceEx bindingSource);
        /// <summary>
        /// 刷新树型控件的数据。
        /// </summary>
        /// <param name="treeListCtl"></param>
        /// <param name="lstData"></param>
        void RefreshTreeListData(MB.XWinLib.XtraTreeList.TreeListEx treeListCtl, IList lstData);
        ///// <summary>
        ///// 设置节点的ImageIndex.
        ///// </summary>
        ///// <param name="e"></param>
        //void SetTreeNodeStateImage(DevExpress.XtraTreeList.GetStateImageEventArgs e);
        /// <summary>
        /// 格式化树型控件节点。
        /// </summary>
        /// <param name="treeListCtl"></param>
        /// <param name="treeNode"></param>
        void FormatTreeListNode(MB.XWinLib.XtraTreeList.TreeListEx treeListCtl, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs args);
        /// <summary>
        /// 获取树型配置的信息。
        /// </summary>
        MB.WinBase.Common.TreeListViewCfgInfo TreeViewCfg { get; }
    }
}
