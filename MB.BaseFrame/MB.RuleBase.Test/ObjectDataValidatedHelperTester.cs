using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.RuleBase.Common;
using System.Runtime.Serialization;

namespace MB.RuleBase.Test
{

    //static class Program
    //{
    //    [STAThread]
    //    static void Main()
    //    {
    //        ObjectDataValidatedHelperTester test = new ObjectDataValidatedHelperTester();
    //        test.TestCheckValueIsExists();
    //        Console.ReadLine();
    //    }


    //}

    /// <summary>
    /// Summary description for ObjectDataValidatedHelperTester
    /// </summary>
    [TestClass]
    public class ObjectDataValidatedHelperTester
    {
        public ObjectDataValidatedHelperTester()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestCheckValueIsExists()
        {
            ObjectDataValidatedHelper validator = new ObjectDataValidatedHelper();
            UdOrderDisParamInfo ud = new UdOrderDisParamInfo();
            ud.ID = 16;
            ud.DOC_TYPE = "1";
            ud.UD_PARAM_GRP_CODE = "1234";
            ud.UD_SYSTEM = "NEW_ERP";
            ud.GOODS_PRO = null;
            validator.CheckValueIsExists(UdOrderDisParamIDT.BaseData, ud, new string[] { "ID", "DOC_TYPE", "UD_PARAM_GRP_CODE","UD_SYSTEM", "GOODS_PRO" });

            validator = new ObjectDataValidatedHelper();
            ud = new UdOrderDisParamInfo();
            ud.ID = 16;
            ud.DOC_TYPE = "1";
            ud.UD_PARAM_GRP_CODE = "1234";
            ud.UD_SYSTEM = "NEW_ERP";
            ud.GOODS_PRO = "";
            validator.CheckValueIsExists(UdOrderDisParamIDT.BaseData, ud, new string[] { "ID", "DOC_TYPE", "UD_PARAM_GRP_CODE", "UD_SYSTEM", "GOODS_PRO" });

            BfUserInfo user = new BfUserInfo();
            user.ID = 1;
            bool result = validator.CheckValueIsExists(BfUserIDT.BaseData, user, new string[] { "NAME" });

            Assert.AreEqual<bool>(true, result);

            user = new BfUserInfo();
            user.NAME = "Cloud";
            result = validator.CheckValueIsExists(BfUserIDT.BaseData, user, new string[] { "NAME", "IS_VALIDATE" });
            Assert.AreEqual<bool>(false, result);

            user = new BfUserInfo();
            user.IS_VALIDATE = true;
            result = validator.CheckValueIsExists(BfUserIDT.BaseData, user, new string[] { "NAME", "IS_VALIDATE" });
            Assert.AreEqual<bool>(false, result);
        }
    }


    public enum BfUserIDT
    {
        [MB.RuleBase.Atts.ObjectDataMapping("BfUser", EntityType = typeof(BfUserInfo), KeyIsSelfAdd = true, MappingTableName = "BF_USER")]
        BaseData = 0,

    }

    /// <summary> 
    /// 用户信息数据实体
    /// </summary> 
    [MB.Orm.Mapping.Att.ModelMap("BF_USER", "BfUser", new string[] { "ID" })]
    public class BfUserInfo
    {
        public BfUserInfo()
        {

        }
        private int _ID;
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _NAME;
        [MB.Orm.Mapping.Att.ColumnMap("NAME", System.Data.DbType.String)]
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        private string _CODE;
        [MB.Orm.Mapping.Att.ColumnMap("CODE", System.Data.DbType.String)]
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        private int _OWNER_ID;
        [MB.Orm.Mapping.Att.ColumnMap("OWNER_ID", System.Data.DbType.Int32)]
        public int OWNER_ID
        {
            get { return _OWNER_ID; }
            set { _OWNER_ID = value; }
        }
        private string _OWNER_CODE;
        [MB.Orm.Mapping.Att.ColumnMap("OWNER_CODE", System.Data.DbType.String)]
        public string OWNER_CODE
        {
            get { return _OWNER_CODE; }
            set { _OWNER_CODE = value; }
        }
        private string _OWNER_NAME;
        [MB.Orm.Mapping.Att.ColumnMap("OWNER_NAME", System.Data.DbType.String)]
        public string OWNER_NAME
        {
            get { return _OWNER_NAME; }
            set { _OWNER_NAME = value; }
        }
        private DateTime _EXPIRE_TIME;
        [MB.Orm.Mapping.Att.ColumnMap("EXPIRE_TIME", System.Data.DbType.DateTime)]
        public DateTime EXPIRE_TIME
        {
            get { return _EXPIRE_TIME; }
            set { _EXPIRE_TIME = value; }
        }
        private bool? _IS_VALIDATE;
        [MB.Orm.Mapping.Att.ColumnMap("IS_VALIDATE", System.Data.DbType.Boolean)]
        public bool? IS_VALIDATE
        {
            get { return _IS_VALIDATE; }
            set { _IS_VALIDATE = value; }
        }

