using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MB.Orm.DB
{
    /// <summary>
    /// 数据库创建工厂接口。
    /// </summary>
    public interface IDatabaseFactory
    {
        /// <summary>
        /// 默认从动态路由表中创建一个连接写库的数据库连接字符窜
        /// </summary>
        /// <returns></returns>
        Database CreateDatabase();
        /// <summary>
        /// 从云计算平台动态路由表中获取
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Database CreateDatabase(bool readOnly);
        /// <summary>
        /// 根据应用编码、数据库名称、是否特定设置的读写需求来创建
        /// </summary>
        /// <param name="appCode"></param>
        /// <param name="dbName"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Database CreateDatabase(string appCode, string dbName, bool readOnly);
        /// <summary>
        /// 指定Db配置名称，目前只能从本地App.Config 的 配置中找对应的名称
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        Database CreateDatabase(string dbName);
        /// <summary>
        /// 根据上下文来创建所需要的数据库
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <returns></returns>
        Database CreateDatabase(OperationDatabaseContext databaseContext);
    }
}
