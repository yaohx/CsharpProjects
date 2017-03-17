using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;

using MB.WcfService.IFace;
using MB.Util.Model;
namespace MB.WcfService.VersionAutoUpdate {
    /// <summary>
    /// 应用程序版本自动更新处理相关。
    /// </summary>
    public abstract class ApplicationVersion : IApplicationVersion {
        private static readonly string VERSION_PATH =  MB.Util.General.GeApplicationDirectory() + @"ClientVersion";
        private static readonly string VERSION_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"ClientVersion\Version {0}";
        //单个buffer 的长度。
        private  static readonly int SINGLE_BUFFER_LENGTH = MB.BaseFrame.SOD.L_SINGLE_PACK_MAX_LENGTH ;
        #region IApplicationVersion 成员
        /// <summary>
        /// 获取当前执行的版本。
        /// </summary>
        /// <returns></returns>
        public virtual double CurrentActiveVersion() {
            MB.Util.TraceEx.Write("ApplicationVersion.CurrentActiveVersion() 版本路径" + VERSION_PATH);
            if (!System.IO.Directory.Exists(VERSION_PATH)) {
                MB.Util.TraceEx.Write("ApplicationVersion.CurrentActiveVersion() 没有设置版本");
                return 0;
            }

            string[] versions = System.IO.Directory.GetDirectories(VERSION_PATH);
            if (versions == null || versions.Length == 0) {
                MB.Util.TraceEx.Write("ApplicationVersion.CurrentActiveVersion() 路径命名不合法");
                return 0;
            }
            SortedList<string, string> sLst = new SortedList<string, string>();
            foreach (var v in versions) {
                string t = System.IO.Path.GetFileName(v);
                sLst.Add(t, t); 
            }
            MB.Util.TraceEx.Write("ApplicationVersion.CurrentActiveVersion() 得到版本:" + string.Join(",", sLst.Values.ToArray()));

            string active = sLst.Values.Last();
            string[] vv = active.Split(' ');
            double num = MB.Util.MyConvert.Instance.ToDouble(vv[1]);
            return num;
        }
        /// <summary>
        /// 获取当前版本所有需要下载的文件。
        /// </summary>
        /// <returns></returns>
        public virtual string CurrentVersionFileNames(double versionNumber) {
            string versionFiles = string.Format(VERSION_FILE_PATH, versionNumber);
            var files = System.IO.Directory.GetFiles(versionFiles);
            List<VersionUpdateFileInfo> vs = new List<VersionUpdateFileInfo>();

            getPackVersionFiles(vs, versionNumber, string.Empty);

            MB.Util.Serializer.EntityXmlSerializer<VersionUpdateFileInfo> serializer = new MB.Util.Serializer.EntityXmlSerializer<VersionUpdateFileInfo>();
            return serializer.Serializer(vs) ;
        }
        /// <summary>
        /// 获取指定版本和文件指定位置的单个Buffer。
        /// </summary>
        /// <param name="versionNumber"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [MB.Aop.InjectionMethodSwitch(false)]
        public virtual byte[] GetFileBuffer(double versionNumber, string fileName, long position) {
            string filePath = string.Format(VERSION_FILE_PATH, versionNumber) + @"\" + fileName;
            if (!System.IO.File.Exists(filePath)) 
                return new byte[0];
  
            byte[] tempBuffer;
            using (FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {

                long fileLength = fStream.Length;

                if (position >= fileLength) return new byte[0];

                fStream.Seek(position, SeekOrigin.Begin);
                if (position + SINGLE_BUFFER_LENGTH > fileLength) {
                    int remainLength = (int)(fileLength - position);
                    tempBuffer = new byte[remainLength];
                    fStream.Read(tempBuffer, 0, remainLength);
                }
                else {
                    tempBuffer = new byte[SINGLE_BUFFER_LENGTH];
                    fStream.Read(tempBuffer, 0, SINGLE_BUFFER_LENGTH);
                }
                fStream.Close();
            }
            return tempBuffer;
        }

        #endregion

        #region 内部函数...
        //判断是否已经发布了大版本；
        //获取大版本的文件；
        //获取小版本需要更新的文件
        private void getPackVersionFiles(List<VersionUpdateFileInfo> fileList,double versionNumber,string childDirectoryName) {
            string versionFilePath = string.Format(VERSION_FILE_PATH, versionNumber);
            if(!string.IsNullOrEmpty(childDirectoryName))
                  versionFilePath += @"\" + childDirectoryName;

            var files = System.IO.Directory.GetFiles(versionFilePath);

            foreach (string f in files) {
                System.IO.FileInfo fInfo = new System.IO.FileInfo(f);
                VersionUpdateFileInfo item = new VersionUpdateFileInfo();
                item.FileName = fInfo.Name;
                item.FileLength = fInfo.Length;
                item.FileExtension = fInfo.Extension;
                item.ChildDirectoryName = childDirectoryName;
                fileList.Add(item);
            }
            string[] childPaths = System.IO.Directory.GetDirectories(versionFilePath);
            foreach (string child in childPaths) {
                //获取子目录的名称
                string name = System.IO.Path.GetFileName(child);
                getPackVersionFiles(fileList, versionNumber, name);  
            }
        }

        #endregion 内部函数...
    }
}
