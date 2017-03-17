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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DIYReport.UserDIY
{
	/// <summary>
	/// SectionCaption 的摘要说明。
	/// </summary>
	[ToolboxItem(false)]
	public class SectionCaption : System.Windows.Forms.UserControl
	{
		#region 内部自动生成代码...
		private System.Windows.Forms.Label labCaption;
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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.labCaption = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labCaption
			// 
			this.labCaption.Location = new System.Drawing.Point(0, 0);
			this.labCaption.Name = "labCaption";
			this.labCaption.Size = new System.Drawing.Size(200, 16);
			this.labCaption.TabIndex = 0;
			this.labCaption.Text = "label1";
			this.labCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labCaption_MouseDown);
			// 
			// SectionCaption
			// 
			this.BackColor = Color.Silver ;
			this.Controls.Add(this.labCaption);
			this.Name = "SectionCaption";
			this.Size = new System.Drawing.Size(416, 32);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SectionCaption_MouseDown);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		#region 变量定义...
		public static int CAPTION_HEIGHT = 14;
		private DIYReport.UserDIY.DesignSection _Section;
		#endregion 变量定义...

		#region 构造函数...
		public SectionCaption() {
			InitializeComponent();
			
			this.Height = CAPTION_HEIGHT;
			labCaption.Height = CAPTION_HEIGHT;
			labCaption.Location = new Point(0,0);

		}
		#endregion 构造函数...
		#region 扩展的Public 方法...
		public void SetActive(bool pActive){
			if(pActive){
				this.BackColor = Color.Blue;
				labCaption.ForeColor = Color.White ;  
			}
			else{
				this.BackColor = Color.Silver  ;
				labCaption.ForeColor = Color.Black ;
				
			}
			labCaption.BackColor =  this.BackColor;
		}
		#endregion 扩展的Public 方法...

		private void SectionCaption_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			this.Section.SectionList.SetActiveSection(this.Section); 

			DesignEnviroment.CurrentRptObj = this.Section.DataObj ; 
			DesignEnviroment.UICmidExecutor.ExecCommand(UICommands.SetObjProperty) ; 
		}

		private void labCaption_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			this.Section.SectionList.SetActiveSection(this.Section); 

			DesignEnviroment.CurrentRptObj = this.Section.DataObj ; 
			DesignEnviroment.UICmidExecutor.ExecCommand(UICommands.SetObjProperty) ; 
		}
		#region 扩展的Public 属性...
		
		public DIYReport.UserDIY.DesignSection Section{
			get{
				return _Section;
			}
			set{
				_Section = value;
			}
		}
		public string Caption{
			get{
				return labCaption.Text ;
			}
			set{
				labCaption.Text = value;
			}
		}
		#endregion 扩展的Public 属性...
	}
}
