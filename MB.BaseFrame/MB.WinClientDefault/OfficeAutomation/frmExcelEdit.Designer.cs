namespace MB.WinClientDefault.OfficeAutomation {
    partial class frmExcelEdit {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExcelEdit));
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.panelLeftBottomBtns = new System.Windows.Forms.Panel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.excelViewer = new MB.WinOfficeAutomation.ExcelViewer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            this.panelLeftBottomBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.panelLeftBottomBtns);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 390);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(839, 46);
            this.panelControl.TabIndex = 1;
            // 
            // panelLeftBottomBtns
            // 
            this.panelLeftBottomBtns.Controls.Add(this.btnSave);
            this.panelLeftBottomBtns.Controls.Add(this.btnClose);
            this.panelLeftBottomBtns.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLeftBottomBtns.Location = new System.Drawing.Point(521, 2);
            this.panelLeftBottomBtns.Name = "panelLeftBottomBtns";
            this.panelLeftBottomBtns.Size = new System.Drawing.Size(316, 42);
            this.panelLeftBottomBtns.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 1;
            this.btnSave.ImageList = this.imageList;
            this.btnSave.Location = new System.Drawing.Point(83, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Ribbon_Exit_16x16.png");
            this.imageList.Images.SetKeyName(1, "Ribbon_Save_16x16.png");
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 0;
            this.btnClose.ImageList = this.imageList;
            this.btnClose.Location = new System.Drawing.Point(187, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // excelViewer
            // 
            this.excelViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.excelViewer.Location = new System.Drawing.Point(0, 0);
            this.excelViewer.Name = "excelViewer";
            this.excelViewer.Size = new System.Drawing.Size(839, 390);
            this.excelViewer.TabIndex = 2;
            this.excelViewer.Text = "excelViewer1";
            // 
            // frmExcelEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 436);
            this.Controls.Add(this.excelViewer);
            this.Controls.Add(this.panelControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExcelEdit";
            this.Text = "EXCEL编辑窗口";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelLeftBottomBtns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private WinOfficeAutomation.ExcelViewer excelViewer;
        private System.Windows.Forms.Panel panelLeftBottomBtns;
    }
}