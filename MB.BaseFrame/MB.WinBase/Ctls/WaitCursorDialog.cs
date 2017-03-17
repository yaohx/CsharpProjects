using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Ctls
{
    /// <summary>
    /// 程序处理的光标 
    /// </summary>
    public partial class WaitCursorDialog : Form
    {
        public WaitCursorDialog(): this(string.Empty)
        {
        }
        public WaitCursorDialog(string waitingMessage) {
            InitializeComponent();
            if (!string.IsNullOrEmpty(waitingMessage))
                this.label1.Text = waitingMessage;
        }
    }
}
