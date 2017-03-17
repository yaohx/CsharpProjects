using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MB.WinBase.IFace;
using System.IO;
using MB.Util.Model;
using System.ComponentModel;
using MB.WinBase;
using MB.XWinLib.XtraGrid;
using System.Xml;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.XtraGrid;
using MB.WinBase.Common.DynamicGroup;

namespace MB.WinClientDefault.DynamicGroup {
    /// <summary>
    /// 关于动态聚组的一些
    /// </summary>
    public class DynamicGroupUIHelper {
        /// <summary>
        /// 刷新动态聚组条件设置的消息ID
        /// 用于通知主窗口，动态聚组按钮是否需要点亮的消息控制
        /// </summary>
        public static readonly string DYNAMIC_GROUP_ACTIVE_MSG_ID = "519C6345-D625-4CC0-A5DA-DBAE1AB1EADE";
        /// <summary>
        /// 用来表示到底是聚合字段还是分组字段
        /// </summary>
        public static readonly string DYMANIC_GROUP_AGG_TYPE = "AGG_TYPE";
        /// <summary>
        /// 动态聚组条件网格，增加选择框，选择框的字段名
        /// </summary>
        public static readonly string DYMANIC_GROUP_COL_SELECTED = "SELECTED";
      
        #region 动态聚组条件UI XML
        /// <summary>
        /// 动态聚组框的客户端UI XML
        /// </summary>
        private const string DYNAMIC_CONDITION_UI_XML_CONTENT = @"<?xml version='1.0' encoding='utf-8' ?>
<Entity>
  <Columns>
    <Column Name = 'ENTITY_NAME' Description = '所属对象' DataType = 'System.String' IsKey = 'False' IsNull = 'True'  Visibled = 'False' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'ENTITY_DESCRIPTION' Description = '所属对象' DataType = 'System.String' IsKey = 'False' IsNull = 'True'  Visibled = 'True' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'COLUMN_NAME' Description = '聚组列名'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'False' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'COLUMN_DESCRIPTION' Description = '聚组列'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'COLUMN_FIELD_TYPE' Description = '聚组列类型'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'AGG_TYPE' Description = '聚合类型'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'True' VisibleWidth = '100' />
    <Column Name = 'AGG_CONDITION_OPERATOR' Description = '操作符' DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'True' VisibleWidth = '100' />
    <Column Name = 'AGG_VALUE' Description = '聚组值' DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'True' VisibleWidth = '100' />
  </Columns>
  <GridViews>
    <GridViewLayout Name='DefaultGridView'>
      <Column Name='Date'>
        <DisplayFormat>
          <FormatString>yyyy-MM-dd HH:mm:ss</FormatString>
          <FormatType>DateTime</FormatType>
        </DisplayFormat>
      </Column>
    </GridViewLayout>
  </GridViews>
  <EditUI>
    <Column Name='AGG_CONDITION_OPERATOR'>
      <EditControlType>LookUpEdit</EditControlType>
      <SaveLocalCache>True</SaveLocalCache>
      <TextFieldName>NAME</TextFieldName>
      <ValueFieldName>ID</ValueFieldName>
      <InvokeDataSourceDesc Type='MB.WinClientDefault.DynamicGroup.DynamicGroupUIColumns, MB.WinClientDefault.dll' Method='GetOperators, COLUMN_FIELD_TYPE' />
      <EditCtlDataMappings>
        <EditCtlDataMappingInfo ColumnName = 'AGG_CONDITION_OPERATOR' SourceColumnName = 'ID' />
      </EditCtlDataMappings>
    </Column>
    <Column Name='AGG_TYPE'>
      <EditControlType>LookUpEdit</EditControlType>
      <SaveLocalCache>True</SaveLocalCache>
      <TextFieldName>NAME</TextFieldName>
      <ValueFieldName>ID</ValueFieldName>
      <InvokeDataSourceDesc Type='MB.WinClientDefault.DynamicGroup.DynamicGroupUIColumns, MB.WinClientDefault.dll' Method='GetSummaryItems, COLUMN_FIELD_TYPE' />
      <EditCtlDataMappings>
        <EditCtlDataMappingInfo ColumnName = 'AGG_TYPE' SourceColumnName = 'ID' />
      </EditCtlDataMappings>
    </Column>
  </EditUI>
</Entity>
";
        #endregion

        private static readonly string DYNAMIC_GROUP_DIR = MB.Util.General.GeApplicationDirectory() + @"DynamicGroupSetting\";
        private static readonly string DYNAMIC_GROUP_SETTING_FULLNAME = DYNAMIC_GROUP_DIR + "{0}.xml";

