using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
namespace WinTestProject {
    public partial class frmtesthastable : Form {
        public frmtesthastable() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            //MB.Util.MyHashtable<string, string> table = new MB.Util.MyHashtable<string, string>(new MB.Util.Comparer.StringEqualityComparer(true));

            //table.Add("aAA", "b");
            //table.Add("aAB", "b");
            //table.Add("aAC", "b");

            //if (table.ContainsKey("abc"))
            //    MessageBox.Show("OK");


            //MB.Util.Serializer.EntityXmlSerializer<ProductInfoXX> se = new MB.Util.Serializer.EntityXmlSerializer<ProductInfoXX>();
            //List<ProductInfoXX> lstData = new List<ProductInfoXX>();
            //lstData.Add(new ProductInfoXX());
            //lstData.Add(new ProductInfoXX());

            //string str = se.Serializer(lstData);


//            string xml = @"<EntitysRoot>
//  <Roow>
//    <Name>
//      
//    </Name>
//  </Row>
//</EntitysRoot>
//<EntitysRoot>
//  <Roow>
//    <Name>
//
//    </Name>
//  </Roow>
//</EntitysRoot>
//<EntitysRoot>
//  <Roow>
//    <Name>
//
//    </Name>
//  </Roow>
//</EntitysRoot>";

//            string[] ss = System.Text.RegularExpressions.Regex.Split(xml,@"</EntitysRoot>");

        }
    }

    //class ProductInfoXX {
    //    public string Name { get; set; }
    //    public string Code { get; set; }
    //}
}
