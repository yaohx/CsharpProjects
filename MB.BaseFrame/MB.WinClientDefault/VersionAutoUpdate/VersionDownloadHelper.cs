//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	版本更新处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using MB.WinBase.VersionAutoUpdate;
using MB.Util.Model;
namespace MB.WinClientDefault.VersionAutoUpdate {
    /// <summary>
    /// 客户端版本自动更新处理。
    /// </summary>
    [MB.Aop.InjectionManager]
    public class VersionDownloadHelper : System.ContextBoundObject, IDisposable, MB.WinBase.IFace.IWaitDialogFormHoster {                     
        private static readonly string VERSION_AUTO_UPDATE_CFG = "VersionAutoUpdate";
  

    //    private static readonly string VERSION_NUMBER = "VersionNumber";
        private static readonly string VERSION_UPDATE_PATH = MB.Util.General.GeApplicationDirectory() + @"AutoUpdate\Version {0}\";
        private static readonly string TEMP_FILE_EXTENSION =".~mbtemp";
        private static readonly string DOWNLOAD_LOG = "VersionDownloadLog.ini";

        private static readonly int SINGLE_FILE_MAX_LENGTH =  MB.BaseFrame.SOD.L_SINGLE_PACK_MAX_LENGTH;
        private IVersionDownload _AutoUpdate;
        private FileDownloadWaitDialog _DownloadWaitDialog;
        private MB.Util.AsynWorkThread _WorkThread;
        private MB.WinBase.Common.WorkWaitDialogArgs _WaitProcessState;
        private List<VersionUpdateFileInfo> _VersionDownloadFiles;
        private double _CurrentNewVersionNumber = 0;

        /// <summary>
        /// 判断是否版本自动更新配置信息。
        /// </summary>
        public static bool NotExistsAutoDownloadCfg() {
            string versionCfg = System.Configuration.ConfigurationManager.AppSettings[VERSION_AUTO_UPDATE_CFG];
            return  string.IsNullOrEmpty(versionCfg);
        }
 

        #region 自定义事件处理相关...
        private System.EventHandler<RunWorkerCompletedEventArgs> _WorkerCompleted;
        public event System.EventHandler<RunWorkerCompletedEventArgs> WorkerCompleted {
            add {
                _WorkerCompleted += value;
            }
            remove {
                _WorkerCompleted -= value;
            }
        }
        private void onWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (_WorkerCompleted != null)
                _WorkerCompleted(sender, e);

            
        }
        #endregion 自定义事件处理相关...

        //2 数相除，返回整数，不进行四五入
        public static int DividendToInt32(long dividend, long divisor) {
            long c;
            long re = Math.DivRem(dividend, divisor, out c);

            return System.Convert.ToInt32(c > 0 ? re + 1 : re);
        }

        /// <summary>
        ///  客户端版本自动更新处理。
        /// </summary>
        public VersionDownloadHelper() {

            _WaitProcessState = new MB.WinBase.Common.WorkWaitDialogArgs();

            createClientAutoUpdateObject();
        }

        /// <summary>
        /// 判断并显示下载处理窗口。
        /// </summary>
        /// <returns></returns>
        public bool DownloadVersionFileDialog() {

            bool b = CheckExistsNewVersion(out _CurrentNewVersionNumber);
            if (!b) return false;
            //初始化下栽文件
            iniForDownloadFiles(_CurrentNewVersionNumber);

            _WorkThread = new MB.Util.AsynWorkThread();
            _DownloadWaitDialog = new FileDownloadWaitDialog(this, _AutoUpdate, _VersionDownloadFiles);
            _DownloadWaitDialog.ClickCanceled += new EventHandler(_DownloadWaitDialog_ClickCanceled);
            DialogResult re = _DownloadWaitDialog.ShowDialog();

            return !_WaitProcessState.Cancel;
        }

        void _DownloadWaitDialog_ClickCanceled(object sender, EventArgs e) {
            cancelLoad(); 
        }
        /// <summary>
        /// 开始执行线程工作。
        /// </summary>
        public void BeginRunWork() {
            //初始化工作线程
            iniAsynWorkThread();
            _WorkThread.RunWorkerAsync(_CurrentNewVersionNumber);
        }

