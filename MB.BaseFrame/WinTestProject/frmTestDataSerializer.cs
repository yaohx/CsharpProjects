using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace WinTestProject {
    public partial class frmTestDataSerializer : Form {
        private string _SerXmlString;

        public frmTestDataSerializer() {
            InitializeComponent();
        }

        private void butSerializer_Click(object sender, EventArgs e) {
            FlowData data = new FlowData();
            List<BaseData> childs = new List<BaseData>();
            data.Name = "我的测试流程";
            data.Childs = childs;
            data.Location = new Point(12, 45);

            childs.Add(new ProductSeInfo("qwrqr1", "erwerw1", "234234", "eryery", 5, 6));
            childs.Add(new ProductSeInfo("qwrqr2", "erwerw2", "234234", "eryery", 5, 6));
            childs.Add(new ProductSeInfo("qwrqr3", "erwerw3", "234234", "eryery", 5, 6));
            childs.Add(new ProductSeInfo("qwrqr4", "erwerw4", "234234", "eryery", 5, 6));

            var v = new MB.Util.Serializer.EntityXmlSerializer<FlowData>();
            _SerXmlString = v.SingleSerializer(data,"FlowData") ;   
        }

        private void butDeSerializer_Click(object sender, EventArgs e) {
            var v = new MB.Util.Serializer.EntityXmlSerializer<FlowData>();
            var data = v.SingleDeSerializer(_SerXmlString, "FlowData");   

            
        }

        private void button1_Click(object sender, EventArgs e) {
            string xml = "<FilterRoot AdvanceFilter='False'><Parameter><PropertyName>1</PropertyName><Value><![CDATA[0]]></Value><Condition>Special</Condition><OrderIndex>0</OrderIndex><Limited>False</Limited><MultiValue>False</MultiValue></Parameter></FilterRoot>";
            var pars = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.DeSerializer(xml);
            MessageBox.Show("OK");
        }


    }
    public class FlowData {
        private string _Name;
        private List<BaseData> _Childs;
        private Point _Location;
        public FlowData() {
            _Childs = new List<BaseData>();
        }
        [MB.Util.Serializer.ValueXmlSerializer(GeneralStruct = true)]   
        [DataMember] 
        public System.Drawing.Point Location {
            get {
                return _Location;
            }
            set {
                _Location = value;
            }
        }
        [DataMember]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }

        }
        [MB.Util.Serializer.ValueXmlSerializer(CreateByInstanceType = true)]
        [DataMember]
        public List<BaseData> Childs {
            get {
                return _Childs;
            }
            set {
                _Childs = value;
            }
        }

    }
    public abstract class BaseData {
        private int _ID;
        [DataMember]
        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }
    }

    public class ProductSeInfo : BaseData {
        public ProductSeInfo() {
        }
        public ProductSeInfo(string name, string code, string size, string sizeName, int count, int amount) {
            _Name = name;
            _Code = code;
            _Size = size;
            _SizeName = sizeName;
            _Count = count;
            _Amount = amount;
        }
        private int _ID;
        [DataMember]
        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }
        private string _Name;
        [DataMember] 
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Code;
        [DataMember]
        public string Code {
            get { return _Code; }
            set { _Code = value; }
        }
        private string _Size;
        [DataMember]
        public string Size {
            get { return _Size; }
            set { _Size = value; }
        }
        private string _SizeName;
        [DataMember]
        public string SizeName {
            get { return _SizeName; }
            set { _SizeName = value; }
        }
        private int _Count;
        [DataMember]
        public int Count {
            get { return _Count; }
            set { _Count = value; }
        }
        private int _Amount;
        [DataMember]
        public int Amount {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private DateTime _CreateDate;
        [DataMember]
        public DateTime CreateDate {
            get {
                return _CreateDate;
            }
            set {
                _CreateDate = value;
            }
        }
    }
}
