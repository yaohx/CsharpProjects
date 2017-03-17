namespace MB.WinClientDefault {
    partial class DefaultLoginForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultLoginForm));
            this.cobService = new System.Windows.Forms.ComboBox();
            this.cobUserName = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.myComboxRememberProvider1 = new MB.WinBase.Design.MyComboxRememberProvider(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.myButExit = new MB.WinBase.Ctls.MyButton();
            this.myButLogin = new MB.WinBase.Ctls.MyButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labVersion = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cobService
            // 
            this.myComboxRememberProvider1.SetComboxSaveMaxCount(this.cobService, 10);
            this.cobService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobService.FormattingEnabled = true;
            this.cobService.Location = new System.Drawing.Point(192, 106);
            this.cobService.Name = "cobService";
            this.cobService.Size = new System.Drawing.Size(174, 22);
            this.cobService.TabIndex = 0;
            // 
            // cobUserName
            // 
            this.myComboxRememberProvider1.SetComboxSaveMaxCount(this.cobUserName, 10);
            this.cobUserName.FormattingEnabled = true;
            this.cobUserName.Location = new System.Drawing.Point(192, 136);
            this.cobUserName.Name = "cobUserName";
            this.cobUserName.Size = new System.Drawing.Size(174, 22);
            this.cobUserName.TabIndex = 2;
            this.cobUserName.Leave += new System.EventHandler(this.cobUserName_Leave);
            // 
            // txtPassword
            // 
            this.txtPassword.AllowDrop = true;
            this.txtPassword.Location = new System.Drawing.Point(192, 167);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(174, 22);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, -14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(458, 278);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // myButExit
            // 
            this.myButExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.myButExit.Location = new System.Drawing.Point(289, 208);
            this.myButExit.Name = "myButExit";
            this.myButExit.Size = new System.Drawing.Size(77, 26);
            this.myButExit.TabIndex = 29;
            this.myButExit.Text = "退出(&E)";
            this.myButExit.Click += new System.EventHandler(this.butQuit_Click);
            // 
            // myButLogin
            // 
            this.myButLogin.Location = new System.Drawing.Point(192, 208);
            this.myButLogin.Name = "myButLogin";
            this.myButLogin.Size = new System.Drawing.Size(79, 26);
            this.myButLogin.TabIndex = 30;
            this.myButLogin.Text = "登录(&L)";
            this.myButLogin.Click += new System.EventHandler(this.butLogin_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(70, 288);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(232, 14);
            this.labelControl7.TabIndex = 34;
            this.labelControl7.Text = "美特斯邦威服饰股份有限公司-信息管理中心";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(4, 288);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(65, 14);
            this.labelControl6.TabIndex = 33;
            this.labelControl6.Text = "研发单位：";
            // 
            // labVersion
            // 
            this.labVersion.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labVersion.Appearance.Options.UseForeColor = true;
            this.labVersion.Location = new System.Drawing.Point(70, 268);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(114, 14);
            this.labVersion.TabIndex = 32;
            this.labVersion.Text = "R2 2009-12-14 0001";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(4, 268);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 14);
            this.labelControl4.TabIndex = 31;
            this.labelControl4.Text = "Version：";
            // 
            // DefaultLoginForm
            // 
            this.AcceptButton = this.myButLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.myButExit;
            this.ClientSize = new System.Drawing.Size(458, 308);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labVersion);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.myButLogin);
            this.Controls.Add(this.myButExit);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cobUserName);
            this.Controls.Add(this.cobService);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DefaultLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cobUserName;
        private System.Windows.Forms.ComboBox cobService;
        private MB.WinBase.Design.MyComboxRememberProvider myComboxRememberProvider1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MB.WinBase.Ctls.MyButton myButExit;
        private MB.WinBase.Ctls.MyButton myButLogin;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labVersion;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}