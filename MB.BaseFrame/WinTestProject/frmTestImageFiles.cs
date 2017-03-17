using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestImageFiles : Form {
        public frmTestImageFiles() {
            InitializeComponent();
            this.Load += new EventHandler(frmTestImageFiles_Load);
        }

        void frmTestImageFiles_Load(object sender, EventArgs e) {
            List<MyMageInfo> lstData = new List<MyMageInfo>();
            MyMageInfo newInfo = new MyMageInfo();
            newInfo.ID = 1;
            Image img = Image.FromFile(@"E:\MyImage\Ascent.jpg");
            newInfo.ImageData = MB.Util.MyConvert.Instance.ImageToByte(img);
            lstData.Add(newInfo);
            this.ucImageFileList1.ImageFieldName = "ImageData";
            this.ucImageFileList1.DataSource = lstData;
 
        }
    }

    public class MyMageInfo {
        public int ID { get; set; }
        public byte[] ImageData { get; set; }
    }
}
