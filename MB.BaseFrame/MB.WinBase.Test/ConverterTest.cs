using MB.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MB.WinBase.Test
{
    
    
    /// <summary>
    ///This is a test class for ConverterTest and is intended
    ///to contain all ConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConverterTest
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
        ///A test for Converter Constructor
        ///</summary>
        [TestMethod()]
        public void ConverterConstructorTest()
        {
            Converter target = new Converter();
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }


        [TestMethod()]
        public void DeserializeTest()
        {
            try
            {
                MyTest myTest = new MyTest();

                myTest.ID = 1;
                myTest.dtDate = DateTime.Now;
                myTest.Remark = "Json数据解析，回车换行符支持测试\r\n";

                string path = @"C:\\Users\\Werish\\Desktop\\Project Key.txt";

                myTest.bPhoto = File.ReadAllBytes(path);

                string jsonData = Converter.Serialize(myTest);

                MessageBoxEx.Show(jsonData);
            }
            catch (Exception ex)
            {
                Assert.Inconclusive("Verify the correctness of this test method." + ex.Message);
            }
        }

    }
}
