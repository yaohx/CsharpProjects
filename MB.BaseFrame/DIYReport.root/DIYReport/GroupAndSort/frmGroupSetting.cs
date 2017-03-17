//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-04-19
// Description	:	组级字段分组间隔设置。
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DIYReport.GroupAndSort
{
	/// <summary>
	/// frmGroupSetting 组级字段分组间隔设置。
	/// </summary>
	public class frmGroupSetting : System.Windows.Forms.Form
	{
		#region 内部自动生成代码...
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butSure;
		private System.Windows.Forms.Button butQuit;
		private System.Windows.Forms.Label labField1;
		private System.Windows.Forms.ComboBox cobDivide1;
		private System.Windows.Forms.Label labField2;
		private System.Windows.Forms.Label labField3;
		private System.Windows.Forms.Label labField4;
		private System.Windows.Forms.ComboBox cobDivide2;
		private System.Windows.Forms.ComboBox cobDivide3;
		private System.Windows.Forms.ComboBox cobDivide4;
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labField1 = new System.Windows.Forms.Label();
			this.cobDivide1 = new System.Windows.Forms.ComboBox();
			this.butSure = new System.Windows.Forms.Button();
			this.butQuit = new System.Windows.Forms.Button();
			this.labField2 = new System.Windows.Forms.Label();
			this.labField3 = new System.Windows.Forms.Label();
			this.labField4 = new System.Windows.Forms.Label();
			this.cobDivide2 = new System.Windows.Forms.ComboBox();
			this.cobDivide3 = new System.Windows.Forms.ComboBox();
			this.cobDivide4 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.Navy;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "请为组级字段选定分组间隔：";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "组级字段：";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(176, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 24);
			this.label3.TabIndex = 2;
			this.label3.Text = "分组间隔：";
			// 
			// labField1
			// 
			this.labField1.BackColor = System.Drawing.Color.White;
			this.labField1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labField1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labField1.Location = new System.Drawing.Point(8, 64);
			this.labField1.Name = "labField1";
			this.labField1.Size = new System.Drawing.Size(136, 24);
			this.labField1.TabIndex = 3;
			this.labField1.Text = "label4";
			// 
			// cobDivide1
			// 
			this.cobDivide1.Location = new System.Drawing.Point(176, 64);
			this.cobDivide1.Name = "cobDivide1";
			this.cobDivide1.Size = new System.Drawing.Size(136, 20);
			this.cobDivide1.TabIndex = 4;
			this.cobDivide1.Text = "cobDivide";
			// 
			// butSure
			// 
			this.butSure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSure.Location = new System.Drawing.Point(336, 64);
			this.butSure.Name = "butSure";
			this.butSure.Size = new System.Drawing.Size(88, 24);
			this.butSure.TabIndex = 5;
			this.butSure.Text = "确定(&S)";
			this.butSure.Click += new System.EventHandler(this.butSure_Click);
			// 
			// butQuit
			// 
			this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butQuit.Location = new System.Drawing.Point(336, 96);
			this.butQuit.Name = "butQuit";
			this.butQuit.Size = new System.Drawing.Size(88, 24);
			this.butQuit.TabIndex = 6;
			this.butQuit.Text = "取消(&Q)";
			this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
			// 
			// labField2
			// 
			this.labField2.BackColor = System.Drawing.Color.White;
			this.labField2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labField2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labField2.Location = new System.Drawing.Point(8, 96);
			this.labField2.Name = "labField2";
			this.labField2.Size = new System.Drawing.Size(136, 24);
			this.labField2.TabIndex = 7;
			this.labField2.Text = "label4";
			// 
			// labField3
			// 
			this.labField3.BackColor = System.Drawing.Color.White;
			this.labField3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labField3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labField3.Location = new System.Drawing.Point(8, 128);
			this.labField3.Name = "labField3";
			this.labField3.Size = new System.Drawing.Size(136, 24);
			this.labField3.TabIndex = 8;
			this.labField3.Text = "label4";
			// 
			// labField4
			// 
			this.labField4.BackColor = System.Drawing.Color.White;
			this.labField4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labField4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labField4.Location = new System.Drawing.Point(8, 160);
			this.labField4.Name = "labField4";
			this.labField4.Size = new System.Drawing.Size(136, 24);
			this.labField4.TabIndex = 9;
			this.labField4.Text = "label4";
			// 
			// cobDivide2
			// 
			this.cobDivide2.Location = new System.Drawing.Point(176, 96);
			this.cobDivide2.Name = "cobDivide2";
			this.cobDivide2.Size = new System.Drawing.Size(136, 20);
			this.cobDivide2.TabIndex = 10;
			this.cobDivide2.Text = "comboBox1";
			// 
			// cobDivide3
			// 
			this.cobDivide3.Location = new System.Drawing.Point(176, 128);
			this.cobDivide3.Name = "cobDivide3";
			this.cobDivide3.Size = new System.Drawing.Size(136, 20);
			this.cobDivide3.TabIndex = 11;
			this.cobDivide3.Text = "comboBox2";
			// 
			// cobDivide4
			// 
			this.cobDivide4.Location = new System.Drawing.Point(176, 160);
			this.cobDivide4.Name = "cobDivide4";
			this.cobDivide4.Size = new System.Drawing.Size(136, 20);
			this.cobDivide4.TabIndex = 12;
			this.cobDivide4.Text = "comboBox3";
			// 
			// frmGroupSetting
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(440, 190);
			this.ControlBox = false;
			this.Controls.Add(this.cobDivide4);
			this.Controls.Add(this.cobDivide3);
			this.Controls.Add(this.cobDivide2);
			this.Controls.Add(this.labField4);
			this.Controls.Add(this.labField3);
			this.Controls.Add(this.labField2);
			this.Controls.Add(this.butQuit);
			this.Controls.Add(this.butSure);
			this.Controls.Add(this.cobDivide1);
			this.Controls.Add(this.labField1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmGroupSetting";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "分组间隔";
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		#region 变量定义...
		private IList _FieldLst;
		private IList _LabFields;
		private IList _CobDivides;
		#endregion 变量定义...

		#region 构造函数...
		public frmGroupSetting(IList pFieldLst) {
			InitializeComponent();
			_FieldLst = pFieldLst;
			_LabFields = new ArrayList(); 
			_LabFields.Add(labField1 );
			_LabFields.Add(labField2 );
			_LabFields.Add(labField3 );
			_LabFields.Add(labField4 );
			_CobDivides = new ArrayList();
			_CobDivides.Add(cobDivide1); 
			_CobDivides.Add(cobDivide2);
			_CobDivides.Add(cobDivide3);
			_CobDivides.Add(cobDivide4);

			foreach(Label lab in _LabFields){
				lab.Visible = false;
			}
			foreach(ComboBox box in _CobDivides){
				box.Tag = null;
				box.Visible = false;
			}
			dispGroupField();

		}
		#endregion 构造函数...
		
		#region 界面显示控制...
		//显示分组的字段
		private void dispGroupField(){
			int i = 0;
			foreach(RptFieldInfo info in _FieldLst){
				if(info.IsGroup){
					Label lab = _LabFields[i] as Label;
					lab.Visible = true;
					ComboBox box = _CobDivides[i] as ComboBox ;
					box.Visible = true;
					lab.Text = info.Description; 
					box.Tag = info;
					addToCobDivides(box,info);
					i ++;
				}
			}
		}
		//增加间隔的描述信息到下拉列表框中
		private void addToCobDivides(ComboBox pBox,RptFieldInfo pFieldInfo){
			string[] vals = GroupDivide.GetDivideTextByType(pFieldInfo.DataType);
			pBox.Items.Clear(); 
			foreach(string val in vals){
				pBox.Items.Add(val);
			}
			if(pFieldInfo.DivideName!=null &&  pFieldInfo.DivideName!=""){
				pBox.Text = pFieldInfo.DivideName;
			}
			else{
				pBox.SelectedIndex = 0;
			}
		}
		//获取组级分隔设置的信息
		private void getDivideInfo(){
			foreach(ComboBox box in _CobDivides){
				if(box.Tag !=null){
					RptFieldInfo info = box.Tag as RptFieldInfo;
					info.DivideName = box.Text ;
				}
			}
		}
		#endregion 界面显示控制...

		private void butQuit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void butSure_Click(object sender, System.EventArgs e) {
			getDivideInfo();
			this.Close();
		}
	}
}
