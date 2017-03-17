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

namespace MB.Tools.CodeGenerate {
    class LocalLanguageObject {
        private Dictionary<string, string> _LanguageTable;

        public LocalLanguageObject(System.Windows.Forms.RichTextBox rxtLanguage) {
            _LanguageTable = new Dictionary<string, string>();
            if (rxtLanguage.Text != "") {
                string[] vs = rxtLanguage.Text.Split(new string[] { "\n" },int.MaxValue, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in vs) {
                    string temp = s.Replace("\t", " ");
                    string[] line = temp.Split(' ');
                    if (line.Length > 1) {
                        if (_LanguageTable.ContainsKey(line[1])) continue;
                        _LanguageTable.Add(line[1], line[0]);
                    }
                }
            }
        }

        public string GetDescription(string fieldName) {
            if (_LanguageTable.ContainsKey(fieldName))
                return _LanguageTable[fieldName];
            else
                return fieldName;
        }
    }
}
