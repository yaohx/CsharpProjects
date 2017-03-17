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

using MB.Tools.CodeGenerate;
namespace MB.Tools {
    class UIXmlGenerateHelper {
        private static readonly string TEMPLETE_NAME = "MB.Tools.CodeTemplet.UIXmlTemplet.txt";
        private static readonly string PARAM_PROPERTY_NAME = "#PropertyName#";
        private static readonly string PARAM_DESCRIPTION = "#Description#";
        private static readonly string PARAM_IS_KEY= "#IsKey#";
        private static readonly string PARAM_IS_NULL = "#IsNull#";
        private static readonly string PARAM_VISIBLED = "#Visibled#";
         private static readonly string PARAM_CANEDIT = "#CanEdit#";
         private static readonly string PARAM_WIDTH = "#VisibleWidth#";
        private static readonly string PARAM_DATA_TYPE = "#DataType#";

        private static readonly string[] _RED_STRING = new string[] { "Name", "Description", "DataType", "IsKey", "IsNull","CanEdit", "Visibled", "VisibleWidth" };

        private DataColumnCollection _DataColumns;
        private LocalLanguageObject _Language;
        public UIXmlGenerateHelper(DataColumnCollection dcs, LocalLanguageObject language) {
            _DataColumns = dcs;
            _Language = language;
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

                if (string.Compare(tempLine.Trim(), ShareLib.PARAM_WHILE, true) == 0) {
                    foreach (DataColumn dc in _DataColumns) {
                        foreach (string pro in whileLines) {
                            string propertyLine = pro;
                            if (!string.IsNullOrEmpty(propertyLine)) {
                                propertyLine = propertyLine.Replace(PARAM_PROPERTY_NAME, dc.ColumnName);
                                propertyLine = propertyLine.Replace(PARAM_DESCRIPTION, _Language.GetDescription(dc.ColumnName));
                                propertyLine = propertyLine.Replace(PARAM_IS_KEY, "False");
                                propertyLine = propertyLine.Replace(PARAM_IS_NULL, "True");
                                propertyLine = propertyLine.Replace(PARAM_VISIBLED, "True");
                                propertyLine = propertyLine.Replace(PARAM_CANEDIT, "True");
                                propertyLine = propertyLine.Replace(PARAM_WIDTH, "100");
                                string dataType = ShareLib.Instance.ConvertDataTypeFullName(dc.ColumnName, dc.DataType);
                                propertyLine = propertyLine.Replace(PARAM_DATA_TYPE, dataType);
                            }
                            appendLine(rxtBox, propertyLine);
                        }
                    }
                    continue;
                }
                string dataLine = tempLine;
                appendLine(rxtBox, dataLine);

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
       
    }
}
