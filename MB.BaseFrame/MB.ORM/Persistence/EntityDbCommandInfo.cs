using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Persistence {
    /// <summary>
    /// 数据实体对象的DbCommand 描述信息。
    /// </summary>
    public class EntityDbCommandInfo {
        private object _DataEntity;
        private System.Data.Common.DbCommand _DbCommand;
        private bool _IsPartPropertyUpdate;
        private bool _IsMainEntity;
        private MB.Orm.Enums.OperationType _OperationType;
        /// <summary>
        /// 构造函数..
        /// </summary>
        /// <param name="dataEntity"></param>
        /// <param name="dbCommand"></param>
        public EntityDbCommandInfo(object dataEntity, System.Data.Common.DbCommand dbCommand)
            : this(dataEntity, dbCommand, false) {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="dataEntity"></param>
        /// <param name="_dbCommand"></param>
        /// <param name="isMainEntity"></param>
        public EntityDbCommandInfo(object dataEntity, System.Data.Common.DbCommand dbCommand, bool isMainEntity) {
            _DataEntity = dataEntity;
            _DbCommand = dbCommand;
            _IsMainEntity = isMainEntity;

            _OperationType = MB.Orm.Enums.OperationType.None; 
        }
        #region public 属性...
        /// <summary>
        /// 当前进行操作的数据实体。
        /// </summary>
        public object DataEntity {
            get {
                return _DataEntity;
            }
            set {
                _DataEntity = value;
            }
        }
        /// <summary>
        /// 数据库操作的DbCommand.
        /// </summary>
        public System.Data.Common.DbCommand DbCommand {
            get {
                return _DbCommand;
            }
            set {
                _DbCommand = value;
            }
        }
        /// <summary>
        /// 在批量处理中是否为主要单据描述对象。
        /// </summary>
        public bool IsMainEntity {
            get {
                return _IsMainEntity;
            }
            set {
                _IsMainEntity = value;
            }
        }
        /// <summary>
        /// 判断是否为部分属性更新。
        /// </summary>
        public bool IsPartPropertyUpdate {
            get {
                return _IsPartPropertyUpdate;
            }
            set {
                _IsPartPropertyUpdate = value;
            }
        }
        /// <summary>
        /// 当前命令的操作方式。
        /// </summary>
        public MB.Orm.Enums.OperationType OperationType {
            get {
                return _OperationType;
            }
            set {
                _OperationType = value;
            }
        }
        #endregion public 属性...
    }
}