        private IClientRuleQueryBase _ClientRuleObject; //客户端UI Rule
        private GridControlEx _GirdControl; //客户端呈现动态聚组的网格
        private DynamicGroupCfgHelper _DynamicGroupConfigHelper; //动态聚组配置帮助类
        private LayoutXmlConfigHelper _UIXmlHelper; //客户端界面UIXml配置
        private DynamicGroupCfgInfo _DynamicCfgInfo; //动态聚组的配置

        #region 构造函数
        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="clientRuleObject">客户端UI Rule</param>
        /// <param name="girdControl">用于显示的Grid Control</param>
        public DynamicGroupUIHelper(IClientRuleQueryBase clientRuleObject,
            MB.XWinLib.XtraGrid.GridControlEx girdControl) {
            _ClientRuleObject = clientRuleObject;
            _GirdControl = girdControl;
            _UIXmlHelper = LayoutXmlConfigHelper.Instance;
            _DynamicGroupConfigHelper = new DynamicGroupCfgHelper(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
            _DynamicCfgInfo = _DynamicGroupConfigHelper.LoadDynamicGroupCfg(_ClientRuleObject);
        }
        #endregion

        #region 关于保存默认聚组设定的方法

        /// <summary>
        /// 保存配置信息
        /// </summary>
        public void SaveDynamicGroupSettings(List<DynamicGroupUIColumns> dynamicGroupColumns) {
            IClientRuleQueryBase clientRuleObject = _ClientRuleObject;
            try {
                //保存列配置信息
                if (!Directory.Exists(DYNAMIC_GROUP_DIR)) Directory.CreateDirectory(DYNAMIC_GROUP_DIR);
                if (dynamicGroupColumns != null && dynamicGroupColumns.Count > 0) {
                    //string sectionName = clientRuleObject.GetType().FullName + "~" + pivotGrid.Name;
                    string sectionName = getDynamicSettingSectionName();
                    string fullFileName = string.Format(DYNAMIC_GROUP_SETTING_FULLNAME, sectionName);
                    if (File.Exists(fullFileName))
                        File.Delete(fullFileName);
                    var serializer = new MB.Util.Serializer.DataContractFileSerializer<List<DynamicGroupUIColumns>>(fullFileName);
                    serializer.Write(dynamicGroupColumns);
                }
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "保存动态聚组设定失败");
            }
        }

        /// <summary>
        /// 恢复汇总列的信息
        /// </summary>
        public List<DynamicGroupUIColumns> RestoreDynamicGroupSetting() {
            IClientRuleQueryBase clientRuleObject = _ClientRuleObject;
            try {
                string sectionName = getDynamicSettingSectionName();
                string fullFileName = string.Format(DYNAMIC_GROUP_SETTING_FULLNAME, sectionName);
                if (File.Exists(fullFileName)) {
                    var settings = new MB.Util.Serializer.DataContractFileSerializer<List<DynamicGroupUIColumns>>(fullFileName).Read();
                    return settings;
                }

                return null;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "恢复动态聚组设定失败");
            }
        }

