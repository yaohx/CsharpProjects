//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DIYReport.UserDIY
{
	/// <summary>
	/// FrmEditCssClass 编辑打印对象的样式。
	/// </summary>
	public class FrmEditCssClass : System.Windows.Forms.Form
	{
		#region 内部自动生成代码...
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ListBox lstCssName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Panel panProperty;
		private System.Windows.Forms.PropertyGrid pGridMain;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butQuit;
		private System.Windows.Forms.Button butSave;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panProperty = new System.Windows.Forms.Panel();
			this.lstCssName = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAdd = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.butQuit = new System.Windows.Forms.Button();
			this.butSave = new System.Windows.Forms.Button();
			this.pGridMain = new System.Windows.Forms.PropertyGrid();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panProperty.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.butSave);
			this.panel1.Controls.Add(this.butQuit);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 334);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 40);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.butDelete);
			this.panel2.Controls.Add(this.butAdd);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.lstCssName);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(184, 334);
			this.panel2.TabIndex = 1;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(184, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(4, 334);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// panProperty
			// 
			this.panProperty.Controls.Add(this.pGridMain);
			this.panProperty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panProperty.Location = new System.Drawing.Point(188, 0);
			this.panProperty.Name = "panProperty";
			this.panProperty.Size = new System.Drawing.Size(196, 334);
			this.panProperty.TabIndex = 3;
			// 
			// lstCssName
			// 
			this.lstCssName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstCssName.ItemHeight = 12;
			this.lstCssName.Location = new System.Drawing.Point(8, 32);
			this.lstCssName.Name = "lstCssName";
			this.lstCssName.Size = new System.Drawing.Size(168, 256);
			this.lstCssName.TabIndex = 0;
			this.lstCssName.SelectedIndexChanged += new System.EventHandler(this.lstCssName_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "样式名称：";
			// 
			// butAdd
			// 
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAdd.Location = new System.Drawing.Point(8, 304);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(80, 24);
			this.butAdd.TabIndex = 2;
			this.butAdd.Text = "增加(&A)";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDelete.Location = new System.Drawing.Point(96, 304);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(72, 24);
			this.butDelete.TabIndex = 3;
			this.butDelete.Text = "删除(&D)";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butQuit
			// 
			this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butQuit.Location = new System.Drawing.Point(304, 8);
			this.butQuit.Name = "butQuit";
			this.butQuit.Size = new System.Drawing.Size(72, 24);
			this.butQuit.TabIndex = 0;
			this.butQuit.Text = "关闭(&Q)";
			this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
			// 
			// butSave
			// 
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSave.Location = new System.Drawing.Point(224, 8);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(72, 24);
			this.butSave.TabIndex = 1;
			this.butSave.Text = "应用(&S)";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// pGridMain
			// 
			this.pGridMain.CommandsVisibleIfAvailable = true;
			this.pGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pGridMain.LargeButtons = false;
			this.pGridMain.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pGridMain.Location = new System.Drawing.Point(0, 0);
			this.pGridMain.Name = "pGridMain";
			this.pGridMain.Size = new System.Drawing.Size(196, 334);
			this.pGridMain.TabIndex = 0;
			this.pGridMain.Text = "pGridMain";
			this.pGridMain.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pGridMain.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.pGridMain.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pGridMain_PropertyValueChanged);
			// 
			// FrmEditCssClass
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(384, 374);
			this.Controls.Add(this.panProperty);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmEditCssClass";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "报表对象样式设置";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panProperty.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		private DIYReport.ReportModel.RptCssClassList _CssList;

		#region 构造函数...
		public FrmEditCssClass(DIYReport.ReportModel.RptCssClassList pCssList) {
			InitializeComponent();
			
			_CssList = pCssList;
			addCssToListBox();
		}
		#endregion 构造函数...
  
		//初始化css的数据...
		private void addCssToListBox(){
			lstCssName.Items.Clear(); 
			foreach(DIYReport.ReportModel.RptCssClass css in _CssList){
				lstCssName.Items.Add(css.Name ); 
			}
		}
		//
		private void addCss(){
			string cssName = _CssList.GetNewCssName() ;
			DIYReport.ReportModel.RptCssClass css = new DIYReport.ReportModel.RptCssClass(cssName);
			_CssList.Add(css);
			lstCssName.Items.Add(css.Name );
		}
		private void removeCss(){
			if(lstCssName.Items.Count > 0 && lstCssName.SelectedItem!=null){
				_CssList.Remove(((DIYReport.ReportModel.RptCssClass)lstCssName.SelectedItem).Key);
				lstCssName.Items.Remove(lstCssName.SelectedItem);
			}
		}

		private void lstCssName_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(lstCssName.SelectedItem!=null){
				pGridMain.SelectedObject = _CssList.GetCssByName(lstCssName.SelectedItem.ToString())  ;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			removeCss();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			addCss();
		}

		private void pGridMain_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e) {
			//处理重名的情况
			if(e.ChangedItem.Label == "Name"){
				if(e.ChangedItem.Value.ToString()!=e.OldValue.ToString()){
					if(_CssList[e.ChangedItem.Value.ToString()]!=null){
						MessageBox.Show("该名称已经存在，请重新命名。","操作提示"); 
						_CssList.GetCssByName(lstCssName.SelectedItem.ToString()).Name = e.OldValue.ToString() ;
					}
					lstCssName.Items.Insert(lstCssName.SelectedIndex, e.ChangedItem.Value.ToString());
					lstCssName.Items.Remove(lstCssName.SelectedItem);
				}
			}
		}

		private void butQuit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void butSave_Click(object sender, System.EventArgs e) {
			
		}

	}
}
