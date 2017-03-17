using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.WinBase.Common;

namespace MB.Util.Test
{
    [TestClass]
    public class XmlUntilTest
    {
        private static readonly string COLUMN_CONFIG_NODE = "/Entity/Columns/Column";

        private string _xmlFileName;


        [TestInitialize]
        public void Initialize()
        {
            _xmlFileName = "UI_BfUser.xml";
        }

        [TestMethod]
        public void XmlConfigHelper_CreateEntityList()
        {
            string COLUMN_CONFIG_NODE = "/Entity/Columns/Column";
            string xmlFileName = "UI_BfUser.xml";
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + xmlFileName;

            var cols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<ColumnPropertyInfo>("Name", fullPath, COLUMN_CONFIG_NODE);
        }
    }
}
