//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	Localization 本地化语言处理类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Resources;
using System.Globalization;

namespace MB.XWinLib.Localization
{
	/// <summary>
	/// PrintLocal 本地化语言处理类。。
	/// </summary>
	public class PrintLocal: DevExpress.XtraPrinting.Localization.PreviewLocalizer {
		ResourceManager manager = null;

		public PrintLocal() {
			CreateResourceManager();
		}
		protected virtual void CreateResourceManager() {
			if(manager != null) this.manager.ReleaseAllResources();
            string resName = "MB.XWinLib.Localization.PrintLocalRes";
			CultureInfo curInfo = System.Threading.Thread.CurrentThread.CurrentCulture    ;
			string cName = curInfo.Name;
			if(!cName.Equals("zh-CN")) //如果是中文的话直接获取默认值就可以。
                resName = resName + "_en";
				//resName = resName + "_" + cName;
			this.manager = new ResourceManager(resName, this.GetType().Assembly);
		}
		protected virtual ResourceManager Manager { get { return manager; } }
		public override string Language { get { return CultureInfo.CurrentUICulture.Name; }}
		public override string GetLocalizedString(DevExpress.XtraPrinting.Localization.PreviewStringId id) {
                string resStr = "PreviewStringId." + id.ToString();
                string ret = Manager.GetString(resStr);
                if (ret == null) {
                    MB.Util.TraceEx.Write(string.Format("在PrintLocal 字符 {0} 语言本地化时没找到", resStr));

                    ret = "";
                }
                return ret;
          
		}
	}
}
