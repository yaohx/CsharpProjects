using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MB.Util.Test
{
    [TestClass]
    public class ExtendMessageHelperTest
    {
        [TestMethod]
        public void SendSMSThruRESTService()
        {
            Model.SMSEntity sms = new Model.SMSEntity();
            sms.MessageEntity = "Test";
            sms.MobileNumbers.Add("13564683421");
            sms.MsgGroup = "测试短信";
            sms.SendUserName = "MB.BaseFrame.UnitTest";
            ExtendMessageHelper.Instance.SendMobileMessage(sms);
        }
    }
}
