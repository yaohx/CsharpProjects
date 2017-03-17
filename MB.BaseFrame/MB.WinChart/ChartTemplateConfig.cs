using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using MB.WinChart.Model;
using MB.WinChart.Share;
using MB.Util;
using MB.Util.Serializer;

namespace MB.WinChart
{
	/// <summary>
	/// 模板类.
	/// </summary>
	public partial class ChartTemplateConfig : Form
	{
		#region Private variables

		private string _Module;
		private TemplateInfo _TemplateInfo;

		#endregion

		#region Construct

		/// <summary>
		/// 构造函数.
		/// </summary>
		/// <param name="module">模块标识.</param>
		/// <param name="templateInfo">模板信息.</param>
		public ChartTemplateConfig(string module,TemplateInfo templateInfo)
		{
			InitializeComponent();

			try
			{
				_Module = module;
				_TemplateInfo = templateInfo;

				bindLstTemplate();
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		#endregion

		#region Delegate events

		private System.EventHandler<EventArgs> _TemplateConfigAfterDataApply;

		/// <summary>
		/// 配制模板响应事件.
		/// </summary>
		public event System.EventHandler<EventArgs> TemplateConfigAfterDataApply
		{
			add
			{
				_TemplateConfigAfterDataApply += value;
			}
			remove
			{
				_TemplateConfigAfterDataApply -= value;
			}
		}

		#endregion 

		#region Events

		private void onTemplateConfigAfterDataApply()
		{
			if (_TemplateConfigAfterDataApply != null)
			{
				_TemplateConfigAfterDataApply(this, new EventArgs());
			}
		}

		private void butAdd_Click(object sender, EventArgs e)
		{
			try
			{
				string fileName = txtAddName.Text;

				if (!string.IsNullOrEmpty(fileName))
				{
					createTemplateXml(_Module, fileName, _TemplateInfo);

					txtAddName.Text = string.Empty;
					bindLstTemplate();
					onTemplateConfigAfterDataApply();
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (-1 != lstTemplate.SelectedIndex)
				{
					string name = string.Empty;

					name = lstTemplate.SelectedItem.ToString();

					if (!string.IsNullOrEmpty(name))
					{
						deleteTemplateXml(_Module, name);
						bindLstTemplate();
						onTemplateConfigAfterDataApply();
					}
				}
			}
			catch (Exception ex)
			{
				MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
			}
		}

		private void buttonExit_Click(object sender, EventArgs e)
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

		#endregion

		#region Private methods

		private void setTemplatePath()
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();
			string path = chartViewHelper.GetFullPath();

			DirectoryInfo directoryInfo = new DirectoryInfo(path);

			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
		}

		private string getFullTemplateFileName(string module)
		{
			string fullName = string.Empty;

			setTemplatePath();

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

		private void bindLstTemplate()
		{
			List<string> lists = getTemplateNames(_Module);
			lstTemplate.DataSource = lists;
		}

		private void createTemplateXml(string module, string name, TemplateInfo templateInfo)
		{
			List<TemplateInfo> templateInfos = getTemplateInfos(module);
			TemplateInfo isContainTemplateInfo = new TemplateInfo(name);

			if (templateInfos.Contains(isContainTemplateInfo))
			{
				templateInfos.Remove(isContainTemplateInfo);
			}

			templateInfo.Name = name;
			if (templateInfos.Count >= ChartViewStatic.MAX_TEMPLATE_NUM)
			{
				templateInfos.RemoveAt(templateInfos.Count - 1);
			}

			templateInfos.Add(templateInfo);

			string fullFileName = getFullTemplateFileName(module);

			EntityXmlSerializer<TemplateInfo> entityXmlSerializer = new EntityXmlSerializer<TemplateInfo>();
			string xml = entityXmlSerializer.Serializer(templateInfos);

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);

			xmlDocument.Save(fullFileName);
		}

		private void deleteTemplateXml(string module, string name)
		{
			List<TemplateInfo> templateInfos = getTemplateInfos(module);
			TemplateInfo isContainTemplateInfo = new TemplateInfo(name);

			if (templateInfos.Contains(isContainTemplateInfo))
			{
				templateInfos.Remove(isContainTemplateInfo);
			}

			string fullFileName = getFullTemplateFileName(module);

			EntityXmlSerializer<TemplateInfo> entityXmlSerializer = new EntityXmlSerializer<TemplateInfo>();
			string xml = entityXmlSerializer.Serializer(templateInfos);

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);

			xmlDocument.Save(fullFileName);
		}

		#endregion
	}
}
