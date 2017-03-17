using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace MB.WinClientDefault.OfficeAutomation {
    /// <summary>
    /// Excel编辑器需要实现的接口
    /// </summary>
    public interface IExcelEditor : IDisposable {
        /// <summary>
        /// 打开excel,并且返回excelFile的路径
        /// </summary>
        /// <returns></returns>
        string OpenExcel();

        /// <summary>
        /// 提交excel中编辑的数据
        /// </summary>
        IList Submit();
    }
}
