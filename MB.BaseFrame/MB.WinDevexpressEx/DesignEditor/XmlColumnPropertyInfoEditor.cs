using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinBase.DesignEditor;
namespace MB.XWinLib.DesignEditor {
    /// <summary>
    /// 条件样式下拉选择字段编辑控件。
    /// </summary>
    public class XmlColumnPropertyInfoEditor : AbstractComboBoxEditor {
        /// <summary>
        /// 加载选择的列信息。
        /// </summary>
        /// <param name="lstBox"></param>
        /// <param name="contextInstance"></param>
        protected override void AddDataToList(System.Windows.Forms.ListBox lstBox, object contextInstance) {
            if (contextInstance == null ) return;

            XtraStyleConditionInfo conditionInfo = contextInstance as XtraStyleConditionInfo;
            DevExpress.XtraGrid.Views.Grid.GridView  gridView = conditionInfo.Tag as DevExpress.XtraGrid.Views.Grid.GridView;
            
          
            lstBox.Items.Clear();
            foreach (DevExpress.XtraGrid.Columns.GridColumn colInfo in gridView.Columns) {
                MB.Util.Model.CodeNameInfo codeName = new MB.Util.Model.CodeNameInfo(colInfo.FieldName, colInfo.Caption);
                lstBox.Items.Add(codeName);
            }
        }
        protected override object GetSelectedItem(object selectedItem, object contextInstance) {
            MB.Util.Model.CodeNameInfo colInfo = selectedItem as MB.Util.Model.CodeNameInfo;
            XtraStyleConditionInfo conditionInfo = contextInstance as XtraStyleConditionInfo;
            conditionInfo.ColumnName = colInfo.CODE;
            return colInfo.NAME;
        }
    }
}
