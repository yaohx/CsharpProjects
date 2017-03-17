using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.IFace {
    public interface Transaction {
        void Begin();
        void Commit();
        void Rollback();
        IPersistenceManager PersistenceManager { get; }
    }
}