        /// <summary>
        /// 判断是否存在新版本。
        /// 注意：主版本的发布一定要以整数的形式来发布。
        /// </summary>
        /// <param name="newNumber"></param>
        /// <returns></returns>
        public bool CheckExistsNewVersion(out double newNumber) {
            double currentVersion = VersionAutoUpdateHelper.GetClientVersionNumber();
            try {
                double serverVersionNum = _AutoUpdate.GetServerPackClientVN();
                newNumber = serverVersionNum;

                bool exists = serverVersionNum > currentVersion;

                int sNum = (int)newNumber;
                int cNum = (int)currentVersion;
                //如果大版本不一致的话要先下载大版本(针对美邦的现状来进行解决)
                if (sNum > cNum && sNum > 1)
                    newNumber = sNum;

                return exists;
            }
            catch(System.ServiceModel.CommunicationObjectFaultedException cex){
                throw new MB.Util.APPException("连接服务有误,请检查服务配置是否正确或服务是否已启动", MB.Util.APPMessageType.DisplayToUser,cex );  
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Empty); 
                //throw new MB.Util.APPException("获取服务端的版本号出错,请检查版本自动更新的组件配置是否正确！", MB.Util.APPMessageType.DisplayToUser, ex);
            }
        }

        // 初始化需要下载的文件列表。
        private void iniForDownloadFiles(double versionNumber) {
            _VersionDownloadFiles = getDownloadFiles(versionNumber);

            resetDownloadFile(_VersionDownloadFiles, versionNumber);
        }

        #region 异步调用处理相关...
        //初始化工作线程。
        private void iniAsynWorkThread() {
            _WorkThread.WorkerReportsProgress = true;
            _WorkThread.WorkerSupportsCancellation = true;
            _WorkThread.DoWork += new DoWorkEventHandler(_WorkThread_DoWork);
            _WorkThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_WorkThread_RunWorkerCompleted);
            _WorkThread.ProgressChanged += new ProgressChangedEventHandler(_WorkThread_ProgressChanged);


        }

        void _WorkThread_ProgressChanged(object sender, ProgressChangedEventArgs e) {

            _WaitProcessState.CurrentProcessContent = e.UserState; 
        }
        void _WorkThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            onWorkerCompleted(sender, e);

            _WaitProcessState.Processed = true;   
        }

        void _WorkThread_DoWork(object sender, DoWorkEventArgs e) {
            versionFileDownload(e);
        }
        //取消数据加载。
        private void cancelLoad() {
            _WorkThread.CancelAsync();
            _WaitProcessState.Cancel = true;
            _WaitProcessState.Processed = true;
            _WorkThread.Dispose();
        }
        //版本文件下载。
        private void versionFileDownload(DoWorkEventArgs e) {
            double versionNumber = (double)e.Argument ;
            foreach (var file in _VersionDownloadFiles) {
                if (file.Completed || file.FileLength == 0) continue;

                string versionTempPath = string.Format(VERSION_UPDATE_PATH, versionNumber);

                string filePath = file.FileName;
                if(!string.IsNullOrEmpty(file.ChildDirectoryName ))
                      filePath = file.ChildDirectoryName + @"\" + filePath;

                if (!string.IsNullOrEmpty(file.ChildDirectoryName))
                    versionTempPath += file.ChildDirectoryName + @"\";

                string fileFullPath = versionTempPath + file.FileName + TEMP_FILE_EXTENSION;
                if (!System.IO.Directory.Exists(versionTempPath))
                    System.IO.Directory.CreateDirectory(versionTempPath);

                //频繁的IO 操作 可能会影响系统性能，在不考虑断点下载的情况下可以进行优化
                using (FileStream fStream = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write)) {

                    while (!file.Completed) {
                        byte[] buffer = _AutoUpdate.GetFileBuffer(versionNumber, filePath, file.HasDownLoad);

                        file.HasDownLoad += buffer.Length;

                        _WorkThread.ReportProgress(DividendToInt32(file.HasDownLoad * 100 , file.FileLength), file);

                        fStream.Write(buffer, 0, buffer.Length);

                        if (buffer.Length < SINGLE_FILE_MAX_LENGTH) {
                            file.HasDownLoad = file.FileLength;
                            _WorkThread.ReportProgress(DividendToInt32(file.HasDownLoad * 100 , file.FileLength), file);
                         
                            file.Completed = true;
                           
                        }

                        System.Threading.Thread.Sleep(100);
                    }
                    fStream.Close();
                }

                //判断单个文件的下载是否已经完成,如果完成那么就更改文件后缀名称。
                if (file.Completed) { 
                    string newFileFullName = versionTempPath + file.FileName;
                    if (System.IO.File.Exists(newFileFullName))
                        System.IO.File.Delete(newFileFullName);

                    System.IO.File.Move(fileFullPath, versionTempPath + file.FileName);
                    System.IO.File.Delete(fileFullPath);
                    System.Threading.Thread.Sleep(150);
                }
            }
            //如果全部都完成了那么再更改路径的名称。
            //string versionPath = string.Format(VERSION_UPDATE_PATH, versionNumber);
            //System.IO.Directory.Move(versionTempPath, versionPath);
            //更改完成的标志
            string iniFullName = string.Format(VERSION_UPDATE_PATH, versionNumber) + DOWNLOAD_LOG;
            MB.Util.IniFile.WriteString("VersionUpdate", "DownLoad", "true", iniFullName);
        }
        #endregion 异步调用处理相关...


        #region 内部函数处理...
        //根据配置信息创建版本自动更新组件。
        private void createClientAutoUpdateObject() {
            string versionCfg = System.Configuration.ConfigurationManager.AppSettings[VERSION_AUTO_UPDATE_CFG];
            if (string.IsNullOrEmpty(versionCfg)) {
                return;
            }

            string[] cfgs = versionCfg.Split(',');
            try {
                _AutoUpdate = MB.Util.DllFactory.Instance.LoadObject(cfgs[0], cfgs[1]) as IVersionDownload;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("版本自动更新组件配置有误！", MB.Util.APPMessageType.DisplayToUser); 
            }
        }
        //获取需要下载的文件描述信息。
        private List<VersionUpdateFileInfo> getDownloadFiles(double versionNumber) {
            string forDownloadFiles = string.Empty;
            try {
                forDownloadFiles = _AutoUpdate.GetUpdateFileNames(versionNumber);
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("从服务端获取版本{0} 需要更新的文件时 出错！", versionNumber), MB.Util.APPMessageType.DisplayToUser,ex);
            }
            if (string.IsNullOrEmpty(forDownloadFiles))
                return null;

            MB.Util.Serializer.EntityXmlSerializer<VersionUpdateFileInfo> files = new MB.Util.Serializer.EntityXmlSerializer<VersionUpdateFileInfo>();

            return files.DeSerializer(forDownloadFiles);
        }
        //根据已经下载的信息重置 文件的内容
        private void resetDownloadFile(List<VersionUpdateFileInfo> fileList,double fileNumber){
            //获取本地的版本已经下载信息
            string versionPath = string.Format(VERSION_UPDATE_PATH, fileNumber);
            if (!System.IO.Directory.Exists(versionPath)) return;

            foreach (var fInfo in fileList) {
                string filePath = versionPath;
                if(string.IsNullOrEmpty(fInfo .ChildDirectoryName))
                    filePath += fInfo.ChildDirectoryName + @"\";

                filePath += fInfo.FileName;
                if (System.IO.File.Exists(filePath)) {
                    fInfo.HasDownLoad = fInfo.FileLength;
                    fInfo.Completed = true;
                }
                else if (System.IO.File.Exists(filePath + TEMP_FILE_EXTENSION)) {
                    FileInfo fInfoData = new FileInfo(filePath + TEMP_FILE_EXTENSION);
                    fInfo.HasDownLoad = fInfoData.Length;
                }
                else {
                    continue;
                }
            }
        }
        #endregion 内部函数处理...


        #region IDisposable 成员

        public void Dispose() {
            try {
                _DownloadWaitDialog.Dispose();
                _DownloadWaitDialog = null;
                _WorkThread.Dispose();
                _WorkThread = null;
            }
            catch { }
        }

        #endregion

        #region IWaitDialogFormHoster 成员
        /// <summary>
        /// 
        /// </summary>
        public MB.WinBase.Common.WorkWaitDialogArgs ProcessState {
            get { return _WaitProcessState; }
        }
        #endregion
    }

}
