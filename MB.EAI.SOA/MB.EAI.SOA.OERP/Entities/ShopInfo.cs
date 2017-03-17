using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.EAI.SOA.OERP.Entities
{
    [DataContract]
    [KnownType(typeof(ShopInfo))]
    public class ShopInfo : MB.Orm.Common.BaseModel
    {
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ShopID", System.Data.DbType.String)]
        public string ShopID { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Name", System.Data.DbType.String)]
        public string Name { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("OwnerID", System.Data.DbType.String)]
        public string OwnerID { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("OwnerName", System.Data.DbType.String)]
        public string OwnerName { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Hierarchy", System.Data.DbType.String)]
        public string Hierarchy { get; set; }        
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Province", System.Data.DbType.String)]
        public string Province { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("City", System.Data.DbType.String)]
        public string City { get; set; }
    }

}
