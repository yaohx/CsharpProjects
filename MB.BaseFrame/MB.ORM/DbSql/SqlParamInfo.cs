using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace MB.Orm.DbSql {
    /// <summary>
    /// 执行SQL语句需要的参数。
    /// </summary>
    [Serializable]
    public class SqlParamInfo : ICloneable  {
        private string _Name;
        private string _MappingName;
        private string _Description;
        private System.Data.ParameterDirection _Direction;
        private System.Data.DbType _DbType;
        private int _Length;
        private bool _Overcast;
        private object _Value;

        #region 构造函数...
        /// <summary>
        /// 
        /// </summary>
        public SqlParamInfo() {
        }

        public SqlParamInfo(string name)
            : this(name, System.Data.DbType.String) {
        }
        public SqlParamInfo(string name, System.Data.DbType  dbType) {
            _Name = name;
            _DbType = dbType;
            _Direction = ParameterDirection.Input;
        }
        public SqlParamInfo(string name, System.Data.DbType dbType, int length, System.Data.ParameterDirection direction) {
            _Name = name;
            _DbType = dbType;
            _Length = length;
            _Direction = direction;
        }
        public SqlParamInfo(string name,object value, System.Data.DbType dbType) {
            _Name = name;
            _DbType = dbType;
            _Value = value;
            _Direction = ParameterDirection.Input;
        }

        #endregion 构造函数...

        /// <summary>
        /// ToString。
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return "Name:" + _Name + "; Description:" + _Description + "; Direction:" + _Direction.ToString() +
                    "; TypeName:" + _DbType + "; Length:" + _Length.ToString() + " ;Overcast:" + _Overcast.ToString();
        }

        #region public Property...
        /// <summary>
        /// 参数名称
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
        /// 映射到对象的属性名称。
        /// </summary>
        public string MappingName {
            get {
                if (string.IsNullOrEmpty(_MappingName)) {
                    if (_Name.IndexOf(MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX) == 0) {
                        _MappingName = _Name.Substring(1, _Name.Length - 1);
                    }
                    else {
                        _MappingName = _Name;
                    }
                }
                 return _MappingName;
            }
            set {
                _MappingName = value;
            }
        }
        /// <summary>
        /// 参数描述
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
        /// 输入、输出方向
        /// </summary>
        public System.Data.ParameterDirection Direction {
            get {
                return _Direction;
            }
            set {
                _Direction = value;
            }
        }
        /// <summary>
        /// 参数类型
        /// </summary>
        public DbType DbType {
            get {
                return _DbType;
            }
            set {
                _DbType = value;
            }
        }
        /// <summary>
        /// 参数长度。
        /// </summary>
        public int Length {
            get {
                return _Length;
            }
            set {
                _Length = value;
            }
        }
        /// <summary>
        /// 当前的参数值。
        /// </summary>
        public object Value {
            get {
                return _Value;
            }
            set {
                _Value = value;
            }
        }
        /// <summary>
        /// 判断参数的值是否在拼接SQL 时直接被覆盖掉。
        /// </summary>
        public bool Overcast {
            get {
                return _Overcast;
            }
            set {
                _Overcast = value;
            }
        }
        #endregion public Property...

        #region ICloneable 成员

        public SqlParamInfo Clone() {
            return base.MemberwiseClone() as SqlParamInfo;
        }

        #endregion

        #region ICloneable 成员

        object ICloneable.Clone() {
            return base.MemberwiseClone();
        }

        #endregion
    }
}
