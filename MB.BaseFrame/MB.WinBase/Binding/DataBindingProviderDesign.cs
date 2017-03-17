using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AllowPartiallyTrustedCallers]
namespace MB.WinBase.Binding {
    /// <summary>
    /// 数据绑定
    /// </summary>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class DataBindingProviderDesign : UITypeEditor {
        private static IContainer _ParentContainer;
        public static void SetParentContainer(IContainer container) {
            _ParentContainer = container;
        }

        private IServiceProvider _Provider;
        public DataBindingProviderDesign() {
        }
        public override object EditValue(ITypeDescriptorContext context,
            IServiceProvider provider, object value) {

            _Provider = provider;
            
            ListBox lstFields = new ListBox();
            lstFields.SelectedIndexChanged += new EventHandler(lstFields_SelectedIndexChanged);


            if (_ParentContainer != null) {
                foreach (Component s in _ParentContainer.Components) {
                    IDataBindingProvider iProvider = s as IDataBindingProvider;
                    if (iProvider != null) {
                        lstFields.Items.Add(s);
                    }

                }
            }
            // Display the combolist
            ((IWindowsFormsEditorService)provider.GetService(
                typeof(IWindowsFormsEditorService))).DropDownControl(lstFields);
            if (lstFields.SelectedItem != null) {
                return lstFields.SelectedItem;
            }
            else {
                return value;
            }

        }

        void lstFields_SelectedIndexChanged(object sender, EventArgs e) {
            ((IWindowsFormsEditorService)_Provider.GetService(
               typeof(IWindowsFormsEditorService))).CloseDropDown();
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            if ((context != null) && (context.Instance != null))
                return UITypeEditorEditStyle.DropDown;

            return base.GetEditStyle(context);
        } 

    }
}
