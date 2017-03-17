using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConsoleApplication1
{
    public class HttpTest
    {
        public static void Main()
        {
            HttpTest.GetHttpRequest();
        }
        static void GetHttpRequest()
        {
            WebClient client = new WebClient();
            string str = "My test";
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(str);

            var t = client.UploadData("http://localhost:5821/Default.aspx", "POST", bytes);
        }


    }
}
