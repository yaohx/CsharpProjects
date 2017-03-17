//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-26
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DIYReport.UserDIY
{
	/// <summary>
	/// FrmArrowOperate 设计的控件方向控制
	/// </summary>
	public class FrmArrowOperate : System.Windows.Forms.Form
	{
		#region 内部自动生成代码...
		private System.Windows.Forms.Button butTop;
		private System.Windows.Forms.Button butRight;
		private System.Windows.Forms.Button butBottom;
		private System.Windows.Forms.Button butLeft;
		private System.Windows.Forms.CheckBox chkIsSize;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmArrowOperate));
			this.butTop = new System.Windows.Forms.Button();
			this.butRight = new System.Windows.Forms.Button();
			this.butBottom = new System.Windows.Forms.Button();
			this.butLeft = new System.Windows.Forms.Button();
			this.chkIsSize = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butTop
			// 
			this.butTop.Image = ((System.Drawing.Image)(resources.GetObject("butTop.Image")));
			this.butTop.Location = new System.Drawing.Point(24, 0);
			this.butTop.Name = "butTop";
			this.butTop.Size = new System.Drawing.Size(16, 24);
			this.butTop.TabIndex = 0;
			this.butTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butTop_MouseDown);
			// 
			// butRight
			// 
			this.butRight.Image = ((System.Drawing.Image)(resources.GetObject("butRight.Image")));
			this.butRight.Location = new System.Drawing.Point(40, 24);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(24, 16);
			this.butRight.TabIndex = 1;
			this.butRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butRight_MouseDown);
			// 
			// butBottom
			// 
			this.butBottom.Image = ((System.Drawing.Image)(resources.GetObject("butBottom.Image")));
			this.butBottom.Location = new System.Drawing.Point(24, 40);
			this.butBottom.Name = "butBottom";
			this.butBottom.Size = new System.Drawing.Size(16, 24);
			this.butBottom.TabIndex = 2;
			this.butBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butBottom_MouseDown);
			// 
			// butLeft
			// 
			this.butLeft.Image = ((System.Drawing.Image)(resources.GetObject("butLeft.Image")));
			this.butLeft.Location = new System.Drawing.Point(0, 24);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(24, 16);
			this.butLeft.TabIndex = 3;
			this.butLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butLeft_MouseDown);
			// 
			// chkIsSize
			// 
			this.chkIsSize.Location = new System.Drawing.Point(24, 24);
			this.chkIsSize.Name = "chkIsSize";
			this.chkIsSize.Size = new System.Drawing.Size(16, 16);
			this.chkIsSize.TabIndex = 4;
			this.chkIsSize.Text = "checkBox1";
			// 
			// FrmArrowOperate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(66, 64);
			this.Controls.Add(this.chkIsSize);
			this.Controls.Add(this.butLeft);
			this.Controls.Add(this.butBottom);
			this.Controls.Add(this.butRight);
			this.Controls.Add(this.butTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmArrowOperate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "微调器";
			this.Load += new System.EventHandler(this.FrmArrowOperate_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		private DesignSectionList _SectionList;

		#region 构造函数...

		public FrmArrowOperate(DesignSectionList pSectionList) {
			InitializeComponent();
			_SectionList = pSectionList;
			//让窗口在最顶层
			DIYReport.PublicFun.SetWindowPos(this.Handle,-1,0,0,0,0,3);
		}

		#endregion 构造函数...

		#region 扩展的public 方法...
		/// <summary>
		/// 显示方向盘控制窗口
		/// </summary>
		/// <param name="pControlList"></param>
		/// <param name="pParent"></param>
		/// <returns></returns>
		public static void ShowArrowForm(DesignSectionList  pSectionList,Form pParent){
			bool b = false;
			foreach(Form frm in pParent.OwnedForms){
				if(frm.Name == "FrmArrowOperate"){
					b = true;
					frm.Show();
					break;
				}
			}
			if(!b){
				FrmArrowOperate frm =  new FrmArrowOperate(pSectionList);
				pParent.AddOwnedForm(frm);
				frm.Show();
			}
			
			
		}
		#endregion 扩展的public 方法...


		private void butLeft_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			_SectionList.GetActiveSection().DesignControls.ProcessArrowKeyDown(37,chkIsSize.Checked ); 
		}

		private void butTop_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			_SectionList.GetActiveSection().DesignControls.ProcessArrowKeyDown(38,chkIsSize.Checked );
		}

		private void butRight_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			_SectionList.GetActiveSection().DesignControls.ProcessArrowKeyDown(39,chkIsSize.Checked );
		}

		private void butBottom_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			_SectionList.GetActiveSection().DesignControls.ProcessArrowKeyDown(40,chkIsSize.Checked );
		}

		private void FrmArrowOperate_Load(object sender, System.EventArgs e) {
			ToolTip tip = new ToolTip();
			tip.SetToolTip(chkIsSize ,"");
		}
	}
}