        private bool _IS_SPECIAL;
        [MB.Orm.Mapping.Att.ColumnMap("IS_SPECIAL", System.Data.DbType.Boolean)]
        public bool IS_SPECIAL
        {
            get { return _IS_SPECIAL; }
            set { _IS_SPECIAL = value; }
        }

        private string _PASSWORD;
        [MB.Orm.Mapping.Att.ColumnMap("PASSWORD", System.Data.DbType.String)]
        public string PASSWORD
        {
            get { return _PASSWORD; }
            set { _PASSWORD = value; }
        }
        private string _LAST_MODIFIED_USER;
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_USER", System.Data.DbType.String)]
        public string LAST_MODIFIED_USER
        {
            get { return _LAST_MODIFIED_USER; }
            set { _LAST_MODIFIED_USER = value; }
        }
        private DateTime _LAST_MODIFIED_DATE;
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_DATE", System.Data.DbType.DateTime)]
        public DateTime LAST_MODIFIED_DATE
        {
            get { return _LAST_MODIFIED_DATE; }
            set { _LAST_MODIFIED_DATE = value; }
        }

        private Decimal? _Special_Column;
        [MB.Orm.Mapping.Att.ColumnMap("SpecialColumn", System.Data.DbType.Decimal)]
        public Decimal? SpecialColumn
        {
            get { return _Special_Column; }
            set { _Special_Column = value; }
        }


    }


    public enum UdOrderDisParamIDT
    {
        [MB.RuleBase.Atts.ObjectDataMapping("UdOrderDisParam", EntityType = typeof(UdOrderDisParamInfo), KeyIsSelfAdd = true, MappingTableName = "UD_ORDER_DIS_PARAM")]
        [EnumMember]
        BaseData = 0,
    }

    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2012-07-16 02:17 
    /// </summary> 
    [MB.Orm.Mapping.Att.ModelMap("UD_ORDER_DIS_PARAM", "UdOrderDisParam", new string[] { "ID" })]
    public class UdOrderDisParamInfo : MB.Orm.Common.BaseModel
    {

