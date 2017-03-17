namespace MB.WinChart
{
	partial class ChartConfigure
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
			this.btnClose = new System.Windows.Forms.Button();
			this.rBtnDirect = new System.Windows.Forms.RadioButton();
			this.rBtnCompare = new System.Windows.Forms.RadioButton();
			this.btnApplication = new System.Windows.Forms.Button();
			this.groupBoxTemplate = new System.Windows.Forms.GroupBox();
			this.butChoose = new System.Windows.Forms.Button();
			this.dbcboTemplate = new System.Windows.Forms.ComboBox();
			this.dbcboChartType = new System.Windows.Forms.ComboBox();
			this.panelChoose = new System.Windows.Forms.Panel();
			this.groupBoxClassify = new System.Windows.Forms.GroupBox();
			this.chkListClassify = new System.Windows.Forms.CheckedListBox();
			this.groupBoxCompare = new System.Windows.Forms.GroupBox();
			this.dbcboCompare = new System.Windows.Forms.ComboBox();
			this.groupBoxChartType = new System.Windows.Forms.GroupBox();
			this.groupBoxAnalysis = new System.Windows.Forms.GroupBox();
			this.rBtnAnalysis = new System.Windows.Forms.RadioButton();
			this.groupBoxTemplate.SuspendLayout();
			this.panelChoose.SuspendLayout();
			this.groupBoxClassify.SuspendLayout();
			this.groupBoxCompare.SuspendLayout();
			this.groupBoxChartType.SuspendLayout();
			this.groupBoxAnalysis.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(268, 468);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "关闭(&Q)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// rBtnDirect
			// 
			this.rBtnDirect.AutoSize = true;
			this.rBtnDirect.Location = new System.Drawing.Point(14, 87);
			this.rBtnDirect.Name = "rBtnDirect";
			this.rBtnDirect.Size = new System.Drawing.Size(71, 16);
			this.rBtnDirect.TabIndex = 2;
			this.rBtnDirect.Text = "数据走势";
			this.rBtnDirect.UseVisualStyleBackColor = true;
			this.rBtnDirect.CheckedChanged += new System.EventHandler(this.rBtn_CheckedChanged);
			// 
			// rBtnCompare
			// 
			this.rBtnCompare.AutoSize = true;
			this.rBtnCompare.Location = new System.Drawing.Point(14, 55);
			this.rBtnCompare.Name = "rBtnCompare";
			this.rBtnCompare.Size = new System.Drawing.Size(71, 16);
			this.rBtnCompare.TabIndex = 1;
			this.rBtnCompare.Text = "数据对比";
			this.rBtnCompare.UseVisualStyleBackColor = true;
			this.rBtnCompare.CheckedChanged += new System.EventHandler(this.rBtn_CheckedChanged);
			// 
			// btnApplication
			// 
			this.btnApplication.Location = new System.Drawing.Point(187, 468);
			this.btnApplication.Name = "btnApplication";
			this.btnApplication.Size = new System.Drawing.Size(75, 23);
			this.btnApplication.TabIndex = 4;
			this.btnApplication.Text = "应用(&A)";
			this.btnApplication.UseVisualStyleBackColor = true;
			this.btnApplication.Click += new System.EventHandler(this.btnApplication_Click);
			// 
			// groupBoxTemplate
			// 
			this.groupBoxTemplate.Controls.Add(this.butChoose);
			this.groupBoxTemplate.Controls.Add(this.dbcboTemplate);
			this.groupBoxTemplate.Location = new System.Drawing.Point(7, 3);
			this.groupBoxTemplate.Name = "groupBoxTemplate";
			this.groupBoxTemplate.Size = new System.Drawing.Size(311, 49);
			this.groupBoxTemplate.TabIndex = 3;
			this.groupBoxTemplate.TabStop = false;
			this.groupBoxTemplate.Text = "模板选择:";
			// 
			// butChoose
			// 
			this.butChoose.AutoSize = true;
			this.butChoose.Location = new System.Drawing.Point(240, 18);
			this.butChoose.Name = "butChoose";
			this.butChoose.Size = new System.Drawing.Size(52, 23);
			this.butChoose.TabIndex = 2;
			this.butChoose.Text = "...";
			this.butChoose.UseVisualStyleBackColor = true;
			this.butChoose.Click += new System.EventHandler(this.butChoose_Click);
			// 
			// dbcboTemplate
			// 
			this.dbcboTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dbcboTemplate.FormattingEnabled = true;
			this.dbcboTemplate.Location = new System.Drawing.Point(11, 21);
			this.dbcboTemplate.Name = "dbcboTemplate";
			this.dbcboTemplate.Size = new System.Drawing.Size(223, 20);
			this.dbcboTemplate.TabIndex = 1;
			this.dbcboTemplate.SelectedIndexChanged += new System.EventHandler(this.dbcboTemplate_SelectedIndexChanged);
			// 
			// dbcboChartType
			// 
			this.dbcboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dbcboChartType.FormattingEnabled = true;
			this.dbcboChartType.Location = new System.Drawing.Point(10, 22);
			this.dbcboChartType.Name = "dbcboChartType";
			this.dbcboChartType.Size = new System.Drawing.Size(121, 20);
			this.dbcboChartType.TabIndex = 0;
			// 
			// panelChoose
			// 
			this.panelChoose.Controls.Add(this.groupBoxClassify);
			this.panelChoose.Controls.Add(this.groupBoxCompare);
			this.panelChoose.Controls.Add(this.groupBoxTemplate);
			this.panelChoose.Controls.Add(this.groupBoxChartType);
			this.panelChoose.Controls.Add(this.groupBoxAnalysis);
			this.panelChoose.Location = new System.Drawing.Point(12, 14);
			this.panelChoose.Name = "panelChoose";
			this.panelChoose.Size = new System.Drawing.Size(331, 448);
			this.panelChoose.TabIndex = 2;
			// 
			// groupBoxClassify
			// 
			this.groupBoxClassify.Controls.Add(this.chkListClassify);
			this.groupBoxClassify.Location = new System.Drawing.Point(7, 206);
			this.groupBoxClassify.Name = "groupBoxClassify";
			this.groupBoxClassify.Size = new System.Drawing.Size(311, 175);
			this.groupBoxClassify.TabIndex = 6;
			this.groupBoxClassify.TabStop = false;
			this.groupBoxClassify.Text = "数值/分类选择:";
			// 
			// chkListClassify
			// 
			this.chkListClassify.CheckOnClick = true;
			this.chkListClassify.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkListClassify.FormattingEnabled = true;
			this.chkListClassify.Location = new System.Drawing.Point(3, 17);
			this.chkListClassify.Name = "chkListClassify";
			this.chkListClassify.Size = new System.Drawing.Size(305, 148);
			this.chkListClassify.TabIndex = 1;
			// 
			// groupBoxCompare
			// 
			this.groupBoxCompare.Controls.Add(this.dbcboCompare);
			this.groupBoxCompare.Location = new System.Drawing.Point(7, 206);
			this.groupBoxCompare.Name = "groupBoxCompare";
			this.groupBoxCompare.Size = new System.Drawing.Size(311, 49);
			this.groupBoxCompare.TabIndex = 4;
			this.groupBoxCompare.TabStop = false;
			this.groupBoxCompare.Text = "比较项:";
			this.groupBoxCompare.Visible = false;
			// 
			// dbcboCompare
			// 
			this.dbcboCompare.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dbcboCompare.FormattingEnabled = true;
			this.dbcboCompare.Location = new System.Drawing.Point(3, 17);
			this.dbcboCompare.Name = "dbcboCompare";
			this.dbcboCompare.Size = new System.Drawing.Size(305, 20);
			this.dbcboCompare.TabIndex = 0;
			this.dbcboCompare.SelectedIndexChanged += new System.EventHandler(this.dbcboCompare_SelectedIndexChanged);
			// 
			// groupBoxChartType
			// 
			this.groupBoxChartType.Controls.Add(this.dbcboChartType);
			this.groupBoxChartType.Location = new System.Drawing.Point(162, 77);
			this.groupBoxChartType.Name = "groupBoxChartType";
			this.groupBoxChartType.Size = new System.Drawing.Size(156, 123);
			this.groupBoxChartType.TabIndex = 2;
			this.groupBoxChartType.TabStop = false;
			this.groupBoxChartType.Text = "图形类型选择:";
			// 
			// groupBoxAnalysis
			// 
			this.groupBoxAnalysis.Controls.Add(this.rBtnDirect);
			this.groupBoxAnalysis.Controls.Add(this.rBtnCompare);
			this.groupBoxAnalysis.Controls.Add(this.rBtnAnalysis);
			this.groupBoxAnalysis.Location = new System.Drawing.Point(7, 74);
			this.groupBoxAnalysis.Name = "groupBoxAnalysis";
			this.groupBoxAnalysis.Size = new System.Drawing.Size(143, 126);
			this.groupBoxAnalysis.TabIndex = 0;
			this.groupBoxAnalysis.TabStop = false;
			this.groupBoxAnalysis.Text = "分析数据:";
			// 
			// rBtnAnalysis
			// 
			this.rBtnAnalysis.AutoSize = true;
			this.rBtnAnalysis.Checked = true;
			this.rBtnAnalysis.Location = new System.Drawing.Point(13, 25);
			this.rBtnAnalysis.Name = "rBtnAnalysis";
			this.rBtnAnalysis.Size = new System.Drawing.Size(71, 16);
			this.rBtnAnalysis.TabIndex = 0;
			this.rBtnAnalysis.TabStop = true;
			this.rBtnAnalysis.Text = "数据分布";
			this.rBtnAnalysis.UseVisualStyleBackColor = true;
			this.rBtnAnalysis.CheckedChanged += new System.EventHandler(this.rBtn_CheckedChanged);
			// 
			// ChartConfigure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(354, 501);
			this.Controls.Add(this.btnApplication);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.panelChoose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChartConfigure";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "配置数据";
			this.groupBoxTemplate.ResumeLayout(false);
			this.groupBoxTemplate.PerformLayout();
			this.panelChoose.ResumeLayout(false);
			this.groupBoxClassify.ResumeLayout(false);
			this.groupBoxCompare.ResumeLayout(false);
			this.groupBoxChartType.ResumeLayout(false);
			this.groupBoxAnalysis.ResumeLayout(false);
			this.groupBoxAnalysis.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.RadioButton rBtnDirect;
		private System.Windows.Forms.RadioButton rBtnCompare;
		private System.Windows.Forms.Button btnApplication;
		private System.Windows.Forms.GroupBox groupBoxTemplate;
		private System.Windows.Forms.Button butChoose;
		private System.Windows.Forms.ComboBox dbcboTemplate;
		private System.Windows.Forms.ComboBox dbcboChartType;
		private System.Windows.Forms.Panel panelChoose;
		private System.Windows.Forms.GroupBox groupBoxChartType;
		private System.Windows.Forms.GroupBox groupBoxAnalysis;
		private System.Windows.Forms.RadioButton rBtnAnalysis;
		private System.Windows.Forms.GroupBox groupBoxCompare;
		private System.Windows.Forms.ComboBox dbcboCompare;
		private System.Windows.Forms.GroupBox groupBoxClassify;
		private System.Windows.Forms.CheckedListBox chkListClassify;
	}
}