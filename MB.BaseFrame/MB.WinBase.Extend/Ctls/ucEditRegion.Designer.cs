namespace MB.WinBase.Extend.Ctls
{
    partial class ucEditRegion
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.bEditRegion = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bEditRegion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bEditRegion
            // 
            this.bEditRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bEditRegion.Location = new System.Drawing.Point(0, 0);
            this.bEditRegion.Name = "bEditRegion";
            this.bEditRegion.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.bEditRegion.Properties.Appearance.Options.UseBackColor = true;
            this.bEditRegion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down, "", 15, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.bEditRegion.Properties.ReadOnly = true;
            this.bEditRegion.Size = new System.Drawing.Size(220, 21);
            this.bEditRegion.TabIndex = 0;
            // 
            // ucEditRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bEditRegion);
            this.Name = "ucEditRegion";
            this.Size = new System.Drawing.Size(220, 21);
            ((System.ComponentModel.ISupportInitialize)(this.bEditRegion.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit bEditRegion;


    }
}
