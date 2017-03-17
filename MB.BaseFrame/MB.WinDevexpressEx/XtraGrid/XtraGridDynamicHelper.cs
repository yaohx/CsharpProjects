//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-08-10
// Description	:	XtraGridDynamicHelper  封装对XtraGrid的动态列加载操作处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.IFace;
using System.Collections;
using MB.XWinLib.Share;
using MB.WinBase.Common;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 动态列设置的帮助类
    /// </summary>
    public class XtraGridDynamicHelper {
        private static readonly string GRID_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"GridColumnSetting\";
        private static readonly string GRID_COLUMN_SETTING_FULLNAME = GRID_FILE_PATH + "GridDynamicColumnSetting.xml";

        #region Instance...
        private static Object _Obj = new object();
        private static XtraGridDynamicHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected XtraGridDynamicHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static XtraGridDynamicHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new XtraGridDynamicHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        public IList GetXtraGridDynamicColumns(IClientRuleQueryBase clientRuleObject) {
            try {
                var setting = GetXtraGridDynamicSettingInfo(clientRuleObject);
                if (setting == null) return null;
                string templateName = setting.Name;

                return DynamicColumnSettingHelper.GetDynamicColumnByTemplateName(clientRuleObject, templateName);
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write(string.Format("动态列加载失败，错误信息为：{0}", ex.Message));
                return null;
            }
        }

        public GridDynamicColumnSettingInfo GetXtraGridDynamicSettingInfo(IClientRuleQueryBase clientRuleObject) {
            try {
                IList list = DynamicColumnSettingHelper.GetDynamicColumnSettings(clientRuleObject);
                if (list == null || list.Count == 0) return null;

                List<GridDynamicColumnSettingInfo> settings = list as List<GridDynamicColumnSettingInfo>;
                var sort = settings.OrderByDescending(o => o.LastModifyDate).ToList();

                return sort[0];
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write(string.Format("获取动态列设置模板信息失败，错误信息为：{0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 得到列信息，用于加载与呈现UI Grid时使用
        /// </summary>
        /// <param name="clientRuleObject"></param>
        /// <returns></returns>
        public Dictionary<string, ColumnPropertyInfo> GetDynamicColumns(IClientRuleQueryBase clientRuleObject) {
            Dictionary<string, ColumnPropertyInfo> bindingPropertys = new Dictionary<string, ColumnPropertyInfo>();
            try {
                Dictionary<string, ColumnPropertyInfo> propertys = clientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns();
                if (clientRuleObject.CurrentQueryBehavior.Columns != null && clientRuleObject.CurrentQueryBehavior.Columns.Length > 0) {
                    string[] cols = clientRuleObject.CurrentQueryBehavior.Columns.Split(',');
                    foreach (string col in cols) {
                        if (propertys.Keys.Contains(col)) {
                            bindingPropertys.Add(col, propertys[col]);
                        }
                    }
                }
                else bindingPropertys = propertys;
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write(string.Format("执行方法 GetDynamicColumns 出错，获取动态列异常，错误信息为：{0}", ex.Message));
                bindingPropertys = clientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns();
            }

            return bindingPropertys;
        }

        /// <summary>
        /// 把动态列信息实现的列信息放入消息头
        /// UIRule需要设定主键以保证主键列必需被加载，用于打开某一个记录时使用
        /// </summary>
        /// <param name="clientRuleObject"></param>
        public void AppendQueryBehaviorColumns(IClientRuleQueryBase clientRuleObject) {
            try {
                if (clientRuleObject.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad) {
                    IList list = GetXtraGridDynamicColumns(clientRuleObject);
                    if (list != null && list.Count > 0) {
                        string[] keys = clientRuleObject.ClientLayoutAttribute.EntityKeys;
                        clientRuleObject.CurrentQueryBehavior.Columns = string.Join(",", keys);

                        List<DynamicColumnInfo> colList = list as List<DynamicColumnInfo>;
                        int colListLength = colList.Count;
                        for (int i = 0; i < colListLength - 1; i++) {
                            if (keys.Contains(colList[i].Name))
                                colList.RemoveAt(i);
                        }


                        string[] cols = (from a in colList select a.Name).ToArray();


                        clientRuleObject.CurrentQueryBehavior.Columns += "," + string.Join(",", cols);
                    }
                    else clientRuleObject.CurrentQueryBehavior.Columns = string.Empty;
                }
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write(string.Format("执行方法 AppendQueryBehaviorColumns 出错，添加动态列信息异常，错误信息为：{0}", ex.Message));
            }
        }
    }
}
