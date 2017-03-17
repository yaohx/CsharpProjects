using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MB.WinBase.IFace;
using System.IO;
using MB.Util.Model;

namespace MB.WinClientDefault.DynamicGroup {
    /// <summary>
    /// 关于动态聚组的一些
    /// </summary>
    public static class DynamicGroupCommon {
        #region 动态聚组条件UI XML
        public const string DYNAMIC_CONDITION_UI_XML_CONTENT = @"<?xml version='1.0' encoding='utf-8' ?>
<Entity>
  <Columns>
    <Column Name = 'ENTITY_NAME' Description = '所属对象' DataType = 'System.String' IsKey = 'False' IsNull = 'True'  Visibled = 'False' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'ENTITY_DESCRIPTION' Description = '所属对象' DataType = 'System.String' IsKey = 'False' IsNull = 'True'  Visibled = 'True' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'GROUP_NAME' Description = '聚组列名'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'False' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'GROUP_FIELD_TYPE' Description = '聚组列类型'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'False' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'SUMMARY_TYPE' Description = '聚合类型'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'True' VisibleWidth = '100' />
    <Column Name = 'GROUP_DESCRIPTION' Description = '聚组列'  DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'false' VisibleWidth = '100' />
    <Column Name = 'GROUP_CONDITION_OPERATOR' Description = '操作符' DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'True' VisibleWidth = '100' />
    <Column Name = 'GROUP_VALUE' Description = '聚组值' DataType = 'System.String' IsKey = 'False' IsNull = 'False'  Visibled = 'True' CanEdit = 'True' VisibleWidth = '100' />
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
    <Column Name='GROUP_CONDITION_OPERATOR'>
      <EditControlType>LookUpEdit</EditControlType>
      <SaveLocalCache>True</SaveLocalCache>
      <TextFieldName>NAME</TextFieldName>
      <ValueFieldName>ID</ValueFieldName>
      <InvokeDataSourceDesc Type='MB.WinClientDefault.DynamicGroup.DynamicGroupCondition, MB.WinClientDefault.dll' Method='GetOperators, GROUP_FIELD_TYPE' />
      <EditCtlDataMappings>
        <EditCtlDataMappingInfo ColumnName = 'GROUP_CONDITION_OPERATOR' SourceColumnName = 'ID' />
      </EditCtlDataMappings>
    </Column>
    <Column Name='SUMMARY_TYPE'>
      <EditControlType>LookUpEdit</EditControlType>
      <SaveLocalCache>True</SaveLocalCache>
      <TextFieldName>NAME</TextFieldName>
      <ValueFieldName>ID</ValueFieldName>
      <InvokeDataSourceDesc Type='MB.WinClientDefault.DynamicGroup.DynamicGroupCondition, MB.WinClientDefault.dll' Method='GetSummaryItems, GROUP_FIELD_TYPE' />
      <EditCtlDataMappings>
        <EditCtlDataMappingInfo ColumnName = 'SUMMARY_TYPE' SourceColumnName = 'ID' />
      </EditCtlDataMappings>
    </Column>
  </EditUI>
</Entity>
";
        #endregion

        /// <summary>
        /// 刷新动态聚组条件设置的消息ID
        /// </summary>
        public static readonly string DYNAMIC_GROUP_ACTIVE_MSG_ID = "519C6345-D625-4CC0-A5DA-DBAE1AB1EADE";

        private static readonly string DYNAMIC_GROUP_DIR = MB.Util.General.GeApplicationDirectory() + @"DynamicGroupSetting\";
        private static readonly string DYNAMIC_GROUP_SETTING_FULLNAME = DYNAMIC_GROUP_DIR + "{0}.xml";
        private static readonly string DYNAMIC_GROUP_PIVOT_GRID_LAYOUT = DYNAMIC_GROUP_DIR + "{0}_Pivot.xml";

        #region 关于保存默认聚组设定的方法
        /// <summary>
        /// 恢复Pivot设置
        /// </summary>
        public static void RestoreDynamicGroupSettingPivotLayout(MB.XWinLib.PivotGrid.PivotGridEx pivotGrid, IClientRuleQueryBase clientRuleObject) {
            string sectionName = getDynamicSettingSectionName(clientRuleObject);
            string fullFileName = string.Format(DYNAMIC_GROUP_PIVOT_GRID_LAYOUT, sectionName);
            if (File.Exists(fullFileName))
                pivotGrid.RestoreLayoutFromXml(fullFileName);
        }

