//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-02
// Description	:	 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.DbSql;

namespace MB.Orm.DbSql.SmartBuilder
{
    /// <summary>
    /// BuilderData
    /// </summary>
    internal class BuilderData
    {
        private Type _EntityType;
        private MB.Orm.Mapping.ModelMappingInfo _EntityMapping;

        public MB.Orm.Mapping.ModelMappingInfo EntityMapping {
            get {
                return _EntityMapping;
            }
        }
        public List<SmartTableColumnInfo> Columns {
            get;
            set;
        }
        public List<SqlParamInfo> Parameters {
            get;
            set;
        }
        public object Item {
            get;
            set;
        }
        public List<SmartTableColumnInfo> Where {
            get;
            set;
        }

        public BuilderData(Type entityType) {
            _EntityType = entityType;
            _EntityMapping = MB.Orm.Mapping.AttMappingManager.Instance.GetModelMappingInfo(entityType);

            Parameters = new List<SqlParamInfo>();
            Columns = new List<SmartTableColumnInfo>();
            Where = new List<SmartTableColumnInfo>();
        }
    }
}
