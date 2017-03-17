using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.EAI.SOA.OERP.Entities
{
    public class DistroHeaderHelper
    {
        public string UnitID { get; set; }
        public string Doc_num { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("Src_Doc_Type", System.Data.DbType.String)]
        public string Src_Doc_Type { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("Src_Doc_Unit", System.Data.DbType.String)]
        public string Src_Doc_Unit { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("Src_Doc_Num", System.Data.DbType.String)]
        public string Src_Doc_Num { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("WIF_NUM", System.Data.DbType.String)]
        public string WIF_NUM { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("STORE_NBR", System.Data.DbType.String)]
        public string STORE_NBR { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("PO_NBR", System.Data.DbType.String)]
        public string PO_NBR { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("SHPMT_NBR", System.Data.DbType.String)]
        public string SHPMT_NBR { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("STAT_CODE", System.Data.DbType.String)]
        public string STAT_CODE { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("DISTRO_BREAK_ATTR", System.Data.DbType.String)]
        public string DISTRO_BREAK_ATTR { get; set; }
        [MB.Orm.Mapping.Att.ColumnMap("START_SHIP_DATE", System.Data.DbType.String)]
        public string START_SHIP_DATE { get; set; }

        public string IN_STORE_DATE { get; set; }
        public string DISTRO_TYPE { get; set; }
        public string IDT_NUM { get; set; }
    }
}
