using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MB.Util.Serializer;
//using Oracle.DataAccess.Client;
using System.Runtime.Serialization;
using MB.Util.XmlConfig;
using System.Collections;

namespace WinTestProject {
    public partial class testLightweightTextSerializer : Form {
        public testLightweightTextSerializer() {
            InitializeComponent();
        }

        private static void testDataSet() {
            DataTable dtTable = createTable();
            StringBuilder sb = new StringBuilder();

            decimal count = 1000;
            for (int i = 0; i < 100000; i++) {
                DataRow dr = dtTable.NewRow();
                dr["ID"] = i;
                dr["Name"] = "中国" + i;
                dr["Code"] = "AAA" + i;
                dr["CreateDate"] = DateTime.Now;
                dr["Count"] = count.ToString("#,###.00");
                dtTable.Rows.Add(dr);

                object[] vals = dr.ItemArray;
                foreach (object v in vals) {
                    sb.Append(v);
                    sb.Append("\t");
                }
                sb.Append("\n");
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream tstream = new MemoryStream()) {
                formatter.Serialize(tstream, dtTable);
                Console.WriteLine("T:" + tstream.ToArray().Length);
            }
            using (MemoryStream tstream = new MemoryStream()) {
                formatter.Serialize(tstream, sb.ToString());
                Console.WriteLine("S:" + tstream.ToArray().Length);
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN", false);

            LightweightTextSerializer ser = new LightweightTextSerializer(new ASCIIEncoding());
            byte[] bs = ser.Serializer(dtTable);
            Console.WriteLine("L:" + bs.Length);

            DataTable rLstM = ser.DeSerializer(bs);
        }

        private static void testDataReader() {
            //string cn = "Data Source=MBDHHDB_165;Persist Security Info=True;User ID=DHHUSER;Password=DHHUSER123;Unicode=True";
            //string sql = "select * from MBFS_FUC_DTL where ROWNUM <30000";
            //System.Data.OracleClient.OracleConnection ocn = new System.Data.OracleClient.OracleConnection(cn);
            //ocn.Open();
            //System.Data.OracleClient.OracleCommand ocmd = new System.Data.OracleClient.OracleCommand(sql, ocn);

            //DateTime begin = System.DateTime.Now;
            //OracleDataAdapter adapter = new OracleDataAdapter(ocmd);
            //DataSet ds = new DataSet();
            //adapter.Fill(ds);
            //DateTime end = System.DateTime.Now;
            //TimeSpan span = end.Subtract(begin);


            //BinaryFormatter formatter = new BinaryFormatter();
            //using (MemoryStream tstream = new MemoryStream()) {
            //    formatter.Serialize(tstream, ds);

            //    byte[] bytes_c = MB.Util.Compression.Instance.Zip(tstream.ToArray());
            //    Console.WriteLine(string.Format("总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2}", ds.Tables[0].Rows.Count, bytes_c.Length, span.TotalMilliseconds));
            //    //  Console.WriteLine(string.Format("转换后的Bytes数为{0}", bytes_c.Length));
            //}

            //var readder = ocmd.ExecuteReader();

            //LightweightTextSerializer ser = new LightweightTextSerializer(new ASCIIEncoding());

            //begin = System.DateTime.Now;
            //byte[] bytes = ser.Serializer(readder);
            //end = System.DateTime.Now;
            //span = end.Subtract(begin);
            //Console.WriteLine(string.Format("总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2} DATAREADER", 10000, bytes.Length, span.TotalMilliseconds));

            //begin = System.DateTime.Now;
            //DataTable rLstM = ser.DeSerializer(bytes);
            //end = System.DateTime.Now;
            //span = end.Subtract(begin);

            //Console.WriteLine(string.Format("多线程 转换Bytes数为长度{0},形成{1}个对象,花费时间为{2}", bytes.Length, rLstM.Rows.Count, span.TotalMilliseconds));

            //StringBuilder sb = new StringBuilder();
            //DataRow[] drs = ds.Tables[0].Select();
            //DataColumnCollection cols = ds.Tables[0].Columns; 
            //foreach (DataRow dr in drs) {
            //    foreach (DataColumn dc in cols) {
            //        sb.Append(dr[dc.ColumnName]);
            //        sb.Append("\t");
            //    }
            //    sb.Append("\n");
            //}

            //using (MemoryStream tstream = new MemoryStream()) {
            //    formatter.Serialize(tstream, sb.ToString());
            //    byte[] bytes_c = MB.Util.Compression.Instance.Zip(tstream.ToArray());
            //    Console.WriteLine(string.Format("总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2} DATAREADER", 10000, bytes_c.Length, span.TotalMilliseconds));
            //}
        }


        private static void testTable() {
            try {

                LightweightTextSerializer ser = new LightweightTextSerializer();

                DataTable dtTable = createTable();

                decimal count = 1000;
                for (int i = 0; i < 10000; i++) {
                    DataRow dr = dtTable.NewRow();
                    dr["ID"] = i;
                    dr["Name"] = "中国" + i;
                    dr["Code"] = "AAA" + i;
                    dr["CreateDate"] = DateTime.Now.ToString("yyyy*MM*dd HH:mm");
                    dr["Count"] = count.ToString("$#,###.00");
                    dtTable.Rows.Add(dr);
                }



                DateTime begin = System.DateTime.Now;
                byte[] bytes = ser.Serializer(dtTable);
                DateTime end = System.DateTime.Now;
                TimeSpan span = end.Subtract(begin);
                Console.WriteLine(string.Format("总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2}", dtTable.Rows.Count, bytes.Length, span.TotalMilliseconds));

                //BinaryFormatter formatter = new BinaryFormatter();
                //using (MemoryStream tstream = new MemoryStream()) {
                //    formatter.Serialize(tstream, dtTable);
                //    byte[] bytes_c = MB.Util.Compression.Instance.Zip(tstream.ToArray());
                //    Console.WriteLine(string.Format("总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2}", dtTable.Rows.Count, bytes_c.Length, span.TotalMilliseconds));
                //}


                begin = System.DateTime.Now;
                DataTable rLstM = ser.DeSerializer(bytes);
                end = System.DateTime.Now;
                span = end.Subtract(begin);

                Console.WriteLine(string.Format("多线程 转换Bytes数为长度{0},形成{1}个对象,花费时间为{2}", bytes.Length, rLstM.Rows.Count, span.TotalMilliseconds));

                Console.WriteLine("OK!");
            }
            catch (Exception ex) {
                Console.WriteLine("出错!" + ex.Message);
            }
        }
        private static void testEntityList() {
            try {

                LightweightTextSerializer ser = new LightweightTextSerializer();

                ArrayList lstData = new ArrayList();

                for (int i = 0; i < 10000; i++) {
                    MyProduct p = new MyProduct();
                    p.ID = i;
                    p.Name = "中国" + i;
                    p.Code = "AAA" + i;
                    p.CreateDate = System.DateTime.Now;
                    p.Count = 100;
                    lstData.Add(p);

                }

                //DateTime begin = System.DateTime.Now;
                //byte[] soapSer = MB.Util.Serializer.SoapSerializer.DefaultInstance.SerializerToByte(lstData);
                //DateTime end = System.DateTime.Now;
                //TimeSpan span = end.Subtract(begin);
                //Console.WriteLine(string.Format("SOAP 总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2}", lstData.Count, soapSer.Length, span.TotalMilliseconds));


                //  begin = System.DateTime.Now;
                //MB.Util.Serializer.EntityXmlSerializer<MyProduct> xmS = new MB.Util.Serializer.EntityXmlSerializer<MyProduct>();
                //string ss = xmS.Serializer(products);
                //byte[] xmlBytes = (new ASCIIEncoding()).GetBytes(ss); 
                //  end = System.DateTime.Now;
                //  span = end.Subtract(begin);
                //  Console.WriteLine(string.Format("XML 总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2}", lstData.Count, xmlBytes.Length, span.TotalMilliseconds));


                DateTime begin = System.DateTime.Now;
                byte[] bytes = ser.Serializer(typeof(MyProduct), lstData);
                DateTime end = System.DateTime.Now;
                TimeSpan span = end.Subtract(begin);
                Console.WriteLine(string.Format("总共转换{0}个对象,转换后的Bytes数为{1},花费时间为{2}", lstData.Count, bytes.Length, span.TotalMilliseconds));
                //  Console.ReadLine();
                //begin = System.DateTime.Now;
                //IList rLst = ser.DeSerializer(typeof(MyProduct), bytes);
                //end = System.DateTime.Now;
                //span = end.Subtract(begin);

                //Console.WriteLine(string.Format("转换Bytes数为长度{0},形成{1}个对象,花费时间为{2}", bytes.Length, rLst.Count, span.TotalMilliseconds));

                begin = System.DateTime.Now;
                IList rLstM = ser.DeSerializer(typeof(MyProductEX), bytes);
                end = System.DateTime.Now;
                span = end.Subtract(begin);

                Console.WriteLine(string.Format("多线程 转换Bytes数为长度{0},形成{1}个对象,花费时间为{2}", bytes.Length, rLstM.Count, span.TotalMilliseconds));

                Console.WriteLine("OK!");
            }
            catch (Exception ex) {
                Console.WriteLine("出错!" + ex.Message);
            }
        }

        static DataTable createTable() {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("CreateDate", typeof(DateTime));
            dt.Columns.Add("Count", typeof(decimal));

            return dt;
        }

        private void button1_Click(object sender, EventArgs e) {
            testDataSet();
        }
    }


    [Serializable]
    [DataContract]
    [MB.Util.XmlConfig.ModelXmlConfig()]
    public class MyProduct {
        [DataMember]
        [PropertyXmlConfig]
        public int ID { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public string Code { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public string Name { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public DateTime CreateDate { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public decimal Count { get; set; }
    }
    [Serializable]
    [DataContract]
    [MB.Util.XmlConfig.ModelXmlConfig()]
    public class MyProductEX {
        [DataMember]
        [PropertyXmlConfig]
        public int ID { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public string Code { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public string Name { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public DateTime CreateDate { get; set; }
        [DataMember]
        [PropertyXmlConfig]
        public decimal Count { get; set; }
    }
}
