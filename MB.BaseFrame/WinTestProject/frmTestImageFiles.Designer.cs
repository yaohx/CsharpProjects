namespace WinTestProject {
    partial class frmTestImageFiles {
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
            this.ucImageFileList1 = new MB.WinClientDefault.Ctls.ucImageFileList();
            this.SuspendLayout();
            // 
            // ucImageFileList1
            // 
            this.ucImageFileList1.BackColor = System.Drawing.Color.Transparent;
            this.ucImageFileList1.DefaultImageHeight = 100;
            this.ucImageFileList1.DefaultImageWidth = 120;
            this.ucImageFileList1.ImageFieldName = null;
            this.ucImageFileList1.KeyFieldName = null;
            this.ucImageFileList1.Location = new System.Drawing.Point(30, 14);
            this.ucImageFileList1.Name = "ucImageFileList1";
            this.ucImageFileList1.RemarkFieldCaption = null;
            this.ucImageFileList1.RemarkFieldName = null;
            this.ucImageFileList1.Size = new System.Drawing.Size(494, 120);
            this.ucImageFileList1.TabIndex = 0;
            // 
            // frmTestImageFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 369);
            this.Controls.Add(this.ucImageFileList1);
            this.Name = "frmTestImageFiles";
            this.Text = "frmTestImageFiles";
            this.ResumeLayout(false);

        }

        #endregion

        private MB.WinClientDefault.Ctls.ucImageFileList ucImageFileList1;
    }
}