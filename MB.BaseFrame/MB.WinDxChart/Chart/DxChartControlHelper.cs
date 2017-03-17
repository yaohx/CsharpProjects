//---------------------------------------------------------------- 
// Copyright (C) 2008-2012 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-11-30
// Description	:	DxChartControlHelper 操作可编辑的ChartControl 。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.Common;
using DevExpress.XtraCharts;
using System.Data;
using MB.WinBase.IFace;
using MB.Util;
using DevExpress.XtraLayout;
using MB.WinDxChart.IFace;
using MB.Util.Model.Chart;
using System.Collections;
using System.Windows.Forms;

namespace MB.WinDxChart.Chart
{
    public class DxChartControlHelper
    {
        #region Instance...
        private static Object _Obj = new object();
        private static DxChartControlHelper _Instance;


        ChartTemplateHelper _TemplateHelper;
        ChartLayoutTemplateHelper _LayoutTemplateHelper;

        IChartTemplateClient _TemplateClient;
        IChartLayoutTemplateClient _LayoutTemplateClient;
        
        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected DxChartControlHelper() {
            _TemplateHelper = new ChartTemplateHelper();
            _LayoutTemplateHelper = new ChartLayoutTemplateHelper();

            _TemplateClient = _TemplateHelper.CreateTemplateClient();
            _LayoutTemplateClient = _LayoutTemplateHelper.CreateLayoutTemplateClient();
        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static DxChartControlHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new DxChartControlHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region 绑定数据源


        public void BindingDxChartControl(DevExpress.XtraCharts.ChartControl chartControl, List<ChartSeriesInfo> seriesList)
        {
            try
            {
                if (seriesList != null && seriesList.Count > 0)
                {
                    foreach (ChartSeriesInfo seriesInfo in seriesList)
                    {
                        ViewType viewType = ViewType.Bar;

                        try {
                            viewType = (ViewType)Enum.Parse(typeof(ViewType), seriesInfo.ViewType, true);
                        }
                        catch {
                            //ignore the exception
                        }

                        DevExpress.XtraCharts.Series newSeries = new DevExpress.XtraCharts.Series(seriesInfo.Name, viewType);
                        if (!string.IsNullOrEmpty(seriesInfo.ArgumentDataMember))
                        {
                            newSeries.ArgumentDataMember = seriesInfo.ArgumentDataMember;
                            newSeries.ValueDataMembers.AddRange(seriesInfo.ValueDataMember);
                            newSeries.ValueScaleType = (ScaleType)Enum.Parse(typeof(ScaleType), seriesInfo.ViewType);
                            newSeries.View.Color = seriesInfo.Color;
                        }
                        else if (seriesInfo.SeriesPoints != null && seriesInfo.SeriesPoints.Count > 0)
                        {
                            foreach (ChartSeriesPointInfo point in seriesInfo.SeriesPoints)
                            {
                                newSeries.Points.Add(new SeriesPoint(point.Argument, point.ValueParams));
                            }
                        }

                        chartControl.Series.Add(newSeries);
                    }
                }

                chartControl.RefreshData();
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(ex.StackTrace);
                throw new MB.Util.APPException(ex.Message, Util.APPMessageType.SysErrInfo);
            }
        }

        public DataSet InvokeChartDataSource(string ruleName, string filterString)
        {
            string[] assembleName = ruleName.Split('~');
            IClientRuleQueryBase clientRule = (IClientRuleQueryBase)MB.Util.DllFactory.Instance.LoadObject(assembleName[0], assembleName[1] + ".dll");
            MB.Util.Model.QueryParameterInfo[] filterParams = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.DeSerializer(filterString);
            return clientRule.GetObjectAsDataSet(0, filterParams);
        }

        #endregion

        public byte[] GetChartTemplateFile(int id)
        {
            List<MB.Util.Model.QueryParameterInfo> filter = new List<Util.Model.QueryParameterInfo>();
            filter.Add(new Util.Model.QueryParameterInfo("ID", id, DataFilterConditions.Equal));

            List<ChartTemplateInfo> list = _TemplateClient.GetObjects(filter.ToArray());
            if (list != null && list.Count > 0) return list[0].TEMPLATE_FILE;
            return null;
        }

        public ChartTemplateInfo GetChartTemplateById(int id)
        {
            List<MB.Util.Model.QueryParameterInfo> filter = new List<Util.Model.QueryParameterInfo>();
            filter.Add(new Util.Model.QueryParameterInfo("ID", id, DataFilterConditions.Equal));

            List<ChartTemplateInfo> list = _TemplateClient.GetObjects(filter.ToArray());
            if (list != null && list.Count > 0) return list[0];
            return null;
        }

        public List<ChartTemplateInfo> GetChartTemplateByUser(string userCode)
        {
            return _TemplateClient.GetObjectByUser(userCode);
        }

        public List<ChartTemplateInfo> GetChartTemplateList(MB.Util.Model.QueryParameterInfo[] queryParams)
        {
            return _TemplateClient.GetObjects(queryParams);
        }

        public int SaveChartTemplate(ChartTemplateInfo entity)
        {
            return _TemplateClient.AddObject(entity);
        }

        public int SaveChartLayoutTemplate(ChartLayoutTemplateInfo entity, List<ChartLayoutItemInfo> items)
        {
            return _LayoutTemplateClient.AddObject(entity, items);
        }

        public List<ChartLayoutTemplateInfo> GetLayoutTemplateByUser(string userCode)
        {
            return _LayoutTemplateClient.GetObjectByUserCode(userCode);
        }

        public List<ChartLayoutItemInfo> GetObjectDetail(int id)
        {
            return _LayoutTemplateClient.GetObjectDetail(id);
        }

        public int DeleteTemplate(int id)
        {
            return _TemplateClient.DeleteObject(id);
        }
    }
}
