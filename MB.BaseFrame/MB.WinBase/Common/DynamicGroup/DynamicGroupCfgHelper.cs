using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase;
using System.Xml;
using MB.Util.Model;
using MB.WinBase.Common;
using System.Data;
using System.Reflection;
using MB.WinBase.Atts;

namespace MB.WinBase.Common.DynamicGroup {
    /// <summary>
    /// DynamicGroupCfgHelper 动态聚组网格分析配置信息
    /// </summary>
    public class DynamicGroupCfgHelper {

        //Pivot grid 在
        private static readonly string MAIN_COLUMN_NODE = "/Entity/DynamicGroup/MainEntity/Columns/Column";
        private static readonly string DETAIL_COLUMN_NODE = "/Entity/DynamicGroup/DetailEntity/Columns/Column";
        private static readonly string RELATIONS = "/Entity/DynamicGroup/Relations/Relation";
        private static readonly string MAIN_ENTITY_INFO = "/Entity/DynamicGroup/MainEntity";
        private static readonly string DETAIL_ENTITY_INFO = "/Entity/DynamicGroup/DetailEntity";
        private static readonly string DYNAMIC_ENTITY_INFO = "/Entity/DynamicGroup/DynamicEntity/Columns/Column";


        private string _XMLFile; //在构造函数中初始化
        private Dictionary<string, ColumnPropertyInfo> _UIColsSetting; //客户端UI配置的信息
        /// <summary>
        /// 构造函数
        /// </summary>
        public DynamicGroupCfgHelper(string xmlFile) {
            _XMLFile = xmlFile;
            _UIColsSetting = LayoutXmlConfigHelper.Instance.GetColumnPropertys(_XMLFile);
        }

        #region 呈现动态聚组结果的配置规则

        /// <summary>
        /// 动态聚组结果显示列的配置
        /// 1.先读取UI配置
        /// 2.再读取动态聚组的配置
        /// 3.合并配置，合并的优先级是动态聚组的配置优先, 动态聚组的配置的属性值优先
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ColumnPropertyInfo> GetResultColPropertys() {
            string xmlFileName = _XMLFile;
            Dictionary<string, ColumnPropertyInfo> uiColSettings = _UIColsSetting;
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            MB.Util.TraceEx.Write(string.Format("开始从 XML 文件 {0} 中获取列的信息", xmlFileName));

            var dyResultCols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<ColumnPropertyInfo>("Name", xmlFileFullName, DYNAMIC_ENTITY_INFO);

            #region 构造属性访问器
            Type typeObject = typeof(ColumnPropertyInfo);
            PropertyInfo[] infos = typeObject.GetProperties();
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (PropertyInfo info in infos) {
                object[] atts = info.GetCustomAttributes(typeof(XmlAttribute), true);
                if (atts == null) continue;

                dAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(typeObject, info));
            }
            #endregion

            int orderIndex = 0;
            foreach (ColumnPropertyInfo dyInfo in dyResultCols.Values) {
                if (uiColSettings.ContainsKey(dyInfo.Name)) {
                    ColumnPropertyInfo uiInfo = uiColSettings[dyInfo.Name];
                    foreach (MB.Util.Emit.DynamicPropertyAccessor propAccess in dAccs.Values) {
                        object uiProValue = propAccess.Get(uiInfo);
                        object dyProValue = propAccess.Get(dyInfo);
                        if (dyProValue == null && uiProValue != null)
                            propAccess.Set(dyInfo, uiProValue);
                    }
                    if (dyInfo.OrderIndex <= 0)
                        dyInfo.OrderIndex = orderIndex++;
                }
            }
            return dyResultCols;
        }

        #endregion


