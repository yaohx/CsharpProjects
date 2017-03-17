namespace MB.WinClientDefault.Common {
    partial class FrmQuickCellDataInput {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuickCellDataInput));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tButQuit = new System.Windows.Forms.ToolStripButton();
            this.grdCtlMain = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tButQuit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(679, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tButQuit
            // 
            this.tButQuit.Image = ((System.Drawing.Image)(resources.GetObject("tButQuit.Image")));
            this.tButQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tButQuit.Name = "tButQuit";
            this.tButQuit.Size = new System.Drawing.Size(67, 22);
            this.tButQuit.Text = "关闭(&Q)";
            this.tButQuit.Click += new System.EventHandler(this.tButQuit_Click);
            // 
            // grdCtlMain
            // 
            this.grdCtlMain.DataIOControl = null;
            this.grdCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCtlMain.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.grdCtlMain.Location = new System.Drawing.Point(0, 25);
            this.grdCtlMain.MainView = this.gridViewMain;
            this.grdCtlMain.Name = "grdCtlMain";
            this.grdCtlMain.ShowOptionMenu = false;
            this.grdCtlMain.Size = new System.Drawing.Size(679, 413);
            this.grdCtlMain.TabIndex = 3;
            this.grdCtlMain.ValidedDeleteKeyDown = false;
            this.grdCtlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // gridViewMain
            // 
            this.gridViewMain.GridControl = this.grdCtlMain;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsView.ColumnAutoWidth = false;
            this.gridViewMain.OptionsView.ShowFooter = true;
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            // 
            // FrmQuickCellDataInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 438);
            this.Controls.Add(this.grdCtlMain);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQuickCellDataInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "可编辑网格快速编辑...";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tButQuit;
        private MB.XWinLib.XtraGrid.GridControlEx grdCtlMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
    }
}