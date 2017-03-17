//---------------------------------------------------------------- 
// All rights reserved. 
// Author		:	chendc
// Create date	:	2003-01-04
// Description	:	AuthRightInfo
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Text;

namespace MB.Aop.SoftRegistry
{
	/// <summary>
	/// AuthRightInfo 权限鉴定的描述信息。
	/// </summary>
	public class AuthRightInfo {

		#region 变量定义...
		//判断授权是否通过
		private bool _PassIsRight;
		//软件使用的最后时间
		private DateTime _EndDate;
		//授权的连接个数
		private int _LinkCount;
		//权限鉴定文件的名称
		private string _SNFileName;
		//当前的硬盘ID
		private string _HardDC;
		//使用次数
		private int _UserCount;
		private ClientAppType _ClientAppType;
		#endregion 变量定义...
		
		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		public AuthRightInfo() {
			//_ClientAppType = CSServer.Utils.ClientAppType.UP2;
		}	
		#endregion 构造函数...

		#region 覆盖基类的方法...
		/// <summary>
		/// 获取描述的字符窜。
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			string dateStr = _EndDate.ToString("yy-MM-dd"); 
			StringBuilder msgStr = new StringBuilder();
			if(_LinkCount > 0)
				msgStr.Append("有" + _LinkCount.ToString() + "用户被授权;");
			msgStr.Append("使用截止期为" + dateStr.Substring(0,2) + "年"+ dateStr.Substring(3,2) + 
						"月" + dateStr.Substring(6,2) + "日");
			if(_UserCount > 0)
				msgStr.Append("使用次数为：" + _UserCount.ToString());
			return msgStr.ToString();
		}

		#endregion 覆盖基类的方法...

		#region Public属性...
		public bool PassIsRight {
			get{
				return _PassIsRight;
			}
			set{
				_PassIsRight = value;
			}
		}
		public DateTime EndDate {
			get{
				return _EndDate;
			}
			set{
				_EndDate = value;
			}
		}
		public int LinkCount {
			get{
				return _LinkCount;
			}
			set{
				_LinkCount = value;
			}
		}
		public string SNFileName{
			get{
				return _SNFileName;
			}
			set{
				_SNFileName = value;
			}
		}
		public string HardDC{
			get{
				return _HardDC;
			}
			set{
				_HardDC = value;

			}
		}
		public ClientAppType ClientAppType{
			get{
				return _ClientAppType;
			}
			set{
				_ClientAppType = value;

			}
		}
		public int UserCount {
			get{
				return _UserCount;
			}
			set{
				_UserCount = value;
			}
		}
		#endregion Public属性...
	}
}

