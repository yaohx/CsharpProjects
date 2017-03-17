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
    ///  约束编辑的属性是否可以进行输入。
    /// </summary>
    public class ConvertToDropDownList : StringConverter {
        private TypeConverter.StandardValuesCollection theValues;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
            return true;
        }
    }
}
