using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MB.WinClientDefault.Common {
    /// <summary>
    /// 图表分析帮助处理
    /// </summary>
   public class ChartAnalyzeHelper {
       private static readonly string DEFAULT_SETTING = "MB.WinChart.ChartView,MB.WinChart.dll";
       private static string _ActiveSetting = DEFAULT_SETTING;

       /// <summary>
       /// 当前活动的配置信息。
       /// </summary>
       public static string ActiveSetting {
           get {
               return _ActiveSetting;
           }
           set {
               _ActiveSetting = value;
           }
       }
       /// <summary>
       /// Instance.
       /// </summary>
       public static ChartAnalyzeHelper Instance {
           get {
               return MB.Util.SingletonProvider<ChartAnalyzeHelper>.Instance;  
           }
       }
       /// <summary>
       /// 创建默认设置的图表控件。
       /// </summary>
       /// <returns></returns>
       public MB.WinBase.IFace.IChartViewControl CreateDefaultChartControl() {
           if (string.IsNullOrEmpty(ChartAnalyzeHelper.ActiveSetting))
               throw new MB.Util.APPException("获取不到当前活动的图表配置信息");

           string[] cfgs = ChartAnalyzeHelper.ActiveSetting.Split(',');
           object chartControl = MB.Util.DllFactory.Instance.LoadObject(cfgs[0], cfgs[1]);
           if (chartControl == null)
               throw new MB.Util.APPException("创建图表分析控件有误");

           MB.WinBase.IFace.IChartViewControl chartView = chartControl as MB.WinBase.IFace.IChartViewControl;
           if (chartView == null)
               throw new MB.Util.APPException("所有图表分析必须实现MB.WinBase.IFace.IChartViewControl 接口");
           return chartView;
       }
    }
}
