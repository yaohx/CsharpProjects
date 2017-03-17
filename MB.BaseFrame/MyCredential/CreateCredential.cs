using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MB.Util;
using MB.Util.Model;
using MB.Util.Serializer;

namespace MyCredential {
    public partial class CreateCredential : Form {
        private static readonly string APP_ROOT_PATH = AppDomain.CurrentDomain.BaseDirectory;
        private CredentialDataHelper _CredentialDataHelper;
        public CreateCredential() {
            InitializeComponent();

            _CredentialDataHelper = new CredentialDataHelper();
            this.Load += new EventHandler(CreateCredential_Load);
        }

        void CreateCredential_Load(object sender, EventArgs e) {

            resetFormatString();

            dataGridDetail.DataSource = _CredentialDataHelper.CurrentBinding.Tables[0].DefaultView;
        }

        private void butCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void butSure_Click(object sender, EventArgs e) {

            string fileName = "uf.credential";
            if (cobHostType.Text == "WindowsServer")
                fileName = "uf.tcp.credential";

            if (string.Compare(txtPassword.Text, txtPassword2.Text) != 0) { 
                MessageBox.Show("密码输入有误，请重新输入！");
                return;
            }
            StringBuilder s = createCredential();
            string dstr = DESDataEncrypt.EncryptString(s.ToString());

            StreamWriter w = new StreamWriter(APP_ROOT_PATH + fileName);
            w.Write(dstr);
            w.Close();
            w.Dispose();

            MessageBox.Show("证书生成成功，请把它发给你的客户端用户！ 证书路径：" + APP_ROOT_PATH + fileName);
            this.Close();
        }

        private StringBuilder createCredential() {
            string baseAddress = "Http://" + txtServerIP.Text;
            if (numPort.Value > 0 && numPort.Value != 80)
                baseAddress += ":" + numPort.Value.ToString();

            WcfCredentialInfo credential = new WcfCredentialInfo(baseAddress, txtUserName.Text, txtPassword.Text, true);
            credential.EndpointFormatString = txtFormatString.Text;
            credential.ReplaceRelativePathLastDot = chkReplaceLastDot.Checked;
            if (cobHostType.Text == "IIS")
                credential.HostType = WcfServiceHostType.IIS;
            else
                credential.HostType = WcfServiceHostType.WS;

            if (!string.IsNullOrEmpty(txtDomain.Text))
                credential.Domain = txtDomain.Text;

            credential.AppendDetails = _CredentialDataHelper.CredentialToString();

            StringBuilder s = new StringBuilder();
            EntityXmlSerializer<WcfCredentialInfo> ser = new EntityXmlSerializer<WcfCredentialInfo>();
            s.Append(ser.SingleSerializer(credential, string.Empty));

            return s;
        }
        private void CreateWindowsCredential_Load(object sender, EventArgs e) {
            txtServerIP.Text = GetLocalhostIPAddress();
            txtUserName.Text = "administrator";
            cobHostType.Items.Add("IIS");
            cobHostType.Items.Add("WindowsServer");
            cobHostType.SelectedIndex = 0;
        }
        /// <summary> 
        /// 获取本机IP 
        /// </summary> 
        /// <returns></returns> 
        public static string GetLocalhostIPAddress() {
            string hostName = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry hostInfo = System.Net.Dns.GetHostEntry(hostName);
            System.Net.IPAddress[] IpAddr = hostInfo.AddressList;

            if (IpAddr.Length > 0)
                return IpAddr[0].ToString();

            return string.Empty;
        }
        private void chkDominUser_CheckedChanged(object sender, EventArgs e) {
            txtDomain.ReadOnly = !chkDominUser.Checked;
        }

        private void cobHostType_SelectedIndexChanged(object sender, EventArgs e) {
            txtBindingType.Text = cobHostType.Text == "IIS" ? "wsHttp" : "netTcp";
            if (cobHostType.Text == "IIS") {
                numPort.Value = 18051;
            }
            else {
                numPort.Value = 8989;
            }
        }
        private void chkLocalService_CheckedChanged(object sender, EventArgs e) {
            var isLocal = false;
            txtDomain.Enabled = !isLocal && chkDominUser.Checked;
            txtUserName.Enabled = !isLocal;
            txtPassword.Enabled = !isLocal;
            txtPassword2.Enabled = !isLocal;
            if (isLocal) {
                txtServerIP.Text = "localhost";
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtPassword2.Text = string.Empty;
                txtDomain.Text = string.Empty;
            }
        }
        private void txtServerIP_TextChanged(object sender, EventArgs e) {
            resetFormatString();
        }

        private void numPort_ValueChanged(object sender, EventArgs e) {
            resetFormatString();
        }

        private void resetFormatString() {
            if (numPort.Value == 80m)
                txtFormatString.Text = string.Format("http://{0}/", txtServerIP.Text) + "{0}.svc";
            else
                txtFormatString.Text = string.Format("http://{0}:{1}/", txtServerIP.Text, numPort.Value) + "{0}.svc";
        }
    }
}
