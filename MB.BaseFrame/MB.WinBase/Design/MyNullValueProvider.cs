using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MB.WinBase.Design {
    /// <summary>
    /// 空值提示描述 
    /// </summary>
    [ProvideProperty("NullValueDescription", typeof(Control))]
    public partial class MyNullValueProvider : Component, IExtenderProvider {

        private Dictionary<Control, string> _MyNullValueTable;
        public MyNullValueProvider() {
            InitializeComponent();

            _MyNullValueTable = new Dictionary<Control, string>();
        }

        public MyNullValueProvider(IContainer container) {
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
        /// <summary>
        /// 判断当前控件是否为空值。
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        public bool IsNullValue(Control txtBox) {
            if (!_MyNullValueTable.ContainsKey(txtBox))
                return false;

            if (txtBox.Text == "" || txtBox.Text == _MyNullValueTable[txtBox])
                return true;
            else
                return false;
        }
        /// <summary>
        ///  获取或者设置控值的描述信息
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        [Description("获取或者设置控值的描述信息"), Category("布局")]
        public string GetNullValueDescription(Control txtBox) {
            if (_MyNullValueTable.ContainsKey(txtBox))
                return _MyNullValueTable[txtBox];
            else
                return string.Empty;
        }
        /// <summary>
        ///  获取或者设置控值的描述信息
        /// </summary>
        /// <param name="txtBox"></param>
        /// <param name="nullValueDesc"></param>
        [Description("获取或者设置控值的描述信息"), Category("布局")]
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
        private bool _InnerEvent;
        #region 对象事件...
        void txtBox_TextChanged(object sender, EventArgs e) {
            if (_InnerEvent) return;

            Control txt = (sender as Control);
            if (string.IsNullOrEmpty(txt.Text) || txt.Text == _MyNullValueTable[txt]) {
                txt.ForeColor = Color.Gray;
                txt.Text = _MyNullValueTable[txt];
            }
            else {
                txt.ForeColor = Color.Black;
            }
        }

        void txtBox_Leave(object sender, EventArgs e) {
            Control txt = (sender as Control);
            if (string.IsNullOrEmpty(txt.Text) || txt.Text == _MyNullValueTable[txt]) {
                txt.ForeColor = Color.Gray;
                txt.Text = _MyNullValueTable[txt];
            }
            else {
                txt.ForeColor = Color.Black;
            }
        }

        void txtBox_Enter(object sender, EventArgs e) {
           
            Control txt = (sender as Control);
            if (string.Compare(txt.Text, _MyNullValueTable[txt], true) == 0 || txt.ForeColor == Color.Gray) {
                _InnerEvent = true;
                txt.Text = "";
                _InnerEvent = false;
            }
            txt.ForeColor = Color.Black;
        }
        #endregion 对象事件...

    }
}
