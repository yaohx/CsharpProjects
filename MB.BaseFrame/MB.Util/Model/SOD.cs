using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.BaseFrame {
    /// <summary>
    /// 通用常量定义。
    /// </summary>
    public class SOD {
        /// <summary>
        /// 在方法调用中日记记录中参数的最大长度。
        /// </summary>
        public const int LOG_PARAMTER_VALUE_MAX_LENGTH = 50;
        /// <summary>
        /// 动态列分隔符
        /// </summary>
        public static readonly char DYNAMC_SEPARATOR_CHAR = '~';
        /// <summary>
        /// 允许为 空的值类型。
        /// </summary>
        public static readonly string NULLABLE_VALUE_TYPE = "System.Nullable`1[{0}]";
        /// <summary>
        /// 动态列左边的名称
        /// </summary>
        public static readonly string DYNAMIC_COL_LEFT_NAME = "~Active" +DYNAMC_SEPARATOR_CHAR;
        /// <summary>
        /// 动态列的左边名称。
        /// </summary>
        public static readonly string DYNAMIC_COLUMN_NAME = "~Active" + DYNAMC_SEPARATOR_CHAR + "{0}" + DYNAMC_SEPARATOR_CHAR + "{1}";
        /// <summary>
        /// 打开模块
        /// </summary>
        public static readonly string MODULE_COMMAND_OPEN = "Open";
        /// <summary>
        /// 模块对象新增加
        /// </summary>
        public static readonly string MODULE_COMMAND_ADD = "AddNew";
        /// <summary>
        /// 模块对象修改
        /// </summary>
        public static readonly string MODULE_COMMAND_EDIT = "Edit";
        /// <summary>
        /// 模块对象查询
        /// </summary>
        public static readonly string MODULE_COMMAND_QUERY = "Query";
        /// <summary>
        /// 模块对象删除
        /// </summary>
        public static readonly string MODULE_COMMAND_DELETE = "Delete";

        /// <summary>
        /// 模块对象保存
        /// </summary>
        public static readonly string MODULE_COMMAND_SAVE= "Save";
        /// <summary>
        /// 模块数据提交。
        /// </summary>
        public static readonly string MODULE_COMMAND_SUBMIT = "Submit";
        /// <summary>
        /// 模块数据取消提交。
        /// </summary>
        public static readonly string MODULE_COMMAND_CANCEL_SUBMIT = "CancelSubmit";
        /// <summary>
        /// 模块数据导入。
        /// </summary>
        public static readonly string MODULE_COMMAND_IMPORT = "DataImport";

        /// <summary>
        /// 当前用户ID      
        /// </summary>
        public static readonly string PARAM_CURRENT_USER_ID = "#CURRENT_USER_ID#";
        /// <summary>
        /// 当前系统时间。
        /// </summary>
        public static readonly string PARAM_DATETIME_NOW = "#SYSTEM_DATE_TIME#";
        /// <summary>
        /// 当前登录用户。
        /// </summary>
        public static readonly string CURRENT_USER_IDENTITY = "CurrentLoginUser";
        /// <summary>
        /// 获取当前用户查询的行为信息
        /// </summary>
        public static readonly string QUERY_BEHAVIOR_MESSAGE_HEADER = "QueryBehaviorMessage";
        /// <summary>
        /// 获取当前查询的总记录数
        /// </summary>
        public static readonly string QUERY_RESPONSE_INFO = "QueryResponseInfo";
        /// <summary>
        /// 性能检测消息头开关，表示客户端是否开启性能检测
        /// </summary>
        public static readonly string PERFORMANCE_MONITOR_SWITCH_MESSAGE_HEADER = "PerformanceMonitorSwitch";
        /// <summary>
        ///  中间层性能检测数据
        /// </summary>
        public static readonly string PERFORMANCE_MONITOR_RESPONSE_MESSAGE_HEADER = "PerformanceMonitorResponse";
        /// <summary>
        /// 消息头传递的默认namespace
        /// </summary>
        public static readonly string MESSAGE_HEADER_NAME_SPACE = "http://www.metersbonwe.com/";
        
        /// <summary>
        /// 对象键值。
        /// </summary>
        public static readonly string OBJECT_PROPERTY_ID = "ID";
        /// <summary>
        /// 实体类的最后修改时间。
        /// </summary>
        public static readonly string ENTITY_LAST_MODIFIED_DATE = "LAST_MODIFIED_DATE";
        /// <summary>
        /// 基于WCF 数据传输单个包的数据长度。
        /// </summary>
        public static readonly int L_SINGLE_PACK_MAX_LENGTH = 1024 * 8;
        /// <summary>
        /// 在日志记录中单行记录的最大字节数长度。
        /// 超过将不做日志记录处理。
        /// </summary>
        public static readonly int L_SINGLE_MAX_LOG_LENGTH = 1024 * 8;
        /// <summary>
        /// 以中国式为标准的非区域性时间格式
        /// 带时间格式
        /// System.Globalization.DateTimeFormatInfo.InvariantInfo
        /// 在数据库操作中使用 DATABASE_DATE_TIME_FORMATE
        /// </summary>
        public static readonly string DATE_TIME_FORMATE = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        ///  以中国式为标准的非区域性时间格式
        /// 不带时间的日期格式
        /// 在数据库操作中使用 DATABASE_WITHOUT_DATE_TIME_FORMATE
        /// </summary>
        public static readonly string DATE_WITHOUT_TIME_FORMATE = "yyyy-MM-dd";

        /// <summary>
        /// 数据库中具体到秒的时间格式，只能在SQL 中使用。
        /// 在代码中使用 DATE_TIME_FORMATE
        /// </summary>
        public static readonly string DATABASE_DATE_TIME_FORMATE = "YYYY-MM-DD HH24:MI:SS" ;
        /// <summary>
        /// 数据库操作中不带时间秒的数据格式
        /// 在代码中使用 DATE_WITHOUT_TIME_FORMATE 格式。
        /// </summary>
        public static readonly string DATABASE_WITHOUT_DATE_TIME_FORMATE = "YYYY-MM-DD";

        /// <summary>
        /// 表示该值以下的都是通用类型，超过或等于该值的为扩展类型。
        /// </summary>
        public const int OVER_DOC_STATE_LIMIT = 0x10000; //65536

        /// <summary>
        /// 登录用户管理人员编码。
        /// </summary>
        public static readonly string ADMINISTRATOR_USER_CODE = "SA";

        public const int DEFAULT_MAX_SHOT_COUNT = 20000;
    }
}
