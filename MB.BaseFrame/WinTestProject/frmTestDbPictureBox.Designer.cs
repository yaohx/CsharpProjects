namespace WinTestProject
{
    partial class frmTestDbPictureBox
    {
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
            this.ucDBPictureBox1 = new MB.WinBase.Ctls.ucDbPictureBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucDBPictureBox1
            // 
            this.ucDBPictureBox1.BackColor = System.Drawing.Color.White;
            this.ucDBPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDBPictureBox1.Image = null;
            this.ucDBPictureBox1.ImageData = null;
            this.ucDBPictureBox1.Location = new System.Drawing.Point(12, 12);
            this.ucDBPictureBox1.Name = "ucDBPictureBox1";
            this.ucDBPictureBox1.ReadOnly = false;
            this.ucDBPictureBox1.Size = new System.Drawing.Size(323, 165);
            this.ucDBPictureBox1.SizeModel = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ucDBPictureBox1.TabIndex = 0;
            // 
            // frmTestDbPictureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 248);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucDBPictureBox1);
            this.Name = "frmTestDbPictureBox";
            this.Text = "frmTestDbPictureBox";
            this.ResumeLayout(false);

        }

        #endregion

        private MB.WinBase.Ctls.ucDbPictureBox ucDBPictureBox1;
        private System.Windows.Forms.Button button1;
    }
}