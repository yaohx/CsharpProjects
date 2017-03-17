using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MB.WinBase.Ctls
{
	/// <summary>
	/// frmAddSheet 表格插入初始化。
	/// </summary>
	public class frmAddSheet : System.Windows.Forms.Form {
		#region 内部自动生成代码...
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butQuit;
		private System.Windows.Forms.Button butSure;
		private System.Windows.Forms.NumericUpDown numCol;
		private System.Windows.Forms.NumericUpDown numRow;
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numRow = new System.Windows.Forms.NumericUpDown();
			this.numCol = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.butQuit = new System.Windows.Forms.Button();
			this.butSure = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCol)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numRow);
			this.groupBox1.Controls.Add(this.numCol);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 112);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "表格尺寸：";
			// 
			// numRow
			// 
			this.numRow.Location = new System.Drawing.Point(184, 64);
			this.numRow.Minimum = new System.Decimal(new int[] {
																   1,
																   0,
																   0,
																   0});
			this.numRow.Name = "numRow";
			this.numRow.Size = new System.Drawing.Size(136, 21);
			this.numRow.TabIndex = 3;
			this.numRow.Value = new System.Decimal(new int[] {
																 3,
																 0,
																 0,
																 0});
			// 
			// numCol
			// 
			this.numCol.Location = new System.Drawing.Point(184, 24);
			this.numCol.Minimum = new System.Decimal(new int[] {
																   1,
																   0,
																   0,
																   0});
			this.numCol.Name = "numCol";
			this.numCol.Size = new System.Drawing.Size(136, 21);
			this.numCol.TabIndex = 2;
			this.numCol.Value = new System.Decimal(new int[] {
																 5,
																 0,
																 0,
																 0});
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 32);
			this.label2.TabIndex = 1;
			this.label2.Text = "行数(&R)：";
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "列数(&C)：";
			// 
			// butQuit
			// 
			this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butQuit.Location = new System.Drawing.Point(248, 128);
			this.butQuit.Name = "butQuit";
			this.butQuit.Size = new System.Drawing.Size(80, 24);
			this.butQuit.TabIndex = 1;
			this.butQuit.Text = "取消(&Q)";
			this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
			// 
			// butSure
			// 
			this.butSure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSure.Location = new System.Drawing.Point(160, 128);
			this.butSure.Name = "butSure";
			this.butSure.Size = new System.Drawing.Size(80, 24);
			this.butSure.TabIndex = 2;
			this.butSure.Text = "确定(&S)";
			this.butSure.Click += new System.EventHandler(this.butSure_Click);
			// 
			// frmAddSheet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(338, 160);
			this.Controls.Add(this.butSure);
			this.Controls.Add(this.butQuit);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmAddSheet";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "插入表格";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numRow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCol)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#endregion 内部自动生成代码...

		#region 构造函数...
		/// <summary>
		/// 构造函数.
		/// </summary>
		public frmAddSheet(RichTextBox rtxtBox) {

			InitializeComponent();


			_RtxtBox = rtxtBox;
		}
		#endregion 构造函数...

		#region 变量定义...
		private RichTextBox _RtxtBox;
		#endregion 变量定义...

	
		#region 界面操作事件...
		private void butQuit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void butSure_Click(object sender, System.EventArgs e) {
			addSheet();

			this.Close();
		}
		#endregion 界面操作事件...

		#region 内部处理函数...
		private void addSheet(){
			StringBuilder sb = new StringBuilder();
			int col = System.Convert.ToInt32(numCol.Value);
			int row = System.Convert.ToInt32(numRow.Value);

			string rtfHead = @"{\rtf1\ansi\ansicpg936\deff0\deflang1033\deflangfe2052{\fonttbl{\f0\froman\fprq2\fcharset0 Times New Roman;}{\f1\fnil\fprq2\fcharset134 \'cb\'ce\'cc\'e5;}{\f2\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}\viewkind4\uc1";
			//\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx{0}
			string rowHead=@"\trowd\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10";
			string rowAdd = @"\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx{0}";
			string rowBody = @"\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx8748\pard\intbl\nowidctlpar\f0\fs24\cell\cell\f1\row";
			string rtfEnd = @"}";
					
			string lengthInfo = string.Empty;

			string temp = string.Empty;
					
			int length = 8748/col;
			int lengthAdd = length;
			lengthInfo = length.ToString();

			sb.Append(rtfHead);
			sb.Append(rowHead);
			for(int i = col - 1; i> 0; i--) {
						
				lengthInfo = length.ToString();
				string.Format(rowAdd, lengthInfo );
				temp += string.Format(rowAdd, lengthInfo); 
				length = length + lengthAdd;
			}
			rowBody = temp+rowBody;
			sb.Append(rowBody);
			string rowW = rowHead + rowBody;
			//@"\trowd\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx2160\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx4320\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx8748\pard\intbl\nowidctlpar\f0\fs24\cell\cell\f1\row\trowd\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx2160\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx4320\clbrdrt\brdrw15\brdrs\clbrdrl\brdrw15\brdrs\clbrdrb\brdrw15\brdrs\clbrdrr\brdrw15\brdrs \cellx8748\pard\intbl\nowidctlpar\f0\fs24\cell\cell\f1\row"
			for(int i = row - 1; i > 0; i--) {
						
				sb.Append(rowW);
			}					
			sb.Append(rtfEnd);

			_RtxtBox.SelectedRtf = sb.ToString();
		}
		#endregion 内部处理函数...
	}
}
