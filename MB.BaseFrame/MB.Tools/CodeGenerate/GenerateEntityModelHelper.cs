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
    class GenerateEntityModelHelper {
        private static readonly string TEMPLETE_NAME = "MB.Tools.CodeTemplet.EntityModelTemplet.txt";
        private static readonly string PARAM_XML_FILE_NAME = "#XmlFileName#";
        private static readonly string PARAM_TABLE_NAME = "#TableName#";
        private static readonly string PARAM_MODEL_NAME = "#ModelName#";
        private static readonly string PARAM_KEY_PROPERTY = "#KeyProperty#";
        private static readonly string PARAM_PROPERTY_NAME = "#PropertyName#";
        private static readonly string PARAM_DATA_TYPE = "#DataType#";
        private static readonly string PARAM_SQL_DATA_TYPE = "#SqlDataType#";
       //玩玩
        private static readonly string[] _BLUE_STRING = new string[] { "public", "private", "class", "typeof", "string", "int", "value", "return","get","set","bool","new" };

        private string _TableName;
        private string _ModelName;
        private string _KeyName;
        private DataColumnCollection _DataColumns;

        public GenerateEntityModelHelper(string tableName, string modelName, string keyName, DataColumnCollection dcs) {
            _TableName = tableName;
            _ModelName = modelName;
            _KeyName = keyName;
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

                if (string.Compare(line.Trim(), ShareLib.PARAM_WHILE, true) == 0) {
                    foreach (DataColumn dc in _DataColumns) {
                        foreach (string pro in whileLines) {
                            string propertyLine = pro;
                            if (!string.IsNullOrEmpty(propertyLine)) {
                                propertyLine = propertyLine.Replace(PARAM_PROPERTY_NAME, dc.ColumnName);

                                string dataType = ShareLib.Instance.ConvertDataType(dc, dc.DataType);
                                propertyLine = propertyLine.Replace(PARAM_DATA_TYPE, dataType);

                                propertyLine = propertyLine.Replace(PARAM_SQL_DATA_TYPE, ShareLib.Instance.SystemTypeNameToDbType(dc.ColumnName, dc.DataType.FullName));
                            }
                            appendLine(rxtBox,propertyLine);
                        }
                    }
                    continue;
                }
                string dataLine = tempLine;
                if(!string.IsNullOrEmpty(tempLine)){
                    dataLine = dataLine.Replace(PARAM_TABLE_NAME, _TableName);
                    dataLine = dataLine.Replace(PARAM_MODEL_NAME, _ModelName + "Info");
                    dataLine = dataLine.Replace(PARAM_KEY_PROPERTY, _KeyName);
                    dataLine = dataLine.Replace(PARAM_XML_FILE_NAME, _ModelName); 
                }
                appendLine(rxtBox,dataLine);
 
            }
        }
        private void appendLine(System.Windows.Forms.RichTextBox rxtBox, string text) {
            if (!string.IsNullOrEmpty(text)) {
                string[] ss = text.Split(' ');
                foreach (string s in ss) {
                    Color oldColor = rxtBox.SelectionColor;
                    if (Array.IndexOf<string>(_BLUE_STRING, s) >= 0)
                        rxtBox.SelectionColor = Color.Blue;

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


    }
}
