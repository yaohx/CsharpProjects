namespace MB.WinClientDefault.Ctls {
    partial class ucBfModuleComment {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBfModuleComment));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rxtInputComment = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cobCommentType = new System.Windows.Forms.ComboBox();
            this.butSubmitComment = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tButFirstPage = new System.Windows.Forms.ToolStripButton();
            this.tButPreviousePage = new System.Windows.Forms.ToolStripButton();
            this.tsTxtCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsLabTotalPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tButNextPage = new System.Windows.Forms.ToolStripButton();
            this.tButLastPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tButRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tButClear = new System.Windows.Forms.ToolStripButton();
            this.rxtModuleComment = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 206);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(602, 2);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rxtInputComment);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 208);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 103);
            this.panel1.TabIndex = 2;
            // 
            // rxtInputComment
            // 
            this.rxtInputComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtInputComment.EnableAutoDragDrop = true;
            this.rxtInputComment.Location = new System.Drawing.Point(0, 0);
            this.rxtInputComment.Name = "rxtInputComment";
            this.rxtInputComment.Size = new System.Drawing.Size(498, 103);
            this.rxtInputComment.TabIndex = 1;
            this.rxtInputComment.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cobCommentType);
            this.panel2.Controls.Add(this.butSubmitComment);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(498, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(104, 103);
            this.panel2.TabIndex = 0;
            // 
            // cobCommentType
            // 
            this.cobCommentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobCommentType.FormattingEnabled = true;
            this.cobCommentType.Location = new System.Drawing.Point(8, 51);
            this.cobCommentType.Name = "cobCommentType";
            this.cobCommentType.Size = new System.Drawing.Size(95, 20);
            this.cobCommentType.TabIndex = 3;
            // 
            // butSubmitComment
            // 
            this.butSubmitComment.Location = new System.Drawing.Point(6, 9);
            this.butSubmitComment.Name = "butSubmitComment";
            this.butSubmitComment.Size = new System.Drawing.Size(93, 27);
            this.butSubmitComment.TabIndex = 0;
            this.butSubmitComment.Text = "提交(&S)";
            this.butSubmitComment.UseVisualStyleBackColor = true;
            this.butSubmitComment.Click += new System.EventHandler(this.butSubmitComment_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(602, 31);
            this.panel3.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tButFirstPage,
            this.tButPreviousePage,
            this.tsTxtCurrentPage,
            this.toolStripSeparator1,
            this.tsLabTotalPage,
            this.toolStripSeparator2,
            this.tButNextPage,
            this.tButLastPage,
            this.toolStripSeparator3,
            this.tButRefresh,
            this.toolStripSeparator4,
            this.tButClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(602, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tButFirstPage
            // 
            this.tButFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tButFirstPage.Enabled = false;
            this.tButFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("tButFirstPage.Image")));
            this.tButFirstPage.Name = "tButFirstPage";
            this.tButFirstPage.RightToLeftAutoMirrorImage = true;
            this.tButFirstPage.Size = new System.Drawing.Size(23, 22);
            this.tButFirstPage.Text = "移到第一条记录";
            // 
            // tButPreviousePage
            // 
            this.tButPreviousePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tButPreviousePage.Enabled = false;
            this.tButPreviousePage.Image = ((System.Drawing.Image)(resources.GetObject("tButPreviousePage.Image")));
            this.tButPreviousePage.Name = "tButPreviousePage";
            this.tButPreviousePage.RightToLeftAutoMirrorImage = true;
            this.tButPreviousePage.Size = new System.Drawing.Size(23, 22);
            this.tButPreviousePage.Text = "移到上一条记录";
            // 
            // tsTxtCurrentPage
            // 
            this.tsTxtCurrentPage.AccessibleName = "位置";
            this.tsTxtCurrentPage.AutoSize = false;
            this.tsTxtCurrentPage.Name = "tsTxtCurrentPage";
            this.tsTxtCurrentPage.Size = new System.Drawing.Size(50, 21);
            this.tsTxtCurrentPage.Text = "1";
            this.tsTxtCurrentPage.ToolTipText = "当前位置";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsLabTotalPage
            // 
            this.tsLabTotalPage.Name = "tsLabTotalPage";
            this.tsLabTotalPage.Size = new System.Drawing.Size(23, 22);
            this.tsLabTotalPage.Text = "/ 1";
            this.tsLabTotalPage.ToolTipText = "总项数";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tButNextPage
            // 
            this.tButNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tButNextPage.Enabled = false;
            this.tButNextPage.Image = ((System.Drawing.Image)(resources.GetObject("tButNextPage.Image")));
            this.tButNextPage.Name = "tButNextPage";
            this.tButNextPage.RightToLeftAutoMirrorImage = true;
            this.tButNextPage.Size = new System.Drawing.Size(23, 22);
            this.tButNextPage.Text = "移到下一条记录";
            // 
            // tButLastPage
            // 
            this.tButLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tButLastPage.Enabled = false;
            this.tButLastPage.Image = ((System.Drawing.Image)(resources.GetObject("tButLastPage.Image")));
            this.tButLastPage.Name = "tButLastPage";
            this.tButLastPage.RightToLeftAutoMirrorImage = true;
            this.tButLastPage.Size = new System.Drawing.Size(23, 22);
            this.tButLastPage.Text = "移到最后一条记录";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tButRefresh
            // 
            this.tButRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tButRefresh.Image")));
            this.tButRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tButRefresh.Name = "tButRefresh";
            this.tButRefresh.Size = new System.Drawing.Size(67, 22);
            this.tButRefresh.Text = "刷新(&R)";
            this.tButRefresh.Click += new System.EventHandler(this.tButRefresh_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tButClear
            // 
            this.tButClear.Enabled = false;
            this.tButClear.Image = ((System.Drawing.Image)(resources.GetObject("tButClear.Image")));
            this.tButClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tButClear.Name = "tButClear";
            this.tButClear.Size = new System.Drawing.Size(67, 22);
            this.tButClear.Text = "清除(&C)";
            this.tButClear.Click += new System.EventHandler(this.tButClear_Click);
            // 
            // rxtModuleComment
            // 
            this.rxtModuleComment.BackColor = System.Drawing.Color.White;
            this.rxtModuleComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtModuleComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtModuleComment.Location = new System.Drawing.Point(0, 31);
            this.rxtModuleComment.Name = "rxtModuleComment";
            this.rxtModuleComment.ReadOnly = true;
            this.rxtModuleComment.Size = new System.Drawing.Size(602, 175);
            this.rxtModuleComment.TabIndex = 5;
            this.rxtModuleComment.Text = "";
            // 
            // ucBfModuleComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.rxtModuleComment);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "ucBfModuleComment";
            this.Size = new System.Drawing.Size(602, 311);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rxtInputComment;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cobCommentType;
        private System.Windows.Forms.Button butSubmitComment;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox rxtModuleComment;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tButFirstPage;
        private System.Windows.Forms.ToolStripButton tButPreviousePage;
        private System.Windows.Forms.ToolStripTextBox tsTxtCurrentPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsLabTotalPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tButNextPage;
        private System.Windows.Forms.ToolStripButton tButLastPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tButRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tButClear;
    }
}
