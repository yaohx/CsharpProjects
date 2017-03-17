using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault.Ctls {
    public partial class frmImageView : MB.WinClientDefault.AbstractBaseForm   {
        private Image _ImageData;
        public frmImageView(Image imageData) {
            InitializeComponent();

            picMain.Image = imageData;  
        }
        protected override void OnClosing(CancelEventArgs e) {
            if (_ImageData != null)
                _ImageData.Dispose();
            base.OnClosing(e);
        }

    }
}
