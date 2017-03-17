using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Common
{
    public struct DataRelationValue<TBaseModel>
    {
        public TBaseModel Parent { get; set; }

        public TBaseModel Child { get; set; }

    }
}
