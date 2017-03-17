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
    /// 下拉计算列的数字选择处理。
    /// </summary>
    [UserRepositoryItem("Register")]
    public class XtraRepositoryItemCalcEditEx : DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit {
        internal const string EditorName = "MyCalcEdit";

        static XtraRepositoryItemCalcEditEx() {
            Register();
        }
        public XtraRepositoryItemCalcEditEx() {
               
        }

        public static void Register() {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(MyCalcEdit),
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
            return new XtraRepositoryItemCalcEditEx();
        }
    }
    [ToolboxItem(false)]
    public class MyCalcEdit : DevExpress.XtraEditors.CalcEdit {
        //取消滚轮 增 减
        protected override void OnSpin(DevExpress.XtraEditors.Controls.SpinEventArgs e) {
            e.Handled = true;
            base.OnSpin(e);
        }

    }
}
