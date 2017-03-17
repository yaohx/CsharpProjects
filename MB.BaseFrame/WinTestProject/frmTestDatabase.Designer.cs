namespace WinTestProject {
    partial class frmTestDatabase {
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
            MB.WinBase.Binding.DesignColumnXmlCfgInfo designColumnXmlCfgInfo1 = new MB.WinBase.Binding.DesignColumnXmlCfgInfo();
            MB.WinBase.Binding.DesignColumnXmlCfgInfo designColumnXmlCfgInfo2 = new MB.WinBase.Binding.DesignColumnXmlCfgInfo();
            MB.WinBase.Binding.DesignColumnXmlCfgInfo designColumnXmlCfgInfo3 = new MB.WinBase.Binding.DesignColumnXmlCfgInfo();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.ucClickButtonInput1 = new MB.WinBase.Ctls.ucClickButtonInput();
            this.myTableLayoutPanelProvider1 = new MB.WinBase.Design.MyTableLayoutPanelProvider(this.components);
            this.myDataBindingProvider1 = new MB.WinBase.Binding.MyDataBindingProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(295, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(552, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(171, 50);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 284);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(195, 40);
            this.button4.TabIndex = 3;
            this.button4.Text = "测试OracleDataAccess";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(225, 285);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(228, 39);
            this.button5.TabIndex = 4;
            this.button5.Text = "测试Oracle模拟";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(11, 234);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(186, 44);
            this.button6.TabIndex = 5;
            this.button6.Text = "测试Enterprise4";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dateTimePicker1
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.dateTimePicker1, null);
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(225, 244);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(178, 21);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.myTableLayoutPanelProvider1.SetAutoLayoutPanel(this.tableLayoutPanel1, true);
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker2, 1, 2);
            this.myTableLayoutPanelProvider1.SetIsCaption(this.tableLayoutPanel1, false);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 81);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(730, 129);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // comboBox1
            // 
            designColumnXmlCfgInfo1.ColumnDescription = "名称";
            designColumnXmlCfgInfo1.ColumnName = "Name";
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.comboBox1, designColumnXmlCfgInfo1);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(144, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(178, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "@名称";
            // 
            // textBox1
            // 
            designColumnXmlCfgInfo2.ColumnDescription = "编码";
            designColumnXmlCfgInfo2.ColumnName = "Code";
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.textBox1, designColumnXmlCfgInfo2);
            this.textBox1.Location = new System.Drawing.Point(144, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "@编码";
            // 
            // dateTimePicker2
            // 
            designColumnXmlCfgInfo3.ColumnDescription = "创建日期";
            designColumnXmlCfgInfo3.ColumnName = "CreateDate";
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.dateTimePicker2, designColumnXmlCfgInfo3);
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dateTimePicker2.Location = new System.Drawing.Point(144, 55);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(178, 21);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // textBox2
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.textBox2, null);
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox2.Location = new System.Drawing.Point(481, 225);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(183, 21);
            this.textBox2.TabIndex = 9;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(499, 277);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(220, 36);
            this.button7.TabIndex = 10;
            this.button7.Text = "测试SQL 参数连接";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // ucClickButtonInput1
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.ucClickButtonInput1, null);
            this.ucClickButtonInput1.AllowInput = true;
            this.ucClickButtonInput1.BackColor = System.Drawing.Color.Transparent;
            this.ucClickButtonInput1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ucClickButtonInput1.HideFilterPane = false;
            this.ucClickButtonInput1.Location = new System.Drawing.Point(225, 217);
            this.ucClickButtonInput1.MultiSelect = false;
            this.ucClickButtonInput1.Name = "ucClickButtonInput1";
            this.ucClickButtonInput1.ReadOnly = false;
            this.ucClickButtonInput1.Size = new System.Drawing.Size(172, 21);
            this.ucClickButtonInput1.TabIndex = 8;
            // 
            // myDataBindingProvider1
            // 
            this.myDataBindingProvider1.XmlConfigFile = null;
            // 
            // frmTestDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 437);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.ucClickButtonInput1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmTestDatabase";
            this.Text = "frmTestDatabase";
            this.Load += new System.EventHandler(this.frmTestDatabase_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private MB.WinBase.Binding.MyDataBindingProvider myDataBindingProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MB.WinBase.Design.MyTableLayoutPanelProvider myTableLayoutPanelProvider1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private MB.WinBase.Ctls.ucClickButtonInput ucClickButtonInput1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button7;
    }
}