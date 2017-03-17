using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

using Dundas.Charting.WinControl;

using MB.WinBase;
using MB.WinBase.Common;
using MB.WinChart.Model;
using MB.WinChart.Resource;
using MB.Util;
using MB.Util.Serializer;

namespace MB.WinChart.Share
{
	/// <summary>
	/// 报表功能共公类.
	/// </summary>
	public class ChartViewHelper
	{
		#region Property

		private int _XValueCount;

		/// <summary>
		/// XValue数量.
		/// </summary>
		public int XValueCount
		{
		  get { return _XValueCount; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 建立报表标准数据源.
		/// </summary>
		/// <param name="dataSet">数据源.</param>
		/// <param name="columnPropertyInfos">配制源.</param>
		/// <param name="chartEventArgs">报表事件信息.</param>
		/// <param name="pagerSettings">翻页信息.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>配制成功的数据源.</returns>
		public List<ChartAreaInfo> CreateChartData(
			DataSet dataSet, List<ColumnPropertyInfo> columnPropertyInfos,
			ChartEventArgs chartEventArgs, PagerSettings pagerSettings)
		{
			try
			{
				List<ChartAreaInfo> chartAreaInfos = new List<ChartAreaInfo>();
				DataColumnCollection dataColumnCollection = dataSet.Tables[0].Columns;
				DataColumn dataColumnX = chartEventArgs.XAxis;
				DataColumn dataColumnY = chartEventArgs.YAxis;
				DataColumn dataColumnZ = chartEventArgs.ZAxis;

				if (null == chartEventArgs.YAxis)
				{
					dataColumnY = GetDefaultDataColumnY(columnPropertyInfos, dataColumnCollection);
				}

				if (null == chartEventArgs.XAxis)
				{
					dataColumnX = GetDefaultDataColumnX(columnPropertyInfos, dataColumnCollection);
				}

				chartEventArgs.NeedSortXAxis = needBeSort(dataColumnX, columnPropertyInfos);

				if (null != chartEventArgs.ZAxis)
				{
					dataSet = countDataSetByDataColumns(dataColumnX, dataColumnY, dataColumnZ, dataSet);

					if (SeriesChartType.Pie == chartEventArgs.ChartType)
					{
						chartAreaInfos = createMultiChartAreaData(dataSet, dataColumnX, dataColumnY, dataColumnZ, pagerSettings);
					}
					if (SeriesChartType.Line == chartEventArgs.ChartType || SeriesChartType.Column == chartEventArgs.ChartType)
					{
						if (chartEventArgs.AnalyseData == ChartViewStatic.R_BTN_COMPARE_NAME)
						{
							chartAreaInfos = createMultiChartAreaData(dataSet, dataColumnX, dataColumnY, dataColumnZ, pagerSettings);
						}
						else
						{
							chartAreaInfos = createLineOrColumnChartData(dataSet, dataColumnX, dataColumnY, dataColumnZ, pagerSettings);
						}
					}
				}
				else
				{
					dataSet = countDataSetByDataColumns(dataColumnX, dataColumnY, null, dataSet);

					chartAreaInfos = createChartData(dataSet, dataColumnX, dataColumnY, pagerSettings);
				}

				bool needConvertToShortDateTime = false;

				foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
				{
					if (innerColumnPropertyInfo.ChartDataType == ChartViewStatic.DATE)
					{
						needConvertToShortDateTime = true;
						break;
					}
				}

				if (needConvertToShortDateTime)
				{
					formatChartAreaInfo(chartAreaInfos);
				}

				return chartAreaInfos;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取报表类型数组.
		/// </summary>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>报表类型数组.</returns>
		public string[] GetChartTypeItems()
		{
			try
			{
				string itemPie = getMessage(ChartViewStatic.ITEM_PIE_KEY);
				string itemLine = getMessage(ChartViewStatic.ITEM_LINE_KEY);
				string itemColumn = getMessage(ChartViewStatic.ITEM_COLUMN_KEY);

				return new string[] { itemPie, itemLine, itemColumn };
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 根据报表类型名获取报表类型.
		/// </summary>
		/// <param name="chartType">报表类型名.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>报表类型</returns>
		public SeriesChartType GetChartType(string chartType)
		{
			try
			{
				SeriesChartType seriesChartType = SeriesChartType.Pie;

				string[] items = GetChartTypeItems();

				if (chartType == items[0])
				{
					seriesChartType = SeriesChartType.Pie;
				}
				if (chartType == items[1])
				{
					seriesChartType = SeriesChartType.Line;
				}
				if (chartType == items[2])
				{
					seriesChartType = SeriesChartType.Column;
				}

				return seriesChartType;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 根据报表类型获取报表类型名.
		/// </summary>
		/// <param name="seriesChartType">报表类型.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>报表类型名.</returns>
		public string GetChartTypeItem(SeriesChartType seriesChartType)
		{
			try
			{
				string item = string.Empty;

				string[] items = GetChartTypeItems();

				if (SeriesChartType.Pie == seriesChartType)
				{
					item = items[0];
				}
				if (SeriesChartType.Line == seriesChartType)
				{
					item = items[1];
				}
				if (SeriesChartType.Column == seriesChartType)
				{
					item = items[2];
				}

				return item;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 根据报表类型名获取显示在UI上的报表类型名称.
		/// </summary>
		/// <param name="seriesChartTypeName">报表类型名.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>显示在UI上的报表类型名称.</returns>
		public string GetChartTypeItem(string seriesChartTypeName)
		{
			try
			{
				string item = string.Empty;

				string[] items = GetChartTypeItems();

				if (SeriesChartType.Pie.ToString() == seriesChartTypeName)
				{
					item = items[0];
				}
				if (SeriesChartType.Line.ToString() == seriesChartTypeName)
				{
					item = items[1];
				}
				if (SeriesChartType.Column.ToString() == seriesChartTypeName)
				{
					item = items[2];
				}

				return item;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取数据列.
		/// </summary>
		/// <param name="name">数据列名.</param>
		/// <param name="dataColumnCollection">数据列集.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>数据列.</returns>
		public DataColumn GetDataColumn(string name, DataColumnCollection dataColumnCollection)
		{
			try
			{
				DataColumn dataColumn = null;

				foreach (DataColumn dc in dataColumnCollection)
				{
					if (dc.ColumnName == name)
					{
						dataColumn = dc;
						break;
					}
				}

				return dataColumn;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 生成报表颜色数组.
		/// </summary>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>报表颜色数组.</returns>
		public Color[] GenerateColors()
		{
			try
			{
				Color[] colors = new Color[ChartViewStatic.COLOR_COUNTS];

				colors[0] = Color.FromArgb(ChartViewStatic.RED);
				colors[1] = Color.FromArgb(ChartViewStatic.GREEN);
				colors[2] = Color.FromArgb(ChartViewStatic.BLUE);
				colors[3] = Color.FromArgb(ChartViewStatic.YELLOW);
				colors[4] = Color.FromArgb(ChartViewStatic.SILVER);
				colors[5] = Color.FromArgb(ChartViewStatic.LIME);
				colors[6] = Color.FromArgb(ChartViewStatic.CYAN);
				colors[7] = Color.FromArgb(ChartViewStatic.FUCHSIA);
				colors[8] = Color.FromArgb(ChartViewStatic.GRAY);
				colors[9] = Color.FromArgb(ChartViewStatic.MAROON);
				colors[10] = Color.FromArgb(ChartViewStatic.OLIVE);
				colors[11] = Color.FromArgb(ChartViewStatic.COLOR_RED_1, ChartViewStatic.COLOR_GREEN_1, ChartViewStatic.COLOR_BLUE_1);
				colors[12] = Color.FromArgb(ChartViewStatic.COLOR_RED_2, ChartViewStatic.COLOR_GREEN_2, ChartViewStatic.COLOR_BLUE_2);
				colors[13] = Color.FromArgb(ChartViewStatic.COLOR_RED_3, ChartViewStatic.COLOR_GREEN_3, ChartViewStatic.COLOR_BLUE_3);
				colors[14] = Color.FromArgb(ChartViewStatic.COLOR_RED_4, ChartViewStatic.COLOR_GREEN_4, ChartViewStatic.COLOR_BLUE_4);
				colors[15] = Color.FromArgb(ChartViewStatic.COLOR_RED_5, ChartViewStatic.COLOR_GREEN_5, ChartViewStatic.COLOR_BLUE_5);
				colors[16] = Color.FromArgb(ChartViewStatic.COLOR_RED_6, ChartViewStatic.COLOR_GREEN_6, ChartViewStatic.COLOR_BLUE_6);
				colors[17] = Color.FromArgb(ChartViewStatic.COLOR_RED_7, ChartViewStatic.COLOR_GREEN_7, ChartViewStatic.COLOR_BLUE_7);
				colors[18] = Color.FromArgb(ChartViewStatic.COLOR_RED_8, ChartViewStatic.COLOR_GREEN_8, ChartViewStatic.COLOR_BLUE_8);
				colors[19] = Color.FromArgb(ChartViewStatic.COLOR_RED_9, ChartViewStatic.COLOR_GREEN_9, ChartViewStatic.COLOR_BLUE_9);
				colors[20] = Color.FromArgb(ChartViewStatic.COLOR_RED_10, ChartViewStatic.COLOR_GREEN_10, ChartViewStatic.COLOR_BLUE_10);
				colors[21] = Color.FromArgb(ChartViewStatic.COLOR_RED_11, ChartViewStatic.COLOR_GREEN_11, ChartViewStatic.COLOR_BLUE_11);
				colors[22] = Color.FromArgb(ChartViewStatic.COLOR_RED_12, ChartViewStatic.COLOR_GREEN_12, ChartViewStatic.COLOR_BLUE_12);
				colors[23] = Color.FromArgb(ChartViewStatic.COLOR_RED_13, ChartViewStatic.COLOR_GREEN_13, ChartViewStatic.COLOR_BLUE_13);
				colors[24] = Color.FromArgb(ChartViewStatic.COLOR_RED_14, ChartViewStatic.COLOR_GREEN_14, ChartViewStatic.COLOR_BLUE_14);
				colors[25] = Color.FromArgb(ChartViewStatic.COLOR_RED_15, ChartViewStatic.COLOR_GREEN_15, ChartViewStatic.COLOR_BLUE_15);
				colors[26] = Color.FromArgb(ChartViewStatic.COLOR_RED_16, ChartViewStatic.COLOR_GREEN_16, ChartViewStatic.COLOR_BLUE_16);
				colors[27] = Color.FromArgb(ChartViewStatic.COLOR_RED_17, ChartViewStatic.COLOR_GREEN_17, ChartViewStatic.COLOR_BLUE_17);
				colors[28] = Color.FromArgb(ChartViewStatic.COLOR_RED_18, ChartViewStatic.COLOR_GREEN_18, ChartViewStatic.COLOR_BLUE_18);
				colors[29] = Color.FromArgb(ChartViewStatic.COLOR_RED_19, ChartViewStatic.COLOR_GREEN_19, ChartViewStatic.COLOR_BLUE_19);
				colors[30] = Color.FromArgb(ChartViewStatic.COLOR_RED_20, ChartViewStatic.COLOR_GREEN_20, ChartViewStatic.COLOR_BLUE_20);
				colors[31] = Color.FromArgb(ChartViewStatic.COLOR_RED_21, ChartViewStatic.COLOR_GREEN_21, ChartViewStatic.COLOR_BLUE_21);
				colors[32] = Color.FromArgb(ChartViewStatic.COLOR_RED_22, ChartViewStatic.COLOR_GREEN_22, ChartViewStatic.COLOR_BLUE_22);
				colors[33] = Color.FromArgb(ChartViewStatic.COLOR_RED_23, ChartViewStatic.COLOR_GREEN_23, ChartViewStatic.COLOR_BLUE_23);
				colors[34] = Color.FromArgb(ChartViewStatic.COLOR_RED_24, ChartViewStatic.COLOR_GREEN_24, ChartViewStatic.COLOR_BLUE_24);
				colors[35] = Color.FromArgb(ChartViewStatic.COLOR_RED_25, ChartViewStatic.COLOR_GREEN_25, ChartViewStatic.COLOR_BLUE_25);

				return colors;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取默认的Y轴数据列.
		/// </summary>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <param name="dataColumnCollection">数据列集.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>默认的Y轴数据列.</returns>
		public DataColumn GetDefaultDataColumnY(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			try
			{
				DataColumn dataColumnY = new DataColumn();

				ColumnPropertyInfo columnPropertyInfo = null;
				foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
				{

					bool result = JudgeIsAllowedYAxes(innerColumnPropertyInfo.AllowChartAxes);
					if (result)
					{
						columnPropertyInfo = innerColumnPropertyInfo;
						break;
					}
				}

				if (null != columnPropertyInfo)
				{
					foreach (DataColumn dc in dataColumnCollection)
					{
						if (dc.ColumnName == columnPropertyInfo.Name)
						{
							dataColumnY = dc;
						}
					}
				}

				return dataColumnY;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取默认的X轴数据列.
		/// </summary>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <param name="dataColumnCollection">数据列集.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>默认的X轴数据列.</returns>
		public DataColumn GetDefaultDataColumnX(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			try
			{
				DataColumn dataColumnX = new DataColumn();

				ColumnPropertyInfo columnPropertyInfo = null;
				foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
				{
					bool result = JudgeIsAllowedXAxes(innerColumnPropertyInfo.AllowChartAxes);
					if (result)
					{
						columnPropertyInfo = innerColumnPropertyInfo;
						break;
					}
				}

				if (null != columnPropertyInfo)
				{
					foreach (DataColumn dc in dataColumnCollection)
					{
						if (dc.ColumnName == columnPropertyInfo.Name)
						{
							dataColumnX = dc;
							break;
						}
					}
				}

				return dataColumnX;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取默认的时间类型X轴数据列.
		/// </summary>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <param name="dataColumnCollection">数据列集.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>默认的时间类型X轴数据列.</returns>
		public DataColumn GetDateTimeDataColumnX(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			try
			{
				DataColumn dataColumnX = new DataColumn();

				ColumnPropertyInfo columnPropertyInfo = null;
				foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
				{
					bool result = JudgeIsAllowedXAxes(innerColumnPropertyInfo.AllowChartAxes);
					if (result && ChartDataType.Date.ToString() == innerColumnPropertyInfo.ChartDataType)
					{
						columnPropertyInfo = innerColumnPropertyInfo;
						break;
					}
				}

				if (null != columnPropertyInfo)
				{
					foreach (DataColumn dc in dataColumnCollection)
					{
						if (dc.ColumnName == columnPropertyInfo.Name)
						{
							dataColumnX = dc;
							break;
						}
					}
				}

				return dataColumnX;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取下一个X轴数据列.
		/// </summary>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <param name="dataColumnCollection">数据列集.</param>
		/// <param name="selectedDataColumn">当前选择列.</param>
		///<exception cref="MB.Util.APPException"/>
		/// <returns>下一个X轴数据列.</returns>
		public DataColumn GetNextDataColumnX(
		    List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection, DataColumn selectedDataColumn)
		{
		    try
		    {
		        DataColumn nextDataColumn = null;

		        int startIndex = 0;
		        for (int i = 0; i < columnPropertyInfos.Count; i++)
		        {
		            if (columnPropertyInfos[i].Name == selectedDataColumn.ColumnName)
		            {
		                startIndex = i;
		                break;
		            }
		        }

		        ColumnPropertyInfo columnPropertyInfo = null;
		        for (int i = startIndex + 1; i < columnPropertyInfos.Count; i++)
		        {
					bool isAllowedXAxes = JudgeIsAllowedXAxes(columnPropertyInfos[i].AllowChartAxes);

					if (isAllowedXAxes)
					{
						columnPropertyInfo = columnPropertyInfos[i];
						break;
					}
		        }

				if (null != columnPropertyInfo)
				{
					foreach (DataColumn dc in dataColumnCollection)
					{
						if (dc.ColumnName == columnPropertyInfo.Name)
						{
							nextDataColumn = dc;
							break;
						}
					}
				}

				return nextDataColumn;
		    }
		    catch (Exception ex)
		    {
		        string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
		    }
		}

		/// <summary>
		/// 获取默认的Z轴数据列.
		/// </summary>
		/// <param name="dataColumnCollection">数据列集.</param>
		/// <param name="dataColumnX">X轴数据列.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>默认的Z轴数据列.</returns>
		public DataColumn GetDefaultDataColumnZ(DataColumnCollection dataColumnCollection, DataColumn dataColumnX)
		{
			try
			{
				DataColumn dataColumnZ = new DataColumn();

				foreach (DataColumn dc in dataColumnCollection)
				{
					if (dc.DataType.ToString() == ChartViewStatic.SYSTEM_STRING ||
						dc.DataType.ToString() == ChartViewStatic.SYSTEM_DATETIME)
					{
						if (dataColumnX != dc)
						{
							dataColumnZ = dc;
							break;
						}
					}
				}

				return dataColumnZ;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取默认的Z轴数据列.
		/// </summary>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <param name="dataColumnCollection">数据列集.</param>
		/// <param name="dataColumnX">X轴数据列.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>默认的Z轴数据列.</returns>
		public DataColumn GetDefaultDataColumnZ(
			List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection, DataColumn dataColumnX)
		{
			try
			{
				DataColumn dataColumnZ = new DataColumn();

				ColumnPropertyInfo columnPropertyInfo = null;
				foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
				{
					bool result = JudgeIsAllowedZAxes(innerColumnPropertyInfo.AllowChartAxes);
					
					if (result && (innerColumnPropertyInfo.Name != dataColumnX.ColumnName))
					{
						columnPropertyInfo = innerColumnPropertyInfo;
						break;
					}
				}

				if (null != columnPropertyInfo)
				{
					foreach (DataColumn dc in dataColumnCollection)
					{
						if (dc.ColumnName == columnPropertyInfo.Name)
						{
							dataColumnZ = dc;
							break;
						}
					}
				}

				return dataColumnZ;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取模板文件夹全名.
		/// </summary>
		/// <returns>取得的文件夹全名.</returns>
		public string GetFullPath()
		{
			try
			{
				string fullPath = string.Empty;

				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(MB.Util.General.GeApplicationDirectory())
					.Append(ChartViewStatic.TEMPLATE_FOLDER)
					.Append(ChartViewStatic.PATH);

				fullPath = stringBuilder.ToString();

				return fullPath;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取模板全名.
		/// </summary>
		/// <param name="module">模板名.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>模板全名.</returns>
		public string GetFullTemplateFileName(string module)
		{
			try
			{
				string fullFileName = string.Empty;

				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(MB.Util.General.GeApplicationDirectory())
					.Append(ChartViewStatic.TEMPLATE_FOLDER)
					.Append(ChartViewStatic.PATH)
					.Append(module)
					.Append(ChartViewStatic.XML_SUFFIX);

				fullFileName = stringBuilder.ToString();

				return fullFileName;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 判断是否允许设置为Y轴.
		/// </summary>
		/// <param name="allowedAxes">允许设置的轴.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>是否允许设置为Y轴.</returns>
		public bool JudgeIsAllowedYAxes(string allowedAxes)
		{
			try
			{
				bool result = false;

				AllowChartAxes compareAllowedAxes = convertToAllowedAxes(allowedAxes);

				if (AllowChartAxes.Y == (compareAllowedAxes & AllowChartAxes.Y))
				{
					result = true;
				}

				return result;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 判断是否允许设置为X轴.
		/// </summary>
		/// <param name="allowedAxes">允许设置的轴.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>是否允许设置为X轴.</returns>
		public bool JudgeIsAllowedXAxes(string allowedAxes)
		{
			try
			{
				bool result = false;

				AllowChartAxes compareAllowedAxes = convertToAllowedAxes(allowedAxes);

				if (AllowChartAxes.X == (compareAllowedAxes & AllowChartAxes.X))
				{
					result = true;
				}

				return result;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 判断是否允许设置为Z轴.
		/// </summary>
		/// <param name="allowedAxes">允许设置的轴.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>是否允许设置为Z轴.</returns>
		public bool JudgeIsAllowedZAxes(string allowedAxes)
		{
			try
			{
				bool result = false;

				AllowChartAxes compareAllowedAxes = convertToAllowedAxes(allowedAxes);

				if (AllowChartAxes.Z == (compareAllowedAxes & AllowChartAxes.Z))
				{
					result = true;
				}

				return result;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}
		
		/// <summary>
		/// 获取当前模块的模板信息集.
		/// </summary>
		/// <param name="module">模块名.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>模板信息集.</returns>
		public List<TemplateInfo> GetTemplateInfos(string module)
		{
			try
			{
				List<TemplateInfo> templateInfos = new List<TemplateInfo>();

				string fileName = GetFullTemplateFileName(module);

				if (File.Exists(fileName))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(fileName);

					EntityXmlSerializer<TemplateInfo> entityXmlSerializer = new EntityXmlSerializer<TemplateInfo>();

					templateInfos = entityXmlSerializer.DeSerializer(xmlDocument.InnerXml);
					templateInfos.Sort(new TemplateInfoDataCompare());
					templateInfos.Reverse();
				}

				return templateInfos;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取配制项.
		/// </summary>
		/// <param name="name">配制项名.</param>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>配制项.</returns>
		public ColumnPropertyInfo GetColumnPropertyInfo(string name, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			try
			{
				ColumnPropertyInfo columnPropertyInfo = null;

				foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
				{
					if (innerColumnPropertyInfo.Name == name)
					{
						columnPropertyInfo = innerColumnPropertyInfo;
						break;
					}
				}

				return columnPropertyInfo;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 转化为<c>ShortDate</c>.
		/// </summary>
		/// <param name="value">字符串.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns><c>ShortDate</c>.</returns>
		public string ConvertToShortDateString(string value)
		{
			try
			{
				string result = value;

				DateTime dateTime = DateTime.Now;
				if (DateTime.TryParse(value, out dateTime))
				{
					result = dateTime.ToShortDateString();
				}

				return result;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取配制项名.
		/// </summary>
		/// <param name="description">描述.</param>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>配制项名.</returns>
		public string GetColumnPropertyInfoName(string description, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			try
			{
				string name = string.Empty;

				foreach (ColumnPropertyInfo columnPropertyInfo in columnPropertyInfos)
				{
					if (columnPropertyInfo.Description == description)
					{
						name = columnPropertyInfo.Name;
						break;
					}
				}

				return name;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取配制项描述.
		/// </summary>
		/// <param name="columnName">配制项名.</param>
		/// <param name="columnPropertyInfos">配制集.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>配制项描述.</returns>
		public string GetDescription(string columnName, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			try
			{
				string description = string.Empty;

				foreach (ColumnPropertyInfo columnPropertyInfo in columnPropertyInfos)
				{
					if (columnPropertyInfo.Name == columnName)
					{
						description = columnPropertyInfo.Description;
						break;
					}
				}

				return description;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 获取X轴数量数组.
		/// </summary>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>X轴数量数组.</returns>
		public int[] GetXCounts()
		{
			try
			{
				return new int[] { ChartViewStatic.DEFAULT_XCOUNTS, 
					ChartViewStatic.XCOUNTS_10, ChartViewStatic.XCOUNTS_15,
					ChartViewStatic.XCOUNTS_25, ChartViewStatic.XCOUNTS_30 };
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		/// <summary>
		/// 转化为图表数据类型.
		/// </summary>
		/// <param name="dataType">默认数据类型.</param>
		/// <exception cref="MB.Util.APPException"/>
		/// <returns>图表数据类型.</returns>
		public string ConvertToChartDataType(string dataType)
		{
			try
			{
				string chartDataType = string.Empty;

				if (dataType.Contains(ChartViewStatic.INT) || dataType.Contains(ChartViewStatic.DECIMAL))
				{
					chartDataType = ChartDataType.Number.ToString();
				}
				if (dataType.Contains(ChartViewStatic.DATE))
				{
					chartDataType = ChartDataType.Date.ToString();
				}
				if (dataType.Contains(ChartViewStatic.STRING))
				{
					chartDataType = ChartDataType.String.ToString();
				}

				return chartDataType;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		public AllowChartAxes ConvertToSingleAllowedAxes(string allowedAxes)
		{
			string[] allowedAxesArray = allowedAxes.Split(ChartViewStatic.SPLITE_ALLOWED_AXES);
			AllowChartAxes convertedAllowedAxes = AllowChartAxes.None;

			if (1 == allowedAxesArray.Length)
			{
				convertedAllowedAxes = (AllowChartAxes)Enum.Parse(typeof(AllowChartAxes), allowedAxesArray[0]);
			}

			return convertedAllowedAxes;
		}

		#endregion

		#region Private methods

		private string getMessage(string key)
		{
			string message = string.Empty;

			ResourcesHelper resourcesHelper = new ResourcesHelper();
			message = resourcesHelper.GetMessage(key);

			return message;
		}

		private string getClientMsg(string methodName, Exception e)
		{
			string clientMsg = string.Empty;

			StringBuilder stringBuilder = new StringBuilder();

			clientMsg = stringBuilder.Append(GetType().Name)
				.Append(ChartViewStatic.DOT)
				.Append(methodName)
				.Append(ChartViewStatic.COLON)
				.Append(e.Message).ToString();

			return clientMsg;
		}

		private Dictionary<string, List<Dictionary<string, ArrayList>>> getDictionary(
			DataSet dataSet, DataColumn dataColumnX, DataColumn dataColumnY, DataColumn dataColumnZ)
		{
			Dictionary<string, List<Dictionary<string, ArrayList>>> dictionary =
				new Dictionary<string, List<Dictionary<string, ArrayList>>>();

			int indexZ = dataSet.Tables[0].Columns.IndexOf(dataColumnZ.ColumnName);
			int indexX = dataSet.Tables[0].Columns.IndexOf(dataColumnX.ColumnName);
			int indexY = dataSet.Tables[0].Columns.IndexOf(dataColumnY.ColumnName);

			List<string> columnZValues = new List<string>();
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				if (!columnZValues.Contains(row[indexZ].ToString()))
				{
					columnZValues.Add(row[indexZ].ToString());
				}
			}

			List<Dictionary<string, ArrayList>> listDictionary = null;
			Dictionary<string, ArrayList> innerDictionary = null;

			foreach (string columnZValue in columnZValues)
			{
				listDictionary = new List<Dictionary<string, ArrayList>>();

				List<string> columnXValues = new List<string>();
				string filterString = string.Format("{0} ='{1}'", dataSet.Tables[0].Columns[indexZ].ColumnName, columnZValue);
				DataRow[] drsFilterX = dataSet.Tables[0].Select(filterString);
				
				foreach (DataRow row in drsFilterX)
				{
					if (!columnXValues.Contains(row[indexX].ToString()))
					{
						columnXValues.Add(row[indexX].ToString());
					}
				}

				foreach (string columnXValue in columnXValues)
				{
					innerDictionary = new Dictionary<string, ArrayList>();

					ArrayList columnYValues = new ArrayList();

					foreach (DataRow row in drsFilterX)
					{
						if (row[indexX].ToString() == columnXValue)
						{
							columnYValues.Add(row[indexY].ToString());
						}
					}

					innerDictionary.Add(columnXValue, columnYValues);

					listDictionary.Add(innerDictionary);
				}

				dictionary.Add(columnZValue, listDictionary);
			}

			return dictionary;
		}

		private void setXValueCount(int count)
		{
			_XValueCount = count;
		}

		private List<string> getAllXValue(
			Dictionary<string, List<Dictionary<string, ArrayList>>> dictionary)
		{
			List<string> allXValue = new List<string>();

			foreach (KeyValuePair<string, List<Dictionary<string, ArrayList>>> keyValuePair in dictionary)
			{
				foreach (Dictionary<string, ArrayList> innerDictionary in keyValuePair.Value)
				{
					foreach (KeyValuePair<string, ArrayList> lastInnerKeyValuePair in innerDictionary)
					{
						if (!allXValue.Contains(lastInnerKeyValuePair.Key))
						{
							allXValue.Add(lastInnerKeyValuePair.Key);
						}
					}
				}
			}

			setXValueCount(allXValue.Count);

			return allXValue;
		}

		private Dictionary<string, ArrayList> constructNullValueDictionary(string xValue)
		{
			Dictionary<string, ArrayList> dictionary = new Dictionary<string, ArrayList>();

			dictionary.Add(xValue, new ArrayList { 0 });

			return dictionary;
		}

		private List<string> cloneList(List<string> lists)
		{
			List<string> cloneList = new List<string>();

			foreach (string list in lists)
			{
				cloneList.Add(list);
			}

			return cloneList;
		}

		private Dictionary<string, List<Dictionary<string, ArrayList>>> cloneDictionary(
			Dictionary<string, List<Dictionary<string, ArrayList>>> dictionary)
		{
			Dictionary<string, List<Dictionary<string, ArrayList>>> cloneDictionary =
				new Dictionary<string, List<Dictionary<string, ArrayList>>>();

			foreach (KeyValuePair<string, List<Dictionary<string, ArrayList>>> keyValuePair in dictionary)
			{
				cloneDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}

			return cloneDictionary;
		}

		private Dictionary<string, List<Dictionary<string, ArrayList>>> configSuitDictionary(
			Dictionary<string, List<Dictionary<string, ArrayList>>> dictionary)
		{
			Dictionary<string, List<Dictionary<string, ArrayList>>> configedDictionary = cloneDictionary(dictionary);

			List<string> allXValue = getAllXValue(dictionary);

			foreach (KeyValuePair<string, List<Dictionary<string, ArrayList>>> keyValuePair in dictionary)
			{
				List<string> needAddXValues = cloneList(allXValue);
				foreach (Dictionary<string, ArrayList> innerDictionary in keyValuePair.Value)
				{
					foreach (KeyValuePair<string, ArrayList> lastInnerKeyValuePair in innerDictionary)
					{
						if (needAddXValues.Contains(lastInnerKeyValuePair.Key))
						{
							needAddXValues.Remove(lastInnerKeyValuePair.Key);
						}
					}
				}

				foreach (string needAddXValue in needAddXValues)
				{
					Dictionary<string, ArrayList> nullValueDictionary = constructNullValueDictionary(needAddXValue);
					configedDictionary[keyValuePair.Key].Add(nullValueDictionary);
					configedDictionary[keyValuePair.Key].Sort(new DictionaryDataCompare());
				}
			}

			return configedDictionary;
		}

		private Dictionary<string, ArrayList> getDictionary(
			DataSet dataSet, DataColumn dataColumnX, DataColumn dataColumnY)
		{
			Dictionary<string, ArrayList> dictionary = new Dictionary<string, ArrayList>();

			int indexX = dataSet.Tables[0].Columns.IndexOf(dataColumnX.ColumnName);
			int indexY = dataSet.Tables[0].Columns.IndexOf(dataColumnY.ColumnName);

			List<string> columnXValues = new List<string>();
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				if (!columnXValues.Contains(row[indexX].ToString()))
				{
					columnXValues.Add(row[indexX].ToString());
				}
			}

			foreach (string columnXValue in columnXValues)
			{
				ArrayList columnYValues = new ArrayList();

				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					if (row[indexX].ToString() == columnXValue)
					{
						columnYValues.Add(row[indexY].ToString());
					}
				}

				dictionary.Add(columnXValue, columnYValues);
			}

			setXValueCount(dictionary.Keys.Count);

			return dictionary;
		}

		private List<ChartAreaInfo> createMultiChartAreaData(
			DataSet dataSet, DataColumn dataColumnX, DataColumn dataColumnY, DataColumn dataColumnZ, PagerSettings pagerSettings)
		{
			List<ChartAreaInfo> chartAreaInfos = new List<ChartAreaInfo>();

			Dictionary<string, List<Dictionary<string, ArrayList>>> dictionary =
				getDictionary(dataSet, dataColumnX, dataColumnY, dataColumnZ);

			dictionary = configSuitDictionary(dictionary);

			foreach (KeyValuePair<string, List<Dictionary<string, ArrayList>>> keyValuePair in dictionary)
			{
				List<PointInfo> pointInfos = new List<PointInfo>();
				int pageIndex = 0;
				int pageMin = pagerSettings.PageIndex * pagerSettings.PageSize;
				int pageMax = (pagerSettings.PageIndex + 1) * pagerSettings.PageSize;

				foreach (Dictionary<string, ArrayList> innerDictionary in keyValuePair.Value)
				{
					foreach (KeyValuePair<string, ArrayList> lastInnerKeyValuePair in innerDictionary)
					{
						if (pageIndex >= pageMin && pageIndex <= pageMax)
						{
							PointInfo pointInfo = new PointInfo();

							pointInfo.XValue = lastInnerKeyValuePair.Key;
							pointInfo.YValue = lastInnerKeyValuePair.Value.ToArray();
							pointInfos.Add(pointInfo);
						}
						pageIndex++;
					}
				}

				List<SeriesInfo> seriesInfos = new List<SeriesInfo>();
				SeriesInfo seriesInfo = new SeriesInfo();
				seriesInfo.Name = keyValuePair.Key;
				seriesInfo.Points = pointInfos;
				seriesInfos.Add(seriesInfo);

				ChartAreaInfo chartAreaInfo = new ChartAreaInfo();
				chartAreaInfo.Name = keyValuePair.Key;
				chartAreaInfo.Series = seriesInfos;

				chartAreaInfos.Add(chartAreaInfo);
			}

			return chartAreaInfos;
		}

		private List<ChartAreaInfo> createLineOrColumnChartData(
			DataSet dataSet, DataColumn dataColumnX, DataColumn dataColumnY, DataColumn dataColumnZ, PagerSettings pagerSettings)
		{
			List<ChartAreaInfo> chartAreaInfos = new List<ChartAreaInfo>();

			Dictionary<string, List<Dictionary<string, ArrayList>>> dictionary =
				getDictionary(dataSet, dataColumnX, dataColumnY, dataColumnZ);

			dictionary = configSuitDictionary(dictionary);

			ChartAreaInfo chartAreaInfo = new ChartAreaInfo();
			List<SeriesInfo> seriesInfos = new List<SeriesInfo>();

			foreach (KeyValuePair<string, List<Dictionary<string, ArrayList>>> keyValuePair in dictionary)
			{
				List<PointInfo> pointInfos = new List<PointInfo>();
				int pageIndex = 0;
				int pageMin = pagerSettings.PageIndex * pagerSettings.PageSize;
				int pageMax = (pagerSettings.PageIndex + 1) * pagerSettings.PageSize;

				foreach(Dictionary<string, ArrayList> innerDictionary in keyValuePair.Value)
				{
					foreach (KeyValuePair<string, ArrayList> lastInnerKeyValuePair in innerDictionary)
					{
						if (pageIndex >= pageMin && pageIndex <= pageMax)
						{
							PointInfo pointInfo = new PointInfo();

							pointInfo.XValue = lastInnerKeyValuePair.Key;
							pointInfo.YValue = lastInnerKeyValuePair.Value.ToArray();
							pointInfos.Add(pointInfo);
						}
						pageIndex++;
					}
				}

				SeriesInfo seriesInfo = new SeriesInfo();
				seriesInfo.Name = keyValuePair.Key;
				seriesInfo.Points = pointInfos;

				seriesInfos.Add(seriesInfo);
			}

			chartAreaInfo.Series = seriesInfos;
			chartAreaInfo.Name = ChartViewStatic.CHART_CHARTAREA_NAME;
			chartAreaInfos.Add(chartAreaInfo);
			
			return chartAreaInfos;
		}

		private List<ChartAreaInfo> createChartData(
			DataSet dataSet, DataColumn dataColumnX, DataColumn dataColumnY, PagerSettings pagerSettings)
		{
			List<ChartAreaInfo> chartAreaInfos = new List<ChartAreaInfo>();

			Dictionary<string, ArrayList> dictionary = getDictionary(dataSet, dataColumnX, dataColumnY);

			List<PointInfo> pointInfos = new List<PointInfo>();
			int pageIndex = 0;
			int pageMin = pagerSettings.PageIndex * pagerSettings.PageSize;
			int pageMax = (pagerSettings.PageIndex + 1) * pagerSettings.PageSize;

			foreach (KeyValuePair<string, ArrayList> keyValuePair in dictionary)
			{
				if (pageIndex >= pageMin && pageIndex <= pageMax)
				{
					PointInfo pointInfo = new PointInfo();

					pointInfo.XValue = keyValuePair.Key;
					pointInfo.YValue = keyValuePair.Value.ToArray();

					pointInfos.Add(pointInfo);
				}
				pageIndex++;
			}

			SeriesInfo seriesInfo = new SeriesInfo();
			seriesInfo.Name = ChartViewStatic.CHART_SERIES_NAME;
			seriesInfo.Points = pointInfos;

			List<SeriesInfo> seriesInfos = new List<SeriesInfo>();
			seriesInfos.Add(seriesInfo);

			ChartAreaInfo chartAreaInfo = new ChartAreaInfo();
			chartAreaInfo.Name = ChartViewStatic.CHART_CHARTAREA_NAME;
			chartAreaInfo.Series = seriesInfos;

			chartAreaInfos.Add(chartAreaInfo);

			return chartAreaInfos;
		}

		private bool needBeSort(DataColumn dataColumnX, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			bool result = false;

			ColumnPropertyInfo columnPropertyInfo = GetColumnPropertyInfo(dataColumnX.ColumnName, columnPropertyInfos);

			if (columnPropertyInfo.ChartDataType == ChartDataType.Date.ToString())
			{
				result = true;
			}

			return result;
		}

		private void formatChartAreaInfo(List<ChartAreaInfo> chartAreaInfos)
		{
			for (int i = 0; i < chartAreaInfos.Count; i++)
			{
				chartAreaInfos[i].Name = ConvertToShortDateString(chartAreaInfos[i].Name);

				for (int j = 0; j < chartAreaInfos[i].Series.Count; j++)
				{
					chartAreaInfos[i].Series[j].Name = ConvertToShortDateString(chartAreaInfos[i].Series[j].Name);

					for (int k = 0; k < chartAreaInfos[i].Series[j].Points.Count; k++)
					{
						chartAreaInfos[i].Series[j].Points[k].XValue = 
							(object)(ConvertToShortDateString(chartAreaInfos[i].Series[j].Points[k].XValue.ToString()));
					}
				}
			}
		}

		private AllowChartAxes convertToAllowedAxes(string allowedAxes)
		{
			string[] allowedAxesArray = allowedAxes.Split(ChartViewStatic.SPLITE_ALLOWED_AXES);
			AllowChartAxes convertedAllowedAxes = AllowChartAxes.None;


			if (null != allowedAxesArray)
			{
				foreach (string innerAllowedAxes in allowedAxesArray)
				{
					AllowChartAxes contractAllowedAxes = (AllowChartAxes)Enum.Parse(typeof(AllowChartAxes), innerAllowedAxes);

					convertedAllowedAxes = convertedAllowedAxes | contractAllowedAxes;
				}
			}

			return convertedAllowedAxes;
		}

		private int findValueInDataSetRowNumber(string shortDateTime, string dataColumnZValue, int indexX, int indexZ, DataSet dataSet)
		{
		    int rowNumber = -1;

		    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
		    {
				string shortDataColumnXValue = ConvertToShortDateString(dataSet.Tables[0].Rows[i][indexX].ToString());
				if (shortDataColumnXValue == shortDateTime)
		        {
					if (-1 != indexZ)
					{
						if (dataSet.Tables[0].Rows[i][indexZ].ToString() == dataColumnZValue)
						{
							rowNumber = i;
						}
					}
					else
					{
						rowNumber = i;
					}

					break;
		        }
		    }

		    return rowNumber;
		}

		private DataSet countDataSetByDataColumns(DataColumn dataColumnX, DataColumn dataColumnY, DataColumn dataColumnZ, DataSet dataSet)
		{
			DataSet countedDataSet;

			int indexX = dataSet.Tables[0].Columns.IndexOf(dataColumnX.ColumnName);
			int indexY = dataSet.Tables[0].Columns.IndexOf(dataColumnY.ColumnName);
			int indexZ = -1;
			int columnCount = dataSet.Tables[0].Columns.Count;

			if (dataColumnX.DataType.ToString() == ChartViewStatic.SYSTEM_DATETIME)
			{
				if (null != dataColumnZ)
				{
					indexZ = dataSet.Tables[0].Columns.IndexOf(dataColumnZ);
				}

				countedDataSet = new DataSet();
				DataTable dataTable = new DataTable();
				foreach (DataColumn dataColumn in dataSet.Tables[0].Columns)
				{
					DataColumn newDataColumn = new DataColumn(dataColumn.ColumnName, dataColumn.DataType);

					dataTable.Columns.Add(newDataColumn);
				}
				countedDataSet.Tables.Add(dataTable);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					string shortDateTime = ConvertToShortDateString(dataRow[indexX].ToString());

					int findIndex = -1;

					if (-1 != indexZ)
					{
						string dataColumnZValue = dataRow[indexZ].ToString();

						findIndex = findValueInDataSetRowNumber(shortDateTime, dataColumnZValue, indexX, indexZ, countedDataSet);
					}
					else
					{
						findIndex = findValueInDataSetRowNumber(shortDateTime, string.Empty, indexX, -1, countedDataSet);
					}

					if (-1 != findIndex)
					{
						float oldValue = float.Parse(countedDataSet.Tables[0].Rows[findIndex][indexY].ToString());
						float newValue = oldValue + float.Parse(dataRow[indexY].ToString());

						countedDataSet.Tables[0].Rows[findIndex][indexY] = newValue.ToString();
					}
					else
					{
						DataRow newDataRow = countedDataSet.Tables[0].NewRow();
						newDataRow.ItemArray = dataRow.ItemArray;

						countedDataSet.Tables[0].Rows.Add(newDataRow);
					}
				}
			}
			else
			{
				countedDataSet = dataSet;
			}

			dataColumnX = countedDataSet.Tables[0].Columns[indexX];

			dataColumnY = countedDataSet.Tables[0].Columns[indexY];

			if (-1 != indexZ)
			{
				dataColumnZ = countedDataSet.Tables[0].Columns[indexZ];
			}

			return countedDataSet;
		}

		#endregion
	}
}
