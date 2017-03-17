using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MB.Util.Expressions
{
    /// <summary>
    /// ExpressionHelper. 从表达式中获取属性或者方法的名称。
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// 从Action 中获取属性名称。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Action<T>> expression) {
            return getNameFromLambda(expression);
        }
        /// <summary>
        /// 从函数中获取属性名称。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T>> expression) {
            return getNameFromLambda(expression);
        }
        /// <summary>
        /// 从函数中获取属性名称。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T, Object>> expression) {
            return getNameFromLambda(expression);
        }
        /// <summary>
        /// 从方法中获取名称。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetMethodName<T>(Expression<Action<T>> expression) {
            return getNameFromLambda(expression);
        }
        //
        private static string getNameFromLambda(LambdaExpression lambda) {
            MemberExpression memberExpression = null;
            if (lambda.Body is UnaryExpression) {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            } else if (lambda.Body is MemberExpression)
                memberExpression = lambda.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.Name;
            if (lambda.Body is MethodCallExpression) {
                var methodCallExpression = lambda.Body as MethodCallExpression;
                return methodCallExpression.Method.Name;
            }
            throw new ArgumentException(String.Format("Expression '{0}' 没有提供方法的名称或者属性的名称.", lambda));
        }
    } 
}
