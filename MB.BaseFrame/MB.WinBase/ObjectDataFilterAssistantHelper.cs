using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.WinBase.Atts;
using MB.Util.Model;
using System.Collections;
using MB.WinBase.Binding;
namespace MB.WinBase
{
    public class ObjectDataFilterAssistantHelper
    {
        #region Instance...
        private static object _Object = new object();
        private static ObjectDataFilterAssistantHelper _Instance;

        protected ObjectDataFilterAssistantHelper() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static ObjectDataFilterAssistantHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new ObjectDataFilterAssistantHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        
        /// <summary>
        /// 创建数据获取帮助窗口。
        /// </summary>
        /// <param name="cfgInfo"></param>
        /// <param name="parentForm"></param>
        /// <returns></returns>
        public MB.WinBase.IFace.IDataAssistant CreateDataAssistantObject(object srcControl, ColumnEditCfgInfo cfgInfo,
            System.Windows.Forms.Control parentHoster) {
            MB.WinBase.IFace.IClientRuleQueryBase clientQueryRule = null;
            return CreateDataAssistantObject(srcControl, cfgInfo, parentHoster, ref clientQueryRule);
        }

        /// <summary>
        /// 创建数据获取帮助窗口。
        /// </summary>
        /// <param name="cfgInfo"></param>
        /// <param name="parentForm"></param>
        /// <param name="clientQueryRule"></param>
        /// <returns></returns>
        public MB.WinBase.IFace.IDataAssistant CreateDataAssistantObject(object srcControl, ColumnEditCfgInfo cfgInfo,
                                System.Windows.Forms.Control parentHoster,
                                 ref MB.WinBase.IFace.IClientRuleQueryBase clientQueryRule) {
            MB.WinBase.Common.ColumnEditCfgInfo columnEditCfgInfo = cfgInfo;
            if (columnEditCfgInfo == null) {
                throw new MB.Util.APPException("需要进行查询的对象ColumnEditCfg 没有进行进行相应的配置。", MB.Util.APPMessageType.DisplayToUser);

            }
            if (cfgInfo.CreateDataAssistant != null)
            {
                return cfgInfo.CreateDataAssistant(srcControl);
            }   //判断是否存在自定义弹出窗口的配置,如果存在就进行处理
            else if (columnEditCfgInfo.ClickButtonShowForm != null && !string.IsNullOrEmpty(columnEditCfgInfo.ClickButtonShowForm.Type))
            {
                //edit by aifang 2012-4-19 
                MB.WinBase.IFace.IInvokeDataAssistantHoster hoster = parentHoster as MB.WinBase.IFace.IInvokeDataAssistantHoster;
                if (hoster != null)
                {
                    MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs arg = new MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs(columnEditCfgInfo);
                    arg.ClientRule = clientQueryRule;
                    hoster.BeforeShowDataAssistant(srcControl, arg);
                    if (arg.Cancel)
                        return null;

                    clientQueryRule = arg.ClientRule;
                    columnEditCfgInfo = arg.ClumnEditCfgInfo;
                }
                //end

                string[] pars = null;
                if (!string.IsNullOrEmpty(columnEditCfgInfo.ClickButtonShowForm.TypeConstructParams))
                    pars = columnEditCfgInfo.ClickButtonShowForm.TypeConstructParams.Split(',');
                string[] frmSetting = columnEditCfgInfo.ClickButtonShowForm.Type.Split(',');
                object frm = MB.Util.DllFactory.Instance.LoadObject(frmSetting[0], pars, frmSetting[1]);
                if ((frm as Form) == null)
                    throw new MB.Util.APPException(" ColumnEditCfgInfo 的 ClickButtonShowForm 配置不是有效的一个窗口类", MB.Util.APPMessageType.SysErrInfo);
                if ((frm as MB.WinBase.IFace.IDataAssistant) == null)
                    throw new MB.Util.APPException(" ClickButtonInput 弹出的自定义窗口必须实现 MB.WinBase.IFace.IDataAssistant 接口", MB.Util.APPMessageType.SysErrInfo);

                return frm as MB.WinBase.IFace.IDataAssistant;
            }
            else
            {
                if (columnEditCfgInfo.InvokeDataSourceDesc == null)
                {
                    throw new MB.Util.APPException(" ColumnEditCfg 配置项的InvokeDataSourceDesc 没有进行进行相应的配置。", MB.Util.APPMessageType.SysErrInfo);
                }
                string[] descs = columnEditCfgInfo.InvokeDataSourceDesc.Type.Split(',');
                string[] conPars = null;
                if (!string.IsNullOrEmpty(cfgInfo.InvokeDataSourceDesc.TypeConstructParams))
                    conPars = cfgInfo.InvokeDataSourceDesc.TypeConstructParams.Split(',');

                if (clientQueryRule == null)
                    clientQueryRule = MB.Util.DllFactory.Instance.LoadObject(descs[0], conPars, descs[1]) as MB.WinBase.IFace.IClientRuleQueryBase;

                MB.Util.TraceEx.Assert(clientQueryRule != null, "调用UI层业务控制类配置有误！");

                //edit by chendc 2010-09-03 
                MB.WinBase.IFace.IInvokeDataAssistantHoster hoster = parentHoster as MB.WinBase.IFace.IInvokeDataAssistantHoster;
                if (hoster != null)
                {
                    MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs arg = new MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs(columnEditCfgInfo);
                    arg.ClientRule = clientQueryRule;
                    hoster.BeforeShowDataAssistant(srcControl, arg);
                    if (arg.Cancel)
                        return null;

                    clientQueryRule = arg.ClientRule;
                }

                string defaultQueryFilterForm = "MB.WinClientDefault.QueryFilter.FrmGetObjectDataAssistant,MB.WinClientDefault.Dll";
                string[] formSetting = defaultQueryFilterForm.Split(',');
                MB.WinBase.IFace.IGetObjectDataAssistant dataAssistant = MB.Util.DllFactory.Instance.LoadObject(formSetting[0], formSetting[1]) as MB.WinBase.IFace.IGetObjectDataAssistant;
                MB.Util.TraceEx.Assert(dataAssistant != null, "帮助查找窗口必须实现接口MB.WinBase.IFace.IGetObjectDataAssistant！");

                resetDataAssistant(clientQueryRule, columnEditCfgInfo,null, hoster, dataAssistant);

                return dataAssistant;
            }
        }

