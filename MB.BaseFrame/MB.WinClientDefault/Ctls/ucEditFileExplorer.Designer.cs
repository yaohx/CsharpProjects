namespace MB.WinClientDefault.Ctls {
    partial class ucEditFileExplorer {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEditFileExplorer));
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.mItemLargeIcon = new System.Windows.Forms.MenuItem();
            this.mItemSmallicon = new System.Windows.Forms.MenuItem();
            this.mItemList = new System.Windows.Forms.MenuItem();
            this.mItemDetail = new System.Windows.Forms.MenuItem();
            this.imgOperate = new System.Windows.Forms.ImageList(this.components);
            this.toolBarMain = new System.Windows.Forms.ToolBar();
            this.tButAddNew = new System.Windows.Forms.ToolBarButton();
            this.tButOpenPath = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tButDelete = new System.Windows.Forms.ToolBarButton();
            this.tButOutPut = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.tButPreview = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.tButView = new System.Windows.Forms.ToolBarButton();
            this.lstFileExplorer = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mItemLargeIcon,
            this.mItemSmallicon,
            this.mItemList,
            this.mItemDetail});
            // 
            // mItemLargeIcon
            // 
            this.mItemLargeIcon.Index = 0;
            this.mItemLargeIcon.Text = "缩略图(&H)";
            this.mItemLargeIcon.Click += new System.EventHandler(this.mItemLargeIcon_Click);
            // 
            // mItemSmallicon
            // 
            this.mItemSmallicon.Index = 1;
            this.mItemSmallicon.Text = "平铺(&S)";
            this.mItemSmallicon.Click += new System.EventHandler(this.mItemSmallicon_Click);
            // 
            // mItemList
            // 
            this.mItemList.Index = 2;
            this.mItemList.Text = "列表(&L)";
            this.mItemList.Click += new System.EventHandler(this.mItemList_Click);
            // 
            // mItemDetail
            // 
            this.mItemDetail.Index = 3;
            this.mItemDetail.Text = "详细信息(&D)";
            this.mItemDetail.Click += new System.EventHandler(this.mItemDetail_Click);
            // 
            // imgOperate
            // 
            this.imgOperate.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgOperate.ImageStream")));
            this.imgOperate.TransparentColor = System.Drawing.Color.Transparent;
            this.imgOperate.Images.SetKeyName(0, "");
            this.imgOperate.Images.SetKeyName(1, "");
            this.imgOperate.Images.SetKeyName(2, "");
            this.imgOperate.Images.SetKeyName(3, "");
            this.imgOperate.Images.SetKeyName(4, "");
            this.imgOperate.Images.SetKeyName(5, "Node0.ICO");
            // 
            // toolBarMain
            // 
            this.toolBarMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tButAddNew,
            this.tButOpenPath,
            this.toolBarButton1,
            this.tButDelete,
            this.tButOutPut,
            this.toolBarButton4,
            this.tButPreview,
            this.toolBarButton6,
            this.tButView});
            this.toolBarMain.DropDownArrows = true;
            this.toolBarMain.ImageList = this.imgOperate;
            this.toolBarMain.Location = new System.Drawing.Point(0, 0);
            this.toolBarMain.Name = "toolBarMain";
            this.toolBarMain.ShowToolTips = true;
            this.toolBarMain.Size = new System.Drawing.Size(602, 28);
            this.toolBarMain.TabIndex = 2;
            this.toolBarMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarMain_ButtonClick);
            // 
            // tButAddNew
            // 
            this.tButAddNew.ImageIndex = 0;
            this.tButAddNew.Name = "tButAddNew";
            this.tButAddNew.Tag = "addNew";
            this.tButAddNew.ToolTipText = "新增";
            // 
            // tButOpenPath
            // 
            this.tButOpenPath.ImageIndex = 5;
            this.tButOpenPath.Name = "tButOpenPath";
            this.tButOpenPath.ToolTipText = "批量新增";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tButDelete
            // 
            this.tButDelete.ImageIndex = 1;
            this.tButDelete.Name = "tButDelete";
            this.tButDelete.Tag = "delete";
            this.tButDelete.ToolTipText = "删除";
            // 
            // tButOutPut
            // 
            this.tButOutPut.ImageIndex = 4;
            this.tButOutPut.Name = "tButOutPut";
            this.tButOutPut.Tag = "output";
            this.tButOutPut.ToolTipText = "导出";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tButPreview
            // 
            this.tButPreview.ImageIndex = 3;
            this.tButPreview.Name = "tButPreview";
            this.tButPreview.Tag = "preview";
            this.tButPreview.ToolTipText = "查看详细内容";
            // 
            // toolBarButton6
            // 
            this.toolBarButton6.Name = "toolBarButton6";
            this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tButView
            // 
            this.tButView.DropDownMenu = this.contextMenu1;
            this.tButView.ImageIndex = 2;
            this.tButView.Name = "tButView";
            this.tButView.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tButView.ToolTipText = "查看";
            // 
            // lstFileExplorer
            // 
            this.lstFileExplorer.Location = new System.Drawing.Point(12, 34);
            this.lstFileExplorer.Name = "lstFileExplorer";
            this.lstFileExplorer.Size = new System.Drawing.Size(400, 264);
            this.lstFileExplorer.TabIndex = 3;
            this.lstFileExplorer.UseCompatibleStateImageBehavior = false;
            this.lstFileExplorer.View = System.Windows.Forms.View.Details;
            this.lstFileExplorer.DoubleClick += new System.EventHandler(this.lstFileExplorer_DoubleClick);
            // 
            // ucEditFileExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstFileExplorer);
            this.Controls.Add(this.toolBarMain);
            this.Name = "ucEditFileExplorer";
            this.Size = new System.Drawing.Size(602, 356);
            this.Load += new System.EventHandler(this.ucEditFileExplorer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem mItemSmallicon;
        private System.Windows.Forms.MenuItem mItemLargeIcon;
        private System.Windows.Forms.MenuItem mItemList;
        private System.Windows.Forms.MenuItem mItemDetail;
        private System.Windows.Forms.ImageList imgOperate;
        private System.Windows.Forms.ToolBar toolBarMain;
        private System.Windows.Forms.ToolBarButton tButAddNew;
        private System.Windows.Forms.ToolBarButton tButDelete;
        private System.Windows.Forms.ToolBarButton tButOutPut;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton tButPreview;
        private System.Windows.Forms.ToolBarButton toolBarButton6;
        private System.Windows.Forms.ToolBarButton tButView;
        private System.Windows.Forms.ListView lstFileExplorer;
        private System.Windows.Forms.ToolBarButton tButOpenPath;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
    }
}
