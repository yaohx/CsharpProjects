//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	实体对象XML 文件配置处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.Text;
using System.Configuration; 

namespace MB.Util.XmlConfig {
    /// <summary>
    /// 实体对象XML 文件配置处理相关。
    /// 主要针对从XML 文件获取配置数据处理相关。
    /// </summary>
    public class XmlConfigHelper{
        /// <summary>
        /// SQL stirng 对应的配置信息。
        /// </summary>
        public static readonly string SQL_CONFIG_NODE = "/Entity/Sqls/Sql";
        public static string XML_FILE_PATH = MB.Util.General.GeApplicationDirectory() +
                                             ConfigurationSettings.AppSettings["XmlConfigPath"].ToString();

        #region Instance...
        private static Object _Obj = new object();
        private static XmlConfigHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        public XmlConfigHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static XmlConfigHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new XmlConfigHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 获取单个配置项。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyAttributeName"></param>
        /// <param name="xmlFileFullName"></param>
        /// <param name="nodeListPath"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public T CreateConfigInfoByXmlDoc<T>(string keyAttributeName, XmlDocument xmls, string nodeListPath, string keyValue) where T : class {
            try {
                if (xmls == null) return default(T);

                XmlNodeList nodeList = xmls.SelectNodes(nodeListPath);

                if (nodeList == null || nodeList.Count == 0) return default(T);
                XmlNode xmlNode = null;
                if (!string.IsNullOrEmpty(keyValue)) {
                    foreach (XmlNode node in nodeList) {
                        if (node.NodeType != XmlNodeType.Element)
                            continue;

                        if (node.Attributes[keyAttributeName] != null && string.Compare(node.Attributes[keyAttributeName].Value, keyValue, true) == 0) {
                            xmlNode = node;
                            break;
                        }
                    }
                }
                if (xmlNode == null) return default(T);

                Type entityType = typeof(T);
                T entity = (T)DllFactory.Instance.CreateInstance(entityType);
                FillEntityValue(entity, xmlNode);

                return entity;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Format("加载XML配置信息 {1} 出错！", nodeListPath));
            }

        }

        /// <summary>
        /// 获取单个配置项。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyAttributeName"></param>
        /// <param name="xmlFileFullName"></param>
        /// <param name="nodeListPath"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public T CreateConfigInfo<T>(string keyAttributeName, string xmlFileFullName, string nodeListPath, string keyValue) where T : class {
            try {
                XmlDocument xmls = LoadXmlConfigFile(xmlFileFullName);
                return CreateConfigInfoByXmlDoc<T>(keyAttributeName, xmls, nodeListPath, keyValue);
                
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Format("加载XML文件{0} 的配置信息 {1} 出错！", xmlFileFullName, nodeListPath));
            }

        }

        #region 根据XML Document读取配置

        public Dictionary<string, T> CreateEntityListByXmlDoc<T>(string keyAttributeName, XmlDocument xmls, string nodeListPath) where T : class,new() {
            return CreateEntityListByXmlDoc<T>(keyAttributeName, xmls, nodeListPath, string.Empty);
        }

        public Dictionary<string, T> CreateEntityListByXmlDoc<T>(string keyAttributeName, XmlDocument xmls, string nodeListPath, string keyValue) {
            XmlNode rootNode = null;
            return CreateEntityListByXmlDoc<T>(keyAttributeName, xmls, nodeListPath, keyValue, out rootNode);
        }

        public Dictionary<string, T> CreateEntityListByXmlDoc<T>(string keyAttributeName, XmlDocument xmls, string nodeListPath, string keyValue, out XmlNode findRootNode) {
            try {
                findRootNode = null;
                if (xmls == null) return null;

                XmlNodeList nodeList = xmls.SelectNodes(nodeListPath);

                if (nodeList == null || nodeList.Count == 0) {
                    // MB.Util.TraceEx.Write(string.Format("XML 文件{0} 中找不到相应的路径 {1}",xmlFileFullName,nodeListPath)); 
                    return null;
                }
                XmlNodeList entityNodes = nodeList;
                if (!string.IsNullOrEmpty(keyValue)) {
                    foreach (XmlNode node in nodeList) {
                        if (node.NodeType != XmlNodeType.Element)
                            continue;

                        if (node.Attributes[keyAttributeName] != null && string.Compare(node.Attributes[keyAttributeName].Value, keyValue, true) == 0) {
                            entityNodes = node.ChildNodes;
                            findRootNode = node;
                            break;
                        }
                    }
                }

                Dictionary<string, T> cols = new Dictionary<string, T>();
                foreach (XmlNode node in entityNodes) {
                    if (node.NodeType != XmlNodeType.Element)
                        continue;

                    Type entityType = typeof(T);
                    T entity = (T)DllFactory.Instance.CreateInstance(entityType);

                    if (node.Attributes[keyAttributeName] != null && node.Attributes[keyAttributeName].Value != null) {
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, keyAttributeName, node.Attributes[keyAttributeName].Value);
                    }

                    FillEntityValue(entity, node);

                    object key = null;
                    if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, keyAttributeName))
                        key = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, keyAttributeName);
                    if (key == null) {
                        key = System.Guid.NewGuid();
                        //throw new MB.Util.APPException(string.Format("在根据XM文件{0} 创建对象{1} 时,XML Node{2}的配置键值有误！",
                        //                                xmlFileFullName, entity.GetType().FullName, node.ToString()), MB.Util.APPMessageType.SysErrInfo);

                    }
                    cols.Add(key.ToString(), entity);

                }
                return cols;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Format("加载XML的配置信息 {0} 出错！", nodeListPath));
            }
        }

        #endregion

        #region 根据XML文件读取配置
        /// <summary>
        ///  根据XML 配置文件和.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyAttributeName"></param>
        /// <param name="xmlFileFullName"></param>
        /// <param name="nodeListPath"></param>
        /// <returns></returns>
        public Dictionary<string, T> CreateEntityList<T>(string keyAttributeName, string xmlFileFullName, string nodeListPath) where T : class,new() {
            return CreateEntityList<T>(keyAttributeName, xmlFileFullName, nodeListPath, string.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyAttributeName"></param>
        /// <param name="xmlFileFullName"></param>
        /// <param name="nodeListPath"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public Dictionary<string, T> CreateEntityList<T>(string keyAttributeName, string xmlFileFullName, string nodeListPath, string keyValue) {
            XmlNode rootNode = null;
            return CreateEntityList<T>(keyAttributeName, xmlFileFullName, nodeListPath, keyValue, out rootNode);
        }
        /// <summary>           
        ///  根据XML 配置文件和.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyAttributeName"></param>
        /// <param name="xmlFileFullName"></param>
        /// <param name="nodeListPath"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public Dictionary<string, T> CreateEntityList<T>(string keyAttributeName, string xmlFileFullName, string nodeListPath, string keyValue,out XmlNode findRootNode) {
            try {

                XmlDocument xmls = LoadXmlConfigFile(xmlFileFullName);
                return CreateEntityListByXmlDoc<T>(keyAttributeName, xmls, nodeListPath, keyValue, out findRootNode);
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Format("加载XML文件{0} 的配置信息 {1} 出错！", xmlFileFullName, nodeListPath));
            }
        }

        #endregion 

        /// <summary>
        /// 根据XML 节点设置实体对象的属性或者字段的值。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="xmlNode"></param>
        public void FillEntityValue(object entity, XmlNode xmlNode) {
            if (entity == null) return;
            ModelXmlConfigAttribute att = Attribute.GetCustomAttribute(entity.GetType(), typeof(ModelXmlConfigAttribute)) as ModelXmlConfigAttribute;
            if (att == null) {
                return;
            }
            if (att.ByXmlNodeAttribute) {
                fillEntityValueByXmlAttribute(entity, xmlNode);
            }
            else {
                fillEntityByXmlInnerText(entity, xmlNode);
            }
        }
        /// <summary>
        /// 加载XML 配置文档。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public XmlDocument LoadXmlConfigFile(string xmlFileFullName) {
            XmlDocument xmlDoc = new XmlDocument();
            bool exist = System.IO.File.Exists(xmlFileFullName);
            try {
                if (exist) {
                    xmlDoc.Load(xmlFileFullName);
                }
                else {
                    throw new MB.Util.APPException(string.Format("表结构的配置文件找不到!,具体是文件在目录:{0} 中找不到.", xmlFileFullName));
                }
            }
            catch (Exception e) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(e,string.Format("加载XML 配置文件:{0} 出错", xmlFileFullName));
            }
            return xmlDoc;
        }
        /// <summary>
        /// 根据XML 文件名称构建完整的XML 配置文件完整路径。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public static string BuildXmlConfigFileFullName(string xmlFileName) {
            if (string.IsNullOrEmpty(XML_FILE_PATH))
                throw new MB.Util.APPException("需要在App.Config 中配置 XmlConfigPath 配置项,请检查！");

            if (XML_FILE_PATH.IndexOf(":") < 0) {
                XML_FILE_PATH = MB.Util.General.GeApplicationDirectory() +
                                ConfigurationSettings.AppSettings["XmlConfigPath"].ToString();
            }
            if (xmlFileName.IndexOf(".") < 0) {
                xmlFileName += ".xml";
            }
            string fileFullName = XML_FILE_PATH + xmlFileName;
            return fileFullName;
        }

        #region 内部函数处理...
        //通过XML Node 的 Attribute 来设置值。
        private void fillEntityValueByXmlAttribute(object entity, XmlNode xmlNode) {
            PropertyInfo[] pros = entity.GetType().GetProperties();
            foreach (PropertyInfo info in pros) {
                if (info.IsSpecialName || !info.CanWrite)
                    continue;
                PropertyXmlConfigAttribute att = Attribute.GetCustomAttribute(info, typeof(PropertyXmlConfigAttribute)) as PropertyXmlConfigAttribute;

                if (att == null || !att.Switch) continue;

                string attName = string.IsNullOrEmpty(att.MappingName) ? info.Name : att.MappingName;

                if (info.PropertyType.IsValueType || string.Compare(info.PropertyType.Name, "String", true) == 0) {

                    if (xmlNode.Attributes[attName] == null) continue;

                    object val = xmlNode.Attributes[attName].Value.Trim();
                    if (info.PropertyType.IsEnum)
                        val = Enum.Parse(info.PropertyType, val.ToString());

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, val);
                }
                else {
                    XmlNode refrenceNode = getNodeByNodeName(xmlNode.ChildNodes, attName);
                    if(refrenceNode!=null)
                        setRefrenceEntityValue(entity, info, att, refrenceNode);
                }

            }
        }
        //通过InnerText 来设置实体对象属性的值。
        private void fillEntityByXmlInnerText(object entity, XmlNode xmlNode) {

            PropertyInfo[] pros = entity.GetType().GetProperties();
            foreach (PropertyInfo info in pros) {

                if (info.IsSpecialName || !info.CanWrite) continue;

                PropertyXmlConfigAttribute att = Attribute.GetCustomAttribute(info, typeof(PropertyXmlConfigAttribute)) as PropertyXmlConfigAttribute;

                if (att == null || !att.Switch) continue;

                string nodeName = string.IsNullOrEmpty(att.MappingName) ? info.Name : att.MappingName;
                XmlNode propertyNode = getNodeByNodeName(xmlNode.ChildNodes, nodeName);

                if (propertyNode == null) continue;

                if (info.PropertyType.IsValueType || string.Compare(info.PropertyType.Name, "String", true) == 0) {

                    if (string.IsNullOrEmpty(propertyNode.InnerText)) continue;

                    object val = propertyNode.InnerText.Trim();
                    //考虑到Enum 的值都是通过描述来配置的，所以这里需要特殊处理一下。
                    if (info.PropertyType.IsEnum)
                        val = Enum.Parse(info.PropertyType, val.ToString());

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name,val );
                }
                else if(info.PropertyType.IsArray){
                    string val = propertyNode.InnerText.Trim();
                    if (string.IsNullOrEmpty(val)) continue;
                    if (string.Compare(info.PropertyType.Name, "String[]", true) == 0) {
                        string[] aVals = val.Split(',');
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, aVals);
                    }
                    else {
                        throw new MB.Util.APPException("在 fillEntityByXmlInnerText 时,对于数组类型,目前只支持String[] 如果还需要其它的再追加"); 
                    }

                }
                else {
                    XmlNode refrenceNode = getNodeByNodeName(xmlNode.ChildNodes, nodeName );
                    if(refrenceNode!=null)
                        setRefrenceEntityValue(entity, info, att, refrenceNode);
                }
            }
        }
        //根据XML 节点的名称在集合在获取对应的节点
        private XmlNode getNodeByNodeName(XmlNodeList nodeList, string nodeName) {
            foreach (XmlNode node in nodeList) {
                if(string.Compare(node.Name,nodeName,true)==0)
                    return node;
            }
            return null;
        }
        //设置引用类型的值
        private void setRefrenceEntityValue(object entity, PropertyInfo info, PropertyXmlConfigAttribute att, XmlNode xmlNode) {
            //判断是否引用集合
            if (info.PropertyType.GetInterface("IList") != null) {
                if (att.ReferenceModelType == null ) return;

                object value = info.GetValue(entity, null);

                if (value == null) return;

                IList lstValue = value as IList;
                XmlNodeList childsNodes = null;
                if (att.NotExistsGroupNode) {
                    childsNodes = xmlNode.ParentNode.ChildNodes;
                }
                else {
                    childsNodes = xmlNode.ChildNodes;
                }

                if (childsNodes.Count == 0) return ;

                string nodeName = string.IsNullOrEmpty(att.MappingName) ? info.Name : att.MappingName;
                foreach (XmlNode lstChild in childsNodes) {
                    if (lstChild.NodeType != XmlNodeType.Element) continue;

                   // if (string.Compare(lstChild.Name, nodeName, true) != 0) continue;

                    object childEntity = DllFactory.Instance.CreateInstance(att.ReferenceModelType);
                    lstValue.Add(childEntity);
                    ModelXmlConfigAttribute t = Attribute.GetCustomAttribute(childEntity.GetType(), typeof(ModelXmlConfigAttribute)) as ModelXmlConfigAttribute;
                    if (t == null) {
                        MB.Util.TraceEx.Write(string.Format("类型{0} 没有配置ModelXmlConfigAttribute。", childEntity.GetType().FullName));
                        continue;
                    }
                    if(t.ByXmlNodeAttribute)
                        fillEntityValueByXmlAttribute(childEntity, lstChild);
                    else
                        fillEntityByXmlInnerText(childEntity, lstChild);
                }
            }
            else {
                object childEntity = DllFactory.Instance.CreateInstance(info.PropertyType);

                MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, childEntity);

                ModelXmlConfigAttribute t = Attribute.GetCustomAttribute(childEntity.GetType(), typeof(ModelXmlConfigAttribute)) as ModelXmlConfigAttribute;
                if (t == null) {
                    MB.Util.TraceEx.Write(string.Format("类型{0} 没有配置ModelXmlConfigAttribute。", childEntity.GetType().FullName));
                    return;
                }

                if(t.ByXmlNodeAttribute)
                    fillEntityValueByXmlAttribute(childEntity, xmlNode);
                else
                    fillEntityByXmlInnerText(childEntity, xmlNode);

            }
        }
        #endregion 内部函数处理...
    }
}
