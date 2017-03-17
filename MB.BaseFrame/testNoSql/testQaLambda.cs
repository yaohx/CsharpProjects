using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace testNoSql
{
    class testQaLambda
    {
        public void testGetMemberName() {
            ExpressionHelper.GetPropertyName<myClass>(o => o.Code);



        }
       
        public string MyData { get; set; }

        #region private methods

        #endregion



    }
    public class ExpressionHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> expression) {
            return GetNameFromLambda(expression);
        }
        public static string GetPropertyName<T>(Expression<Func<T, Object>> expression) {
            return GetNameFromLambda(expression);
        }
        public static string GetMethodName<T>(Expression<Action<T>> expression) {
            return GetNameFromLambda(expression);
        }
        private static string GetNameFromLambda(LambdaExpression lambda) {
            MemberExpression memberExpression = null;
            if (lambda.Body is UnaryExpression) {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            } else if (lambda.Body is MemberExpression) memberExpression = lambda.Body as MemberExpression; if (memberExpression != null)
                return memberExpression.Member.Name; if (lambda.Body is MethodCallExpression) {
                var methodCallExpression = lambda.Body as MethodCallExpression; return methodCallExpression.Method.Name;
            }
            throw new ArgumentException(String.Format("Expression '{0}' did not provide a property or method name.", lambda));
        }
    }
    class myClass
    {
        public string Code { get; set; }
    }
}
