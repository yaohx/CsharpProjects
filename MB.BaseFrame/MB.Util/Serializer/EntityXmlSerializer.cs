//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	实体对象的XML 窜行化。
//                  尽量在接口的传递中不要使用转换为XML 来进行传递。
//                  提供该方法的实现是在特殊的处理中有用。
//                  注意：
//                  1)对于引用类型的属性，必须先配置 PropertyXmlConfigAttribute。否则将忽略不处理
//                  2)对于数组,目前只处理 string[],其它类型不做处理。 数组的值用分号隔开。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;

using MB.Util.Model;
//using MB.Util.XmlConfig;  
namespace MB.Util.Serializer {
    /// <summary>
    ///实体对象的XML 窜行化。(优先考虑性能问题，忽略特殊需求)
    ///尽量在接口的传递中不要使用转换为XML 来进行传递。
    ///提供该方法的实现是在特殊的处理中有用。
    ///注意：
    /// 1)对于引用类型的属性，必须先配置 PropertyXmlConfigAttribute。否则将忽略不处理
    /// 2)对于数组,目前只处理 string[],其它类型不做处理。 数组的值用分号隔开。
    /// 3)只支持简单对象的系列化,对于范型不完全支持
    /// </summary>
    public class EntityXmlSerializer<T> where T : class,new() {
        private static readonly string ROW_NODE = "Row";
        private static readonly string ENTITY_ROOT_PATH = "/EntityRoot/Row";
        private static readonly string XML_CDATA = "<![CDATA[{0}]]>";
        private static readonly string XML_NODE_ATT = "{0}='{1}'";
        private static readonly string XML_NODE_ATT_DATATYPE = "Type";
        private static readonly string CHILD_ITEM = "Item";
        private static readonly string[] SINGLE_DATA_TYPE = new string[] { "INT16", "INT32", "DECIMAL", "FLOAT", "DOUBLE", "INT64", "INT" ,"STRING"};

        private MB.Util.MyDataCache<PropertyInfo, ValueXmlSerializerAttribute> _PropertysXmlAtt;
        private Dictionary<PropertyInfo, bool> _PropertysIsDataMember;

        public EntityXmlSerializer() {
            _PropertysXmlAtt = new MyDataCache<PropertyInfo, ValueXmlSerializerAttribute>(50,500);
            _PropertysIsDataMember = new Dictionary<PropertyInfo, bool>();
        }
        #region Entity 系列化为字符窜...
        /// <summary>
        /// 单个实体对象的系列化
        /// </summary>
        /// <param name="entity">需要实例化的实体对象</param>
        /// <param name="rootNodeName">系列化对像在XML 文件中的根节点</param>
        /// <returns>返回系列化后的XML 字符窜</returns>
        public string SingleSerializer(T entity,string rootNodeName) {
            StringBuilder xml = new StringBuilder();
            if (string.IsNullOrEmpty(rootNodeName))
                rootNodeName = entity.GetType().Name;

            writeFirstMarker(xml, rootNodeName, null,null);
            processObject(xml, entity,typeof(T));
            writeLastMarker(xml, rootNodeName);
           
            return xml.ToString();
        }

        /// <summary>
        ///  单个实体对象的反系列化
        /// </summary>
        /// <param name="xmlString">系列化的XML字符窜</param>
        /// <param name="path">实体对象节点路径名称，如果为空，那么它的路径等于info.GetType().Name</param>
        /// <returns>实体对象</returns>
        public T SingleDeSerializer(string xmlString, string path) {
            if (string.IsNullOrEmpty(xmlString)) return null;
            XmlDocument xmlDoc = new XmlDocument();
            try {
                T info = new T();
                xmlDoc.LoadXml(xmlString);
                if (string.IsNullOrEmpty(path))
                    path = info.GetType().Name;

                XmlNode rootNode = xmlDoc.SelectSingleNode(path);

                fillProperty(info,typeof(T), rootNode);
                return info;
            }
            catch (Exception ex) {
                
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "反系列化实体对象有误。参数有误！");
            }
        }
        #endregion Entity 系列化为字符窜...

