using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Dundas.Charting.WinControl;

using MB.Util;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.WinChart.DundasChart;
using MB.WinChart.IFace;
using MB.WinChart.Model;
using MB.WinChart.Resource;
using MB.WinChart.Share;
using System.Reflection;
using System.Threading;

namespace MB.WinChart
{
	/// <summary>
	/// 报表呈现窗口
	/// </summary>
	public partial class ChartView : UserControl, IChartViewControl
	{
		#region Private variables

		private DataSet _DataSet;
		private List<ColumnPropertyInfo> _ColumnPropertyInfos;
		private Chart _BaseChart;
		private ChartEventArgs _ChartEventArgs;
		private string _Identity;
		private PagerSettings _PagerSettings;
		private int _XValueCount;
		private ChartTypeInfo _DefaultChartType;
		//把值转换为描述。
		private Dictionary<string, Dictionary<string, string>> _EditDataHasTable;

		#endregion

		#region Construct

		/// <summary>
		/// 默认构造函数.
		/// </summary>
		public ChartView()
		{
			InitializeComponent();

			_BaseChart = new Chart();
			_BaseChart.Name = ChartViewStatic.CHART_NAME;
			_PagerSettings = new PagerSettings();
		}

		#endregion

		#region Events

		private void ChartView_Load(object sender, EventArgs e)
		{
			try
			{
				bindCombobox();
				drawChart();
			}
			catch(Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void frm_AfterDataApply(object sender, ChartEventArgs e)
		{
			try
			{
				_PagerSettings.PageIndex = 0;
				prepareChart(e, _PagerSettings);

				_ChartEventArgs = e;

				bindDefaultControl();
				drawChart();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void tsBtnOption_Click(object sender, EventArgs e)
		{
			try
			{
				ChartConfigure frm = new ChartConfigure(_DataSet.Tables[0].Columns, _ColumnPropertyInfos, _ChartEventArgs, _Identity);
				frm.AfterDataApply += new EventHandler<ChartEventArgs>(frm_AfterDataApply);
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void tsBtn3D_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				show3D();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void tsDbcboXAxisAngle_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				setXAxisLabelsAngle();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void dbcboPointStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ToolStripComboBox toolStripComboBox = sender as ToolStripComboBox;

				if (null != toolStripComboBox)
				{
					string percentItem = getMessage(ChartViewStatic.PERCENT);

					if (toolStripComboBox.SelectedItem.ToString() == percentItem)
					{
						setPointStyle(true);
					}
					else
					{
						setPointStyle(false);
					}
				}

				
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
		{
			try
			{
				_PagerSettings.PageIndex -= 1;
				showBindingNavigator(_PagerSettings);
				prepareChart(_ChartEventArgs, _PagerSettings);
				drawChart();

				setPageButton();
				bindDefaultControl();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
		{
			try
			{
				_PagerSettings.PageIndex += 1;
				showBindingNavigator(_PagerSettings);
				prepareChart(_ChartEventArgs, _PagerSettings);
				drawChart();

				setPageButton();
				bindDefaultControl();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void tsBtnShowValue_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				tsDbcboPointStyle.Enabled = tsBtnShowValue.Checked;
				showPointValue();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
		{
			try
			{
				_PagerSettings.PageIndex = 0;
				showBindingNavigator(_PagerSettings);
				prepareChart(_ChartEventArgs, _PagerSettings);
				drawChart();

				setPageButton();
				bindDefaultControl();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
		{
			try
			{
				_PagerSettings.PageIndex = _XValueCount / _PagerSettings.PageSize;
				showBindingNavigator(_PagerSettings);
				prepareChart(_ChartEventArgs, _PagerSettings);
				drawChart();

				setPageButton();
				bindDefaultControl();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void bindingNavigatorPositionItem_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				int pageIndex = 0;

				if (int.TryParse(bindingNavigatorPositionItem.Text.Trim(), out pageIndex))
				{
					if (pageIndex < 0)
					{
						pageIndex = 0;
					}
					if (pageIndex > _PagerSettings.PageCount)
					{
						pageIndex = _PagerSettings.PageCount;
					}

					_PagerSettings.PageIndex = pageIndex - 1;
					prepareChart(_ChartEventArgs, _PagerSettings);
					drawChart();

					showBindingNavigator(_PagerSettings);
					setPageButton();
					bindDefaultControl();
				}
				else if (string.IsNullOrEmpty(bindingNavigatorPositionItem.Text.Trim()))
				{
					pageIndex = 0;

					_PagerSettings.PageIndex = pageIndex;
					prepareChart(_ChartEventArgs, _PagerSettings);
					drawChart();

					showBindingNavigator(_PagerSettings);
					setPageButton();
					bindDefaultControl();
				}
				else
				{
					string errorMessaeg = getMessage(ChartViewStatic.ERROR_LEGAL_PAGEINDEX);
					APPException appException = new APPException(errorMessaeg, APPMessageType.DisplayToUser);
					MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(appException);
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		#endregion

		#region IChartViewControl 成员

		/// <summary>
		/// <see cref="MB.WinBase.IFace.CreateDataBinding"/>
		/// </summary>
		public void CreateDataBinding(IClientRuleConfig clientRule, DataSet dsData)
		{
			try
			{
				
				_DataSet = dsData;

				bool notNullDataSource = judgeIsNullDataSource(clientRule, dsData);

				if (notNullDataSource)
				{
					List<ColumnPropertyInfo> columnPropertyInfos = getColumnPropertyInfo(clientRule);
					_ColumnPropertyInfos = columnPropertyInfos;

					string identity = getIdentity(clientRule);
					_Identity = identity;

					if (null != _DefaultChartType)
					{
						bindDefaultChart(_DefaultChartType, dsData.Tables[0].Columns, _PagerSettings);
					}
					else
					{
						ChartViewHelper chartViewHelper = new ChartViewHelper();

						ChartEventArgs chartEventArgs = new ChartEventArgs();
						chartEventArgs.ChartType = SeriesChartType.Pie;
						_ChartEventArgs = chartEventArgs;

						List<ChartAreaInfo> chartAreaInfos = chartViewHelper.CreateChartData(dsData, columnPropertyInfos, chartEventArgs, _PagerSettings);

						_XValueCount = chartViewHelper.XValueCount;
						_PagerSettings.PageIndex = 0;
						_PagerSettings.PageCount = _XValueCount / _PagerSettings.PageSize + 1;
						showBindingNavigator(_PagerSettings);

						IChart chart = new PieChart(_BaseChart);
						chart.AreaDataList = chartAreaInfos;
					}

					setNavigationStatus(true);
					setPageButton();
				}
				else
				{
					setNavigationStatus(false);
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		/// <summary>
		/// <see cref="MB.WinBase.IFace.RefreshLayout"/>
		/// </summary>
		public void RefreshLayout()
		{
			try
			{
				drawChart();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		/// <summary>
		/// 默认模板配制.
		/// </summary>
		public ChartTypeInfo DefaultChartType
		{
			get
			{
				return _DefaultChartType;
			}
			set
			{
				_DefaultChartType = value;
			}
		}

        ///// <summary>
        /////  把数据实体集合类转换为 客户可分析DataSet 的格式。
        ///// </summary>
        ///// <param name="entitys">数据实体集.</param>
        ///// <param name="propertys">可配制的属性.</param>
        ///// <param name="columnsEdit">编辑属性.</param>
        ///// <returns>转化后的DataSet.</returns>
        //public DataSet ConvertEntityToDataSet(System.Collections.IList entitys, 
        //    Dictionary<string, ColumnPropertyInfo> propertys, Dictionary<string, ColumnEditCfgInfo> columnsEdit,)
        //{
        //    if (entitys == null || propertys == null) return null;
        //    _EditDataHasTable = new Dictionary<string, Dictionary<string, string>>();
        //    try
        //    {
        //        DataTable dtData = new DataTable();
        //        List<ColumnPropertyInfo> filterColumnPropertyInfos = new List<ColumnPropertyInfo>();

        //        foreach (ColumnPropertyInfo info in propertys.Values)
        //        {
        //            if (!string.IsNullOrEmpty(info.ChartDataType))
        //            {
        //                if (null != entitys && 0 != entitys.Count)
        //                {
        //                    if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entitys[0], info.Name))
        //                    {
        //                        string dataType = info.DataType;
        //                        if (columnsEdit != null && columnsEdit.ContainsKey(info.Name))
        //                            dataType = ChartViewStatic.SYSTEM_STRING;

        //                        dtData.Columns.Add(info.Name, MB.Util.General.CreateSystemType(dataType, false));

        //                        filterColumnPropertyInfos.Add(info);
        //                    }
        //                }
        //            }
        //        }

        //        Type t = entitys[0].GetType();
        //        foreach (object entity in entitys)
        //        {
        //            DataRow drNew = dtData.NewRow();
        //            dtData.Rows.Add(drNew);
        //            foreach (ColumnPropertyInfo info in filterColumnPropertyInfos)
        //            {
        //                var v = t.GetProperty(info.Name);
        //                object val = v.GetValue(entity, null);

        //                if (val == null || val == System.DBNull.Value) continue;

        //                if (columnsEdit != null && columnsEdit.ContainsKey(info.Name))
        //                {
        //                    drNew[info.Name] = convertValueToDescription(val, columnsEdit[info.Name]);
        //                }
        //                else
        //                {
        //                    drNew[info.Name] = MB.Util.MyConvert.Instance.ChangeType(val, dtData.Columns[info.Name].DataType);
        //                }
        //            }
        //        }

        //        DataSet dsData = new DataSet();
        //        dsData.Tables.Add(dtData);
        //        return dsData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MB.Util.APPException("执行 ConvertEntityToDataSet 出错" + ex.Message);
        //    }
        //}

		#region Add private method


		#endregion

		#endregion

		#region private methods

		private void bindDefaultChart(ChartTypeInfo chartTypeInfo, DataColumnCollection dataColumnCollection,
			PagerSettings pagerSettings)
		{
			ChartEventArgs chartEventArgs = new ChartEventArgs();

			ChartViewHelper chartViewHelper = new ChartViewHelper();

			chartEventArgs.ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), chartTypeInfo.DefaultChartType.ToString()) ;
			chartEventArgs.XAxis = chartViewHelper.GetDataColumn(chartTypeInfo.XAxis, dataColumnCollection);
			chartEventArgs.YAxis = chartViewHelper.GetDataColumn(chartTypeInfo.YAxis, dataColumnCollection);
			chartEventArgs.ZAxis = chartViewHelper.GetDataColumn(chartTypeInfo.ZAxis, dataColumnCollection);

			prepareChart(chartEventArgs, pagerSettings);
		}

		private void setChartStyle()
		{
			_BaseChart.BackColor = Color.FromArgb(
				ChartViewStatic.BACK_COLOR_RED, ChartViewStatic.BACK_COLOR_GREEN, ChartViewStatic.BACK_COLOR_BLUE);
			_BaseChart.BackGradientEndColor = System.Drawing.Color.White;
			_BaseChart.BackGradientType = GradientType.TopBottom;
			_BaseChart.BackgroundImageLayout = ImageLayout.Tile;
			_BaseChart.BorderLineColor = Color.FromArgb(
				ChartViewStatic.BORDER_LINE_COLOR_RED, ChartViewStatic.BORDER_LINE_COLOR_GREEN, ChartViewStatic.BORDER_LINE_COLOR_BLUE);
			_BaseChart.BorderLineStyle = ChartDashStyle.Solid;
			_BaseChart.BorderLineWidth = ChartViewStatic.BORDER_LINE_WIDTH;
			_BaseChart.Palette = ChartColorPalette.Pastel;
			_BaseChart.Dock = DockStyle.Fill;

			show3D();
		}

		private void showPointValue()
		{
			foreach (Series series in _BaseChart.Series)
			{
				series.ShowLabelAsValue = tsBtnShowValue.Checked;
			}
		}

		private void show3D()
		{
			foreach (ChartArea chartArea in _BaseChart.ChartAreas)
			{
				chartArea.Area3DStyle.Enable3D = tsBtn3D.Checked;
			}
		}

		private void bindTsDbcboXAxisAngle()
		{
			tsDbcboXAxisAngle.SelectedIndex = 0;
		}

		private void bindTsDbcboPointStyle()
		{
			tsDbcboPointStyle.SelectedIndex = 0;
		}

		private void bindCombobox()
		{
			bindTsDbcboXAxisAngle();
			bindTsDbcboPointStyle();
		}

		private void bindShow3D()
		{
			tsBtn3D.Checked = true;
		}

		private void bindTsBtnShowValue()
		{
			tsBtnShowValue.Checked = true;
			showPointValue();
		}

		private void bindDefaultControl()
		{
			bindCombobox();
			bindShow3D();
			bindTsBtnShowValue();
		}

		private void setXAxisLabelsAngle()
		{
			foreach (ChartArea chartArea in _BaseChart.ChartAreas)
			{
				chartArea.AxisX.LabelStyle.FontAngle = int.Parse(tsDbcboXAxisAngle.SelectedItem.ToString());
			}
		}

		private string getNumItem(int index)
		{
			string format = string.Empty;

			switch (index)
			{
				case 1:
					format = LabelStyle.C.ToString();
					break;
				case 2:
					format = LabelStyle.C0.ToString();
					break;
				case 3:
					format = LabelStyle.E.ToString();
					break;
				case 4:
					format = LabelStyle.E0.ToString();
					break;
				case 5:
					format = LabelStyle.P.ToString();
					break;
				default:
					format = string.Empty;
					break;
			}

			return format;
		}

		private void setPointStyle(bool isPercent)
		{
			if (null != _ChartEventArgs)
			{
				_ChartEventArgs.IsPercent = isPercent;
				prepareChart(_ChartEventArgs, _PagerSettings);
			}

			string format = getNumItem(tsDbcboPointStyle.SelectedIndex);

			foreach (Series series in _BaseChart.Series)
			{
				series.LabelFormat = format;
			}

			showPointValue();
			show3D();
		}

		private void drawChart()
		{
			setChartStyle();

			if (null != _DataSet)
			{
				if (this.panelChart.Controls.ContainsKey(_BaseChart.Name))
				{
					this.panelChart.Controls.RemoveByKey(_BaseChart.Name);
				}

				this.panelChart.Controls.AddRange(new System.Windows.Forms.Control[] { _BaseChart });
			}
		}

		private string getMessage(string key)
		{
			string message = string.Empty;

			ResourcesHelper resourcesHelper = new ResourcesHelper();
			message = resourcesHelper.GetMessage(key);

			return message;
		}

		private List<ColumnPropertyInfo> filterColumnPropertyInfos(List<ColumnPropertyInfo> columnPropertyInfos)
		{
			List<ColumnPropertyInfo> filtedColumnPropertyInfos = new List<ColumnPropertyInfo>();

			foreach (ColumnPropertyInfo columnPropertyInfo in columnPropertyInfos)
			{
				if (!string.IsNullOrEmpty(columnPropertyInfo.AllowChartAxes))
				{
					if (string.IsNullOrEmpty(columnPropertyInfo.ChartDataType))
					{
						string chartDataType = convertToChartDataType(columnPropertyInfo.DataType);

						columnPropertyInfo.ChartDataType = chartDataType;
					}

					filtedColumnPropertyInfos.Add(columnPropertyInfo);
				}
			}

			return filtedColumnPropertyInfos;
		}

		private List<ColumnPropertyInfo> getColumnPropertyInfo(IClientRuleConfig clientRule)
		{
			List<ColumnPropertyInfo> filtedColumnPropertyInfos = new List<ColumnPropertyInfo>();

			List<ColumnPropertyInfo> columnPropertyInfos = new List<ColumnPropertyInfo>();
			var cols = clientRule.UIRuleXmlConfigInfo.GetDefaultColumns();
			columnPropertyInfos.AddRange(cols.Values);

			filtedColumnPropertyInfos = filterColumnPropertyInfos(columnPropertyInfos);

			return filtedColumnPropertyInfos;
		}

		private string getIdentity(IClientRuleConfig clientRule)
		{
			string identity = string.Format("{0} {1}", clientRule.GetType().FullName, clientRule.ClientLayoutAttribute.UIXmlConfigFile);

			return identity;
		}

		private int[] getXCounts()
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			int[] xCounts = chartViewHelper.GetXCounts();

			return xCounts;
		}

		private string convertToChartDataType(string dataType)
		{
			string chartDataType = string.Empty;

			ChartViewHelper chartViewHelper = new ChartViewHelper();

			chartDataType = chartViewHelper.ConvertToChartDataType(dataType);

			return chartDataType;
		}

		private bool judgeIsNullDataSource(IClientRuleConfig clientRule, DataSet dsData)
		{
			bool result = false;
			if (clientRule == null)
			{
				string message = getMessage(ChartViewStatic.ERROR_NULL_CLIENTRULE);
				throw new MB.Util.APPException(message, APPMessageType.DisplayToUser);
			}

			if (dsData == null || dsData.Tables[0].Rows.Count == 0)
			{
				string message = getMessage(ChartViewStatic.ERROR_NULL_DATASET);
				throw new MB.Util.APPException(message, APPMessageType.DisplayToUser);
			}

			List<ColumnPropertyInfo> columnPropertyInfos = getColumnPropertyInfo(clientRule);

			if (0 != columnPropertyInfos.Count && 0 != dsData.Tables[0].Rows.Count)
			{
				result = true;
			}
			

			return result;
		}

		private void setPageButton()
		{
			if (_PagerSettings.PageIndex <= 0)
			{
				bindingNavigatorMovePreviousItem.Enabled = false;
				bindingNavigatorMoveFirstItem.Enabled = false;
			}
			else
			{
				bindingNavigatorMovePreviousItem.Enabled = true;
				bindingNavigatorMoveFirstItem.Enabled = true;
			}

			int nextPageIndex = (_PagerSettings.PageIndex + 1) * _PagerSettings.PageSize;
			if (nextPageIndex > _XValueCount)
			{
				bindingNavigatorMoveNextItem.Enabled = false;
				bindingNavigatorMoveLastItem.Enabled = false;
			}
			else
			{
				bindingNavigatorMoveNextItem.Enabled = true;
				bindingNavigatorMoveLastItem.Enabled = true;
			}
		}

		private void prepareChart(ChartEventArgs e, PagerSettings pagerSettings)
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			List<ChartAreaInfo> chartAreaInfos = chartViewHelper.CreateChartData(
				_DataSet, _ColumnPropertyInfos, e, pagerSettings);

			_XValueCount = chartViewHelper.XValueCount;
			pagerSettings.PageCount = _XValueCount / pagerSettings.PageSize + 1;
			showBindingNavigator(_PagerSettings);

			if (e.ChartType == SeriesChartType.Pie)
			{
				using (IChart chart = new PieChart(_BaseChart))
				{
					chart.IsPercent = e.IsPercent;
					chart.AreaDataList = chartAreaInfos;
				}
			}

			bool isMultiChartArea = false;
			if (e.AnalyseData == ChartViewStatic.R_BTN_COMPARE_NAME)
			{
				isMultiChartArea = true;
			}

			if (e.ChartType == SeriesChartType.Line)
			{
				using (IChart chart = new LineChart(_BaseChart, e.NeedSortXAxis, isMultiChartArea))
				{
					chart.IsPercent = e.IsPercent;
					chart.AreaDataList = chartAreaInfos;
				}
			}

			if (e.ChartType == SeriesChartType.Column)
			{
				using (IChart chart = new ColumnChart(_BaseChart, e.NeedSortXAxis, isMultiChartArea))
				{
					chart.IsPercent = e.IsPercent;
					chart.AreaDataList = chartAreaInfos;
				}
			}

			_ChartEventArgs = e;
		}

		private void showBindingNavigatorPositionItem(int pageIndex)
		{
			bindingNavigatorPositionItem.Text = pageIndex.ToString();
		}

		private void showBindingNavigatorCountItem(int pageCount)
		{
			bindingNavigatorCountItem.Text = pageCount.ToString();
		}

		private void showBindingNavigator(PagerSettings pagerSettings)
		{
			showBindingNavigatorPositionItem(pagerSettings.ShowPageIndex);
			showBindingNavigatorCountItem(pagerSettings.PageCount);
		}

		private void tsBtnExport_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = ChartViewStatic.EXPORT_SUFFIX;
				var dre = sfd.ShowDialog();
				string fileName = sfd.FileName;
				if (!string.IsNullOrEmpty(fileName))
				{
					Bitmap bitmap = new Bitmap(ChartViewStatic.EXPORT_SIZE_X, ChartViewStatic.EXPORT_SIZE_Y);

					_BaseChart.DrawToBitmap(bitmap, new Rectangle(ChartViewStatic.EXPORT_LOCATE_X, ChartViewStatic.EXPORT_LOCATE_Y,
						ChartViewStatic.EXPORT_WIDTH, ChartViewStatic.EXPORT_HEIGHT));

					bitmap.Save(fileName);
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void setNavigationStatus(bool status)
		{
			bindingNavigatorMoveFirstItem.Enabled = status;
			bindingNavigatorMovePreviousItem.Enabled = status;
			bindingNavigatorPositionItem.Enabled = status;
			bindingNavigatorMoveNextItem.Enabled = status;
			bindingNavigatorMoveLastItem.Enabled = status;
			tsBtnShowValue.Enabled = status;
			tsDbcboXAxisAngle.Enabled = status;
			tsBtnOption.Enabled = status;
			tsBtnExport.Enabled = status;
		}

		private string convertValueToDescription(object val, ColumnEditCfgInfo lookUpCol)
		{
			if (val == null || lookUpCol.DataSource == null) return string.Empty;
			if (!_EditDataHasTable.ContainsKey(lookUpCol.Name))
			{
				DataTable dt = MB.Util.MyConvert.Instance.ToDataTable(lookUpCol.DataSource, string.Empty);
				Dictionary<string, string> hasData = new Dictionary<string, string>();
				DataRow[] drs = dt.Select();
				foreach (DataRow dr in drs)
				{
					hasData.Add(dr[lookUpCol.ValueFieldName].ToString(), dr[lookUpCol.TextFieldName].ToString());
				}
				_EditDataHasTable.Add(lookUpCol.Name, hasData);
			}
			var temp = _EditDataHasTable[lookUpCol.Name];
			if (temp.ContainsKey(val.ToString()))
				return temp[val.ToString()];
			else
				return val.ToString();
		}

		#region Igonre PageCounts

		//private void tsDbcboXAxisCounts_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    ToolStripComboBox toolStripComboBox = sender as ToolStripComboBox;

		//    if (null != toolStripComboBox)
		//    {
		//        int pageSize = int.Parse(toolStripComboBox.SelectedItem.ToString());

		//        _PagerSettings.PageSize = pageSize;

		//        if (null != _ChartEventArgs)
		//        {
		//            prepareChart(_ChartEventArgs, _PagerSettings);

		//            setPageButton();
		//        }
		//    }
		//}

		//private void bindTsDbcboXAxisCounts()
		//{
		//    tsDbcboXAxisCounts.Items.Clear();

		//    int[] xCounts = getXCounts();

		//    foreach (int xCount in xCounts)
		//    {
		//        tsDbcboXAxisCounts.Items.Add(xCount.ToString());
		//    }

		//    tsDbcboXAxisCounts.SelectedIndex = 0;
		//}

		#endregion

		#endregion
	}
}
