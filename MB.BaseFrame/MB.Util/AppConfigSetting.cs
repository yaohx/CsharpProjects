using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MB.Util {
    /// <summary>
    /// ConfigSeting 应该程序配置文件处理相关。
    /// </summary>
    public class AppConfigSetting {
        private static string PARENT_SETTING_NODE = "/configuration/appSettings";
        private static string NODES_PATH = "/configuration/appSettings/add";



        #region 设置配置文件相关...
        /// <summary>
        /// 设置配置文件中值（每一个配置的信息都以这样的方式(key = " + pKeyName +" ,value =)）
        /// </summary>
        /// <param name="pKeyName">得到的键值的名称</param>
        /// <param name="pKeyValue">要设置的键值</param>
        public static void SetKeyValue(string keyName, string keyValue) {
            SetKeyValue(keyName, keyValue, null, null);
        }
        /// <summary>
        /// 设置配置文件中值（每一个配置的信息都以这样的方式(key = " + pKeyName +" ,value =)）
        /// </summary>
        /// <param name="pKeyName">得到的键值的名称</param>
        /// <param name="pKeyValue"> 要设置的键值</param>
        /// <param name="pFullPathName">配置文件完整的路径</param>
        public static void SetKeyValue(string keyName, string keyValue, string fullPathName) {
            SetKeyValue(keyName, keyValue, fullPathName, null);
        }
        /// <summary>
        /// 设置配置文件中值（每一个配置的信息都以这样的方式(key = " + pKeyName +" ,value =)）
        /// </summary>
        /// <param name="pKeyName">得到的键值的名称</param>
        /// <param name="pKeyValue"></param>
        /// <param name="pFullPathName">配置文件完整的路径</param>
        /// <param name="pNodesPath">Node在XML配置文件的路径></param>
        public static void SetKeyValue(string keyName, string keyValue, string fullPathName, string nodesPath) {
            string nodePath = NODES_PATH;
            if (!string.IsNullOrEmpty(nodesPath)) {
                nodePath = nodesPath;
            }
            XmlDocument XMLDoc;
            XMLDoc = new XmlDocument();
            string pathName = string.Empty;
            if (!string.IsNullOrEmpty(fullPathName)) {
                pathName = fullPathName;
            }
            else {
                pathName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            }
            bool b = System.IO.File.Exists(pathName);
            if (!b) {
                MB.Util.TraceEx.Write("配置文件" + pathName + "不存在!", APPMessageType.SysFileInfo);
                return;
            }
            try {
                XMLDoc.Load(pathName);
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("加载配置文件{0}出错", fullPathName) + ex.Message, APPMessageType.SysErrInfo);
            }

            XmlNodeList nodes = XMLDoc.SelectNodes(nodePath);
            bool findKey = false;
            foreach (XmlNode node in nodes) {
                if (string.Compare( node.Attributes["key"].Value, keyName,true)==0) {
                    node.Attributes["value"].Value = keyValue;
                    findKey = true;
                    break;
                }
            }
            if (!findKey) {
                XmlNode parentNode = XMLDoc.SelectSingleNode(PARENT_SETTING_NODE);

                XmlElement newNode = XMLDoc.CreateElement("add");
                XmlAttribute att = XMLDoc.CreateAttribute("key");
                att.Value = keyName;
                newNode.Attributes.Append(att);

                att = XMLDoc.CreateAttribute("value");
                att.Value = keyValue;
                newNode.Attributes.Append(att);

                parentNode.AppendChild(newNode);

                //UP.Utils.TraceEx.Write("没有配置键值，请以这样的方式(key = " + pKeyName +" ,value =)在config文件中配置。",UP.Data.Model.SysMessageLevel.CodeRunInfo);   
            }
            XMLDoc.Save(pathName);
        }
        #endregion 设置配置文件相关...

        #region 获取配置文件处理信息相关...
        /// <summary>
        /// 得到配置文件中值（每一个配置的信息都以这样的方式(key = " + pKeyName +" ,value =)）
        /// </summary>
        /// <param name="pKeyName">得到的键值的名称</param>
        /// <returns> 返回 Key对应的值</returns>
        public static string GetKeyValue(string keyName) {
            return GetKeyValue(keyName, null, null);
        }
        /// <summary>
        ///  得到配置文件中值（每一个配置的信息都以这样的方式(key = " + pKeyName +" ,value =)）
        /// </summary>
        /// <param name="pKeyName">得到的键值的名称</param>
        /// <param name="pFullPathName">配置文件完整的路径</param>
        /// <returns>返回 Key对应的值</returns>
        public static string GetKeyValue(string keyName, string fullPathName) {
            return GetKeyValue(keyName, fullPathName, null);
        }
        /// <summary>
        ///  得到配置文件中值（每一个配置的信息都以这样的方式(key = " + pKeyName +" ,value =)）
        /// </summary>
        /// <param name="pKeyName">得到的键值的名称</param>
        /// <param name="pFullPathName">配置文件完整的路径</param>
        /// <param name="pNodesPath">Node在XML配置文件的路径</param>
        /// <returns>返回 Key对应的值</returns>
        public static string GetKeyValue(string keyName, string fullPathName, string nodesPath) {
            string nodePath = NODES_PATH;
            if (nodesPath != null && nodesPath != "") {
                nodePath = nodesPath;
            }
            XmlDocument XMLDoc;
            XMLDoc = new XmlDocument();
            string pathName = "";

            if (fullPathName != null && fullPathName != "") {
                pathName = fullPathName;
            }
            else {
                //通过它也可以得到 AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
                pathName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            }
            bool b = System.IO.File.Exists(pathName);
            if (!b) {
                MB.Util.TraceEx.Write("配置文件" + pathName + "不存在!", APPMessageType.SysFileInfo);
                return null;
            }
            XMLDoc.Load(pathName);
            XmlNodeList nodes = XMLDoc.SelectNodes(nodePath);
            string val = null;
            foreach (XmlNode node in nodes) {
                if (node.Attributes["key"].Value.ToLower() == keyName.ToLower()) {
                    val = node.Attributes["value"].Value;
                    break;
                }
            }
            if (val == null || val == "") {
                MB.Util.TraceEx.Write("代码执行说明: (key = " + keyName + " ,value =)在config文件中没有配置。");
            }
            return val;
        }
        #endregion 获取配置文件处理信息相关...
    }
}
