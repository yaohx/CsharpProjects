using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Model;

namespace MB.Orm.DbSql.DynamicGroupBuilder {
    public abstract class DynamicGroupBuilder {
        /// <summary>
        /// 动态聚组客户端的设置
        /// </summary>
        protected Util.Model.DynamicGroupSetting _DynamicGroupSettings;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="setting"></param>
        public DynamicGroupBuilder(Util.Model.DynamicGroupSetting setting) {
            _DynamicGroupSettings = setting;
        }

        /// <summary>
        /// 需要继承的具体类型继承并且重写
        /// </summary>
        /// <param name="sqlFilter"></param>
        /// <returns></returns>
        public virtual string BuildDynamicQuery(string sqlFilter) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 得到查询的字段Select以后，from之前的查询字段
        /// </summary>
        /// <returns></returns>
        protected virtual string GetQueryFields() {
            Dictionary<string, List<DataAreaField>> datafields = _DynamicGroupSettings.DataAreaFields;
            string querySql = string.Empty;

            querySql = GetGroupFields();

            querySql += ",";

            foreach (var dataList in datafields) {
                foreach (MB.Util.Model.DataAreaField data in dataList.Value) {
                    querySql += this.GetQueryField(dataList.Key, data) + string.Format(" AS {0},", data.Name);
                }
            }
            querySql = querySql.TrimEnd(',');

            return querySql;
        }

        /// <summary>
        /// 得到分组的字段。用于SQL语句的GroupBy以后
        /// </summary>
        /// <returns></returns>
        protected virtual string GetGroupFields() {
            Dictionary<string, List<string>> groupFields = _DynamicGroupSettings.GroupFields;
            string groupSql = string.Empty;

            foreach (var groupList in groupFields) {
                foreach (string group in groupList.Value) {
                    groupSql += groupList.Key + "." + group + ",";
                }
            }
            groupSql = groupSql.TrimEnd(',');
            return groupSql;
        }


        private string GetGroupField(string entityName, string groupField) {
            string alias = this.GetFieldAlias(entityName);
            string fieldName = groupField;
            if (!string.IsNullOrEmpty(alias))
                fieldName = alias + "." + fieldName;
            else
                fieldName = entityName + "." + fieldName;
            return fieldName;
        }

        private string GetQueryField(string entityName, DataAreaField dataField) {
            string alias = this.GetFieldAlias(entityName);
            string fieldName = dataField.Name;
            if (!string.IsNullOrEmpty(alias))
                fieldName = alias + "." + fieldName;
            else
                fieldName = entityName + "." + fieldName;

            switch (dataField.SummaryItemType) {
                case SummaryItemType.Average:
                    return string.Format("AVG({0})", fieldName);
                case SummaryItemType.Count:
                    return string.Format("COUNT({0})", fieldName);
                case SummaryItemType.Max:
                    return string.Format("MAX({0})", fieldName);
                case SummaryItemType.Min:
                    return string.Format("MIN({0})", fieldName);
                case SummaryItemType.Sum:
                    return string.Format("SUM({0})", fieldName);
                case SummaryItemType.None:
                default:
                    return fieldName;

            }
        }


        private string GetFieldAlias(string entityName) {
            DynamicGroupEntityInfos entityInfos = _DynamicGroupSettings.EntityInfos;
            if (entityName.CompareTo(entityInfos.MainEntity.Name) == 0)
                return entityInfos.MainEntity.Alias;
            else if (entityName.CompareTo(entityInfos.DetailEntity.Name) == 0)
                return entityInfos.DetailEntity.Alias;
            else
                throw new Util.APPException("动态聚组查询表的别名出错GetFieldAlias");
        }

