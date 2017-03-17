namespace MB.WinClientDefault {
    partial class DefaultAsynCustomQueryView {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultAsynCustomQueryView));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labTitleMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabCtlMain = new System.Windows.Forms.TabControl();
            this.tPageGeneral = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabCtlMain.SuspendLayout();
            this.SuspendLayout();
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
            this.dockPanel1.FloatSize = new System.Drawing.Size(200, 121);
            this.dockPanel1.ID = new System.Guid("2358d1a4-37aa-401b-a23f-60d4401102d0");
            this.dockPanel1.Location = new System.Drawing.Point(0, 426);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 53);
            this.dockPanel1.Size = new System.Drawing.Size(862, 53);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.statusStrip1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(856, 25);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labTitleMsg,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(856, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
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
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // tabCtlMain
            // 
            this.tabCtlMain.Controls.Add(this.tPageGeneral);
            this.tabCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlMain.ImageList = this.imageList1;
            this.tabCtlMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtlMain.Name = "tabCtlMain";
            this.tabCtlMain.SelectedIndex = 0;
            this.tabCtlMain.Size = new System.Drawing.Size(862, 426);
            this.tabCtlMain.TabIndex = 8;
            // 
            // tPageGeneral
            // 
            this.tPageGeneral.ImageIndex = 2;
            this.tPageGeneral.Location = new System.Drawing.Point(4, 23);
            this.tPageGeneral.Name = "tPageGeneral";
            this.tPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tPageGeneral.Size = new System.Drawing.Size(854, 399);
            this.tPageGeneral.TabIndex = 0;
            this.tPageGeneral.Text = "自定义视图";
            this.tPageGeneral.UseVisualStyleBackColor = true;
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
            // DefaultAsynCustomQueryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 479);
            this.Controls.Add(this.tabCtlMain);
            this.Controls.Add(this.dockPanel1);
            this.Name = "DefaultAsynCustomQueryView";
            this.Text = "自定义数据查询窗口";
            this.Load += new System.EventHandler(this.DefaultAsynCustomQueryView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabCtlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labTitleMsg;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabControl tabCtlMain;
        private System.Windows.Forms.TabPage tPageGeneral;
        private System.Windows.Forms.ImageList imageList1;

    }
}