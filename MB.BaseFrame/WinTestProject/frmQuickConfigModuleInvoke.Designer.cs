namespace WinTestProject {
    partial class frmQuickConfigModuleInvoke {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuickConfigModuleInvoke));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butQuit = new System.Windows.Forms.Button();
            this.butSure = new System.Windows.Forms.Button();
            this.butSelectDll = new System.Windows.Forms.Button();
            this.cobDllFileName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trvClassType = new System.Windows.Forms.TreeView();
            this.gridControlEx1 = new MB.XWinLib.XtraGrid.GridControlEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEx1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cobDllFileName);
            this.panel1.Controls.Add(this.butSelectDll);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 46);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "浏览：";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.butSure);
            this.panel2.Controls.Add(this.butQuit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 298);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(536, 43);
            this.panel2.TabIndex = 1;
            // 
            // butQuit
            // 
            this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butQuit.Location = new System.Drawing.Point(460, 8);
            this.butQuit.Name = "butQuit";
            this.butQuit.Size = new System.Drawing.Size(66, 26);
            this.butQuit.TabIndex = 0;
            this.butQuit.Text = "取消(&Q)";
            this.butQuit.UseVisualStyleBackColor = true;
            // 
            // butSure
            // 
            this.butSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSure.Location = new System.Drawing.Point(385, 8);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(71, 26);
            this.butSure.TabIndex = 1;
            this.butSure.Text = "确定(&S)";
            this.butSure.UseVisualStyleBackColor = true;
            // 
            // butSelectDll
            // 
            this.butSelectDll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSelectDll.Location = new System.Drawing.Point(489, 9);
            this.butSelectDll.Name = "butSelectDll";
            this.butSelectDll.Size = new System.Drawing.Size(42, 21);
            this.butSelectDll.TabIndex = 2;
            this.butSelectDll.Text = "...";
            this.butSelectDll.UseVisualStyleBackColor = true;
            this.butSelectDll.Click += new System.EventHandler(this.butSelectDll_Click);
            // 
            // cobDllFileName
            // 
            this.cobDllFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cobDllFileName.FormattingEnabled = true;
            this.cobDllFileName.Location = new System.Drawing.Point(58, 8);
            this.cobDllFileName.Name = "cobDllFileName";
            this.cobDllFileName.Size = new System.Drawing.Size(425, 20);
            this.cobDllFileName.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trvClassType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 252);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DLL 组件：";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(227, 46);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 252);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControlEx1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(231, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 252);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "配置信息：";
            // 
            // trvClassType
            // 
            this.trvClassType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvClassType.ImageIndex = 0;
            this.trvClassType.ImageList = this.imageList1;
            this.trvClassType.Location = new System.Drawing.Point(3, 17);
            this.trvClassType.Name = "trvClassType";
            this.trvClassType.SelectedImageIndex = 0;
            this.trvClassType.Size = new System.Drawing.Size(221, 232);
            this.trvClassType.TabIndex = 0;
            // 
            // gridControlEx1
            // 
            this.gridControlEx1.DataIOControl = null;
            this.gridControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlEx1.InstanceIdentity = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.gridControlEx1.Location = new System.Drawing.Point(3, 17);
            this.gridControlEx1.MainView = this.gridView1;
            this.gridControlEx1.Name = "gridControlEx1";
            this.gridControlEx1.ShowOptionMenu = false;
            this.gridControlEx1.Size = new System.Drawing.Size(299, 232);
            this.gridControlEx1.TabIndex = 0;
            this.gridControlEx1.ValidedDeleteKeyDown = false;
            this.gridControlEx1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlEx1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "script.ico");
            this.imageList1.Images.SetKeyName(1, "automgr.ico");
            // 
            // frmQuickConfigModuleInvoke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(536, 341);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "frmQuickConfigModuleInvoke";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模块快速配置";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEx1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butSure;
        private System.Windows.Forms.Button butQuit;
        private System.Windows.Forms.Button butSelectDll;
        private System.Windows.Forms.ComboBox cobDllFileName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView trvClassType;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MB.XWinLib.XtraGrid.GridControlEx gridControlEx1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}