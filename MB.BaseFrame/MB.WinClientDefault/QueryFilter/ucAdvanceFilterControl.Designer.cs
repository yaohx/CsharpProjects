namespace MB.WinClientDefault.QueryFilter {
    partial class ucAdvanceFilterControl {
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkTemplateOperate = new System.Windows.Forms.LinkLabel();
            this.cobFilterTemplate = new System.Windows.Forms.ComboBox();
            this.cMenuTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sMenuTemplateSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.sMenuTemplateDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.sMenuTemplateClear = new System.Windows.Forms.ToolStripMenuItem();
            this.myScrollablePanel1 = new System.Windows.Forms.Panel();
            this.panMain = new System.Windows.Forms.Panel();
            this.myScrollablePanel2 = new System.Windows.Forms.Panel();
            this.cMenuTemplate.SuspendLayout();
            this.myScrollablePanel1.SuspendLayout();
            this.myScrollablePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择查询模板：";
            // 
            // lnkTemplateOperate
            // 
            this.lnkTemplateOperate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkTemplateOperate.AutoSize = true;
            this.lnkTemplateOperate.Location = new System.Drawing.Point(607, 12);
            this.lnkTemplateOperate.Name = "lnkTemplateOperate";
            this.lnkTemplateOperate.Size = new System.Drawing.Size(53, 12);
            this.lnkTemplateOperate.TabIndex = 1;
            this.lnkTemplateOperate.TabStop = true;
            this.lnkTemplateOperate.Text = "模板操作";
            this.lnkTemplateOperate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTemplateOperate_LinkClicked);
            // 
            // cobFilterTemplate
            // 
            this.cobFilterTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cobFilterTemplate.FormattingEnabled = true;
            this.cobFilterTemplate.Location = new System.Drawing.Point(98, 9);
            this.cobFilterTemplate.Name = "cobFilterTemplate";
            this.cobFilterTemplate.Size = new System.Drawing.Size(503, 20);
            this.cobFilterTemplate.TabIndex = 0;
            this.cobFilterTemplate.SelectedIndexChanged += new System.EventHandler(this.cobFilterTemplate_SelectedIndexChanged);
            // 
            // cMenuTemplate
            // 
            this.cMenuTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sMenuTemplateSave,
            this.toolStripMenuItem1,
            this.sMenuTemplateDelete,
            this.toolStripMenuItem2,
            this.sMenuTemplateClear});
            this.cMenuTemplate.Name = "cMenuTemplate";
            this.cMenuTemplate.Size = new System.Drawing.Size(161, 82);
            // 
            // sMenuTemplateSave
            // 
            this.sMenuTemplateSave.Name = "sMenuTemplateSave";
            this.sMenuTemplateSave.Size = new System.Drawing.Size(160, 22);
            this.sMenuTemplateSave.Text = "保存为模板(&S)";
            this.sMenuTemplateSave.Click += new System.EventHandler(this.sMenuTemplateSave_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // sMenuTemplateDelete
            // 
            this.sMenuTemplateDelete.Name = "sMenuTemplateDelete";
            this.sMenuTemplateDelete.Size = new System.Drawing.Size(160, 22);
            this.sMenuTemplateDelete.Text = "删除当前模板(&D)";
            this.sMenuTemplateDelete.Click += new System.EventHandler(this.sMenuTemplateDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // sMenuTemplateClear
            // 
            this.sMenuTemplateClear.Name = "sMenuTemplateClear";
            this.sMenuTemplateClear.Size = new System.Drawing.Size(160, 22);
            this.sMenuTemplateClear.Text = "清除所有模板(&C)";
            this.sMenuTemplateClear.Click += new System.EventHandler(this.sMenuTemplateClear_Click);
            // 
            // myScrollablePanel1
            // 
            this.myScrollablePanel1.Controls.Add(this.panMain);
            this.myScrollablePanel1.Controls.Add(this.myScrollablePanel2);
            this.myScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myScrollablePanel1.Location = new System.Drawing.Point(0, 0);
            this.myScrollablePanel1.Name = "myScrollablePanel1";
            this.myScrollablePanel1.Size = new System.Drawing.Size(663, 443);
            this.myScrollablePanel1.TabIndex = 2;
            // 
            // panMain
            // 
            this.panMain.Location = new System.Drawing.Point(57, 121);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(477, 94);
            this.panMain.TabIndex = 1;
            // 
            // myScrollablePanel2
            // 
            this.myScrollablePanel2.Controls.Add(this.lnkTemplateOperate);
            this.myScrollablePanel2.Controls.Add(this.label1);
            this.myScrollablePanel2.Controls.Add(this.cobFilterTemplate);
            this.myScrollablePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.myScrollablePanel2.Location = new System.Drawing.Point(0, 0);
            this.myScrollablePanel2.Name = "myScrollablePanel2";
            this.myScrollablePanel2.Size = new System.Drawing.Size(663, 31);
            this.myScrollablePanel2.TabIndex = 0;
            // 
            // ucAdvanceFilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.myScrollablePanel1);
            this.Name = "ucAdvanceFilterControl";
            this.Size = new System.Drawing.Size(663, 443);
            this.cMenuTemplate.ResumeLayout(false);
            this.myScrollablePanel1.ResumeLayout(false);
            this.myScrollablePanel2.ResumeLayout(false);
            this.myScrollablePanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkTemplateOperate;
        private System.Windows.Forms.ComboBox cobFilterTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cMenuTemplate;
        private System.Windows.Forms.ToolStripMenuItem sMenuTemplateSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sMenuTemplateDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sMenuTemplateClear;
        private System.Windows.Forms.Panel myScrollablePanel1;
        private System.Windows.Forms.Panel myScrollablePanel2;
        private System.Windows.Forms.Panel panMain;
    }
}
