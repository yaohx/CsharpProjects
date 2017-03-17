namespace MB.WinChart
{
	partial class ChartTemplateConfig
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.butAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.txtAddName = new System.Windows.Forms.TextBox();
			this.panelContent = new System.Windows.Forms.Panel();
			this.panelDataGridView = new System.Windows.Forms.Panel();
			this.lstTemplate = new System.Windows.Forms.ListBox();
			this.panelDelete = new System.Windows.Forms.Panel();
			this.buttonExit = new System.Windows.Forms.Button();
			this.panelAdd = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panelContent.SuspendLayout();
			this.panelDataGridView.SuspendLayout();
			this.panelDelete.SuspendLayout();
			this.panelAdd.SuspendLayout();
			this.SuspendLayout();
			// 
			// butAdd
			// 
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Location = new System.Drawing.Point(220, 10);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 23);
			this.butAdd.TabIndex = 1;
			this.butAdd.Text = "新增(&A)";
			this.butAdd.UseVisualStyleBackColor = true;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(7, 7);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "删除(&D)";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// txtAddName
			// 
			this.txtAddName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtAddName.Location = new System.Drawing.Point(75, 12);
			this.txtAddName.MaxLength = 128;
			this.txtAddName.Name = "txtAddName";
			this.txtAddName.Size = new System.Drawing.Size(139, 21);
			this.txtAddName.TabIndex = 0;
			// 
			// panelContent
			// 
			this.panelContent.Controls.Add(this.panelDataGridView);
			this.panelContent.Controls.Add(this.panelDelete);
			this.panelContent.Controls.Add(this.panelAdd);
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(0, 0);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(303, 338);
			this.panelContent.TabIndex = 8;
			// 
			// panelDataGridView
			// 
			this.panelDataGridView.Controls.Add(this.lstTemplate);
			this.panelDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDataGridView.Location = new System.Drawing.Point(0, 41);
			this.panelDataGridView.Name = "panelDataGridView";
			this.panelDataGridView.Size = new System.Drawing.Size(214, 297);
			this.panelDataGridView.TabIndex = 7;
			// 
			// lstTemplate
			// 
			this.lstTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstTemplate.FormattingEnabled = true;
			this.lstTemplate.ItemHeight = 12;
			this.lstTemplate.Location = new System.Drawing.Point(0, 0);
			this.lstTemplate.Name = "lstTemplate";
			this.lstTemplate.Size = new System.Drawing.Size(214, 292);
			this.lstTemplate.TabIndex = 0;
			// 
			// panelDelete
			// 
			this.panelDelete.Controls.Add(this.buttonExit);
			this.panelDelete.Controls.Add(this.btnDelete);
			this.panelDelete.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelDelete.Location = new System.Drawing.Point(214, 41);
			this.panelDelete.Name = "panelDelete";
			this.panelDelete.Size = new System.Drawing.Size(89, 297);
			this.panelDelete.TabIndex = 6;
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(7, 42);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(75, 23);
			this.buttonExit.TabIndex = 4;
			this.buttonExit.Text = "关闭(&Q)";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// panelAdd
			// 
			this.panelAdd.Controls.Add(this.label1);
			this.panelAdd.Controls.Add(this.txtAddName);
			this.panelAdd.Controls.Add(this.butAdd);
			this.panelAdd.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelAdd.Location = new System.Drawing.Point(0, 0);
			this.panelAdd.Name = "panelAdd";
			this.panelAdd.Size = new System.Drawing.Size(303, 41);
			this.panelAdd.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "输入名称:";
			// 
			// ChartTemplateConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(303, 338);
			this.Controls.Add(this.panelContent);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChartTemplateConfig";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "管理模板";
			this.panelContent.ResumeLayout(false);
			this.panelDataGridView.ResumeLayout(false);
			this.panelDelete.ResumeLayout(false);
			this.panelAdd.ResumeLayout(false);
			this.panelAdd.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.TextBox txtAddName;
		private System.Windows.Forms.Panel panelContent;
		private System.Windows.Forms.Panel panelAdd;
		private System.Windows.Forms.Panel panelDelete;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Panel panelDataGridView;
		private System.Windows.Forms.ListBox lstTemplate;
		private System.Windows.Forms.Label label1;
	}
}