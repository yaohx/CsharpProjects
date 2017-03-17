namespace MB.Tools.LogView {
    partial class frmLogAnalyze {
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.butQAMiddleError = new System.Windows.Forms.Button();
            this.butQAMiddleLayer = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.butQAMiddleError);
            this.panel1.Controls.Add(this.butQAMiddleLayer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 48);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(396, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "分析所有用户数";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(254, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "分析同时合同提交数";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // butQAMiddleError
            // 
            this.butQAMiddleError.Location = new System.Drawing.Point(131, 2);
            this.butQAMiddleError.Name = "butQAMiddleError";
            this.butQAMiddleError.Size = new System.Drawing.Size(117, 32);
            this.butQAMiddleError.TabIndex = 1;
            this.butQAMiddleError.Text = "分析出错率";
            this.butQAMiddleError.UseVisualStyleBackColor = true;
            this.butQAMiddleError.Click += new System.EventHandler(this.butQAMiddleError_Click);
            // 
            // butQAMiddleLayer
            // 
            this.butQAMiddleLayer.Location = new System.Drawing.Point(3, 3);
            this.butQAMiddleLayer.Name = "butQAMiddleLayer";
            this.butQAMiddleLayer.Size = new System.Drawing.Size(122, 32);
            this.butQAMiddleLayer.TabIndex = 0;
            this.butQAMiddleLayer.Text = "分析中间层并发数";
            this.butQAMiddleLayer.UseVisualStyleBackColor = true;
            this.butQAMiddleLayer.Click += new System.EventHandler(this.butQAMiddleLayer_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 48);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(738, 343);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // frmLogAnalyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 391);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Name = "frmLogAnalyze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日志分析工具...";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butQAMiddleLayer;
        private System.Windows.Forms.Button butQAMiddleError;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}