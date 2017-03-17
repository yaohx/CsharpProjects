//---------------------------------------------------------------- 
// All rights reserved. 
// Author		:	chendc
// Create date	:	2003-01-04
// Description	:	FrmAuthorization
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Management;
using System.IO;


namespace MB.Aop.SoftRegistry {
	/// <summary>
	/// FrmAuthorization 授权鉴定窗口。
	/// </summary>
	public class FrmAuthorization : System.Windows.Forms.Form {

		#region System auto create...

		private System.Windows.Forms.Label labSource;
		private System.Windows.Forms.Label labaccredit;
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button butSure;
		private System.Windows.Forms.Button butExit;
		private System.Windows.Forms.TextBox txtSerNumber;
		private System.Windows.Forms.TextBox txtAuthNumber;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
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
            this.txtSerNumber = new System.Windows.Forms.TextBox();
            this.labSource = new System.Windows.Forms.Label();
            this.labaccredit = new System.Windows.Forms.Label();
            this.txtAuthNumber = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butExit = new System.Windows.Forms.Button();
            this.butSure = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSerNumber
            // 
            this.txtSerNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSerNumber.Location = new System.Drawing.Point(16, 40);
            this.txtSerNumber.Name = "txtSerNumber";
            this.txtSerNumber.ReadOnly = true;
            this.txtSerNumber.Size = new System.Drawing.Size(376, 21);
            this.txtSerNumber.TabIndex = 0;
            // 
            // labSource
            // 
            this.labSource.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labSource.Location = new System.Drawing.Point(16, 24);
            this.labSource.Name = "labSource";
            this.labSource.Size = new System.Drawing.Size(152, 16);
            this.labSource.TabIndex = 1;
            this.labSource.Text = "软件序列号:";
            // 
            // labaccredit
            // 
            this.labaccredit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labaccredit.Location = new System.Drawing.Point(16, 72);
            this.labaccredit.Name = "labaccredit";
            this.labaccredit.Size = new System.Drawing.Size(136, 16);
            this.labaccredit.TabIndex = 2;
            this.labaccredit.Text = "授权码:";
            // 
            // txtAuthNumber
            // 
            this.txtAuthNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAuthNumber.Location = new System.Drawing.Point(16, 96);
            this.txtAuthNumber.Name = "txtAuthNumber";
            this.txtAuthNumber.Size = new System.Drawing.Size(376, 21);
            this.txtAuthNumber.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.butExit);
            this.panel1.Controls.Add(this.butSure);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 40);
            this.panel1.TabIndex = 4;
            // 
            // butExit
            // 
            this.butExit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.butExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.butExit.Location = new System.Drawing.Point(322, 6);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(66, 29);
            this.butExit.TabIndex = 2;
            this.butExit.Text = "关闭(&E)";
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // butSure
            // 
            this.butSure.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.butSure.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.butSure.Location = new System.Drawing.Point(243, 6);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(73, 31);
            this.butSure.TabIndex = 1;
            this.butSure.Text = "确定(&S)";
            this.butSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.txtSerNumber);
            this.groupBox1.Controls.Add(this.labaccredit);
            this.groupBox1.Controls.Add(this.labSource);
            this.groupBox1.Controls.Add(this.txtAuthNumber);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 126);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "授权";
            // 
            // FrmAuthorization
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(400, 166);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAuthorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "使用授权";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#endregion System auto create...

		private AuthRightInfo _RightData;


		#region 构造函数...
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pRightData"></param>
		public FrmAuthorization(AuthRightInfo pRightData) {
			InitializeComponent();

			_RightData = pRightData;
		}
		#endregion 构造函数...

		private string _SerNumber;

		private void Form1_Load(object sender, System.EventArgs e) {
			
			_SerNumber = AuthDataEncrypt.GetHd();
			txtSerNumber.Text = AuthDataEncrypt.EncryptHDString(_SerNumber);  
		}
		 
		//writer HardWare Sn Into File  CreadSn.Txt
		private void butGetWord_Click(object sender, System.EventArgs e) {
			StringBuilder sb = new StringBuilder();
			string fileName = Environment.SystemDirectory  + @"\ufSn.Txt";
			try {
				if (System.IO.File.Exists(fileName)) {
					System.IO.File.Delete(fileName);
				}
				sb.Append("请将下列序列号通过电话或者E-Mail方式通知软件供应商,以得到授权码:" );
				sb.Append("\r\n" );
				sb.Append(this.txtSerNumber.Text );
				sb.Append("\r\n" );
				byte[] bts = Encoding.UTF8.GetBytes(sb.ToString());
				FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
				fs.Write(bts,0,bts.Length);
				fs.Close();
				System.Diagnostics.ProcessStartInfo  Info  =  new  System.Diagnostics.ProcessStartInfo();

				Info.FileName = fileName;
				Info.WorkingDirectory  =  System.IO.Path.GetTempPath();

				//声明一个程序类
				System.Diagnostics.Process  Proc  ;

				try {
					//
					//启动外部程序
					//
					Proc  =  System.Diagnostics.Process.Start(Info);
				}
				catch(Exception exx) {
					//TraceEx.Write("系统找不到指定的程序文件。 " + exx.Message );
				}
			}
			catch(Exception ex) {
				//UP.Utils.TraceEx.Write("获取系统密码有误:" +  ex.Message); 
			 }
		}

		//get KeyIn Accredit PassWord determine Right Or Wrong
		private void butSure_Click(object sender, System.EventArgs e) {
			try {
				string sourceStr = this.txtAuthNumber.Text; 
				AuthRightInfo tempAuthInfo = AuthHelper.AuthDataRight(sourceStr); 
				if(tempAuthInfo==null){
					MessageBox.Show("授权码错误!请与软件供应商联系!","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					_RightData.PassIsRight = false;
					return;
				}
				_RightData.PassIsRight  = string.Compare(_SerNumber , tempAuthInfo.HardDC,true)==0 && tempAuthInfo.EndDate > System.DateTime.Now;

				if (_RightData.PassIsRight) {
					_RightData.EndDate  = tempAuthInfo.EndDate;
					_RightData.LinkCount  = tempAuthInfo.LinkCount;
					
					MessageBox.Show("授权码正确!"+ _RightData.ToString(),"操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information );

					//save accreditSn					
					string file;
					if(_RightData.SNFileName!=null && _RightData.SNFileName.Length >0)
                        file = MB.Util.General.GeApplicationDirectory() +  _RightData.SNFileName + ".Txt";
					else
                        file = AppDomain.CurrentDomain.BaseDirectory + @"UfAccreditSn.Txt";
					try {
						StringBuilder sb = new StringBuilder();
						if(System.IO.File.Exists(file)) {
							System.IO.File.Delete(file);
						}
						sb.Append( sourceStr);
						byte[] bts = Encoding.UTF8.GetBytes(sb.ToString());
						FileStream fs = new FileStream(file, FileMode.CreateNew, FileAccess.Write);
						fs.Write(bts,0,bts.Length);
						fs.Close();
					}
					catch(Exception ex) {
						MessageBox.Show(ex.ToString());
					}
					this.Close();
				}
				else {
					MessageBox.Show("授权码错误!请与软件供应商联系!","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					_RightData.PassIsRight = false;
				}
			}
			catch {
				MessageBox.Show("授权码错误!请与软件供应商联系!","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				_RightData.PassIsRight = false;
			}

		}

		private void butExit_Click(object sender, System.EventArgs e) {
			this.Close();
		}
	}
}
