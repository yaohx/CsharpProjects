//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-20。
// Description	:	获取UI 层 客户端XML 配置信息相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

using MB.WinBase.Common; 
namespace MB.WinBase {
    /// <summary>
    /// 获取UI 层 客户端XML 配置信息相关。 
    /// </summary>
    public class LayoutXmlConfigHelper {

        #region 通用XML 节点配置路径定义...
        public static readonly string LOGIC_KEYS_CFG_PATH = "/Entity/LogicKeys";
        public static readonly string LOGIC_EXISTS_CHECK_CFG_PATH = "/Entity/DBDataExistsCheck";
        /// <summary>
        /// 字段对应列的配置信息
        /// </summary>
        public static readonly string COLUMN_CONFIG_NODE = "/Entity/Columns/Column";
        /// <summary>
        /// Edit UI 的列配置信息。
        /// </summary>
        public static readonly string EDIT_UI_CONFIG_NODE = "/Entity/EditUI/Column";
        /// <summary>
        /// 汇总的列配置信息
        /// </summary>
        public static readonly string SUMMARY_COLUMN_CONFIG_NODE = "/Entity/Summary/Column";
        /// <summary>
        /// 汇总的列配置信息
        /// </summary>
        public static readonly string SUMMARY_INFO_CONFIG_NODE = "/Entity/Summary";
        /// <summary>
        /// Group Info 的字段配置信息。
        /// </summary>
        public static readonly string GROUP_FIELDS_CONFIG_NODE = "/Entity/EditUI/GroupFields/GroupField";
        /// <summary>
        /// 列表样式列配置信息。
        /// </summary>
        public static readonly string STYLE_CONDITION_CONFIG_NODE = "/Entity/StyleCondition/Condition";
        /// <summary>
        /// 纵向转横向需要配置的列配置信息。
        /// </summary>
        public static readonly string HVIEW_UI_CONFIG_NODE = "/Entity/ConfigHViewObj";
        /// <summary>
        /// 用户查询配置的信息。
        /// </summary>
        public static readonly string QUERY_ELEMENTS = "/Entity/DataFilter/Elements";
        /// <summary>
        ///  获取网格列布局的配置信息。
        /// </summary>
        public static readonly string GRID_VIEW_COLUMN_CONFIG_NODE = "/Entity/GridViews/GridViewLayout";
        /// <summary>
        /// 横向动态转换的配置信息。
        /// </summary>
        public static readonly string HVIEW_CONVERT_CFG = "/Entity/GridViews/HViewConvertCfgParam";
        /// <summary>
        /// 数据导入配置相关。
        /// </summary>
        public static readonly string DATA_IMPORT_CFG = "/Entity/DataImport";
        /// <summary>
        /// 树型控件的节点配置信息
        /// </summary>
        public static readonly string TREE_LIST_VIEW_CONFIG_NODE = "/Entity/TreeListView/TreeListViewCfg";

        private static readonly string MAIN_GRID_VIEW_ROW_HEIGHT = "/Entity/Columns";

        /// <summary>
        /// 图表的配置信息
        /// </summary>
        private static readonly string CHART_VIEW_CFG = "/Entity/Charts/Chart";
        #endregion 通用XML 节点配置路径定义...

        //配置 InvokeDataSourceDesc的数组长度
        private const int DEFAULT_ROW_HEIGHT = 16;

        #region Instance...
        private static Object _Obj = new object();
        private static LayoutXmlConfigHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        public LayoutXmlConfigHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static LayoutXmlConfigHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new LayoutXmlConfigHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region 数据转换处理相关...
        /// <summary>
        /// 根据配置的XML文件得到一个空的DataSet
        /// </summary>
        /// <param name="colPropertys"></param>
        /// <returns></returns>
        public DataSet CreateNULLDataByFieldPropertys(Dictionary<string, ColumnPropertyInfo> colPropertys) {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            var cols = colPropertys.Values.OrderBy(o => o.OrderIndex);
            foreach (ColumnPropertyInfo info in cols) {
                if (!info.Visibled) continue;
                DataColumn newCol = new DataColumn(info.Name,MB.Util.General.CreateSystemType(info.DataType,false));
                newCol.Caption = info.Description;
                dt.Columns.Add(newCol);
            }
            return ds;
        }
        #endregion 数据转换处理相关...

