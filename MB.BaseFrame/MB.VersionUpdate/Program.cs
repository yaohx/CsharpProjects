using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MB.VersionUpdate {

    class Program {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"> 不同参数之间用分号搁开 第一个为版本号，第二个为应用程序文件名称</param>
        static void Main(string[] args) {

                if (args == null || args.Length != 1) {
                    System.Windows.Forms.MessageBox.Show("不是有效的可执行文件！1" + string.Join(",", args));
                    return;
                }
                string[] s = args[0].Split(',');
                if (s.Length != 2) {
                    System.Windows.Forms.MessageBox.Show("不是有效的可执行文件！2" + string.Join(",", args));
                    return;
                }
                System.Threading.Thread.Sleep(200);

                double versionNumber = System.Convert.ToDouble(s[0]);
                string mainFileName = s[1];

                try {
                    //在更新之前先关闭 主程序
                    string mainName = mainFileName.Substring(0, mainFileName.Length - 4); //把 .exe 去掉
                    Process[] processes = Process.GetProcessesByName(mainName);
                    //遍历与当前进程名称相同的进程列表
                    foreach (Process process in processes) {
                        IniFile.WriteString("Version " + versionNumber.ToString(), "发现并关闭进程" + mainFileName, "...", AppDomain.CurrentDomain.BaseDirectory + @"VersionUpdateLog.ini");
                        process.Kill();
                        System.Threading.Thread.Sleep(200);
                    }
                    VersionUpdate update = new VersionUpdate();
                    update.NewVersionUpdate(versionNumber);

                    update.StartNewApplication(mainFileName, versionNumber);
                }
                catch (MyAppException mex) {
                    System.Windows.Forms.MessageBox.Show(string.Format("版本{0} 更新出错！" + mex, versionNumber));
                }
                catch (Exception ex) {
                    System.Windows.Forms.MessageBox.Show(string.Format("版本{0} 更新出错！" + ex, versionNumber));
                }
        }


    }
}
