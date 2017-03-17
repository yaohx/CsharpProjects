using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.DesignEditor;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using MB.Util.Model;

namespace MB.XWinLib.DesignEditor
{
    /// <summary>
    /// 条件样式值编辑器.有些值需要使用下拉框来选择值,比如单据进度.
    /// </summary>
    public class StyleConditionValueEditor : AbstractComboBoxEditor
    {
        /// <summary>
        /// 条件样式值编辑器显示下拉框,把RepositoryItemLookUpEdit中的值显示出来
        /// </summary>
        /// <param name="lstBox"></param>
        /// <param name="contextInstance"></param>
        protected override void AddDataToList(System.Windows.Forms.ListBox lstBox, object contextInstance)
        {
            XtraStyleConditionInfo conditionInfo = contextInstance as XtraStyleConditionInfo;
            GridView gridView = conditionInfo.Tag as GridView;

            if (gridView!=null)
            {
                // 条件样式中的列名
                if (!string.IsNullOrEmpty(conditionInfo.ColumnName))
                {
                    var column = gridView.Columns[conditionInfo.ColumnName];

                    lstBox.Items.Clear();

                    if (column != null)
                    {
                        if (column.ColumnEdit is RepositoryItemLookUpEdit)
                        {
                            var ddl = column.ColumnEdit as RepositoryItemLookUpEdit;

                            if (ddl.DataSource is DataView)
                            {
                                var view = ddl.DataSource as DataView;

                                foreach (DataRow row in view.Table.Rows)
                                {
                                    CodeNameInfo codeName = new CodeNameInfo(
                                        row[ddl.ValueMember].ToString(),
                                        row[ddl.DisplayMember].ToString() + "-" + row[ddl.ValueMember].ToString());
                                    lstBox.Items.Add(codeName);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取选中的值.
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="contextInstance"></param>
        /// <returns></returns>
        protected override object GetSelectedItem(object selectedItem, object contextInstance)
        {
            CodeNameInfo colInfo = selectedItem as CodeNameInfo;                    
            return colInfo.NAME;
        }
    }
}
