using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.VersionAutoUpdate {
   /// <summary>
   ///  版本自动更新需要实现的接口。
   /// </summary>
   public interface IClientVersionUpdate {
        /// <summary>
        ///  检查是否需要存在新的版本。
        /// </summary>
        /// <returns></returns>
        bool CheckExistsNewVersion();
        /// <summary>
        /// 获取服务端发布的当前活动版本号。
        /// </summary>
        /// <returns></returns>
        double GetServerPackClientVersion();
        /// <summary>
        /// 获取需要下载的版本文件名称。
        /// </summary>
        /// <param name="versionNumber"></param>
        /// <returns></returns>
        List<string> GetVersionFileNames(double versionNumber);
        /// <summary>
        ///  获取单个Buffer。
        /// </summary>
        /// <param name="versionNumber"></param>
        /// <param name="fileName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        byte[] GetFileBuffer(double versionNumber, string fileName, long index);

    }
}
