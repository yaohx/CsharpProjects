using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 网纲浏览窗口必须要实现的接口。
    /// </summary>
    public interface IViewGridForm : IForm {

        /// <summary>
        /// 是否显示所有页数
        /// </summary>
        bool IsTotalPageDisplayed { get; set; }
        /// <summary>
        /// 数据保存。
        /// </summary>
        /// <returns></returns>
        int Save();
        /// <summary>
        /// 创建新对象。
        /// </summary>
        /// <returns></returns>
        int AddNew();
        /// <summary>
        /// 复制一个新行。
        /// </summary>
        /// <returns></returns>
        int CopyAsNew();
        /// <summary>
        /// 打开一个数据实体。
        /// </summary>
        /// <returns></returns>
        int Open();
        /// <summary>
        /// 删除当前选择的行。
        /// </summary>
        /// <returns></returns>
        int Delete();
        /// <summary>
        /// 查询处理。
        /// </summary>
        int Query();
        /// <summary>
        /// 刷新。
        /// </summary>
        /// <returns></returns>
        int Refresh();
        /// <summary>
        /// 数据导入处理。
        /// </summary>
        /// <returns></returns>
        int DataImport();
        /// <summary>
        /// 当前浏览数据的网格窗口。
        /// </summary>
        object GetCurrentMainGridView(bool mustEditGrid);

        /// <summary>
        /// 判断是否存在未保存的数据。
        /// </summary>
        bool ExistsUnSaveData();

        /// <summary>
        /// 数据导出。
        /// </summary>
        int DataExport();

        /// <summary>
        /// 刷新浏览界面，重新加载列及数据
        /// </summary>
        /// <returns></returns>
        int ReloadData();
    }
}
