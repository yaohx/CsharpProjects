using MB.WinBase.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Schema;
using System.Xml;

namespace MB.WinBase.Test.Common
{
    /// <summary>
    ///This is a test class for FilterElementCfgsTest and is intended
    ///to contain all FilterElementCfgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FilterElementCfgsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetSchema
        ///</summary>
        [TestMethod()]
        public void GetSchemaTest()
        {
            XmlSchemaSet xs = new XmlSchemaSet();
            FilterElementCfgs.GetSchema(xs);         
        }
    }
}
