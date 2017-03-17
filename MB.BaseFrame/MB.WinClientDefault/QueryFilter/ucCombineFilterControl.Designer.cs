namespace MB.WinClientDefault.QueryFilter
{
    partial class ucCombineFilterControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCombineFilterControl));
            this.tabCtlFilterMain = new MB.WinBase.Ctls.MyTabPageControl();
            this.tPageSimgleFilter = new DevExpress.XtraTab.XtraTabPage();
            this.panQuickFilter = new System.Windows.Forms.Panel();
            this.tPageAdvanceFilter = new DevExpress.XtraTab.XtraTabPage();
            this.panAdvanceFilter = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtlFilterMain)).BeginInit();
            this.tabCtlFilterMain.SuspendLayout();
            this.tPageSimgleFilter.SuspendLayout();
            this.tPageAdvanceFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtlFilterMain
            // 
            this.tabCtlFilterMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlFilterMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtlFilterMain.Name = "tabCtlFilterMain";
            this.tabCtlFilterMain.SelectedTabPage = this.tPageSimgleFilter;
            this.tabCtlFilterMain.Size = new System.Drawing.Size(531, 331);
            this.tabCtlFilterMain.TabIndex = 7;
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
            this.tPageSimgleFilter.Size = new System.Drawing.Size(524, 300);
            this.tPageSimgleFilter.Text = "快速查询";
            // 
            // panQuickFilter
            // 
            this.panQuickFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panQuickFilter.Location = new System.Drawing.Point(0, 0);
            this.panQuickFilter.Name = "panQuickFilter";
            this.panQuickFilter.Size = new System.Drawing.Size(524, 300);
            this.panQuickFilter.TabIndex = 0;
            // 
            // tPageAdvanceFilter
            // 
            this.tPageAdvanceFilter.Controls.Add(this.panAdvanceFilter);
            this.tPageAdvanceFilter.Image = ((System.Drawing.Image)(resources.GetObject("tPageAdvanceFilter.Image")));
            this.tPageAdvanceFilter.Name = "tPageAdvanceFilter";
            this.tPageAdvanceFilter.Size = new System.Drawing.Size(524, 300);
            this.tPageAdvanceFilter.Text = "高级查询";
            // 
            // panAdvanceFilter
            // 
            this.panAdvanceFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAdvanceFilter.Location = new System.Drawing.Point(0, 0);
            this.panAdvanceFilter.Name = "panAdvanceFilter";
            this.panAdvanceFilter.Size = new System.Drawing.Size(524, 300);
            this.panAdvanceFilter.TabIndex = 0;
            // 
            // ucCombineFilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabCtlFilterMain);
            this.Name = "ucCombineFilterControl";
            this.Size = new System.Drawing.Size(531, 331);
            ((System.ComponentModel.ISupportInitialize)(this.tabCtlFilterMain)).EndInit();
            this.tabCtlFilterMain.ResumeLayout(false);
            this.tPageSimgleFilter.ResumeLayout(false);
            this.tPageAdvanceFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinBase.Ctls.MyTabPageControl tabCtlFilterMain;
        private DevExpress.XtraTab.XtraTabPage tPageSimgleFilter;
        private System.Windows.Forms.Panel panQuickFilter;
        private DevExpress.XtraTab.XtraTabPage tPageAdvanceFilter;
        private System.Windows.Forms.Panel panAdvanceFilter;

    }
}
