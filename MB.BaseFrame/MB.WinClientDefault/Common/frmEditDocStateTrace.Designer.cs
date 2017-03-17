namespace MB.WinClientDefault.Common {
    partial class frmEditDocStateTrace {
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
            this.butQuit = new MB.WinBase.Ctls.MyButton();
            this.butSure = new MB.WinBase.Ctls.MyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.myTabPageControl1 = new MB.WinBase.Ctls.MyTabPageControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.txtOpRemark = new System.Windows.Forms.TextBox();
            this.dTimeOpDate = new System.Windows.Forms.DateTimePicker();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myTabPageControl1)).BeginInit();
            this.myTabPageControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butQuit
            // 
            this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butQuit.Location = new System.Drawing.Point(460, 296);
            this.butQuit.Name = "butQuit";
            this.butQuit.Size = new System.Drawing.Size(78, 28);
            this.butQuit.TabIndex = 1;
            this.butQuit.Text = "取消(&Q)";
            this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
            // 
            // butSure
            // 
            this.butSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSure.Location = new System.Drawing.Point(376, 296);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(78, 28);
            this.butSure.TabIndex = 2;
            this.butSure.Text = "确定(&S)";
            this.butSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 53);
            this.panel1.TabIndex = 3;
            // 
            // myTabPageControl1
            // 
            this.myTabPageControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.myTabPageControl1.Appearance.Options.UseForeColor = true;
            this.myTabPageControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myTabPageControl1.Location = new System.Drawing.Point(0, 53);
            this.myTabPageControl1.Name = "myTabPageControl1";
            this.myTabPageControl1.SelectedTabPage = this.xtraTabPage1;
            this.myTabPageControl1.Size = new System.Drawing.Size(544, 243);
            this.myTabPageControl1.TabIndex = 4;
            this.myTabPageControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.txtOpRemark);
            this.xtraTabPage1.Controls.Add(this.dTimeOpDate);
            this.xtraTabPage1.Controls.Add(this.txtUserName);
            this.xtraTabPage1.Controls.Add(this.label3);
            this.xtraTabPage1.Controls.Add(this.label2);
            this.xtraTabPage1.Controls.Add(this.label1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(535, 211);
            this.xtraTabPage1.Text = "编辑数据";
            // 
            // txtOpRemark
            // 
            this.txtOpRemark.Location = new System.Drawing.Point(136, 76);
            this.txtOpRemark.MaxLength = 500;
            this.txtOpRemark.Multiline = true;
            this.txtOpRemark.Name = "txtOpRemark";
            this.txtOpRemark.Size = new System.Drawing.Size(393, 133);
            this.txtOpRemark.TabIndex = 5;
            // 
            // dTimeOpDate
            // 
            this.dTimeOpDate.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dTimeOpDate.Enabled = false;
            this.dTimeOpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTimeOpDate.Location = new System.Drawing.Point(133, 40);
            this.dTimeOpDate.Name = "dTimeOpDate";
            this.dTimeOpDate.Size = new System.Drawing.Size(160, 22);
            this.dTimeOpDate.TabIndex = 4;
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(133, 8);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(160, 22);
            this.txtUserName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "操作描述：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "操作日期:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "操作人：";
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.ForeColor = System.Drawing.Color.Maroon;
            this.labTitle.Location = new System.Drawing.Point(18, 12);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(55, 14);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "操作类型";
            // 
            // frmEditDocStateTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 332);
            this.Controls.Add(this.myTabPageControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.butSure);
            this.Controls.Add(this.butQuit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditDocStateTrace";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请输入单据状态改变的相应信息";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myTabPageControl1)).EndInit();
            this.myTabPageControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MB.WinBase.Ctls.MyButton butQuit;
        private MB.WinBase.Ctls.MyButton butSure;
        private System.Windows.Forms.Panel panel1;
        private MB.WinBase.Ctls.MyTabPageControl myTabPageControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.TextBox txtOpRemark;
        private System.Windows.Forms.DateTimePicker dTimeOpDate;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labTitle;
    }
}