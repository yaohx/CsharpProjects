using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Util.Model;
namespace MB.BaseFrame {
    /// <summary>
    /// 框架公共数据提供。
    /// </summary>
    public class MBShareDataProvider {
        private static readonly List<CodeNameInfo> _DocStates;
        private static readonly string SYS_CODE_NAME_PATH = "SysCodeName/CodeNameType";
        /// <summary>
        /// 
        /// </summary>
        static MBShareDataProvider() {
            _DocStates = new List<CodeNameInfo>();
            _DocStates.Add(new CodeNameInfo(0, "0", "录入中"));
            _DocStates.Add(new CodeNameInfo(1, "1", "已确认"));
            _DocStates.Add(new CodeNameInfo(2, "2", "已审核"));

        }
        /// <summary>
        /// 系统单据状态。
        /// </summary>
        public static List<CodeNameInfo> DocStates {
            get {
                return _DocStates;
            }
        }
        
        /// <summary>
        /// 从配置的XML 文件中获取系统编码。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="codeNameType"></param>
        /// <returns></returns>
        public static List<CodeNameInfo> GetSysCodeNameFromXml(string xmlFileName, string codeNameType) {
            string fileFullName = MB.Util.XmlConfig.XmlConfigHelper.BuildXmlConfigFileFullName(xmlFileName);  
            MB.Util.XmlConfig.XmlConfigHelper xmlHelper = new MB.Util.XmlConfig.XmlConfigHelper();
            var lstData = xmlHelper.CreateEntityList<CodeNameInfo>("Name", fileFullName, SYS_CODE_NAME_PATH, codeNameType);
           return new List<CodeNameInfo>(lstData.Values.ToArray());
        }
    }
}
