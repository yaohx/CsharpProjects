using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 自定义的过滤弹出框
    /// 当点击XtraGird列头上的过滤按钮弹出
    /// </summary>
    public class MyXtraGridFilterDialog : DevExpress.XtraGrid.Filter.FilterCustomDialog {
        private DevExpress.Data.Filtering.CriteriaOperator _Opertor; //需要返回的过滤条件

        public MyXtraGridFilterDialog(GridColumn col)
            : base(col) {
        }

        /// <summary>
        /// 得到自定义的过滤条件
        /// </summary>
        /// <returns>自定义的过滤条件</returns>
        public CriteriaOperator GetCustomFiltersCriterion() {
            return _Opertor;
        }

        /// <summary>
        /// 重写过滤条件生成方法，修改LIKE表达式的行为
        /// </summary>
        /// <returns></returns>
        protected override DevExpress.Data.Filtering.CriteriaOperator GetFiltersCriterion() {
            DevExpress.Data.Filtering.CriteriaOperator opertor = base.GetFiltersCriterion();
            resetOperandValue(opertor);
            _Opertor = opertor;
            return opertor;
        }

        /// <summary>
        /// 重新设定表达式，
        /// 如果是LIKE表达式，自动在前后加上%%
        /// </summary>
        /// <param name="operandValue">需要被设定的表达式</param>
        private void resetOperandValue(CriteriaOperator critalOperator) {
            if (critalOperator is GroupOperator) {
                //多个表达式连接在一起的
                GroupOperator groupOperator = critalOperator as GroupOperator;
                foreach (CriteriaOperator cOperator in groupOperator.Operands) {
                    resetOperandValue(cOperator);
                }
            }
            else if (critalOperator is UnaryOperator) {
                //一元表达式，例如NOT，一元表示下还可能包括二元表达式
                UnaryOperator unaryOperator = critalOperator as UnaryOperator;
                resetOperandValue(unaryOperator.Operand);
            }
            //二元表达式，需要被格式化的表达式
            else if (critalOperator is BinaryOperator) {
                BinaryOperator binaryOperator = critalOperator as BinaryOperator;
                if (binaryOperator.OperatorType == BinaryOperatorType.Like) {
                    OperandValue operandValue = binaryOperator.RightOperand as OperandValue;
                    if (operandValue.Value is string) {
                        if (((string)operandValue.Value).IndexOf("%") < 0)
                            ((OperandValue)((BinaryOperator)critalOperator).RightOperand).Value = "%" + operandValue.Value.ToString() + "%";
                    }
                }
            }
        }
    }
}
