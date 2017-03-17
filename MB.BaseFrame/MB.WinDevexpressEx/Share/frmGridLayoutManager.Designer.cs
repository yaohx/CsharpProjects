namespace MB.XWinLib.Share
{
    partial class frmGridLayoutManager
    {
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "按单据日期显示",
            "111"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("按门店编码显示");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGridLayoutManager));
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSave = new System.Windows.Forms.Button();
            this.butQuit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLayoutName = new System.Windows.Forms.TextBox();
            this.lnkDelete = new System.Windows.Forms.LinkLabel();
            this.lnkAdd = new System.Windows.Forms.LinkLabel();
            this.lstLayoutNames = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butSave);
            this.panel1.Controls.Add(this.butQuit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 319);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 40);
            this.panel1.TabIndex = 0;
            // 
            // butSave
            // 
            this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSave.Location = new System.Drawing.Point(165, 7);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(76, 26);
            this.butSave.TabIndex = 1;
            this.butSave.Text = "应用(&A)";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // butQuit
            // 
            this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butQuit.Location = new System.Drawing.Point(247, 7);
            this.butQuit.Name = "butQuit";
            this.butQuit.Size = new System.Drawing.Size(76, 26);
            this.butQuit.TabIndex = 0;
            this.butQuit.Text = "关闭(&Q)";
            this.butQuit.UseVisualStyleBackColor = true;
            this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "请输入需要保存布局名称：";
            // 
            // txtLayoutName
            // 
            this.txtLayoutName.Location = new System.Drawing.Point(6, 32);
            this.txtLayoutName.Name = "txtLayoutName";
            this.txtLayoutName.Size = new System.Drawing.Size(317, 21);
            this.txtLayoutName.TabIndex = 2;
            // 
            // lnkDelete
            // 
            this.lnkDelete.AutoSize = true;
            this.lnkDelete.Location = new System.Drawing.Point(276, 12);
            this.lnkDelete.Name = "lnkDelete";
            this.lnkDelete.Size = new System.Drawing.Size(47, 12);
            this.lnkDelete.TabIndex = 3;
            this.lnkDelete.TabStop = true;
            this.lnkDelete.Text = "删除(&D)";
            this.lnkDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDelete_LinkClicked);
            // 
            // lnkAdd
            // 
            this.lnkAdd.AutoSize = true;
            this.lnkAdd.Location = new System.Drawing.Point(221, 12);
            this.lnkAdd.Name = "lnkAdd";
            this.lnkAdd.Size = new System.Drawing.Size(47, 12);
            this.lnkAdd.TabIndex = 4;
            this.lnkAdd.TabStop = true;
            this.lnkAdd.Text = "新增(&A)";
            this.lnkAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdd_LinkClicked);
            // 
            // lstLayoutNames
            // 
            this.lstLayoutNames.BackColor = System.Drawing.SystemColors.Window;
            this.lstLayoutNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstLayoutNames.FullRowSelect = true;
            this.lstLayoutNames.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.lstLayoutNames.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lstLayoutNames.Location = new System.Drawing.Point(6, 60);
            this.lstLayoutNames.MultiSelect = false;
            this.lstLayoutNames.Name = "lstLayoutNames";
            this.lstLayoutNames.Size = new System.Drawing.Size(318, 254);
            this.lstLayoutNames.TabIndex = 5;
            this.lstLayoutNames.UseCompatibleStateImageBehavior = false;
            this.lstLayoutNames.View = System.Windows.Forms.View.Details;
            this.lstLayoutNames.DoubleClick += new System.EventHandler(this.lstLayoutNames_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "布局名称";
            this.columnHeader1.Width = 300;
            // 
            // frmGridLayoutManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 359);
            this.Controls.Add(this.lstLayoutNames);
            this.Controls.Add(this.lnkAdd);
            this.Controls.Add(this.lnkDelete);
            this.Controls.Add(this.txtLayoutName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(337, 387);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(337, 387);
            this.Name = "frmGridLayoutManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网格布局管理";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Button butQuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLayoutName;
        private System.Windows.Forms.LinkLabel lnkDelete;
        private System.Windows.Forms.LinkLabel lnkAdd;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lstLayoutNames;
    }
}