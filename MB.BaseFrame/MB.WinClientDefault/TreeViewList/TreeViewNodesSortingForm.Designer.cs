namespace MB.WinClientDefault.TreeViewList {
    partial class TreeViewNodesSortingForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewNodesSortingForm));
            this.trvLstMain = new MB.XWinLib.XtraTreeList.TreeListEx();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.btnSaveItem = new DevExpress.XtraBars.BarButtonItem();
            this.btnQuit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trvLstMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // trvLstMain
            // 
            this.trvLstMain.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.trvLstMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.trvLstMain.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.trvLstMain.Location = new System.Drawing.Point(50, 56);
            this.trvLstMain.Name = "trvLstMain";
            this.trvLstMain.OptionsView.EnableAppearanceEvenRow = true;
            this.trvLstMain.Size = new System.Drawing.Size(473, 263);
            this.trvLstMain.TabIndex = 27;
            this.trvLstMain.CalcNodeDragImageIndex += new DevExpress.XtraTreeList.CalcNodeDragImageIndexEventHandler(this.trvLstMain_CalcNodeDragImageIndex);
            this.trvLstMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvLstMain_DragDrop);
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imageList1;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSaveItem,
            this.btnQuit});
            this.barManager.MainMenu = this.barTools;
            this.barManager.MaxItemId = 3;
            this.barManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barManager_ItemClick);
            // 
            // barTools
            // 
            this.barTools.BarName = "Main menu";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnQuit)});
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DisableClose = true;
            this.barTools.OptionsBar.DisableCustomization = true;
            this.barTools.OptionsBar.MultiLine = true;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.Caption = "保存(&S)";
            this.btnSaveItem.Id = 1;
            this.btnSaveItem.ImageIndex = 7;
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnQuit
            // 
            this.btnQuit.Caption = "关闭(&E)";
            this.btnQuit.Id = 2;
            this.btnQuit.ImageIndex = 4;
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(684, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 376);
            this.barDockControlBottom.Size = new System.Drawing.Size(684, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 350);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(684, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 350);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tEUndo.ico");
            this.imageList1.Images.SetKeyName(1, "SubmitChecked.ICO");
            this.imageList1.Images.SetKeyName(2, "18BASEDATA18.ico");
            this.imageList1.Images.SetKeyName(3, "7SCJHTC7.ICO");
            this.imageList1.Images.SetKeyName(4, "18BASEDATA18.ico");
            this.imageList1.Images.SetKeyName(5, "SymbolMinus.ico");
            this.imageList1.Images.SetKeyName(6, "SymbolPlus.ico");
            this.imageList1.Images.SetKeyName(7, "FileSave.ico");
            this.imageList1.Images.SetKeyName(8, "ArrowLargeLeft.ico");
            this.imageList1.Images.SetKeyName(9, "ArrowLargeRight.ico");
            // 
            // TreeViewNodesSortingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 376);
            this.Controls.Add(this.trvLstMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TreeViewNodesSortingForm";
            this.Text = "子节点排序";
            this.Load += new System.EventHandler(this.TreeViewNodesSortingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trvLstMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XWinLib.XtraTreeList.TreeListEx trvLstMain;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem btnSaveItem;
        private DevExpress.XtraBars.BarButtonItem btnQuit;
    }
}