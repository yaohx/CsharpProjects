using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.Util;

namespace MB.WinBase.Design {
    [ToolboxItem(true)]
    [ProvideProperty("ComboxSaveMaxCount", typeof(ComboBox))]
    public partial class MyComboxRememberProvider : Component, IExtenderProvider {
        private Dictionary<ComboBox, int> _MyValueTable;

        public MyComboxRememberProvider() {
            InitializeComponent();

            _MyValueTable = new Dictionary<ComboBox, int>();
        }

        public MyComboxRememberProvider(IContainer container) {
            container.Add(this);

            InitializeComponent();
            _MyValueTable = new Dictionary<ComboBox, int>();
        }
        #region IExtenderProvider 成员

        public bool CanExtend(object extendee) {
            if (extendee is ComboBox) {
                return true;
            }

            else
                return false;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="combox"></param>
        /// <returns></returns>
        [Description("获取或者设置编辑的下拉控件需要存储的项个数。"), Category("数据")]
        public int GetComboxSaveMaxCount(ComboBox combox) {
            if (_MyValueTable.ContainsKey(combox))
                return _MyValueTable[combox];
            else
                return 0;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        /// <param name="maxItem"></param>
        [Description("获取或者设置编辑的下拉控件需要存储的项个数。"), Category("布局")]
        public void SetComboxSaveMaxCount(ComboBox combox, int maxItem) {
            if (_MyValueTable.ContainsKey(combox)) {
                _MyValueTable[combox] = maxItem;
            }
            else {
                _MyValueTable.Add(combox, maxItem);
                combox.Enter += new EventHandler(combox_Enter);
                combox.Leave += new EventHandler(combox_Leave);

            }
        }

        void combox_Enter(object sender, EventArgs e) {
            var box = sender as ComboBox;
            if (box.DropDownStyle == ComboBoxStyle.DropDown)
                MB.WinBase.Ctls.ComboxExtenderHelper.ReadFromFile(box);
        }

        void combox_Leave(object sender, EventArgs e) {
            var box = sender as ComboBox;
            if (box.DropDownStyle == ComboBoxStyle.DropDown) {
                MB.WinBase.Ctls.ComboxExtenderHelper.SaveToFile(box, box.Text);
            }
            else
                MB.WinBase.Ctls.ComboxExtenderHelper.SaveSelected(box);
        }
    }
}
