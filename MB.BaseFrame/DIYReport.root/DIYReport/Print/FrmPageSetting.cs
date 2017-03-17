//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	打印页面设置 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DIYReport.Print {
	/// <summary>
	/// FrmPageSetting 打印页面设置。
	/// </summary>
	public class FrmPageSetting : System.Windows.Forms.Form {
		#region 内部自动生成代码...
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button butQuit;
		private System.Windows.Forms.Button butSure;
		private System.Windows.Forms.TabPage tabMarge;
		private System.Windows.Forms.TabPage tabPageSize;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cobPageSize;
		private System.Windows.Forms.RadioButton radOrientationH;
		private System.Windows.Forms.RadioButton radOrientationV;
		private System.Windows.Forms.NumericUpDown nudBottom;
		private System.Windows.Forms.NumericUpDown nudRight;
		private System.Windows.Forms.NumericUpDown nudLeft;
		private System.Windows.Forms.NumericUpDown nudTop;
		private System.Windows.Forms.NumericUpDown nudHeight;
		private System.Windows.Forms.NumericUpDown nudWidth;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmPageSetting));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabMarge = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radOrientationH = new System.Windows.Forms.RadioButton();
			this.radOrientationV = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.nudTop = new System.Windows.Forms.NumericUpDown();
			this.nudLeft = new System.Windows.Forms.NumericUpDown();
			this.nudRight = new System.Windows.Forms.NumericUpDown();
			this.nudBottom = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPageSize = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.nudWidth = new System.Windows.Forms.NumericUpDown();
			this.nudHeight = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cobPageSize = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.butSure = new System.Windows.Forms.Button();
			this.butQuit = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabMarge.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBottom)).BeginInit();
			this.tabPageSize.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabMarge);
			this.tabControl1.Controls.Add(this.tabPageSize);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(362, 240);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabMarge
			// 
			this.tabMarge.Controls.Add(this.groupBox2);
			this.tabMarge.Controls.Add(this.groupBox1);
			this.tabMarge.Location = new System.Drawing.Point(4, 21);
			this.tabMarge.Name = "tabMarge";
			this.tabMarge.Size = new System.Drawing.Size(354, 215);
			this.tabMarge.TabIndex = 0;
			this.tabMarge.Text = "页边距";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radOrientationH);
			this.groupBox2.Controls.Add(this.radOrientationV);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(8, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(336, 64);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "方向：";
			// 
			// radOrientationH
			// 
			this.radOrientationH.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radOrientationH.Location = new System.Drawing.Point(96, 32);
			this.radOrientationH.Name = "radOrientationH";
			this.radOrientationH.TabIndex = 0;
			this.radOrientationH.Text = "横向(&S)";
			// 
			// radOrientationV
			// 
			this.radOrientationV.Checked = true;
			this.radOrientationV.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radOrientationV.Location = new System.Drawing.Point(16, 32);
			this.radOrientationV.Name = "radOrientationV";
			this.radOrientationV.Size = new System.Drawing.Size(72, 24);
			this.radOrientationV.TabIndex = 1;
			this.radOrientationV.TabStop = true;
			this.radOrientationV.Text = "纵向(&P)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.nudTop);
			this.groupBox1.Controls.Add(this.nudLeft);
			this.groupBox1.Controls.Add(this.nudRight);
			this.groupBox1.Controls.Add(this.nudBottom);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(336, 80);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "页边距(毫米)：";
			// 
			// nudTop
			// 
			this.nudTop.Location = new System.Drawing.Point(56, 24);
			this.nudTop.Maximum = new System.Decimal(new int[] {
																   500,
																   0,
																   0,
																   0});
			this.nudTop.Name = "nudTop";
			this.nudTop.Size = new System.Drawing.Size(80, 21);
			this.nudTop.TabIndex = 7;
			this.nudTop.Value = new System.Decimal(new int[] {
																 25,
																 0,
																 0,
																 0});
			// 
			// nudLeft
			// 
			this.nudLeft.Location = new System.Drawing.Point(56, 48);
			this.nudLeft.Maximum = new System.Decimal(new int[] {
																	500,
																	0,
																	0,
																	0});
			this.nudLeft.Name = "nudLeft";
			this.nudLeft.Size = new System.Drawing.Size(80, 21);
			this.nudLeft.TabIndex = 6;
			this.nudLeft.Value = new System.Decimal(new int[] {
																  30,
																  0,
																  0,
																  0});
			// 
			// nudRight
			// 
			this.nudRight.Location = new System.Drawing.Point(240, 48);
			this.nudRight.Maximum = new System.Decimal(new int[] {
																	 500,
																	 0,
																	 0,
																	 0});
			this.nudRight.Name = "nudRight";
			this.nudRight.Size = new System.Drawing.Size(88, 21);
			this.nudRight.TabIndex = 5;
			this.nudRight.Value = new System.Decimal(new int[] {
																   30,
																   0,
																   0,
																   0});
			// 
			// nudBottom
			// 
			this.nudBottom.Location = new System.Drawing.Point(240, 24);
			this.nudBottom.Maximum = new System.Decimal(new int[] {
																	  500,
																	  0,
																	  0,
																	  0});
			this.nudBottom.Name = "nudBottom";
			this.nudBottom.Size = new System.Drawing.Size(88, 21);
			this.nudBottom.TabIndex = 4;
			this.nudBottom.Value = new System.Decimal(new int[] {
																	25,
																	0,
																	0,
																	0});
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(200, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "右(&R):";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "左(&L):";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(200, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "下(&B):";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "上(&T):";
			// 
			// tabPageSize
			// 
			this.tabPageSize.Controls.Add(this.groupBox3);
			this.tabPageSize.Location = new System.Drawing.Point(4, 21);
			this.tabPageSize.Name = "tabPageSize";
			this.tabPageSize.Size = new System.Drawing.Size(354, 215);
			this.tabPageSize.TabIndex = 1;
			this.tabPageSize.Text = "纸张";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.nudWidth);
			this.groupBox3.Controls.Add(this.nudHeight);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.cobPageSize);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(16, 16);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(328, 112);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "纸张大小(毫米)：";
			// 
			// nudWidth
			// 
			this.nudWidth.Location = new System.Drawing.Point(128, 56);
			this.nudWidth.Maximum = new System.Decimal(new int[] {
																	 500,
																	 0,
																	 0,
																	 0});
			this.nudWidth.Name = "nudWidth";
			this.nudWidth.Size = new System.Drawing.Size(96, 21);
			this.nudWidth.TabIndex = 6;
			this.nudWidth.Value = new System.Decimal(new int[] {
																   210,
																   0,
																   0,
																   0});
			// 
			// nudHeight
			// 
			this.nudHeight.Location = new System.Drawing.Point(128, 80);
			this.nudHeight.Maximum = new System.Decimal(new int[] {
																	  500,
																	  0,
																	  0,
																	  0});
			this.nudHeight.Name = "nudHeight";
			this.nudHeight.Size = new System.Drawing.Size(96, 21);
			this.nudHeight.TabIndex = 5;
			this.nudHeight.Value = new System.Decimal(new int[] {
																	297,
																	0,
																	0,
																	0});
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 16);
			this.label6.TabIndex = 4;
			this.label6.Text = "高度(&E):";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 16);
			this.label5.TabIndex = 3;
			this.label5.Text = "宽度(&W):";
			// 
			// cobPageSize
			// 
			this.cobPageSize.Location = new System.Drawing.Point(16, 24);
			this.cobPageSize.Name = "cobPageSize";
			this.cobPageSize.Size = new System.Drawing.Size(208, 20);
			this.cobPageSize.TabIndex = 0;
			this.cobPageSize.SelectedIndexChanged += new System.EventHandler(this.cobPageSize_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Teal;
			this.panel1.Controls.Add(this.butSure);
			this.panel1.Controls.Add(this.butQuit);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 200);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(362, 40);
			this.panel1.TabIndex = 1;
			// 
			// butSure
			// 
			this.butSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSure.Location = new System.Drawing.Point(186, 8);
			this.butSure.Name = "butSure";
			this.butSure.Size = new System.Drawing.Size(80, 24);
			this.butSure.TabIndex = 1;
			this.butSure.Text = "确定(&S)";
			this.butSure.Click += new System.EventHandler(this.butSure_Click);
			// 
			// butQuit
			// 
			this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butQuit.Location = new System.Drawing.Point(274, 8);
			this.butQuit.Name = "butQuit";
			this.butQuit.Size = new System.Drawing.Size(80, 24);
			this.butQuit.TabIndex = 0;
			this.butQuit.Text = "取消(&Q)";
			this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
			// 
			// FrmPageSetting
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(362, 240);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmPageSetting";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "页面设置";
			this.tabControl1.ResumeLayout(false);
			this.tabMarge.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBottom)).EndInit();
			this.tabPageSize.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		#region 变量定义...
		private static decimal SEP_MINI = System.Convert.ToDecimal(3.938) ;//System.Convert.ToDecimal(72 / 25.4f);
		private DIYReport.ReportModel.RptReport _Report;

		#endregion 变量定义...

		#region 构造函数...
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pReport"></param>
		public FrmPageSetting(DIYReport.ReportModel.RptReport pReport) {
			InitializeComponent();
			_Report = pReport;
			addToPaperName();
			iniPageSetting();
		}
		#endregion 构造函数...


		private void butQuit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void butSure_Click(object sender, System.EventArgs e) {
			bool b = checkedPageSetting();
			if(b){
				PageSettings pageSet = new PageSettings();
			
				Margins mag = new Margins(System.Convert.ToInt32(nudLeft.Value * SEP_MINI),System.Convert.ToInt32(nudRight.Value  * SEP_MINI) ,
					System.Convert.ToInt32(nudTop.Value  * SEP_MINI),System.Convert.ToInt32(nudBottom.Value  * SEP_MINI ));

				PaperSize size = RptPageSetting.GetPaperSizeByName(_Report.PrintDocument,_Report.PrintName , cobPageSize.Text.Trim());//   new PaperSize(cobPageSize.Text,System.Convert.ToInt32(nudWidth.Value  * SEP_MINI) ,System.Convert.ToInt32(nudHeight.Value   * SEP_MINI));
				if(size.Kind == PaperKind.Custom){
					size.Width = System.Convert.ToInt32(nudWidth.Value  * SEP_MINI);
					size.Height = System.Convert.ToInt32(nudHeight.Value   * SEP_MINI);
				}
			
				_Report.PaperSize = size;
				_Report.Margins = mag;
				_Report.IsLandscape = radOrientationH.Checked;

				PageSettings setting = new PageSettings();
				setting.Margins = _Report.Margins;
				setting.Landscape = _Report.IsLandscape;
				 
				setting.PaperSize = _Report.PaperSize;
				//setting..PrinterSettings 
				//setting.PrinterSettings.
				//setting.PaperSize.Kind = PaperKind.A3;  
				 
				_Report.PrintDocument.DefaultPageSettings = setting;
				this.Close(); 
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e) {
			PaperSize size = new PaperSize(cobPageSize.Text,System.Convert.ToInt32(nudWidth.Value  * SEP_MINI) ,System.Convert.ToInt32(nudHeight.Value   * SEP_MINI));
			setPaperSize(size);
		}
		#region 其它内部处理的函数...
		//检查边距和纸张大小的设置是否合法
		private bool checkedPageSetting(){
			if(nudLeft.Value > 500 || nudRight.Value  > 500){
				MessageBox.Show("左边距或者右边距设置过大，请重新设置.","操作提示");
				return false;
			}
			if(nudTop.Value > 500 || nudBottom.Value  > 500){
				MessageBox.Show("上边距或者下边距设置过大，请重新设置.","操作提示");
				return false;
			}
			if(nudLeft.Value  + nudRight.Value  >= nudWidth.Value){
				MessageBox.Show("左边距和右边距设置重叠，请重新设置.","操作提示");
				return false;
			}
			if(nudTop.Value  + nudBottom.Value  >= nudHeight.Value){
				MessageBox.Show("上边距和下边距设置重叠，请重新设置.","操作提示");
				return false;
			}
			return true;												   
		}
		//初始化操作的界面
		private void iniPageSetting(){
			Margins mag = _Report.Margins;
			PaperSize size = _Report.PaperSize; 
			radOrientationH.Checked = _Report.IsLandscape ;
			radOrientationV.Checked = !_Report.IsLandscape ;

			nudLeft.Value  = mag.Left  /SEP_MINI ;
			nudRight.Value = mag.Right   /SEP_MINI;
			nudBottom.Value = mag.Bottom  /SEP_MINI;
			nudTop.Value = mag.Top  /SEP_MINI;
		
			cobPageSize.Text = size.PaperName; 

			setPaperSize(size);

		}
		//初始化纸张类型
		private void addToPaperName(){			 
			cobPageSize.Items.Clear();

//			 
//			System.Drawing.Printing.PrintDocument doc;
//			doc.PrinterSettings.PaperSizes[0].Kind = PaperKind. 
//			foreach(string  print in System.Drawing.Printing.PrinterSettings.InstalledPrinters){
//				doc.PrinterSettings.PrinterName = print;
//				
//				for(int i  = 0; i < doc.PrinterSettings.PaperSizes.Count;i ++){
//					if(cobPageSize.Items.IndexOf(doc.PrinterSettings.PaperSizes[i].PaperName ) < 0){
//						cobPageSize.Items.Add(doc.PrinterSettings.PaperSizes[i].PaperName);
//					}
//				}
//			}

			IList aList = getAllPaperInfo();
			for(int i = 0; i< aList.Count ; i++){
				cobPageSize.Items.Add(aList[i]);
			}
 			cobPageSize.SelectedIndex = 1;
			  

		}
		//自定义所有size 
		private IList getAllPaperInfo(){
			ArrayList alist = new ArrayList();
			alist.Add(new PaperSizeInfo("A3",297,420)); 
			alist.Add(new PaperSizeInfo("A4",210,297)); 
			alist.Add(new PaperSizeInfo("A5",148,210)); 
			alist.Add(new PaperSizeInfo("16开(18.4 * 26 厘米)",184,260)); 
			alist.Add(new PaperSizeInfo("32开(13 * 18.4 厘米)",130,184)); 
			alist.Add(new PaperSizeInfo("自定义",210,297)); 
			//System.Drawing.Printing.PrintDocument doc = new PrintDocument();
			return alist;
		}
		//设置显示的值 
		private void setPaperSize(PaperSize pSize){
			PaperSize size = pSize; 
			int width = radOrientationV.Checked ?size.Height : size.Width;
			int height = radOrientationH.Checked?size.Width  : size.Height;
			nudWidth.Value = size.Width /SEP_MINI;
			nudHeight.Value = size.Height/SEP_MINI;
		}
		#endregion 其它内部处理的函数...

		private void cobPageSize_SelectedIndexChanged(object sender, System.EventArgs e) {
			PaperSizeInfo info  = cobPageSize.SelectedItem as PaperSizeInfo;
			if(info!=null){
				nudWidth.Value = info.Width;
				nudHeight.Value = info.Height ;
			}
			bool b = info.PaperName =="自定义";
			nudWidth.Enabled = b;
			nudHeight.Enabled = b;
		}
	}

	#region Papers 处理...
	class PaperSizeInfo{
		public string PaperName;
		public int Width;
		public int Height;
		public PaperSizeInfo(string pPaperName,int pWidth,int pHeight){
			PaperName = pPaperName;
			Width = pWidth;
			Height = pHeight;
		}
		public override string ToString() {
			return PaperName;
		}

	}
	#endregion PaperName 处理...
}


