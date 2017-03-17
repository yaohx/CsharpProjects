using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.Persistence;

namespace MB.Orm.DbSql.DynamicGroupBuilder
{
    public class DynamicGroupBuilderFactory
    {
        /// <summary>
        /// 创建动态聚组SQL生成对象
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static DynamicGroupBuilder CreateQueryBuilder(MB.Util.Model.DynamicGroupSetting setting)
        {
            var db = DatabaseHelper.CreateDatabase();
            MB.Orm.Enums.DatabaseType dbType = DatabaseHelper.GetDatabaseType(db);
            if (dbType == Enums.DatabaseType.Oracle)
            {
                return new DynamicGroupOracleBuilder(setting);
            }

            return null;
        }
    }
}
