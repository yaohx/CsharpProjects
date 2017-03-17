using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinDxChart.Chart
{
    public class ChartTemplateHelper
    {
        private static readonly string APP_CONFIG_KEY_NAME = "ChartTemplate";

        /// <summary>
        /// 客户端模块评语实现客户端。
        /// </summary>
        /// <returns></returns>
        public MB.WinDxChart.IFace.IChartTemplateClient CreateTemplateClient()
        {
            string cfgSetting = System.Configuration.ConfigurationSettings.AppSettings[APP_CONFIG_KEY_NAME];
            if (string.IsNullOrEmpty(cfgSetting)) return null;
            string[] cfgs = cfgSetting.Split(',');
            object instance = MB.Util.DllFactory.Instance.LoadObject(cfgs[0], cfgs[1]);
            if (instance == null)
                throw new MB.Util.APPException(string.Format("创建{0} , {1} 有误,请检查", cfgs[0], cfgs[1]));

            MB.WinDxChart.IFace.IChartTemplateClient commentClient = instance as MB.WinDxChart.IFace.IChartTemplateClient;
            if (commentClient == null)
                throw new MB.Util.APPException("客户端模块实现客户端需要实现 MB.WinDxChart.IFace.IChartTemplateClient 接口");

            return commentClient;
        }
    }
}
