//---------------------------------------------------------------- 
// All rights reserved. 
// Author		:	chendc
// Create date	:	2003-01-04
// Description	:	AuthHelper
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Windows.Forms ;
using System.Management;

namespace MB.Aop.SoftRegistry
{
	/// <summary>
	/// AuthHelper 系统系列号鉴定。
	/// </summary>
	public class AuthHelper {
		#region private construct...
		/// <summary>
		/// add private construct function to prevent instance.
		/// </summary>
		private  AuthHelper() {
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#endregion private construct...
		private const int MAX_SHOW_MSG = 10;

		#region public static function...
		/// <summary>
		/// 系统系列号鉴定。
		/// </summary>
		/// <returns></returns>
		public static bool AuthRight(string privateName){
			AuthRightInfo rightData = new AuthRightInfo();
			rightData.SNFileName = privateName;
			bool b = autoRight(rightData);
			return b;
		}
		/// <summary>
		/// 获取用户当前鉴权的信息。
		/// </summary>
		/// <param name="privateName"></param>
		/// <returns></returns>
		public static AuthRightInfo GetAuthInfo(string privateName){
			//UP.Utils.TraceEx.Write("开始进行权限鉴定.....");
			AuthRightInfo rightData = new AuthRightInfo();
			rightData.SNFileName = privateName;
			bool b = autoRight(rightData);
			if(b){
				return rightData;
			}
			return rightData;
		}
		#endregion public static function...
		

		#region private function...
		//权限鉴定
		private static bool autoRight(AuthRightInfo  authDataInfo){
			string file;
			if(authDataInfo.SNFileName!=null && authDataInfo.SNFileName.Length >0)
                file = MB.Util.General.GeApplicationDirectory() + authDataInfo.SNFileName + ".Txt";
			else
                file = MB.Util.General.GeApplicationDirectory() + @"UfAccreditSn.Txt";
			//检验是否已经拥有了授权码
	
			try {
				System.IO.StreamReader  read = new System.IO.StreamReader (file);
				string strTemp = read.ReadLine ();
				read.Close();
				//	TraceEx.Write(strTemp);
				AuthRightInfo tempInfo = AuthDataRight(strTemp);
				if (tempInfo!= null) {
					
					authDataInfo.EndDate  = tempInfo.EndDate;
					DateTime curdate = System.DateTime.Now;
					authDataInfo.LinkCount  = tempInfo.LinkCount;
					if (curdate >= authDataInfo.EndDate) {
						MessageBox.Show ("授权码已经到期,请与软件供应商联系!","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
						authDataInfo.PassIsRight = false;
					}
					else {
						TimeSpan tPan = authDataInfo.EndDate.Subtract(curdate);

						if(tPan.Days  < MAX_SHOW_MSG)
							MessageBox.Show ("授权码 "+ tPan.Days + " 天后到期,请与软件供应商联系!","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

						authDataInfo.PassIsRight = true;
					}
				}//验证授权码是否正确
			}
			catch(Exception ee) {
				//UP.Utils.TraceEx.Write("启动监控获取相关数据出错." + ee.Message);
				authDataInfo.PassIsRight = false;
			}

			if (!authDataInfo.PassIsRight) {
				//启动授权码接受Form
				FrmAuthorization frm = new FrmAuthorization(authDataInfo)  ;
				frm.BringToFront(); 
				frm.ShowDialog();
//				if (!authDataInfo.PassIsRight) {
//					MessageBox.Show("您没有正确的授权!,程序退出","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
//				}
			}
			return authDataInfo.PassIsRight ;
		}
		#endregion private function...
		/// <summary>
		/// 鉴定并分析授权码字符窜。
		/// </summary>
		/// <param name="autoString"></param>
		/// <returns></returns>
		public static AuthRightInfo AuthDataRight(string autoString){

			string descStr = AuthDataEncrypt.DescryptHDString(autoString);
			if(descStr==null || descStr.Length < 16)
				return null;

			string endDate = AuthDataEncrypt.ToDec(descStr.Substring(descStr.Length - 5,5)).ToString();
			descStr = descStr.Substring(0,descStr.Length - 5);

			string linkCount = AuthDataEncrypt.ToDec(descStr.Substring(descStr.Length - 5,5)).ToString() ;
			descStr = descStr.Substring(0,descStr.Length - 5);
			AuthRightInfo autoInfo = new AuthRightInfo();
			autoInfo.EndDate = DateTime.FromOADate(double.Parse(endDate));
			autoInfo.LinkCount = int.Parse(linkCount);
			autoInfo.HardDC = descStr;
           
			return autoInfo;
		}

	}
}
