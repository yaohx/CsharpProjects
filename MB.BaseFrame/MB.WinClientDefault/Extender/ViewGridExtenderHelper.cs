using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MB.WinBase.IFace;  

namespace MB.WinClientDefault.Extender {
    /// <summary>
    /// 提供网格浏览控件的公共处理方法。
    /// </summary>
    public class ViewGridExtenderHelper {
        /// <summary>
        /// 通过单据状态绘制 行的状态。
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="gridView"></param>
        /// <param name="e"></param>
        public static void CustomDrawRowStyleByDocState(IViewGridForm parentForm, DevExpress.XtraGrid.Views.Grid.GridView gridView, 
                                                        DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e) {
            if (e.RowHandle < 0) return;

            object entity = gridView.GetRow(e.RowHandle);
            //在新增对象又关闭新增的情况下，虽然rowHandle还存在，但是由于对象已经被取消，数据源中为NULL
            if (entity == null) return;


            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(entity);
            if (!exists) return;

            var v = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(entity);
            if (v == MB.Util.Model.DocState.Progress) {
                if (!gridView.IsRowSelected(e.RowHandle))
                 e.Appearance.ForeColor = Color.Blue;
            }

        }

    }
}
