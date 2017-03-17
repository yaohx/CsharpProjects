namespace MB.WinClientDefault.DynamicGroup {
    partial class ucDynamicGroupResult {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gridResult = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridViewResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // gridResult
            // 
            this.gridResult.DataIOControl = null;
            this.gridResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResult.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.gridResult.Location = new System.Drawing.Point(0, 0);
            this.gridResult.MainView = this.gridViewResult;
            this.gridResult.Name = "gridResult";
            this.gridResult.ShowOptionMenu = false;
            this.gridResult.Size = new System.Drawing.Size(777, 472);
            this.gridResult.TabIndex = 6;
            this.gridResult.ValidedDeleteKeyDown = false;
            this.gridResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewResult});
            // 
            // gridViewResult
            // 
            this.gridViewResult.GridControl = this.gridResult;
            this.gridViewResult.Name = "gridViewResult";
            // 
            // ucDynamicGroupResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridResult);
            this.Name = "ucDynamicGroupResult";
            this.Size = new System.Drawing.Size(777, 472);
            this.Load += new System.EventHandler(this.ucDynamicGroupResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XWinLib.XtraGrid.GridControlEx gridResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewResult;
    }
}
