namespace MB.WinClientDefault {
    partial class XtraGridViewEditForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraGridViewEditForm));
            this.panTitle = new System.Windows.Forms.Panel();
            this.tabCtlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdCtlMain = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labTitleMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.labTotalPageNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.labCurrentPageNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.lnkPreviousPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lnkNextPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabCtlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTitle
            // 
            this.panTitle.BackColor = System.Drawing.SystemColors.Info;
            this.panTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTitle.ForeColor = System.Drawing.Color.Black;
            this.panTitle.Location = new System.Drawing.Point(0, 0);
            this.panTitle.Name = "panTitle";
            this.panTitle.Size = new System.Drawing.Size(880, 7);
            this.panTitle.TabIndex = 4;
            this.panTitle.Visible = false;
            // 
            // tabCtlMain
            // 
            this.tabCtlMain.Controls.Add(this.tabPage1);
            this.tabCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlMain.ImageList = this.imageList1;
            this.tabCtlMain.Location = new System.Drawing.Point(0, 7);
            this.tabCtlMain.Name = "tabCtlMain";
            this.tabCtlMain.SelectedIndex = 0;
            this.tabCtlMain.Size = new System.Drawing.Size(880, 551);
            this.tabCtlMain.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdCtlMain);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(872, 524);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "编辑列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdCtlMain
            // 
            this.grdCtlMain.DataIOControl = null;
            this.grdCtlMain.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.grdCtlMain.Location = new System.Drawing.Point(49, 43);
            this.grdCtlMain.MainView = this.gridViewMain;
            this.grdCtlMain.Name = "grdCtlMain";
            this.grdCtlMain.ShowOptionMenu = false;
            this.grdCtlMain.Size = new System.Drawing.Size(635, 359);
            this.grdCtlMain.TabIndex = 0;
            this.grdCtlMain.ValidedDeleteKeyDown = false;
            this.grdCtlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // gridViewMain
            // 
            this.gridViewMain.GridControl = this.grdCtlMain;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewMain_RowCellStyle);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Add.ico");
            // 
            // dockManager1
            // 
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
            this.dockPanel1.ID = new System.Guid("ad652f59-c51e-470d-a96e-795c090aa702");
            this.dockPanel1.Location = new System.Drawing.Point(0, 558);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 60);
            this.dockPanel1.Size = new System.Drawing.Size(880, 60);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.statusStrip1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(874, 32);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labTitleMsg,
            this.labTotalPageNumber,
            this.labCurrentPageNumber,
            this.lnkPreviousPage,
            this.lnkNextPage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(874, 32);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labTitleMsg
            // 
            this.labTitleMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitleMsg.Name = "labTitleMsg";
            this.labTitleMsg.Size = new System.Drawing.Size(524, 27);
            this.labTitleMsg.Spring = true;
            this.labTitleMsg.Text = "查询耗时 0 毫秒";
            this.labTitleMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalPageNumber
            // 
            this.labTotalPageNumber.Name = "labTotalPageNumber";
            this.labTotalPageNumber.Size = new System.Drawing.Size(51, 27);
            this.labTotalPageNumber.Text = "总共0页";
            // 
            // labCurrentPageNumber
            // 
            this.labCurrentPageNumber.Name = "labCurrentPageNumber";
            this.labCurrentPageNumber.Size = new System.Drawing.Size(63, 27);
            this.labCurrentPageNumber.Text = "当前第0页";
            // 
            // lnkPreviousPage
            // 
            this.lnkPreviousPage.AutoSize = false;
            this.lnkPreviousPage.IsLink = true;
            this.lnkPreviousPage.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkPreviousPage.Name = "lnkPreviousPage";
            this.lnkPreviousPage.Size = new System.Drawing.Size(94, 27);
            this.lnkPreviousPage.Text = "上一页";
            // 
            // lnkNextPage
            // 
            this.lnkNextPage.AutoSize = false;
            this.lnkNextPage.IsLink = true;
            this.lnkNextPage.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkNextPage.Name = "lnkNextPage";
            this.lnkNextPage.Size = new System.Drawing.Size(94, 27);
            this.lnkNextPage.Text = "下一页";
            // 
            // XtraGridViewEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 618);
            this.Controls.Add(this.tabCtlMain);
            this.Controls.Add(this.panTitle);
            this.Controls.Add(this.dockPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "XtraGridViewEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "可编辑的网格浏览界面";
            this.tabCtlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTitle;
        private System.Windows.Forms.TabControl tabCtlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private MB.XWinLib.XtraGrid.GridControlEx grdCtlMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labTitleMsg;
        private System.Windows.Forms.ToolStripStatusLabel labTotalPageNumber;
        private System.Windows.Forms.ToolStripStatusLabel labCurrentPageNumber;
        private System.Windows.Forms.ToolStripStatusLabel lnkPreviousPage;
        private System.Windows.Forms.ToolStripStatusLabel lnkNextPage;
    }
}