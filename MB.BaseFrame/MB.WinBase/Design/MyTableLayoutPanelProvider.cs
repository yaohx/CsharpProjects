using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Design {
    [System.Diagnostics.DebuggerStepThrough()]
    [ProvideProperty("AutoLayoutPanel", typeof(TableLayoutPanel))]
    [ProvideProperty("IsCaption", typeof(TableLayoutPanel))]
    public partial class MyTableLayoutPanelProvider : Component, IExtenderProvider {
        private Dictionary<TableLayoutPanel, PanelInfo> _MyLayoutTable;
        private const int DEAFULT_ROW_HEIGHT = 26;
        private const int CAPTION_PANEL_WIDTH = 141;
        private const int EDIT_PANEL_WIDTH = 200;


        public MyTableLayoutPanelProvider() {
            InitializeComponent();

            _MyLayoutTable = new Dictionary<TableLayoutPanel, PanelInfo>();
        }

        public MyTableLayoutPanelProvider(IContainer container) {
            container.Add(this);

            InitializeComponent();
            _MyLayoutTable = new Dictionary<TableLayoutPanel, PanelInfo>();
        }

        #region IExtenderProvider 成员

        public bool CanExtend(object extendee) {
            return extendee is TableLayoutPanel;  
        }

        #endregion
        /// <summary>
        ///  获取或者设置TableLayoutPanel 的布局
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        [Description("获取或者设置TableLayoutPanel 的布局"), Category("布局")]
        public bool GetAutoLayoutPanel(TableLayoutPanel panel) {
            if (!_MyLayoutTable.ContainsKey(panel)) return false;
            return _MyLayoutTable[panel].AutoLayout;
        }
        /// <summary>
        ///  获取或者设置TableLayoutPanel 的布局
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="autoLayout"></param>
        [Description("获取或者设置TableLayoutPanel 的布局"), Category("布局")]
        public void SetAutoLayoutPanel(TableLayoutPanel panel, bool autoLayout) {
            if (_MyLayoutTable.ContainsKey(panel)) {
                if (!autoLayout) {
                    panel.Resize -= new EventHandler(panel_Resize);
                    _MyLayoutTable.Remove(panel);

                }
                else {
                    panel_Resize(panel, null);
                }
            }
            else {
                if (autoLayout) {
                    _MyLayoutTable.Add(panel,new PanelInfo(autoLayout,true));
                    panel.Resize += new EventHandler(panel_Resize);
                    panel_Resize(panel, null);
                }
            }
        }
        [Description("获取或者设置TableLayoutPanel是否Caption"), Category("布局")]
        [Browsable(false)]
        public bool GetIsCaption(TableLayoutPanel panel) {
            //if (!_MyLayoutTable.ContainsKey(panel)) return false;

            //return _MyLayoutTable[panel].IsCaption;
            return false;
        }
        [Description("获取或者设置TableLayoutPanel 是否Caption"), Category("布局")]
        [Browsable(false)]
        public void SetIsCaption(TableLayoutPanel panel, bool isCaption) {
            //if (_MyLayoutTable.ContainsKey(panel)) {
            //    _MyLayoutTable[panel].IsCaption = isCaption;
            //    panel.BackColor = isCaption ? System.Drawing.Color.FromArgb(212, 228, 248) : System.Drawing.Color.Transparent;
            //    panel.Width = isCaption ? CAPTION_PANEL_WIDTH : EDIT_PANEL_WIDTH;
            //    panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            //}
        }

        void panel_Resize(object sender, EventArgs e) {
            TableLayoutPanel tlPanel = sender as TableLayoutPanel;

            //if (tlPanel.ColumnCount != 1) {
            //    tlPanel.ColumnStyles.Clear();
            //    tlPanel.ColumnCount = 1;
            //    tlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            //}
            if (tlPanel.ColumnStyles.Count > 0) {
                for (int i = 0; i < tlPanel.ColumnStyles.Count; i++) {
                    tlPanel.ColumnStyles[i].SizeType = SizeType.Absolute;
                   tlPanel.ColumnStyles[i].Width = (i % 2) == 0 ? CAPTION_PANEL_WIDTH : EDIT_PANEL_WIDTH;
                }
            }
            

            int height = tlPanel.Height;
            int rowCount = height / DEAFULT_ROW_HEIGHT;
            tlPanel.RowStyles.Clear();
            tlPanel.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++) {
                tlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            }
        }

        class PanelInfo {
            public PanelInfo(bool autoLayout,bool isCaption) {
                AutoLayout = autoLayout;
                IsCaption = isCaption;
            }
            public bool AutoLayout { get; set; }
            public bool IsCaption { get; set; }
        }
    }
}