        private void resetDataAssistant(IClientRuleQueryBase clientQueryRule, ColumnEditCfgInfo columnEditCfgInfo, object currentEditObject,
            IInvokeDataAssistantHoster hoster, IGetObjectDataAssistant dataAssistant)
        {
            dataAssistant.HideFilterPane = columnEditCfgInfo.HideFilterPane;
            dataAssistant.FilterClientRule = clientQueryRule;
            dataAssistant.MultiSelect = columnEditCfgInfo.DefaultBatchAdd;
            dataAssistant.InvokeDataSourceDesc = columnEditCfgInfo.InvokeDataSourceDesc;
            dataAssistant.QueryObject = null;
            dataAssistant.ClumnEditCfgInfo = columnEditCfgInfo;
            dataAssistant.InvokeParentControl = this;

            dataAssistant.CurrentEditObject =  currentEditObject;

            if (hoster != null)
                dataAssistant.InvokeFilterParentFormHoster = hoster;
        }

        /// <summary>
        /// 创建数据获取帮助窗口。
        /// </summary>
        /// <param name="cfgInfo"></param>
        /// <param name="parentForm"></param>
        /// <param name="clientQueryRule"></param>
        /// <returns></returns>
        public MB.WinBase.IFace.IDataAssistant CreateDataAssistantObject(object srcControl,object currentEditObject,
            ColumnEditCfgInfo cfgInfo,MB.WinBase.IFace.IClientRuleQueryBase clientRule)
        {
            MB.WinBase.Common.ColumnEditCfgInfo columnEditCfgInfo = cfgInfo;
            MB.WinBase.IFace.IClientRuleQueryBase clientQueryRule = null;
            if (columnEditCfgInfo == null)
            {
                throw new MB.Util.APPException("需要进行查询的对象ColumnEditCfg 没有进行进行相应的配置。", MB.Util.APPMessageType.DisplayToUser);

            }
            if (cfgInfo.CreateDataAssistant != null)
            {
                return cfgInfo.CreateDataAssistant(srcControl);
            }   //判断是否存在自定义弹出窗口的配置,如果存在就进行处理
            else if (columnEditCfgInfo.ClickButtonShowForm != null && !string.IsNullOrEmpty(columnEditCfgInfo.ClickButtonShowForm.Type))
            {
                //edit by aifang 2012-4-19 
                MB.WinBase.IFace.IInvokeDataAssistantHoster hoster = clientRule as MB.WinBase.IFace.IInvokeDataAssistantHoster;
                if (hoster != null)
                {
                    MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs arg = new MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs(columnEditCfgInfo);
                    arg.ClientRule = clientQueryRule;
                    hoster.BeforeShowDataAssistant(srcControl, arg);
                    if (arg.Cancel)
                        return null;

                    clientQueryRule = arg.ClientRule;
                    columnEditCfgInfo = arg.ClumnEditCfgInfo;
                }
                //end

                string[] pars = null;
                if (!string.IsNullOrEmpty(columnEditCfgInfo.ClickButtonShowForm.TypeConstructParams))
                    pars = columnEditCfgInfo.ClickButtonShowForm.TypeConstructParams.Split(',');
                string[] frmSetting = columnEditCfgInfo.ClickButtonShowForm.Type.Split(',');
                object frm = MB.Util.DllFactory.Instance.LoadObject(frmSetting[0], pars, frmSetting[1]);
                if ((frm as Form) == null)
                    throw new MB.Util.APPException(" ColumnEditCfgInfo 的 ClickButtonShowForm 配置不是有效的一个窗口类", MB.Util.APPMessageType.SysErrInfo);
                if ((frm as MB.WinBase.IFace.IDataAssistant) == null)
                    throw new MB.Util.APPException(" ClickButtonInput 弹出的自定义窗口必须实现 MB.WinBase.IFace.IDataAssistant 接口", MB.Util.APPMessageType.SysErrInfo);

                return frm as MB.WinBase.IFace.IDataAssistant;
            }
            else
            {
                if (columnEditCfgInfo.InvokeDataSourceDesc == null)
                {
                    throw new MB.Util.APPException(" ColumnEditCfg 配置项的InvokeDataSourceDesc 没有进行进行相应的配置。", MB.Util.APPMessageType.SysErrInfo);
                }
                string[] descs = columnEditCfgInfo.InvokeDataSourceDesc.Type.Split(',');
                string[] conPars = null;
                if (!string.IsNullOrEmpty(cfgInfo.InvokeDataSourceDesc.TypeConstructParams))
                    conPars = cfgInfo.InvokeDataSourceDesc.TypeConstructParams.Split(',');

                if (clientQueryRule == null) 
                    clientQueryRule = MB.Util.DllFactory.Instance.LoadObject(descs[0], conPars, descs[1]) as MB.WinBase.IFace.IClientRuleQueryBase;

                MB.Util.TraceEx.Assert(clientQueryRule != null, "调用UI层业务控制类配置有误！");

                //edit by chendc 2010-09-03 
                MB.WinBase.IFace.IInvokeDataAssistantHoster hoster = clientRule as MB.WinBase.IFace.IInvokeDataAssistantHoster;
                if (hoster != null)
                {
                    MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs arg = new MB.WinBase.IFace.InvokeDataAssistantHosterEventArgs(columnEditCfgInfo);
                    arg.CurrentEditObject = currentEditObject;
                    arg.ClientRule = clientQueryRule;
                    hoster.BeforeShowDataAssistant(srcControl, arg);
                    if (arg.Cancel)
                        return null;

                    clientQueryRule = arg.ClientRule;
                }

                string defaultQueryFilterForm = "MB.WinClientDefault.QueryFilter.FrmGetObjectDataAssistant,MB.WinClientDefault.Dll";
                string[] formSetting = defaultQueryFilterForm.Split(',');
                MB.WinBase.IFace.IGetObjectDataAssistant dataAssistant = MB.Util.DllFactory.Instance.LoadObject(formSetting[0], formSetting[1]) as MB.WinBase.IFace.IGetObjectDataAssistant;
                MB.Util.TraceEx.Assert(dataAssistant != null, "帮助查找窗口必须实现接口MB.WinBase.IFace.IGetObjectDataAssistant！");

                resetDataAssistant(clientQueryRule, columnEditCfgInfo,currentEditObject, hoster, dataAssistant);

                return dataAssistant;
            }
        }

