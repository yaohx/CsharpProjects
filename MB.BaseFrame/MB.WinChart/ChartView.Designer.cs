namespace MB.WinChart
{
	partial class ChartView
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartView));
			this.panelChart = new System.Windows.Forms.Panel();
			this.panelTool = new System.Windows.Forms.Panel();
			this.toolStripBar = new System.Windows.Forms.ToolStrip();
			this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
			this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
			this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsBtnShowValue = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsDbcboPointStyle = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsBtn3D = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsLabXAxisAngle = new System.Windows.Forms.ToolStripLabel();
			this.tsDbcboXAxisAngle = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsBtnOption = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
			this.panelTool.SuspendLayout();
			this.toolStripBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelChart
			// 
			this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelChart.Location = new System.Drawing.Point(0, 0);
			this.panelChart.Name = "panelChart";
			this.panelChart.Size = new System.Drawing.Size(800, 307);
			this.panelChart.TabIndex = 13;
			// 
			// panelTool
			// 
			this.panelTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(224)))));
			this.panelTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTool.Controls.Add(this.toolStripBar);
			this.panelTool.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTool.Location = new System.Drawing.Point(0, 0);
			this.panelTool.Name = "panelTool";
			this.panelTool.Size = new System.Drawing.Size(800, 32);
			this.panelTool.TabIndex = 14;
			// 
			// toolStripBar
			// 
			this.toolStripBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.toolStripSeparator7,
            this.tsBtnShowValue,
            this.toolStripSeparator2,
            this.tsDbcboPointStyle,
            this.toolStripSeparator1,
            this.tsBtn3D,
            this.toolStripSeparator4,
            this.tsLabXAxisAngle,
            this.tsDbcboXAxisAngle,
            this.toolStripSeparator5,
            this.tsBtnOption,
            this.toolStripSeparator3,
            this.tsBtnExport});
			this.toolStripBar.Location = new System.Drawing.Point(0, 0);
			this.toolStripBar.Name = "toolStripBar";
			this.toolStripBar.Size = new System.Drawing.Size(798, 30);
			this.toolStripBar.TabIndex = 0;
			this.toolStripBar.Text = "toolStrip1";
			// 
			// bindingNavigatorMoveFirstItem
			// 
			this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
			this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
			this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 27);
			this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
			this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
			// 
			// bindingNavigatorMovePreviousItem
			// 
			this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
			this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
			this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 27);
			this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
			this.bindingNavigatorMovePreviousItem.Click += new System.EventHandler(this.bindingNavigatorMovePreviousItem_Click);
			// 
			// bindingNavigatorPositionItem
			// 
			this.bindingNavigatorPositionItem.AccessibleName = "位置";
			this.bindingNavigatorPositionItem.AutoSize = false;
			this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
			this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
			this.bindingNavigatorPositionItem.Text = "0";
			this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
			this.bindingNavigatorPositionItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bindingNavigatorPositionItem_KeyDown);
			// 
			// bindingNavigatorCountItem
			// 
			this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
			this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 27);
			this.bindingNavigatorCountItem.Text = "/ {0}";
			this.bindingNavigatorCountItem.ToolTipText = "总项数";
			// 
			// bindingNavigatorMoveNextItem
			// 
			this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
			this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
			this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 27);
			this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
			this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
			// 
			// bindingNavigatorMoveLastItem
			// 
			this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
			this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
			this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 27);
			this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
			this.bindingNavigatorMoveLastItem.Click += new System.EventHandler(this.bindingNavigatorMoveLastItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 30);
			// 
			// tsBtnShowValue
			// 
			this.tsBtnShowValue.Checked = true;
			this.tsBtnShowValue.CheckOnClick = true;
			this.tsBtnShowValue.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsBtnShowValue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBtnShowValue.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnShowValue.Image")));
			this.tsBtnShowValue.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBtnShowValue.Name = "tsBtnShowValue";
			this.tsBtnShowValue.Size = new System.Drawing.Size(23, 27);
			this.tsBtnShowValue.Text = "显示数值";
			this.tsBtnShowValue.CheckedChanged += new System.EventHandler(this.tsBtnShowValue_CheckedChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
			// 
			// tsDbcboPointStyle
			// 
			this.tsDbcboPointStyle.AutoSize = false;
			this.tsDbcboPointStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsDbcboPointStyle.Items.AddRange(new object[] {
            "单位",
            "货币",
            "整型货币",
            "科学计数",
            "整型科学计数",
            "百分比"});
			this.tsDbcboPointStyle.Name = "tsDbcboPointStyle";
			this.tsDbcboPointStyle.Size = new System.Drawing.Size(100, 20);
			this.tsDbcboPointStyle.SelectedIndexChanged += new System.EventHandler(this.dbcboPointStyle_SelectedIndexChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
			// 
			// tsBtn3D
			// 
			this.tsBtn3D.Checked = true;
			this.tsBtn3D.CheckOnClick = true;
			this.tsBtn3D.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsBtn3D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBtn3D.Image = ((System.Drawing.Image)(resources.GetObject("tsBtn3D.Image")));
			this.tsBtn3D.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBtn3D.Name = "tsBtn3D";
			this.tsBtn3D.Size = new System.Drawing.Size(23, 27);
			this.tsBtn3D.Text = "3D";
			this.tsBtn3D.CheckedChanged += new System.EventHandler(this.tsBtn3D_CheckedChanged);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 30);
			// 
			// tsLabXAxisAngle
			// 
			this.tsLabXAxisAngle.Name = "tsLabXAxisAngle";
			this.tsLabXAxisAngle.Size = new System.Drawing.Size(53, 27);
			this.tsLabXAxisAngle.Text = "X轴角度:";
			// 
			// tsDbcboXAxisAngle
			// 
			this.tsDbcboXAxisAngle.AutoSize = false;
			this.tsDbcboXAxisAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsDbcboXAxisAngle.Items.AddRange(new object[] {
            "0",
            "30",
            "60",
            "90",
            "-30",
            "-60",
            "-90"});
			this.tsDbcboXAxisAngle.Name = "tsDbcboXAxisAngle";
			this.tsDbcboXAxisAngle.Size = new System.Drawing.Size(40, 20);
			this.tsDbcboXAxisAngle.SelectedIndexChanged += new System.EventHandler(this.tsDbcboXAxisAngle_SelectedIndexChanged);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 30);
			// 
			// tsBtnOption
			// 
			this.tsBtnOption.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnOption.Image")));
			this.tsBtnOption.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBtnOption.Name = "tsBtnOption";
			this.tsBtnOption.Size = new System.Drawing.Size(73, 27);
			this.tsBtnOption.Text = "高级选项";
			this.tsBtnOption.Click += new System.EventHandler(this.tsBtnOption_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
			// 
			// tsBtnExport
			// 
			this.tsBtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBtnExport.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExport.Image")));
			this.tsBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBtnExport.Name = "tsBtnExport";
			this.tsBtnExport.Size = new System.Drawing.Size(23, 27);
			this.tsBtnExport.Text = "导出报表";
			this.tsBtnExport.Click += new System.EventHandler(this.tsBtnExport_Click);
			// 
			// ChartView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.panelTool);
			this.Controls.Add(this.panelChart);
			this.Name = "ChartView";
			this.Size = new System.Drawing.Size(800, 307);
			this.Load += new System.EventHandler(this.ChartView_Load);
			this.panelTool.ResumeLayout(false);
			this.panelTool.PerformLayout();
			this.toolStripBar.ResumeLayout(false);
			this.toolStripBar.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelChart;
		private System.Windows.Forms.Panel panelTool;
		private System.Windows.Forms.ToolStrip toolStripBar;
		private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripButton tsBtnShowValue;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripComboBox tsDbcboPointStyle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsBtn3D;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripLabel tsLabXAxisAngle;
		private System.Windows.Forms.ToolStripComboBox tsDbcboXAxisAngle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton tsBtnOption;
		private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
		private System.Windows.Forms.ToolStripButton tsBtnExport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

	}
}
