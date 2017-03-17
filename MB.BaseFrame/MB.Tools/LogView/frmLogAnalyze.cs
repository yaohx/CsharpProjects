using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.Tools.LogView {
    public partial class frmLogAnalyze : Form {
        public frmLogAnalyze() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                string fullPath = folderBrowserDialog1.SelectedPath;

                List<string> allFiles = new List<string>();
                fillAllFiles(allFiles, fullPath);

                //richTextBox1
                Dictionary<string, string> ips = new Dictionary<string, string>(); 
                
                foreach (string f in allFiles) {
                    string line = string.Empty;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(f)) {
                        while ((line = sr.ReadLine()) != null) {
                            string  ip = LogFileHelper.getClientIP(line);
                            if (!ips.ContainsKey(ip)) {
                                ips.Add(ip, f);
                            }
  
                        }
                    }
                }
                richTextBox1.Clear();
                foreach (string ip in ips.Keys) {
                    richTextBox1.AppendText(ip + " " + ips[ip] );
                    richTextBox1.AppendText("\n");
                }
            }
        }
        

        //获取当前目录以及下级目录的所有的文件。 
        private void fillAllFiles(List<string> allFiles, string path) {
            string[] files = System.IO.Directory.GetFiles(path);
            foreach (string str in files) {
                allFiles.Add(str);
            }
            string[] allPaths = System.IO.Directory.GetDirectories(path);
            foreach (string p in allPaths) {
                fillAllFiles(allFiles, p);
            }
        }

        private void butQAMiddleError_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                string fullPath = folderBrowserDialog1.SelectedPath;

                List<string> allFiles = new List<string>();
                fillAllFiles(allFiles, fullPath);

                //richTextBox1
                Dictionary<string, string> datas = new Dictionary<string, string>();

                foreach (string f in allFiles) {
                    string line = string.Empty;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(f)) {
                        while ((line = sr.ReadLine()) != null) {
                            if (string.IsNullOrEmpty(line)) continue;
                            string type = LogFileHelper.getMessageType(line,new LogDataLoadInfo());
                            if (type!="代码执行轨迹") {
                                if(!datas.ContainsKey(line))
                                    datas.Add(line, f);
                            }

                        }
                    }
                }
                richTextBox1.Clear();
                foreach (string ip in datas.Keys) {
                    richTextBox1.AppendText(ip + " " + datas[ip]);
                    richTextBox1.AppendText("\n");
                }
            }
        }

        private void butQAMiddleLayer_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                string fullPath = folderBrowserDialog1.SelectedPath;

                List<string> allFiles = new List<string>();
                fillAllFiles(allFiles, fullPath);

                //richTextBox1
                Dictionary<string, UserDateInfo> datas = new Dictionary<string, UserDateInfo>();

                foreach (string f in allFiles) {
                    string line = string.Empty;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(f)) {
                        DateTime lastTime = DateTime.MinValue;
                        while ((line = sr.ReadLine()) != null) {
                            if (string.IsNullOrEmpty(line)) continue;
                            DateTime dt = LogFileHelper.getDatetime(line);
                            string ip = LogFileHelper.getClientIP(line);

                            string key = ip + " " + dt.ToString("yyyy-MM-dd HH:mm:ss");
                            if (!datas.ContainsKey(key)) {
                                TimeSpan span = dt.Subtract(lastTime);
                                if(span.TotalSeconds <= 1)
                                    datas.Add(key, new UserDateInfo(ip, dt.ToString("yyyy-MM-dd HH:mm:ss")));

                                lastTime = dt;
                            }
                           
                        }
                        sr.Close();
                    }
                }
                if (datas.Count > 0) {
                    frmViewIpAndDate frm = new frmViewIpAndDate(datas.Values.ToList<UserDateInfo>());
                    frm.ShowDialog(); 
                }
            }
        }

        private void butExport_Click(object sender, EventArgs e) {
            
        }
    }

    public class UserDateInfo {
        public UserDateInfo(string ip,string date) {
            IP = ip;
            Date = date;
        }
        public string IP {
            get;
            set;
        }
        public string Date {
            get;
            set;
        }
    }
}
