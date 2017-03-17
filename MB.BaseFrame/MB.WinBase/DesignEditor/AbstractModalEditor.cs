using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace MB.WinBase.DesignEditor
{
    public abstract class AbstractModalEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            this.GetDialog(context.Instance).ShowDialog();
            return this.GetEditValue();
        }

        protected abstract Form GetDialog(object instance);

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if ((context != null) && (context.Instance != null))
            {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context);
        }

        protected abstract object GetEditValue();
    }
}