        public UdOrderDisParamInfo()
        {

        }
        private int _ID;
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _DOC_TYPE;
        [MB.Orm.Mapping.Att.ColumnMap("DOC_TYPE", System.Data.DbType.String)]
        public string DOC_TYPE
        {
            get { return _DOC_TYPE; }
            set { _DOC_TYPE = value; }
        }
        private bool _IS_FIRST_DIS;
        [MB.Orm.Mapping.Att.ColumnMap("IS_FIRST_DIS", System.Data.DbType.Boolean)]
        public bool IS_FIRST_DIS
        {
            get { return _IS_FIRST_DIS; }
            set { _IS_FIRST_DIS = value; }
        }
        private decimal _FST_SEND_FFRATE;
        [MB.Orm.Mapping.Att.ColumnMap("FST_SEND_FFRATE", System.Data.DbType.Decimal)]
        public decimal FST_SEND_FFRATE
        {
            get { return _FST_SEND_FFRATE; }
            set { _FST_SEND_FFRATE = value; }
        }
        private bool _IS_MULTI_DIS;
        [MB.Orm.Mapping.Att.ColumnMap("IS_MULTI_DIS", System.Data.DbType.Boolean)]
        public bool IS_MULTI_DIS
        {
            get { return _IS_MULTI_DIS; }
            set { _IS_MULTI_DIS = value; }
        }
        private decimal _AUTO_CLOSE_FFRATE;
        [MB.Orm.Mapping.Att.ColumnMap("AUTO_CLOSE_FFRATE", System.Data.DbType.Decimal)]
        public decimal AUTO_CLOSE_FFRATE
        {
            get { return _AUTO_CLOSE_FFRATE; }
            set { _AUTO_CLOSE_FFRATE = value; }
        }
        private bool _IS_DIS_LOCK;
        [MB.Orm.Mapping.Att.ColumnMap("IS_DIS_LOCK", System.Data.DbType.Boolean)]
        public bool IS_DIS_LOCK
        {
            get { return _IS_DIS_LOCK; }
            set { _IS_DIS_LOCK = value; }
        }
        private decimal _REAUTO_CLOSE_FFRATE;
        [MB.Orm.Mapping.Att.ColumnMap("REAUTO_CLOSE_FFRATE", System.Data.DbType.Decimal)]
        public decimal REAUTO_CLOSE_FFRATE
        {
            get { return _REAUTO_CLOSE_FFRATE; }
            set { _REAUTO_CLOSE_FFRATE = value; }
        }
        private decimal _REAUTO_CNT;
        [MB.Orm.Mapping.Att.ColumnMap("REAUTO_CNT", System.Data.DbType.Decimal)]
        public decimal REAUTO_CNT
        {
            get { return _REAUTO_CNT; }
            set { _REAUTO_CNT = value; }
        }
        private string _UD_PARAM_GRP_CODE;
        [MB.Orm.Mapping.Att.ColumnMap("UD_PARAM_GRP_CODE", System.Data.DbType.String)]
        public string UD_PARAM_GRP_CODE
        {
            get { return _UD_PARAM_GRP_CODE; }
            set { _UD_PARAM_GRP_CODE = value; }
        }
        private decimal _WAREH_CRS_QTY;
        [MB.Orm.Mapping.Att.ColumnMap("WAREH_CRS_QTY", System.Data.DbType.Decimal)]
        public decimal WAREH_CRS_QTY
        {
            get { return _WAREH_CRS_QTY; }
            set { _WAREH_CRS_QTY = value; }
        }
        private decimal _FULL_ORDER_FFRATE;
        [MB.Orm.Mapping.Att.ColumnMap("FULL_ORDER_FFRATE", System.Data.DbType.Decimal)]
        public decimal FULL_ORDER_FFRATE
        {
            get { return _FULL_ORDER_FFRATE; }
            set { _FULL_ORDER_FFRATE = value; }
        }
        private decimal _FULL_COLOR_FFRATE;
        [MB.Orm.Mapping.Att.ColumnMap("FULL_COLOR_FFRATE", System.Data.DbType.Decimal)]
        public decimal FULL_COLOR_FFRATE
        {
            get { return _FULL_COLOR_FFRATE; }
            set { _FULL_COLOR_FFRATE = value; }
        }
        private decimal _FULL_SPEC_FFRATE;
        [MB.Orm.Mapping.Att.ColumnMap("FULL_SPEC_FFRATE", System.Data.DbType.Decimal)]
        public decimal FULL_SPEC_FFRATE
        {
            get { return _FULL_SPEC_FFRATE; }
            set { _FULL_SPEC_FFRATE = value; }
        }
        private decimal _RESERVERD_DAYS;
        [MB.Orm.Mapping.Att.ColumnMap("RESERVERD_DAYS", System.Data.DbType.Decimal)]
        public decimal RESERVERD_DAYS
        {
            get { return _RESERVERD_DAYS; }
            set { _RESERVERD_DAYS = value; }
        }
        private string _UD_SYSTEM;
        [MB.Orm.Mapping.Att.ColumnMap("UD_SYSTEM", System.Data.DbType.String)]
        public string UD_SYSTEM
        {
            get { return _UD_SYSTEM; }
            set { _UD_SYSTEM = value; }
        }
        private string _GOODS_PRO;
        [MB.Orm.Mapping.Att.ColumnMap("GOODS_PRO", System.Data.DbType.String)]
        public string GOODS_PRO
        {
            get { return _GOODS_PRO; }
            set { _GOODS_PRO = value; }
        }
        private bool _IS_DIS_NOT_ONSHELVES;
        [MB.Orm.Mapping.Att.ColumnMap("IS_DIS_NOT_ONSHELVES", System.Data.DbType.Boolean)]
        public bool IS_DIS_NOT_ONSHELVES
        {
            get { return _IS_DIS_NOT_ONSHELVES; }
            set { _IS_DIS_NOT_ONSHELVES = value; }
        }
        private bool _IS_DIS_CHECK;
        [MB.Orm.Mapping.Att.ColumnMap("IS_DIS_CHECK", System.Data.DbType.Boolean)]
        public bool IS_DIS_CHECK
        {
            get { return _IS_DIS_CHECK; }
            set { _IS_DIS_CHECK = value; }
        }
        private bool _IS_AUTO_DIS;
        [MB.Orm.Mapping.Att.ColumnMap("IS_AUTO_DIS", System.Data.DbType.Boolean)]
        public bool IS_AUTO_DIS
        {
            get { return _IS_AUTO_DIS; }
            set { _IS_AUTO_DIS = value; }
        }
    } 


}
