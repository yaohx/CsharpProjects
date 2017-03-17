using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using System.ComponentModel.Design.Serialization;

using MB.WinBase.Ctls;
namespace MB.WinBase.Binding
{

    /// <summary>
    /// 设计窗口窗口控件数据绑定。
    /// </summary>
    [ProvideProperty("AdvanceDataBinding", typeof(Control))]
    [ProvideProperty("PropertyName", typeof(Control))]
    public partial class MyDataBindingProvider : Component, IExtenderProvider, IDataBindingProvider
    {
        #region 变量定义...
        private static readonly string[] _CAN_BINDING_CTLS = new string[] {"TextBox","Label","ComboBox",
                                                                           "ucClickButtonInput" ,"CheckBox",
                                                                           "DateTimePicker","NumericUpDown",
                                                                           "ucIamgeIcoEdit","RichTextBoxEx","ucDbPictureBox","ucPopupRegionEdit"};
        public static readonly string[] INCLUDE_TAG_TEXT_CTLS = new string[] { "TextBox", "ComboBox",
                                                                     "ucClickButtonInput", "RichTextBoxEx","ucPopupRegionEdit" };

        private static readonly string COLUMN_CONFIG_NODE = "/Entity/Columns/Column";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Dictionary<string, DesignColumnXmlCfgInfo> ActiveColumnXmlCfgs;

        private Dictionary<Control, DesignColumnXmlCfgInfo> _DataBindings;
        private Dictionary<string, DesignColumnXmlCfgInfo> _ColumnXmlCfgs;
        private string _XmlConfigFile;
        private const int DEFAULT_EDIT_CTL_WIDTH = 178;
        #endregion 变量定义...

        #region construct function...
        /// <summary>
        /// construct function...
        /// </summary>
        public MyDataBindingProvider() {
            InitializeComponent();
            _DataBindings = new Dictionary<Control, DesignColumnXmlCfgInfo>();
        }
        /// <summary>
        /// construct function...
        /// </summary>
        /// <param name="container"></param>
        public MyDataBindingProvider(IContainer container) {
            container.Add(this);

            InitializeComponent();
            _DataBindings = new Dictionary<Control, DesignColumnXmlCfgInfo>();


        }
        #endregion construct function...

        /// <summary>
        /// 设置控件绑订的信息
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        [EditorAttribute(typeof(DataBindingProviderDesign), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("设置控件绑订的信息。"), Category("数据绑定")]
        public DesignColumnXmlCfgInfo GetAdvanceDataBinding(Control ctl) {
            ActiveColumnXmlCfgs = _ColumnXmlCfgs;


            if (_DataBindings.ContainsKey(ctl))
                return _DataBindings[ctl];
            else
                return null;
        }
        /// <summary>
        /// 设置控件绑订的信息
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="column"></param>
        [EditorAttribute(typeof(DataBindingProviderDesign), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("设置控件绑订的信息。"), Category("数据绑定")]
        public void SetAdvanceDataBinding(Control ctl, DesignColumnXmlCfgInfo column) {
            if (column == null) return;

            if (_DataBindings.ContainsKey(ctl))
                _DataBindings[ctl] = column;
            else
                _DataBindings.Add(ctl, column);
            string ctlName = ctl.GetType().Name;
            if (Array.IndexOf<string>(INCLUDE_TAG_TEXT_CTLS, ctlName) >= 0) {
                ctl.Text = "@" + column.ColumnDescription;
                ctl.Width = ctl.Width < DEFAULT_EDIT_CTL_WIDTH ? DEFAULT_EDIT_CTL_WIDTH : ctl.Width;
            }
            else if (ctl is Label) {
                ctl.Text = column.ColumnDescription + "：";
                ctl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                (ctl as Label).TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                //ctl.ForeColor = column.Nullable?System.Drawing.Color.Black : System.Drawing.Color.Red;

                //if (column.IsKey)
                //    ctl.Font = new System.Drawing.Font(ctl.Font, System.Drawing.FontStyle.Bold);

            }
            else if (ctl is CheckBox) {
                ctl.Text = column.ColumnDescription;
            }
            else if (ctl is DateTimePicker) {
                ctl.Width = DEFAULT_EDIT_CTL_WIDTH;
            }
            else {
            }

        }
        /// <summary>
        /// 获取绑定的属性名称。
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        [Description("获取绑定的属性名称")]
        public string GetPropertyName(Control ctl) {
            if (_DataBindings.ContainsKey(ctl))
                return _DataBindings[ctl].ColumnName;
            else
                return string.Empty;
        }

        #region IExtenderProvider 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="extendee"></param>
        /// <returns></returns>
        public bool CanExtend(object extendee) {
            return Array.IndexOf<string>(_CAN_BINDING_CTLS, extendee.GetType().Name) >= 0;
        }

        #endregion

        /// <summary>
        /// 列的配置XML描述信息。
        /// </summary>
        [EditorAttribute(typeof(XmlConfigSelectDesign), typeof(System.Drawing.Design.UITypeEditor))]
        public string XmlConfigFile {
            get {
                return _XmlConfigFile;
            }
            set {
                if (System.IO.File.Exists(value)) {
                    _XmlConfigFile = value;
                    //加载完整的XML 列配置信息
                    loadCfgColumn(value);

                    ActiveColumnXmlCfgs = _ColumnXmlCfgs;
                }
            }
        }

        #region IDataBindingProvider 成员
        /// <summary>
        /// 数据绑定的控件描述信息。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<Control, DesignColumnXmlCfgInfo> DataBindings {
            get { return _DataBindings; }
        }
        /// <summary>
        /// 数据列配置信息。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, DesignColumnXmlCfgInfo> ColumnXmlCfgs {
            get { return _ColumnXmlCfgs; }
        }

