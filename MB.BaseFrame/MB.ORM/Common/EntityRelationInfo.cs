using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Common
{
    /// <summary>
    /// 实体对象之间的配置信息。
    /// </summary>
    internal class EntityRelationInfo
    {
        public EntityRelationInfo(Type entity, EntityRelationMapInfo mapInfo) {
            EntityType = entity;
            RelationMap = mapInfo;
        }
        internal Type EntityType { get; private set; }
        internal EntityRelationMapInfo RelationMap { get; private set; }
    }

    /// <summary>
    /// 列名称之间的对应关系
    /// </summary>
    internal class EntityRelationMapInfo
    {
        public EntityRelationMapInfo() { }
        public EntityRelationMapInfo(string fName, string kName) {
            ForeingKeyName = fName;
            KeyName = kName;
        }
        internal string ForeingKeyName { get; private set; }
        internal string KeyName { get; private set; }

    }
}
