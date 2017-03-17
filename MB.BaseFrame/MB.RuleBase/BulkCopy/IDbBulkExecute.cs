using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace MB.RuleBase.BulkCopy {
    /// <summary>
    /// 数据库批量处理必须实现的接口。
    /// </summary>
    public interface IDbBulkExecute : IDisposable {
        event DbBulkExecuteEventHandle SqlRowsCopied;

        /// <summary>
        /// 每一批处理的行数。
        /// </summary>
        int BatchSize { get; set; }
        /// <summary>
        /// 超时。
        /// </summary>
        int BulkCopyTimeout { get; set; }
        /// <summary>
        /// 定义在生成事件之前处理的行数。
        /// </summary>
        int NotifyAfter { get; set; }

        /// <summary>
        /// 数据库处理事务，主要是支持非TransactionScope而增加。
        /// </summary>
        IDbTransaction DbTransaction { get;}
        
        /// <summary>
        /// 把集合类中的所有实体对象复制到指定的表中。
        /// 特殊要求：如果是实体类，要求实体类的属性必须和数据库表中的字段名称一致。
        /// </summary>
        /// <param name="lstData">实体集合类或者DataRow[]数组</param>
        void WriteToServer(string xmlFileName, string sqlName, IList lstData);       
    }
}
