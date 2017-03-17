namespace MB.WinClientDefault.QueryFilter {
    partial class FrmGetObjectDataAssistant {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetObjectDataAssistant));
            this.lnkChecked = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUnCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.panBottom = new System.Windows.Forms.Panel();
            this.mpanFilterTop = new System.Windows.Forms.Panel();
            this.nubMaxShotCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.butPreviouss = new MB.WinBase.Ctls.MyButton();
            this.butNext = new MB.WinBase.Ctls.MyButton();
            this.butSure = new MB.WinBase.Ctls.MyButton();
            this.butQuit = new MB.WinBase.Ctls.MyButton();
            this.panMain = new System.Windows.Forms.Panel();
            this.myGroupPanel1 = new MB.WinBase.Ctls.MyGroupPanel();
            this.labMessage = new System.Windows.Forms.Label();
            this.myTabPageControl1 = new MB.WinBase.Ctls.MyTabPageControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlQry = new DevExpress.XtraEditors.PanelControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labTitleMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.lnkPreviousPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lnkNextPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.panBottom.SuspendLayout();
            this.mpanFilterTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nubMaxShotCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGroupPanel1)).BeginInit();
            this.myGroupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myTabPageControl1)).BeginInit();
            this.myTabPageControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlQry)).BeginInit();
            this.pnlQry.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lnkChecked
            // 
            this.lnkChecked.AutoSize = true;
            this.lnkChecked.Location = new System.Drawing.Point(3, 17);
            this.lnkChecked.Name = "lnkChecked";
            this.lnkChecked.Size = new System.Drawing.Size(31, 14);
            this.lnkChecked.TabIndex = 4;
            this.lnkChecked.TabStop = true;
            this.lnkChecked.Text = "选择";
            this.lnkChecked.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChecked_LinkClicked);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCheckAll,
            this.menuUnCheckAll});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 48);
            // 
            // menuCheckAll
            // 
            this.menuCheckAll.Name = "menuCheckAll";
            this.menuCheckAll.Size = new System.Drawing.Size(129, 22);
            this.menuCheckAll.Text = "全选(&A)";
            this.menuCheckAll.Click += new System.EventHandler(this.menuCheckAll_Click);
            // 
            // menuUnCheckAll
            // 
            this.menuUnCheckAll.Name = "menuUnCheckAll";
            this.menuUnCheckAll.Size = new System.Drawing.Size(129, 22);
            this.menuUnCheckAll.Text = "全不选(&U)";
            this.menuUnCheckAll.Click += new System.EventHandler(this.menuUnCheckAll_Click);
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.mpanFilterTop);
            this.panBottom.Controls.Add(this.butPreviouss);
            this.panBottom.Controls.Add(this.butNext);
            this.panBottom.Controls.Add(this.butSure);
            this.panBottom.Controls.Add(this.butQuit);
            this.panBottom.Controls.Add(this.lnkChecked);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(0, 365);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(594, 40);
            this.panBottom.TabIndex = 7;
            // 
            // mpanFilterTop
            // 
            this.mpanFilterTop.Controls.Add(this.nubMaxShotCount);
            this.mpanFilterTop.Controls.Add(this.label1);
            this.mpanFilterTop.Location = new System.Drawing.Point(3, 2);
            this.mpanFilterTop.Name = "mpanFilterTop";
            this.mpanFilterTop.Size = new System.Drawing.Size(187, 34);
            this.mpanFilterTop.TabIndex = 9;
            // 
            // nubMaxShotCount
            // 
            this.nubMaxShotCount.Location = new System.Drawing.Point(99, 7);
            this.nubMaxShotCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nubMaxShotCount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nubMaxShotCount.Name = "nubMaxShotCount";
            this.nubMaxShotCount.Size = new System.Drawing.Size(82, 22);
            this.nubMaxShotCount.TabIndex = 1;
            this.nubMaxShotCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "单页显示条数：";
            // 
            // butPreviouss
            // 
            this.butPreviouss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butPreviouss.Location = new System.Drawing.Point(245, 7);
            this.butPreviouss.Name = "butPreviouss";
            this.butPreviouss.Size = new System.Drawing.Size(80, 28);
            this.butPreviouss.TabIndex = 8;
            this.butPreviouss.Text = "上一步(&P)";
            this.butPreviouss.Click += new System.EventHandler(this.butPreviouss_Click);
            // 
            // butNext
            // 
            this.butNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butNext.Location = new System.Drawing.Point(331, 7);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(80, 28);
            this.butNext.TabIndex = 7;
            this.butNext.Text = "下一步(&N)";
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // butSure
            // 
            this.butSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSure.Location = new System.Drawing.Point(418, 7);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(81, 28);
            this.butSure.TabIndex = 6;
            this.butSure.Text = "确定(&S)";
            this.butSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // butQuit
            // 
            this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butQuit.Location = new System.Drawing.Point(506, 7);
            this.butQuit.Name = "butQuit";
            this.butQuit.Size = new System.Drawing.Size(84, 28);
            this.butQuit.TabIndex = 5;
            this.butQuit.Text = "取消(&Q)";
            this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
            // 
            // panMain
            // 
            this.panMain.Location = new System.Drawing.Point(52, 21);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(198, 83);
            this.panMain.TabIndex = 9;
            // 
            // myGroupPanel1
            // 
            this.myGroupPanel1.Controls.Add(this.labMessage);
            this.myGroupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGroupPanel1.Location = new System.Drawing.Point(0, 0);
            this.myGroupPanel1.Name = "myGroupPanel1";
            this.myGroupPanel1.ShowCaption = false;
            this.myGroupPanel1.Size = new System.Drawing.Size(594, 40);
            this.myGroupPanel1.TabIndex = 4;
            this.myGroupPanel1.Text = "myGroupPanel1";
            this.myGroupPanel1.Visible = false;
            // 
            // labMessage
            // 
            this.labMessage.Font = new System.Drawing.Font("SimSun", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMessage.ForeColor = System.Drawing.Color.Maroon;
            this.labMessage.Location = new System.Drawing.Point(6, 2);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(539, 31);
            this.labMessage.TabIndex = 0;
            this.labMessage.Text = "获取对象数据小助手";
            this.labMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myTabPageControl1
            // 
            this.myTabPageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabPageControl1.Location = new System.Drawing.Point(0, 40);
            this.myTabPageControl1.Name = "myTabPageControl1";
            this.myTabPageControl1.SelectedTabPage = this.xtraTabPage1;
            this.myTabPageControl1.Size = new System.Drawing.Size(594, 325);
            this.myTabPageControl1.TabIndex = 10;
            this.myTabPageControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage1.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage1.Image")));
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(587, 294);
            this.xtraTabPage1.Text = "选择数据";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Collapsed = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlQry);
            this.splitContainerControl1.Panel2.MinSize = 27;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(587, 294);
            this.splitContainerControl1.SplitterPosition = 27;
            this.splitContainerControl1.TabIndex = 11;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // pnlQry
            // 
            this.pnlQry.Controls.Add(this.statusStrip1);
            this.pnlQry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQry.Location = new System.Drawing.Point(0, 0);
            this.pnlQry.Name = "pnlQry";
            this.pnlQry.Size = new System.Drawing.Size(587, 27);
            this.pnlQry.TabIndex = 10;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labTitleMsg,
            this.lnkPreviousPage,
            this.lnkNextPage});
            this.statusStrip1.Location = new System.Drawing.Point(2, 2);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(583, 23);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labTitleMsg
            // 
            this.labTitleMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitleMsg.Name = "labTitleMsg";
            this.labTitleMsg.Size = new System.Drawing.Size(446, 18);
            this.labTitleMsg.Spring = true;
            this.labTitleMsg.Text = "查询耗时 0 毫秒";
            this.labTitleMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkPreviousPage
            // 
            this.lnkPreviousPage.AutoSize = false;
            this.lnkPreviousPage.IsLink = true;
            this.lnkPreviousPage.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkPreviousPage.Name = "lnkPreviousPage";
            this.lnkPreviousPage.Size = new System.Drawing.Size(60, 18);
            this.lnkPreviousPage.Text = "上一页";
            // 
            // lnkNextPage
            // 
            this.lnkNextPage.AutoSize = false;
            this.lnkNextPage.IsLink = true;
            this.lnkNextPage.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkNextPage.Name = "lnkNextPage";
            this.lnkNextPage.Size = new System.Drawing.Size(60, 18);
            this.lnkNextPage.Text = "下一页";
            // 
            // FrmGetObjectDataAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 405);
            this.Controls.Add(this.myTabPageControl1);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.myGroupPanel1);
            this.MinimizeBox = false;
            this.Name = "FrmGetObjectDataAssistant";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "获取对象数据小助手";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            this.mpanFilterTop.ResumeLayout(false);
            this.mpanFilterTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nubMaxShotCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGroupPanel1)).EndInit();
            this.myGroupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myTabPageControl1)).EndInit();
            this.myTabPageControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlQry)).EndInit();
            this.pnlQry.ResumeLayout(false);
            this.pnlQry.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.LinkLabel lnkChecked;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuCheckAll;
        private System.Windows.Forms.ToolStripMenuItem menuUnCheckAll;
        private MB.WinBase.Ctls.MyGroupPanel myGroupPanel1;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Panel panMain;
        private MB.WinBase.Ctls.MyButton butPreviouss;
        private MB.WinBase.Ctls.MyButton butNext;
        private MB.WinBase.Ctls.MyButton butSure;
        private MB.WinBase.Ctls.MyButton butQuit;
        private MB.WinBase.Ctls.MyTabPageControl myTabPageControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.Panel mpanFilterTop;
        private System.Windows.Forms.NumericUpDown nubMaxShotCount;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.PanelControl pnlQry;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labTitleMsg;
        private System.Windows.Forms.ToolStripStatusLabel lnkPreviousPage;
        private System.Windows.Forms.ToolStripStatusLabel lnkNextPage;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}