namespace MB.WinClientDefault.QueryFilter {
    partial class FrmQueryFilterInput {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryFilterInput));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panBottom = new System.Windows.Forms.Panel();
            this.mbutSure = new MB.WinBase.Ctls.MyButton();
            this.mpanFilterTop = new System.Windows.Forms.Panel();
            this.cbQueryAll = new System.Windows.Forms.CheckBox();
            this.cbShowTotalPage = new System.Windows.Forms.CheckBox();
            this.lnkDynamaicSetting = new System.Windows.Forms.LinkLabel();
            this.nubMaxShotCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mbutQuit = new MB.WinBase.Ctls.MyButton();
            this.tabCtlFilterMain = new MB.WinBase.Ctls.MyTabPageControl();
            this.tPageSimgleFilter = new DevExpress.XtraTab.XtraTabPage();
            this.panQuickFilter = new System.Windows.Forms.Panel();
            this.tPageAdvanceFilter = new DevExpress.XtraTab.XtraTabPage();
            this.panAdvanceFilter = new System.Windows.Forms.Panel();
            this.panBottom.SuspendLayout();
            this.mpanFilterTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nubMaxShotCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtlFilterMain)).BeginInit();
            this.tabCtlFilterMain.SuspendLayout();
            this.tPageSimgleFilter.SuspendLayout();
            this.tPageAdvanceFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Query.ico");
            this.imageList1.Images.SetKeyName(1, "Add.ico");
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.mbutSure);
            this.panBottom.Controls.Add(this.mpanFilterTop);
            this.panBottom.Controls.Add(this.mbutQuit);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(0, 304);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(637, 40);
            this.panBottom.TabIndex = 4;
            // 
            // mbutSure
            // 
            this.mbutSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mbutSure.Location = new System.Drawing.Point(469, 6);
            this.mbutSure.Name = "mbutSure";
            this.mbutSure.Size = new System.Drawing.Size(80, 28);
            this.mbutSure.TabIndex = 7;
            this.mbutSure.Text = "确定(&S)";
            this.mbutSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // mpanFilterTop
            // 
            this.mpanFilterTop.Controls.Add(this.cbQueryAll);
            this.mpanFilterTop.Controls.Add(this.cbShowTotalPage);
            this.mpanFilterTop.Controls.Add(this.lnkDynamaicSetting);
            this.mpanFilterTop.Controls.Add(this.nubMaxShotCount);
            this.mpanFilterTop.Controls.Add(this.label1);
            this.mpanFilterTop.Location = new System.Drawing.Point(3, 3);
            this.mpanFilterTop.Name = "mpanFilterTop";
            this.mpanFilterTop.Size = new System.Drawing.Size(408, 34);
            this.mpanFilterTop.TabIndex = 5;
            // 
            // cbQueryAll
            // 
            this.cbQueryAll.AutoSize = true;
            this.cbQueryAll.Location = new System.Drawing.Point(243, 8);
            this.cbQueryAll.Name = "cbQueryAll";
            this.cbQueryAll.Size = new System.Drawing.Size(62, 18);
            this.cbQueryAll.TabIndex = 4;
            this.cbQueryAll.Text = "不分页";
            this.cbQueryAll.UseVisualStyleBackColor = true;
            this.cbQueryAll.CheckedChanged += new System.EventHandler(this.cbQueryAll_CheckedChanged);
            // 
            // cbShowTotalPage
            // 
            this.cbShowTotalPage.AutoSize = true;
            this.cbShowTotalPage.Location = new System.Drawing.Point(151, 8);
            this.cbShowTotalPage.Name = "cbShowTotalPage";
            this.cbShowTotalPage.Size = new System.Drawing.Size(86, 18);
            this.cbShowTotalPage.TabIndex = 3;
            this.cbShowTotalPage.Text = "显示总页数";
            this.cbShowTotalPage.UseVisualStyleBackColor = true;
            this.cbShowTotalPage.CheckedChanged += new System.EventHandler(this.cbShowTotalPage_CheckedChanged);
            // 
            // lnkDynamaicSetting
            // 
            this.lnkDynamaicSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDynamaicSetting.AutoSize = true;
            this.lnkDynamaicSetting.Location = new System.Drawing.Point(321, 9);
            this.lnkDynamaicSetting.Name = "lnkDynamaicSetting";
            this.lnkDynamaicSetting.Size = new System.Drawing.Size(67, 14);
            this.lnkDynamaicSetting.TabIndex = 2;
            this.lnkDynamaicSetting.TabStop = true;
            this.lnkDynamaicSetting.Text = "动态列设置";
            this.lnkDynamaicSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDynamaicSetting_LinkClicked);
            // 
            // nubMaxShotCount
            // 
            this.nubMaxShotCount.Location = new System.Drawing.Point(69, 7);
            this.nubMaxShotCount.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nubMaxShotCount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nubMaxShotCount.Name = "nubMaxShotCount";
            this.nubMaxShotCount.Size = new System.Drawing.Size(76, 22);
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
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "单页条数：";
            // 
            // mbutQuit
            // 
            this.mbutQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mbutQuit.Location = new System.Drawing.Point(557, 6);
            this.mbutQuit.Name = "mbutQuit";
            this.mbutQuit.Size = new System.Drawing.Size(75, 28);
            this.mbutQuit.TabIndex = 6;
            this.mbutQuit.Text = "取消(&Q)";
            this.mbutQuit.Click += new System.EventHandler(this.butQuit_Click);
            // 
            // tabCtlFilterMain
            // 
            this.tabCtlFilterMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlFilterMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtlFilterMain.Name = "tabCtlFilterMain";
            this.tabCtlFilterMain.SelectedTabPage = this.tPageSimgleFilter;
            this.tabCtlFilterMain.Size = new System.Drawing.Size(637, 304);
            this.tabCtlFilterMain.TabIndex = 6;
            this.tabCtlFilterMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tPageSimgleFilter,
            this.tPageAdvanceFilter});
            this.tabCtlFilterMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabCtlFilterMain_SelectedPageChanged);
            // 
            // tPageSimgleFilter
            // 
            this.tPageSimgleFilter.Controls.Add(this.panQuickFilter);
            this.tPageSimgleFilter.Image = ((System.Drawing.Image)(resources.GetObject("tPageSimgleFilter.Image")));
            this.tPageSimgleFilter.Name = "tPageSimgleFilter";
            this.tPageSimgleFilter.Size = new System.Drawing.Size(630, 273);
            this.tPageSimgleFilter.Text = "快速查询";
            // 
            // panQuickFilter
            // 
            this.panQuickFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panQuickFilter.Location = new System.Drawing.Point(0, 0);
            this.panQuickFilter.Name = "panQuickFilter";
            this.panQuickFilter.Size = new System.Drawing.Size(630, 273);
            this.panQuickFilter.TabIndex = 0;
            // 
            // tPageAdvanceFilter
            // 
            this.tPageAdvanceFilter.Controls.Add(this.panAdvanceFilter);
            this.tPageAdvanceFilter.Image = ((System.Drawing.Image)(resources.GetObject("tPageAdvanceFilter.Image")));
            this.tPageAdvanceFilter.Name = "tPageAdvanceFilter";
            this.tPageAdvanceFilter.Size = new System.Drawing.Size(630, 273);
            this.tPageAdvanceFilter.Text = "高级查询";
            // 
            // panAdvanceFilter
            // 
            this.panAdvanceFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAdvanceFilter.Location = new System.Drawing.Point(0, 0);
            this.panAdvanceFilter.Name = "panAdvanceFilter";
            this.panAdvanceFilter.Size = new System.Drawing.Size(630, 273);
            this.panAdvanceFilter.TabIndex = 0;
            // 
            // FrmQueryFilterInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 344);
            this.Controls.Add(this.tabCtlFilterMain);
            this.Controls.Add(this.panBottom);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmQueryFilterInput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据查找";
            this.Load += new System.EventHandler(this.FrmQueryFilterInput_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmQueryFilterInput_KeyDown);
            this.panBottom.ResumeLayout(false);
            this.mpanFilterTop.ResumeLayout(false);
            this.mpanFilterTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nubMaxShotCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtlFilterMain)).EndInit();
            this.tabCtlFilterMain.ResumeLayout(false);
            this.tPageSimgleFilter.ResumeLayout(false);
            this.tPageAdvanceFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Panel mpanFilterTop;
        private MB.WinBase.Ctls.MyTabPageControl tabCtlFilterMain;
        private DevExpress.XtraTab.XtraTabPage tPageSimgleFilter;
        private DevExpress.XtraTab.XtraTabPage tPageAdvanceFilter;
        private System.Windows.Forms.Panel panQuickFilter;
        private System.Windows.Forms.Panel panAdvanceFilter;
        private MB.WinBase.Ctls.MyButton mbutSure;
        private MB.WinBase.Ctls.MyButton mbutQuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nubMaxShotCount;
        private System.Windows.Forms.LinkLabel lnkDynamaicSetting;
        private System.Windows.Forms.CheckBox cbShowTotalPage;
        private System.Windows.Forms.CheckBox cbQueryAll;
    }
}