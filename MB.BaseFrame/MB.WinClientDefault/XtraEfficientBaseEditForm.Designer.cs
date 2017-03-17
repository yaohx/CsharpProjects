namespace MB.WinClientDefault
{
    partial class XtraEfficientBaseEditForm
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraEfficientBaseEditForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.bntMoveFirstItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntMovePreviousItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntPositionItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntMoveNextItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntMoveLastItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntAddNewItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntDeleteItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntSaveItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntSubmitItem = new DevExpress.XtraBars.BarButtonItem();
            this.bntCancelSubmitItem = new DevExpress.XtraBars.BarButtonItem();
            this.butExtendItem = new DevExpress.XtraBars.BarSubItem();
            this.tsButQuit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.bntPositionItem,
            this.bntMoveNextItem,
            this.bntAddNewItem,
            this.bntDeleteItem,
            this.bntSaveItem,
            this.bntSubmitItem,
            this.bntCancelSubmitItem,
            this.barButtonItem9,
            this.butExtendItem,
            this.bntMoveFirstItem,
            this.bntMoveLastItem,
            this.tsButQuit,
            this.bntMovePreviousItem});
            this.barManager1.MaxItemId = 18;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.barManager1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bntItem_ItemClick);
            // 
            // barTools
            // 
            this.barTools.Appearance.Options.UseTextOptions = true;
            this.barTools.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bntMoveFirstItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntMovePreviousItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntPositionItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntMoveNextItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntMoveLastItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntAddNewItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntDeleteItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntSaveItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntSubmitItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bntCancelSubmitItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.butExtendItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.tsButQuit, true)});
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DisableClose = true;
            this.barTools.OptionsBar.DisableCustomization = true;
            this.barTools.OptionsBar.MultiLine = true;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // bntMoveFirstItem
            // 
            this.bntMoveFirstItem.Caption = "<<";
            this.bntMoveFirstItem.Id = 12;
            this.bntMoveFirstItem.Name = "bntMoveFirstItem";
            // 
            // bntMovePreviousItem
            // 
            this.bntMovePreviousItem.Caption = "<";
            this.bntMovePreviousItem.Id = 17;
            this.bntMovePreviousItem.Name = "bntMovePreviousItem";
            // 
            // bntPositionItem
            // 
            this.bntPositionItem.Caption = "{0}/{1}";
            this.bntPositionItem.Id = 3;
            this.bntPositionItem.Name = "bntPositionItem";
            // 
            // bntMoveNextItem
            // 
            this.bntMoveNextItem.Caption = ">";
            this.bntMoveNextItem.Id = 4;
            this.bntMoveNextItem.Name = "bntMoveNextItem";
            // 
            // bntMoveLastItem
            // 
            this.bntMoveLastItem.Caption = ">>";
            this.bntMoveLastItem.Id = 13;
            this.bntMoveLastItem.Name = "bntMoveLastItem";
            // 
            // bntAddNewItem
            // 
            this.bntAddNewItem.Caption = "新增(&N)";
            this.bntAddNewItem.Id = 5;
            this.bntAddNewItem.ImageIndex = 6;
            this.bntAddNewItem.Name = "bntAddNewItem";
            this.bntAddNewItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bntDeleteItem
            // 
            this.bntDeleteItem.Caption = "删除(&D)";
            this.bntDeleteItem.Id = 6;
            this.bntDeleteItem.ImageIndex = 5;
            this.bntDeleteItem.Name = "bntDeleteItem";
            this.bntDeleteItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bntSaveItem
            // 
            this.bntSaveItem.Caption = "保存(&S)";
            this.bntSaveItem.Id = 7;
            this.bntSaveItem.ImageIndex = 7;
            this.bntSaveItem.Name = "bntSaveItem";
            this.bntSaveItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bntSubmitItem
            // 
            this.bntSubmitItem.Caption = "确认";
            this.bntSubmitItem.Id = 8;
            this.bntSubmitItem.ImageIndex = 1;
            this.bntSubmitItem.Name = "bntSubmitItem";
            this.bntSubmitItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bntCancelSubmitItem
            // 
            this.bntCancelSubmitItem.Caption = "重做";
            this.bntCancelSubmitItem.Id = 9;
            this.bntCancelSubmitItem.ImageIndex = 0;
            this.bntCancelSubmitItem.Name = "bntCancelSubmitItem";
            this.bntCancelSubmitItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // butExtendItem
            // 
            this.butExtendItem.Caption = "功能菜单";
            this.butExtendItem.Id = 11;
            this.butExtendItem.Name = "butExtendItem";
            // 
            // tsButQuit
            // 
            this.tsButQuit.Caption = "关闭(&E)";
            this.tsButQuit.Id = 14;
            this.tsButQuit.ImageIndex = 4;
            this.tsButQuit.Name = "tsButQuit";
            this.tsButQuit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(648, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 418);
            this.barDockControlBottom.Size = new System.Drawing.Size(648, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 392);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(648, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 392);
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
            // barStaticItem1
            // 
            this.barStaticItem1.Id = 2;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "关闭(&E)";
            this.barButtonItem9.Id = 10;
            this.barButtonItem9.Name = "barButtonItem9";
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // XtraEfficientBaseEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 418);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "XtraEfficientBaseEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AbstractEditBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem bntPositionItem;
        private DevExpress.XtraBars.BarButtonItem bntMoveNextItem;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem bntAddNewItem;
        private DevExpress.XtraBars.BarButtonItem bntDeleteItem;
        private DevExpress.XtraBars.BarButtonItem bntSaveItem;
        private DevExpress.XtraBars.BarButtonItem bntSubmitItem;
        private DevExpress.XtraBars.BarButtonItem bntCancelSubmitItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarSubItem butExtendItem;
        private DevExpress.XtraBars.BarButtonItem bntMoveFirstItem;
        private DevExpress.XtraBars.BarButtonItem bntMoveLastItem;
        private DevExpress.XtraBars.BarButtonItem tsButQuit;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraBars.BarButtonItem bntMovePreviousItem;
    }
}