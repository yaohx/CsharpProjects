using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace MB.Orm.Persistence
{
    /// <summary>
    ///  EntityCodeHelper： 统一处理单据号增加的解决方案。
    /// </summary>
    public class EntityCodeHelper
    {
        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static EntityCodeHelper NewInstance
        {
            get
            {
                return new EntityCodeHelper();
            }
        }

        /// <summary>
        /// 获取单据对象的单据号
        /// 在不传入单据分区编码的情况下，使用全局的分区编码*
        /// </summary>
        /// <param name="docTypeName">单据类型编码</param>
        /// <returns>单据号</returns>
        public string GetEntityCode(string docTypeName)
        {
            return GetEntityCode(docTypeName, "*");
        }


        /// <summary>
        /// 获取单据对象的单据号。
        /// 备注：业务系统需要提供存储过程的方式来实现以便性能上得到提高。
        /// 每次只能获取一个标识符号
        /// 在Oracle 中需要创建 FU_GET_NEXT_DOC_CODE SQL 函数
        /// </summary>
        /// <param name="docTypeName">单据类型编码</param>
        /// <param name="sectCode">单据标识</param>
        /// <returns>单据号</returns>
        public string GetEntityCode(string docTypeName, string sectCode)
        {
            string connName = MB.Util.AppConfigSetting.GetKeyValue("NewIDCoreDBConnStr");
            Database db;
            if (string.IsNullOrEmpty(connName))
                db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            else
                db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(connName);

            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);

            if (databaseType == MB.Orm.Enums.DatabaseType.MSSQLServer ||
                databaseType == Enums.DatabaseType.Sqlite || databaseType == Enums.DatabaseType.MySql)
            {
                throw new NotImplementedException("未实现基于非ORACLE的取单据号的方法");
            }
            //对于Oracle 需要通过Oracle 函数来进行实现。
            //为了减少对公共表的锁定时间, 这里每次都要创建一个新事务，完成了就释放。
            string qrySql = "SELECT FU_GET_NEXT_DOC_CODE(:p_DOC_TYPE_NAME,:pSectCode) FROM DUAL";
            DbCommand cmdSelect = db.GetSqlStringCommand(qrySql);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew))
            {
                try
                {
                    db.AddInParameter(cmdSelect, "p_DOC_TYPE_NAME", DbType.AnsiString, docTypeName);
                    db.AddInParameter(cmdSelect, "pSectCode", DbType.AnsiString, sectCode);
                    object val = db.ExecuteScalar(cmdSelect);

                    //如果有异常抛出 该方法不会执行，那么将自动执行回滚
                    scope.Complete();

                    return val.ToString();
                }
                catch (Exception ex)
                {
                    throw new MB.Util.APPException("执行 GetEntityCode 出错！", MB.Util.APPMessageType.SysDatabaseInfo, ex);
                }
                finally
                {
                    try
                    {
                        cmdSelect.Dispose();
                    }
                    catch { }
                }
            }
        }
    }
}
