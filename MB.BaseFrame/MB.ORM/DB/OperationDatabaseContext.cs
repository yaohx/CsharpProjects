using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.DB
{
    /// <summary>
    /// 操作数据库配置信息,目前只以读写分离的实现为主。
    /// </summary>
    public class OperationDatabaseContext
    {
        private string _AppCode;
        private string _DbName;
        private bool _Readonly;
        private bool _CreateFromLocalAppConfiguration;

        private DynamicDatabaseSettingInfo _DatabaseSettingInfo;
        [ThreadStatic]
        private static OperationDatabaseContext _Current;

        #region constructer...
        /// <summary>
        /// constructer...
        /// </summary>
        public OperationDatabaseContext() {

        }
        /// <summary>
        /// constructer...
        /// </summary>
        /// <param name="readOnly"></param>
        public OperationDatabaseContext(bool readOnly) {
            _Readonly = readOnly;
            if (_Current != null)
                _DbName = _Current.DbName;
        }
        /// <summary>
        /// constructer...
        /// </summary>
        /// <param name="dbName"></param>
        public OperationDatabaseContext(string dbName) {
            _DbName = dbName;
            _CreateFromLocalAppConfiguration = true;
        }
        #endregion

        /// <summary>
        /// 当前的数据库配置信息.
        /// </summary>
        public static OperationDatabaseContext Current {
            get {
                return _Current;
            }
            set {
                _Current = value;
            }
        }
        public DynamicDatabaseSettingInfo DatabaseSettingInfo {
            get {
                return _DatabaseSettingInfo;
            }
            set {
                _DatabaseSettingInfo = value;
            }
        }
        public string DbName {
            get {
                return _DbName;
            }
        }
        public string AppCode {
            get {
                return _AppCode;
            }
            set {
                _AppCode = value;
            }
        }
        public bool ReadOnly {
            get {
                return _Readonly;
            }
        }
        public bool CreateFromLocalAppConfiguration {
            get {
                return _CreateFromLocalAppConfiguration;
            }
        }
    }
}
