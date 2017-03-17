using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;
using MB.WinBase.IFace;
using System.IO;

namespace MB.XWinLib.Share
{
    public class DynamicColumnSettingHelper
    {
        private static readonly string GRID_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"GridColumnSetting\";
        private static readonly string GRID_COLUMN_SETTING_FULLNAME = GRID_FILE_PATH + "GridDynamicColumnSetting.xml";

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="clientRuleObject"></param>
        /// <returns></returns>
        public static IList GetDynamicColumnSettings(IClientRuleQueryBase clientRuleObject)
        {
            try
            {
                var gridColumnSettings = new MB.Util.Serializer.DataContractFileSerializer<List<GridColumnSettingInfo>>(GRID_COLUMN_SETTING_FULLNAME).Read();
                if (gridColumnSettings == null) return null;

                string sectionName = clientRuleObject.GetType().FullName + " " + clientRuleObject.ClientLayoutAttribute.UIXmlConfigFile;
                var dynamicColumnSettings = gridColumnSettings.Find(o => o.Name.Equals(sectionName));
                if (dynamicColumnSettings == null || dynamicColumnSettings.DynamicColumns.Count == 0) return null;

                return dynamicColumnSettings.DynamicColumns;
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(ex.Message, Util.APPMessageType.SysErrInfo);
                return null;
            }
        }

        /// <summary>
        /// 保存列配置信息
        /// </summary>
        /// <param name="clientRuleObject"></param>
        /// <param name="templateName"></param>
        /// <param name="cols"></param>
        /// <param name="settings"></param>
        public static void SaveDynamicColumnSettings(IClientRuleQueryBase clientRuleObject,string templateName,List<DynamicColumnInfo> cols,List<GridColumnSettingInfo> settings)
        {
            try
            {
                //保存列配置信息
                if (!Directory.Exists(GRID_FILE_PATH)) Directory.CreateDirectory(GRID_FILE_PATH);

                if (!string.IsNullOrEmpty(templateName))
                {
                    string sectionName = clientRuleObject.GetType().FullName + " " + clientRuleObject.ClientLayoutAttribute.UIXmlConfigFile + " " + templateName + ".xml";
                    var dynamicColumn = new MB.Util.Serializer.DataContractFileSerializer<List<DynamicColumnInfo>>(GRID_FILE_PATH + sectionName);
                    dynamicColumn.Write(cols);
                }

                //保存主配置信息
                var gridSetting = new MB.Util.Serializer.DataContractFileSerializer<List<GridColumnSettingInfo>>(GRID_COLUMN_SETTING_FULLNAME);
                gridSetting.Write(settings);
            }
            catch (Exception ex)
            {
                throw new MB.Util.APPException(ex.Message, Util.APPMessageType.SysErrInfo);
            }
        }

        /// <summary>
        /// 删除动态列设置信息
        /// </summary>
        /// <param name="clientRuleObject"></param>
        /// <param name="templateName"></param>
        public static void DeleteDynamicColumnSetting(IClientRuleQueryBase clientRuleObject, string templateName)
        {
            try
            {
                string sectionName = clientRuleObject.GetType().FullName + " " + clientRuleObject.ClientLayoutAttribute.UIXmlConfigFile + " " + templateName + ".xml";
                if (File.Exists(GRID_FILE_PATH + sectionName))
                {
                    File.Delete(GRID_FILE_PATH + sectionName);
                }
            }
            catch (Exception ex)
            {
                throw new MB.Util.APPException(ex.Message, Util.APPMessageType.SysErrInfo);
            }
        }

        /// <summary>
        /// 根据模板名称获取列配置信息
        /// </summary>
        /// <param name="clientRuleObject"></param>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public static List<DynamicColumnInfo> GetDynamicColumnByTemplateName(IClientRuleQueryBase clientRuleObject, string templateName)
        {
            try
            {                
                string sectionName = clientRuleObject.GetType().FullName + " " + clientRuleObject.ClientLayoutAttribute.UIXmlConfigFile + " " + templateName + ".xml";
                var dynamicColumn = new MB.Util.Serializer.DataContractFileSerializer<List<DynamicColumnInfo>>(GRID_FILE_PATH + sectionName);
                return dynamicColumn.Read();
            }
            catch (Exception ex)
            {
                throw new MB.Util.APPException(ex.Message, Util.APPMessageType.SysErrInfo);
            }
        }
    }

    [DataContract]
    public class GridColumnSettingInfo
    {
        private string _Name;
        private List<GridDynamicColumnSettingInfo> _DynamicColumns;

        [DataMember]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        [DataMember]
        public List<GridDynamicColumnSettingInfo> DynamicColumns
        {
            get { return _DynamicColumns; }
            set { _DynamicColumns = value; }
        }
    }


    [DataContract]
    public class GridDynamicColumnSettingInfo
    {
        private string _Name;
        private DateTime _LastModifyDate;

        [DataMember]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [DataMember]
        public DateTime LastModifyDate {
            get { return _LastModifyDate; }
            set { _LastModifyDate = value; }
        }
    }

    [DataContract]
    public class DynamicColumnInfo
    {
        private string _IsSelected;
        [DataMember]
        public string IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; }
        }

        private string _Name;
        [DataMember]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Description;
        [DataMember]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
