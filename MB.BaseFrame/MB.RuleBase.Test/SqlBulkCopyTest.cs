using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.Orm.DB;
using MB.RuleBase.BulkCopy;
using MB.Util;

namespace MB.RuleBase.Test {
    [TestClass]
    public class SqlBulkCopyTest {
        [TestMethod]
        public void TestSQLBuikInsert() {

            List<TmpTimeSplitInfo> tmpTimeSplitInfos = new List<TmpTimeSplitInfo>();
            List<MyTable> myTables = new List<MyTable>();
            for (int i = 0; i < 3; i++) {
                TmpTimeSplitInfo time = new TmpTimeSplitInfo();
                time.USER_UID = i.ToString();
                time.USER_CODE = i.ToString();
                tmpTimeSplitInfos.Add(time);

                MyTable my = new MyTable();
                my.ID = i;
                my.Name = "Name" + i.ToString();
                myTables.Add(my);
            }
            //MB.OS  ; MB.BPM.KQ
            using (MB.Orm.DB.OperationDatabaseScope scope = new OperationDatabaseScope("MB.BPM.KQ")) {
                try {
                    tmpTimeSplitInfos.ForEach(p =>  {
                        p.LOAD_TIME = DateTime.Now;
                        p.APP_DATE = DateTime.Now;
                    });
                    //using (TransactionScope trans = new TransactionScope()) {
                    //    using (IDbBulkExecute bulk = DbBulkExecuteFactory.CreateDbBulkExecute()) {
                    //        //MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQueryByEntity("TmpTimeSplit",
                    //        //                                                                             "AddObject",
                    //        //                                                                             tmpTimeSplitInfo[0]);
                    //        bulk.WriteToServer("TmpTimeSplit", "AddObject", tmpTimeSplitInfo);
                    //    }
                    //    trans.Complete();
                    //}

                    //using (IDbBulkExecute bulk = DbBulkExecuteFactory.CreateDbBulkExecute()) {
                    //    bulk.WriteToServer("TmpTimeSplit", "AddObject", tmpTimeSplitInfos);
                    //}
                    using (IDbBulkExecute bulk = DbBulkExecuteFactory.CreateDbBulkExecute()) {
                        bulk.WriteToServer("MyTable", "AddObject", tmpTimeSplitInfos);
                    }

                }
                catch (Exception ex) {
                    throw new APPException(ex.Message, APPMessageType.SysErrInfo);
                }

            }
        }
    }

    public class MyTable : MB.Orm.Common.BaseModel {
        private int _ID;

        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;

        [MB.Orm.Mapping.Att.ColumnMap("Name", System.Data.DbType.String)]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
    }

    public class TmpTimeSplitInfo : MB.Orm.Common.BaseModel {

        public TmpTimeSplitInfo() {

        }


        private string _USER_UID;

        [MB.Orm.Mapping.Att.ColumnMap("USER_UID", System.Data.DbType.String)]
        public string USER_UID {
            get { return _USER_UID; }
            set { _USER_UID = value; }
        }
        private string _USER_CODE;

        [MB.Orm.Mapping.Att.ColumnMap("USER_CODE", System.Data.DbType.String)]
        public string USER_CODE {
            get { return _USER_CODE; }
            set { _USER_CODE = value; }
        }
        private Decimal _APP_CATEGORY;

        [MB.Orm.Mapping.Att.ColumnMap("APP_CATEGORY", System.Data.DbType.Decimal)]
        public Decimal APP_CATEGORY {
            get { return _APP_CATEGORY; }
            set { _APP_CATEGORY = value; }
        }
        private string _APP_CODE;

        [MB.Orm.Mapping.Att.ColumnMap("APP_CODE", System.Data.DbType.String)]
        public string APP_CODE {
            get { return _APP_CODE; }
            set { _APP_CODE = value; }
        }
        private DateTime _APP_DATE;

        [MB.Orm.Mapping.Att.ColumnMap("APP_DATE", System.Data.DbType.DateTime)]
        public DateTime APP_DATE {
            get { return _APP_DATE; }
            set { _APP_DATE = value; }
        }
        private decimal _LEAVE_TIME;

        [MB.Orm.Mapping.Att.ColumnMap("LEAVE_TIME", System.Data.DbType.Decimal)]
        public decimal LEAVE_TIME {
            get { return _LEAVE_TIME; }
            set { _LEAVE_TIME = value; }
        }
        private string _UNIT;

        [MB.Orm.Mapping.Att.ColumnMap("UNIT", System.Data.DbType.String)]
        public string UNIT {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        private string _OA_RECORD_ID;

        [MB.Orm.Mapping.Att.ColumnMap("OA_RECORD_ID", System.Data.DbType.String)]
        public string OA_RECORD_ID {
            get { return _OA_RECORD_ID; }
            set { _OA_RECORD_ID = value; }
        }
        private string _REMARKS;

        [MB.Orm.Mapping.Att.ColumnMap("REMARKS", System.Data.DbType.String)]
        public string REMARKS {
            get { return _REMARKS; }
            set { _REMARKS = value; }
        }
        private DateTime _LOAD_TIME;

        [MB.Orm.Mapping.Att.ColumnMap("LOAD_TIME", System.Data.DbType.DateTime)]
        public DateTime LOAD_TIME {
            get { return _LOAD_TIME; }
            set { _LOAD_TIME = value; }
        }
        private Decimal _FLAG;

        [MB.Orm.Mapping.Att.ColumnMap("FLAG", System.Data.DbType.Decimal)]
        public Decimal FLAG {
            get { return _FLAG; }
            set { _FLAG = value; }
        }
    } 


}
