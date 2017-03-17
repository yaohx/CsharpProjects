namespace MB.WinClientDefault {
    partial class DefaultAsynQueryViewForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultAsynQueryViewForm));
            this.tabCtlMain = new System.Windows.Forms.TabControl();
            this.tPageGeneral = new System.Windows.Forms.TabPage();
            this.grdCtlMain = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tPageMultiView = new System.Windows.Forms.TabPage();
            this.pivGrdCtlMain = new MB.XWinLib.PivotGrid.PivotGridEx();
            this.tPageQAChart = new System.Windows.Forms.TabPage();
            this.tPageModuleComment = new System.Windows.Forms.TabPage();
            this.tPageDynamicGroup = new System.Windows.Forms.TabPage();
            this.ucDynamicGroupResultInQuery = new MB.WinClientDefault.DynamicGroup.ucDynamicGroupResult();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labTitleMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.labQueryContent = new System.Windows.Forms.ToolStripStatusLabel();
            this.labTotalPageNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.labCurrentPageNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.lnkPreviousPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lnkNextPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tabCtlMain.SuspendLayout();
            this.tPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            this.tPageMultiView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivGrdCtlMain)).BeginInit();
            this.tPageDynamicGroup.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCtlMain
            // 
            this.tabCtlMain.Controls.Add(this.tPageGeneral);
            this.tabCtlMain.Controls.Add(this.tPageMultiView);
            this.tabCtlMain.Controls.Add(this.tPageQAChart);
            this.tabCtlMain.Controls.Add(this.tPageModuleComment);
            this.tabCtlMain.Controls.Add(this.tPageDynamicGroup);
            this.tabCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlMain.ImageList = this.imageList1;
            this.tabCtlMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtlMain.Name = "tabCtlMain";
            this.tabCtlMain.SelectedIndex = 0;
            this.tabCtlMain.Size = new System.Drawing.Size(843, 420);
            this.tabCtlMain.TabIndex = 7;
            this.tabCtlMain.SelectedIndexChanged += new System.EventHandler(this.tabCtlMain_SelectedIndexChanged);
            // 
            // tPageGeneral
            // 
            this.tPageGeneral.Controls.Add(this.grdCtlMain);
            this.tPageGeneral.ImageIndex = 2;
            this.tPageGeneral.Location = new System.Drawing.Point(4, 23);
            this.tPageGeneral.Name = "tPageGeneral";
            this.tPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tPageGeneral.Size = new System.Drawing.Size(835, 393);
            this.tPageGeneral.TabIndex = 0;
            this.tPageGeneral.Text = "普通视图";
            this.tPageGeneral.UseVisualStyleBackColor = true;
            // 
            // grdCtlMain
            // 
            this.grdCtlMain.DataIOControl = null;
            this.grdCtlMain.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.grdCtlMain.Location = new System.Drawing.Point(21, 26);
            this.grdCtlMain.MainView = this.gridViewMain;
            this.grdCtlMain.Name = "grdCtlMain";
            this.grdCtlMain.ShowOptionMenu = false;
            this.grdCtlMain.Size = new System.Drawing.Size(596, 168);
            this.grdCtlMain.TabIndex = 0;
            this.grdCtlMain.ValidedDeleteKeyDown = false;
            this.grdCtlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            this.grdCtlMain.DoubleClick += new System.EventHandler(this.grdCtlMain_DoubleClick);
            this.grdCtlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grdCtlMain_MouseMove);
            // 
            // gridViewMain
            // 
            this.gridViewMain.GridControl = this.grdCtlMain;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsView.ShowFooter = true;
            this.gridViewMain.CustomDrawFooterCell += new DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventHandler(this.gridViewMain_CustomDrawFooterCell);
            // 
            // tPageMultiView
            // 
            this.tPageMultiView.Controls.Add(this.pivGrdCtlMain);
            this.tPageMultiView.ImageIndex = 1;
            this.tPageMultiView.Location = new System.Drawing.Point(4, 23);
            this.tPageMultiView.Name = "tPageMultiView";
            this.tPageMultiView.Size = new System.Drawing.Size(835, 393);
            this.tPageMultiView.TabIndex = 1;
            this.tPageMultiView.Text = "多维视图";
            this.tPageMultiView.UseVisualStyleBackColor = true;
            // 
            // pivGrdCtlMain
            // 
            this.pivGrdCtlMain.CreateOtherMenuByUser = false;
            this.pivGrdCtlMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.pivGrdCtlMain.Location = new System.Drawing.Point(9, 20);
            this.pivGrdCtlMain.Name = "pivGrdCtlMain";
            this.pivGrdCtlMain.Size = new System.Drawing.Size(554, 349);
            this.pivGrdCtlMain.TabIndex = 0;
            // 
            // tPageQAChart
            // 
            this.tPageQAChart.ImageIndex = 3;
            this.tPageQAChart.Location = new System.Drawing.Point(4, 23);
            this.tPageQAChart.Name = "tPageQAChart";
            this.tPageQAChart.Size = new System.Drawing.Size(835, 393);
            this.tPageQAChart.TabIndex = 2;
            this.tPageQAChart.Text = "图表分析";
            this.tPageQAChart.UseVisualStyleBackColor = true;
            // 
            // tPageModuleComment
            // 
            this.tPageModuleComment.ImageIndex = 4;
            this.tPageModuleComment.Location = new System.Drawing.Point(4, 23);
            this.tPageModuleComment.Name = "tPageModuleComment";
            this.tPageModuleComment.Size = new System.Drawing.Size(835, 393);
            this.tPageModuleComment.TabIndex = 3;
            this.tPageModuleComment.Text = "发表评论";
            this.tPageModuleComment.UseVisualStyleBackColor = true;
            // 
            // tPageDynamicGroup
            // 
            this.tPageDynamicGroup.Controls.Add(this.ucDynamicGroupResultInQuery);
            this.tPageDynamicGroup.ImageIndex = 5;
            this.tPageDynamicGroup.Location = new System.Drawing.Point(4, 23);
            this.tPageDynamicGroup.Name = "tPageDynamicGroup";
            this.tPageDynamicGroup.Size = new System.Drawing.Size(835, 393);
            this.tPageDynamicGroup.TabIndex = 4;
            this.tPageDynamicGroup.Text = "动态聚组";
            this.tPageDynamicGroup.UseVisualStyleBackColor = true;
            // 
            // ucDynamicGroupResultInQuery
            // 
            this.ucDynamicGroupResultInQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDynamicGroupResultInQuery.Location = new System.Drawing.Point(0, 0);
            this.ucDynamicGroupResultInQuery.Name = "ucDynamicGroupResultInQuery";
            this.ucDynamicGroupResultInQuery.Size = new System.Drawing.Size(835, 393);
            this.ucDynamicGroupResultInQuery.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "SelectNodeTypeIco.ICO");
            this.imageList1.Images.SetKeyName(1, "QYZHZX15.ICO");
            this.imageList1.Images.SetKeyName(2, "CWCB12.ico");
            this.imageList1.Images.SetKeyName(3, "Chart.ICO");
            this.imageList1.Images.SetKeyName(4, "3KHGX3.ico");
            this.imageList1.Images.SetKeyName(5, "Aggregation.ico");
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labTitleMsg,
            this.labQueryContent,
            this.labTotalPageNumber,
            this.labCurrentPageNumber,
            this.lnkPreviousPage,
            this.lnkNextPage,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(837, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labTitleMsg
            // 
            this.labTitleMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitleMsg.Name = "labTitleMsg";
            this.labTitleMsg.Size = new System.Drawing.Size(94, 20);
            this.labTitleMsg.Text = "查询耗时 0 毫秒";
            this.labTitleMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labQueryContent
            // 
            this.labQueryContent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labQueryContent.ForeColor = System.Drawing.Color.Navy;
            this.labQueryContent.Name = "labQueryContent";
            this.labQueryContent.Size = new System.Drawing.Size(463, 20);
            this.labQueryContent.Spring = true;
            this.labQueryContent.Text = "labQueryContent";
            this.labQueryContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalPageNumber
            // 
            this.labTotalPageNumber.Name = "labTotalPageNumber";
            this.labTotalPageNumber.Size = new System.Drawing.Size(51, 20);
            this.labTotalPageNumber.Text = "总共0页";
            // 
            // labCurrentPageNumber
            // 
            this.labCurrentPageNumber.Name = "labCurrentPageNumber";
            this.labCurrentPageNumber.Size = new System.Drawing.Size(63, 20);
            this.labCurrentPageNumber.Text = "当前第0页";
            // 
            // lnkPreviousPage
            // 
            this.lnkPreviousPage.AutoSize = false;
            this.lnkPreviousPage.IsLink = true;
            this.lnkPreviousPage.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkPreviousPage.Name = "lnkPreviousPage";
            this.lnkPreviousPage.Size = new System.Drawing.Size(60, 20);
            this.lnkPreviousPage.Text = "上一页";
            // 
            // lnkNextPage
            // 
            this.lnkNextPage.AutoSize = false;
            this.lnkNextPage.IsLink = true;
            this.lnkNextPage.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkNextPage.Name = "lnkNextPage";
            this.lnkNextPage.Size = new System.Drawing.Size(60, 20);
            this.lnkNextPage.Text = "下一页";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.DockingOptions.HideImmediatelyOnAutoHide = true;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dockPanel1.Appearance.Options.UseBackColor = true;
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel1.DockVertical = DevExpress.Utils.DefaultBoolean.True;
            this.dockPanel1.FloatSize = new System.Drawing.Size(200, 121);
            this.dockPanel1.ID = new System.Guid("01d44114-5db1-46f0-b3ea-c660e516585a");
            this.dockPanel1.Location = new System.Drawing.Point(0, 420);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 53);
            this.dockPanel1.Size = new System.Drawing.Size(843, 53);
            this.dockPanel1.TabText = "Results";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.statusStrip1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(837, 25);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(843, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 473);
            this.barDockControlBottom.Size = new System.Drawing.Size(843, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 473);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(843, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 473);
            // 
            // DefaultAsynQueryViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 473);
            this.Controls.Add(this.tabCtlMain);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DefaultAsynQueryViewForm";
            this.Text = "数据查询分析浏览窗口";
            this.tabCtlMain.ResumeLayout(false);
            this.tPageGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            this.tPageMultiView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivGrdCtlMain)).EndInit();
            this.tPageDynamicGroup.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtlMain;
        private System.Windows.Forms.TabPage tPageGeneral;
        private MB.XWinLib.XtraGrid.GridControlEx grdCtlMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private System.Windows.Forms.TabPage tPageMultiView;
        private MB.XWinLib.PivotGrid.PivotGridEx pivGrdCtlMain;
        private System.Windows.Forms.TabPage tPageQAChart;
        private System.Windows.Forms.TabPage tPageModuleComment;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labTitleMsg;
        private System.Windows.Forms.ToolStripStatusLabel lnkPreviousPage;
        private System.Windows.Forms.ToolStripStatusLabel lnkNextPage;
        private System.Windows.Forms.ToolStripStatusLabel labQueryContent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private System.Windows.Forms.ToolStripStatusLabel labTotalPageNumber;
        private System.Windows.Forms.ToolStripStatusLabel labCurrentPageNumber;
        private System.Windows.Forms.TabPage tPageDynamicGroup;
        private DynamicGroup.ucDynamicGroupResult ucDynamicGroupResultInQuery;

    }
}