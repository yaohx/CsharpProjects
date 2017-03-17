//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	读取Aspect配置文件的类.
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using System.Configuration;

namespace MB.Aop {

    /// <summary>
    /// 读取Aspect配置文件的类
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough()]
    public class AspectConfigHandler : IConfigurationSectionHandler {
        private IList<AspectInfo> _Aspects;
        /// <summary>
        /// 由所有配置节处理程序实现，以分析配置节的 XML
        /// </summary>
        /// <param name="parent">对应父配置节中的配置设置</param>
        /// <param name="context">在从 ASP.NET 配置系统中调用 Create 时为 HttpConfigurationContext。否则，该参数是保留参数，并且为空引用。</param>
        /// <param name="section">一个 XmlNode，它包含配置文件中的配置信息。提供对配置节 XML 内容的直接访问。</param>
        /// <returns>配置对象</returns>
        object IConfigurationSectionHandler.Create(object parent, object context, XmlNode section) {
            _Aspects = new  List<AspectInfo>();
            //IList<AspectInfo> aspects = InjectionManager.Aspects;
            if (Object.Equals(section, null)) {
                throw (new ArgumentNullException());
            }
            XmlNodeList storageInfo = section.SelectNodes("Aspect");	//读取Aspect节点
            if (!object.Equals(storageInfo, null))						//当配置节点不为空的时候，循环读出每个Aspect的信息
			{
                foreach (XmlNode node in storageInfo) {

                    checkAndThrowException(node);

                    string[] typeName = node.Attributes["type"].Value.Split(',');
                    string deploymodel = node.Attributes["deploy-model"].Value;
                    string pointcut = node.Attributes["pointcut-type"].Value;
                    string actionposition = node.Attributes["action-position"].Value;
                    string[] match = node.Attributes["match"].Value.Split(',');
                    if (match[0].StartsWith("*")) {
                        _Aspects.Add(new AspectInfo(typeName[1], typeName[0], deploymodel, pointcut, actionposition,
                            match[0], match[1], match[0].Substring(1, match[0].Length - 1) + "--" + match[1]));
                    }
                    else {
                        _Aspects.Add(new AspectInfo(typeName[1], typeName[0], deploymodel, pointcut, actionposition,
                            match[0], match[1], match[0] + "--" + match[1]));
                    }
                }
            }

            return _Aspects;
        }
        internal IList<AspectInfo> Aspects {
            get {
                return _Aspects;
            }
        }
        private void checkAndThrowException(XmlNode node) {
            StringBuilder errMsg = new StringBuilder();
            if (node.Attributes["type"] == null)
                errMsg.Append("type ");

            if (node.Attributes["deploy-model"] == null)
                errMsg.Append("deploy-model ");

            if (node.Attributes["pointcut-type"] == null)
                errMsg.Append("pointcut-type ");

            if (node.Attributes["action-position"] == null)
                errMsg.Append("action-position ");

            if (node.Attributes["match"] == null)
                errMsg.Append("match ");

            if (errMsg.Length > 0)
                throw new Exception("在注入方面的配置中,节点：" + errMsg.ToString() + " 的配置不能为空！");
        }
    }

}

