using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestReflectionEmit : Form {
        public frmTestReflectionEmit() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            EmitProductInfo info = new EmitProductInfo();

            MB.Util.Emit.DynamicPropertyAccessor begin = new MB.Util.Emit.DynamicPropertyAccessor(typeof(EmitProductInfo), "BeginDate");
            begin.Set(info,null);

            MB.Util.Emit.DynamicPropertyAccessor datas = new MB.Util.Emit.DynamicPropertyAccessor(typeof(EmitProductInfo), "Datas");

            byte[] bts = new byte[] { 243, 34, 234 };
            datas.Set(info, bts);
            MB.Util.Emit.DynamicPropertyAccessor price = new MB.Util.Emit.DynamicPropertyAccessor(typeof(EmitProductInfo), "Price");

            price.Set(info, 234);

            MB.Util.Emit.DynamicPropertyAccessor isStart = new MB.Util.Emit.DynamicPropertyAccessor(typeof(EmitProductInfo), "IsStart");

            isStart.Set(info,null);

            string name = bts.GetType().Name;
            MessageBox.Show("OK" + info.IsStart.ToString() ); 
        }
    }

    public class EmitProductInfo {
        private DateTime _BeginDate;

        public DateTime BeginDate {
            get { return _BeginDate; }
            set { _BeginDate = value; }
        }
        private Decimal _Amount;

        public Decimal Amount {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _Code;

        public string Code {
            get { return _Code; }
            set { _Code = value; }
        }
        private Int32 _Count;

        public Int32 Count {
            get { return _Count; }
            set { _Count = value; }
        }
        private byte[] _Datas;

        public byte[] Datas {
            get { return _Datas; }
            set { _Datas = value; }
        }
        private bool _IsStart;

        public bool IsStart {
            get { return _IsStart; }
            set { _IsStart = value; }
        }

        private Decimal? _Price;

        public Decimal? Price {
            get { return _Price; }
            set { _Price = value; }
        }
    }
}
