//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-01
// Description	:	代码生成
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MB.Tools {
    [ProvideProperty("NullValueDescription", typeof(Control))]
    public partial class NullValueProvider : Component, IExtenderProvider {

        private Dictionary<Control, string> _MyNullValueTable;
        public NullValueProvider() {
            InitializeComponent();

            _MyNullValueTable = new Dictionary<Control, string>();
        }

        public NullValueProvider(IContainer container) {
            container.Add(this);

            InitializeComponent();

            _MyNullValueTable = new Dictionary<Control, string>();
        }

        #region IExtenderProvider 成员

        public bool CanExtend(object extendee) {
            if (extendee is TextBox || extendee is RichTextBox)
                return true;
            else if ((extendee is ComboBox) && (extendee as ComboBox).DropDownStyle == ComboBoxStyle.DropDown) {
                return true;
            }

            else
                return false;
        }

        #endregion
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<Control, string> MyNullValueTable {
            get {
                return _MyNullValueTable;
            }
        }
        public bool IsNullValue(Control txtBox) {
            if (!_MyNullValueTable.ContainsKey(txtBox))
                return false;

            if (txtBox.Text == "" || txtBox.Text == _MyNullValueTable[txtBox])
                return true;
            else
                return false;
        }
        public string GetNullValueDescription(Control txtBox) {
            if (_MyNullValueTable.ContainsKey(txtBox))
                return _MyNullValueTable[txtBox];
            else
                return string.Empty;
        }

        public void SetNullValueDescription(Control txtBox, string nullValueDesc) {
            if (string.IsNullOrEmpty(nullValueDesc)) return;
 
            if (_MyNullValueTable.ContainsKey(txtBox)) {
                _MyNullValueTable[txtBox] = nullValueDesc;
            }
            else {
                _MyNullValueTable.Add(txtBox, nullValueDesc);
                txtBox.Enter += new EventHandler(txtBox_Enter);
                txtBox.Leave += new EventHandler(txtBox_Leave);
                txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
            }
            txtBox.ForeColor = Color.Gray;
            txtBox.Text = nullValueDesc;

        }

        void txtBox_TextChanged(object sender, EventArgs e) {
            Control txt = (sender as Control);
            if (txt.Text == "" || txt.Text == _MyNullValueTable[txt]) {
                txt.ForeColor = Color.Gray;
            }
            else {
                txt.ForeColor = Color.Black;
            }
        }

        void txtBox_Leave(object sender, EventArgs e) {
            Control txt = (sender as Control);
            if (txt.Text == "" || txt.Text == _MyNullValueTable[txt]) {
                txt.ForeColor = Color.Gray;
                txt.Text = _MyNullValueTable[txt];
            }
            else {
                txt.ForeColor = Color.Black;
            }
        }

        void txtBox_Enter(object sender, EventArgs e) {
            Control txt = (sender as Control);
            if(txt.Text == _MyNullValueTable[txt])
                txt.Text = "";
            txt.ForeColor = Color.Black;
        }

    }
}
