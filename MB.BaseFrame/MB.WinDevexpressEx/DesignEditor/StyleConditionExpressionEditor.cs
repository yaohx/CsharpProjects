using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.DesignEditor;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Design;
using MB.XWinLib.XtraGrid;
using System.Windows.Forms;
using DevExpress.XtraGrid;


namespace MB.XWinLib.DesignEditor
{
    /// <summary>
    /// 条件表达式编辑器
    /// </summary>
    public class StyleConditionExpressionEditor : AbstractModalEditor
    {
        // Fields
        private ConditionExpressionEditorForm _form;

        /// <summary>
        /// 获取编辑框
        /// </summary>
        /// <param name="instance">XtraStyleConditionInfo实例</param>
        /// <returns>返回编辑框实例</returns>
        protected override Form GetDialog(object instance)
        {
            XtraStyleConditionInfo conditionInfo = instance as XtraStyleConditionInfo;
            GridView gridView = conditionInfo.Tag as GridView;

            StyleFormatCondition condition=null;

            foreach (StyleFormatCondition item in gridView.FormatConditions)
            {
                if(item.Tag.ToString()==conditionInfo.Name)
                {
                    condition=item;
                    break;
                }
            }

            this._form = new ConditionExpressionEditorForm(condition, null);

            return this._form;
        }

        /// <summary>
        /// 获取表达式
        /// </summary>
        /// <returns>表达式字符串</returns>
        protected override object GetEditValue()
        {
            return this._form.Expression;
        }
    }
}
