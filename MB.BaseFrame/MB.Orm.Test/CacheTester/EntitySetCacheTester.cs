using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.Orm.EntitySetCache;
using System.Collections;

namespace MB.Orm.Test {
    [TestClass]
    [DeploymentItem(@"CacheTester\EntitySetCacheCfg.xml", @"ConfigFile\")]
    public class EntitySetCacheTester : ICacheLoader {
        [TestMethod]
        public void AddEntitySetCache() {
            CacheContainer.Current.LoadCache();

            List<User> users = CacheContainer.Current.GetObjects<User>();
            FilterParameter p1 = FilterParameter.CreateFilterParamater<User>("ID", 0);
            FilterParameter p2 = FilterParameter.CreateFilterParamater<User>("Name", "Name1", FilterCondition.Like);
            IList filterUsers = CacheContainer.Current.GetObjectsWithFilter<User>(p2);


            List<BfCodeInfo> codes = CacheContainer.Current.GetObjects<BfCodeInfo>();
            FilterParameter pc2 = FilterParameter.CreateFilterParamater<BfCodeInfo>("CODE", "2", FilterCondition.Like);
            IList filterCodes = CacheContainer.Current.GetObjectsWithFilter<BfCodeInfo>(pc2);

        }

        #region ICacheLoader Members

        public System.Collections.IList LoadCache() {
            List<User> users = new List<User>();
            for (int i = 0; i <= 100; i++) {
                User user = new User();
                user.ID = i;
                user.Name = "Name" + i.ToString();
                users.Add(user);
            }
            return users;
        }

        public System.Collections.IList LoadIncrementalCache() {
            throw new NotImplementedException();
        }

        #endregion
    }




    public class User : MB.Orm.Common.BaseModel {
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



    public class CodeInfoCacheLoader : ICacheLoader {

        public IList LoadCache() {
            List<BfCodeInfo> myCodes = new List<BfCodeInfo>();

            for (int i = 0; i < 100; i++) {
                BfCodeInfo myCode = new BfCodeInfo();
                myCode.ID = i;
                myCode.CODE = "MyCode" + i.ToString();
                myCode.DESCRIPTION = "MyCodeDesc" + i.ToString();
                myCodes.Add(myCode);
            }

            return myCodes;
        }

        
    }

}
