namespace MB.WinClientDefault.QueryFilter {
    partial class ucDataTreeListView {
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
            this.trvLstMain = new MB.XWinLib.XtraTreeList.TreeListEx();
            ((System.ComponentModel.ISupportInitialize)(this.trvLstMain)).BeginInit();
            this.SuspendLayout();
            // 
            // trvLstMain
            // 
            this.trvLstMain.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.trvLstMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.trvLstMain.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.trvLstMain.Location = new System.Drawing.Point(20, 34);
            this.trvLstMain.Name = "trvLstMain";
            this.trvLstMain.OptionsView.EnableAppearanceEvenRow = true;
            this.trvLstMain.ParentFieldName = "PARENT_ID";
            this.trvLstMain.PreviewFieldName = "NAME";
            this.trvLstMain.Size = new System.Drawing.Size(474, 287);
            this.trvLstMain.TabIndex = 0;
            this.trvLstMain.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.trvLstMain_BeforeFocusNode);
            this.trvLstMain.DoubleClick += new System.EventHandler(this.trvLstMain_DoubleClick);
            // 
            // ucDataTreeListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.trvLstMain);
            this.Name = "ucDataTreeListView";
            this.Size = new System.Drawing.Size(528, 358);
            ((System.ComponentModel.ISupportInitialize)(this.trvLstMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion   
        private MB.XWinLib.XtraTreeList.TreeListEx trvLstMain;
    }
}
