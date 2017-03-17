using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.Orm.DB;
using MB.Orm.Persistence;

namespace WinTestProject
{
    public partial class frmTestAopInvoke : Form
    {
        public frmTestAopInvoke() {
            InitializeComponent();

  
        }

        private void button1_Click(object sender, EventArgs e) {
            myRule r = new myRule();
            r.testMethod();
        }
    }
    [MB.Aop.InjectionManager]
    public class myRule : System.ContextBoundObject
    {
        public void testMethod() {
        }
    }
}
