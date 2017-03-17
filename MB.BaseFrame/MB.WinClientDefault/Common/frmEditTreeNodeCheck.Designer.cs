namespace MB.WinClientDefault.Common {
    partial class frmEditTreeNodeCheck {
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAllIsSame = new System.Windows.Forms.CheckBox();
            this.butAddAsChild = new System.Windows.Forms.Button();
            this.butAddAsRoot = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAllIsSame);
            this.panel1.Controls.Add(this.butAddAsChild);
            this.panel1.Controls.Add(this.butAddAsRoot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 45);
            this.panel1.TabIndex = 0;
            // 
            // chkAllIsSame
            // 
            this.chkAllIsSame.AutoSize = true;
            this.chkAllIsSame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chkAllIsSame.Location = new System.Drawing.Point(7, 9);
            this.chkAllIsSame.Name = "chkAllIsSame";
            this.chkAllIsSame.Size = new System.Drawing.Size(122, 18);
            this.chkAllIsSame.TabIndex = 2;
            this.chkAllIsSame.Text = "所有按该方式处理";
            this.chkAllIsSame.UseVisualStyleBackColor = true;
            // 
            // butAddAsChild
            // 
            this.butAddAsChild.Location = new System.Drawing.Point(292, 7);
            this.butAddAsChild.Name = "butAddAsChild";
            this.butAddAsChild.Size = new System.Drawing.Size(85, 29);
            this.butAddAsChild.TabIndex = 1;
            this.butAddAsChild.Text = "添加子级(&C)";
            this.butAddAsChild.UseVisualStyleBackColor = true;
            this.butAddAsChild.Click += new System.EventHandler(this.butAddAsChild_Click);
            // 
            // butAddAsRoot
            // 
            this.butAddAsRoot.Location = new System.Drawing.Point(207, 6);
            this.butAddAsRoot.Name = "butAddAsRoot";
            this.butAddAsRoot.Size = new System.Drawing.Size(79, 30);
            this.butAddAsRoot.TabIndex = 0;
            this.butAddAsRoot.Text = "添加根(&P)";
            this.butAddAsRoot.UseVisualStyleBackColor = true;
            this.butAddAsRoot.Click += new System.EventHandler(this.butAddAsRoot_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = "操作提示：\r\n\r\n    添加根=>添加同级的节点\r\n\r\n    添加子级=>添加为当前节点的子节点";
            // 
            // frmEditTreeNodeCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 156);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditTreeNodeCheck";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请选择增加节点的行为";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butAddAsRoot;
        private System.Windows.Forms.CheckBox chkAllIsSame;
        private System.Windows.Forms.Button butAddAsChild;
        private System.Windows.Forms.Label label1;
    }
}