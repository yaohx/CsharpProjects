using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping.Xml {
    /// <summary>
    /// XmlSqlMappingInfo 单据包含的业务对象配置的。
    /// </summary>
    public class XmlSqlMappingInfo {

        #region public const field...
        public const string SQL_SELECT_BY_KEY = "SelectByKey";
        public const string SQL_SELECT_OBJECT = "SelectObject";
        public const string SQL_ADD_OBJECT = "AddObject";
        public const string SQL_UPDATE_OBJECT = "UpdateObject";
        public const string SQL_DELETE_OBJECT = "DeleteObject";
        public const string SQL_DELETE_NOT_IN_IDS = "DeleteNotInIDS";
        public const string SQL_GET_BY_FOREING_KEY = "GetByForeingKey";
        public const string SQL_INI_GET_BY_LINK_OBJECT = "IniGetByLinkObjectKey";
        #endregion public const field...

        private string _ObjectName;//对象的名称（）
        private string _SelectObject;
        private string _AddObject;
        private string _DeleteObject;
        private string _UpdateObject;
        private string _DeleteNotInIDS;
        private string _GetByForeingKey;
        private string _IniGetByLinkObjectKey;

        private string _KeyName;

        #region 构造函数...
        /// <summary>
        /// 构造函数。
        /// </summary>
        public XmlSqlMappingInfo()
            : this(null) {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        public XmlSqlMappingInfo(string objectName) {
            _SelectObject = SQL_SELECT_OBJECT;
            _AddObject = SQL_ADD_OBJECT;
            _DeleteObject = SQL_DELETE_OBJECT;
            _UpdateObject = SQL_UPDATE_OBJECT;
            _GetByForeingKey = SQL_GET_BY_FOREING_KEY;
            _IniGetByLinkObjectKey = SQL_INI_GET_BY_LINK_OBJECT;

            _KeyName = "ID";
            _ObjectName = objectName;
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="selectName"></param>
        /// <param name="addName"></param>
        /// <param name="updateName"></param>
        /// <param name="deleteName"></param>
        public XmlSqlMappingInfo(string objectName, string selectName, string addName, string updateName, string deleteName, string keyName) {
            _SelectObject = selectName;
            _AddObject = addName;
            _UpdateObject = updateName;
            _DeleteObject = deleteName;
            _KeyName = keyName;
            _ObjectName = objectName;
        }
        #endregion 构造函数...

        #region public 属性...
        /// <summary>
        /// mapping 对象的名称，可以的XML 文件名称或者是表的名称
        /// </summary>
        public string ObjectName {
            get {
                return _ObjectName;
            }
            set {
                _ObjectName = value;
            }
        }
        /// <summary>
        /// 获取对象数据 XML 配置语句名称。
        /// </summary>
        public string SelectObject {
            get {
                return _SelectObject;
            }
            set {
                _SelectObject = value;
            }
        }
        /// <summary>
        /// 增加数据的XML 配置语句名称。
        /// </summary>
        public string AddObject {
            get {
                return _AddObject;
            }
            set {
                _AddObject = value;
            }
        }
        /// <summary>
        /// 修改数据的XML 配置语句名称。
        /// </summary>
        public string UpdateObject {
            get {
                return _UpdateObject;
            }
            set {
                _UpdateObject = value;
            }
        }
        /// <summary>
        /// 删除数据的XML 配置语句名称。
        /// </summary>
        public string DeleteObject {
            get {
                return _DeleteObject;
            }
            set {
                _DeleteObject = value;
            }
        }
        /// <summary>
        /// 对象键值列的名称。
        /// </summary>
        public string KeyName {
            get {
                return _KeyName;
            }
            set {
                _KeyName = value;
            }
        }
        /// <summary>
        /// 通过not in ids 的方式删除不在集合中的数据达到批量删除的效果。
        /// </summary>
        public string DeleteNotInIDS {
            get {
                return _DeleteNotInIDS;
            }
            set {
                _DeleteNotInIDS = value;
            }
        }
        /// <summary>
        /// 通过外键获取该对象的数据.
        /// </summary>
        public string GetByForeingKey {
            get {
                return _GetByForeingKey;
            }
            set {
                _GetByForeingKey = value;
            }
        }
        /// <summary>
        /// 通过连动数据的键值获取新增状态下对象的数据.
        /// </summary>
        public string IniGetByLinkObjectKey {
            get {
                return _IniGetByLinkObjectKey;
            }
            set {
                _IniGetByLinkObjectKey = value;
            }
        }
        #endregion public 属性...

    }
}
