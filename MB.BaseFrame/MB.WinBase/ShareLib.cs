//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	UI 层操作相关的Public 函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Xml;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using MB.WinBase.Common;
namespace MB.WinBase {
    /// <summary>
    /// UI 层操作相关的Public 函数。
    /// </summary>
    public class ShareLib {
        public static string XML_FILE_PATH = null;

        #region Instance...
        private static Object _Obj = new object();
        private static ShareLib _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected ShareLib() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static ShareLib Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new ShareLib();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        static ShareLib() {
            XML_FILE_PATH = MB.Util.General.GeApplicationDirectory() +
                                             ConfigurationSettings.AppSettings["XmlConfigPath"].ToString();
        }

        /// <summary>
        /// 获取多个字段的描述。
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="colPropertys"></param>
        /// <returns></returns>
        public string GetPropertyDescription(string[] propertyName,Dictionary<string,MB.WinBase.Common.ColumnPropertyInfo> colPropertys  ){
            List<string> desc = new List<string>();
            foreach (string name in propertyName) {
                if (colPropertys.ContainsKey(name) )
                    desc.Add(colPropertys[name].Description);
                else
                    desc.Add(name);
            }
            return string.Join(",", desc.ToArray()); 
        }
       /// <summary>
       /// 获取实体对象中多个值做为描述。
       /// </summary>
       /// <param name="propertyName"></param>
       /// <param name="entity"></param>
       /// <returns></returns>
        public string GetMultiEntityValueDescription(string[] propertyName, object entity) {
            List<string> desc = new List<string>();
            foreach (string name in propertyName) {
                if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, name)) {
                    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, name);
                    desc.Add(val.ToString());
                }
            }
            return string.Join(",", desc.ToArray());
        }

        #region 界面对象处理相关...
        /// <summary>
        /// 获取控件所在的父窗口。
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public System.Windows.Forms.Form GetControlParentForm(System.Windows.Forms.Control ctl) {
            if (ctl == null)
                return null;
            System.Windows.Forms.Form frm = ctl as System.Windows.Forms.Form;
            if (frm != null)
                return frm;
            System.Windows.Forms.ContainerControl conCtl = ctl as System.Windows.Forms.ContainerControl;
            if (conCtl != null)
                return conCtl.ParentForm;
            else
                return GetControlParentForm(ctl.Parent);
        }
        /// <summary>
        ///   获取调用的 InvokeDataAssistantHoster
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public System.Windows.Forms.Control GetInvokeDataHosterControl(System.Windows.Forms.Control ctl) {
            if (ctl == null)
                return null;
            MB.WinBase.IFace.IInvokeDataAssistantHoster hoster = ctl as MB.WinBase.IFace.IInvokeDataAssistantHoster;
            if (hoster != null)
                return ctl;
            else
                return GetInvokeDataHosterControl(ctl.Parent);
        }
        /// <summary>
        /// 显示焦点控件。
        /// </summary>
        /// <param name="ctl"></param>
        public void ShowFocusControl(System.Windows.Forms.Control ctl) {
            Control parent = getParentControl(ctl.Parent, "TabPage");
            if (parent != null) {
                //目前只处理一个TabControl 的情况，如果存在 多个情况 还需要进一步处理。
                TabPage tPage = parent as TabPage;
                (tPage.Parent as TabControl).SelectedTab = tPage;
                return;
            }
            parent = getParentControl(ctl.Parent, "XtraTabPage");
            if (parent != null) {
                //目前只处理一个TabControl 的情况，如果存在 多个情况 还需要进一步处理。
                DevExpress.XtraTab.XtraTabPage xPage = parent as DevExpress.XtraTab.XtraTabPage;
                (xPage.Parent as DevExpress.XtraTab.XtraTabControl).SelectedTabPage = xPage;

                return;
            }
        }
        //根据指定的控件 父控件名称找到该控件
        private Control getParentControl(System.Windows.Forms.Control parent, string parentCtlTypeName) {
            if (parent == null)
                return null;
            if (string.Compare(parent.GetType().Name, parentCtlTypeName, true) == 0)
                return parent;

            System.Windows.Forms.ContainerControl conCtl = parent as System.Windows.Forms.ContainerControl;
            if (conCtl != null)
                return conCtl.ParentForm;
            else
                return getParentControl(parent.Parent, parentCtlTypeName);
        }
        #endregion 界面对象处理相关...

        #region 文件选择处理...
        /// <summary>
        /// 文件选择处理
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string SelectedFile(string filter) {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog(); ;
            fileDialog.Filter = filter;
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                return fileDialog.FileName;
            }
            return null;
        }
        #endregion 文件选择处理...

        #region 应用程序初始化处理相关...
        /// <summary>
        /// 执行Windows 应用程序初始化。
        /// </summary>
        public void IniMBBaseFrameForWindows() {
            //创建UserSetting 目录
            string iuserSettingPath = MB.Util.General.GeApplicationDirectory() + "UserSetting";
            if (!System.IO.Directory.Exists(iuserSettingPath))
                System.IO.Directory.CreateDirectory(iuserSettingPath); 
            //清空临时目录(temp) 下的数据

            //判断是否安装基本需要的数据


        }
        #endregion 应用程序初始化处理相关...

        #region XML 配置文件处理相关...
        /// <summary>
        /// 根据XML 文件名称构建完整的XML 配置文件完整路径。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public string BuildXmlConfigFileFullName(string xmlFileName) {
            if(string.IsNullOrEmpty(XML_FILE_PATH))
                throw new MB.Util.APPException("需要在App.Config 中配置 XmlConfigPath 配置项,请检查！");

            if (XML_FILE_PATH.IndexOf(":") < 0) {
                XML_FILE_PATH = MB.Util.General.GeApplicationDirectory() +
                                System.Configuration.ConfigurationManager.AppSettings["XmlConfigPath"];
            }
            if (xmlFileName.IndexOf(".") < 0) {
                xmlFileName += ".xml";
            }
            string fileFullName = XML_FILE_PATH + xmlFileName;
            return fileFullName;
        }
        /// <summary>
        /// 加载XML 配置文档。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public XmlDocument LoadXmlConfigFile(string xmlFileName) {
            string fileFullName = BuildXmlConfigFileFullName(xmlFileName);
            XmlDocument xmlDoc = new XmlDocument();
            bool exist = System.IO.File.Exists(fileFullName);
            try {
                if (exist) {
                    xmlDoc.Load(fileFullName);
                }
                else {
                    throw new MB.Util.APPException(string.Format("表结构的配置文件找不到!,具体是文件{0}在目录:{1} 中找不到.", xmlFileName, XML_FILE_PATH));
                }
            }
            catch (Exception e) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(e,string.Format("加载XML 配置文件:{0} 出错",xmlFileName));
            }
            return xmlDoc;
        }
        #endregion XML 配置文件处理相关...

        #region 数据转换处理相关...
        /// <summary>
        /// 把数据实体集合类转换为 客户可分析DataSet 的格式。
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="entitys"></param>
        /// <param name="convertPropertysName">需要转换的属性名称 (为空 将转换所有可读的类型)</param>
        /// <returns></returns>
        public DataSet ConvertEntityToDataSet(Type entityType, System.Collections.IList entitys, string[] convertPropertysName) {
            var pros = entityType.GetProperties();
            DataTable dtData = new DataTable();
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> entityExistsPros = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (var p in pros) {
                if (p.IsSpecialName || !p.CanRead) continue;

                if (convertPropertysName != null && convertPropertysName.Length > 0 && !convertPropertysName.Contains(p.Name)) continue;

                Type t = MB.Util.General.CreateSystemType(p.PropertyType.FullName, true);
                if (t == null) throw new MB.Util.APPException(string.Format("根据类型名称 {0} 获取类型出错", p.PropertyType.FullName),
                                                                Util.APPMessageType.SysErrInfo);
                dtData.Columns.Add(p.Name, t);
                entityExistsPros.Add(p.Name, new Util.Emit.DynamicPropertyAccessor(entityType, p));
            }
            foreach (object entity in entitys) {
                DataRow drNew = dtData.NewRow();
                dtData.Rows.Add(drNew);
                foreach (string columnName in entityExistsPros.Keys) {
                    object val = entityExistsPros[columnName].Get(entity);// MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, columnName);
                    if (val == null || val == System.DBNull.Value) continue;
                    drNew[columnName] = MB.Util.MyConvert.Instance.ChangeType(val, dtData.Columns[columnName].DataType);
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtData);
            return ds;
        }
        /// <summary>
        ///  把数据实体集合类转换为 客户可分析DataSet 的格式。
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="propertys"></param>
        /// <param name="columnsEdit"></param>
        /// <returns></returns>
        public DataSet ConvertEntityToDataSet(System.Collections.IList entitys, Dictionary<string, ColumnPropertyInfo> propertys,
            Dictionary<string, ColumnEditCfgInfo> columnsEdit) {
            return ConvertEntityToDataSet(entitys, propertys, columnsEdit, false);
        }
        /// <summary>
        ///  把数据实体集合类转换为 客户可分析DataSet 的格式。
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="propertys"></param>
        /// <param name="columnsEdit"></param>
        /// <returns></returns>
        public DataSet ConvertEntityToDataSet(System.Collections.IList entitys, Dictionary<string, ColumnPropertyInfo> propertys, 
                            Dictionary<string, ColumnEditCfgInfo> columnsEdit,bool isForChart) {
            if (entitys == null || propertys == null) return null;

           try {
            DataTable dtData = new DataTable();
            List<ColumnPropertyInfo> cols = new List<ColumnPropertyInfo>();
            foreach (ColumnPropertyInfo info in propertys.Values) {
                if (isForChart) {
                    if (string.IsNullOrEmpty(info.ChartDataType)) continue; 
                }

                string dataType = info.DataType;
                if (columnsEdit!=null && columnsEdit.ContainsKey(info.Name))
                    dataType = "System.String";

                cols.Add(info);
                dtData.Columns.Add(info.Name, MB.Util.General.CreateSystemType(dataType,false)); 
            }
            DataSet dsData = new DataSet();
            dsData.Tables.Add(dtData);

            if (entitys.Count == 0 || cols.Count == 0) {
                return dsData;
            }
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> entityExistsPros = new Dictionary<string,MB.Util.Emit.DynamicPropertyAccessor>();
            object tempEntity = entitys[0];
            foreach (ColumnPropertyInfo info in cols) {
                if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(tempEntity, info.Name)) {
                    if (entityExistsPros.ContainsKey(info.Name)) continue;
  
                    MB.Util.Emit.DynamicPropertyAccessor propertyAccess = new MB.Util.Emit.DynamicPropertyAccessor(tempEntity.GetType(), info.Name);
                    entityExistsPros.Add(info.Name,propertyAccess);

                   // entityExistsPros.Add(info.Name);
                }
            }

            Dictionary<string, Dictionary<string, string>> lookUpDataSource = new Dictionary<string, Dictionary<string, string>>();
            foreach (object entity in entitys) {
                DataRow drNew = dtData.NewRow();
                dtData.Rows.Add(drNew);
                foreach (string columnName in entityExistsPros.Keys) {
                    object val = entityExistsPros[columnName].Get(entity);// MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, columnName);
                    if (val == null || val == System.DBNull.Value) continue;

                    if (columnsEdit != null && columnsEdit.ContainsKey(columnName)) {
                        drNew[columnName] = convertValueToDescription(val, columnsEdit[columnName], lookUpDataSource);
                    }
                    else {
                        drNew[columnName] = MB.Util.MyConvert.Instance.ChangeType(val, dtData.Columns[columnName].DataType);
                    }
                
                }
            }
            return dsData;
           }
           catch (Exception ex) {
               throw new MB.Util.APPException("执行 ConvertEntityToDataSet 出错" + ex.Message);
           }
        }
        /// <summary>
        /// 把数据集合   客户可分析DataSet 的格式。 (主要把存在 ID 转换为文本描述的格式)
        /// </summary>
        /// <param name="dvData"></param>
        /// <param name="propertys"></param>
        /// <param name="columnsEdit"></param>
        /// <returns></returns>
        public DataSet ConvertDataSetToQaDataSet(DataView dvData, Dictionary<string, ColumnPropertyInfo> propertys, Dictionary<string, ColumnEditCfgInfo> columnsEdit) {
            if (dvData == null || propertys == null) return null;

            try {
                DataTable dtData = new DataTable();

                foreach (ColumnPropertyInfo info in propertys.Values) {
                    string dataType = info.DataType;
                    if (columnsEdit != null && columnsEdit.ContainsKey(info.Name))
                        dataType = "System.String";

                    dtData.Columns.Add(info.Name, MB.Util.General.CreateSystemType(dataType,false));
                }
                int count = dvData.Count;
                Dictionary<string, Dictionary<string, string>> lookUpDataSource = new Dictionary<string, Dictionary<string, string>>();

                for (int i = 0; i < count; i++) {
                    DataRow drNew = dtData.NewRow();
                    dtData.Rows.Add(drNew);
                    foreach (ColumnPropertyInfo info in propertys.Values) {
                        if (!dvData.Table.Columns.Contains(info.Name)) continue;

                        object val = dvData[i][info.Name];
                        if (val == null || val == System.DBNull.Value) continue;

                        if (columnsEdit != null && columnsEdit.ContainsKey(info.Name)) {
                            //edit by aifang 2012-5-16 处理异步查询clickButtonInput类型字段值为空（组织编码为空） begin
                            if (columnsEdit[info.Name].DataSource == null)
                                drNew[info.Name] = val.ToString();
                            else drNew[info.Name] = convertValueToDescription(val, columnsEdit[info.Name], lookUpDataSource);
                            //drNew[info.Name] = convertValueToDescription(val, columnsEdit[info.Name], lookUpDataSource);
                            //end
                        }
                        else {
                            drNew[info.Name] = MB.Util.MyConvert.Instance.ChangeType(val, dtData.Columns[info.Name].DataType);
                        }

                    }
                }

                DataSet dsData = new DataSet();
                dsData.Tables.Add(dtData);
                return dsData;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("执行 ConvertDataSetToQaDataSet 出错 " + ex.Message);
            }
        }
        #endregion 数据转换处理相关...

        /// <summary>
        /// 从资源中创建一个位图。
        /// </summary>
        /// <param name="assembly">资源文件所在的配件。</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Bitmap CreateBitmapFromResources( System.Reflection.Assembly assembly ,string fileName) {
            System.Reflection.Assembly asm = assembly;
            System.IO.Stream stream = asm.GetManifestResourceStream(fileName);
            if (stream == null)
                throw new MB.Util.APPException(string.Format("文件名:{0} 在资源{1}中找不到,请确认是否已经添加该文件作为嵌入资源?", fileName, asm.FullName));

            Bitmap image = (Bitmap)Bitmap.FromStream(stream);
            return image;
        }
        /// <summary>
        /// 从资源文件中创建ImageList
        /// </summary>
        /// <param name="assembly">资源文件所在的配件</param>
        /// <param name="files">包含完整命名空间的文件名称。</param>
        /// <returns></returns>
        public ImageList CreateImageListFromResources(System.Reflection.Assembly assembly, params string[] files) {
            ImageList imgs = new ImageList();
            foreach (string fName in files) {
                var  img =  CreateBitmapFromResources(assembly, fName);
                imgs.Images.Add(img); 
            }
            return imgs;
        }

        #region 内部函数处理...
        //把值转换为描述。
        private string convertValueToDescription(object val, ColumnEditCfgInfo lookUpCol,Dictionary<string,Dictionary<string,string>> lookUpDataSource ) {
            if (val == null || lookUpCol.DataSource == null) return string.Empty;

            if (!lookUpDataSource.ContainsKey(lookUpCol.Name)) {
                Dictionary<string, string> hasData = new Dictionary<string, string>();
                DataTable dt = MB.Util.MyConvert.Instance.ToDataTable(lookUpCol.DataSource, string.Empty);
                DataRow[] drs = dt.Select();
                foreach (DataRow dr in drs) {
                    if (dr[lookUpCol.ValueFieldName] == null ||  hasData.ContainsKey(dr[lookUpCol.ValueFieldName].ToString())) continue;
 
                    hasData.Add(dr[lookUpCol.ValueFieldName].ToString(), dr[lookUpCol.TextFieldName].ToString());
                }
                //if (dt != null) {
                //    DataRow[] drs = dt.Select(string.Format("{0}='{1}'", lookUpCol.ValueFieldName, val.ToString()));
                //    if (drs.Length > 0) {
                //        return drs[0][lookUpCol.TextFieldName].ToString();
                //    }
                //}
                lookUpDataSource.Add(lookUpCol.Name, hasData);
            }
            var temp = lookUpDataSource[lookUpCol.Name];
            if(temp.ContainsKey(val.ToString()))
                return temp[val.ToString()];
            else 
            return val.ToString();
            }
        #endregion 内部函数处理...

    }
}
