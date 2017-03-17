using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject
{
    public partial class frmTestDbPictureBox : Form
    {
        private myPicInfo _myPicInfo;
        private System.Windows.Forms.BindingSource _BindingSource;
        public frmTestDbPictureBox() {
            InitializeComponent();

            _myPicInfo = new myPicInfo();
            _BindingSource = new BindingSource();
            _BindingSource.DataSource = _myPicInfo;
            ucDBPictureBox1.DataBindings.Add(new Binding("ImageData", _BindingSource, "ImageData"));

            //DateTime dt = DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd hhLmm:ss"));
            

        }

        private void button1_Click(object sender, EventArgs e) {
            MessageBox.Show(_myPicInfo.ImageData.Length.ToString());
        }
        class myPicInfo
        {
            public byte[] ImageData { get; set; }
        }
    }


}
