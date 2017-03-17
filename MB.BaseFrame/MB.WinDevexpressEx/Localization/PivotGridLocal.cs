//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	PivotGridLocal 本地化语言处理类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Resources;
using System.Globalization;

using DevExpress.XtraPivotGrid.Localization;
namespace MB.XWinLib.Localization
{
	/// <summary>
	/// PivotGridLocal 本地化语言处理类。
	/// </summary>
	public class PivotGridLocal : DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer{
		#region 变量定义...
        private static PivotGridLocal _LocaObj;
		ResourceManager manager = null;
		#endregion 变量定义...

		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		public PivotGridLocal() {
			CreateResourceManager();
		}
		#endregion 构造函数...

		#region 扩展的static 方法...
        public static PivotGridLocal Create() {
			if(_LocaObj == null){
                _LocaObj = new PivotGridLocal();
			}
			return _LocaObj;
		}
		#endregion 扩展的static 方法...

		#region 覆盖基类的方法...
		protected virtual void CreateResourceManager() {
			if(manager != null) this.manager.ReleaseAllResources();
            string resName = "MB.XWinLib.Localization.PivotGridLocalRes";
			CultureInfo curInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
			string cName = curInfo.Name;
			if(!cName.Equals("zh-CN")) //如果是中文的话直接获取默认值就可以。
                resName = resName + "_en";
				//resName = resName + "_" + cName;

			this.manager = new ResourceManager(resName,this.GetType().Assembly);

		}
		protected virtual ResourceManager Manager { get { return manager; } }
		public override string Language { get { return CultureInfo.CurrentUICulture.Name; }}
		public override string GetLocalizedString(PivotGridStringId id) {
			string resStr = "PivotGridStringId." + id.ToString();
			string ret = Manager.GetString(resStr);
            if (ret == null) {
                MB.Util.TraceEx.Write(string.Format("在PivotGridLocal 字符 {0} 语言本地化时没找到", resStr));
                ret = "";
            }
			return ret;
		}
		#endregion 覆盖基类的方法...
	}
}
