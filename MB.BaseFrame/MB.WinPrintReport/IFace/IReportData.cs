using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MB.WinPrintReport.Model;
namespace MB.WinPrintReport.IFace {
    /// <summary>
    /// 打印报表需要实现的接口。
    /// </summary>
    public interface IReportData {
        /// <summary>
        /// 报表对象当前对应的模块ID;
        /// </summary>
        string ModuleID { get; set; }
        /// <summary>
        ///  调用报表打印需要的数据源。
        /// </summary>
        /// <param name="dataSourceCfgInfo"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        DataSet DataSource{get;set;}
        /// <summary>
        /// 判断打印模板是否已经发生改变。
        /// </summary>
        bool PrintTempleteChanged { get; set; }
        /// <summary>
        /// 报表 打印/浏览 参数集合。
        /// </summary>
        Dictionary<string,object> ReportParamList { get; set; }
        /// <summary>
        /// 初始化打印需要的打印模板内容。
        /// </summary>
        /// <param name="templeteID"></param>
        MB.WinPrintReport.Model.PrintTempleteContentInfo GetPrintTempleteContent(System.Guid templeteID);
        /// <summary>
        /// 模板内容存储。
        /// </summary>
        /// <param name="templeteContent"></param>
        /// <returns></returns>
        int SavePrintTemplete(MB.WinPrintReport.Model.PrintTempleteContentInfo printTemplete);

        /// <summary>
        /// 获取当前模块的所有打印模板。
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="templeteID"></param>
        /// <returns></returns>
        MB.WinPrintReport.Model.PrintTempleteContentInfo GetPrintTemplete(System.Guid templeteID);

        /// <summary>
        /// 获取模块下的所有打印模板。
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        List<MB.WinPrintReport.Model.PrintTempleteContentInfo> GetModulePrintTempletes(string moduleID);
    }
}
