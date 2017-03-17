namespace MB.WinBase.Test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucDbPictureBox1 = new MB.WinBase.Ctls.ucDbPictureBox();
            this.SuspendLayout();
            // 
            // ucDbPictureBox1
            // 
            this.ucDbPictureBox1.BackColor = System.Drawing.Color.White;
            this.ucDbPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDbPictureBox1.Image = null;
            this.ucDbPictureBox1.ImageData = null;
            this.ucDbPictureBox1.ImageFileName = null;
            this.ucDbPictureBox1.Location = new System.Drawing.Point(12, 21);
            this.ucDbPictureBox1.Name = "ucDbPictureBox1";
            this.ucDbPictureBox1.ReadOnly = false;
            this.ucDbPictureBox1.Size = new System.Drawing.Size(260, 136);
            this.ucDbPictureBox1.SizeModel = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ucDbPictureBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.ucDbPictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Ctls.ucDbPictureBox ucDbPictureBox1;
    }
}