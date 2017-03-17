//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-0924
// Description	:	默认配置处理的报表数据处理类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.WinPrintReport {
    /// <summary>
    /// 默认配置处理的报表数据处理类。
    /// </summary>
    public class DefaultReportData : MB.WinPrintReport.IFace.IReportData {
        #region 变量定义...
        private static readonly string PRINT_TEMPLETE_PATH = MB.Util.General.GeApplicationDirectory() + @"PrintTemplete\";
        private static readonly string PRINT_TEMPLETE_EX_NAME = ".rpt";
        private static readonly string PRINT_TEMPLETE_NAME = "PrintTemplete_{0}_{1}" + PRINT_TEMPLETE_EX_NAME;
        private static readonly string SAVE_ROOT_PATH = "PrintTempleteRoot";
        private static readonly string SIMPLE_TEMPLETE = "_{0}_";

        private DataSet _DataSource;
        private Dictionary<string, object> _ReportParamList;
        private string _ModuleID;
        private bool _PrintTempleteChanged;
        private System.Guid _TempleteGuid;
        #endregion 变量定义...

        /// <summary>
        /// 默认配置处理的报表数据处理类。 
       /// </summary>
       /// <param name="moduleID">报表对象对应的模块ID</param>
       /// <param name="dataSource">报表对象需要的数据源</param>
       /// <param name="reportParamList">报表对象的参数</param>
        public DefaultReportData(string moduleID,DataSet  dataSource,Dictionary<string,object> reportParamList) : this(moduleID,Guid.Empty,dataSource,reportParamList) {
           
        }
        /// <summary>
        /// 默认配置处理的报表数据处理类
        /// </summary>
        /// <param name="moduleID">报表对象对应的模块ID<</param>
        /// <param name="templeteID">指定的报表GUID</param>
        /// <param name="dataSource">报表对象需要的数据源</param>
        /// <param name="reportParamList">报表对象的参数</param>
        public DefaultReportData(string moduleID, System.Guid templeteID,DataSet dataSource, Dictionary<string, object> reportParamList) {
            _ModuleID = moduleID;
            _DataSource = dataSource;
            _ReportParamList = reportParamList;
            _TempleteGuid = templeteID;
            if (!System.IO.Directory.Exists(PRINT_TEMPLETE_PATH))
                System.IO.Directory.CreateDirectory(PRINT_TEMPLETE_PATH);
        }
        #region IReportData 成员...

        #region public 属性...
        /// <summary>
        /// 报表对象当前对应的模块ID。
        /// </summary>
        public string ModuleID {
            get {
                return _ModuleID;
            }
            set {
                _ModuleID = value;
            }
        }
        /// <summary>
        /// 判断打印模板是否已经发生改变。
        /// </summary>
        public bool PrintTempleteChanged {
            get {
                return _PrintTempleteChanged;
            }
            set {
                _PrintTempleteChanged = value;
            }
        }
        /// <summary>
        /// 报表设计和浏览需要的数据源。
        /// 注意： 目前只支持DataSet 格式，如果需要传入其它数据类型,请先转换。
        /// </summary>
        public DataSet DataSource {
            get {
                return _DataSource;
            }
            set {
                _DataSource = value;
            }
        }
        /// <summary>
        /// 报表需要的用户参数列表。
        /// </summary>
        public Dictionary<string, object> ReportParamList {
            get {
                return _ReportParamList;
            }
            set {
                _ReportParamList = value;
            }
        }
        #endregion public 属性...

        #region public 方法...
        /// <summary>
        /// 打印模板的设计结构。
        /// </summary>
        /// <param name="templeteID"></param>
        /// <returns></returns>
        public MB.WinPrintReport.Model.PrintTempleteContentInfo GetPrintTempleteContent(System.Guid templeteID) {
            throw new MB.Util.APPException("该方法的使用适合报表模板存储在库中的情况,目前还没有实现");
        }

        /// <summary>
        /// 保存打印模板的设计内容。
        /// </summary>
        /// <param name="templeteContent"></param>
        /// <returns></returns>
        public int SavePrintTemplete(MB.WinPrintReport.Model.PrintTempleteContentInfo printTemplete) {
            string fileName = string.Format(PRINT_TEMPLETE_PATH + PRINT_TEMPLETE_NAME, _ModuleID, printTemplete.GID.ToString() );
            var xmlSerializer = new MB.Util.Serializer.EntityXmlSerializer<MB.WinPrintReport.Model.PrintTempleteContentInfo>();
            string xmlString = xmlSerializer.SingleSerializer(printTemplete, SAVE_ROOT_PATH);
            MB.Util.MyFileStream.Instance.StreamWriter(fileName, xmlString);  
            return 0;
        }

        /// <summary>
        /// 根据模块ID 获取该模块下的所有打印报表。
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public MB.WinPrintReport.Model.PrintTempleteContentInfo GetPrintTemplete(System.Guid templeteID) {
            string fileName = string.Format(PRINT_TEMPLETE_PATH + PRINT_TEMPLETE_NAME, _ModuleID, templeteID.ToString());
            string xmlString = MB.Util.MyFileStream.Instance.StreamReader(fileName);
            if (string.IsNullOrEmpty(xmlString)) {
                return null;
            }

            var xmlSerializer = new MB.Util.Serializer.EntityXmlSerializer<MB.WinPrintReport.Model.PrintTempleteContentInfo>();
            return xmlSerializer.SingleDeSerializer(xmlString, SAVE_ROOT_PATH);  
        }
        /// <summary>
        /// 获取指定模块下的所有打印报表模板。
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public List<MB.WinPrintReport.Model.PrintTempleteContentInfo> GetModulePrintTempletes(string moduleID) {
            List<MB.WinPrintReport.Model.PrintTempleteContentInfo> rpts = new List<MB.WinPrintReport.Model.PrintTempleteContentInfo>();
            List<System.Guid> gids = getAllPrintTempleteIDS(moduleID);
            if (gids == null) return null;
            foreach (System.Guid id in gids) {
                var rpt = GetPrintTemplete(id);
                if (rpt.GID != id)
                    throw new MB.Util.APPException(string.Format("打印报表GUID {0} 和报表模板内容中的GUID {1} 不一致,请检查.", id,rpt.GID), Util.APPMessageType.SysFileInfo);
                if (rpt != null)
                    rpts.Add(rpt);
            }

            return rpts;
        }
        #endregion public 方法...

        #endregion IReportData 成员...

        #region 内部函数处理...
        //获取指定模块下的所有打印模板文件名称。
        private List<System.Guid> getAllPrintTempleteIDS(string moduleID) {
            if (!System.IO.Directory.Exists(PRINT_TEMPLETE_PATH)) return null;

            List<System.Guid> templeteNames = new List<System.Guid>();
            string[] files = System.IO.Directory.GetFiles(PRINT_TEMPLETE_PATH, "*" + PRINT_TEMPLETE_EX_NAME);
            foreach (string file in files) {
                //string exName = System.IO.Path.GetExtension(file);
                //if (string.Compare(exName, PRINT_TEMPLETE_EX_NAME, true) != 0)
                //    continue;
                if (file.IndexOf(string.Format(SIMPLE_TEMPLETE, moduleID)) < 0) continue;

                string name = System.IO.Path.GetFileNameWithoutExtension(file);
                try {
                    string strId = getGidFromFileName(name);
                    System.Guid gid = new Guid(strId);
                    if (_TempleteGuid != Guid.Empty && _TempleteGuid != gid)
                        continue;

                    templeteNames.Add(gid);
                }
                catch {
                    continue;
                }
            }
            return templeteNames;
        }
        //从报表名称获取 报表的模板ID
        private string getGidFromFileName(string fileName) {
            int s = fileName.LastIndexOf('_');
            if (s < 0) return string.Empty;
            return fileName.Substring(s + 1, fileName.Length - s - 1);
        }
        #endregion 内部函数处理...
    }
}
