using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using MB.Util;
using MB.Util.XmlConfig;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 模板信息.
	/// </summary>
	[ModelXmlConfig(ByXmlNodeAttribute = false)]
	public class TemplateInfo
	{
		#region Construct

		/// <summary>
		/// 默认构造函数.
		/// </summary>
		public TemplateInfo()
		{
			// Nothing to do.
		}

		/// <summary>
		/// 构造函数.
		/// </summary>
		/// <param name="name">模板名称.</param>
		public TemplateInfo(string name)
		{
			this._Name = name;
		}

		#endregion

		private string _Name;
		
		/// <summary>
		/// 名称.
		/// </summary>
		[DataMember]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		private string _AnalyseData;
		
		/// <summary>
		/// 分类类型.
		/// </summary>
		[DataMember]
		public string AnalyseData
		{
			get { return _AnalyseData; }
			set { _AnalyseData = value; }
		}

		private string _XAxis;
		
		/// <summary>
		/// X轴信息.
		/// </summary>
		[DataMember]
		public string XAxis
		{
			get { return _XAxis; }
			set { _XAxis = value; }
		}

		private string _YAxis;
		
		/// <summary>
		/// Y轴信息.
		/// </summary>
		[DataMember]
		public string YAxis
		{
			get { return _YAxis; }
			set { _YAxis = value; }
		}

		private string _ZAxis;

		/// <summary>
		/// Z轴信息.
		/// </summary>
		[DataMember]
		public string ZAxis
		{
			get { return _ZAxis; }
			set { _ZAxis = value; }
		}

		private string _ChartType;

		/// <summary>
		/// 图表类型.
		/// </summary>
		[DataMember]
		public string ChartType
		{
			get { return _ChartType; }
			set { _ChartType = value; }
		}

		private DateTime _UpdateTime;

		/// <summary>
		/// 修改时间.
		/// </summary>
		[DataMember]
		public DateTime UpdateTime
		{
			get { return _UpdateTime; }
			set { _UpdateTime = value; }
		}

		#region Override method

		/// <summary>
		/// 比较相等项.
		/// </summary>
		/// <param name="obj">数据源.</param>
		/// <returns>
		/// <c>ture</c>相等.
		/// <c>false</c>不相等.
		/// </returns>
		public override bool Equals(object obj)
		{
			TemplateInfo templateInfo = obj as TemplateInfo;

			return (this._Name == templateInfo.Name);
		}

		/// <summary>
		/// 重写<c>Hash</c>值.
		/// </summary>
		/// <returns>Hashed值.</returns>
		public override int GetHashCode()
		{
			return this._Name.GetHashCode();
		}

		#endregion
	}
}
