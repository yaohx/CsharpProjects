namespace MB.WinClientDefault.QueryFilter {
    partial class ucDataCheckListView {
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
            this.grdCtlMain = new DevExpress.XtraGrid.GridControl();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCtlMain
            // 
            this.grdCtlMain.Location = new System.Drawing.Point(46, 42);
            this.grdCtlMain.MainView = this.gridViewMain;
            this.grdCtlMain.Name = "grdCtlMain";
            this.grdCtlMain.ServerMode = true;
            this.grdCtlMain.Size = new System.Drawing.Size(408, 255);
            this.grdCtlMain.TabIndex = 0;
            this.grdCtlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            this.grdCtlMain.DoubleClick += new System.EventHandler(this.grdCtlMain_DoubleClick);
            this.grdCtlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grdCtlMain_MouseMove);
            // 
            // gridViewMain
            // 
            this.gridViewMain.GridControl = this.grdCtlMain;
            this.gridViewMain.IndicatorWidth = 50;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            this.gridViewMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewMain_CustomDrawRowIndicator);
            this.gridViewMain.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewMain_CellValueChanging);
            // 
            // ucDataCheckListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.grdCtlMain);
            this.Name = "ucDataCheckListView";
            this.Size = new System.Drawing.Size(531, 358);
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdCtlMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
    }
}
