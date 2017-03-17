namespace WinTestProject {
    partial class frmTestStyleImage {
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
            this.butGetStyleImage = new System.Windows.Forms.Button();
            this.butSaveStyleImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butGetStyleImage
            // 
            this.butGetStyleImage.Location = new System.Drawing.Point(12, 36);
            this.butGetStyleImage.Name = "butGetStyleImage";
            this.butGetStyleImage.Size = new System.Drawing.Size(141, 33);
            this.butGetStyleImage.TabIndex = 0;
            this.butGetStyleImage.Text = "获取硬盘款式图";
            this.butGetStyleImage.UseVisualStyleBackColor = true;
            this.butGetStyleImage.Click += new System.EventHandler(this.butGetStyleImage_Click);
            // 
            // butSaveStyleImage
            // 
            this.butSaveStyleImage.Location = new System.Drawing.Point(233, 37);
            this.butSaveStyleImage.Name = "butSaveStyleImage";
            this.butSaveStyleImage.Size = new System.Drawing.Size(145, 31);
            this.butSaveStyleImage.TabIndex = 1;
            this.butSaveStyleImage.Text = "保存图片到本地硬盘";
            this.butSaveStyleImage.UseVisualStyleBackColor = true;
            // 
            // frmTestStyleImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 230);
            this.Controls.Add(this.butSaveStyleImage);
            this.Controls.Add(this.butGetStyleImage);
            this.Name = "frmTestStyleImage";
            this.Text = "frmTestStyleImage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butGetStyleImage;
        private System.Windows.Forms.Button butSaveStyleImage;
    }
}