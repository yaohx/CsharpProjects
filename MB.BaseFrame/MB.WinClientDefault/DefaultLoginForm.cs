using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault {
    public partial class DefaultLoginForm : MB.WinClientDefault.AbstractBaseForm {// DevExpress.XtraBars.Ribbon.RibbonForm {
        public DefaultLoginForm() {
            InitializeComponent();
            this.Load += new EventHandler(DefaultLoginForm_Load);
        }

        void DefaultLoginForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            //cobLanguage.Items.Add("中文");
            //cobLanguage.Items.Add("英文");
            //cobLanguage.SelectedIndex = 0;

            loadWcfServerCfg();

            MB.WinBase.Ctls.ComboxExtenderHelper.ReadFromFile(cobUserName);
            MB.WinBase.Ctls.ComboxExtenderHelper.ResumeSelected(cobService);
            //本地化处理
            MB.XWinLib.Localization.DevexpressLib.LocalSetting();    
        }
        // 加载配置的WCF 服务项。
        private void loadWcfServerCfg() {
            string servers = System.Configuration.ConfigurationManager.AppSettings["WcfServers"];
            if (string.IsNullOrEmpty(servers))
                throw MB.Util.APPExceptionHandlerHelper.CreateDisplayToUser("请在App.Config 文件在配置 WcfServers 服务项。\n 例如：<add key ='WcfServers' value='本地开发服务器,localhost:8283;测试服务器mb0.credential;小范围服务器,mb1.credential;正式服务器,mb2.credential' />");

            string[] ss = servers.Split(';');
            foreach (string s in ss) {
                string[] vals = s.Split(',');
                if (vals.Length != 2) continue;

                cobService.Items.Add(new MB.Util.Model.ServerConfigInfo(vals[0], vals[1]));
            }
            cobService.SelectedIndex = 0;
        }
        private void butQuit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void butLogin_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(cobUserName.Text)) {
                MB.WinBase.MessageBoxEx.Show("登录用户不能为空,请输入！");
                return;
            }
            using(MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)){ 
                //把选择的服务项存储到
                MB.Util.MyNetworkCredential.CurrentSelectedServerInfo = cobService.SelectedItem as MB.Util.Model.ServerConfigInfo;

                bool existsVersion = CheckApplicationVersion();
                if (existsVersion) return;

                try {

                    OnClickLogin(cobUserName.Text, txtPassword.Text);
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                }
            }
        }
        /// <summary>
        /// 获取或设置应用程序版本号。
        /// </summary>
        protected string VersionNumber {
            get {
                return labVersion.Text;
            }
            set {
                labVersion.Text = value;
            }
        }
        protected virtual string GetApplicationName() {
            throw new MB.Util.APPException(string.Format("登录窗口的 方法{0} 需要覆盖实现", "GetApplicationName"));
        }
        protected virtual bool  CheckApplicationVersion() {
            bool notExistsCfg = MB.WinClientDefault.VersionAutoUpdate.VersionDownloadHelper.NotExistsAutoDownloadCfg();
            if (!notExistsCfg) {
                bool exitsVersion = false;
                try {
                    //cursor.ProcessState.CurrentProcessContent = "版本检查..." ;

                    MB.WinClientDefault.VersionAutoUpdate.VersionAutoUpdateHelper vu = new
                        MB.WinClientDefault.VersionAutoUpdate.VersionAutoUpdateHelper(GetApplicationName());

                    exitsVersion = vu.CheckAndUpdateNewVersion();


                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                    return true;
                }

                if (exitsVersion) return true;
            }
            return false;
        }

        /// <summary>
        /// 获取连接的服务器名称。
        /// </summary>
        protected virtual string GetLinkServerName {
            get {
                return cobService.Text;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        protected virtual void OnClickLogin(string user, string pwd) {

        }

        private void txtPassword_Leave(object sender, EventArgs e) {

        }

        private void cobUserName_Leave(object sender, EventArgs e) {
            cobUserName.Text = cobUserName.Text.ToUpper(); 
        }

        private void cobSkins_SelectedIndexChanged(object sender, EventArgs e) {
           
        }

        private void setDefaultSkinStyle() {
            //switch (cobSkins.SelectedIndex) {
            //    case 0:
            //        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");
            //        break;
            //    case 1:
            //        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue");
            //        break;
            //        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Silver");
            //    case 2:
            //        break;
            //    default:
            //        break;

            //}
        }
    }
}
