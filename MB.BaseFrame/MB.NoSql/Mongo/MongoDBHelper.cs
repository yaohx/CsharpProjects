using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MB.NoSql.Mongo
{
    /// <summary>
    /// 封装对MongoDB 的操作处理。
    /// </summary>
    public class MongoDBHelper<T> : IDisposable
    {
        private static string MONGO_DB_CFG_STRING = "MongoDbConnection";
        //
        private static string CONNECTION_STRING = string.Empty;
        //数据库名
        private string _DataBaseName = string.Empty;
        private string _ConnectionString = string.Empty;//"mongodb://192.168.149.230";
        private MongoServer _Server;
        private MongoCollection<T> _DataCollection;

        static MongoDBHelper() {
            CONNECTION_STRING = System.Configuration.ConfigurationManager.AppSettings[MONGO_DB_CFG_STRING];
            if (string.IsNullOrEmpty(CONNECTION_STRING))
                throw new MB.Util.APPException("请先配置Mongo数据库连接字符窜.例如:MongoDbConnection=IP:database", Util.APPMessageType.SysErrInfo);
        
        }
        #region 构造函数...
        /// <summary>
        /// 封装对MongoDB 的操作处理。
        /// </summary>
        public MongoDBHelper() 
            : this(false) {
        }
        /// <summary>
        /// 封装对MongoDB 的操作处理。
        /// </summary>
        public MongoDBHelper(bool createNewCollection) {
            var cfgs = CONNECTION_STRING.Split(':');
            if(cfgs.Length!=2)
                throw new MB.Util.APPException("请先配置Mongo数据库连接字符窜配置有误.例如:MongoDbConnection=IP:database", Util.APPMessageType.SysErrInfo);

            _ConnectionString = string.Format("mongodb://{0}",cfgs[0]);
            _DataBaseName = cfgs[1];

            _Server = MongoServer.Create(_ConnectionString);
            string collectionName = typeof(T).FullName;
            //获取databaseName对应的数据库，不存在则自动创建
            MongoDatabase mongoDatabase = _Server.GetDatabase(_DataBaseName) as MongoDatabase;

            if(createNewCollection)
                mongoDatabase.DropCollection(collectionName);

            if (mongoDatabase.CollectionExists(collectionName))
                mongoDatabase.CreateCollectionSettings<T>(collectionName);

            _DataCollection = mongoDatabase.GetCollection<T>(collectionName);
            //链接数据库
            _Server.Connect();
        }
        #endregion 构造函数...

        /// <summary>
        /// 通过ID 获取对应的实体对象。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetDataByID(int id) {
            var info = _DataCollection.FindOneById(new BsonInt32(id));
            return (T)info;
        }
        /// <summary>
        /// 保存实体对象。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Save(T data) {
            _DataCollection.Save<T>(data);
            return 1;
        }
        /// <summary>
        /// 清除集合中的所有数据。
        /// </summary>
        /// <returns></returns>
        public int Clear() {
            _DataCollection.RemoveAll();
            return 1;
        }
        #region IDisposable...
        private bool disposed = false;

       /// <summary>
        /// Dispose
       /// </summary>
        public void Dispose() {
            Dispose(true);
            //
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    try {
                        if(_Server!=null)
                            _Server.Disconnect();
                    }
                    catch { }
         
                }
                disposed = true;
            }
        }

        ~MongoDBHelper() {

          Dispose(false);
        }

        #endregion IDisposable...

    }
}
