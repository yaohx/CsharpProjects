namespace WinTestProject {
    partial class frmTestDataSerializer {
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
            this.butSerializer = new System.Windows.Forms.Button();
            this.butDeSerializer = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butSerializer
            // 
            this.butSerializer.Location = new System.Drawing.Point(12, 12);
            this.butSerializer.Name = "butSerializer";
            this.butSerializer.Size = new System.Drawing.Size(159, 34);
            this.butSerializer.TabIndex = 0;
            this.butSerializer.Text = "系列化";
            this.butSerializer.UseVisualStyleBackColor = true;
            this.butSerializer.Click += new System.EventHandler(this.butSerializer_Click);
            // 
            // butDeSerializer
            // 
            this.butDeSerializer.Location = new System.Drawing.Point(188, 12);
            this.butDeSerializer.Name = "butDeSerializer";
            this.butDeSerializer.Size = new System.Drawing.Size(154, 34);
            this.butDeSerializer.TabIndex = 1;
            this.butDeSerializer.Text = "反系列化";
            this.butDeSerializer.UseVisualStyleBackColor = true;
            this.butDeSerializer.Click += new System.EventHandler(this.butDeSerializer_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "测试数据反序列化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTestDataSerializer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 329);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.butDeSerializer);
            this.Controls.Add(this.butSerializer);
            this.Name = "frmTestDataSerializer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系列化测试";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butSerializer;
        private System.Windows.Forms.Button butDeSerializer;
        private System.Windows.Forms.Button button1;
    }
}