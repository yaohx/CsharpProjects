using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Model;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace MB.Util
{
    /// <summary>
    ///美邦移动短信发送。
    /// </summary>
   public class ExtendMessageHelper
    {
       private static readonly string REST_MOBILE_MESSAGE_CFG = "SMSServiceCfg";
       private static readonly string MOBILE_MESSAGE_CFG = "MobileMessageCfg";
       private static readonly string EMAIL_SETTING_CFG = "EmailSettingCfg";

   
        #region Instance...
        private static object _Object = new object();
        private static ExtendMessageHelper _Instance;

        protected ExtendMessageHelper() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static ExtendMessageHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new ExtendMessageHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 发送短信信息。
        /// </summary>
        /// <param name="msgInfo"></param>
        /// <returns></returns>
        public bool SendMobileMessage(SMSEntity msgInfo)
        {
            try
            {
                string url = MB.Util.AppConfigSetting.GetKeyValue(REST_MOBILE_MESSAGE_CFG);
                if (string.IsNullOrEmpty(url))
                    throw new APPException("SMSServiceCfg配置节点不能为空", APPMessageType.SysErrInfo);

                RESTServiceHelper restService = new RESTServiceHelper();
                return restService.PostDataAsJson<SMSEntity, bool>(url, msgInfo);
            }
            catch (Exception ex)
            {
                throw new MB.Util.APPException("手机短信REST发送失败:" + ex.ToString(), APPMessageType.SysErrInfo);
            }
        }

       /// <summary>
       /// 发送短信信息。
       /// </summary>
       /// <param name="msgInfo"></param>
       /// <returns></returns>
       public bool SendMobileMessage(MobileMessageInfo msgInfo) {
           try {
               string cfg = System.Configuration.ConfigurationManager.AppSettings[MOBILE_MESSAGE_CFG].ToString();
               string[] cfgs = cfg.Split(',');
               string url = cfgs[0];
               string method = cfgs[1];

               string[] nus = msgInfo.Mobile.Split(',');
               var newMsg = (MobileMessageInfo)msgInfo.Clone();
               foreach (var t in nus) {
                   newMsg.Mobile = t;
                   MB.Util.MyInvokeWebServiceHelper.InvokeWebService(url, method, new object[] { newMsg.ToString() });
               }
               return true;
           }
           catch (Exception ex) {
               throw new MB.Util.APPException("手机短信发送" + ex.Message, APPMessageType.SysErrInfo);
           }
       }
       /// <summary>
       /// 短信接收
       /// </summary>
       public void ReceiveMobileMessage() {
           throw new MB.Util.APPException("接收短信端口没有开放", APPMessageType.SysErrInfo);
       }
       /// <summary>
       /// 邮件发送
       /// </summary>
       /// <param name="touser">指定的用户</param>
       /// <param name="subject">标题</param>
       /// <param name="body">内容</param>
       /// <param name="isBodyHtml">是否为html 格式</param>
       /// <param name="attaachFiles">附件(完整的路径名称)</param>
       public void SendEmail(string touser, string subject, string body, bool isBodyHtml,string[] attaachFiles) {
           try {
               string fromUser = string.Empty;
               var sender = getSmtpClient(out fromUser);

               System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
               string[] tos = touser.Split(',');
               foreach (var user in tos)
                   mailMsg.To.Add(user);

               mailMsg.From = new System.Net.Mail.MailAddress(fromUser + "@metersbonwe.com");
               mailMsg.Subject = subject;
               mailMsg.SubjectEncoding = Encoding.UTF8;
               mailMsg.Body = body;
               mailMsg.BodyEncoding = Encoding.UTF8;
               mailMsg.IsBodyHtml = false;
               mailMsg.Priority = MailPriority.High;

               //获取所有邮件附件
               if (attaachFiles != null && attaachFiles.Length > 0) {
                   string[] file = attaachFiles;
                   for (int n = 0; n < file.Length; n++) {
                       if (file[n] != "") {
                           //附件对象
                           Attachment data = new Attachment(file[n], MediaTypeNames.Application.Octet);
                           //附件资料
                           ContentDisposition disposition = data.ContentDisposition;
                           disposition.CreationDate = System.IO.File.GetCreationTime(file[n]);
                           disposition.ModificationDate = System.IO.File.GetLastWriteTime(file[n]);
                           disposition.ReadDate = System.IO.File.GetLastAccessTime(file[n]);
                           //加入邮件附件
                           mailMsg.Attachments.Add(data);
                       }
                   }
               }

               sender.Send(mailMsg);
           }
           catch (Exception ex) {
               throw new MB.Util.APPException("邮件发送有误" + ex.Message, APPMessageType.SysErrInfo);
           }
       }
       /// <summary>
       /// 邮件发送
       /// </summary>
       /// <param name="mailMsg"></param>
       /// <returns></returns>
       public void SendEmail(System.Net.Mail.MailMessage mailMsg) {
          
           try {
               string user = string.Empty;
               var sender = getSmtpClient(out user);
               if (mailMsg.From == null )
                   mailMsg.From = new System.Net.Mail.MailAddress(user + "@metersbonwe.com");

               sender.Send(mailMsg);
           }
           catch (Exception ex) {
               throw new MB.Util.APPException("邮件发送有误" + ex.Message, APPMessageType.SysErrInfo);
           }
       }
       /// <summary>
       /// 邮件接收
       /// </summary>
       /// <returns></returns>
       public bool ReceiveEmail() {
           throw new MB.Util.APPException("接收功能没有开放", APPMessageType.SysErrInfo);
       }

       //获取默认设置的邮件服务器
       private System.Net.Mail.SmtpClient getSmtpClient(out string user) {
           string cfg = System.Configuration.ConfigurationManager.AppSettings[EMAIL_SETTING_CFG].ToString();
           string[] cfgs = cfg.Split(',');
           string[] servers = cfgs[0].Split(':');
           string[] crds = cfgs[1].Split('/');

           System.Net.Mail.SmtpClient sender = new System.Net.Mail.SmtpClient();
           sender.Host = servers[0];
           sender.Port = int.Parse(servers[1]);
           sender.Credentials = new NetworkCredential(crds[0], crds[1]);
           user = crds[0];
           return sender;
       }
    }
}
