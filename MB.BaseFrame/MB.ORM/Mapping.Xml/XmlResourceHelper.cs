//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-12。
// Description	:	 XML 配置文件 读取。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping.Xml
{
    /// <summary>
    /// XML 配置文件 读取。
    /// </summary>
    internal class XmlResourceHelper
    {
        #region 变量定义...
        private static readonly string LOAD_XML_FROM_RESX = "LoadXmlFromResx";
        private static readonly string RESX_ASSEMBLY_DLL = "ResxAssemblyDll";
        private static readonly string CONFIG_FILE_PATH = @"ConfigFile\";

        private static readonly string RESX_FULL_NAME = @"{0}.ConfigFile.{1}";
        #endregion 变量定义...

        #region CreateXmlDocument...
        /// <summary>
        /// 创建XML document
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public System.Xml.XmlDocument CreateXmlDocument(string xmlFileName) {
            string val = System.Configuration.ConfigurationManager.AppSettings[LOAD_XML_FROM_RESX];
            if (string.Compare(System.IO.Path.GetExtension(xmlFileName), ".XML", true) != 0)
                xmlFileName += ".xml";

            bool fromResx = false;
            if (!string.IsNullOrEmpty(val))
                fromResx = System.Convert.ToBoolean(val);

            System.Xml.XmlDocument xDoc = null;

            if (!fromResx) {
                xmlFileName = MB.Util.General.GeApplicationDirectory() + CONFIG_FILE_PATH + xmlFileName;
                //需要判断是从本地文件还是资源文件中进行加载
                if (!System.IO.File.Exists(xmlFileName))
                    throw new MB.Util.APPException(string.Format("XML 文件{0}不存在",xmlFileName), Util.APPMessageType.SysErrInfo);

                string cacheKey = xmlFileName;

                if (CacheProxy.ContainsXmlCfgFile(cacheKey)) {
                    xDoc = CacheProxy.GetCacheXmlCfgFile<System.Xml.XmlDocument>(cacheKey);
                }
                else {
                    xDoc = new System.Xml.XmlDocument();

                    xDoc.Load(xmlFileName);

                    CacheProxy.CacheXmlCfgFile(cacheKey, xDoc);
                }

               
            }
            else {
                string assemblyName = System.Configuration.ConfigurationManager.AppSettings[RESX_ASSEMBLY_DLL];
                if (string.IsNullOrEmpty(assemblyName))
                    throw new MB.Util.APPException(string.Format("请配置XML 资源文件所属的配件名称{0}", RESX_ASSEMBLY_DLL));

                System.Reflection.Assembly asm = System.Reflection.Assembly.LoadWithPartialName(assemblyName);
                if (asm == null)
                    throw new MB.Util.APPException(string.Format("请检查配件{0} 是否存在", assemblyName));

                xmlFileName = string.Format(RESX_FULL_NAME, assemblyName, xmlFileName);


                string cacheKey2 = xmlFileName;

                if (CacheProxy.ContainsXmlCfgFile(cacheKey2)) {
                    xDoc = CacheProxy.GetCacheXmlCfgFile<System.Xml.XmlDocument>(cacheKey2);
                }
                else {
                    xDoc = createBitmapFromResources(asm, xmlFileName);

                    CacheProxy.CacheXmlCfgFile(cacheKey2, xDoc);
                }

               

            }
            return xDoc;
        }
        #endregion CreateXmlDocument...

        #region 内部函数处理...
        //从资源中创建一个位图。
        private System.Xml.XmlDocument createBitmapFromResources(System.Reflection.Assembly assembly, string fileName) {
            System.Reflection.Assembly asm = assembly;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.IO.Stream stream = asm.GetManifestResourceStream(fileName);
            if (stream == null)
                throw new MB.Util.APPException(string.Format("文件名:{0} 在资源{1}中找不到,请确认是否已经添加该文件作为嵌入资源?", fileName, asm.FullName));

            //byte[] bts = new byte[stream.Length];
            //stream.Read(bts, 0, bts.Length);
            //string xml = System.Text.ASCIIEncoding.UTF8.GetString(bts);


            System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.ASCIIEncoding.UTF8);

            var xml = reader.ReadToEnd();

            doc.LoadXml(xml);

            return doc;
        }
        #endregion 内部函数处理...
    }
}
