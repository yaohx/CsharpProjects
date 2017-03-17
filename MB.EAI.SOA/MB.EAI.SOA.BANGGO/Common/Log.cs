using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MB.EAI.SOA.BANGGO.Common
{
    public class Log
    {
        public static Log Current=new Log();
        private string tmpLogDir = "temp";
        private readonly string basedir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public void WriteLog(string writeContent)
        {
            string logDir = Path.Combine(basedir, tmpLogDir);
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            string logfile = Path.Combine(logDir, string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd")));
            if (!File.Exists(logfile))
            {
                var fileStream=File.Create(logfile);
                fileStream.Close();
                fileStream.Dispose();
            }
            using (var sw = File.AppendText(logfile))
            {
                sw.WriteLine(writeContent);
                sw.Close();
            }

        }
    }
}