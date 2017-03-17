namespace WinTestProject {
    partial class XtraForm1 {
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
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.myTableLayoutPanelProvider1 = new MB.WinBase.Design.MyTableLayoutPanelProvider(this.components);
            this.myDataBindingProvider1 = new MB.WinBase.Binding.MyDataBindingProvider(this.components);
            this.myGroupPanel1 = new MB.WinBase.Ctls.MyGroupPanel();
            this.myTabPageControl1 = new MB.WinBase.Ctls.MyTabPageControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.开始分拣ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分拣装箱完成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.myGroupPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTabPageControl1)).BeginInit();
            this.myTabPageControl1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Black";
            // 
            // myDataBindingProvider1
            // 
            this.myDataBindingProvider1.XmlConfigFile = null;
            // 
            // myGroupPanel1
            // 
            this.myGroupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGroupPanel1.Location = new System.Drawing.Point(0, 26);
            this.myGroupPanel1.Name = "myGroupPanel1";
            this.myGroupPanel1.Size = new System.Drawing.Size(636, 125);
            this.myGroupPanel1.TabIndex = 4;
            this.myGroupPanel1.Text = "myGroupPanel1";
            // 
            // myTabPageControl1
            // 
            this.myTabPageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabPageControl1.Location = new System.Drawing.Point(0, 151);
            this.myTabPageControl1.Name = "myTabPageControl1";
            this.myTabPageControl1.SelectedTabPage = this.xtraTabPage1;
            this.myTabPageControl1.Size = new System.Drawing.Size(636, 294);
            this.myTabPageControl1.TabIndex = 5;
            this.myTabPageControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(627, 263);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(627, 263);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始分拣ToolStripMenuItem,
            this.分拣装箱完成ToolStripMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(143, 54);
            // 
            // 开始分拣ToolStripMenuItem
            // 
            this.开始分拣ToolStripMenuItem.Name = "开始分拣ToolStripMenuItem";
            this.开始分拣ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.开始分拣ToolStripMenuItem.Text = "开始分拣";
            this.开始分拣ToolStripMenuItem.Click += new System.EventHandler(this.开始分拣ToolStripMenuItem_Click);
            // 
            // 分拣装箱完成ToolStripMenuItem
            // 
            this.分拣装箱完成ToolStripMenuItem.Name = "分拣装箱完成ToolStripMenuItem";
            this.分拣装箱完成ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.分拣装箱完成ToolStripMenuItem.Text = "分拣装箱完成";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 6);
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 445);
            this.Controls.Add(this.myTabPageControl1);
            this.Controls.Add(this.myGroupPanel1);
            this.LookAndFeel.SkinName = "Black";
            this.Name = "XtraForm1";
            this.Text = "发货单";
            this.Controls.SetChildIndex(this.myGroupPanel1, 0);
            this.Controls.SetChildIndex(this.myTabPageControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.myGroupPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTabPageControl1)).EndInit();
            this.myTabPageControl1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private MB.WinBase.Design.MyTableLayoutPanelProvider myTableLayoutPanelProvider1;
        private MB.WinBase.Binding.MyDataBindingProvider myDataBindingProvider1;
        private MB.WinBase.Ctls.MyGroupPanel myGroupPanel1;
        private MB.WinBase.Ctls.MyTabPageControl myTabPageControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 开始分拣ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分拣装箱完成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

    }
}