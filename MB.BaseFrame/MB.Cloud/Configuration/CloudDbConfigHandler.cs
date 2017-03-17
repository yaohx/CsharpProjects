using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace MB.Cloud.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class CloudDbConfigHandler : IConfigurationSectionHandler
    {
        private CloudConfigInfo _CloudDbConfigInfo;
        internal CloudConfigInfo CloudDbConfigInfo {
            get {
                return _CloudDbConfigInfo;
            }
        }
        /// <summary>
        /// 由所有配置节处理程序实现，以分析配置节的 XML
        /// </summary>
        /// <param name="parent">对应父配置节中的配置设置</param>
        /// <param name="context">在从 ASP.NET 配置系统中调用 Create 时为 HttpConfigurationContext。否则，该参数是保留参数，并且为空引用。</param>
        /// <param name="section">一个 XmlNode，它包含配置文件中的配置信息。提供对配置节 XML 内容的直接访问。</param>
        /// <returns>配置对象</returns>
        object IConfigurationSectionHandler.Create(object parent, object context, XmlNode section) {
            _CloudDbConfigInfo = new CloudConfigInfo();
            if (Object.Equals(section, null)) {
                throw (new ArgumentNullException());
            }
            var storageInfo = section.SelectSingleNode("DB");	//读取Aspect节点
            if (!object.Equals(storageInfo, null))						//当配置节点不为空的时候，循环读出每个Aspect的信息
			{
                foreach (XmlNode node in storageInfo.ChildNodes) {

                    if (node.NodeType != XmlNodeType.Element) continue;
                    if (string.Compare(node.Name, "CloudDbCode", true) == 0)
                        _CloudDbConfigInfo.CloudDbCode = node.InnerText;

                    if (string.Compare(node.Name, "CloudDbManager", true) == 0)
                        _CloudDbConfigInfo.CloudDbManager = createCloudDbManager(node);

                    if (string.Compare(node.Name, "CloudDBTemplates", true) == 0)
                        _CloudDbConfigInfo.CloudDBTemplates = createCloudTemplate(node);

                }
            }
            checkCfg();

            return _CloudDbConfigInfo;
        }
        private CloudDbManager createCloudDbManager(XmlNode node) {
            CloudDbManager cm = new CloudDbManager();

            checkNodeAttribute(node, "agent", "CloudDbManager");
            cm.Agent = node.Attributes["agent"].Value;

            checkNodeAttribute(node, "databaseService", "CloudDbManager");
            cm.DatabaseService = node.Attributes["databaseService"].Value;

            checkNodeAttribute(node, "monitorService", "CloudDbManager");
            cm.MonitorService = node.Attributes["monitorService"].Value;

            checkNodeAttribute(node, "groupService", "CloudDbManager");
            cm.GroupService = node.Attributes["groupService"].Value;

            checkNodeAttribute(node, "groupName", "CloudDbManager");
            cm.GroupName = node.Attributes["groupName"].Value;
            return cm;
        }
        private List<CloudDBTemplate> createCloudTemplate(XmlNode node) {
            List<CloudDBTemplate> tems = new List<CloudDBTemplate>();
            foreach (XmlNode c in node.ChildNodes) {
                if (c.NodeType != XmlNodeType.Element) continue;
                CloudDBTemplate t = new CloudDBTemplate();

                checkNodeAttribute(c, "provider", "CloudDBTemplate");
                t.Provider = c.Attributes["provider"].Value;

                checkNodeAttribute(c, "connection", "CloudDBTemplate");
                t.ConnectionTemplate = c.Attributes["connection"].Value;

                tems.Add(t);
            }
            return tems;
        }
        private void checkNodeAttribute(XmlNode node, string attName, string nodeName) {
            if (node.Attributes[attName] == null)
                throw new MB.Util.APPException(string.Format("请检查云计算平台数据库配置项{0} 中的 {1} 是否正确配置", nodeName, attName));
        }
        //检验云计算配置项的完整性
        private void checkCfg() {
            if (string.IsNullOrEmpty(_CloudDbConfigInfo.CloudDbCode))
                throw new MB.Util.APPException("请检查云计算平台 CloudDbCode 配置项是否存在 ");

            if (_CloudDbConfigInfo.CloudDbManager==null)
                throw new MB.Util.APPException("请检查云计算平台 CloudDbManager 配置项是否存在 ");

            if (_CloudDbConfigInfo.CloudDBTemplates==null)
                throw new MB.Util.APPException("请检查云计算平台 CloudDBTemplate 配置项是否存在 ");
        } 
    }
}
