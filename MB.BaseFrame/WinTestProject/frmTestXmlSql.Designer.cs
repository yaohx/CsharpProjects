namespace WinTestProject {
    partial class frmTestXmlSql {
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
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ucClickButtonInput1 = new MB.WinBase.Ctls.ucClickButtonInput();
            this.ucClickButtonInput2 = new MB.WinBase.Ctls.ucClickButtonInput();
            this.ucClickButtonInput3 = new MB.WinBase.Ctls.ucClickButtonInput();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ucClickButtonInput1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ucClickButtonInput2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ucClickButtonInput3, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(59, 223);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(366, 108);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ucClickButtonInput1
            // 
            this.ucClickButtonInput1.AllowInput = true;
            this.ucClickButtonInput1.BackColor = System.Drawing.Color.Transparent;
            this.ucClickButtonInput1.HideFilterPane = false;
            this.ucClickButtonInput1.Location = new System.Drawing.Point(186, 3);
            this.ucClickButtonInput1.MultiSelect = false;
            this.ucClickButtonInput1.Name = "ucClickButtonInput1";
            this.ucClickButtonInput1.ReadOnly = false;
            this.ucClickButtonInput1.Size = new System.Drawing.Size(73, 21);
            this.ucClickButtonInput1.TabIndex = 0;
            // 
            // ucClickButtonInput2
            // 
            this.ucClickButtonInput2.AllowInput = true;
            this.ucClickButtonInput2.BackColor = System.Drawing.Color.Transparent;
            this.ucClickButtonInput2.HideFilterPane = false;
            this.ucClickButtonInput2.Location = new System.Drawing.Point(186, 37);
            this.ucClickButtonInput2.MultiSelect = false;
            this.ucClickButtonInput2.Name = "ucClickButtonInput2";
            this.ucClickButtonInput2.ReadOnly = false;
            this.ucClickButtonInput2.Size = new System.Drawing.Size(83, 21);
            this.ucClickButtonInput2.TabIndex = 1;
            // 
            // ucClickButtonInput3
            // 
            this.ucClickButtonInput3.AllowInput = true;
            this.ucClickButtonInput3.BackColor = System.Drawing.Color.Transparent;
            this.ucClickButtonInput3.HideFilterPane = false;
            this.ucClickButtonInput3.Location = new System.Drawing.Point(186, 71);
            this.ucClickButtonInput3.MultiSelect = false;
            this.ucClickButtonInput3.Name = "ucClickButtonInput3";
            this.ucClickButtonInput3.ReadOnly = false;
            this.ucClickButtonInput3.Size = new System.Drawing.Size(81, 21);
            this.ucClickButtonInput3.TabIndex = 2;
            // 
            // frmTestXmlSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 410);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Name = "frmTestXmlSql";
            this.Text = "frmTestXmlSql";
            this.Load += new System.EventHandler(this.frmTestXmlSql_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MB.WinBase.Ctls.ucClickButtonInput ucClickButtonInput1;
        private MB.WinBase.Ctls.ucClickButtonInput ucClickButtonInput2;
        private MB.WinBase.Ctls.ucClickButtonInput ucClickButtonInput3;
    }
}