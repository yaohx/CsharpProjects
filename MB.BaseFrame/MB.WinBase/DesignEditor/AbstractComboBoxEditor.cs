using System;
using System.Data;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace MB.WinBase.DesignEditor {

    /// <summary>
    /// 提供下拉选择编辑的方法。
    /// </summary>
    public abstract class AbstractComboBoxEditor : System.Drawing.Design.UITypeEditor {
        protected IServiceProvider _Provider;
        ListBox _LstFields;
        public AbstractComboBoxEditor() {
            _LstFields = new ListBox();
        }

        public override object EditValue(ITypeDescriptorContext context,
            IServiceProvider provider, object value) {
            _Provider = provider;
            _LstFields.BorderStyle = BorderStyle.None;
            _LstFields.SelectedIndexChanged += new EventHandler(lstFields_SelectedIndexChanged);

            AddDataToList(_LstFields, context.Instance);

            // 如果下拉列表为空,则不显示该控件
            if (_LstFields.Items.Count > 0)
            {
                ((IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService))).DropDownControl(_LstFields);

                if (_LstFields.SelectedItem != null)
                {
                    return GetSelectedItem(_LstFields.SelectedItem, context.Instance);
                }
                else
                {
                    return value;
                }
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {

            if ((context != null) && (context.Instance != null))
                return UITypeEditorEditStyle.DropDown;

            return base.GetEditStyle(context);
        }
        private void lstFields_SelectedIndexChanged(object sender, EventArgs e) {
            ((IWindowsFormsEditorService)_Provider.GetService(
                typeof(IWindowsFormsEditorService))).CloseDropDown();
        }

        //增加绑定字段 。
        protected virtual void AddDataToList(ListBox pLst,object contextInstance) {
            throw new MB.Util.APPException("AddDataToList 方法必须实现");

        }
        //获取选择的行。
        protected virtual object GetSelectedItem(object selectedItem,object contextInstance) {
            return selectedItem;
        }
    }
}