        #region 获取配置信息处理相关...

        /// <summary>
        /// 获取指定 XML 文件逻辑主键。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public string[] GetLogicKeys(string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            XmlDocument xmlDoc = MB.Util.XmlConfig.XmlConfigHelper.Instance.LoadXmlConfigFile(xmlFileFullName);
            if (xmlDoc != null) {
                XmlNode node = xmlDoc.SelectSingleNode(LOGIC_KEYS_CFG_PATH);
                if (node != null) {
                    string keys = node.InnerText;
                    return keys.Split(',');
                }
            }
            return new string[] { }; 
        }
        /// <summary>
        /// 获取需要进行数据库存在判断的字段名称。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public string[] GetExistsCheckFields(string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            XmlDocument xmlDoc = MB.Util.XmlConfig.XmlConfigHelper.Instance.LoadXmlConfigFile(xmlFileFullName);
            if (xmlDoc != null) {
                XmlNode node = xmlDoc.SelectSingleNode(LOGIC_EXISTS_CHECK_CFG_PATH);
                if (node != null) {
                    string keys = node.InnerText;
                    return keys.Split(',');
                }
            }
            return new string[] { };
        }
        /// <summary>
        /// 获取主窗口行的高度
        /// </summary>
        /// <returns></returns>
        public int GetMainGridViewRowHeight(string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            XmlDocument xmlDoc = MB.Util.XmlConfig.XmlConfigHelper.Instance.LoadXmlConfigFile(xmlFileFullName);
            if (xmlDoc != null) {
                XmlNode node = xmlDoc.SelectSingleNode(MAIN_GRID_VIEW_ROW_HEIGHT);
                if (node == null || node.Attributes["MainRowHelght"] == null) return DEFAULT_ROW_HEIGHT;

                return MB.Util.MyConvert.Instance.ToInt(node.Attributes["MainRowHelght"].Value);   
            }
            return DEFAULT_ROW_HEIGHT;
        }
        /// <summary>
        /// 获取树型控件的节点配置名称。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public TreeListViewCfgInfo GetTreeListViewCfgInfo(string xmlFileName, string cfgName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            if (string.IsNullOrEmpty(cfgName))
                cfgName = "Default";

            TreeListViewCfgInfo treeCfgInfo = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateConfigInfo <TreeListViewCfgInfo>("Name", xmlFileFullName, TREE_LIST_VIEW_CONFIG_NODE, cfgName);
            if (treeCfgInfo == null || string.IsNullOrEmpty(treeCfgInfo.KeyFieldName) ||
               string.IsNullOrEmpty(treeCfgInfo.ParentFieldName) ||
               string.IsNullOrEmpty(treeCfgInfo.DisplayFieldName))
                throw new MB.Util.APPException(string.Format("获取文件{0} 的 TreeListViewCfgInfo 配置文件出错,{1}、{2}、{3} 都不能为空，请检查",
                                               xmlFileName,"KeyFieldName", "ParentFieldName", "DisplayFieldName"), MB.Util.APPMessageType.SysErrInfo);

            return treeCfgInfo;
        }
        /// <summary>
        /// 获取横向动态转换的配置信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public MB.WinBase.Data.HViewConvertCfgParam GetHViewConvertCfgParam(string xmlFileName, string cfgName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            if (string.IsNullOrEmpty(cfgName))
                cfgName = "Default";

            return MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateConfigInfo<MB.WinBase.Data.HViewConvertCfgParam>("Name", xmlFileFullName, HVIEW_CONVERT_CFG, cfgName);
        }
        /// <summary>
        /// 获取数据导入配置相关信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public DataImportCfgInfo GetDataImportCfgInfo(string xmlFileName, string cfgName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            if (string.IsNullOrEmpty(cfgName))
                cfgName = "Default";

            return MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateConfigInfo<DataImportCfgInfo>("Name", xmlFileFullName, DATA_IMPORT_CFG, cfgName);
         
        }

        /// <summary>
        /// 获取网格列布局的配置信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public GridViewLayoutInfo GetGridColumnLayoutInfo(XmlDocument xmls, string cfgName) {
            if (string.IsNullOrEmpty(cfgName))
                cfgName = "Default";

            return MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateConfigInfoByXmlDoc<GridViewLayoutInfo>("Name", xmls, GRID_VIEW_COLUMN_CONFIG_NODE, cfgName);

        }

        /// <summary>
        /// 获取网格列布局的配置信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public GridViewLayoutInfo GetGridColumnLayoutInfo(string xmlFileName, string cfgName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            if (string.IsNullOrEmpty(cfgName))
                cfgName = "Default";

           return MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateConfigInfo<GridViewLayoutInfo>("Name", xmlFileFullName, GRID_VIEW_COLUMN_CONFIG_NODE, cfgName);
         
        }
        /// <summary>
        ///  根据xml文件名称 获取相应的对象查询配置信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public FilterElementCfgs GetDataFilterCfgElements(string xmlFileName, string cfgName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            XmlNode rootNode = null;
            var lstData = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<DataFilterElementCfgInfo>("Name", xmlFileFullName, QUERY_ELEMENTS, cfgName, out rootNode);
            if (lstData == null || lstData.Count == 0)
                throw new MB.Util.APPException(string.Format("根据查询带{0} 在 XML 文件{1} 中找不到相应的Node,请检查配置文件",cfgName,xmlFileName));
 
            FilterElementCfgs cfgs = new FilterElementCfgs(cfgName,lstData);
            if (rootNode != null) {
                if (rootNode.Attributes["AllowEmptyFilter"] != null) {
                    cfgs.AllowEmptyFilter = MB.Util.MyConvert.Instance.ToBool(rootNode.Attributes["AllowEmptyFilter"].Value);   
                }

                if (rootNode.Attributes["AllowQueryAll"] != null) {
                    cfgs.AllowQueryAll = MB.Util.MyConvert.Instance.ToBool(rootNode.Attributes["AllowQueryAll"].Value);  
                }

                if (rootNode.Attributes["Width"] != null)
                    cfgs.Width = MB.Util.MyConvert.Instance.ToInt(rootNode.Attributes["Width"].Value);

                if (rootNode.Attributes["Height"] != null)
                    cfgs.Height = MB.Util.MyConvert.Instance.ToInt(rootNode.Attributes["Height"].Value);
            }
            return cfgs;
        }

        /// <summary>
		/// 根据xml文件名称 获取相应的列表 Style Info 的信息。
		/// </summary>
		/// <param name="mapXmlFileName"></param>
		/// <returns></returns>
        public Dictionary<string, StyleConditionInfo> GetStyleConditions(string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            return  MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<StyleConditionInfo>("Name", xmlFileFullName, STYLE_CONDITION_CONFIG_NODE);   
        }

        /// <summary>
        /// 获取XML 文件配置中列的配置信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public Dictionary<string, ColumnPropertyInfo> GetColumnPropertys(XmlDocument xmls) {
            var cols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityListByXmlDoc<ColumnPropertyInfo>("Name", xmls, COLUMN_CONFIG_NODE);
            int orderIndex = 0;
            foreach (ColumnPropertyInfo info in cols.Values) {
                if (info.OrderIndex <= 0)
                    info.OrderIndex = orderIndex++;
            }
            return cols;
        }

        /// <summary>
        /// 获取XML 文件配置中列的配置信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public Dictionary<string, ColumnPropertyInfo> GetColumnPropertys(string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            MB.Util.TraceEx.Write(string.Format("开始从 XML 文件 {0} 中获取列的信息",xmlFileName));

            var cols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<ColumnPropertyInfo>("Name", xmlFileFullName, COLUMN_CONFIG_NODE);
            int orderIndex = 0;
            foreach (ColumnPropertyInfo info in cols.Values) {
                if (info.OrderIndex <= 0)
                    info.OrderIndex = orderIndex++;
            }
            return cols;
        }

        /// <summary>
        /// 根据XML 文件获取 对应列需要的编辑配置信息。
        /// </summary>
        /// <param name="columnPropertys"></param>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public Dictionary<string, ColumnEditCfgInfo> GetColumnEdits(Dictionary<string, ColumnPropertyInfo> columnPropertys, XmlDocument xmls) {
            Dictionary<string, ColumnEditCfgInfo> cfgCols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityListByXmlDoc<ColumnEditCfgInfo>("Name", xmls, EDIT_UI_CONFIG_NODE);
            if (cfgCols != null && cfgCols.Count > 0)
                fillEditColsDataSource(columnPropertys, cfgCols);

            return cfgCols;
        }

        /// <summary>
        /// 根据XML 文件获取 对应列需要的编辑配置信息。
        /// </summary>
        /// <param name="columnPropertys"></param>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public Dictionary<string, ColumnEditCfgInfo> GetColumnEdits(Dictionary<string, ColumnPropertyInfo> columnPropertys,string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
             Dictionary<string, ColumnEditCfgInfo> cfgCols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<ColumnEditCfgInfo>("Name", xmlFileFullName, EDIT_UI_CONFIG_NODE);
             if(cfgCols!=null && cfgCols.Count > 0)
                 fillEditColsDataSource(columnPropertys,cfgCols);

             return cfgCols;
        }

        /// <summary>
        /// 得到汇总信息
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public SummaryInfo GetSummaryInfo(string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            Dictionary<string, SummaryInfo> cfgCols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<SummaryInfo>("Name", xmlFileFullName, SUMMARY_INFO_CONFIG_NODE);
            SummaryInfo summaryInfo = null;
            if (cfgCols != null && cfgCols.Count > 0) {
                summaryInfo = cfgCols.FirstOrDefault().Value;
                fillSummaryInfoDataSource(summaryInfo); //填充数据源

                var colProp = GetColumnPropertys(xmlFileName);
                summaryInfo.ColumnSummaryInfos = getColumnSummary(colProp, xmlFileName); //加载汇总列信息
                

            }
            return summaryInfo;
        }


        /// <summary>
        /// 根据XML 文件获取 对应列需要的编辑配置信息。
        /// </summary>
        /// <param name="columnPropertys"></param>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        private Dictionary<string, ColumnSummaryInfo> getColumnSummary(Dictionary<string, ColumnPropertyInfo> columnPropertys, string xmlFileName) {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            Dictionary<string, ColumnSummaryInfo> cfgCols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<ColumnSummaryInfo>("Name", xmlFileFullName, SUMMARY_COLUMN_CONFIG_NODE);
            if (cfgCols != null && cfgCols.Count > 0)
                handleSummaryCols(columnPropertys, cfgCols);

            return cfgCols;
        }

        /// <summary>
        /// 根据XML 文件获取 图表需要的配置信息。
        /// </summary>
        /// <param name="columnPropertys"></param>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public Dictionary<string, ChartViewCfgInfo> getChartViewCfg(string xmlFileName)
        {
            string xmlFileFullName = ShareLib.Instance.BuildXmlConfigFileFullName(xmlFileName);
            Dictionary<string, ChartViewCfgInfo> cfgCols = MB.Util.XmlConfig.XmlConfigHelper.Instance.CreateEntityList<ChartViewCfgInfo>("Name", xmlFileFullName, CHART_VIEW_CFG);

            return cfgCols;
        }

        #endregion 获取配置信息处理相关...

        #region 加载编辑列数据相关...
        //加载下拉编辑框的数据源
        private void fillEditColsDataSource(Dictionary<string, ColumnPropertyInfo> columnPropertys, Dictionary<string, ColumnEditCfgInfo> cfgCols) {
            foreach (ColumnEditCfgInfo cfgInfo in cfgCols.Values) {
                if (cfgInfo.InvokeDataSourceDesc==null)
                    continue;
                if (!cfgInfo.SaveLocalCache)
                    continue;
                if (string.IsNullOrEmpty(cfgInfo.InvokeDataSourceDesc.Method))
                    throw new MB.Util.APPException(string.Format("列{0} 的InvokeDataSourceDesc配置中Method 的 配置不能为空 ", cfgInfo.Name), Util.APPMessageType.SysErrInfo);

                string[] descs = cfgInfo.InvokeDataSourceDesc.Type.Split(',');
                string[] method = cfgInfo.InvokeDataSourceDesc.Method.Split(',');
                string[] conPars = null;
                if (method.Length == 0)
                    throw new MB.Util.APPException(string.Format("列{0}的调用方法没有配置",cfgInfo.Name) , MB.Util.APPMessageType.SysErrInfo);
  
                if (!string.IsNullOrEmpty(cfgInfo.InvokeDataSourceDesc.TypeConstructParams))
                    conPars = cfgInfo.InvokeDataSourceDesc.TypeConstructParams.Split(','); 

                //if (descs.Length != INVOKE_METHOD_CFG_A_LENGTH) {
                //    MB.Util.TraceEx.Write("EDITUI 的 InvokeDataSourceDesc 配置有误,配置必须包含类、方法、参数和配件！");  
                //    continue;
                //}

                object instance = MB.Util.DllFactory.Instance.LoadObject(descs[0], conPars, descs[1]);
                if (instance == null)
                    throw new MB.Util.APPException(string.Format("在配置下拉框数据源时根据 {0}, {1} 实例化类出错" ,descs[1],descs[0]), Util.APPMessageType.SysErrInfo);
                object val = method.Length == 1 ? MB.Util.MyReflection.Instance.InvokeMethod(instance, method[0]) :
                                               MB.Util.MyReflection.Instance.InvokeMethod(instance, method[0], method[1].Split(';'));
                
                if (val == null) continue;

                if (val.GetType().Equals(typeof(DataSet))) {
                    cfgInfo.DataSource = val;
                    continue;
                }
                IList lst = val as IList;
                if (lst != null) {
                    cfgInfo.DataSource = convertToDataSet(columnPropertys, cfgInfo, lst);
                    continue;
                }
                throw new MB.Util.APPException("EDITUI 的 InvokeDataSourceDesc 配置有误,配置的方法返回值必须是 IList 或DataSet 请注意配置 ValueFieldName 和 TextFieldName", MB.Util.APPMessageType.SysErrInfo);  
            }
        }
        //转换获取到的数据源。  
        private DataSet convertToDataSet(Dictionary<string, ColumnPropertyInfo> columnPropertys, ColumnEditCfgInfo cfgInfo, IList lstDatas) {
            DataSet dsData = new DataSet();
            DataTable dt = new DataTable();
            ColumnPropertyInfo columnInfo = null;
            if (columnPropertys!=null && columnPropertys.ContainsKey(cfgInfo.Name))
                columnInfo = columnPropertys[cfgInfo.Name];
            if(columnInfo!=null && !string.IsNullOrEmpty(columnInfo.DataType))
                dt.Columns.Add(cfgInfo.ValueFieldName, MB.Util.General.CreateSystemType(columnInfo.DataType,false));
            else
                dt.Columns.Add(cfgInfo.ValueFieldName);

            if(!dt.Columns.Contains(cfgInfo.TextFieldName)) //添加列判断，兼容Value和Text字段值相同的情况  modify by aifang 2012-10-16
                dt.Columns.Add(cfgInfo.TextFieldName);

            if (lstDatas != null && lstDatas.Count > 0) {
                object entity = lstDatas[0];
                if (entity.GetType().Equals(typeof(KeyValuePair<string, string>))) {
                    foreach (KeyValuePair<string, string> valInfo in lstDatas) {
                        dt.Rows.Add(valInfo.Key, valInfo.Value);
                    }
                }
                else {
                    Type entityType = entity.GetType();
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, cfgInfo.ValueFieldName))
                        throw new MB.Util.APPException(string.Format("请检查XML 配置文件 EDITUI 列 {0} 的下拉列表框返回值中是否包含 属性{1} ,请配置正确的ValueFieldName", cfgInfo.Name, cfgInfo.ValueFieldName), MB.Util.APPMessageType.SysErrInfo);
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, cfgInfo.TextFieldName))
                        throw new MB.Util.APPException(string.Format("请检查XML 配置文件 EDITUI 列 {0} 的下拉列表框返回值中是否包含 属性{1},请配置正确的TextFieldName", cfgInfo.Name, cfgInfo.TextFieldName), MB.Util.APPMessageType.SysErrInfo);

                    return MB.Util.MyConvert.Instance.ConvertEntityToDataSet(entityType,lstDatas,null);
                    //foreach (object obj in lstDatas) {
                    //    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(obj, cfgInfo.ValueFieldName);
                    //    if (columnInfo != null && !string.IsNullOrEmpty(columnInfo.DataType)) {
                    //        val = MB.Util.MyReflection.Instance.ConvertValueType(MB.Util.General.CreateSystemType(columnInfo.DataType, false), val);
                    //    }
                    //    object txt = MB.Util.MyReflection.Instance.InvokePropertyForGet(obj, cfgInfo.TextFieldName);

                    //    dt.Rows.Add(val, txt);
                    //}
                }
            }
            dsData.Tables.Add(dt);
            return dsData;
        }
        #endregion 加载编辑列数据相关...

        #region 加载汇总列数据相关
        /// <summary>
        /// 填充汇总UI的数据源
        /// </summary>
        /// <param name="summaryInfo"></param>
        private void fillSummaryInfoDataSource(SummaryInfo summaryInfo) {
            if (summaryInfo.InvokeDataSourceDesc == null)
                return;

            if (string.IsNullOrEmpty(summaryInfo.InvokeDataSourceDesc.Method))
                throw new MB.Util.APPException(string.Format("Summray {0} 的InvokeDataSourceDesc配置中Method 的 配置不能为空 ", summaryInfo.Name), Util.APPMessageType.SysErrInfo);

            string[] descs = summaryInfo.InvokeDataSourceDesc.Type.Split(',');
            string[] method = summaryInfo.InvokeDataSourceDesc.Method.Split(',');
            string[] conPars = null;
            if (method.Length == 0)
                throw new MB.Util.APPException(string.Format("Summray{0}的调用方法没有配置", summaryInfo.Name), MB.Util.APPMessageType.SysErrInfo);

            if (!string.IsNullOrEmpty(summaryInfo.InvokeDataSourceDesc.TypeConstructParams))
                conPars = summaryInfo.InvokeDataSourceDesc.TypeConstructParams.Split(',');


            object instance = MB.Util.DllFactory.Instance.LoadObject(descs[0], conPars, descs[1]);
            if (instance == null)
                throw new MB.Util.APPException(string.Format("Summray在获取数据源时根据 {0}, {1} 实例化类出错", descs[1], descs[0]), Util.APPMessageType.SysErrInfo);
            object val = method.Length == 1 ? MB.Util.MyReflection.Instance.InvokeMethod(instance, method[0]) :
                                           MB.Util.MyReflection.Instance.InvokeMethod(instance, method[0], method[1].Split(';'));

            if (val == null) return;

            if (val.GetType().Equals(typeof(DataSet))) {
                throw new MB.Util.APPException("Summray 的 InvokeDataSourceDesc 配置有误,配置的方法返回值必须是 IList", MB.Util.APPMessageType.SysErrInfo);
            }
            IList lst = val as IList;
            if (lst != null) {
                summaryInfo.DataSource = lst;
                return;
            }
        }


       

        /// <summary>
        /// 处理汇总的列的信息
        /// 包括了从ColumnPropertyInfo填充一些信息，处理一些误加入空格的情况
        /// </summary>
        /// <param name="colPropertys"></param>
        /// <param name="summaryCols"></param>
        private void handleSummaryCols(Dictionary<string, ColumnPropertyInfo> colPropertys, Dictionary<string, ColumnSummaryInfo> summaryCols) {
            string logHeader = "[FillSummaryCols]";
            foreach (ColumnSummaryInfo sumCol in summaryCols.Values) {
                ColumnPropertyInfo colProperty = colPropertys[sumCol.ColumnName];
                if (colProperty == null) {
                    MB.Util.TraceEx.Write("{0}-列{1}未在ColumnPropertyInfo中配置", logHeader, sumCol.Name);
                    continue;
                }

                //如果是汇总列，并且没有汇总函数，则继承colProperty的设置
                if (sumCol.IsSummaryItem && sumCol.SummaryItemType == SummaryItemType.None) {
                    if (colProperty.SummaryItemType == SummaryItemType.None) {
                        MB.Util.TraceEx.Write("{0}-列{1}配置的SummaryItemType不能为None。可以配置为Average，Count，Max，Min，Sum", logHeader, sumCol.Name);
                        continue;
                    }
                    else
                        sumCol.SummaryItemType = colProperty.SummaryItemType;
                }
            }
        }

        

        #endregion


      
    }
}
