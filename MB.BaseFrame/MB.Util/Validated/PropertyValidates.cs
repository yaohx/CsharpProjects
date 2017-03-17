using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace MB.Util.Validated
{
    #region PropertyValidates...
    /// <summary>
    /// 列的描述信息集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PropertyValidates<T> : PropertyValidates
    {
        private bool _CreateAC;
        private Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> _QuickDataAccess;

        #region 构造函数...

        /// <summary>
        /// 
        /// </summary>
        public PropertyValidates()
            : this(false) {

        }
        /// <summary>
        /// 判断是否创建快速访问Emit.
        /// </summary>
        /// <param name="createAC"></param>
        public PropertyValidates(bool createAC) {
            _CreateAC = createAC;
            if (_CreateAC)
                _QuickDataAccess = new Dictionary<string, Util.Emit.DynamicPropertyAccessor>();
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> QuickDataAccess {
            get {
                return _QuickDataAccess;
            }
        }

        #region Add...
        /// <summary>
        /// 创建一个新ColumnProperty 信息.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnDescription"></param>
        /// <param name="length"></param>
        /// <param name="allowNull"></param>
        /// <param name="isKey"></param>
        /// <returns></returns>
        public PropertyValidatedInfo Add(Expression<Func<T, object>> columnName, string description, int maxLength, bool isNull) {
            PropertyInfo colInfo = null;
            var colProInfo = createInfo(columnName, out colInfo);

            colProInfo.Description = description;
            colProInfo.DataType = colInfo.PropertyType;
            colProInfo.MaxLength = maxLength;
            colProInfo.IsNull = isNull;


            return colProInfo;
        }

        /// <summary>
        /// 创建一个新ColumnProperty 信息.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="description"></param>
        /// <param name="maxValue"></param>
        /// <param name="maxDecimalPlaces"></param>
        /// <returns></returns>
        public PropertyValidatedInfo Add(Expression<Func<T, object>> columnName, string description, double maxValue, double minValue, int maxDecimalPlaces) {
            PropertyInfo colInfo = null;
            var colProInfo = createInfo(columnName, out colInfo);

            colProInfo.Description = description;
            colProInfo.DataType = colInfo.PropertyType;
            colProInfo.MaxValue = maxValue;
            colProInfo.MinValue = minValue;
            colProInfo.MaxDecimalPlaces = maxDecimalPlaces;

            return colProInfo;
        }

        #endregion

        #region Get...
        /// <summary>
        /// 根据名称获取注册的信息。
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public PropertyValidatedInfo Get(Expression<Func<T, object>> columnName) {
            string colName = MB.Util.Expressions.ExpressionHelper.GetPropertyName<T>(columnName);
            if (string.IsNullOrEmpty(colName))
                throw new MB.Util.APPException(string.Format("根据表达式{0} 获取不到属性的名称,请检查", columnName), Util.APPMessageType.SysErrInfo);

            return this.ContainsKey(colName) ? this[colName] : null;
        }
        #endregion


        #region 内部函数...
        //创建一个验证信息。
        private PropertyValidatedInfo createInfo(Expression<Func<T, object>> columnName, out PropertyInfo colInfo) {
            var colName = MB.Util.Expressions.ExpressionHelper.GetPropertyName<T>(columnName);
            if (string.IsNullOrEmpty(colName))
                throw new MB.Util.APPException(string.Format("根据表达式{0} 获取不到属性的名称,请检查", columnName), Util.APPMessageType.SysErrInfo);

            if (this.ContainsKey(colName))
                throw new MB.Util.APPException(string.Format("当前集合中已经包含属性{0}", colName), Util.APPMessageType.SysErrInfo);

            var temp = typeof(T).GetProperty(colName);
            if (temp == null)
                throw new MB.Util.APPException(string.Format("根据表达式{0} 获取不到属性的名称,请检查是否为public property", columnName), Util.APPMessageType.SysErrInfo);
            colInfo = temp;
            var newInfo = new PropertyValidatedInfo() { Name = colName };
            this.Add(colInfo.Name, newInfo);

            if (_CreateAC) {
                Util.Emit.DynamicPropertyAccessor ac = new Util.Emit.DynamicPropertyAccessor(typeof(T), colInfo);
                _QuickDataAccess[colInfo.Name] = ac;
            }
            return newInfo;
        }
        #endregion
    }

    /// <summary>
    /// 列的描述信息集合.
    /// </summary>
    public class PropertyValidates : System.Collections.Generic.Dictionary<string, PropertyValidatedInfo>
    {
    }
    #endregion PropertyValidates...

    #region PropertyValidatedInfo...
    /// <summary>
    /// 对象属性字段验证信息。
    /// </summary>
    public class PropertyValidatedInfo
    {
        private string _Name; //字段的名称
        private string _Description; //字段的描述信息
        private Type _DataType; //字段的类型
        private int _MaxLength = -1; //字段的长度
        private bool _IsNull = true;//该字段是否可以为空
        private double _MinValue = double.MinValue; //最大值
        private double _MaxValue = double.MaxValue; //最小值
        private int _MaxDecimalPlaces = -1; //小数点的最大位数
        private int _FitLength;

        #region 构造函数...

        public PropertyValidatedInfo() { }
        public PropertyValidatedInfo(string name, string description, Type dataType, int maxLength, bool isNull) {
            _Name = name;
            _Description = description;
            _DataType = dataType;
            _MaxLength = maxLength;
            _IsNull = isNull;
        }
        #endregion

        #region public 属性...

        /// <summary>
        /// 
        /// </summary>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Type DataType {
            get {
                return _DataType;
            }
            set {
                _DataType = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MaxLength {
            get {
                return _MaxLength;
            }
            set {
                _MaxLength = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsNull {
            get {
                return _IsNull;
            }
            set {
                _IsNull = value;
            }
        }
        public double MinValue {
            get {
                return _MinValue;
            }
            set {
                _MinValue = value;
            }
        }
        public double MaxValue {
            get {
                return _MaxValue;
            }
            set {
                _MaxValue = value;
            }
        }
        public int MaxDecimalPlaces {
            get {
                return _MaxDecimalPlaces;
            }
            set {
                _MaxDecimalPlaces = value;
            }
        }
        public int FitLength {
            get {
                return _FitLength;
            }
            set {
                _FitLength = value;
            }
        }
        #endregion
    }

    #endregion
}
