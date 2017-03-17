//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-07
// Description	:	Cache 管理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------using System;
using System;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Resources;

using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace MB.Orm {
    /// <summary>
    /// MbOrmCofigurationHandler 
    /// </summary>
    public class MbOrmCacheCofigurationHandler : IConfigurationSectionHandler {
        object IConfigurationSectionHandler.Create(object parent, object context, XmlNode section) {
            ICacheItemExpiration[] ie = null;
            try {
                if (object.Equals(section, null)) {
                    throw (new MB.Orm.Exceptions.ArgumentNullException("section"));
                }

                XmlNode storageInfo = section.SelectSingleNode("MB.ExpirationPolicy");
                if (!object.Equals(storageInfo, null)) {
                    string interval = storageInfo.Attributes["ExpirationCheckInterval"].Value;
                    string assemblyName = storageInfo.Attributes["AssemblyName"].Value;
                    string className = storageInfo.Attributes["ClassName"].Value;
                    GetICacheItemExpiration(interval, assemblyName, className, ref ie);
                }
                return ie;

            }
            catch (Exception genException) {
                throw genException;
            }
        }
        //获取设置的异常过期策略
        private void GetICacheItemExpiration(string interval, string assemblyName, string className, ref ICacheItemExpiration[] ie) {
            switch (className) {
                case "Microsoft.ApplicationBlocks.Cache.ExpirationsImplementations.SlidingTime":
                    double dinterval = double.Parse(interval);
                    ie = new SlidingTime[1];
                    ie[0] = new SlidingTime(TimeSpan.FromSeconds(dinterval));
                    break;
                case "Microsoft.ApplicationBlocks.Cache.ExpirationsImplementations.AbsoluteTime":
                    double secondsAfter = double.Parse(interval);
                    System.DateTime dt = System.DateTime.Now.AddSeconds(secondsAfter);
                    ie = new AbsoluteTime[1];
                    ie[0] = new AbsoluteTime(dt);
                    break;
                case "Microsoft.ApplicationBlocks.Cache.ExpirationsImplementations.FileDependency":
                    ie = new FileDependency[1];
                    ie[0] = new FileDependency(interval);
                    break;
                case "Microsoft.ApplicationBlocks.Cache.ExpirationsImplementations.ExtendedFormatTime":
                    ie = new ExtendedFormatTime[1];
                    ie[0] = new ExtendedFormatTime(interval);
                    break;
                default:
                    ICacheItemExpiration ieTemp = Assembly.Load(assemblyName).CreateInstance(className) as ICacheItemExpiration;
                    ie = Array.CreateInstance(ieTemp.GetType(), 1) as ICacheItemExpiration[];
                    ie[0] = ieTemp;
                    break;
            }
        }
    }
}
