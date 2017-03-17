using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.Tools.LogView {
    class LogFileHelper {
        private static readonly string COST_TEXT = "总共花费(毫秒)";
        private static string _LasLoadLogData;
        private LogFileHelper() {
        }

        /// <summary>
        /// 加载日志文件内容到DataGrid 网格控件中。
        /// </summary>
        /// <param name="dGrid"></param>
        /// <param name="fullFileName"></param>
        public static void LoadDataToCtl(DevExpress.XtraGrid.GridControl dGrid, string[] fullFileName,
                        LogDataLoadInfo loadInfo) {

            var gridView = dGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;  
            DataTable dtData = createFormLogFile(fullFileName, loadInfo);
            if (dtData == null) return;
            dGrid.DataSource = dtData.DefaultView;
            if (dtData == null) return;

            var col = gridView.Columns["CreateDate"];
            col.Caption = "日期";
            col.Width = 120;
            formatColumn(col);

            col = gridView.Columns["CreateTime"];
            col.Caption = "时间";
            col.Width = 60;
            formatColumn(col);

            col = gridView.Columns["RowIndex"];
            col.Caption = "索引";
            col.Width = 60;
            formatColumn(col);

            col = gridView.Columns["ClientIP"];
            col.Caption = "用户机器IP";
            col.Width = 100;
            formatColumn(col);

            col = gridView.Columns["MessageType"];
            col.Caption = "消息类型";
            col.Width = 100;
            formatColumn(col);

            col = gridView.Columns["Detail"];
            col.Caption = "明细";
            col.Width = 600;
            formatColumn(col);

            col = gridView.Columns["ExecuteTime"];
            col.Caption = "花费(毫秒)";
            col.Width = 100;
            formatColumn(col);
        }
        private static void formatColumn(DevExpress.XtraGrid.Columns.GridColumn gridColumn) {
            gridColumn.OptionsColumn.ReadOnly = true;
        }
        /// <summary>
        /// 那未增加的数据增加到网格中。
        /// </summary>
        /// <param name="dGrid"></param>
        /// <param name="fullFileName"></param>
        public static void AddLeaveLogData(DevExpress.XtraGrid.GridControl dGrid, string[] fullFileName, LogDataLoadInfo loadInfo) {
            if (dGrid.DataSource == null) return;

            DataView dv = dGrid.DataSource as DataView;
            foreach(string fName in fullFileName)
                addLeaveLogData(dv.Table, fName, loadInfo);

            //dGrid.Refresh();
        }
        // 从日志文件中创建对应的数据表。
        private static System.Data.DataTable createFormLogFile(string[] fullFileName, LogDataLoadInfo loadInfo) {
         
            DataTable dtLog = createDataTable();
            SortedList<string, string> files = new SortedList<string, string>();
            foreach (var f in fullFileName)
                files.Add(f, f);
            foreach (string fname in files.Values)
                fillDataTable(dtLog, fname, loadInfo);
            return dtLog;
        }


        #region 内部函数处理...
        //查日志表结构。
        private static System.Data.DataTable createDataTable() {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("RowIndex", typeof(System.Int32));
            dtData.Columns.Add("CreateDate", typeof(System.DateTime));
            dtData.Columns.Add("CreateTime", typeof(System.String));
            dtData.Columns.Add("ClientIP", typeof(System.String)).Caption = "用户机器IP";
            // dtData.Columns.Add("UserName", typeof(System.String)).Caption = "用户名称";
            dtData.Columns.Add("ExecuteTime", typeof(System.Decimal)).Caption = "花费(毫秒)";
            dtData.Columns.Add("MessageType", typeof(System.String)).Caption = "消息类型";
            // dtData.Columns.Add("Action", typeof(System.String)).Caption = "动作";
            dtData.Columns.Add("Detail", typeof(System.String)).Caption = "明细";


            return dtData;
        }
        //填充日志文件。
        private static void fillDataTable(DataTable dtData, string fileName, LogDataLoadInfo loadInfo) {
            if (!System.IO.File.Exists(fileName)) return; 
            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);

            string line = null;
            DataRow curRow = null;
            bool hasSetBeginDate = false;
            while ((line = sr.ReadLine()) != null) {
                DateTime sDate = getDatetime(line);

                if (sDate == System.DateTime.MinValue) {
                    if (curRow != null)
                    {
                        string strLine = curRow["Detail"].ToString() + line;
                        curRow["Detail"] = strLine;

                        curRow["ExecuteTime"] = getExecuteTime(strLine, loadInfo);
                    }
                    continue;
                }
                if (!hasSetBeginDate) {
                    loadInfo.BeginDate = sDate;
                    hasSetBeginDate = true;
                }

                loadInfo.EndDate = sDate;
                curRow = dtData.NewRow();
                curRow["RowIndex"] = dtData.Rows.Count;
                curRow["CreateDate"] = sDate;
                if (sDate != System.DateTime.MinValue)
                    curRow["CreateTime"] = sDate.ToLongTimeString();

                curRow["ClientIP"] = getClientIP(line);
                curRow["MessageType"] = getMessageType(line, loadInfo);
                curRow["Detail"] = getDetail(line);
                curRow["ExecuteTime"] = getExecuteTime(line, loadInfo);
                dtData.Rows.Add(curRow);
            }
            _LasLoadLogData = line;
            sr.Close();
            sr.Dispose();

        }
        //增加剩余的日志数据
        private static void addLeaveLogData(DataTable dtData, string fileName, LogDataLoadInfo loadInfo) {
            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);

            string line = null;
            bool hasFound = false;
            DataRow curRow = null;
            while ((line = sr.ReadLine()) != null) {
                if (!hasFound) {
                    if (string.Compare(line, _LasLoadLogData, true) != 0)
                        continue;
                    hasFound = true;
                }
                DateTime sDate = getDatetime(line);
                if (sDate == System.DateTime.MinValue) {
                    if (curRow != null)
                        curRow["Detail"] = curRow["Detail"].ToString() + line;
                    continue;
                }
                curRow = dtData.NewRow();
                curRow["RowIndex"] = dtData.Rows.Count;
                curRow["CreateDate"] = sDate;
                if (sDate != System.DateTime.MinValue)
                    curRow["CreateTime"] = sDate.ToLongTimeString();
                curRow["ClientIP"] = getClientIP(line);
                curRow["MessageType"] = getMessageType(line, loadInfo);
                curRow["Detail"] = getDetail(line);
                curRow["ExecuteTime"] = getExecuteTime(line, loadInfo);
                dtData.Rows.Add(curRow);
            }
            _LasLoadLogData = line;
            sr.Close();
            sr.Dispose();
        }
        //日志日期 
        public  static DateTime getDatetime(string strLine) {
            if (strLine.Length == 0 || strLine.Substring(0, 1) == " ")
                return System.DateTime.MinValue;

            string logTimeEndSign = System.Configuration.ConfigurationManager.AppSettings["LogTimeEndSign"];
            if (string.IsNullOrEmpty(logTimeEndSign))
                logTimeEndSign = ":(";
            int lastFlag = strLine.IndexOf(logTimeEndSign);
            if (lastFlag < 0) return System.DateTime.MinValue;
            if (strLine.IndexOf('-') != 4) return System.DateTime.MinValue;
            try {
                string val = strLine.Substring(0, lastFlag);
                DateTime rdt = DateTime.MinValue;
                System.DateTime.TryParse(val, out rdt);

                return rdt;
                
            }
            catch {
                return System.DateTime.MinValue;
            }
        }
        //获取机器IP
        public static string getClientIP(string strLine) {
            if (string.IsNullOrEmpty(strLine)) return string.Empty;
            int firstFlag = strLine.IndexOf('(');
            int lastFlag = strLine.IndexOf(')');
            if (firstFlag < 0 || lastFlag < 0) return string.Empty;

            return strLine.Substring(firstFlag + 1, lastFlag - firstFlag - 1);
        }
        //获取消息类型
        public static string getMessageType(string strLine, LogDataLoadInfo loadInfo) {
            if (string.IsNullOrEmpty(strLine)) return string.Empty;

            int firstFlag = strLine.IndexOf(')');
            int lastFlag = strLine.IndexOf("-->");
            if (firstFlag < 0 || lastFlag < 0) return string.Empty;
            string str = strLine.Substring(firstFlag + 1, lastFlag - firstFlag - 1);
            if (string.Compare(str, "代码执行轨迹", true) != 0)
                loadInfo.ErrCount++;

            return str;
        }
        //获取消息明细
        public static string getDetail(string strLine) {
            int firstFlag = strLine.IndexOf("-->");
            return strLine.Substring(firstFlag + 1, strLine.Length - firstFlag - 1);
        }
        private static decimal getExecuteTime(string strLine, LogDataLoadInfo loadInfo)
        {
            var s = System.Text.RegularExpressions.Regex.Replace(strLine, @"[^\w().]", "");
            int firstFlag = s.IndexOf(COST_TEXT);
            if (firstFlag < 0)
                return 0m;

            int leave_length = firstFlag + COST_TEXT.Length;
            try
            {
                string val = s.Substring(leave_length, s.Length - leave_length);
                decimal dval = System.Decimal.Parse(val);
                if (dval > 1000m)
                    loadInfo.OverTimeCount++;
                return dval;
            }
            catch (Exception ex)
            {
                return -1m;
            }
        }

        #endregion 内部函数处理...
    }

    class LogDataLoadInfo {
        public DateTime BeginDate;
        public DateTime EndDate;
        public int ErrCount;
        public int OverTimeCount;
    }
}
