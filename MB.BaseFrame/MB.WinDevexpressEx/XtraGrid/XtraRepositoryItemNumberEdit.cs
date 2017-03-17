using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 数字编辑控件。
    /// </summary>
    [UserRepositoryItem("Register")]
    public class XtraRepositoryItemNumberEdit : DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit {

        internal const string EditorName = "MySpinEdit";

        static XtraRepositoryItemNumberEdit() {
            Register();
        }
        public XtraRepositoryItemNumberEdit() {

        }

        public static void Register() {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(MySpinEdit),
               null, typeof(DevExpress.XtraEditors.ViewInfo.ButtonEditViewInfo),
               new DevExpress.XtraEditors.Drawing.ButtonEditPainter(), true, null));
        }
        public override string EditorTypeName {
            get {
                return EditorName;
            }
        }

        public override BaseEdit CreateEditor() {
            BaseEdit edit = base.CreateEditor();
            return edit;
        }
        //主要通过反射的方式创建一个新的 RepositoryItem（如 在该控件对应的查询窗口中）
        //通过覆盖它避免反射创建不成功 以及需要的参数为空
        protected override RepositoryItem CreateRepositoryItem() {
            return new XtraRepositoryItemNumberEdit();
        }
      
    }
    [ToolboxItem(false)] 
    public class MySpinEdit : DevExpress.XtraEditors.SpinEdit {
        //const int WM_MOUSEWHEEL = 0x20A;
        //protected override void WndProc(ref System.Windows.Forms.Message msg) {
        //    if (msg.Msg == WM_MOUSEWHEEL) {
        //        return;
        //    }
        //    //System.Windows.Forms.MessageBox.Show("OK");  
        //    base.WndProc(ref msg);
        //}
        //protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e) {
        //    //base.OnMouseWheel(e);
             
        //}
        //取消滚轮 增 减
        protected override void OnSpin(DevExpress.XtraEditors.Controls.SpinEventArgs e) {
            e.Handled = true;
            base.OnSpin(e);
        }

    }
}
