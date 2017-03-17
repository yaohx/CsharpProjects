using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Dundas.Charting.WinControl;

using MB.WinBase.Common;
using MB.WinChart.Model;
using MB.WinChart.Resource;
using MB.WinChart.Share;
using MB.Util;
using MB.Util.Serializer;

namespace MB.WinChart
{
	/// <summary>
	/// 配制窗口类.
	/// </summary>
	public partial class ChartConfigure : Form
	{
		#region Private variables

		private DataColumnCollection _DataColumnCollection;

		private List<ColumnPropertyInfo> _ColumnPropertyInfos;

		private ChartEventArgs _ChartEventArgs;

		private bool _Expand = false;

		private string _Identity;

		#endregion

		#region Construct

		/// <summary>
		/// 构造函数.
		/// </summary>
		/// <param name="dataColumnCollection">数据源列集合.</param>
		/// <param name="columnPropertyInfos">配制项集合.</param>
		/// <param name="chartEventArgs">配制项.</param>
		/// <param name="identity">模块标识.</param>
		public ChartConfigure(
			DataColumnCollection dataColumnCollection, List<ColumnPropertyInfo> columnPropertyInfos,
			ChartEventArgs chartEventArgs, string identity)
		{
			InitializeComponent();

			try
			{
				_DataColumnCollection = dataColumnCollection;
				_ColumnPropertyInfos = columnPropertyInfos;
				_ChartEventArgs = chartEventArgs;
				_Identity = identity;

				bindClassify(ChartViewStatic.R_BTN_ANALYSIS_NAME, _ColumnPropertyInfos, _DataColumnCollection);
				bindDbcboChartType(ChartViewStatic.R_BTN_ANALYSIS_NAME);
				bindDbcboTemplate(_Identity);
				loadLastTimeSettings(_ChartEventArgs);
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		#endregion

		#region Delegate events

		private System.EventHandler<ChartEventArgs> _AfterDataApply;

		/// <summary>
		/// 配制数据后响应事件.
		/// </summary>
		public event System.EventHandler<ChartEventArgs> AfterDataApply
		{
			add
			{
				_AfterDataApply += value;
			}
			remove
			{
				_AfterDataApply -= value;
			}
		}

		#endregion 

		#region Events

		private void ChartConfigure_Load(object sender, EventArgs e)
		{
			// Nothing to do.
		}

		private void onAfterDataApply(ChartEventArgs arg)
		{
			if (_AfterDataApply != null)
			{
				_AfterDataApply(this, arg);
			}
		}

		private void rBtn_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				RadioButton radioButton = sender as RadioButton;

				if (radioButton.Checked)
				{
					if (radioButton.Name == ChartViewStatic.R_BTN_COMPARE_NAME)
					{
						showGroupBoxCompare();
					}
					else
					{
						bindLstTemplateGroupBoxCompare();
					}

					bindClassify(radioButton.Name, _ColumnPropertyInfos, _DataColumnCollection);
					bindDbcboChartType(radioButton.Name);
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void btnApplication_Click(object sender, EventArgs e)
		{
			try
			{
				ChartEventArgs chartEventArgs = new ChartEventArgs();

				string choosedAnalysis = getChoosedRadioButton();
				chartEventArgs.AnalyseData = choosedAnalysis;

				SeriesChartType choosedSeriesChartType = getChoosedSeriesChartType();
				chartEventArgs.ChartType = choosedSeriesChartType;

				Dictionary<string, DataColumn> dictionary = null;

				if (chartEventArgs.AnalyseData == ChartViewStatic.R_BTN_COMPARE_NAME)
				{
					dictionary = getRBtnCompareChoosedAxisNamesDictionary(_DataColumnCollection, _ColumnPropertyInfos);
				}
				else
				{
					dictionary = getChoosedAxisNamesDictionary(_DataColumnCollection, _ColumnPropertyInfos);
				}

				if (0 != dictionary.Count)
				{
					chartEventArgs.YAxis = dictionary[ChartViewStatic.YAXIS];
					chartEventArgs.XAxis = dictionary[ChartViewStatic.XAXIS];
					if (dictionary.Keys.Contains(ChartViewStatic.ZAXIS))
					{
						chartEventArgs.ZAxis = dictionary[ChartViewStatic.ZAXIS];
					}
				}

				chartEventArgs.ChartTemplate = this.dbcboTemplate.SelectedItem.ToString();

				_ChartEventArgs = chartEventArgs;
				onAfterDataApply(chartEventArgs);

			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		#region Template

		private void frm_TemplateConfigAfterDataApply(object sender, EventArgs e)
		{
			bindDbcboTemplate(_Identity);
		}

		private void butChoose_Click(object sender, EventArgs e)
		{
			try
			{
				Dictionary<string, DataColumn> dictionary = null;

				string radioButtonName = getChoosedRadioButton();

				if (radioButtonName == ChartViewStatic.R_BTN_COMPARE_NAME)
				{
					dictionary = getRBtnCompareChoosedAxisNamesDictionary(_DataColumnCollection, _ColumnPropertyInfos);
				}
				else
				{
					dictionary = getChoosedAxisNamesDictionary(_DataColumnCollection, _ColumnPropertyInfos);
				}

				if (0 != dictionary.Count)
				{
					TemplateInfo templateInfo = getCurrentTemplateInfo();

					ChartTemplateConfig chartTemplateConfig =
						new ChartTemplateConfig(_Identity, templateInfo);
					chartTemplateConfig.TemplateConfigAfterDataApply += new EventHandler<EventArgs>(frm_TemplateConfigAfterDataApply);

					chartTemplateConfig.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void dbcboTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ComboBox comboBox = sender as ComboBox;
				if (0 != comboBox.SelectedIndex)
				{
					string fileName = comboBox.SelectedItem.ToString();

					updateFileInfo(_Identity, fileName);

					TemplateInfo templateInfo = getTemplateInfo(_Identity, fileName);
					configTemplateSetting(templateInfo);
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		#endregion

		#endregion

		#region Private methods

		private DataColumn getDefaultDataColumnY(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			DataColumn dataColumn = new DataColumn();

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			dataColumn = chartViewHelper.GetDefaultDataColumnY(columnPropertyInfos, dataColumnCollection);

			return dataColumn;
		}

		private DataColumn getDefaultDataColumnX(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			DataColumn dataColumn = new DataColumn();

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			dataColumn = chartViewHelper.GetDefaultDataColumnX(columnPropertyInfos, dataColumnCollection);

			return dataColumn;
		}

		private DataColumn getDateTimeDataColumnX(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			DataColumn dataColumn = new DataColumn();

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			dataColumn = chartViewHelper.GetDateTimeDataColumnX(columnPropertyInfos, dataColumnCollection);

			return dataColumn;
		}

		private DataColumn getNextDataColumnX(
			List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection, DataColumn dataColumnX)
		{
			DataColumn dataColumn = new DataColumn();

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			dataColumn = chartViewHelper.GetNextDataColumnX(columnPropertyInfos, dataColumnCollection, dataColumnX);

			return dataColumn;
		}

		private DataColumn getDefaultDataColumnZ(
			List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection, DataColumn dataColumnX)
		{
			DataColumn dataColumn = new DataColumn();

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			dataColumn = chartViewHelper.GetDefaultDataColumnZ(columnPropertyInfos, dataColumnCollection, dataColumnX);

			return dataColumn;
		}

		private void bindAnalysisChkListClassify(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			DataColumn dataColumnY = getDefaultDataColumnY(columnPropertyInfos, dataColumnCollection);
			DataColumn dataColumnX = getDefaultDataColumnX(columnPropertyInfos, dataColumnCollection);

			int index = 0;
			foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
			{
				string displayItemName = addAllowChartAxesToChkListClassify(
					innerColumnPropertyInfo.Description, innerColumnPropertyInfo.AllowChartAxes);

				chkListClassify.Items.Add(displayItemName);
				chkListClassify.SetItemChecked(index, false);

				if (dataColumnY.ColumnName == innerColumnPropertyInfo.Name)
				{
					chkListClassify.SetItemChecked(index, true);
				}
				if (dataColumnX.ColumnName == innerColumnPropertyInfo.Name)
				{
					chkListClassify.SetItemChecked(index, true);
				}

				index++;
			}
		}

		private void bindAnalysisDbcboChartType()
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			string[] chartTypeItems = chartViewHelper.GetChartTypeItems();

			foreach (string chartTypeItem in chartTypeItems)
			{
				dbcboChartType.Items.Add(chartTypeItem);
			}

			dbcboChartType.SelectedIndex = 0;
		}

		private void bindCompareDbcboChartType()
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			string itemPie = getMessage(ChartViewStatic.ITEM_PIE_KEY);

			string[] chartTypeItems = chartViewHelper.GetChartTypeItems();

			foreach (string chartTypeItem in chartTypeItems)
			{
				if (itemPie != chartTypeItem)
				{
					dbcboChartType.Items.Add(chartTypeItem);
				}
			}

			dbcboChartType.SelectedIndex = 0;
		}

		private void bindDirectChkListClassify(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			DataColumn dataColumnY = getDefaultDataColumnY(columnPropertyInfos, dataColumnCollection);
			DataColumn dataColumnX = getDateTimeDataColumnX(columnPropertyInfos, dataColumnCollection);
			DataColumn dataColumnZ = getDefaultDataColumnZ(columnPropertyInfos, dataColumnCollection, dataColumnX);

			int index = 0;
			foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
			{
				string displayItemName = addAllowChartAxesToChkListClassify(
					innerColumnPropertyInfo.Description, innerColumnPropertyInfo.AllowChartAxes);

				chkListClassify.Items.Add(displayItemName);
				chkListClassify.SetItemChecked(index, false);

				if (dataColumnY.ColumnName == innerColumnPropertyInfo.Name)
				{
					chkListClassify.SetItemChecked(index, true);
				}
				if (dataColumnX.ColumnName == innerColumnPropertyInfo.Name)
				{
					chkListClassify.SetItemChecked(index, true);
				}
				if (dataColumnZ.ColumnName == innerColumnPropertyInfo.Name)
				{
					chkListClassify.SetItemChecked(index, true);
				}

				index++;
			}
		}

		private void bindDirectDbcboChartType()
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			string itemPie = getMessage(ChartViewStatic.ITEM_PIE_KEY);
			string itemColumn = getMessage(ChartViewStatic.ITEM_COLUMN_KEY);

			string[] chartTypeItems = chartViewHelper.GetChartTypeItems();

			foreach (string chartTypeItem in chartTypeItems)
			{
				if (itemPie != chartTypeItem && itemColumn != chartTypeItem)
				{
					dbcboChartType.Items.Add(chartTypeItem);
				}
			}

			dbcboChartType.SelectedIndex = 0;
		}

		private void bindClassify(string radioButton, List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			chkListClassify.Items.Clear();

			if (ChartViewStatic.R_BTN_ANALYSIS_NAME == radioButton)
			{
				bindAnalysisChkListClassify(columnPropertyInfos, dataColumnCollection);
			}

			if (ChartViewStatic.R_BTN_COMPARE_NAME == radioButton)
			{
				bindRBtnCompare(columnPropertyInfos, dataColumnCollection);
			}

			if (ChartViewStatic.R_BTN_DIRECT_NAME == radioButton)
			{
				bindDirectChkListClassify(columnPropertyInfos, dataColumnCollection);
			}
		}

		private void bindRBtnCompare(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			bindDbcboCompare(columnPropertyInfos, dataColumnCollection, 0);
			bindRBtnCompareChkListClassify(columnPropertyInfos, dataColumnCollection);
		}

		private void bindDbcboChartType(string radioButton)
		{
			dbcboChartType.Items.Clear();

			if (ChartViewStatic.R_BTN_ANALYSIS_NAME == radioButton)
			{
				bindAnalysisDbcboChartType();
			}
			if (ChartViewStatic.R_BTN_COMPARE_NAME == radioButton)
			{
				bindCompareDbcboChartType();
			}
			if (ChartViewStatic.R_BTN_DIRECT_NAME == radioButton)
			{
				bindDirectDbcboChartType();
			}
		}

		private string getChoosedRadioButton()
		{
			string choosedRadioButton = string.Empty;

			List<RadioButton> radioButtons = new List<RadioButton>();

			radioButtons.Add(rBtnAnalysis);
			radioButtons.Add(rBtnCompare);
			radioButtons.Add(rBtnDirect);

			foreach (RadioButton radioButton in radioButtons)
			{
				if (true == radioButton.Checked)
				{
					choosedRadioButton = radioButton.Name;
					break;
				}
			}

			return choosedRadioButton;
		}

		private SeriesChartType getChoosedSeriesChartType()
		{
			SeriesChartType seriesChartType;

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			seriesChartType = chartViewHelper.GetChartType(dbcboChartType.SelectedItem.ToString());

			return seriesChartType;
		}

		private string getChartTypeItem(SeriesChartType seriesChartType)
		{
			string item = string.Empty;

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			item = chartViewHelper.GetChartTypeItem(seriesChartType);

			return item;
		}

		private string getChartTypeItem(string seriesChartTypeName)
		{
			string item = string.Empty;

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			item = chartViewHelper.GetChartTypeItem(seriesChartTypeName);

			return item;
		}

		private DataColumn getDataColumn(string name, DataColumnCollection dataColumnCollection)
		{
			DataColumn dataColumn = null;

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			dataColumn = chartViewHelper.GetDataColumn(name, dataColumnCollection);

			return dataColumn;
		}

		private List<DataColumn> getChoosedDataColumns(DataColumnCollection dataColumnCollection)
		{
			List<DataColumn> dataColumns = new List<DataColumn>();

			for (int i = 0; i < chkListClassify.CheckedItems.Count; i++)
			{
				string description = spliteAllowChartAxesFromChkListClassify(chkListClassify.CheckedItems[i].ToString());
				string dataColumnName = getColumnPropertyInfoName(description, _ColumnPropertyInfos);
				DataColumn dataColumn = getDataColumn(dataColumnName, dataColumnCollection);
				dataColumns.Add(dataColumn);
			}

			return dataColumns;
		}

		private List<ColumnPropertyInfo> getChoosedColumnPropertyInfos(
			List<DataColumn> dataColumns, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			List<ColumnPropertyInfo> choosedColumnPropertyInfos = new List<ColumnPropertyInfo>();

			foreach (DataColumn dataColumn in dataColumns)
			{
				foreach (ColumnPropertyInfo columnPropertyInfo in columnPropertyInfos)
				{
					if (dataColumn.ColumnName == columnPropertyInfo.Name)
					{
						choosedColumnPropertyInfos.Add(columnPropertyInfo);
					}
				}
			}

			return choosedColumnPropertyInfos;
		}

		private bool judgeIsYAxis(DataColumn dataColumn)
		{
			bool result = false;

			if (dataColumn.DataType.ToString() == ChartViewStatic.SYSTEM_INT32 ||
				dataColumn.DataType.ToString() == ChartViewStatic.SYSTEM_SINGLE ||
				dataColumn.DataType.ToString() == ChartViewStatic.SYSTEM_DOUBLE ||
				dataColumn.DataType.ToString() == ChartViewStatic.SYSTEM_DECIMAL)
			{
				result = true;
			}

			return result;
		}

		private ColumnPropertyInfo getColumnPropertyInfo(string name, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();
			ColumnPropertyInfo columnPropertyInfo = chartViewHelper.GetColumnPropertyInfo(name, columnPropertyInfos);

			return columnPropertyInfo;
		}

		private string judgeIsSuitChoosedYValue(List<DataColumn> dataColumns, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			string message = string.Empty;

			List<ColumnPropertyInfo> suitColumnPropertyInfos = new List<ColumnPropertyInfo>();

			foreach (DataColumn dataColumn in dataColumns)
			{
				ColumnPropertyInfo columnPropertyInfo = getColumnPropertyInfo(dataColumn.ColumnName, columnPropertyInfos);

				bool isAllowedYAxes = judgeIsAllowedYAxes(columnPropertyInfo.AllowChartAxes);

				if (isAllowedYAxes && ( ChartDataType.Number.ToString() == columnPropertyInfo.ChartDataType))
				{
					suitColumnPropertyInfos.Add(columnPropertyInfo);
				}
			}

			if (ChartViewStatic.YVALUE_NUM != suitColumnPropertyInfos.Count)
			{
				message = getMessage(ChartViewStatic.ERROR_CHOOSE_YVALUE_COUNT_KEY);
			}

			return message;
		}

		private bool judgeIsSameCloumnProperty(string leftString, string rightString)
		{
			bool result = false;

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			AllowChartAxes leftAllowChartAxes = chartViewHelper.ConvertToSingleAllowedAxes(leftString);
			AllowChartAxes rightAllowChartAxes = chartViewHelper.ConvertToSingleAllowedAxes(rightString);

			if (leftAllowChartAxes != AllowChartAxes.None &&
				rightAllowChartAxes != AllowChartAxes.None)
			{
				if (leftAllowChartAxes == rightAllowChartAxes)
				{
					result = true;
				}
			}

			return result;
		}

		private string judgeIsSuitChoosedXZValue(List<DataColumn> dataColumns, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			string message = string.Empty;

			List<ColumnPropertyInfo> suitColumnPropertyInfos = new List<ColumnPropertyInfo>();

			foreach (DataColumn dataColumn in dataColumns)
			{
				ColumnPropertyInfo columnPropertyInfo = getColumnPropertyInfo(dataColumn.ColumnName, columnPropertyInfos);

				bool isAllowedXAxes = judgeIsAllowedXAxes(columnPropertyInfo.AllowChartAxes);
				bool isAllowedZAxes = judgeIsAllowedZAxes(columnPropertyInfo.AllowChartAxes);

				if (isAllowedXAxes || isAllowedZAxes)
				{
					suitColumnPropertyInfos.Add(columnPropertyInfo);
				}
			}

			if (ChartViewStatic.MAX_ANALYSIS_ITEMS == suitColumnPropertyInfos.Count)
			{
				if (suitColumnPropertyInfos[0].ChartDataType == suitColumnPropertyInfos[1].ChartDataType)
				{
					bool isSameCloumnProperty = judgeIsSameCloumnProperty(
						suitColumnPropertyInfos[0].AllowChartAxes, suitColumnPropertyInfos[1].AllowChartAxes);

					if (isSameCloumnProperty)
					{
						message = getMessage(ChartViewStatic.ERROR_CHOOSE_XZVALUE_ANALYSIS_KEY);
					}
				}
			}

			return message;
		}

		private string checkChoosedDataColumns(List<DataColumn> dataColumns, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			string message = string.Empty;

			if (dataColumns.Count < ChartViewStatic.MIN_SELECTED_ITEMS || dataColumns.Count > ChartViewStatic.MAX_SELECTED_ITEMS)
			{
				message = getMessage(ChartViewStatic.ERROR_CHOOSE_COUNT_KEY);
			}

			if (string.IsNullOrEmpty(message))
			{
				message = judgeIsSuitChoosedYValue(dataColumns, columnPropertyInfos);
			}

			if (string.IsNullOrEmpty(message))
			{
				message = judgeIsSuitChoosedXZValue(dataColumns, columnPropertyInfos);
			}

			return message;
		}

		private Dictionary<string, DataColumn> getChoosedAxisNamesDictionary(
			DataColumnCollection dataColumnCollection, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			Dictionary<string, DataColumn> dictionary = new Dictionary<string, DataColumn>();

			List<DataColumn> dataColumns = getChoosedDataColumns(dataColumnCollection);

			string errorMessage = checkChoosedDataColumns(dataColumns, columnPropertyInfos);
			if (string.IsNullOrEmpty(errorMessage))
			{
				List<ColumnPropertyInfo> choosedColumnPropertyInfos =
					getChoosedColumnPropertyInfos(dataColumns, columnPropertyInfos);

				foreach (ColumnPropertyInfo innerColumnPropertyInfo in choosedColumnPropertyInfos)
				{
					bool isAllowedYAxes = judgeIsAllowedYAxes(innerColumnPropertyInfo.AllowChartAxes);

					if (isAllowedYAxes)
					{
						DataColumn dataColumnY = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
						dictionary.Add(ChartViewStatic.YAXIS, dataColumnY);
						choosedColumnPropertyInfos.Remove(innerColumnPropertyInfo);
						break;
					}
				}

				if (ChartViewStatic.MIN_ANALYSIS_ITEMS == choosedColumnPropertyInfos.Count)
				{
					DataColumn dataColumnX = getDataColumn(choosedColumnPropertyInfos[0].Name, dataColumnCollection);
					dictionary.Add(ChartViewStatic.XAXIS, dataColumnX);
				}
				else if (ChartViewStatic.MAX_ANALYSIS_ITEMS == choosedColumnPropertyInfos.Count)
				{
					foreach (ColumnPropertyInfo innerColumnPropertyInfo in choosedColumnPropertyInfos)
					{
						if (ChartDataType.Date.ToString() == innerColumnPropertyInfo.ChartDataType)
						{
							DataColumn dataColumnX = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
							dictionary.Add(ChartViewStatic.XAXIS, dataColumnX);
						}
						if (ChartDataType.String.ToString() == innerColumnPropertyInfo.ChartDataType)
						{
							bool isAllowedZAxes = judgeIsAllowedZAxes(innerColumnPropertyInfo.AllowChartAxes);
							bool isAllowedXAxes = judgeIsAllowedXAxes(innerColumnPropertyInfo.AllowChartAxes);

							if (isAllowedXAxes && isAllowedZAxes)
							{
								if (!dictionary.Keys.Contains(ChartViewStatic.XAXIS))
								{
									DataColumn dataColumnX = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
									dictionary.Add(ChartViewStatic.XAXIS, dataColumnX);
								}
								else if (!dictionary.Keys.Contains(ChartViewStatic.ZAXIS))
								{
									DataColumn dataColumnZ = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
									dictionary.Add(ChartViewStatic.ZAXIS, dataColumnZ);
								}
							}
							else if (isAllowedXAxes)
							{
								DataColumn dataColumnX = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
								dictionary.Add(ChartViewStatic.XAXIS, dataColumnX);
							}
							else if (isAllowedZAxes)
							{
								DataColumn dataColumnZ = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
								dictionary.Add(ChartViewStatic.ZAXIS, dataColumnZ);
							}
						}
					}
				}
			}
			else
			{
				throw new APPException(errorMessage, APPMessageType.DisplayToUser);
			}

			return dictionary;
		}

		private string getMessage(string key)
		{
			string message = string.Empty;

			ResourcesHelper resourcesHelper = new ResourcesHelper();
			message = resourcesHelper.GetMessage(key);

			return message;
		}

		private void loadLastTimeRBtn(ChartEventArgs chartEventArgs)
		{
			if (rBtnAnalysis.Name == chartEventArgs.AnalyseData)
			{
				rBtnAnalysis.Checked = true;
			}
			if (rBtnCompare.Name == chartEventArgs.AnalyseData)
			{
				rBtnCompare.Checked = true;
			}
			if (rBtnDirect.Name == chartEventArgs.AnalyseData)
			{
				rBtnDirect.Checked = true;
			}
		}

		private void loadLastTimeChkListClassify(ChartEventArgs chartEventArgs)
		{
			for (int i = 0; i < chkListClassify.Items.Count; i++)
			{
				chkListClassify.SetItemChecked(i, false);
			}

			if (null != chartEventArgs.XAxis)
			{
				ColumnPropertyInfo columnPropertyInfo = 
					getColumnPropertyInfo(chartEventArgs.XAxis.ColumnName, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
			if (null != chartEventArgs.YAxis)
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(chartEventArgs.YAxis.ColumnName, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
			if (null != chartEventArgs.ZAxis)
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(chartEventArgs.YAxis.ColumnName, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
		}

		private void loadLastTimeDbcboChartType(ChartEventArgs chartEventArgs)
		{
			dbcboChartType.SelectedItem = getChartTypeItem(chartEventArgs.ChartType);
		}

		private void loadLastTimeTemplate(ChartEventArgs chartEventArgs)
		{
			if (!string.IsNullOrEmpty(chartEventArgs.ChartTemplate))
			{
				int selectIndex = this.dbcboTemplate.Items.IndexOf(chartEventArgs.ChartTemplate);
				this.dbcboTemplate.SelectedIndex = selectIndex;
			}
		}

		private void loadLastTimeSettings(ChartEventArgs chartEventArgs)
		{
			if (null != chartEventArgs && null != chartEventArgs.XAxis && null != chartEventArgs.YAxis)
			{
				loadLastTimeRBtn(chartEventArgs);

				if (chartEventArgs.AnalyseData == ChartViewStatic.R_BTN_COMPARE_NAME)
				{
					loadRBtnCompareLastTimeChkListClassify(chartEventArgs);
				}
				else
				{
					loadLastTimeChkListClassify(chartEventArgs);
				}

				loadLastTimeDbcboChartType(chartEventArgs);
				loadLastTimeTemplate(chartEventArgs);
			}
		}

		private string getDescription(string columnName, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			string description = string.Empty;

			ChartViewHelper chartViewHelper = new ChartViewHelper();

			description = chartViewHelper.GetDescription(columnName, columnPropertyInfos);

			return description;
		}

		private string getColumnPropertyInfoName(string description, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			string name = string.Empty;

			ChartViewHelper chartViewHelper = new ChartViewHelper();

			name = chartViewHelper.GetColumnPropertyInfoName(description, columnPropertyInfos);

			return name;
		}

		private string addAllowChartAxesToChkListClassify(string chkListClassifyItem, string allowChartAxes)
		{
			string allowChartAxesToChkListClassify = string.Empty;
			
			StringBuilder stringBuilder = new StringBuilder();
			allowChartAxesToChkListClassify = stringBuilder.Append(chkListClassifyItem)
				.Append(ChartViewStatic.LEFT_BRACKET)
				.Append(allowChartAxes)
				.Append(ChartViewStatic.RIGHT_BRACKET)
				.ToString();

			return allowChartAxesToChkListClassify;
		}

		private string spliteAllowChartAxesFromChkListClassify(string allowChartAxesToChkListClassify)
		{
			string chkListClassifyItem = string.Empty;

			string[] chkListClassifyItems = allowChartAxesToChkListClassify.Split(ChartViewStatic.LEFT_BRACKET.ToCharArray());

			if (null != chkListClassifyItems)
			{
				chkListClassifyItem = chkListClassifyItems[0];
			}

			return chkListClassifyItem;
		}

		#region Template

		private string getFullTemplateFileName(string module)
		{
			string fullName = string.Empty;

			ChartViewHelper chartViewHelper = new ChartViewHelper();

			fullName = chartViewHelper.GetFullTemplateFileName(module);

			return fullName;
		}

		private List<TemplateInfo> getTemplateInfos(string module)
		{
			List<TemplateInfo> templateInfos = new List<TemplateInfo>();

			ChartViewHelper chartViewHelper = new ChartViewHelper();
			templateInfos = chartViewHelper.GetTemplateInfos(module);

			return templateInfos;
		}

		private TemplateInfo getTemplateInfo(string module, string fileName)
		{
			List<TemplateInfo> templateInfos = getTemplateInfos(module);
			TemplateInfo templateInfo = new TemplateInfo();

			foreach (TemplateInfo innerTemplateInfo in templateInfos)
			{
				if (innerTemplateInfo.Name == fileName)
				{
					templateInfo = innerTemplateInfo;
					break;
				}
			}

			return templateInfo;
		}

		private List<string> getTemplateNames(string module)
		{
			List<string> templateNames = new List<string>();

			List<TemplateInfo> templateInfos = getTemplateInfos(module);

			foreach (TemplateInfo templateInfo in templateInfos)
			{
				templateNames.Add(templateInfo.Name);
			}

			return templateNames;
		}

		private void updateFileInfo(string module, string fileName)
		{
			List<TemplateInfo> templateInfos = getTemplateInfos(module);

			int index = -1;
			for (int i = 0; i < templateInfos.Count; i++)
			{
				if (templateInfos[i].Name == fileName)
				{
					index = i;
					break;
				}
			}

			templateInfos[index].UpdateTime = DateTime.Now;

			string fullFileName = getFullTemplateFileName(module);

			EntityXmlSerializer<TemplateInfo> entityXmlSerializer = new EntityXmlSerializer<TemplateInfo>();
			string xml = entityXmlSerializer.Serializer(templateInfos);

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);

			xmlDocument.Save(fullFileName);
		}

		private void bindDbcboTemplate(string module)
		{
			dbcboTemplate.Items.Clear();

			string item = getMessage(ChartViewStatic.TEMPLATE_KEY);
			dbcboTemplate.Items.Add(item);

			List<string> templateNames = getTemplateNames(module);

			foreach (string templateName in templateNames)
			{
				dbcboTemplate.Items.Add(templateName);
			}

			dbcboTemplate.SelectedIndex = 0;
		}

		private TemplateInfo getCurrentTemplateInfo()
		{
			TemplateInfo templateInfo = new TemplateInfo();

			string choosedAnalysis = getChoosedRadioButton();
			templateInfo.AnalyseData = choosedAnalysis;

			SeriesChartType choosedSeriesChartType = getChoosedSeriesChartType();
			templateInfo.ChartType = choosedSeriesChartType.ToString();

			Dictionary<string, DataColumn> dictionary = null;

			if (choosedAnalysis == ChartViewStatic.R_BTN_COMPARE_NAME)
			{
				dictionary = getRBtnCompareChoosedAxisNamesDictionary(_DataColumnCollection, _ColumnPropertyInfos);
			}
			else
			{
				dictionary = getChoosedAxisNamesDictionary(_DataColumnCollection, _ColumnPropertyInfos);
			}

			if (0 != dictionary.Count)
			{
				templateInfo.YAxis = dictionary[ChartViewStatic.YAXIS].ToString();
				templateInfo.XAxis = dictionary[ChartViewStatic.XAXIS].ToString();
				if (dictionary.Keys.Contains(ChartViewStatic.ZAXIS))
				{
					templateInfo.ZAxis = dictionary[ChartViewStatic.ZAXIS].ToString();
				}
			}

			templateInfo.UpdateTime = DateTime.Now;

			return templateInfo;
		}

		private void setChoosedRadioButton(TemplateInfo templateInfo)
		{
			List<RadioButton> radioButtons = new List<RadioButton>();

			radioButtons.Add(rBtnAnalysis);
			radioButtons.Add(rBtnCompare);
			radioButtons.Add(rBtnDirect);

			foreach (RadioButton radioButton in radioButtons)
			{
				if (templateInfo.AnalyseData == radioButton.Name)
				{
					radioButton.Checked = true;
				}
			}
		}

		private void setChkListClassify(TemplateInfo templateInfo)
		{
			for (int i = 0; i < chkListClassify.Items.Count; i++)
			{
				chkListClassify.SetItemChecked(i, false);
			}

			if (!string.IsNullOrEmpty(templateInfo.XAxis))
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(templateInfo.XAxis, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
			if (!string.IsNullOrEmpty(templateInfo.YAxis))
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(templateInfo.YAxis, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
			if (!string.IsNullOrEmpty(templateInfo.ZAxis))
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(templateInfo.ZAxis, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
		}

		private void setDbcboChartType(TemplateInfo templateInfo)
		{
			int index = 0;

			for (int i = 0; i < dbcboChartType.Items.Count; i++)
			{
				string chartType = getChartTypeItem(templateInfo.ChartType);

				if (dbcboChartType.Items[i].ToString() == chartType)
				{
					index = i;
					break;
				}
			}

			dbcboChartType.SelectedIndex = index;
		}

		private void configTemplateSetting(TemplateInfo templateInfo)
		{
			setChoosedRadioButton(templateInfo);
			bindDbcboChartType(templateInfo.AnalyseData);

			if (templateInfo.AnalyseData != ChartViewStatic.R_BTN_COMPARE_NAME)
			{
				setChkListClassify(templateInfo);
			}
			else
			{
				setRBtnCompareClassify(templateInfo);
			}

			setDbcboChartType(templateInfo);
		}

		private bool judgeIsAllowedYAxes(string allowedAxes)
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			bool result = chartViewHelper.JudgeIsAllowedYAxes(allowedAxes);

			return result;
		}

		private bool judgeIsAllowedXAxes(string allowedAxes)
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			bool result = chartViewHelper.JudgeIsAllowedXAxes(allowedAxes);

			return result;
		}

		private bool judgeIsAllowedZAxes(string allowedAxes)
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			bool result = chartViewHelper.JudgeIsAllowedZAxes(allowedAxes);

			return result;
		}

		#endregion

		#region Compare

		private void dbcboCompare_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;

			if (null != comboBox)
			{
				bindRBtnCompareChkListClassify(_ColumnPropertyInfos, _DataColumnCollection);
			}
		}

		private void bindRBtnCompareChkListClassify(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection)
		{
			chkListClassify.Items.Clear();

			DataColumn dataColumnY = getDefaultDataColumnY(columnPropertyInfos, dataColumnCollection);

			string selectedColumnName = getColumnPropertyInfoName(dbcboCompare.SelectedItem.ToString(), columnPropertyInfos);
			DataColumn selectedColumn = getDataColumn(selectedColumnName, dataColumnCollection);

			DataColumn dataColumnX = getNextDataColumnX(columnPropertyInfos, dataColumnCollection, selectedColumn);

			int index = 0;
			foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
			{
				if (innerColumnPropertyInfo.Name != selectedColumnName)
				{
					string displayItemName = addAllowChartAxesToChkListClassify(
						innerColumnPropertyInfo.Description, innerColumnPropertyInfo.AllowChartAxes);
					chkListClassify.Items.Add(displayItemName);
					chkListClassify.SetItemChecked(index, false);

					if (dataColumnY.ColumnName == innerColumnPropertyInfo.Name)
					{
						chkListClassify.SetItemChecked(index, true);
					}

					if ((null != dataColumnX) && (dataColumnX.ColumnName == innerColumnPropertyInfo.Name))
					{
						chkListClassify.SetItemChecked(index, true);
					}

					index++;
				}
			}
		}

		private void bindDbcboCompare(List<ColumnPropertyInfo> columnPropertyInfos, DataColumnCollection dataColumnCollection, int selectIndex)
		{
			dbcboCompare.Items.Clear();

			foreach (ColumnPropertyInfo innerColumnPropertyInfo in columnPropertyInfos)
			{
				bool isAllowedYAxes = judgeIsAllowedYAxes(innerColumnPropertyInfo.AllowChartAxes);

				if (!isAllowedYAxes)
				{
					dbcboCompare.Items.Add(innerColumnPropertyInfo.Description);
				}
			}

			if (dbcboCompare.SelectedIndex != selectIndex)
			{
				dbcboCompare.SelectedIndex = selectIndex;
			}
		}

		private List<DataColumn> getRBtnCompareChoosedDataColumns(DataColumnCollection dataColumnCollection)
		{
			List<DataColumn> dataColumns = new List<DataColumn>();

			string dataColumnName = getColumnPropertyInfoName(dbcboCompare.SelectedItem.ToString(), _ColumnPropertyInfos);
			DataColumn dataColumn = getDataColumn(dataColumnName, dataColumnCollection);

			dataColumns.Add(dataColumn);

			for (int i = 0; i < chkListClassify.CheckedItems.Count; i++)
			{
				string description = spliteAllowChartAxesFromChkListClassify(chkListClassify.CheckedItems[i].ToString());
				string innerDataColumnName = getColumnPropertyInfoName(description, _ColumnPropertyInfos);
				DataColumn innerDataColumn = getDataColumn(innerDataColumnName, dataColumnCollection);
				dataColumns.Add(innerDataColumn);
			}

			return dataColumns;
		}

		private string judgeIsRBtnCompareSuitChoosedXValue(List<DataColumn> dataColumns, List<ColumnPropertyInfo> columnPropertyInfos, DataColumn compareDataColumn)
		{
			string message = string.Empty;

			foreach (DataColumn dataColumn in dataColumns)
			{
				ColumnPropertyInfo columnPropertyInfo = getColumnPropertyInfo(dataColumn.ColumnName, columnPropertyInfos);

				bool isAllowedYAxes = judgeIsAllowedYAxes(columnPropertyInfo.AllowChartAxes);

				if (!isAllowedYAxes && (columnPropertyInfo.Name != compareDataColumn.ColumnName))
				{
					bool isAllowedXAxes = judgeIsAllowedXAxes(columnPropertyInfo.AllowChartAxes);

					if (!isAllowedXAxes)
					{
						message = getMessage(ChartViewStatic.ERROR_RBTN_COMPARE_CHOOSE_XVALUE_KEY);
						break;
					}
				}
			}

			return message;
		}

		private string checkRBtnCompareChoosedDataColumns(List<DataColumn> dataColumns, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			string message = string.Empty;

			if (dataColumns.Count != ChartViewStatic.COMPARE_ITEMS)
			{
				message = getMessage(ChartViewStatic.ERROR_RBTN_COMPARE_CHOOSE_COUNT_KEY);
			}

			if (string.IsNullOrEmpty(message))
			{
				message = judgeIsSuitChoosedYValue(dataColumns, columnPropertyInfos);
			}

			if (string.IsNullOrEmpty(message))
			{
				string compareDataColumnName = getColumnPropertyInfoName(dbcboCompare.SelectedItem.ToString(), columnPropertyInfos);
				DataColumn compareDataColumn = getDataColumn(compareDataColumnName, _DataColumnCollection);

				message = judgeIsRBtnCompareSuitChoosedXValue(dataColumns, columnPropertyInfos, compareDataColumn);
			}

			return message;
		}

		private Dictionary<string, DataColumn> getRBtnCompareChoosedAxisNamesDictionary(
			DataColumnCollection dataColumnCollection, List<ColumnPropertyInfo> columnPropertyInfos)
		{
			Dictionary<string, DataColumn> dictionary = new Dictionary<string, DataColumn>();

			List<DataColumn> dataColumns = getRBtnCompareChoosedDataColumns(dataColumnCollection);

			string errorMessage = checkRBtnCompareChoosedDataColumns(dataColumns, columnPropertyInfos);
			if (string.IsNullOrEmpty(errorMessage))
			{
				List<ColumnPropertyInfo> choosedColumnPropertyInfos =
					getChoosedColumnPropertyInfos(dataColumns, columnPropertyInfos);

				foreach (ColumnPropertyInfo innerColumnPropertyInfo in choosedColumnPropertyInfos)
				{
					bool isAllowedYAxes = judgeIsAllowedYAxes(innerColumnPropertyInfo.AllowChartAxes);

					if (isAllowedYAxes)
					{
						DataColumn dataColumnY = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
						dictionary.Add(ChartViewStatic.YAXIS, dataColumnY);
						choosedColumnPropertyInfos.Remove(innerColumnPropertyInfo);
						break;
					}
				}

				foreach (ColumnPropertyInfo innerColumnPropertyInfo in choosedColumnPropertyInfos)
				{
					string selectedItemName = getColumnPropertyInfoName(dbcboCompare.SelectedItem.ToString(), columnPropertyInfos);
					if (innerColumnPropertyInfo.Name == selectedItemName)
					{
						DataColumn dataColumnZ = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
						dictionary.Add(ChartViewStatic.ZAXIS, dataColumnZ);
					}
					else
					{
						DataColumn dataColumnX = getDataColumn(innerColumnPropertyInfo.Name, dataColumnCollection);
						dictionary.Add(ChartViewStatic.XAXIS, dataColumnX);
					}
				}
			}
			else
			{
				throw new APPException(errorMessage, APPMessageType.DisplayToUser);
			}

			return dictionary;
		}

		private void loadRBtnCompareLastTimeChkListClassify(ChartEventArgs chartEventArgs)
		{
			if (null != chartEventArgs.ZAxis)
			{
				string description = getDescription(chartEventArgs.ZAxis.ColumnName, _ColumnPropertyInfos);
				int index = dbcboCompare.Items.IndexOf(description);
				dbcboCompare.SelectedIndex = index;
			}

			for (int i = 0; i < chkListClassify.Items.Count; i++)
			{
				chkListClassify.SetItemChecked(i, false);
			}

			if (null != chartEventArgs.XAxis)
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(chartEventArgs.XAxis.ColumnName, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
			if (null != chartEventArgs.YAxis)
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(chartEventArgs.YAxis.ColumnName, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
		}

		private void setRBtnCompareClassify(TemplateInfo templateInfo)
		{
			if (!string.IsNullOrEmpty(templateInfo.ZAxis))
			{
				string description = getDescription(templateInfo.ZAxis, _ColumnPropertyInfos);
				int index = dbcboCompare.Items.IndexOf(description);
				dbcboCompare.SelectedIndex = index;
			}

			for (int i = 0; i < chkListClassify.Items.Count; i++)
			{
				chkListClassify.SetItemChecked(i, false);
			}

			if (!string.IsNullOrEmpty(templateInfo.XAxis))
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(templateInfo.XAxis, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);

				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
			if (!string.IsNullOrEmpty(templateInfo.YAxis))
			{
				ColumnPropertyInfo columnPropertyInfo =
					getColumnPropertyInfo(templateInfo.YAxis, _ColumnPropertyInfos);

				string displayItemName = addAllowChartAxesToChkListClassify(
					columnPropertyInfo.Description, columnPropertyInfo.AllowChartAxes);
				
				int index = chkListClassify.Items.IndexOf(displayItemName);
				chkListClassify.SetItemChecked(index, true);
			}
		}

		private void showGroupBoxCompare()
		{
			groupBoxCompare.Visible = true;

			if (!_Expand)
			{
				Point point = new Point(groupBoxClassify.Location.X, groupBoxClassify.Location.Y + groupBoxCompare.Size.Height);
				groupBoxClassify.Location = point;
				_Expand = !_Expand;
			}
		}

		private void bindLstTemplateGroupBoxCompare()
		{
			if (_Expand)
			{
				groupBoxCompare.Visible = false;
				Point point = new Point(groupBoxClassify.Location.X, groupBoxClassify.Location.Y - groupBoxCompare.Size.Height);
				groupBoxClassify.Location = point;
				_Expand = !_Expand;
			}
		}

		#endregion

		#endregion
	}
}
