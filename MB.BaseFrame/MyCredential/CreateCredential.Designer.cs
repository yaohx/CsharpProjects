namespace MyCredential {
    partial class CreateCredential {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateCredential));
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.butSure = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDominUser = new System.Windows.Forms.CheckBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cobHostType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBindingType = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFormatString = new System.Windows.Forms.TextBox();
            this.chkReplaceLastDot = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridDetail = new System.Windows.Forms.DataGridView();
            this.colCfgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDomain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoginPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(105, 53);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(165, 21);
            this.txtServerIP.TabIndex = 0;
            this.txtServerIP.TextChanged += new System.EventHandler(this.txtServerIP_TextChanged);
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(104, 81);
            this.numPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(65, 21);
            this.numPort.TabIndex = 1;
            this.numPort.Value = new decimal(new int[] {
            18051,
            0,
            0,
            0});
            this.numPort.ValueChanged += new System.EventHandler(this.numPort_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "端口号：";
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(589, 7);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(74, 26);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "关闭(&Q)";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butSure
            // 
            this.butSure.Location = new System.Drawing.Point(508, 7);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(76, 26);
            this.butSure.TabIndex = 0;
            this.butSure.Text = "创建(&G)";
            this.butSure.UseVisualStyleBackColor = true;
            this.butSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "服务器地址：";
            // 
            // txtPassword2
            // 
            this.txtPassword2.Location = new System.Drawing.Point(102, 122);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.PasswordChar = '*';
            this.txtPassword2.Size = new System.Drawing.Size(156, 21);
            this.txtPassword2.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(11, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "密码确认：";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.butSure);
            this.panel1.Controls.Add(this.butCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 42);
            this.panel1.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(11, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "登录用户名称：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(102, 68);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(156, 21);
            this.txtUserName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDominUser);
            this.groupBox1.Controls.Add(this.txtDomain);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPassword2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Location = new System.Drawing.Point(406, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 154);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "必须是有效的该机器Windows登录密码：";
            // 
            // chkDominUser
            // 
            this.chkDominUser.AutoSize = true;
            this.chkDominUser.Location = new System.Drawing.Point(102, 18);
            this.chkDominUser.Name = "chkDominUser";
            this.chkDominUser.Size = new System.Drawing.Size(96, 16);
            this.chkDominUser.TabIndex = 16;
            this.chkDominUser.Text = "用域用户连接";
            this.chkDominUser.UseVisualStyleBackColor = true;
            this.chkDominUser.CheckedChanged += new System.EventHandler(this.chkDominUser_CheckedChanged);
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(103, 39);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.ReadOnly = true;
            this.txtDomain.Size = new System.Drawing.Size(155, 21);
            this.txtDomain.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "域：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(11, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "登录密码：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(102, 95);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(156, 21);
            this.txtPassword.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 28;
            this.label6.Text = "服务承载类型：";
            // 
            // cobHostType
            // 
            this.cobHostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobHostType.Enabled = false;
            this.cobHostType.FormattingEnabled = true;
            this.cobHostType.Location = new System.Drawing.Point(104, 113);
            this.cobHostType.Name = "cobHostType";
            this.cobHostType.Size = new System.Drawing.Size(104, 20);
            this.cobHostType.TabIndex = 2;
            this.cobHostType.SelectedIndexChanged += new System.EventHandler(this.cobHostType_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 40);
            this.panel2.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(12, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(329, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "不建议用管理员打证书，请在服务器上新创建用户来创建证书";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "WCF 绑定类型：";
            // 
            // txtBindingType
            // 
            this.txtBindingType.Enabled = false;
            this.txtBindingType.Location = new System.Drawing.Point(103, 139);
            this.txtBindingType.Name = "txtBindingType";
            this.txtBindingType.Size = new System.Drawing.Size(105, 21);
            this.txtBindingType.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "格式化字符窜：";
            // 
            // txtFormatString
            // 
            this.txtFormatString.Location = new System.Drawing.Point(103, 171);
            this.txtFormatString.Name = "txtFormatString";
            this.txtFormatString.Size = new System.Drawing.Size(297, 21);
            this.txtFormatString.TabIndex = 35;
            // 
            // chkReplaceLastDot
            // 
            this.chkReplaceLastDot.AutoSize = true;
            this.chkReplaceLastDot.Checked = true;
            this.chkReplaceLastDot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReplaceLastDot.Enabled = false;
            this.chkReplaceLastDot.Location = new System.Drawing.Point(214, 141);
            this.chkReplaceLastDot.Name = "chkReplaceLastDot";
            this.chkReplaceLastDot.Size = new System.Drawing.Size(186, 16);
            this.chkReplaceLastDot.TabIndex = 36;
            this.chkReplaceLastDot.Text = "修改相对路径(最后 \',\'=>\'/\')";
            this.chkReplaceLastDot.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridDetail);
            this.groupBox2.Location = new System.Drawing.Point(6, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(664, 210);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "关联应用服务连接信息：";
            // 
            // dataGridDetail
            // 
            this.dataGridDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCfgName,
            this.colUrl,
            this.colDomain,
            this.colLoginName,
            this.colLoginPassword});
            this.dataGridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridDetail.Location = new System.Drawing.Point(3, 17);
            this.dataGridDetail.Name = "dataGridDetail";
            this.dataGridDetail.RowTemplate.Height = 23;
            this.dataGridDetail.Size = new System.Drawing.Size(658, 190);
            this.dataGridDetail.TabIndex = 0;
            // 
            // colCfgName
            // 
            this.colCfgName.DataPropertyName = "CfgName";
            this.colCfgName.HeaderText = "系统编码";
            this.colCfgName.Name = "colCfgName";
            this.colCfgName.Width = 80;
            // 
            // colUrl
            // 
            this.colUrl.DataPropertyName = "Url";
            this.colUrl.HeaderText = "URL格式化字符窜";
            this.colUrl.Name = "colUrl";
            this.colUrl.Width = 260;
            // 
            // colDomain
            // 
            this.colDomain.DataPropertyName = "Domain";
            this.colDomain.HeaderText = "域";
            this.colDomain.Name = "colDomain";
            this.colDomain.Width = 80;
            // 
            // colLoginName
            // 
            this.colLoginName.DataPropertyName = "LoginName";
            this.colLoginName.HeaderText = "登录用户";
            this.colLoginName.Name = "colLoginName";
            // 
            // colLoginPassword
            // 
            this.colLoginPassword.DataPropertyName = "LoginPassword";
            this.colLoginPassword.HeaderText = "登录密码";
            this.colLoginPassword.Name = "colLoginPassword";
            // 
            // CreateCredential
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(674, 459);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkReplaceLastDot);
            this.Controls.Add(this.txtFormatString);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtBindingType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cobHostType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.numPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateCredential";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WCF 服务证书创建";
            this.Load += new System.EventHandler(this.CreateWindowsCredential_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butSure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cobHostType;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkDominUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBindingType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFormatString;
        private System.Windows.Forms.CheckBox chkReplaceLastDot;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCfgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDomain;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoginName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoginPassword;
    }
}