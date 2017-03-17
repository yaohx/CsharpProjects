using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.DbSql {
    /// <summary>
    /// 数据库操作SQL结构。
    /// </summary>
    [Serializable]
    public class SqlString {
        private string _SqlString;
        private List<SqlParamInfo> _ParamFields;


        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="paramField"></param>
        public SqlString(string strSql, List<SqlParamInfo> paramField) {
            this._SqlString = strSql;

            this._ParamFields = paramField;

        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SqlString() { }

        /// <summary>
        /// 拼接出来的数据库操作字符窜。
        /// </summary>
        public string SqlStr {
            get { 
                return this._SqlString; 
            }
            set { 
                this._SqlString = value; 
            }

        }
        /// <summary>
        /// 该SQL语句的参数。
        /// </summary>
        public List<SqlParamInfo> ParamFields {

            get { 
                return this._ParamFields; 
            }

            set { 
                this._ParamFields = value; 
            }
        }

    }
}
