using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinClientDefault.VersionAutoUpdate {
    /// <summary>
    /// 文件更新辅助类。
    /// </summary>
    [MB.Aop.InjectionManager]
    public class VersionAutoUpdateHelper : System.ContextBoundObject {
        private static readonly string APP_ROOT_PATH = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string AUTO_UPDATE_PATH = APP_ROOT_PATH + @"AutoUpdate";
        private static readonly string VERSION_UPDATE_PATH = APP_ROOT_PATH + @"AutoUpdate\Version {0}";
        private static readonly string DOWNLOAD_LOG = VERSION_UPDATE_PATH + @"\VersionDownloadLog.ini";

        private static readonly string VERSION_NUMBER_CFG_KEY = "VersionNumber";
        private static readonly string VERSION_UPDATE_KEY = "VersionUpdate";
        private static readonly string APPLICATION_INFO = APP_ROOT_PATH + "ApplicationInformation.ini";
        private  string _ApplicationName ;//= "DXDH.exe";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationName"></param>
        public VersionAutoUpdateHelper(string applicationName) {
            _ApplicationName = applicationName;
        }
        /// <summary>
        /// 获取客户端版本信息。
        /// </summary>
        /// <returns></returns>
        public static double GetClientVersionNumber() {
            var num = MB.Util.IniFile.ReadString("VersionInformation", VERSION_NUMBER_CFG_KEY, string.Empty, APPLICATION_INFO);
            if (string.IsNullOrEmpty(num))
                num = "0";
            try {
                return System.Convert.ToDouble(num);
            }
            catch {
                return 0;
            }
        }

        /// <summary>
        /// 判断是否存在已经下载完成但未更新的版本，如果存在就更新它。
        /// </summary>
        /// <returns></returns>
        public bool CheckAndUpdateNewVersion() {
            try {
              // MB.Util.TraceEx.Write("52"); 
                if (!System.IO.Directory.Exists(AUTO_UPDATE_PATH))
                    return checkDownloadVersionFiles();
               
                string[] versions = System.IO.Directory.GetDirectories(AUTO_UPDATE_PATH);
                if (versions == null || versions.Length == 0) {
                    return checkDownloadVersionFiles();
                }
               // MB.Util.TraceEx.Write("60"); 
                double clientVersion = GetClientVersionNumber();
                List<string> lst = new List<string>(versions);
                lst.Sort();
                string active = lst.Last();
           
                string pathName =  System.IO.Path.GetFileName(active);
            
                string[] v = pathName.Split(' ');
          
                double lastVersion = System.Convert.ToDouble(v[1]);
                //如果当前应用程序的版本号大于或者等于已下载或者已完成下载的版本,那么就判断并下载是否存在的新版本
                VersionDownloadHelper download = new VersionDownloadHelper();
                double serverNewNumber = 0;
                bool exists = download.CheckExistsNewVersion(out serverNewNumber);

                if (clientVersion >= lastVersion || lastVersion < serverNewNumber) {
                    return checkDownloadVersionFiles();
                }
               // MB.Util.TraceEx.Write("76"); 
                string lastVersionLog = string.Format(DOWNLOAD_LOG, lastVersion);
                string val = MB.Util.IniFile.ReadString("VersionUpdate", "DownLoad", string.Empty, lastVersionLog);
                //判断版本是否已经下载
                if (string.Compare(val, "True", true) == 0) {
                  //  MB.Util.TraceEx.Write("81"); 
                    completedNewVersionUpdate(lastVersion);
                }
                else {
                  //  MB.Util.TraceEx.Write("85"); 
                    //存在未完成的版本。
                    return checkDownloadVersionFiles();
                }
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex,string.Empty );
            }
            //最后表示不存在新版本 
            return false;
        }
        // 通过复制完成最后版本的更新
        private void completedNewVersionUpdate(double versionNumber) {
#if(!DEBUG)
           // MB.Util.TraceEx.Write("100"); 
            string rootPath = string.Format(VERSION_UPDATE_PATH, versionNumber);
            string lastVersionLog = string.Format(DOWNLOAD_LOG, versionNumber);
            //判断是否已经更新完成
            //更新最新版本号     (双保险)
            string val1 = MB.Util.IniFile.ReadString("Version " + versionNumber, VERSION_UPDATE_KEY, string.Empty, APP_ROOT_PATH + @"VersionUpdateLog.ini");
            string val2 = MB.Util.IniFile.ReadString("VersionUpdate", VERSION_UPDATE_KEY, string.Empty, lastVersionLog);
           // MB.Util.TraceEx.Write("106");
            if (string.Compare(val1, "True", true) == 0 || string.Compare(val2, "True", true) == 0) {
                //System.Configuration.ConfigurationManager.AppSettings.Set(VERSION_NUMBER_CFG_KEY, versionNumber.ToString());
                try {
                    //MB.Util.AppConfigSetting.SetKeyValue(VERSION_NUMBER_CFG_KEY, versionNumber.ToString());
                    //MB.Util.TraceEx.Write("110"); 
                    MB.Util.IniFile.WriteString("VersionInformation", VERSION_NUMBER_CFG_KEY, versionNumber.ToString(), APPLICATION_INFO);
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException("检查自动版本更新配置或者设置版本信息时出错，请把App.Config 的只读属性去掉！", MB.Util.APPMessageType.DisplayToUser, ex);
                }
                MB.Util.IniFile.WriteString("VersionUpdate", "成功设置版本号 " + System.DateTime.Now.ToString(), versionNumber.ToString(), lastVersionLog);
            }
            else {
                
                MB.Util.IniFile.WriteString("VersionUpdate", "开始执行版本文件更新 " + System.DateTime.Now.ToString(), versionNumber.ToString(), lastVersionLog);
              //  MB.Util.TraceEx.Write("122");
                createAndStartCopyExeFile(versionNumber);

            }
#endif
        }

        #region 更新主文件处理相关...
        //创建更新 BAT 文件。
        private void createAndStartCopyExeFile(double versionNumber) {
            System.Reflection.Assembly DLL = System.Reflection.Assembly.GetExecutingAssembly();

            System.IO.Stream fs = null;
            System.IO.FileStream sw = null;
            try {
                fs = DLL.GetManifestResourceStream("MB.WinClientDefault.VersionAutoUpdate.MB.VersionUpdate.exe");
                sw = new FileStream(APP_ROOT_PATH + "MB.VersionUpdate.exe", FileMode.Create, FileAccess.Write);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, System.Convert.ToInt32(fs.Length));
                sw.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                try {
                    sw.Flush();
                    sw.Close();
                }
                catch { }
            }

            //执行BAT 文件 并启动老进程
            string pars = versionNumber.ToString() + "," + _ApplicationName;


            startNewApplication("MB.VersionUpdate.exe", pars);

        }
        /// 启动应用程序。
        private void startNewApplication(string appName, string pars) {

            System.Windows.Forms.Application.Exit();
            string fileFullName = APP_ROOT_PATH + appName;
            if (!System.IO.File.Exists(fileFullName)) {
                System.Windows.Forms.MessageBox.Show(string.Format("要打开的应用程序 {0} 不存在！"), fileFullName);
                return;
            }
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(APP_ROOT_PATH + appName, pars);
            System.Diagnostics.Process.Start(startInfo);

        }
        #endregion 更新主文件处理相关...

        // 检查并下载新版本。
        private bool checkDownloadVersionFiles() {
            VersionDownloadHelper download = new VersionDownloadHelper();
            double newNumber = 0;
            bool exists = download.CheckExistsNewVersion(out newNumber);
            if (exists) {
                bool b = download.DownloadVersionFileDialog();

                if (b) {
#if(!DEBUG)
                    //startNewApplication(_ApplicationName, string.Empty);
                    //如果成功下载，需要启动Copy 新版本的功能
                    string lastVersionLog = string.Format(DOWNLOAD_LOG, newNumber);
                   string val = MB.Util.IniFile.ReadString("VersionUpdate", "DownLoad", string.Empty, lastVersionLog);
                   //判断版本是否已经下载
                   if (string.Compare(val, "True", true) == 0) {
                       completedNewVersionUpdate(newNumber);
                   }
#else
                    MB.Util.IniFile.WriteString("VersionInformation", VERSION_NUMBER_CFG_KEY, newNumber.ToString(), APPLICATION_INFO);
#endif

                }
                return true;
            }
            return false;
        }


    }
}