        /// <summary>
        /// 加载动态列的配置文件
        /// </summary>
        /// <param name="ruleConfig"></param>
        /// <returns></returns>
        public DynamicGroupCfgInfo LoadDynamicGroupCfg(MB.WinBase.IFace.IClientRuleConfig ruleConfig) {
            RuleClientLayoutAttribute rAtt = ruleConfig.ClientLayoutAttribute;
            if (rAtt == null || !rAtt.IsDynamicGroupEnabled) {
                throw new MB.Util.APPException(string.Format("要使用动态聚组，必须配置RuleClientLayoutAttribute中的DynamicGroupXmlConfig属性。请检查:{0}是否已经配置！", ruleConfig.GetType().FullName));
            }

            string xmlCfgFile = rAtt.UIXmlConfigFile;
            DynamicGroupCfgHelper cfgHelper = new DynamicGroupCfgHelper(xmlCfgFile);

            //将最终呈现聚组的列信息汇总到 聚组配置的列信息上
            Dictionary<string, ColumnPropertyInfo> resultCols = cfgHelper.GetResultColPropertys();

            DynamicGroupCfgInfo cfg = new DynamicGroupCfgInfo();
            cfg.MainEntityColInfo = cfgHelper.GetColProInfos(true);
            cfg.DetailEntityColInfo = cfgHelper.GetColProInfos(false);
            cfg.MainEntityInfo = cfgHelper.GetEntityInfo(true);
            cfg.DetailEntityInfo = cfgHelper.GetEntityInfo(false);
            cfg.RelationInfo = cfgHelper.GetRelationInfo();
            if (cfg.RelationInfo == null) {
                MB.Util.TraceEx.Write("[动态聚组配置]-由于RelationInfo的信息没有配置，DetailEntityInfo强制设置为空");
                cfg.DetailEntityInfo = null;
            }


            #region 配置验证
            if (cfg.MainEntityColInfo != null)
                validateColInfo(cfg.MainEntityColInfo);

            if (cfg.DetailEntityColInfo != null) {
                validateColInfo(cfg.DetailEntityColInfo);
            }

            if (cfg.RelationInfo != null) {
                foreach (DynamicGroupRelationInfo relationInfo in cfg.RelationInfo.Values) {
                    if (string.IsNullOrEmpty(relationInfo.Column) ||
                        string.IsNullOrEmpty(relationInfo.WithColumn))
                        throw new MB.Util.APPException("动态聚组配置Relation中的Column和WithColumn属性不能为空");
                }
            }

            if (cfg.MainEntityInfo != null) {
                if (string.IsNullOrEmpty(cfg.MainEntityInfo.Name))
                    throw new MB.Util.APPException("动态聚组配置MainEntity中的Name属性不能为空");
            }

            if (cfg.DetailEntityInfo != null) {
                if (string.IsNullOrEmpty(cfg.DetailEntityInfo.Name))
                    throw new MB.Util.APPException("动态聚组配置DetailEntity中的Name属性不能为空");
            }
            #endregion

            return cfg;
        }




        /// <summary>
        /// 获取动态聚组，分组列或者汇总列的设置的配置信息
        /// </summary>
        /// <param name="isMainEntity">主对象则是True，从对象则是False</param>
        /// <returns>对象的列配置信息集合</returns>
        public Dictionary<string, DynamicGroupColumnPropertyInfo> GetColProInfos(bool isMainEntity) {
            string xmlFileName = _XMLFile;
            string entityNode = string.Empty;
            if (isMainEntity)
                entityNode = MAIN_COLUMN_NODE;
            else
                entityNode = DETAIL_COLUMN_NODE;

            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            var cols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<DynamicGroupColumnPropertyInfo>("Name", xmlFileFullName, entityNode);
            if (cols != null) {
                Dictionary<string, ColumnPropertyInfo> uiColSettings = _UIColsSetting;
                foreach (DynamicGroupColumnPropertyInfo dgCol in cols.Values) {
                    if (uiColSettings.ContainsKey(dgCol.Name)) {
                        ColumnPropertyInfo uiInfo = uiColSettings[dgCol.Name];
                        if (string.IsNullOrEmpty(dgCol.Description))
                            dgCol.Description = uiInfo.Description;
                        if (string.IsNullOrEmpty(dgCol.DataType))
                            dgCol.DataType = uiInfo.DataType;
                    }
                    if (dgCol.ColArea == DynamicGroupColArea.None)
                        dgCol.ColArea = DynamicGroupColArea.Group;
                }
            }
            return cols;
        }

        /// <summary>
        /// 得到主/从实体的配置信息
        /// </summary>
        /// <param name="isMainEntity">主对象则是True，从对象则是False</param>
        /// <returns></returns>
        public DynamicGroupEntityInfo GetEntityInfo(bool isMainEntity) {
            string xmlFileName = _XMLFile;
            string entityNode = string.Empty;
            if (isMainEntity)
                entityNode = MAIN_ENTITY_INFO;
            else
                entityNode = DETAIL_ENTITY_INFO;

            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            var cols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<DynamicGroupEntityInfo>("Name", xmlFileFullName, entityNode);
            if (cols != null) {
                DynamicGroupEntityInfo entityInfo = cols.Values.FirstOrDefault<DynamicGroupEntityInfo>();
                return entityInfo;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取动态聚组，两个实体之间的关系的配置信息
        /// </summary>
        /// <returns>主从对象之间的关系配置信息</returns>
        public Dictionary<string, DynamicGroupRelationInfo> GetRelationInfo() {
            string xmlFileName = _XMLFile;
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            var cols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<DynamicGroupRelationInfo>("Name", xmlFileFullName, RELATIONS);
            return cols;
        }


        #region 内部函数
        /// <summary>
        /// 检查配置文件中的聚合字段属性是否正确
        /// </summary>
        /// <param name="colInfos">列配置信息</param>
        private void validateColInfo(Dictionary<string, DynamicGroupColumnPropertyInfo> colInfos) {
            foreach (DynamicGroupColumnPropertyInfo colInfo in colInfos.Values) {
                if (colInfo.ColArea == DynamicGroupColArea.Aggregation
                    && (colInfo.DataType == "System.String" || colInfo.DataType == "System.DateTime")) {
                    throw new MB.Util.APPException(string.Format("聚合字段:{0}的类型不能是 {1}", colInfo.Name, colInfo.DataType));
                }
            }
        }
        #endregion
    }

}
