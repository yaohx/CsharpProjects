//---------------------------------------------------------------- 
// All rights reserved. 
// Author		:	chendc
// Create date	:	2003-01-04
// Description	:	SoftRegistry
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.IO;
using System.Management;


namespace MB.Aop.SoftRegistry
{
    /// <summary>
    /// Tools 的摘要说明。
    /// </summary>
    public class Tools
    {
        [System.Obsolete("")]
        public static string GetHd() {  //97 bit   after Substring  still have 40 bit
            ManagementObjectSearcher wmiSearcher = new ManagementObjectSearcher();

            wmiSearcher.Query = new SelectQuery(
                "Win32_DiskDrive",
                "",
                new string[] { "PNPDeviceID" }
                );
            ManagementObjectCollection myCollection = wmiSearcher.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em =
                myCollection.GetEnumerator();
            em.MoveNext();
            ManagementBaseObject mo = em.Current;
            string id = mo.Properties["PNPDeviceID"].Value.ToString().Trim();
            return id;
        }
        [System.Obsolete("")]
        public static String GetCpuID() {  //16 bit
            try {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                String strCpuID = null;
                foreach (ManagementObject mo in moc) {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch {
                return "";
            }
        }
        [System.Obsolete("")]
        public static string GetShortDate() {  //6 bit
            string sDate = System.DateTime.Now.ToString("yy-MM-dd");
            sDate = sDate.Replace("-", String.Empty);
            sDate = sDate.Substring(2, sDate.Length - 2);
            return sDate;
        }
        [System.Obsolete("")]
        public static string GetVolumeSn() {   //8 bit
            ManagementObject m_objDisk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            string strSN = (string)m_objDisk.GetPropertyValue("VolumeSerialNumber");
            return strSN;

        }
    }
}
