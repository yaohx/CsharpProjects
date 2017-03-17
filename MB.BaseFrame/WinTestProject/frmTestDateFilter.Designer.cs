namespace WinTestProject
{
    partial class frmTestDateFilter
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
            this.ucEditDateFilter1 = new MB.WinBase.Ctls.ucEditDateFilter();
            this.SuspendLayout();
            // 
            // ucEditDateFilter1
            // 
            this.ucEditDateFilter1.BackColor = System.Drawing.Color.Transparent;
            this.ucEditDateFilter1.DateFilterType = MB.WinBase.Ctls.ucEditDateFilter.DateFilterEditType.Today;
            this.ucEditDateFilter1.Location = new System.Drawing.Point(25, 12);
            this.ucEditDateFilter1.Name = "ucEditDateFilter1";
            this.ucEditDateFilter1.Size = new System.Drawing.Size(280, 22);
            this.ucEditDateFilter1.TabIndex = 0;
            // 
            // frmTestDateFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 184);
            this.Controls.Add(this.ucEditDateFilter1);
            this.Name = "frmTestDateFilter";
            this.Text = "frmTestDateFilter";
            this.ResumeLayout(false);

        }

        #endregion

        private MB.WinBase.Ctls.ucEditDateFilter ucEditDateFilter1;
    }
}