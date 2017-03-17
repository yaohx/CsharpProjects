using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinDxChart.IFace;

namespace MB.WinDxChart.Chart
{
    public class ChartLayoutTemplateHelper
    {
        private static readonly string APP_CONFIG_KEY_NAME = "ChartLayoutTemplate";

        /// <summary>
        /// 客户端模块评语实现客户端。
        /// </summary>
        /// <returns></returns>
        public IChartLayoutTemplateClient CreateLayoutTemplateClient()
        {
            string cfgSetting = System.Configuration.ConfigurationSettings.AppSettings[APP_CONFIG_KEY_NAME];
            if (string.IsNullOrEmpty(cfgSetting)) return null;
            string[] cfgs = cfgSetting.Split(',');
            object instance = MB.Util.DllFactory.Instance.LoadObject(cfgs[0], cfgs[1]);
            if (instance == null)
                throw new MB.Util.APPException(string.Format("创建{0} , {1} 有误,请检查", cfgs[0], cfgs[1]));

            IChartLayoutTemplateClient commentClient = instance as IChartLayoutTemplateClient;
            if (commentClient == null)
                throw new MB.Util.APPException("客户端模块评语实现客户端需要实现 MB.WinDxChart.IFace.IChartLayoutTemplateClient 接口");

            return commentClient;
        }
    }
}
