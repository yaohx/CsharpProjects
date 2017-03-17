/**/
//<summary>---------------------------------------------------------------- 
/// Copyright (C) 2008-2009 www.metersbonwe.com
/// All rights reserved. 
/// Author		:	chendc
/// Create date	:	2009-01-04
/// Description	:	ResourcesHelper : 管理程序中的最终用户消息和显示在界面上元素信息（为了多语言版本而增加的类），
///                 该类主要通过XML文件来描述，并根据完整的命名空间来构造出一个个Xml 文件的Node 来记录对应的Msg信息。
/// Modify date	:			By:					Why: 
///</summary>----------------------------------------------------------------
using System;
using System.Xml;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Threading;
using System.Text.RegularExpressions;

namespace MB.Localization {
    /// <summary>
    /// ResourcesHelper 管理程序中的最终用户消息和显示在界面上元素信息（为了多语言版本而增加的类），
    /// </summary>
    public class ResourcesHelper {
        // Xml资源文件列表
        private static System.Collections.Generic.Dictionary<string,XmlDocument> _ReXmlDocs;
        public static readonly string CHINESS_NODE_NAME = "zh-CN";
        private static readonly string UN_TRANSLATE_FILE_PATH = MB.Util.General.GeApplicationDirectory();  
        #region internal 方法...
        /**/
        /// <summary>
        ///  确定区域性特定资源的访问
        /// </summary>
        /// <param name="resxName"> 资源名称</param>
        /// <permission cref="System.Security.PermissionSet">internal</permission>
        /// <example>
        /// <code>
        ///  EnsureResources(FormType);
        /// </code>
        /// </example>
        internal static bool EnsureResources(string resxXmlName) {
            if (_ReXmlDocs == null) {
                _ReXmlDocs = new System.Collections.Generic.Dictionary<string,XmlDocument>();
            }
            try {
                if (!_ReXmlDocs.ContainsKey(resxXmlName)) {
                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                    System.IO.Stream stream = asm.GetManifestResourceStream(resxXmlName);

                    XmlDocument doc = new XmlDocument();
                    doc.Load(stream);
                    _ReXmlDocs[resxXmlName] = doc;
                }
            }
            catch (Exception e) {
                // MB.Untis.TraceEx.Write("获取设置的XML资源文件[" + resxXmlName + "]时出错." + e.Message);
                return false;
            }
            return true;
        }
        #endregion internal 方法...

        #region Public Static 方法...
        /// <summary>
        /// 根据中文文本在数字词典中查找相应指定语言(线程语言)的文本。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="textPath"></param>
        /// <param name="cnText"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public static string GetStringByCN(string xmlFileName, string textPath, string cnText, params string[] paramValues) {
            if (string.IsNullOrEmpty(cnText)) return cnText;
            string val = cnText;
            if (string.Compare(Thread.CurrentThread.CurrentUICulture.Name, CHINESS_NODE_NAME, true) != 0) {
                EnsureResources(xmlFileName);
                if (_ReXmlDocs.ContainsKey(xmlFileName)) {
                    XmlDocument res = _ReXmlDocs[xmlFileName];
                    if (res != null) {
                        val = getStringByCN(res, xmlFileName, textPath, cnText, paramValues);
                    }
                }
            }
            val = formatString(val, paramValues);
            return val;
        }
        /// <summary>
        /// 获取指定文本标身份的字符窜。
        /// </summary>
        /// <param name="pXmlFileName"></param>
        /// <param name="pMsgPath"></param>
        /// <param name="name"></param>
        /// <param name="pParamValues"></param>
        /// <returns></returns>
        public static string GetString(string xmlFileName, string textPath, string langid, string defaultValue, params string[] paramValues) {
            EnsureResources(xmlFileName);
            string val = defaultValue;
            if (_ReXmlDocs.ContainsKey(xmlFileName)) {
                XmlDocument res = _ReXmlDocs[xmlFileName];
              
                if (res != null) {
                    val = getStringByLangid(res, textPath, defaultValue, langid, paramValues);
                }
            }
            val = formatString(val, paramValues);
            return val;
        }
        //根据参数格式化文本。
        private static string formatString(string text, params string[] paramValues) {
            if (text == null || text.Length == 0) return string.Empty;

            if (paramValues != null && paramValues.Length > 0) {
                //text = String.Format(text,paramValues);
                if (paramValues != null && paramValues.Length > 0) {
                    for (int i = 0; i < paramValues.Length; i++) {
                        text = text.Replace("{" + i + "}", paramValues[i].ToString());
                    }
                }
            }
            return text;
        }
        /**/
        /// <summary>
        ///  通知 ResourceManager 对所有 ResourceSet 对象调用 Close，并释放所有资源
        /// </summary>
        /// <remarks> 此方法将缩小正在运行的应用程序中的工作集。以后在此 <see cref="ResourceManager"/> 上的任何资源查找都和第一次查找一样花费时间，因为它需要再次搜索和加载资源。<br/>
        ///  这在某些复杂线程处理方案中可能有用；在这种情况下创建新的 ResourceManager 不失为明智之举。<br/>
        ///  此方法还可用于以下情况：由当前的 ResourceManager 打开的 .resources 文件必须被明确释放，而无需等到 ResourceManager 完全超出范围并对它进行垃圾回收。
        ///</remarks>
        ///<permission cref="System.Security.PermissionSet">public</permission>
        public static void ReleaseAllResources() {
            if (_ReXmlDocs != null) {
                _ReXmlDocs.Clear();
            }
        }
        #endregion Public Static 方法...

