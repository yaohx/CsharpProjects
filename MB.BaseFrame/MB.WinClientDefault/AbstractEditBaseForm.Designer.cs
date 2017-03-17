namespace MB.WinClientDefault {
    partial class AbstractEditBaseForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbstractEditBaseForm));
            this.bindingNavMain = new System.Windows.Forms.BindingNavigator(this.components);
            this.bntMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bntMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bntPositionItem = new System.Windows.Forms.ToolStripLabel();
            this.tsbHideTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bntMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bntMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bntAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bntCancelItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bntDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bntSaveItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bntSubmitItem = new System.Windows.Forms.ToolStripButton();
            this.bntCancelSubmitItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.butExtendItem = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsButQuit = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavMain)).BeginInit();
            this.bindingNavMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingNavMain
            // 
            this.bindingNavMain.AddNewItem = null;
            this.bindingNavMain.BackColor = System.Drawing.Color.White;
            this.bindingNavMain.CountItem = null;
            this.bindingNavMain.DeleteItem = null;
            this.bindingNavMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bntMoveFirstItem,
            this.bntMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bntPositionItem,
            this.tsbHideTextBox,
            this.bindingNavigatorSeparator1,
            this.bntMoveNextItem,
            this.bntMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bntAddNewItem,
            this.bntCancelItem,
            this.toolStripSeparator2,
            this.bntDeleteItem,
            this.toolStripSeparator3,
            this.bntSaveItem,
            this.toolStripSeparator4,
            this.bntSubmitItem,
            this.bntCancelSubmitItem,
            this.toolStripSeparator1,
            this.butExtendItem,
            this.toolStripSeparator5,
            this.tsButQuit});
            this.bindingNavMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.bindingNavMain.Location = new System.Drawing.Point(0, 0);
            this.bindingNavMain.MoveFirstItem = null;
            this.bindingNavMain.MoveLastItem = null;
            this.bindingNavMain.MoveNextItem = null;
            this.bindingNavMain.MovePreviousItem = null;
            this.bindingNavMain.Name = "bindingNavMain";
            this.bindingNavMain.PositionItem = null;
            this.bindingNavMain.Size = new System.Drawing.Size(717, 25);
            this.bindingNavMain.TabIndex = 0;
            this.bindingNavMain.Text = "bindingNavigator1";
            // 
            // bntMoveFirstItem
            // 
            this.bntMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntMoveFirstItem.Enabled = false;
            this.bntMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bntMoveFirstItem.Image")));
            this.bntMoveFirstItem.Name = "bntMoveFirstItem";
            this.bntMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bntMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bntMoveFirstItem.Text = "移到第一条记录";
            // 
            // bntMovePreviousItem
            // 
            this.bntMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntMovePreviousItem.Enabled = false;
            this.bntMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bntMovePreviousItem.Image")));
            this.bntMovePreviousItem.Name = "bntMovePreviousItem";
            this.bntMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bntMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bntMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bntPositionItem
            // 
            this.bntPositionItem.AccessibleName = "位置";
            this.bntPositionItem.AutoSize = false;
            this.bntPositionItem.BackColor = System.Drawing.Color.White;
            this.bntPositionItem.ForeColor = System.Drawing.Color.Maroon;
            this.bntPositionItem.Name = "bntPositionItem";
            this.bntPositionItem.Size = new System.Drawing.Size(80, 14);
            this.bntPositionItem.Text = "0";
            this.bntPositionItem.ToolTipText = "当前位置";
            // 
            // tsbHideTextBox
            // 
            this.tsbHideTextBox.Name = "tsbHideTextBox";
            this.tsbHideTextBox.Size = new System.Drawing.Size(1, 25);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bntMoveNextItem
            // 
            this.bntMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntMoveNextItem.Enabled = false;
            this.bntMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bntMoveNextItem.Image")));
            this.bntMoveNextItem.Name = "bntMoveNextItem";
            this.bntMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bntMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bntMoveNextItem.Text = "移到下一条记录";
            // 
            // bntMoveLastItem
            // 
            this.bntMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntMoveLastItem.Enabled = false;
            this.bntMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bntMoveLastItem.Image")));
            this.bntMoveLastItem.Name = "bntMoveLastItem";
            this.bntMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bntMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bntMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bntAddNewItem
            // 
            this.bntAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bntAddNewItem.Image")));
            this.bntAddNewItem.Name = "bntAddNewItem";
            this.bntAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bntAddNewItem.Size = new System.Drawing.Size(67, 22);
            this.bntAddNewItem.Text = "新增(&N)";
            // 
            // bntCancelItem
            // 
            this.bntCancelItem.Image = ((System.Drawing.Image)(resources.GetObject("bntCancelItem.Image")));
            this.bntCancelItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntCancelItem.Name = "bntCancelItem";
            this.bntCancelItem.Size = new System.Drawing.Size(67, 22);
            this.bntCancelItem.Text = "撤消(&U)";
            this.bntCancelItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bntDeleteItem
            // 
            this.bntDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bntDeleteItem.Image")));
            this.bntDeleteItem.Name = "bntDeleteItem";
            this.bntDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bntDeleteItem.Size = new System.Drawing.Size(67, 22);
            this.bntDeleteItem.Text = "删除(&D)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bntSaveItem
            // 
            this.bntSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("bntSaveItem.Image")));
            this.bntSaveItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntSaveItem.Name = "bntSaveItem";
            this.bntSaveItem.Size = new System.Drawing.Size(67, 22);
            this.bntSaveItem.Text = "保存(&S)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // bntSubmitItem
            // 
            this.bntSubmitItem.Image = ((System.Drawing.Image)(resources.GetObject("bntSubmitItem.Image")));
            this.bntSubmitItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntSubmitItem.Name = "bntSubmitItem";
            this.bntSubmitItem.Size = new System.Drawing.Size(49, 22);
            this.bntSubmitItem.Text = "确认";
            // 
            // bntCancelSubmitItem
            // 
            this.bntCancelSubmitItem.Image = ((System.Drawing.Image)(resources.GetObject("bntCancelSubmitItem.Image")));
            this.bntCancelSubmitItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntCancelSubmitItem.Name = "bntCancelSubmitItem";
            this.bntCancelSubmitItem.Size = new System.Drawing.Size(49, 22);
            this.bntCancelSubmitItem.Text = "重做";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // butExtendItem
            // 
            this.butExtendItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butExtendItem.Image = ((System.Drawing.Image)(resources.GetObject("butExtendItem.Image")));
            this.butExtendItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butExtendItem.Name = "butExtendItem";
            this.butExtendItem.Size = new System.Drawing.Size(66, 22);
            this.butExtendItem.Text = "功能菜单";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsButQuit
            // 
            this.tsButQuit.Image = ((System.Drawing.Image)(resources.GetObject("tsButQuit.Image")));
            this.tsButQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButQuit.Name = "tsButQuit";
            this.tsButQuit.Size = new System.Drawing.Size(67, 22);
            this.tsButQuit.Text = "关闭(&E)";
            this.tsButQuit.Click += new System.EventHandler(this.tsButQuit_Click);
            // 
            // AbstractEditBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(717, 457);
            this.Controls.Add(this.bindingNavMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "AbstractEditBaseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AbstractEditBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavMain)).EndInit();
            this.bindingNavMain.ResumeLayout(false);
            this.bindingNavMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bindingNavMain;
        private System.Windows.Forms.ToolStripButton bntAddNewItem;
        private System.Windows.Forms.ToolStripButton bntDeleteItem;
        private System.Windows.Forms.ToolStripButton bntMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bntMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bntMoveNextItem;
        private System.Windows.Forms.ToolStripButton bntMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton bntSaveItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton butExtendItem;
        private System.Windows.Forms.ToolStripButton bntCancelItem;
        private System.Windows.Forms.ToolStripButton bntSubmitItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton bntCancelSubmitItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel bntPositionItem;
        private System.Windows.Forms.ToolStripTextBox tsbHideTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsButQuit;
    }
}