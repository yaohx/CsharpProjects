using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;

using MB.Util.Model;
namespace MB.Util.Serializer {
    /// <summary>
    /// 查询过滤参数的XML 窜行化。
    /// </summary>
    [MB.Aop.InjectionManager]  
    public class QueryParameterXmlSerializer : System.ContextBoundObject {
        private static readonly string XML_CDATA = "<![CDATA[{0}]]>";
        private static readonly string PARAMETER_NODE = "Parameter";
        private static readonly string GROUP_NAME = "GROUP";
        private static readonly string PARAMETER_GROUP_NODE = "GROUP GroupNodeLinkType='{0}'";
        private static readonly string QUERY_ROOT_PATH = "/FilterRoot";
        private static readonly string GROUP_CHILDS_NAME = "Childs";
        //查询参数排除外的对象属性名称
        private static readonly string[] PARAM_EXCEPTION_PROS_NAME = new string[] { "IsGroupNode", "GroupNodeLinkType", "Childs" };
        /// <summary>
        /// 服务器端最好不要直接使用。
        /// </summary>
        public static QueryParameterXmlSerializer DefaultInstance {
            get {
                return SingletonProvider<QueryParameterXmlSerializer>.Instance;
            }
        }


        /// <summary>
        /// 把参数转换为XML 参数的格式。
        /// </summary>
        /// <param name="queryPars">需要系列化的查询参数描述信息</param>
        /// <returns>系列化后的XML字符窜</returns>
        public string Serializer(QueryParameterInfo[] queryPars) {
            StringBuilder xml = new StringBuilder();
            xml.Append(string.Format("<FilterRoot AdvanceFilter='{0}'>", "False"));
            if (queryPars != null && queryPars.Length > 0) {
                //foreach (QueryParameterInfo info in queryPars) {
                //    writeFirstMarker(xml, PARAMETER_NODE);
                //    serializeSingleObject(xml, info);
                //    writeLastMarker(xml, PARAMETER_NODE);
                //}
                serializeListData(xml, new List<QueryParameterInfo>(queryPars));
            }
            xml.Append("</FilterRoot>");
            return xml.ToString();
        }
        /// <summary>
        /// 把XML 文件转换为参数的格式。
        /// </summary>
        /// <param name="xmlQueryPars">系列化后的XML 字符窜</param>
        /// <returns></returns>
        public QueryParameterInfo[] DeSerializer(string queryParsXmlString) {
            if (string.IsNullOrEmpty(queryParsXmlString)) return null;

            List<QueryParameterInfo> pars = new List<QueryParameterInfo>();
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.LoadXml(queryParsXmlString);

                XmlNode node = xmlDoc.SelectSingleNode(QUERY_ROOT_PATH);
                if (node!=null && node.ChildNodes.Count > 0) {
                    fillListEntity(pars, node.ChildNodes);
                }
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("反系列化传入的查询参数有误！" + ex.Message); 
            }
            return pars.ToArray();
        }


