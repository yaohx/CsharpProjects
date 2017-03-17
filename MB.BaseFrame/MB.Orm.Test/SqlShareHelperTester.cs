using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.Util.Model;

namespace MB.Orm.Test {
    [TestClass]
    public class SqlShareHelperTester {
        [TestMethod]
        public void GetFilterSQL() {

            //QueryParameterInfo[]  parInfos = new MB.Util.Serializer.QueryParameterXmlSerializer().DeSerializer(xmlFilterParams);
            //string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(null, parInfos);


            QueryParameterInfo info1 = new QueryParameterInfo("Code", "111", Util.DataFilterConditions.Equal);
            QueryParameterInfo info2 = new QueryParameterInfo("Date", "222", Util.DataFilterConditions.Between);
            info2.Value2 = "333";
            QueryParameterInfo info3 = new QueryParameterInfo("Name", "4444", Util.DataFilterConditions.Like);
            QueryParameterInfo[] parInfos = new QueryParameterInfo[3];
            parInfos[0] = info1;
            parInfos[1] = info2;
            parInfos[2] = info3;


            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(null, parInfos);

            string xmlFilterParams = @"<FilterRoot AdvanceFilter='False'><Parameter><PropertyName>CREATE_DATE</PropertyName><Value><![CDATA[2007/5/3 0:00:00]]></Value><Value2><![CDATA[2012/5/17 0:00:00]]></Value2><Condition>Between</Condition><DataType>DateTime</DataType><OrderIndex>0</OrderIndex><Limited>False</Limited><MultiValue>False</MultiValue></Parameter><Parameter><PropertyName>LAST_MODIFIED_DATE</PropertyName><Value><![CDATA[2007/5/17 0:00:00]]></Value><Value2><![CDATA[2012/5/17 0:00:00]]></Value2><Condition>Between</Condition><DataType>DateTime</DataType><OrderIndex>0</OrderIndex><Limited>False</Limited><MultiValue>False</MultiValue></Parameter></FilterRoot>";            
            parInfos = new MB.Util.Serializer.QueryParameterXmlSerializer().DeSerializer(xmlFilterParams);

            sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(null, parInfos);


        }
    }
}
