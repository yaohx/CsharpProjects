namespace WinTestProject {
    partial class testBandGridView {
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
            this.gridControlEx1 = new MB.XWinLib.XtraGrid.GridControlEx();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEx1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlEx1
            // 
            this.gridControlEx1.DataIOControl = null;
            this.gridControlEx1.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.gridControlEx1.Location = new System.Drawing.Point(68, 12);
            this.gridControlEx1.MainView = this.advBandedGridView1;
            this.gridControlEx1.Name = "gridControlEx1";
            this.gridControlEx1.ShowOptionMenu = false;
            this.gridControlEx1.Size = new System.Drawing.Size(713, 307);
            this.gridControlEx1.TabIndex = 0;
            this.gridControlEx1.ValidedDeleteKeyDown = false;
            this.gridControlEx1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1});
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.GridControl = this.gridControlEx1;
            this.advBandedGridView1.Name = "advBandedGridView1";
            // 
            // testBandGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 366);
            this.Controls.Add(this.gridControlEx1);
            this.Name = "testBandGridView";
            this.Text = "testBandGridView";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEx1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MB.XWinLib.XtraGrid.GridControlEx gridControlEx1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;


    }
}