        #region 内部函数处理(系列化处理相关)...
        //系列化集合数据
        private void serializeListData(StringBuilder sXml,List<QueryParameterInfo> lstData) {
            foreach (QueryParameterInfo info in lstData) {
                if (info.IsGroupNode) {
                    if (info.Childs != null && info.Childs.Count > 0) {
                        writeFirstMarker(sXml, string.Format(PARAMETER_GROUP_NODE, info.GroupNodeLinkType.ToString()));

                        serializeListData(sXml, info.Childs);

                        writeLastMarker(sXml, "GROUP");
                    }

                }
                else {
                    writeFirstMarker(sXml, PARAMETER_NODE);
                    serializeSingleObject(sXml, info);
                    writeLastMarker(sXml, PARAMETER_NODE);
                }
            }
        }
        //系列化单的对象
        private void serializeSingleObject(StringBuilder sXml, QueryParameterInfo val) {
            Type t = val.GetType();
            PropertyInfo[] pros = t.GetProperties();
            foreach (PropertyInfo info in pros) {
                if (info.IsSpecialName)
                    continue;
                object[] att = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (att == null || att.Length == 0) continue;
                //非查询需要的参数属性去除掉
                if (Array.IndexOf(PARAM_EXCEPTION_PROS_NAME, info.Name) >= 0) continue;

                Type proType = info.PropertyType;
                object v = info.GetValue(val, null);

                if (v != null) {
                    writeFirstMarker(sXml, info.Name);

                    if (info.Name == "Value" || info.Name == "Value2")
                        valueToXmlString(sXml,v,proType);
                        //sXml.Append(string.Format("<![CDATA[{0}]]>",v.ToString().Trim()));
                    else
                        sXml.Append(v.ToString());

                    writeLastMarker(sXml, info.Name);
                }
            }
           
          
        }
        private void writeFirstMarker(StringBuilder sXml, string nodeName) {
            sXml.Append("<");
            sXml.Append(nodeName);
            sXml.Append(">");
        }
        private void writeLastMarker(StringBuilder sXml, string nodeName) {
            sXml.Append("</");
            sXml.Append(nodeName);
            sXml.Append(">");
        }
        //把属性值转换为XML 字符窜的格式
        private void valueToXmlString(StringBuilder sXml, object val, Type valType) {
            if (val != null && !string.IsNullOrEmpty(val.ToString())) {
                if (valType.Equals(typeof(DateTime))  )
                    sXml.Append(string.Format(XML_CDATA, ((DateTime)val).ToString(MB.BaseFrame.SOD.DATE_TIME_FORMATE))); //ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo)
                else if (valType.IsGenericType && valType.Equals(typeof(Nullable<DateTime>))  ) {
                    sXml.Append(string.Format(XML_CDATA, ((DateTime)val).ToString(MB.BaseFrame.SOD.DATE_TIME_FORMATE))); //ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo)
                }
                else
                    sXml.Append(string.Format(XML_CDATA, val));

            }

        }
        #endregion 内部函数处理(系列化处理相关)...

        #region 内部函数处理(反系列化处理相关)...
        //反系列化 查询字符窜。
        private void fillListEntity(List<QueryParameterInfo> lstData, XmlNodeList nodeList) {
            foreach (XmlNode node in nodeList) {
                QueryParameterInfo info = new QueryParameterInfo();
                if (string.Compare(node.Name, GROUP_NAME, true) == 0) {
                    if (node.ChildNodes.Count == 0) continue;

                    info.IsGroupNode = true;
                    if (node.Attributes["GroupNodeLinkType"] != null && !string.IsNullOrEmpty(node.Attributes["GroupNodeLinkType"].Value)) {
                        info.GroupNodeLinkType = (QueryGroupLinkType)Enum.Parse(typeof(QueryGroupLinkType), node.Attributes["GroupNodeLinkType"].Value);
                    }
                    else {
                        info.GroupNodeLinkType = QueryGroupLinkType.AND;
                    }
                    List<QueryParameterInfo> childs = new List<QueryParameterInfo>();
                    info.Childs = childs;

                    fillListEntity(childs,node.ChildNodes);
                }
                else {
                    fillSingleProperty(info, node);
                }

                lstData.Add(info);
            }
        }
        //根据Node 的节点给每个属性赋值。
        private void fillSingleProperty(QueryParameterInfo entity, XmlNode xmlNode) {
            PropertyInfo[] pros = entity.GetType().GetProperties();
            foreach (PropertyInfo info in pros) {

                if (info.IsSpecialName || !info.CanWrite) continue;

                DataMemberAttribute att = Attribute.GetCustomAttribute(info, typeof(DataMemberAttribute)) as DataMemberAttribute;

                if (att == null) continue;

                //非查询需要的参数属性去除掉
                if (Array.IndexOf(PARAM_EXCEPTION_PROS_NAME, info.Name) >= 0) continue;

                XmlNode propertyNode = getNodeByNodeName(xmlNode.ChildNodes, info.Name);

                if (propertyNode == null) continue;

                if (string.IsNullOrEmpty(propertyNode.InnerText)) continue;

                object val = propertyNode.InnerText.Trim();
                //考虑到Enum 的值都是通过描述来配置的，所以这里需要特殊处理一下。
                if (info.PropertyType.IsEnum)
                    val = Enum.Parse(info.PropertyType, val.ToString());

                MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, info.Name, val);

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
        #endregion 内部函数处理(反系列化处理相关)...
    }
}
