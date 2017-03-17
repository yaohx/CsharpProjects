//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-09-18
// Description	:	报表打印模板处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MB.WinPrintReport.Model;
namespace MB.WinPrintReport {
    /// <summary>
    /// 子报表调用 报表 Command 处理相关
    /// </summary>
    public class SubReportCommand : DIYReport.Interface.ISubReportCommand {
        private PrintTempleteContentInfo _PrintTemplete;
        private DataSet _DataSource;
        private List<string> _SubReportNames;
        /// <summary>
        /// 子报表相关处理。
        /// </summary>
        /// <param name="printTemplete"></param>
        /// <param name="dsData"></param>
        public SubReportCommand(MB.WinPrintReport.Model.PrintTempleteContentInfo printTemplete,DataSet dsData) {
            _PrintTemplete = printTemplete;
            _DataSource = dsData;
            _SubReportNames = new List<string>();

            if (_PrintTemplete.Childs != null) {
                foreach (MB.WinPrintReport.Model.PrintTempleteContentInfo child in _PrintTemplete.Childs) {
                    _SubReportNames.Add(child.Name);
                }
            }
        }
        #region ISubReportCommand 成员
        /// <summary>
        /// 通过子报表的名称获取子报表的设计内容。
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public DIYReport.ReportModel.RptReport GetReportContent(string reportName) {
            var childInfo = _PrintTemplete.Childs.FirstOrDefault(o => string.Compare(o.Name, reportName, true) == 0);
            if (childInfo == null) return null;
            DIYReport.ReportModel.RptReport reportContent = DIYReport.ReportReader.Instance().ReadFromXmlString(childInfo.TempleteXmlContent);

            return reportContent;
        }
        /// <summary>
        /// 得到子表的数据源
        /// </summary>
        /// <param name="parentRow"></param>
        /// <param name="relationMember"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public object GetReportDataSource(DataRow parentRow, string relationMember, string reportName)
        {
            PrintTempleteContentInfo childInfo = _PrintTemplete.Childs.FirstOrDefault(o => string.Compare(o.Name, reportName, true) == 0);
            if (childInfo == null) return null;
            if (_DataSource.Tables.Contains(childInfo.DataSource))
            {
                if (string.IsNullOrEmpty(relationMember))
                {
                    return _DataSource.Tables[childInfo.DataSource];
                }
                else
                {
                    DataRow[] drs = parentRow.GetChildRows(relationMember, System.Data.DataRowVersion.Default);
                    DataTable newTable = null;
                    if (drs.Length > 0)
                    {
                        newTable = drs[0].Table.Clone();
                        foreach (DataRow dr in drs)
                        {
                            newTable.Rows.Add(dr.ItemArray);
                        }
                    }
                    return newTable;
                }
            }
            else
                return null;
        }

        /// <summary>
        /// 子报表名称。
        /// </summary>
        public System.Collections.IList SubReportName {
            get {
                return _SubReportNames; 
            }
        }

        #endregion
    }
}
