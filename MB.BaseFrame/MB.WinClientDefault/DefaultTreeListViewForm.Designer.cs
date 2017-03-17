namespace MB.WinClientDefault {
    partial class DefaultTreeListViewForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultTreeListViewForm));
            this.panTitle = new System.Windows.Forms.Panel();
            this.labTitleMessage = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.trvLstMain = new MB.XWinLib.XtraTreeList.TreeListEx();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panTitle.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trvLstMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panTitle
            // 
            this.panTitle.BackColor = System.Drawing.SystemColors.Info;
            this.panTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panTitle.Controls.Add(this.labTitleMessage);
            this.panTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTitle.Location = new System.Drawing.Point(0, 0);
            this.panTitle.Name = "panTitle";
            this.panTitle.Size = new System.Drawing.Size(790, 7);
            this.panTitle.TabIndex = 4;
            this.panTitle.Visible = false;
            // 
            // labTitleMessage
            // 
            this.labTitleMessage.AutoSize = true;
            this.labTitleMessage.Font = new System.Drawing.Font("SimSun", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitleMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labTitleMessage.Location = new System.Drawing.Point(16, 12);
            this.labTitleMessage.Name = "labTitleMessage";
            this.labTitleMessage.Size = new System.Drawing.Size(0, 16);
            this.labTitleMessage.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(790, 556);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.trvLstMain);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(782, 529);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "树型浏览";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // trvLstMain
            // 
            this.trvLstMain.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.trvLstMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.trvLstMain.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.trvLstMain.Location = new System.Drawing.Point(34, 27);
            this.trvLstMain.Name = "trvLstMain";
            this.trvLstMain.OptionsView.EnableAppearanceEvenRow = true;
            this.trvLstMain.Size = new System.Drawing.Size(667, 370);
            this.trvLstMain.TabIndex = 0;
            this.trvLstMain.BeforeDragNode += new DevExpress.XtraTreeList.BeforeDragNodeEventHandler(this.trvLstMain_BeforeDragNode);
            this.trvLstMain.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.trvLstMain_AfterDragNode);
            this.trvLstMain.CustomDrawNodeImages += new DevExpress.XtraTreeList.CustomDrawNodeImagesEventHandler(this.trvLstMain_CustomDrawNodeImages);
            this.trvLstMain.DoubleClick += new System.EventHandler(this.trvLstMain_DoubleClick);
            this.trvLstMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvLstMain_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Function.ICO");
            // 
            // DefaultTreeListViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 563);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "DefaultTreeListViewForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "默认的树型浏览窗口";
            this.panTitle.ResumeLayout(false);
            this.panTitle.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trvLstMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTitle;
        private System.Windows.Forms.Label labTitleMessage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MB.XWinLib.XtraTreeList.TreeListEx trvLstMain;
        private System.Windows.Forms.ImageList imageList1;
    }
}