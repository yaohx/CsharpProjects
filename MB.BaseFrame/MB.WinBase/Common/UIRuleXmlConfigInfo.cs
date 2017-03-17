using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinBase.Atts;  
namespace MB.WinBase.Common {
    /// <summary>
    /// 业务对象的XML配置信息.
    /// </summary>
    public class UIRuleXmlConfigInfo {
        private MB.WinBase.IFace.IClientRuleConfig _ClientRuleConfig;
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _DefaultColumns;
        private Dictionary<string, MB.WinBase.Common.StyleConditionInfo> _DefaultStyleConditions;
        private Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> _ColumnsCfgEdit;

        private Dictionary<string, MB.WinBase.Common.ChartViewCfgInfo> _ChartViewCfg;

        public UIRuleXmlConfigInfo(MB.WinBase.IFace.IClientRuleConfig clientRuleConfig) {
            _ClientRuleConfig = clientRuleConfig;
        }

        /// <summary>
        /// 获取主表默认数值列信息。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> GetDefaultColumns() {
            if (_ClientRuleConfig == null)
                throw new MB.Util.APPException("ClientRuleConfig 的 配置不能为空", Util.APPMessageType.SysErrInfo);

            if (_DefaultColumns == null) {
                RuleClientLayoutAttribute rAtt = _ClientRuleConfig.ClientLayoutAttribute;// AttributeConfigHelper.Instance.GetClientRuleSettingAtt(_ClientRuleConfig);
                if (rAtt == null || string.IsNullOrEmpty(rAtt.UIXmlConfigFile)) {
                    throw new Exceptions.RuleClientConfigException(_ClientRuleConfig); 
                }

                _DefaultColumns = LayoutXmlConfigHelper.Instance.GetColumnPropertys(rAtt.UIXmlConfigFile);  
            }
            return _DefaultColumns;
        }
        /// <summary>
        /// 获取主表默认的样式信息。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, MB.WinBase.Common.StyleConditionInfo> GetDefaultStyleConditions() {
            if (_DefaultStyleConditions == null) {
                RuleClientLayoutAttribute rAtt = _ClientRuleConfig.ClientLayoutAttribute;//AttributeConfigHelper.Instance.GetClientRuleSettingAtt(_ClientRuleConfig);
                if (rAtt == null || string.IsNullOrEmpty(rAtt.UIXmlConfigFile)) {
                    throw new Exceptions.RuleClientConfigException(_ClientRuleConfig);
                }
                _DefaultStyleConditions = LayoutXmlConfigHelper.Instance.GetStyleConditions(rAtt.UIXmlConfigFile);
            }
            return _DefaultStyleConditions;
        }
        /// <summary>
        /// UI层 控制业务类的编辑列信息。
        /// </summary>
        public Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> ColumnsCfgEdit { 
            get{
                if (_ColumnsCfgEdit == null) {
                    var editCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(this.GetDefaultColumns(), _ClientRuleConfig.ClientLayoutAttribute.UIXmlConfigFile);
                    _ColumnsCfgEdit = editCols;
                }
                return _ColumnsCfgEdit;
            }
            set{
                _ColumnsCfgEdit = value;
            }
        }

        /// <summary>
        /// 获取图表默认的配置信息。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, MB.WinBase.Common.ChartViewCfgInfo> GetDefaultChartViewCfg() {
            if (_ChartViewCfg == null) {
                RuleClientLayoutAttribute rAtt = _ClientRuleConfig.ClientLayoutAttribute;
                if (rAtt == null || string.IsNullOrEmpty(rAtt.UIXmlConfigFile)) {
                    return null;
                }
                _ChartViewCfg = LayoutXmlConfigHelper.Instance.getChartViewCfg(rAtt.UIXmlConfigFile);
            }
            return _ChartViewCfg;
        }
    }
}
