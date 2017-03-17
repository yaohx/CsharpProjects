namespace MB.WinPrintReport {
    partial class FrmEditPrintTemplete {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditPrintTemplete));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tButPreview = new System.Windows.Forms.ToolStripButton();
            this.tButDesign = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tButSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tButExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trvReport = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.cobDataSource = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtGID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuTreeNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemButPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAddNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuTreeNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tButPreview,
            this.tButDesign,
            this.toolStripSeparator2,
            this.tButSave,
            this.toolStripSeparator1,
            this.tButExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(534, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tButPreview
            // 
            this.tButPreview.Image = ((System.Drawing.Image)(resources.GetObject("tButPreview.Image")));
            this.tButPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tButPreview.Name = "tButPreview";
            this.tButPreview.Size = new System.Drawing.Size(73, 22);
            this.tButPreview.Text = "打印预览";
            this.tButPreview.Click += new System.EventHandler(this.tButPreview_Click);
            // 
            // tButDesign
            // 
            this.tButDesign.Image = ((System.Drawing.Image)(resources.GetObject("tButDesign.Image")));
            this.tButDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tButDesign.Name = "tButDesign";
            this.tButDesign.Size = new System.Drawing.Size(91, 22);
            this.tButDesign.Text = "报表设计(&P)";
            this.tButDesign.Click += new System.EventHandler(this.tButDesign_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tButSave
            // 
            this.tButSave.Image = ((System.Drawing.Image)(resources.GetObject("tButSave.Image")));
            this.tButSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tButSave.Name = "tButSave";
            this.tButSave.Size = new System.Drawing.Size(67, 22);
            this.tButSave.Text = "保存(&S)";
            this.tButSave.Click += new System.EventHandler(this.tButSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tButExit
            // 
            this.tButExit.Image = ((System.Drawing.Image)(resources.GetObject("tButExit.Image")));
            this.tButExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tButExit.Name = "tButExit";
            this.tButExit.Size = new System.Drawing.Size(67, 22);
            this.tButExit.Text = "关闭(&E)";
            this.tButExit.Click += new System.EventHandler(this.tButExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trvReport);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 301);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报表结构：";
            // 
            // trvReport
            // 
            this.trvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvReport.FullRowSelect = true;
            this.trvReport.Location = new System.Drawing.Point(3, 17);
            this.trvReport.Name = "trvReport";
            this.trvReport.Size = new System.Drawing.Size(181, 281);
            this.trvReport.TabIndex = 0;
            this.trvReport.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trvReport_AfterLabelEdit);
            this.trvReport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trvReport_MouseUp);
            this.trvReport.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvReport_AfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(187, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 301);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRemark);
            this.groupBox2.Controls.Add(this.cobDataSource);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.txtGID);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(191, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 301);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "报表基本描述信息：";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(118, 100);
            this.txtRemark.MaxLength = 500;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(211, 106);
            this.txtRemark.TabIndex = 7;
            // 
            // cobDataSource
            // 
            this.cobDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobDataSource.FormattingEnabled = true;
            this.cobDataSource.Location = new System.Drawing.Point(118, 74);
            this.cobDataSource.Name = "cobDataSource";
            this.cobDataSource.Size = new System.Drawing.Size(211, 20);
            this.cobDataSource.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(118, 47);
            this.txtName.MaxLength = 64;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(211, 21);
            this.txtName.TabIndex = 5;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtGID
            // 
            this.txtGID.Location = new System.Drawing.Point(119, 18);
            this.txtGID.MaxLength = 64;
            this.txtGID.Name = "txtGID";
            this.txtGID.ReadOnly = true;
            this.txtGID.Size = new System.Drawing.Size(210, 21);
            this.txtGID.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "备注：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据源：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "报表名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "报表ID:";
            // 
            // contextMenuTreeNode
            // 
            this.contextMenuTreeNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPrintPreview,
            this.menuItemButPrint,
            this.toolStripMenuItem1,
            this.menuItemAddNew,
            this.menuItemDelete});
            this.contextMenuTreeNode.Name = "contextMenuTreeNode";
            this.contextMenuTreeNode.Size = new System.Drawing.Size(119, 98);
            // 
            // menuItemPrintPreview
            // 
            this.menuItemPrintPreview.Name = "menuItemPrintPreview";
            this.menuItemPrintPreview.Size = new System.Drawing.Size(118, 22);
            this.menuItemPrintPreview.Text = "打印预览";
            this.menuItemPrintPreview.Click += new System.EventHandler(this.menuItemPrintPreview_Click);
            // 
            // menuItemButPrint
            // 
            this.menuItemButPrint.Name = "menuItemButPrint";
            this.menuItemButPrint.Size = new System.Drawing.Size(118, 22);
            this.menuItemButPrint.Text = "报表设计";
            this.menuItemButPrint.Click += new System.EventHandler(this.menuItemButPrint_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 6);
            // 
            // menuItemAddNew
            // 
            this.menuItemAddNew.Name = "menuItemAddNew";
            this.menuItemAddNew.Size = new System.Drawing.Size(118, 22);
            this.menuItemAddNew.Text = "新增(&N)";
            this.menuItemAddNew.Click += new System.EventHandler(this.menuItemAddNew_Click);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.Size = new System.Drawing.Size(118, 22);
            this.menuItemDelete.Text = "删除(&D)";
            this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Function.ICO");
            this.imageList1.Images.SetKeyName(1, "Select_Node1.ICO");
            this.imageList1.Images.SetKeyName(2, "tvuTOC.ico");
            // 
            // FrmEditPrintTemplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 326);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditPrintTemplete";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印报表模板设计";
            this.Load += new System.EventHandler(this.FrmEditPrintTemplete_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuTreeNode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView trvReport;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.ComboBox cobDataSource;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtGID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tButSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tButExit;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeNode;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddNew;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private System.Windows.Forms.ToolStripButton tButDesign;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tButPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem menuItemButPrint;

    }
}