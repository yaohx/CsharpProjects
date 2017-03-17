using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinClientDefault.DataImport
{
    /// <summary>
    /// 模板倒出需要实现的接口
    /// </summary>
    public interface IImportTempletExportProvider
    {
        /// <summary>
        /// 模板导出。
        /// </summary>
        /// <param name="viewForm"></param>
        /// <returns></returns>
        int TempletExport(MB.WinBase.IFace.IViewGridForm viewForm);
    }
}
