using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.ucDbPictureBox1.ImageFileName = "测试图片";
        }
    }
}
