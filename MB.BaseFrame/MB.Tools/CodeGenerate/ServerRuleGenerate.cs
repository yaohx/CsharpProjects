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

using System.Drawing;

namespace MB.Tools {
    class ServerRuleGenerate {
        private static readonly string TEMPLETE_NAME = "MB.Tools.CodeTemplet.ServerRuleTemplet.txt";
        private static readonly string TEMPLETE__INTERFACE_NAME = "MB.Tools.CodeTemplet.ServerInterfaceTemplet.txt";

         private static readonly string PARAM_INTERFACE_NAME = "#RuleInterfaceName#";
        private static readonly string PARAM_MODEL_NAME = "#ModelName#";
        private static readonly string PARAM_OBJECT_IDT = "#ObjectIDT#";
        private static readonly string PARAM_TABLE_NAME = "#TableName#";
        private static readonly string PARAM_XML_FILE_NAME = "#XmlFileName#";
        private static readonly string PARAM_RULE_NAME = "#RuleName#";


        private static readonly string[] _BLUE_STRING = new string[] { "public", "private", "class", "typeof", "string",
                                                            "int", "value", "return","get","set","bool","new" ,
                                                            "interface","as","this","override","object"};

        private string _TableName;
        private string _ModelName;
        private string _KeyName;

        public ServerRuleGenerate(string tableName, string modelName, string keyName) {
            _TableName = tableName;
            _ModelName = modelName;
            _KeyName = keyName;
        }
        /// <summary>
        /// 生成代码。
        /// </summary>
        /// <param name="rxtBox"></param>
        public void GenerateToRichTextBox(System.Windows.Forms.RichTextBox rxtBox,bool isInterface) {
            List<string> whileLines = new List<string>();
            string templeteName = isInterface ? TEMPLETE__INTERFACE_NAME : TEMPLETE_NAME;

            List<string> templeteLines = ShareLib.Instance.ReadRfTemplete(templeteName, ref whileLines);
            foreach (string line in templeteLines) {
                string tempLine = line.Replace(ShareLib.PARAM_SYSTEM_TIME,System.DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
                string dataLine = tempLine;
                if(!string.IsNullOrEmpty(line)){
                    dataLine = dataLine.Replace(PARAM_INTERFACE_NAME, "I" + _ModelName);
                    dataLine = dataLine.Replace(PARAM_MODEL_NAME, _ModelName + "Info");
                    dataLine = dataLine.Replace(PARAM_OBJECT_IDT, _ModelName + "IDT");
                    dataLine = dataLine.Replace(PARAM_TABLE_NAME, _TableName);
                    dataLine = dataLine.Replace(PARAM_XML_FILE_NAME, _ModelName);
                    dataLine = dataLine.Replace(PARAM_RULE_NAME, _ModelName);
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
