//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	征对单据的数据导入处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

using MB.WinClientDefault.Extender;
using MB.Util;
namespace MB.WinClientDefault.DataImport {
    /// <summary>
    /// 征对单据的数据导入处理。
    /// </summary>
    public class DocDataImportHelper {
        private MB.WinBase.IFace.IViewGridForm _ViewGridForm;
        private MB.WinBase.IFace.IClientRule _ClientRule;
        private MB.WinBase.Binding.BindingSourceEx _BindingSource;

        /// <summary>
        /// 显示数据导入对话框。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="clientRule"></param>
        /// <param name="bindingSource"></param>
        public void ShowDataImportDialog(MB.WinBase.IFace.IViewGridForm viewGridForm,
                                        MB.WinBase.Binding.BindingSourceEx bindingSource) {
            _ViewGridForm = viewGridForm;
            _ClientRule = viewGridForm.ClientRuleObject as MB.WinBase.IFace.IClientRule;
            _BindingSource = bindingSource;

            IDocDataImportProvider importProvider = _ClientRule as IDocDataImportProvider;
            if (importProvider == null) {

                MB.Util.TraceEx.Write(string.Format("业务类 {0} 还没有实现相应的数据导入接口 {1}",
                                              _ClientRule.GetType().FullName, "IDocDataImportProvider"), MB.Util.APPMessageType.SysErrInfo);
                throw new MB.Util.APPException("当前模块尚未提供数据导入的功能", APPMessageType.DisplayToUser);
            }
            var importInfo = DefaultDataImportDialog.ShowDataImport(_ViewGridForm as Form, _ClientRule, _ClientRule.ClientLayoutAttribute.UIXmlConfigFile, true);
            if (importInfo == null) return;

            IList hasImportEntity;
            bool b = importProvider.DataImport(_ViewGridForm, importInfo, out hasImportEntity);
            if (!b) return;

            DialogResult re = MB.WinBase.MessageBoxEx.Question("数据导入成功,是否需要从数据库中重新刷新数据?");
            if (re == DialogResult.Yes) {
                _ViewGridForm.Refresh();
            }
            else {
                if (hasImportEntity == null && hasImportEntity.Count == 0) return;

                foreach (object entity in hasImportEntity) {
                    _BindingSource.Add(entity);
                }

            }
        }
       /// <summary>
       ///  数据导入处理相关
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <typeparam name="TChannel"></typeparam>
       /// <param name="viewForm"></param>
       /// <param name="importDataInfo"></param>
       /// <param name="hasImportData"></param>
       /// <returns></returns>
        public static bool DataImport<T, TChannel>(MB.WinBase.IFace.IViewGridForm viewForm,
                                int mainDocDataType, MB.WinClientDefault.DataImport.DataImportInfo importDataInfo,
                                out System.Collections.IList hasImportData) where TChannel : class, IDisposable {
            DataRow[] drs = importDataInfo.ImportData.Tables[0].Select();
            MB.WinBase.UIDataInputValidated dataValidated = new MB.WinBase.UIDataInputValidated(); 
            List<T> lstData = new List<T>();
            Type entityType = typeof(T);
            string[] logicKeys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetLogicKeys(viewForm.ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
            var colPropertys = viewForm.ClientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns();
            string errMsg = string.Empty;
            using (TChannel client = MB.WcfClient.WcfClientFactory.CreateWcfClient<TChannel>(viewForm.ClientRuleObject)) {//   MB.Util.MyNetworkCredential.CreateWcfClientWithCredential<TChannel>()) {
                object val = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(client, "GetCreateNewEntityIds", mainDocDataType, drs.Length);
                int id = MB.Util.MyConvert.Instance.ToInt(val); 
                foreach (DataRow dr in drs) {
                    //在导入之前先进行数据的检验
                    bool check = dataValidated.DataRowValidated(colPropertys, dr, ref errMsg);
                    if(!check)
                        throw new MB.Util.APPException(errMsg, MB.Util.APPMessageType.DisplayToUser);
                    T newInfo = (T)MB.Util.DllFactory.Instance.CreateInstance(entityType);

                    MB.Util.MyReflection.Instance.FillModelObject(newInfo, dr);

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(newInfo,MB.BaseFrame.SOD.OBJECT_PROPERTY_ID, id++);
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(newInfo,MB.WinBase.UIDataEditHelper.ENTITY_STATE_PROPERTY, MB.Util.Model.EntityState.New);

                    //导入时，需要进行逻辑主键的检验, （特别说明： 如果性能上存在问题，可以考虑批量检验的方式）
                    if (logicKeys != null && logicKeys.Length > 0) {
                        object[] checkPars = new object[] { mainDocDataType, newInfo, logicKeys };
                        bool b = (bool)MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(client, "CheckValueIsExists", checkPars);
                        if (b) {
                            string desc = MB.WinBase.ShareLib.Instance.GetPropertyDescription(logicKeys, colPropertys);
                            string valDesc = MB.WinBase.ShareLib.Instance.GetMultiEntityValueDescription(logicKeys, newInfo);
                            string msg = "需要存储的对象属性(" + desc + ") 的值(" + valDesc + ") 在库中已经存在,本次导入不成功";
                            throw new MB.Util.APPException(msg, MB.Util.APPMessageType.DisplayToUser);
                        }
                    }

                    lstData.Add(newInfo);

                    object[] pars = new object[] { mainDocDataType, newInfo, false, null };
                    object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(client, "AddToCache", pars);
                }
                MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(client, "Flush");
            }

            hasImportData = lstData;
            return true;
        }

    
    }
}