        public void ShowProperty(MB.WinBase.Common.ColumnEditCfgInfo columnEditCfgInfo,string columnName,object value)
        {
            try
            {
                if (columnEditCfgInfo.InvokeDataSourceDesc == null)
                {
                    throw new MB.Util.APPException(" ColumnEditCfg 配置项的InvokeDataSourceDesc 没有进行进行相应的配置。", MB.Util.APPMessageType.SysErrInfo);
                }
                string type = columnEditCfgInfo.InvokeDataSourceDesc.Type;
                string method = columnEditCfgInfo.InvokeDataSourceDesc.Method;
                if (columnEditCfgInfo.InvokeDataPropertyDesc != null)
                {
                    type = columnEditCfgInfo.InvokeDataPropertyDesc.Type;
                    method = columnEditCfgInfo.InvokeDataPropertyDesc.Method;
                }

                string[] descs = type.Split(',');
                string[] conPars = null;
                if (!string.IsNullOrEmpty(columnEditCfgInfo.InvokeDataSourceDesc.TypeConstructParams))
                    conPars = columnEditCfgInfo.InvokeDataSourceDesc.TypeConstructParams.Split(',');

                var clientQueryRule = MB.Util.DllFactory.Instance.LoadObject(descs[0], conPars, descs[1]);

                ModuleInvokeConfigAttribute[] atts = AttributeConfigHelper.Instance.GetModuleInvokeConfig(clientQueryRule.GetType());
                var configAtt = atts.Where(o => o.OptType == UICommandType.AddNew).FirstOrDefault();
                if (configAtt == null) throw new MB.Util.APPException("ModuleInvokeConfigAttribute属性未配置  UICommandType.AddNew 类型值", Util.APPMessageType.DisplayToUser);
                string fullName = configAtt.UIViewType.FullName;

                //获取数据
                List<MB.Util.Model.QueryParameterInfo> filterParams = new List<QueryParameterInfo>();
                filterParams.Add(new QueryParameterInfo(columnName, value, Util.DataFilterConditions.Equal));

                ArrayList pars = new ArrayList();
                string[] methods = method.Split(',');
                if (methods.Length > 1)
                {
                    for (int i = 1; i < methods.Length; i++)
                    {
                        pars.Add(methods[i]);
                    }
                }

                pars.Add(filterParams.ToArray());

                object data = MB.Util.MyReflection.Instance.InvokeMethod(clientQueryRule, methods[0], pars.ToArray());
                if (data == null) throw new MB.Util.APPException("获取数据为空!", Util.APPMessageType.DisplayToUser);
                IList list = data as IList;


                BindingSourceEx bindingSource = new BindingSourceEx();
                bindingSource.DataSource = list[0];
                object[] objPars = new object[3];
                objPars[0] = clientQueryRule;
                objPars[1] = ObjectEditType.OpenReadOnly;
                objPars[2] = bindingSource;
                var objFrm = MB.Util.DllFactory.Instance.LoadObject(fullName, objPars, descs[1]);
                Form frm = (Form)objFrm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
    }
}
