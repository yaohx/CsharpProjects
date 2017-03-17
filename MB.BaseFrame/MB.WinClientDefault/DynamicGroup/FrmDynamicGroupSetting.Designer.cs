namespace MB.WinClientDefault.DynamicGroup {
    partial class FrmDynamicGroupSetting {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDynamicGroupSetting));
            this.imageLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.gridControl = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panFunction = new System.Windows.Forms.Panel();
            this.panelFunBtns = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSure = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.panFunction.SuspendLayout();
            this.panelFunBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageLarge
            // 
            this.imageLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.imageLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageLarge.ImageStream")));
            this.imageLarge.Images.SetKeyName(0, "AddNew.ico");
            this.imageLarge.Images.SetKeyName(1, "Close.ico");
            this.imageLarge.Images.SetKeyName(2, "Function.ico");
            this.imageLarge.Images.SetKeyName(3, "Query.ico");
            this.imageLarge.Images.SetKeyName(4, "Submit.ICO");
            this.imageLarge.Images.SetKeyName(5, "Save.ico");
            this.imageLarge.Images.SetKeyName(6, "Close.ico");
            this.imageLarge.Images.SetKeyName(15, "12930B92340-34K6.png");
            this.imageLarge.Images.SetKeyName(16, "2012080809432424_easyicon_cn_48.ico");
            this.imageLarge.Images.SetKeyName(17, "XtraCharts.png");
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            this.imageSmall.Images.SetKeyName(0, "AddNew.ico");
            this.imageSmall.Images.SetKeyName(1, "AdvanceFilter.ico");
            this.imageSmall.Images.SetKeyName(2, "Close.ico");
            this.imageSmall.Images.SetKeyName(3, "Delete.ico");
            this.imageSmall.Images.SetKeyName(4, "Function.ico");
            this.imageSmall.Images.SetKeyName(5, "ModelQuery.ICO");
            this.imageSmall.Images.SetKeyName(6, "Open.ICO");
            this.imageSmall.Images.SetKeyName(7, "Print.ico");
            this.imageSmall.Images.SetKeyName(8, "Query.ico");
            this.imageSmall.Images.SetKeyName(9, "Refresh.ico");
            this.imageSmall.Images.SetKeyName(10, "Save.ico");
            this.imageSmall.Images.SetKeyName(11, "Submit.ICO");
            this.imageSmall.Images.SetKeyName(12, "导出.ICO");
            this.imageSmall.Images.SetKeyName(13, "导入.ico");
            this.imageSmall.Images.SetKeyName(14, "Calculator.ico");
            this.imageSmall.Images.SetKeyName(15, "Message.ICO");
            this.imageSmall.Images.SetKeyName(40, "12930B92340-34K6.png");
            this.imageSmall.Images.SetKeyName(41, "2012080809432424_easyicon_cn_48.ico");
            this.imageSmall.Images.SetKeyName(42, "XtraCharts.png");
            this.imageSmall.Images.SetKeyName(43, "Aggregation.ico");
            // 
            // gridControl
            // 
            this.gridControl.DataIOControl = null;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.ShowOptionMenu = false;
            this.gridControl.Size = new System.Drawing.Size(645, 382);
            this.gridControl.TabIndex = 3;
            this.gridControl.ValidedDeleteKeyDown = false;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView_ShowingEditor);
            // 
            // panFunction
            // 
            this.panFunction.Controls.Add(this.panelFunBtns);
            this.panFunction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFunction.Location = new System.Drawing.Point(0, 382);
            this.panFunction.Name = "panFunction";
            this.panFunction.Size = new System.Drawing.Size(645, 29);
            this.panFunction.TabIndex = 4;
            // 
            // panelFunBtns
            // 
            this.panelFunBtns.Controls.Add(this.btnClear);
            this.panelFunBtns.Controls.Add(this.btnClose);
            this.panelFunBtns.Controls.Add(this.btnSure);
            this.panelFunBtns.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelFunBtns.Location = new System.Drawing.Point(332, 0);
            this.panelFunBtns.Name = "panelFunBtns";
            this.panelFunBtns.Size = new System.Drawing.Size(313, 29);
            this.panelFunBtns.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 22;
            this.btnClose.ImageList = this.imageSmall;
            this.btnClose.Location = new System.Drawing.Point(229, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭(&Q)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSure
            // 
            this.btnSure.ImageIndex = 21;
            this.btnSure.ImageList = this.imageSmall;
            this.btnSure.Location = new System.Drawing.Point(38, 3);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(73, 23);
            this.btnSure.TabIndex = 1;
            this.btnSure.Text = "保存(&S)";
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // btnClear
            // 
            this.btnClear.ImageIndex = 23;
            this.btnClear.ImageList = this.imageSmall;
            this.btnClear.Location = new System.Drawing.Point(134, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(73, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "清空(&C)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FrmDynamicGroupSetting
            // 
            this.AcceptButton = this.btnSure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(645, 411);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.panFunction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDynamicGroupSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "动态聚组列和条件设定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDynamicGroupSetting_FormClosing);
            this.Load += new System.EventHandler(this.XtraFrmDynamicGroupSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.panFunction.ResumeLayout(false);
            this.panelFunBtns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageLarge;
        private DevExpress.Utils.ImageCollection imageSmall;
        private XWinLib.XtraGrid.GridControlEx gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.Panel panFunction;
        private DevExpress.XtraEditors.SimpleButton btnSure;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.Panel panelFunBtns;
        private DevExpress.XtraEditors.SimpleButton btnClear;
    }
}