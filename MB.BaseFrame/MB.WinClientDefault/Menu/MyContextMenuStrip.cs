using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;

namespace MB.WinClientDefault.Menu
{
    /// <summary>
    /// 自定义扩展右键菜单
    /// </summary>
    public partial class MyContextMenuStrip : ContextMenuStrip
    {
        private ColumnEditCfgInfo _ColumnEditCfgInfo;
        private Control _ParentControl;
        /// <summary>
        /// Constructor method
        /// </summary>
        public MyContextMenuStrip()
        {
            CreateContextMenu();
        }

        /// <summary>
        /// XML配置项
        /// </summary>
        public ColumnEditCfgInfo ColumnEditCfgInfo
        {
            get { return _ColumnEditCfgInfo; }
            set { _ColumnEditCfgInfo = value; }
        }

        /// <summary>
        /// 菜单所属控件
        /// </summary>
        public Control ParentControl
        {
            get { return _ParentControl; }
            set { _ParentControl = value; }
        }

        /// <summary>
        /// Create Menu Method
        /// </summary>
        public virtual void CreateContextMenu()
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = "tsmiViewDetail";
            item.Text = "查看详情";

            this.Items.Add(item);

            ToolStripMenuItem itemCopyCell = new ToolStripMenuItem();
            itemCopyCell.Name = "tsmiCopyCell";
            itemCopyCell.Text = "复制单元格";

            this.Items.Add(itemCopyCell);
        }

        /// <summary>
        /// Item Click Event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.Equals("tsmiViewDetail"))
            {
                object value = GetValueFromGrid( _ColumnEditCfgInfo.TextFieldName);
                if (value != null)
                    MB.WinBase.ObjectDataFilterAssistantHelper.Instance.ShowProperty(_ColumnEditCfgInfo, _ColumnEditCfgInfo.TextFieldName, value);
            }

            else if (e.ClickedItem.Name.Equals("tsmiCopyCell"))
            {
                object value = GetValueFromGrid(string.Empty);
                if (value != null)
                    Clipboard.SetText(value.ToString());
                this.Close();
            }
        }

        private object GetValueFromGrid(string cfgPropertyName)
        {
            string propertyName = cfgPropertyName;
            object value = null;
            DevExpress.XtraGrid.GridControl grid = ParentControl as DevExpress.XtraGrid.GridControl;
            if (grid != null)
            {
                var view = grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view.FocusedRowHandle < 0) return null;

                object row = view.GetFocusedRow();
                if (string.IsNullOrEmpty(propertyName))
                    propertyName = view.FocusedColumn.FieldName;

                if (string.Compare(row.GetType().Name, "DataRowView", true) == 0)
                {
                    System.Data.DataRowView drRow = row as System.Data.DataRowView;
                    value = drRow[propertyName];
                }
                else
                {
                    if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(row, propertyName))
                        value = MB.Util.MyReflection.Instance.InvokePropertyForGet(row, propertyName);
                }
            }
            return value;
        }
    }
}
