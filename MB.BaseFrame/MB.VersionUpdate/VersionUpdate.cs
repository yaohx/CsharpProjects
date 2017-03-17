using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MB.VersionUpdate {
    class VersionUpdate {
        private static readonly string APP_ROOT_PATH = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string VERSION_UPDATE_PATH = APP_ROOT_PATH + @"AutoUpdate\Version {0}";
        private static readonly string DOWNLOAD_LOG = VERSION_UPDATE_PATH + @"\VersionDownloadLog.ini";
        private static readonly string VERSION_UPDATE_KEY =  "VersionUpdate";
        private static readonly string REPLACE_EMPTY_PATH = @"\AutoUpdate\Version {0}";
        /// <summary>
        ///  新版本更新
        /// </summary>
        /// <param name="versionNumber"></param>
        public void NewVersionUpdate(double versionNumber) {
            try {
                bool existsMainFile = false;
                //启动bat 文件完成主文件的更新
                IniFile.WriteString("Version " + versionNumber, "开始更新版本", "...", APP_ROOT_PATH + @"VersionUpdateLog.ini");
                string rootPath = string.Format(VERSION_UPDATE_PATH, versionNumber);

                copyNewVersionFiles(rootPath, versionNumber);

                try {
                    //启动bat 文件完成主文件的更新
                    IniFile.WriteString("Version " + versionNumber, VERSION_UPDATE_KEY, "true", APP_ROOT_PATH + @"VersionUpdateLog.ini");
                    IniFile.WriteString("VersionUpdate", VERSION_UPDATE_KEY, "true", string.Format(DOWNLOAD_LOG, versionNumber));
                }
                catch (Exception x) {
                    throw new MyAppException("在版本更新时，记录版本号出错，请检查！");
                }
            }
            catch (MyAppException mEx) {
                throw mEx;
            }
            catch (Exception ex) {
                IniFile.WriteString("Version " + versionNumber, VERSION_UPDATE_KEY, "error", APP_ROOT_PATH + @"VersionUpdateLog.ini");
                throw ex;
            }
        }
        /// <summary>
        /// 启动应用程序。
        /// </summary>
        /// <param name="appName"></param>
        public void StartNewApplication(string appName, double versionNumber) {
            System.Windows.Forms.Application.Exit();
            string val = IniFile.ReadString("Version " + versionNumber, VERSION_UPDATE_KEY, "true", APP_ROOT_PATH + @"VersionUpdateLog.ini");
            if (string.Compare(val, "True", true) != 0)
                return;

            string fileFullName = APP_ROOT_PATH + appName;
            if (!System.IO.File.Exists(fileFullName)) {
                System.Windows.Forms.MessageBox.Show(string.Format("要打开的应用程序 {0} 不存在！",fileFullName));
                return;
            }
            IniFile.WriteString("Version " + versionNumber, "正在启动应用程序... " + System.DateTime.Now.ToString(), APP_ROOT_PATH + appName , APP_ROOT_PATH + @"VersionUpdateLog.ini");
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(APP_ROOT_PATH + appName, string.Empty);
            System.Diagnostics.Process.Start(startInfo);
        }

        //覆盖新版本。
        private void copyNewVersionFiles(string rootPath, double versionNumber) {
            string[] files = System.IO.Directory.GetFiles(rootPath);
            //记录版本更新的时间
            IniFile.WriteString("Version " + versionNumber, "版本更新时间", System.DateTime.Now.ToString(), APP_ROOT_PATH + @"VersionUpdateLog.ini");
            foreach (var f in files) {
                string cFilePath = f.Replace(string.Format(REPLACE_EMPTY_PATH, versionNumber), string.Empty);
                string fileName = System.IO.Path.GetFileName(cFilePath);
                //判断是否为主控文件，主控文件不能直接覆盖,通过启动Bat 文件来完成更新
                if (string.Compare(fileName, "VersionDownloadLog.ini", true) == 0)
                    continue;

                try {
                    createFilePath(cFilePath);
                    setFileAllowRead(cFilePath);

                    System.IO.File.Copy(f, cFilePath, true);
                    //记录版本更新日志
                    IniFile.WriteString("Version " + versionNumber, cFilePath, "完成", APP_ROOT_PATH + @"VersionUpdateLog.ini");
                }
                catch (Exception ex) {
                    IniFile.WriteString("Version " + versionNumber, cFilePath, "出错" + ex.Message, APP_ROOT_PATH + @"VersionUpdateLog.ini");
                    throw new MyAppException("在版本更新时，记录版本更新日志出错！"); 
                }

            }
            string[] directorys = System.IO.Directory.GetDirectories(rootPath);
            if (directorys != null && directorys.Length > 0) {
                foreach (var d in directorys)
                    copyNewVersionFiles(d, versionNumber);
            }
        }
        //设置文件为只读状态
        private void setFileAllowRead(string fileFullPath) {
            if (!System.IO.File.Exists(fileFullPath)) return;

            FileInfo fInfo = new FileInfo(fileFullPath);
            if( fInfo.IsReadOnly)
                fInfo.IsReadOnly = false;
        }
        //创建文件目录
        private void createFilePath(string fileFullPath) {
            string path = System.IO.Path.GetDirectoryName(fileFullPath);
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

        }
    }
}
