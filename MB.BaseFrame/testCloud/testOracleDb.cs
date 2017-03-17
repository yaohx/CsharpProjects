using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testCloud.Model;

namespace testCloud
{
    public class testOracleDb
    {
        public static void testGetObjects()
        {
            var datas = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<int>("BfProductInfo", "GetProductsIds");
            Console.WriteLine(string.Format("成功获取到{0}数据", datas.Count));
            Console.ReadLine();
        }
    }
}
