namespace MB.WinBase.Ctls {
    partial class ucIamgeIcoEdit {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.picImageIco = new System.Windows.Forms.PictureBox();
            this.butSearchImage = new MB.WinBase.Ctls.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.picImageIco)).BeginInit();
            this.SuspendLayout();
            // 
            // picImageIco
            // 
            this.picImageIco.BackColor = System.Drawing.Color.White;
            this.picImageIco.Location = new System.Drawing.Point(3, 2);
            this.picImageIco.Name = "picImageIco";
            this.picImageIco.Size = new System.Drawing.Size(16, 15);
            this.picImageIco.TabIndex = 3;
            this.picImageIco.TabStop = false;
            // 
            // butSearchImage
            // 
            this.butSearchImage.Location = new System.Drawing.Point(21, 0);
            this.butSearchImage.Name = "butSearchImage";
            this.butSearchImage.Size = new System.Drawing.Size(21, 18);
            this.butSearchImage.TabIndex = 4;
            this.butSearchImage.Text = "...";
            this.butSearchImage.Click += new System.EventHandler(this.butSearchImage_Click);
            // 
            // ucIamgeIcoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.butSearchImage);
            this.Controls.Add(this.picImageIco);
            this.Name = "ucIamgeIcoEdit";
            this.Size = new System.Drawing.Size(136, 24);
            ((System.ComponentModel.ISupportInitialize)(this.picImageIco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picImageIco;
        private MyButton butSearchImage;
    }
}
