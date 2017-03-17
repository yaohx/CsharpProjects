using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

using MB.WinChart.Share;
using MB.Util;

namespace MB.WinChart.Resource
{
	/// <summary>
	/// 获取资源信息.
	/// </summary>
	public class ResourcesHelper
	{
		private ResourceManager resourceManager;

		/// <summary>
		/// 获取资源信息.
		/// </summary>
		/// <param name="key">关键字.</param>
		/// <returns>取得的信息.</returns>
		public string GetMessage(string key)
		{
			try
			{
				string fileName = GetType().Namespace + ChartViewStatic.RESOURCES_FILE_NAME;
				string message = string.Empty;

				Assembly assembly = Assembly.GetExecutingAssembly();
				resourceManager = new ResourceManager(fileName, assembly);

				message = resourceManager.GetString(key);

				return message;
			}
			catch (Exception ex)
			{
				string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

				throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
			}
		}

		#region private method

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

		#endregion
	}
}