        #region Private 静态方法处理...
        //根据消息在XML文件中的路径得到对应地区特殊的描述(通过消息指定的标记名称来获取)
        private static string getStringByLangid(XmlDocument xmlDoc, string textPath, string defaultValue, string langid, params string[] paramValues) {
            CultureInfo curInfo = Thread.CurrentThread.CurrentUICulture;
            string txtVal = defaultValue;
            XmlNodeList nodes = xmlDoc.SelectNodes(textPath);
            if (nodes.Count == 0) return txtVal;
            foreach (XmlNode node in nodes) {
                if (node.Attributes["Name"] == null || String.Compare(node.Attributes["Name"].Value, langid, true) != 0)
                    continue;

                foreach (XmlNode dataNode in node.ChildNodes) {
                    if (node.NodeType != XmlNodeType.Element) continue;
                    if (String.Compare(dataNode.Name, curInfo.Name) != 0) continue;

                    string txt = replaceSpeChar(dataNode.InnerText);
                    return txt;
                }
                return txtVal;
            }
            return txtVal;
        }
        //获取中文的描述信息获取对应的文本语言
        private static string getStringByCN(XmlDocument xmlDoc, string resourceName, string textPath, string cnText, params string[] paramValues) {
            CultureInfo curInfo = Thread.CurrentThread.CurrentUICulture;
            XmlNodeList nodes = xmlDoc.SelectNodes(textPath);
            if (nodes.Count == 0) return cnText;
            foreach (XmlNode node in nodes) {
                XmlNode cnNode = node.SelectSingleNode(CHINESS_NODE_NAME);
                if (cnNode == null) continue;
                if (string.Compare(cnNode.InnerText.Trim(), cnText, true) != 0) continue;

                XmlNode currNode = node.SelectSingleNode(curInfo.Name);
                if (currNode == null) {
                    saveUnTranslateText(resourceName, cnText);
                    return cnText;
                }
                else {
                    string txt = replaceSpeChar(currNode.InnerText);
                    return txt;
                }
            }
            saveUnTranslateText(resourceName, cnText);
            return cnText;
        }
        //过滤掉特殊的字符
        private static string replaceSpeChar(string text) {
            string str = text;
            //去掉回车和换行符
            str = Regex.Replace(str, @"[\r\n]+", " ", RegexOptions.IgnoreCase);
            //去掉空格符
            str = Regex.Replace(str, @"[\t]+", " ", RegexOptions.IgnoreCase);
            return str;
        }
        #endregion Private 静态方法处理...

        #region 存储未翻译的中文文本...
        //存储未翻译的中文文本
        private static void saveUnTranslateText(string resourceName, string text) {
            bool b = checkSaveText(resourceName, text);
            if (b) return;
            System.IO.StreamWriter swFile = null;
            try {
                swFile = new System.IO.StreamWriter(UN_TRANSLATE_FILE_PATH + resourceName + ".UnTran.txt", true);
                swFile.WriteLine(text);
            }
            catch {
                //Debug.Assert(false,e.Message);
            }
            finally {
                if (swFile != null) {
                    swFile.Flush();
                    swFile.Close();
                }
            }
        }
        //判断需要存储的文本是否已经记录
        private static bool checkSaveText(string resourceName, string text) {
            System.IO.StreamReader swFile = null;
            try {
                swFile = new System.IO.StreamReader(UN_TRANSLATE_FILE_PATH + resourceName + ".UnTran.txt");
                while (true) {
                    string str = swFile.ReadLine();
                    if (str == null || str.Length == 0)
                        break;
                    if (string.Compare(str, text, true) == 0)
                        return true;
                }
            }
            catch {
                //Debug.Assert(false,e.Message);
            }
            finally {
                if (swFile != null) {
                    swFile.Close();
                }
            }
            return false;
        }
        #endregion 存储未翻译的中文文本...
    }
}
