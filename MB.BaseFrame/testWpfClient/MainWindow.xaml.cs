using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            string str = testWpfClient.Properties.Resources.String1;// MB.WpfBase.AppEnvironmentSetting.GetName();
            MessageBox.Show(str);

            //var db = MB.Orm.DbSql.SqlGeneratorManager.GetSqlGenerator(MB.Orm.Enums.ModelConfigOptions.AutoCreateSql);
            //var count = MB.BusiBase.Common.SmartDAL.Query<MyData>().Where(o => o.CODE, "1").Select(o => o.COUNT);
            //MB.BusiBase.Common.SmartDAL.Update<MyData>().Set(o => o.COUNT, 12).Where(o => o.CODE, "aaa").Execute();

            //new MB.RuleBase.Common.ObjectEditHelper().GetObjects<

         
        }
    }

    public class MyData
    {
        public string CODE { get; set; }
        public int COUNT { get; set; }
    }
}
