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
using System.Drawing.Printing;

namespace MB.WinPrintReport {
    /// <summary>
    /// 报表打印模板处理相关。
    /// </summary>
    public class ReportTemplete {
        private MB.WinPrintReport.IFace.IReportData _ReportData;
        private PrintTempleteContentInfo _CurrentTempleteContent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportData"></param>
        public ReportTemplete(MB.WinPrintReport.IFace.IReportData reportData) {
            _ReportData = reportData;
        }
        #region 报表处理基本操作方法...
        /// <summary>
        /// 打印浏览。
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="templeteID"></param>
        /// <param name="parmValues"></param>
        public void ShowPreview(string moduleID, PrintTempleteContentInfo templeteContent) {
            MB.Util.TraceEx.Write("开始执行 ShowPreview ");
            _CurrentTempleteContent = templeteContent;
            DIYReport.ReportModel.RptReport report = createRptReport(moduleID,templeteContent);
            MB.Util.TraceEx.Write("createRptReport 成功 ");
            if (report != null) {
                DIYReport.UserDIY.DesignEnviroment.CurrentReport = report;
                DIYReport.Extend.Print.XPrintingSystem printSystem = new DIYReport.Extend.Print.XPrintingSystem();
                printSystem.PrintPreview(report);
            }
            else
                throw new MB.Util.APPException(string.Format("该报表模板 {0} 还没有开始绘制,请在报表打印设计器中先设计。", templeteContent.Name), MB.Util.APPMessageType.DisplayToUser);
        }

        /// <summary>
        /// 报表直接打印。可以指定是否弹出打印对话框
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="templeteContent"></param>
        /// <param name="isDirectPrint"></param>
        /// <returns></returns>
        public int Print(string moduleID, PrintTempleteContentInfo templeteContent, bool isPopUpPrintDialog)
        {
            _CurrentTempleteContent = templeteContent;
            DIYReport.ReportModel.RptReport report = createRptReport(moduleID, templeteContent);
            if (report != null)
            {
                if (!isPopUpPrintDialog)
                {
                    PrinterSettings settings = new PrinterSettings();
                    report.PrintName = settings.PrinterName;
                }
                DIYReport.UserDIY.DesignEnviroment.CurrentReport = report;
                DIYReport.Extend.Print.XPrintingSystem printSystem = new DIYReport.Extend.Print.XPrintingSystem();
                printSystem.Print(report);
            }
            else
                throw new MB.Util.APPException(string.Format("该报表模板 {0} 还没有开始绘制,请在报表打印设计器中先设计。", templeteContent.Name), MB.Util.APPMessageType.DisplayToUser);
            return 0;
        }

        /// <summary>
        /// 报表直接打印。弹出打印对话框
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="templeteID"></param>
        /// <param name="parmValues"></param>
        /// <returns></returns>
        public int Print(string moduleID, PrintTempleteContentInfo templeteContent) {
            return Print(moduleID, templeteContent, true);
        }
        /// <summary>
        /// 显示打印模板设计。
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="templeteID"></param>
        public void ShowDesign(string moduleID, PrintTempleteContentInfo templeteContent) {
            _CurrentTempleteContent = templeteContent;
            if (string.IsNullOrEmpty(templeteContent.TempleteXmlContent)) {
               var newReport = DIYReport.ReportModel.RptReport.NewReport(templeteContent.Name, templeteContent.GID);
               templeteContent.TempleteXmlContent = DIYReport.ReportWriter.Instance().BuildXMLString(newReport);
            }

            DIYReport.ReportModel.RptReport reportContent = createRptReport(moduleID,templeteContent);
           
            DIYReport.Extend.EndUserDesigner.MainDesignForm frmDesign = new DIYReport.Extend.EndUserDesigner.MainDesignForm(reportContent);
            frmDesign.BeginXReportIOProcess += new DIYReport.ReportModel.XReportIOEventHandler(frmDesign_BeginXReportIOProcess);
            frmDesign.ShowDialog(); 
        }

        #endregion 报表处理基本操作方法...

        #region 对象事件...
        private void frmDesign_BeginXReportIOProcess(object sender, DIYReport.ReportModel.XReportIOEventArgs e) {
            //下面的例子中演示的是从本地获取和存储
            if (e.CommandID.Equals(DIYReport.UserDIY.UICommands.NewReport)) {
                e.HasProcessed = true;
            }
            else if (e.CommandID.Equals(DIYReport.UserDIY.UICommands.OpenFile)) {
                //这里主要处理显示设计的子报表。
                e.HasProcessed = true;
            }
            else if (e.CommandID.Equals(DIYReport.UserDIY.UICommands.SaveFile)) {

                string templeteContent = DIYReport.ReportWriter.Instance().BuildXMLString(e.DataReport);
                _CurrentTempleteContent.TempleteXmlContent = templeteContent;
               // _ReportData.SavePrintTemplete(_CurrentTempleteContent);
                _ReportData.PrintTempleteChanged = true; 

                e.HasProcessed = true;

            }
            else if (e.CommandID.Equals(DIYReport.UserDIY.UICommands.SaveFileAs)) {

                e.HasProcessed = true;
            }
            else if (e.CommandID.Equals(DIYReport.UserDIY.UICommands.Preview)) {
             
                    ////在预览后保持原先的报表设计字段
                    //IList iFields = e.DataReport.DesignField;
                    //e.DataReport.DataSource = dsData;
                    //e.DataReport.DesignField = iFields;
                
            }
            else {

            }
        }
        #endregion 对象事件...

        #region 内部函数相关处理...
        //创建打印预览和直接打印的报表对象。
        private DIYReport.ReportModel.RptReport createRptReport(string moduleID,PrintTempleteContentInfo templeteContent) {
            try {
                //PrintTempleteContentInfo templeteContent = _ReportData.GetPrintTempleteContent(templeteID);
                DIYReport.ReportModel.RptReport reportContent = DIYReport.ReportReader.Instance().ReadFromXmlString(templeteContent.TempleteXmlContent);
                reportContent.UserParamList = convertToRptParam(_ReportData.ReportParamList);

                //以后再追加报表子表的处理问题
                DIYReport.Interface.ISubReportCommand subCommand = new SubReportCommand(templeteContent, _ReportData.DataSource); 
                //subCommand.SetDataSourceParams = pars;
                reportContent.SubReportCommand = subCommand;

              //  if (_ReportData.DataSource is DataSet) {
                    DataSet dsData = _ReportData.DataSource;
                    reportContent.DataSource = dsData.Tables[templeteContent.DataSource];
                //}
                //else {
                //    throw new MB.Util.APPException("目前报表的数据类型只支持 DataSet 请先转换", MB.Util.APPMessageType.SysErrInfo); 
                //}

                return reportContent;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Format("根据功能模块ID:{0},打印模板ID: {1} 创建报表对象出错。", moduleID, templeteContent.GID));
            }
        }
        //转换为报表需要的参数格式
        private DIYReport.ReportModel.RptParamList convertToRptParam(Dictionary<string, object> pars) {
            DIYReport.ReportModel.RptParamList lstParams = new DIYReport.ReportModel.RptParamList();
            foreach (string key in pars.Keys) {
                lstParams.Add(new DIYReport.ReportModel.RptParam(key, pars[key]));
            }
            return lstParams;
        }
        #endregion 内部函数相关处理...
    }
}
