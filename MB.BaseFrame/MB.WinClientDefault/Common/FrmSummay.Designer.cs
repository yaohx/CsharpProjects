namespace MB.WinClientDefault.Common {
    partial class FrmSummay {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSummay));
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.cbSummaryLevel = new System.Windows.Forms.ComboBox();
            this.lblSummaryItem = new System.Windows.Forms.Label();
            this.gridSummary = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridViewSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.btnInit);
            this.panelControl.Controls.Add(this.btnClose);
            this.panelControl.Controls.Add(this.cbSummaryLevel);
            this.panelControl.Controls.Add(this.lblSummaryItem);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 351);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(655, 52);
            this.panelControl.TabIndex = 0;
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(462, 17);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 3;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Visible = false;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 0;
            this.btnClose.ImageList = this.imageList;
            this.btnClose.Location = new System.Drawing.Point(565, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(62, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Ribbon_Exit_16x16.png");
            // 
            // cbSummaryLevel
            // 
            this.cbSummaryLevel.FormattingEnabled = true;
            this.cbSummaryLevel.Location = new System.Drawing.Point(73, 17);
            this.cbSummaryLevel.Name = "cbSummaryLevel";
            this.cbSummaryLevel.Size = new System.Drawing.Size(121, 22);
            this.cbSummaryLevel.TabIndex = 1;
            this.cbSummaryLevel.SelectedValueChanged += new System.EventHandler(this.cbSummaryLevel_SelectedValueChanged);
            // 
            // lblSummaryItem
            // 
            this.lblSummaryItem.AutoSize = true;
            this.lblSummaryItem.Location = new System.Drawing.Point(12, 20);
            this.lblSummaryItem.Name = "lblSummaryItem";
            this.lblSummaryItem.Size = new System.Drawing.Size(55, 14);
            this.lblSummaryItem.TabIndex = 0;
            this.lblSummaryItem.Text = "汇总级别";
            // 
            // gridSummary
            // 
            this.gridSummary.DataIOControl = null;
            this.gridSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSummary.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.gridSummary.Location = new System.Drawing.Point(0, 0);
            this.gridSummary.MainView = this.gridViewSummary;
            this.gridSummary.Name = "gridSummary";
            this.gridSummary.ShowOptionMenu = false;
            this.gridSummary.Size = new System.Drawing.Size(655, 351);
            this.gridSummary.TabIndex = 1;
            this.gridSummary.ValidedDeleteKeyDown = false;
            this.gridSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSummary});
            // 
            // gridViewSummary
            // 
            this.gridViewSummary.GridControl = this.gridSummary;
            this.gridViewSummary.Name = "gridViewSummary";
            // 
            // FrmSummay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 403);
            this.Controls.Add(this.gridSummary);
            this.Controls.Add(this.panelControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSummay";
            this.Text = "分级查询汇总";
            this.Load += new System.EventHandler(this.FrmSummay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        private System.Windows.Forms.Label lblSummaryItem;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.ComboBox cbSummaryLevel;
        private System.Windows.Forms.ImageList imageList;
        private XWinLib.XtraGrid.GridControlEx gridSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSummary;
        private System.Windows.Forms.Button btnInit;
    }
}