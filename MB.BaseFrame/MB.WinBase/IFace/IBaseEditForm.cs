using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 基于绑定窗口的控件实现。
    /// </summary>
    public interface IBaseDataBindingEdit {
        /// <summary>
        /// 当前对象编辑实体，可以为dataRow.
        /// </summary>
        object CurrentEditEntity { get; }
        /// <summary>
        /// 数据绑定的List。
        /// </summary>
        MB.WinBase.Binding.BindingSourceEx BindingSource { get; set; }
        /// <summary>
        /// 数据绑定提供的对象。
        /// </summary>
        MB.WinBase.Binding.IDataBindingProvider DataBindingProvider { get; set; }
    }
    /// <summary>
    /// 编辑窗口类必须要实现的接口。
    /// </summary>
    public interface IBaseEditForm : IForm, IBaseDataBindingEdit {
        /// <summary>
        /// 当前编辑窗口对象编辑状态。
        /// </summary>
        MB.WinBase.Common.ObjectEditType CurrentEditType { get; set; }

        /// <summary>
        /// 创建一个新的数据实体。
        /// </summary>
        /// <returns></returns>
        int AddNew();
        /// <summary>
        /// 保存当前窗口编辑的数据到缓存中。
        /// </summary>
        /// <returns></returns>
        int Save();
        /// <summary>
        /// 撤消，如果已经保存那么就不能再撤消，只能删除。
        /// </summary>
        /// <returns></returns>
        int Cancel();
        /// <summary>
        /// 删除当前选择的行。
        /// </summary>
        /// <returns></returns>
        int Delete();

        /// <summary>
        /// 提交保存的数据。
        /// </summary>
        /// <returns></returns>
        int Submit();
        /// <summary>
        /// 撤消已经提交的数据。
        /// </summary>
        /// <returns></returns>
        int CancelSubmit();
        /// <summary>
        /// 明细数据编辑临时存储的集合，
        /// 每次数据存储处理时都要从该集合中获取明细的数据进行处理，
        /// 完成后再清空该集合为下次存储处理做准备。
        /// </summary>
        MB.WinBase.UIEditEntityList BeforeSaveDetailEntityCache { get; }
    }

}
