namespace MB.WinClientDefault.DataImport {
    partial class DefaultDataImportDialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultDataImportDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSure = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labTitleMessage = new System.Windows.Forms.Label();
            this.grdCtlMain = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.butSure);
            this.panel1.Controls.Add(this.butCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 413);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 47);
            this.panel1.TabIndex = 0;
            // 
            // butSure
            // 
            this.butSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSure.Location = new System.Drawing.Point(512, 9);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(124, 31);
            this.butSure.TabIndex = 1;
            this.butSure.Text = "导入(&S)";
            this.butSure.UseVisualStyleBackColor = true;
            this.butSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(642, 9);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(87, 31);
            this.butCancel.TabIndex = 0;
            this.butCancel.Text = "取消(&Q)";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labTitleMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(734, 52);
            this.panel2.TabIndex = 3;
            // 
            // labTitleMessage
            // 
            this.labTitleMessage.AutoSize = true;
            this.labTitleMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labTitleMessage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitleMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labTitleMessage.Location = new System.Drawing.Point(20, 14);
            this.labTitleMessage.Name = "labTitleMessage";
            this.labTitleMessage.Size = new System.Drawing.Size(0, 14);
            this.labTitleMessage.TabIndex = 0;
            // 
            // grdCtlMain
            // 
            this.grdCtlMain.DataIOControl = null;
            this.grdCtlMain.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.grdCtlMain.Location = new System.Drawing.Point(35, 84);
            this.grdCtlMain.MainView = this.gridViewMain;
            this.grdCtlMain.Name = "grdCtlMain";
            this.grdCtlMain.ShowOptionMenu = false;
            this.grdCtlMain.Size = new System.Drawing.Size(670, 281);
            this.grdCtlMain.TabIndex = 4;
            this.grdCtlMain.ValidedDeleteKeyDown = false;
            this.grdCtlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            this.grdCtlMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdCtlMain_KeyDown);
            // 
            // gridViewMain
            // 
            this.gridViewMain.GridControl = this.grdCtlMain;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            // 
            // DefaultDataImportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 460);
            this.Controls.Add(this.grdCtlMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "DefaultDataImportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导入处理";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butSure;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labTitleMessage;
        private MB.XWinLib.XtraGrid.GridControlEx grdCtlMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;

           
    }
}