        #endregion

        #region 内部函数处理...
        //加载配置的列信息
        private void loadCfgColumn(string xmlConfigFile) {
            if (xmlConfigFile == null) return;
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlConfigFile);
            }
            catch (Exception ex) {
                MessageBox.Show(string.Format("加载XML 文件{0} 出错。", xmlConfigFile) + ex.Message, "操作提示");
                return;
            }
            if (xmlDoc == null) return;
            XmlNodeList nodeList = xmlDoc.SelectNodes(COLUMN_CONFIG_NODE);

            if (nodeList == null || nodeList.Count == 0) return;
            if (_ColumnXmlCfgs == null)
                _ColumnXmlCfgs = new Dictionary<string, DesignColumnXmlCfgInfo>();
            else
                _ColumnXmlCfgs.Clear();

            foreach (XmlNode node in nodeList) {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                string name = node.Attributes["Name"].Value;
                string desc = name;

                if (node.Attributes["Description"] != null)
                    desc = node.Attributes["Description"].Value;

                DesignColumnXmlCfgInfo cfgInfo = new DesignColumnXmlCfgInfo(name, desc);

                _ColumnXmlCfgs.Add(name, cfgInfo);
            }

        }
        #endregion 内部函数处理...

        #region 控件数据绑定设计...
        /// <summary>
        /// 数据绑定设计类。
        /// </summary>
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public class DataBindingProviderDesign : UITypeEditor
        {
            private IServiceProvider _Provider;
            public DataBindingProviderDesign() {
            }
            public override object EditValue(ITypeDescriptorContext context,
                IServiceProvider provider, object value) {

                _Provider = provider;
                //Create the listbox for display
                ListBox lstFields = new ListBox();
                lstFields.SelectedIndexChanged += new EventHandler(lstFields_SelectedIndexChanged);

                if (MyDataBindingProvider.ActiveColumnXmlCfgs != null) {
                    foreach (DesignColumnXmlCfgInfo s in MyDataBindingProvider.ActiveColumnXmlCfgs.Values) {
                        lstFields.Items.Add(s);
                    }
                }

                // Display the combolist
                ((IWindowsFormsEditorService)provider.GetService(
                    typeof(IWindowsFormsEditorService))).DropDownControl(lstFields);
                if (lstFields.SelectedItem != null) {

                    return lstFields.SelectedItem;
                }
                else {
                    return value;
                }

            }

            void lstFields_SelectedIndexChanged(object sender, EventArgs e) {
                ((IWindowsFormsEditorService)_Provider.GetService(
                   typeof(IWindowsFormsEditorService))).CloseDropDown();
            }

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
                if ((context != null) && (context.Instance != null))
                    return UITypeEditorEditStyle.DropDown;

                return base.GetEditStyle(context);
            }
        }
        #endregion 控件数据绑定设计...

        #region XML 配置文件选择...
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public class XmlConfigSelectDesign : UITypeEditor
        {
            private IServiceProvider _Provider;
            public XmlConfigSelectDesign() {
            }
            public override object EditValue(ITypeDescriptorContext context,
                IServiceProvider provider, object value) {

                _Provider = provider;

                OpenFileDialog fd = new OpenFileDialog();


                if (fd.ShowDialog() == DialogResult.OK) {
                    string fileName = fd.FileName;
                    try {
                        //XmlDocument xDoc = new XmlDocument();
                        //xDoc.Load(fileName);
                        return fileName;
                    }
                    catch (Exception ex) {
                        MessageBox.Show(string.Format("XML 文件 {0} 加载出错！" + ex.Message, fileName), "操作提示");
                        return value;
                    }
                }
                else {
                    return value;
                }

            }


            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
                if ((context != null) && (context.Instance != null))
                    return UITypeEditorEditStyle.Modal;

                return base.GetEditStyle(context);
            }
        }
        #endregion XML 配置文件选择...

    }
}
