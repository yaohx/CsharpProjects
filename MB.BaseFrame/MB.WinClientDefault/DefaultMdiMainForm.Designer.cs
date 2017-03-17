namespace MB.WinClientDefault {
    partial class DefaultMdiMainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultMdiMainForm));
            this.barManagerMain = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.barMainMenu = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticItemUserName = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemTime = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManagerMain = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelFunctionTree = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.trvMainFunction = new System.Windows.Forms.TreeView();
            this.dockPanelOnlineMsg = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.rxtMessage = new System.Windows.Forms.RichTextBox();
            this.xmdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.imgMainMenu = new System.Windows.Forms.ImageList(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerMain)).BeginInit();
            this.dockPanelFunctionTree.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dockPanelOnlineMsg.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmdiManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerMain
            // 
            this.barManagerMain.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools,
            this.barMainMenu,
            this.bar3});
            this.barManagerMain.DockControls.Add(this.barDockControlTop);
            this.barManagerMain.DockControls.Add(this.barDockControlBottom);
            this.barManagerMain.DockControls.Add(this.barDockControlLeft);
            this.barManagerMain.DockControls.Add(this.barDockControlRight);
            this.barManagerMain.DockManager = this.dockManagerMain;
            this.barManagerMain.Form = this;
            this.barManagerMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItemUserName,
            this.barStaticItemTime});
            this.barManagerMain.MainMenu = this.barMainMenu;
            this.barManagerMain.MaxItemId = 3;
            this.barManagerMain.StatusBar = this.bar3;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 1;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.Text = "Tools";
            // 
            // barMainMenu
            // 
            this.barMainMenu.BarName = "Main menu";
            this.barMainMenu.DockCol = 0;
            this.barMainMenu.DockRow = 0;
            this.barMainMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMainMenu.OptionsBar.AllowQuickCustomization = false;
            this.barMainMenu.OptionsBar.UseWholeRow = true;
            this.barMainMenu.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemUserName),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemTime)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStaticItemUserName
            // 
            this.barStaticItemUserName.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItemUserName.Id = 1;
            this.barStaticItemUserName.Name = "barStaticItemUserName";
            this.barStaticItemUserName.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // barStaticItemTime
            // 
            this.barStaticItemTime.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.barStaticItemTime.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItemTime.Id = 2;
            this.barStaticItemTime.Name = "barStaticItemTime";
            this.barStaticItemTime.TextAlignment = System.Drawing.StringAlignment.Far;
            this.barStaticItemTime.Width = 32;
            // 
            // dockManagerMain
            // 
            this.dockManagerMain.Form = this;
            this.dockManagerMain.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelFunctionTree,
            this.dockPanelOnlineMsg});
            this.dockManagerMain.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanelFunctionTree
            // 
            this.dockPanelFunctionTree.Controls.Add(this.dockPanel1_Container);
            this.dockPanelFunctionTree.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelFunctionTree.ID = new System.Guid("40f9fc4c-c3ee-4a27-bd66-bee089801c40");
            this.dockPanelFunctionTree.Location = new System.Drawing.Point(0, 49);
            this.dockPanelFunctionTree.Name = "dockPanelFunctionTree";
            this.dockPanelFunctionTree.Size = new System.Drawing.Size(226, 509);
            this.dockPanelFunctionTree.Text = "功能模块树";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.trvMainFunction);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(220, 606);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // trvMainFunction
            // 
            this.trvMainFunction.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trvMainFunction.Location = new System.Drawing.Point(9, 28);
            this.trvMainFunction.Name = "trvMainFunction";
            this.trvMainFunction.Size = new System.Drawing.Size(116, 319);
            this.trvMainFunction.TabIndex = 0;
            this.trvMainFunction.DoubleClick += new System.EventHandler(this.trvMainFunction_DoubleClick);
            this.trvMainFunction.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMainFunction_AfterSelect);
            this.trvMainFunction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trvMainFunction_MouseMove);
            // 
            // dockPanelOnlineMsg
            // 
            this.dockPanelOnlineMsg.Controls.Add(this.dockPanel2_Container);
            this.dockPanelOnlineMsg.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelOnlineMsg.FloatVertical = true;
            this.dockPanelOnlineMsg.ID = new System.Guid("6182248a-9713-4b7b-bc19-582bcfebc0ad");
            this.dockPanelOnlineMsg.Location = new System.Drawing.Point(0, 558);
            this.dockPanelOnlineMsg.Name = "dockPanelOnlineMsg";
            this.dockPanelOnlineMsg.Size = new System.Drawing.Size(1023, 124);
            this.dockPanelOnlineMsg.Text = "信息互动";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.rxtMessage);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 24);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(791, 97);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // rxtMessage
            // 
            this.rxtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rxtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtMessage.Location = new System.Drawing.Point(39, 13);
            this.rxtMessage.Name = "rxtMessage";
            this.rxtMessage.ReadOnly = true;
            this.rxtMessage.Size = new System.Drawing.Size(509, 69);
            this.rxtMessage.TabIndex = 0;
            this.rxtMessage.Text = "";
            // 
            // xmdiManager
            // 
            this.xmdiManager.MdiParent = this;
            // 
            // imgMainMenu
            // 
            this.imgMainMenu.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this.imgMainMenu.ImageSize = new System.Drawing.Size(16, 16);
            this.imgMainMenu.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Silver";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DefaultMdiMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 705);
            this.Controls.Add(this.dockPanelFunctionTree);
            this.Controls.Add(this.dockPanelOnlineMsg);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "DefaultMdiMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "短线产品订货系统";
            this.Load += new System.EventHandler(this.DefaultMdiMainForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.DefaultMdiMainForm_MdiChildActivate);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerMain)).EndInit();
            this.dockPanelFunctionTree.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanelOnlineMsg.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmdiManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerMain;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.Bar barMainMenu;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking.DockManager dockManagerMain;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelOnlineMsg;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelFunctionTree;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xmdiManager;
        private System.Windows.Forms.TreeView trvMainFunction;
        private System.Windows.Forms.ImageList imgMainMenu;
        private System.Windows.Forms.RichTextBox rxtMessage;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.BarStaticItem barStaticItemUserName;
        private DevExpress.XtraBars.BarStaticItem barStaticItemTime;
        private System.Windows.Forms.Timer timer1;
    }
}