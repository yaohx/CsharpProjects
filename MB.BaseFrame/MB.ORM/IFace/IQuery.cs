using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.IFace {
    /// <summary>
    /// 
    /// </summary>
    public interface IQuery {
        Type EntityType { get; set; }
        string EntityTypeName { get; set; }
        string Filter { get; set; }

       // QueryParameterCollection Parameters { get; set; }
        string Ordering { get; set; }

        bool IgnoreCache { get; set; }

        ICollection QueryObjects();
        DataSet QueryDataSet();

        IPersistenceManager PersistenceManager { get; }

        bool IsClosed { get; }
        void Close();
        void Open();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<T> : IQuery {
        new ICollection<T> QueryObjects();
    }
}