        /// <summary>
        /// 清空汇总列的客户端缓存
        /// </summary>
        public void ClearDynamicGroupSetting() {
            IClientRuleQueryBase clientRuleObject = _ClientRuleObject;
            try {
                string sectionName = getDynamicSettingSectionName();
                string fullFileName = string.Format(DYNAMIC_GROUP_SETTING_FULLNAME, sectionName);
                if (File.Exists(fullFileName)) {
                    File.Delete(fullFileName);
                }
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "清空动态聚组设定失败");
            }
        }

        #endregion


        /// <summary>
        /// 把汇总条件绑定到网格上，让用户能自由操作
        /// 每次动态聚组窗口加载是调用
        /// </summary>
        public void BindDynamicGroupColumnsToGrid() {
            //用到的全局变量
            DynamicGroupCfgInfo dyConfigInfo = _DynamicCfgInfo;
            GridControlEx grid = _GirdControl;
            IClientRuleQueryBase clientRuleObject = _ClientRuleObject;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(DynamicGroupUIHelper.DYNAMIC_CONDITION_UI_XML_CONTENT);
            var cols = _UIXmlHelper.GetColumnPropertys(xmlDoc);
            var colEdits = _UIXmlHelper.GetColumnEdits(cols, xmlDoc);
            var gridViewLayoutInfo = _UIXmlHelper.GetGridColumnLayoutInfo(xmlDoc, string.Empty);

            #region 准备数据源
            //先从缓存的地方恢复设置，如果没有
            List<DynamicGroupUIColumns> columns = RestoreDynamicGroupSetting();
            if (columns == null) {
                //从配置重初始化
                columns = GetDynamicGroupColumnsFromConfig();
            }
            BindingList<DynamicGroupUIColumns>  bindingList = new BindingList<DynamicGroupUIColumns>(columns);
            bindingList.AllowNew = false;
            bindingList.AllowRemove = false;
            #endregion

            var detailBindingParams = new MB.XWinLib.GridDataBindingParam(grid, bindingList);

            MB.XWinLib.XtraGrid.XtraGridEditHelper gridEditHelper = MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance;
            gridEditHelper.CreateEditXtraGrid(detailBindingParams, cols, colEdits, gridViewLayoutInfo);
            gridEditHelper.AppendEditSelectedColumn(grid, "选择", DynamicGroupUIHelper.DYMANIC_GROUP_COL_SELECTED);


            GridView mainView = grid.MainView as GridView;
            if (mainView != null) {
                mainView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                mainView.OptionsSelection.MultiSelect = true;
                mainView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
                mainView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            }

            #region 对聚合的字段加载样式，前景色蓝色
            var aggTypeCol = mainView.Columns.ColumnByFieldName(DynamicGroupUIHelper.DYMANIC_GROUP_AGG_TYPE);
            if (aggTypeCol != null) {
                StyleFormatCondition formatCondition = new StyleFormatCondition(
                DevExpress.XtraGrid.FormatConditionEnum.NotEqual,
                aggTypeCol, "列类型", string.Empty, null, true);

                formatCondition.Appearance.ForeColor = Color.Blue;
                if (formatCondition != null)
                    mainView.FormatConditions.Add(formatCondition);

            }
            #endregion

        }

        /// <summary>
        /// 根据Grid得到动态聚组的最终结果，用来传递到服务端
        /// </summary>
        /// <returns>动态聚组设定，用来传递到服务端的</returns>
        public MB.Util.Model.DynamicGroupSetting GetDynamicGroupSetting() {
            List<DynamicGroupUIColumns> dyUIColumns = new List<DynamicGroupUIColumns>();
            return GetDynamicGroupSetting(ref dyUIColumns);
        }
        /// <summary>
        /// 根据Grid得到动态聚组的最终结果，用来传递到服务端
        /// </summary>
        /// <param name="dyUIColumns">用来保存Grid的当前状态</param>
        /// <returns>动态聚组设定，用来传递到服务端的</returns>
        public MB.Util.Model.DynamicGroupSetting GetDynamicGroupSetting(ref List<DynamicGroupUIColumns> dyUIColumns) {
            //用到的全局变量
            DynamicGroupCfgInfo dyConfigInfo = _DynamicCfgInfo;
            GridControlEx grid = _GirdControl;

            DynamicGroupSetting setting = new DynamicGroupSetting();
            //设置实体
            setting.EntityInfos.MainEntity = dyConfigInfo.MainEntityInfo;
            setting.EntityInfos.DetailEntity = dyConfigInfo.DetailEntityInfo;

            //设置关系
            if (_DynamicCfgInfo.RelationInfo != null)
                setting.RelationInfos = _DynamicCfgInfo.RelationInfo.Values.ToList<DynamicGroupRelationInfo>();

            BindingList<DynamicGroupUIColumns> uiColList = grid.DataSource as BindingList<DynamicGroupUIColumns>;
            foreach (DynamicGroupUIColumns column in uiColList) {
                dyUIColumns.Add(column);

                if (!column.SELECTED)
                    continue;

                string entityName = column.ENTITY_NAME;
                string colName = column.COLUMN_NAME;
                if (string.IsNullOrEmpty(column.AGG_TYPE)) {
                    //对分组列做处理
                    if (setting.GroupFields.ContainsKey(entityName))
                        setting.GroupFields[entityName].Add(colName);
                    else {
                        var list = new List<string>();
                        list.Add(colName);
                        setting.GroupFields.Add(entityName, list);
                    }
                }
                else {
                    //对聚组列做处理
                    DataAreaField dataField = new DataAreaField();
                    dataField.Name = colName;
                    dataField.Description = column.COLUMN_DESCRIPTION;
                    dataField.SummaryItemType = (MB.Util.Model.SummaryItemType)Enum.Parse(typeof(MB.Util.Model.SummaryItemType), column.AGG_TYPE.ToString(), true);

                    if (string.Compare(dyConfigInfo.MainEntityInfo.Name, entityName) == 0) {
                        dataField.DataType = dyConfigInfo.MainEntityColInfo[colName].DataType;
                        dataField.EntityName = entityName;
                        dataField.EntityDescription = dyConfigInfo.MainEntityInfo.Description;
                        if (!string.IsNullOrEmpty(column.AGG_CONDITION_OPERATOR) && !string.IsNullOrEmpty(column.AGG_VALUE)) {
                            dataField.ConditionOperator = (DynamicGroupConditionOperator)Enum.Parse(typeof(DynamicGroupConditionOperator), column.AGG_CONDITION_OPERATOR);
                            dataField.ConditionValue = column.AGG_VALUE;
                        }
                    }
                    else {
                        if (dyConfigInfo.DetailEntityInfo != null) {
                            dataField.DataType = dyConfigInfo.DetailEntityColInfo[colName].DataType;
                            dataField.EntityName = entityName;
                            dataField.EntityDescription = dyConfigInfo.DetailEntityInfo.Description;
                            if (!string.IsNullOrEmpty(column.AGG_CONDITION_OPERATOR) && !string.IsNullOrEmpty(column.AGG_VALUE)) {
                                dataField.ConditionOperator = (DynamicGroupConditionOperator)Enum.Parse(typeof(DynamicGroupConditionOperator), column.AGG_CONDITION_OPERATOR);
                                dataField.ConditionValue = column.AGG_VALUE;
                            }
                        }
                    }

                    if (setting.DataAreaFields.ContainsKey(entityName))
                        setting.DataAreaFields[entityName].Add(dataField);
                    else {
                        var list = new List<DataAreaField>();
                        list.Add(dataField);
                        setting.DataAreaFields.Add(entityName, list);
                    }
                }
            }

            return setting;
        }

        /// <summary>
        /// 根据Rule的全名得到保存在客户端动态聚组配置的sectionName
        /// </summary>
        /// <returns></returns>
        private string getDynamicSettingSectionName() {
            IClientRuleQueryBase clientRuleObject = _ClientRuleObject;
            string sectionName = clientRuleObject.GetType().FullName;
            return sectionName;
        }




        /// <summary>
        /// 得到动态聚组列的配置信息
        /// </summary>
        private List<DynamicGroupUIColumns> GetDynamicGroupColumnsFromConfig() {
            List<DynamicGroupUIColumns> columns = new List<DynamicGroupUIColumns>();
            //把主对象的动态聚组列信息抓取出来
            foreach (DynamicGroupColumnPropertyInfo dyColInfo in _DynamicCfgInfo.MainEntityColInfo.Values) {
                DynamicGroupUIColumns col = new DynamicGroupUIColumns();
                col.ENTITY_NAME = _DynamicCfgInfo.MainEntityInfo.Name;
                col.ENTITY_DESCRIPTION = _DynamicCfgInfo.MainEntityInfo.Description;
                col.COLUMN_NAME = dyColInfo.Name;
                col.COLUMN_DESCRIPTION = dyColInfo.Description;
                col.COLUMN_FIELD_TYPE = dyColInfo.DataType;
                col.COL_AREA = dyColInfo.ColArea;
                col.AGG_TYPE = (dyColInfo.ColArea == DynamicGroupColArea.Group ? string.Empty : dyColInfo.SummaryItemType.ToString());
                col.SELECTED = true;
                columns.Add(col);
            }

            if (_DynamicCfgInfo.DetailEntityColInfo != null && _DynamicCfgInfo.DetailEntityColInfo.Count > 0) {
                foreach (DynamicGroupColumnPropertyInfo dyColInfo in _DynamicCfgInfo.DetailEntityColInfo.Values) {
                    DynamicGroupUIColumns col = new DynamicGroupUIColumns();
                    col.ENTITY_NAME = _DynamicCfgInfo.DetailEntityInfo.Name;
                    col.ENTITY_DESCRIPTION = _DynamicCfgInfo.DetailEntityInfo.Description;
                    col.COLUMN_NAME = dyColInfo.Name;
                    col.COLUMN_DESCRIPTION = dyColInfo.Description;
                    col.COLUMN_FIELD_TYPE = dyColInfo.DataType;
                    col.COL_AREA = dyColInfo.ColArea;
                    col.AGG_TYPE = (dyColInfo.ColArea == DynamicGroupColArea.Group ? string.Empty : dyColInfo.SummaryItemType.ToString());
                    col.SELECTED = true;
                    columns.Add(col);
                }
            }
            return columns;
        }

    }




}
