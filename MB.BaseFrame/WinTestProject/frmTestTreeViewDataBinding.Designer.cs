namespace WinTestProject {
    partial class frmTestTreeViewDataBinding {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestTreeViewDataBinding));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ucCheckedListCombox1 = new MB.WinBase.Ctls.ucComboCheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xtraTreeList1 = new DevExpress.XtraTreeList.TreeList();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.imageEdit1 = new DevExpress.XtraEditors.ImageEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTreeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(159, 588);
            this.treeView1.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "9XZWFKZ9.ico");
            this.imageList1.Images.SetKeyName(1, "6LTKS6.ico");
            this.imageList1.Images.SetKeyName(2, "7SCJHTC7.ICO");
            this.imageList1.Images.SetKeyName(3, "8CJSC8.ico");
            // 
            // ucCheckedListCombox1
            // 
            this.ucCheckedListCombox1.BranchSeparator = "\\";
            this.ucCheckedListCombox1.CaptionText = "";
            this.ucCheckedListCombox1.CaptionVisible = false;
            this.ucCheckedListCombox1.DataSource = null;
            this.ucCheckedListCombox1.DisplayMember = "Name";
            this.ucCheckedListCombox1.FilterByCode = true;
            // 
            // 
            // 
            this.ucCheckedListCombox1.ListObject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ucCheckedListCombox1.ListObject.CheckOnClick = true;
            this.ucCheckedListCombox1.ListObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCheckedListCombox1.ListObject.Location = new System.Drawing.Point(0, 0);
            this.ucCheckedListCombox1.ListObject.Name = "";
            this.ucCheckedListCombox1.ListObject.Size = new System.Drawing.Size(186, 98);
            this.ucCheckedListCombox1.ListObject.TabIndex = 1;
            this.ucCheckedListCombox1.Location = new System.Drawing.Point(165, 58);
            this.ucCheckedListCombox1.Name = "ucCheckedListCombox1";
            this.ucCheckedListCombox1.OnlyItemChecked = false;
            this.ucCheckedListCombox1.Size = new System.Drawing.Size(188, 22);
            this.ucCheckedListCombox1.TabIndex = 3;
            this.ucCheckedListCombox1.ValueMember = "ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 26);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(165, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 29);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xtraTreeList1);
            this.groupBox1.Location = new System.Drawing.Point(165, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 276);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试Xtra TreeViewList:";
            // 
            // xtraTreeList1
            // 
            this.xtraTreeList1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.xtraTreeList1.Appearance.EvenRow.Options.UseBackColor = true;
            this.xtraTreeList1.Location = new System.Drawing.Point(6, 36);
            this.xtraTreeList1.Name = "xtraTreeList1";
            this.xtraTreeList1.Size = new System.Drawing.Size(623, 263);
            this.xtraTreeList1.StateImageList = this.imageList1;
            this.xtraTreeList1.TabIndex = 0;
            this.xtraTreeList1.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.xtraTreeList1_AfterDragNode);
            this.xtraTreeList1.DragObjectOver += new DevExpress.XtraTreeList.DragObjectOverEventHandler(this.xtraTreeList1_DragObjectOver);
            this.xtraTreeList1.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.xtraTreeList1_CustomDrawNodeCell);
            this.xtraTreeList1.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.xtraTreeList1_AfterFocusNode);
            this.xtraTreeList1.DragOver += new System.Windows.Forms.DragEventHandler(this.xtraTreeList1_DragOver);
            this.xtraTreeList1.CustomDrawNodeImages += new DevExpress.XtraTreeList.CustomDrawNodeImagesEventHandler(this.xtraTreeList1_CustomDrawNodeImages);
            this.xtraTreeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.xtraTreeList1_GetStateImage);
            this.xtraTreeList1.DragObjectStart += new DevExpress.XtraTreeList.DragObjectStartEventHandler(this.xtraTreeList1_DragObjectStart);
            this.xtraTreeList1.DragDrop += new System.Windows.Forms.DragEventHandler(this.xtraTreeList1_DragDrop);
            this.xtraTreeList1.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.xtraTreeList1_GetSelectImage);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(456, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(204, 29);
            this.button3.TabIndex = 7;
            this.button3.Text = "绑定XtraTreeList";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 475);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 503);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "编码：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(224, 466);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(268, 21);
            this.txtName.TabIndex = 10;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(224, 494);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(267, 21);
            this.txtCode.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "ID:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(224, 439);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(267, 21);
            this.txtID.TabIndex = 13;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(526, 437);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 29);
            this.button4.TabIndex = 14;
            this.button4.Text = "新增";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(644, 437);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(113, 29);
            this.button5.TabIndex = 15;
            this.button5.Text = "删除";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(524, 481);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(102, 34);
            this.button6.TabIndex = 16;
            this.button6.Text = "导入";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(650, 479);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(106, 35);
            this.button7.TabIndex = 17;
            this.button7.Text = "导出";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(176, 98);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(115, 30);
            this.button8.TabIndex = 18;
            this.button8.Text = "First";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(314, 97);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(126, 30);
            this.button9.TabIndex = 19;
            this.button9.Text = "Next";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(463, 94);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(119, 32);
            this.button10.TabIndex = 20;
            this.button10.Text = "Previous";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(595, 91);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(97, 35);
            this.button11.TabIndex = 21;
            this.button11.Text = "Last";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(182, 531);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(190, 44);
            this.button12.TabIndex = 22;
            this.button12.Text = "button12";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // imageEdit1
            // 
            this.imageEdit1.Location = new System.Drawing.Point(376, 58);
            this.imageEdit1.Name = "imageEdit1";
            this.imageEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageEdit1.Size = new System.Drawing.Size(65, 21);
            this.imageEdit1.TabIndex = 23;
            // 
            // frmTestTreeViewDataBinding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 588);
            this.Controls.Add(this.imageEdit1);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucCheckedListCombox1);
            this.Controls.Add(this.treeView1);
            this.Name = "frmTestTreeViewDataBinding";
            this.Text = "frmTestTreeViewDataBinding";
            this.Load += new System.EventHandler(this.frmTestTreeViewDataBinding_Load_1);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTreeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private MB.WinBase.Ctls.ucComboCheckedListBox ucCheckedListCombox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraTreeList.TreeList xtraTreeList1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private DevExpress.XtraEditors.ImageEdit imageEdit1;
    }
}