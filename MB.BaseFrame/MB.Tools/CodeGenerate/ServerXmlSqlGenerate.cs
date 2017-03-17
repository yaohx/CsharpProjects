//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-01
// Description	:	代码生成
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace MB.Tools {
    class ServerXmlSqlGenerate {
        private static readonly string TEMPLETE_NAME = "MB.Tools.CodeTemplet.ServerXmlTemplet.txt";
        private static readonly string PARAM_SQL_NAME = "#SqlName#";
        private static readonly string PARAM_SQL_CONTEND_NAME = "#SqlContent#";
        private static readonly string PARAM_SQL_PARAMS_NAME = "#SqlParam#";
        private static readonly string PARAM_TABLE_NAME = "#TableName#";
        private static readonly string PARAM_KEY_NAME = "#KeyName#";

        private static readonly string[] _RED_STRING = new string[] { "Name", "Column", "TypeName" };
        private static readonly string  SQL_PARAMS = " <Param Name = '@{0}' Column = '{1}' TypeName = '{2}' />";
        private string _TableName;
        private DataColumnCollection _DataColumns;
        public ServerXmlSqlGenerate(string tableName, DataColumnCollection dcs) {
            _TableName = tableName;
            _DataColumns = dcs;
        }

        /// <summary>
        /// 生成代码。
        /// </summary>
        /// <param name="rxtBox"></param>
        public void GenerateToRichTextBox(System.Windows.Forms.RichTextBox rxtBox) {
            List<string> whileLines = new List<string>();
            List<string> templeteLines = ShareLib.Instance.ReadRfTemplete(TEMPLETE_NAME, ref whileLines);
            foreach (string line in templeteLines) {
               string tempLine = line.Replace(ShareLib.PARAM_SYSTEM_TIME,System.DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
               tempLine = tempLine.Replace(PARAM_TABLE_NAME, _TableName);
                tempLine = tempLine.Replace(PARAM_KEY_NAME, "ID");


                if (string.Compare(tempLine.Trim(), ShareLib.PARAM_WHILE, true) == 0) {
                    List<string> sqlParams = new List<string>();
                    string sqlString = generateSelect(ref sqlParams);
                    appendSql(rxtBox, whileLines, "SelectObject", sqlString, sqlParams);

                    sqlString = generateInsert(ref sqlParams);
                    appendSql(rxtBox, whileLines, "AddObject",sqlString, sqlParams);

                    sqlString = generateUpdate(ref sqlParams);
                    appendSql(rxtBox, whileLines, "UpdateObject",sqlString, sqlParams);

                    sqlString = generateDelete(ref sqlParams);
                    appendSql(rxtBox, whileLines, "DeleteObject",sqlString, sqlParams);

                    continue;
                }

                appendLine(rxtBox, tempLine);

            }
        }
        private void appendLine(System.Windows.Forms.RichTextBox rxtBox, string text) {
            if (!string.IsNullOrEmpty(text)) {
                string[] ss = text.Split(' ');
                foreach (string s in ss) {
                    Color oldColor = rxtBox.SelectionColor;
                    if (Array.IndexOf<string>(_RED_STRING, s) >= 0)
                        rxtBox.SelectionColor = Color.Red;
                    else if (s.IndexOf('"') == 0)
                        rxtBox.SelectionColor = Color.Blue;
                    else
                        rxtBox.SelectionColor = Color.FromArgb(192, 0, 0);

                    rxtBox.AppendText(" ");
                    rxtBox.AppendText(s);
                    rxtBox.SelectionColor = oldColor;
                }
            }
            else {
                rxtBox.AppendText(text);
            }
            rxtBox.AppendText(" \n");
        }
        private void appendSqlLine(System.Windows.Forms.RichTextBox rxtBox, string text) {
            Color oldColor = rxtBox.SelectionColor;
            rxtBox.SelectionColor = Color.Blue;
            rxtBox.AppendText("<![CDATA[" + "\n");
            rxtBox.SelectionColor = Color.Gray;
            rxtBox.AppendText(text + "\n");
            rxtBox.SelectionColor = Color.Blue;
            rxtBox.AppendText("]]>" + "\n");
            rxtBox.SelectionColor = oldColor;
        }
        private void appendSql(System.Windows.Forms.RichTextBox rxtBox, List<string> whileLines,string sqlName,string sqlString,List<string> sqlParams) {
            foreach (string pro in whileLines) {
                string line = pro;
                if (line.IndexOf(PARAM_SQL_NAME) >= 0) {
                    line = line.Replace(PARAM_SQL_NAME, sqlName); 
                }
                else if (line.IndexOf(PARAM_SQL_CONTEND_NAME) >= 0) {
                    line = line.Replace(PARAM_SQL_CONTEND_NAME, sqlString);
                  
                    appendSqlLine(rxtBox, line);

                    continue;
                }
                else if (line.IndexOf(PARAM_SQL_PARAMS_NAME) >= 0) {
                    foreach (string p in sqlParams) {
                        line = p;
                        line = line.Replace("'", System.Convert.ToChar(34).ToString());
                        appendLine(rxtBox, line);
                    }
                    continue;
                }
                else {

                }

                appendLine(rxtBox, line);
            }


        }

        //生成选择语句
        private string generateSelect(ref List<string> parasLines) {
            parasLines.Clear();
            string sql = "SELECT \n {0} \n FROM \n {1} \n  WHERE @Where ;";
            string[] cols = getColumns(false);
            sql = string.Format(sql, string.Join(",", cols),_TableName);
            parasLines.Add(string.Format(SQL_PARAMS,"Where","Where","System.String"));

            return sql;
        }
        //生成insert 语句
        private string generateInsert(ref List<string> parasLines) {
            parasLines.Clear();
            string sql = "INSERT INTO \n {0} \n ({1}) \n VALUES( \n {2});";
            string[] cols = getColumns(false);
            string[] valCols = getColumns(true);
            sql = string.Format(sql,_TableName, string.Join(",", cols), string.Join(",", valCols));
            foreach (DataColumn dc in _DataColumns) {
                string dataType = ShareLib.Instance.ConvertDataTypeFullName(dc.ColumnName,dc.DataType);
                parasLines.Add(string.Format(SQL_PARAMS, dc.ColumnName, dc.ColumnName, dataType));
            }
            return sql;
        }
        //生成Update 语句
        private string generateUpdate(ref List<string> parasLines) {
            parasLines.Clear();
            string sql = "UPDATE  {0} \n SET  {1} \n WHERE ID = @ID ;";
            string[] cols = getUpdateCols();
            sql = string.Format(sql,_TableName, string.Join(",", cols));
            foreach (DataColumn dc in _DataColumns) {
                string dataType = ShareLib.Instance.ConvertDataTypeFullName(dc.ColumnName, dc.DataType);

                parasLines.Add(string.Format(SQL_PARAMS, dc.ColumnName, dc.ColumnName, dataType));
            }
            return sql;
        }
        //生成删除语句
        private string generateDelete(ref List<string> parasLines) {
            parasLines.Clear();
            string sql = "DELETE \n FROM {0} \n WHERE ID = @ID;";
            sql = string.Format(sql, _TableName); 
            parasLines.Add(string.Format(SQL_PARAMS, "ID", "ID", "System.Int32"));
            return sql;
        }

        private string[] getColumns(bool isValueParams) {
            List<string> cols = new List<string>();
            foreach (DataColumn dc in _DataColumns) {
                if (isValueParams)
                    cols.Add("@" + dc.ColumnName + " \n ");
                else
                    cols.Add(dc.ColumnName + " \n "); 
            }
            return cols.ToArray<string>(); 
        }
        private string[] getUpdateCols() {
            List<string> cols = new List<string>();
            foreach (DataColumn dc in _DataColumns) {
                cols.Add(dc.ColumnName + " = @" + dc.ColumnName + " \n ");
            }
            return cols.ToArray<string>(); 
        }

    }
}