        #region 集合系列化处理相关...
        /// <summary>
        /// 把参数转换为XML 参数的格式。
        /// </summary>
        /// <param name="entitys">需要系列化的实体对象集合</param>
        /// <returns>系列化后的字符窜</returns>
        public string Serializer(List<T> entitys) {
            StringBuilder xml = new StringBuilder();
            xml.Append(string.Format("<EntityRoot Version='{0}'>", "X.XXX"));     //0.1 目前还没有任何意义
            if (entitys != null && entitys.Count > 0) {
                foreach (T info in entitys) {
                    writeFirstMarker(xml, ROW_NODE, typeof(T),null);
                    processObject(xml, info,typeof(T));
                    writeLastMarker(xml, ROW_NODE);
                }
            }
            xml.Append("</EntityRoot>");
            return xml.ToString();
        }
        /// <summary>
        /// 把XML 文件转换为参数的格式。
        /// </summary>
        /// <param name="entitysSerializerXmlString">实体集合系列化后的XML字符窜</param>
        /// <returns>实体集合对象</returns>
        public List<T> DeSerializer(string entitysSerializerXmlString) {
            if (string.IsNullOrEmpty(entitysSerializerXmlString)) return null;

            List<T> pars = new List<T>();
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.LoadXml(entitysSerializerXmlString);

                XmlNodeList nodeList = xmlDoc.SelectNodes(ENTITY_ROOT_PATH);
                Type entityType = typeof(T);
                foreach (XmlNode node in nodeList) {
                    T info = new T();
                    fillProperty(info, entityType, node);
                    pars.Add(info);
                }
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("反系列化实体对象有误。参数有误！" + ex.Message);
            }
            return pars;
        }
        #endregion 集合系列化处理相关...

        #region 内部函数处理(系列化处理相关)...
        private void processObject(StringBuilder sXml, object val, Type entityType) {
            processObject(sXml, val, entityType, false);
        }
        private void processObject(StringBuilder sXml, object val, Type entityType, bool exceptCheckDataMember) {
            Type ty =  entityType;
            PropertyInfo[] pros = ty.GetProperties();
            foreach (PropertyInfo info in pros) {
                if (info.IsSpecialName)
                    continue;
                if (!exceptCheckDataMember) {
                    if (_PropertysIsDataMember.ContainsKey(info)) {
                        if (!_PropertysIsDataMember[info]) continue;
                    }
                    else {
                        object[] att = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                        _PropertysIsDataMember[info] = att != null && att.Length > 0;
                        if (att == null || att.Length == 0) continue;
                    }
                }
                ValueXmlSerializerAttribute cfgAtt = null;
                if (_PropertysXmlAtt.ContainsKey(info))
                    cfgAtt = _PropertysXmlAtt[info];
                else {
                    cfgAtt = Attribute.GetCustomAttribute(info, typeof(ValueXmlSerializerAttribute)) as ValueXmlSerializerAttribute;
                    _PropertysXmlAtt.Add(info, cfgAtt);
                }
                if (cfgAtt != null && !cfgAtt.Switch) continue;

                Type proType = info.PropertyType;

                object v = info.GetValue(val, null);

                writeFirstMarker(sXml, info.Name, proType, cfgAtt);
                if (info.PropertyType.IsValueType || string.Compare(info.PropertyType.Name, "String", true) == 0) {
                      valueToXmlString(sXml, v, info.PropertyType, cfgAtt);
                }
                else if (info.PropertyType.IsArray) {
                    if (v == null) continue;
                    if (string.Compare(info.PropertyType.Name, "String[]", true) == 0) {
                        string[] avs = (string[])v;
                        valueToXmlString(sXml, string.Join(",", avs), info.PropertyType, cfgAtt);
                    }
                    else {
                        throw new MB.Util.APPException(string.Format("实体对象的系列化,属性数组类型只支持string[] 类型, {0} 类型暂时不支持",info.PropertyType.Name));
                    }
                }
                else {
                    if (v == null) continue;

                    IList lstEntity = v as IList;
                    if (lstEntity != null) {
                        foreach (object o in lstEntity) {
                            if (o == null) continue;
                            Type childDataType = o.GetType();
                            if(childDataType.IsValueType || string.Compare(childDataType.Name, "String", true) == 0){
                                valueToXmlString(sXml, v, childDataType, cfgAtt);
                                continue;
                            }
                            writeFirstMarker(sXml, CHILD_ITEM, childDataType, cfgAtt);
                            processObject(sXml, o, childDataType);
                            writeLastMarker(sXml, CHILD_ITEM);
                        }
                    }
                    else {
                        processObject(sXml, v, info.PropertyType);
                    }
                }
                writeLastMarker(sXml, info.Name);
            }
        }
        //把属性值转换为XML 字符窜的格式
        private void valueToXmlString(StringBuilder sXml, object val,Type valType, ValueXmlSerializerAttribute cfgAtt) {
            if (val != null && !string.IsNullOrEmpty(val.ToString()) ) {
                if (cfgAtt != null && cfgAtt.GeneralStruct) {
                    processObject(sXml, val, valType,true);
                }
                else {
                    if(valType.Equals(typeof(DateTime)) && val!=null)
                        sXml.Append(string.Format(XML_CDATA, ((DateTime)val).ToString(MB.BaseFrame.SOD.DATE_TIME_FORMATE))); //ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    else if (valType.IsGenericType && valType.Equals(typeof(Nullable<DateTime>)) && val != null) {
                        sXml.Append(string.Format(XML_CDATA, ((DateTime)val).ToString(MB.BaseFrame.SOD.DATE_TIME_FORMATE))); //ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    }
                    else
                        sXml.Append(string.Format(XML_CDATA, val));
                }
            }
 
        }
        //系列化节点开头的标记
        private void writeFirstMarker(StringBuilder sXml, string nodeName, Type dataType, ValueXmlSerializerAttribute cfgAtt) {
            sXml.Append("<");
            sXml.Append(nodeName);
            if (dataType!=null) {
                Type realType = MB.Util.MyReflection.Instance.GetPropertyType(dataType);
                if (cfgAtt != null) {
                    if (cfgAtt.CreateByInstanceType)
                        sXml.Append(" " + string.Format(XML_NODE_ATT, XML_NODE_ATT_DATATYPE, realType.FullName + "," + realType.Assembly.FullName));
                    else if (!string.IsNullOrEmpty(cfgAtt.ValueType))
                        sXml.Append(" " + string.Format(XML_NODE_ATT, XML_NODE_ATT_DATATYPE, cfgAtt.ValueType));
                }

            }
            sXml.Append(">");
        }
        private void writeLastMarker(StringBuilder sXml, string nodeName) {
            sXml.Append("</");
            sXml.Append(nodeName);
            sXml.Append(">");
        }
        #endregion 内部函数处理(系列化处理相关)...

        #region 内部函数处理(反系列化处理相关)...
        //给实体对象赋值
        private void fillProperty(object entity, Type entityType, XmlNode xmlNode) {
            fillProperty(entity, entityType, xmlNode,false);
        }
        private void fillProperty(object entity, Type entityType, XmlNode xmlNode, bool exceptCheckDataMember) {
            PropertyInfo[] pros = entityType.GetProperties();
            foreach (PropertyInfo info in pros) {

                if (info.IsSpecialName || !info.CanWrite) continue;

                if (!exceptCheckDataMember) {
                    if (_PropertysIsDataMember.ContainsKey(info)) {
                        if (!_PropertysIsDataMember[info]) continue;
                    }
                    else {
                        DataMemberAttribute att = Attribute.GetCustomAttribute(info, typeof(DataMemberAttribute)) as DataMemberAttribute;
                        _PropertysIsDataMember[info] =  att != null;
                        if (att == null) continue;
                    }
                }
                ValueXmlSerializerAttribute cfgAtt = null;
                if (_PropertysXmlAtt.ContainsKey(info))
                    cfgAtt = _PropertysXmlAtt[info];
                else {
                    cfgAtt = Attribute.GetCustomAttribute(info, typeof(ValueXmlSerializerAttribute)) as ValueXmlSerializerAttribute;
                    _PropertysXmlAtt.Add(info,cfgAtt);
                }
                if (cfgAtt != null && !cfgAtt.Switch) continue;

                XmlNode propertyNode = getNodeByNodeName(xmlNode.ChildNodes, info.Name);

                if (propertyNode == null) continue;

                if (string.IsNullOrEmpty(propertyNode.InnerText)) continue;

                if (info.PropertyType.IsValueType || string.Compare(info.PropertyType.Name, "String", true) == 0) {
                    if (cfgAtt != null && cfgAtt.GeneralStruct) {
                        XmlNode refrenceNode = getNodeByNodeName(xmlNode.ChildNodes, info.Name);
                        if (refrenceNode != null)
                            setObjectValue(entity, info, cfgAtt, refrenceNode);
                    }
                    else {
                        object val = propertyNode.InnerText.Trim();
                        //考虑到Enum 的值都是通过描述来配置的，所以这里需要特殊处理一下。
                        if (info.PropertyType.IsEnum)
                            val = Enum.Parse(info.PropertyType, val.ToString());

                        MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, val);
                    }
                }
                else if (info.PropertyType.IsArray) {
                    string val = propertyNode.InnerText.Trim();
                    if (string.IsNullOrEmpty(val)) continue;
                    if (string.Compare(info.PropertyType.Name, "String[]", true) == 0) {
                        string[] aVals = val.Split(',');
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, aVals);
                    }
                    else {
                        throw new MB.Util.APPException("在 实体对象XML 反系列化 时,对于数组类型,目前只支持String[] 如果还需要其它的再追加", APPMessageType.SysErrInfo);
                    }

                }
                else {
                    XmlNode refrenceNode = getNodeByNodeName(xmlNode.ChildNodes, info.Name);
                    if (refrenceNode != null)
                        setObjectValue(entity, info, cfgAtt, refrenceNode);
                }

            }
        }
        private void fillValueTypeData(ref object entity, Type entityType, XmlNode xmlNode) {
            PropertyInfo[] pros = entityType.GetProperties();
            foreach (PropertyInfo info in pros) {

                if (info.IsSpecialName || !info.CanWrite) continue;
                XmlNode propertyNode = getNodeByNodeName(xmlNode.ChildNodes, info.Name);

                if (propertyNode == null) continue;
                if (string.IsNullOrEmpty(propertyNode.InnerText)) continue;
                try {
                    object val = propertyNode.InnerText.Trim();
                    //考虑到Enum 的值都是通过描述来配置的，所以这里需要特殊处理一下。
                    if (info.PropertyType.IsEnum)
                        val = Enum.Parse(info.PropertyType, val.ToString());

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, val);
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(string.Format("反系列化结构的时候出错,目前的系列化处理可能还没有支持{0} 这种类型",info.PropertyType.FullName), APPMessageType.SysErrInfo); 
                }

            }
        }
        //根据XML 节点的名称在集合在获取对应的节点
        private XmlNode getNodeByNodeName(XmlNodeList nodeList, string nodeName) {
            foreach (XmlNode node in nodeList) {
                if (string.Compare(node.Name, nodeName, true) == 0)
                    return node;
            }
            return null;
        }
        //设置引用类型的值
        private void setObjectValue(object entity, PropertyInfo info, ValueXmlSerializerAttribute att, XmlNode xmlNode) {
            //判断是否引用集合
            if (info.PropertyType.GetInterface("IList") != null) {
               // if (att.ReferenceModelType == null) return;

                object value = info.GetValue(entity, null);
                //判断并创建一个新的集合容器
                if (value == null) {
                    throw new MB.Util.APPException("所有继承IList接口 的对象属性,在默认的构造函数中先实例化该对象", APPMessageType.SysErrInfo); 
                }

                IList lstValue = value as IList;

                XmlNodeList childsNodes = null;
                //if (att.NotExistsGroupNode) {
                //    childsNodes = xmlNode.ParentNode.ChildNodes;
                //}
                //else {
                    childsNodes = xmlNode.ChildNodes;
               // }

                if (childsNodes.Count == 0) return;

                string nodeName = info.Name;
                foreach (XmlNode lstChild in childsNodes) {
                    if (lstChild.NodeType != XmlNodeType.Element) continue;

                    object childEntity = createObjectByXmlNodeAttribute(lstChild,null);
                    if (childEntity == null) continue;

                    lstValue.Add(childEntity);

                    fillProperty(childEntity, childEntity.GetType(), lstChild);
                }
            }
            else if (info.PropertyType.IsValueType) {
                object v = info.GetValue(entity, null);
                fillValueTypeData(ref v, info.PropertyType, xmlNode);
                MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, v);
            }
            else {
                object proVal = createObjectByXmlNodeAttribute(xmlNode, info.PropertyType);
                if (proVal != null) {
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, proVal);

                    fillProperty(proVal, proVal.GetType(), xmlNode);
                }
            }
        }
        //通过XML 配置的类型属性创建实体对象
        private object createObjectByXmlNodeAttribute(XmlNode xmlNode,Type proType) {
            if (xmlNode.Attributes[XML_NODE_ATT_DATATYPE] != null) {
                string dataType = xmlNode.Attributes[XML_NODE_ATT_DATATYPE].Value;

                string[] types = dataType.Split(',');
                if (types.Length < 2) return null;
                try {
                    object childEntity = DllFactory.Instance.LoadObject(types[0], types[1]);
                    return childEntity;
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(string.Format("反系列化时创建对象{0},{1} 有误 ", types[0], types[1]) + ex.Message, APPMessageType.SysErrInfo);
                }
            }
            else {
                if (proType != null)
                    return MB.Util.DllFactory.Instance.CreateInstance(proType);
                else
                    return null;
            }

        }
        #endregion 内部函数处理(反系列化处理相关)...
    }
}
