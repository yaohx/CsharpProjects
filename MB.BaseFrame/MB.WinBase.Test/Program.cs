using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Json;
using System.IO;
using System.Runtime.Serialization;

namespace MB.WinBase.Test
{
    class Program
    {
        static void Main()
        {
            MyTest myTest = new MyTest();

            myTest.ID = 1;
            myTest.dtDate = DateTime.Now;
            myTest.Remark = "Json数据解析，回车换行符支持测试\r\n";

            string path = @"C:\\Users\\Werish\\Desktop\\Project Key.txt";

            myTest.bPhoto = File.ReadAllBytes(path);

            string jsonData = Converter.Serialize(myTest);
        }
    }


    [Serializable]
    public class MyTest
    {
        [DataMember]
        public int ID{get;set;}

        [DataMember]
        public DateTime dtDate { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public byte[] bPhoto { get; set; }
    }
}