        /// <summary>
        /// 恢复汇总列的信息
        /// </summary>
        public static DynamicGroupSetting GetDynamicGroupSetting(IClientRuleQueryBase clientRuleObject) {
            try {
                string sectionName = getDynamicSettingSectionName(clientRuleObject);
                string fullFileName = string.Format(DYNAMIC_GROUP_SETTING_FULLNAME, sectionName);
                if (File.Exists(fullFileName)) {
                    var settings = new MB.Util.Serializer.DataContractFileSerializer<DynamicGroupSetting>(fullFileName).Read();
                    return settings;
                }

                return null;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "根据UI Rule获取动态聚组配置出错");
            }
        }

        /// <summary>
        /// 恢复汇总列的信息
        /// </summary>
        public static List<DynamicGroupCondition> GetDynamicGroupSettingCondition(IClientRuleQueryBase clientRuleObject) {
            try {
                var settings = GetDynamicGroupSetting(clientRuleObject);
                if (settings != null) {
                    return ConvertDynamicGroupSettingToDataAreaConditions(settings);
                }
                return null;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "根据UI Rule获取动态聚组汇总条件出错");
            }
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        public static void SaveDynamicGroupSettings(MB.XWinLib.PivotGrid.PivotGridEx pivotGrid, IClientRuleQueryBase clientRuleObject, DynamicGroupSetting setting) {
            try {
                //保存列配置信息
                if (!Directory.Exists(DYNAMIC_GROUP_DIR)) Directory.CreateDirectory(DYNAMIC_GROUP_DIR);
                if (setting != null) {
                    //string sectionName = clientRuleObject.GetType().FullName + "~" + pivotGrid.Name;
                    string sectionName = getDynamicSettingSectionName(clientRuleObject);
                    string fullFileName = string.Format(DYNAMIC_GROUP_SETTING_FULLNAME, sectionName);
                    if (File.Exists(fullFileName))
                        File.Delete(fullFileName);
                    var serializer = new MB.Util.Serializer.DataContractFileSerializer<DynamicGroupSetting>(fullFileName);
                    serializer.Write(setting);

                    string pivotFullName = string.Format(DYNAMIC_GROUP_PIVOT_GRID_LAYOUT, sectionName);
                    if (File.Exists(pivotFullName))
                        File.Delete(pivotFullName);
                    pivotGrid.SaveLayoutToXml(pivotFullName);
                }
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "保存动态聚组设定失败");
            }
        }

        #endregion



        /// <summary>
        /// 把设置里面的DataArea转换成IU需要的数据源
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static List<DynamicGroupCondition> ConvertDynamicGroupSettingToDataAreaConditions(MB.Util.Model.DynamicGroupSetting dynamicGroupSetting) {
            List<DynamicGroupCondition> results = new List<DynamicGroupCondition>();
            if (dynamicGroupSetting != null && dynamicGroupSetting.DataAreaFields != null && dynamicGroupSetting.DataAreaFields.Count > 0) {
                foreach (KeyValuePair<string, List<MB.Util.Model.DataAreaField>> dataAreaList in dynamicGroupSetting.DataAreaFields) {
                    dataAreaList.Value.ForEach(data => {
                        DynamicGroupCondition dgCondition = new DynamicGroupCondition();
                        dgCondition.ENTITY_NAME = dataAreaList.Key;
                        dgCondition.ENTITY_DESCRIPTION = data.EntityDescription;
                        dgCondition.GROUP_NAME = data.Name;
                        dgCondition.GROUP_DESCRIPTION = data.Description;
                        dgCondition.GROUP_FIELD_TYPE = data.DataType;
                        dgCondition.SUMMARY_TYPE = data.SummaryItemType.ToString();
                        results.Add(dgCondition);
                    });
                }
            }
            return results;
        }

        private static string getDynamicSettingSectionName(IClientRuleQueryBase clientRuleObject) {
            string sectionName = clientRuleObject.GetType().FullName;
            return sectionName;
        }
    }


    

}