        /// <summary>
        /// 得到聚组FROM的语句
        /// </summary>
        /// <returns></returns>
        protected virtual string GetFromSql() {
            MB.Util.Model.DynamicGroupEntityInfos entityInfos = _DynamicGroupSettings.EntityInfos;
            List<MB.Util.Model.DynamicGroupRelationInfo> relationInfos = _DynamicGroupSettings.RelationInfos;

            var fromSqlBuilder = new StringBuilder();

            if (entityInfos.MainEntity == null)
                throw new MB.Util.APPException("动态聚组主实体没有配置");

            if (string.IsNullOrEmpty(entityInfos.MainEntity.Alias))
                fromSqlBuilder.Append(entityInfos.MainEntity.Name);
            else
                fromSqlBuilder.Append(string.Format(" {0} AS {1} ", entityInfos.MainEntity.Name, entityInfos.MainEntity.Alias));

            if (entityInfos.DetailEntity != null && relationInfos != null && relationInfos.Count > 0) {
                if (string.IsNullOrEmpty(entityInfos.DetailEntity.Alias))
                    fromSqlBuilder.Append(string.Format(" LEFT JOIN {0} ", entityInfos.DetailEntity.Name));
                else
                    fromSqlBuilder.Append(string.Format(" LEFT JOIN {0} AS {1} ", entityInfos.DetailEntity.Name, entityInfos.DetailEntity.Alias));

                foreach (MB.Util.Model.DynamicGroupRelationInfo relationInfo in relationInfos) {
                    string left = string.Empty;
                    string right = string.Empty;
                    if (string.IsNullOrEmpty(entityInfos.MainEntity.Alias))
                        left = entityInfos.MainEntity.Name + "." + relationInfo.Column;
                    else
                        left = entityInfos.MainEntity.Alias + "." + relationInfo.Column;

                    if (string.IsNullOrEmpty(entityInfos.DetailEntity.Alias))
                        right = entityInfos.DetailEntity.Name + "." + relationInfo.WithColumn;
                    else
                        right = entityInfos.DetailEntity.Alias + "." + relationInfo.WithColumn;

                    fromSqlBuilder.Append(string.Format(" ON {0} = {1} ", left, right));

                }
            }

            return fromSqlBuilder.ToString();
        }

        /// <summary>
        /// 得到聚合条件的SQL
        /// </summary>
        /// <returns></returns>
        protected virtual string GetAggConditionSql() {
            Dictionary<string, List<DataAreaField>> datafields = _DynamicGroupSettings.DataAreaFields;
            string querySql = string.Empty;

            foreach (var dataList in datafields) {
                foreach (MB.Util.Model.DataAreaField data in dataList.Value) {
                    if (data.ConditionOperator == DynamicGroupConditionOperator.None ||
                        string.IsNullOrEmpty(data.ConditionValue))
                        continue;

                    string aggField = this.GetQueryField(dataList.Key, data);
                    string aggCondition = string.Format("{0} {1} {2} ", aggField, ConvertToDbOperator(data.ConditionOperator), data.ConditionValue);
                    if (string.IsNullOrEmpty(querySql))
                        querySql += aggCondition;
                    else
                        querySql += string.Format("AND {0}", aggCondition);
                }
            }
            if (!string.IsNullOrEmpty(querySql))
                querySql = querySql.TrimEnd(',');

            return querySql;
        }

        /// <summary>
        /// 转换成数据库中的操作符号
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        protected virtual string ConvertToDbOperator(DynamicGroupConditionOperator op) {
            switch (op) {
                case DynamicGroupConditionOperator.Different:
                    return " <> ";
                case DynamicGroupConditionOperator.Equal :
                    return " = ";
                case DynamicGroupConditionOperator.GreaterThan:
                    return " > ";
                case DynamicGroupConditionOperator.GreaterThanOrEqual:
                    return " >= ";
                case DynamicGroupConditionOperator.LessThan:
                    return " < ";
                case DynamicGroupConditionOperator.LessThanOrEqual:
                    return " <= ";
                case DynamicGroupConditionOperator.None:
                default:
                    return string.Empty;
                
            }
        }
    }
}
