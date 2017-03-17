//using System;
//using System.Data ;
//using System.Drawing;
//using System.Collections;
//using System.Reflection ;
//using System.ComponentModel;
//using System.Windows.Forms;
//
//namespace DIYReport.UserDIY
//{
//	/// <summary>
//	/// FrmEditExpress 表达式生成器。
//	/// </summary>
//	public class FrmEditExpress : System.Windows.Forms.Form
//	{
//		#region 内部自动生产代码...
//		private System.Windows.Forms.StatusBar statusBar1;
//		private System.Windows.Forms.Panel panel1;
//		private System.Windows.Forms.Panel panel2;
//		private System.Windows.Forms.Splitter splitter1;
//		private System.Windows.Forms.TextBox txtViewExpress;
//		private System.Windows.Forms.TreeView trvFieldList;
//		private System.Windows.Forms.ListBox lstFields;
//		private System.Windows.Forms.Button butSure;
//		private System.Windows.Forms.Button butQuit;
//		private System.Windows.Forms.Button butCompiler;
//		private System.Windows.Forms.Button butAdd;
//		private System.Windows.Forms.Button butBigEqu;
//		private System.Windows.Forms.Button butDiv;
//		private System.Windows.Forms.Button butBig;
//		private System.Windows.Forms.Button butLessEqu;
//		private System.Windows.Forms.Button butMult;
//		private System.Windows.Forms.Button butSub;
//		private System.Windows.Forms.Button butLess;
//		private System.Windows.Forms.Button butEqu;
//		private System.Windows.Forms.Button butNotEqu;
//		private System.Windows.Forms.Button butLeftFit;
//		private System.Windows.Forms.Button butLeftBracket;
//		private System.Windows.Forms.Button butRightFit;
//		private System.Windows.Forms.Button butRightBracket;
//		/// <summary>
//		/// 必需的设计器变量。
//		/// </summary>
//		private System.ComponentModel.Container components = null;
//
//		/// <summary>
//		/// 清理所有正在使用的资源。
//		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if( disposing )
//			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
//			}
//			base.Dispose( disposing );
//		}
//
//		#region Windows 窗体设计器生成的代码
//		/// <summary>
//		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
//		/// 此方法的内容。
//		/// </summary>
//		private void InitializeComponent()
//		{
//			this.statusBar1 = new System.Windows.Forms.StatusBar();
//			this.panel1 = new System.Windows.Forms.Panel();
//			this.txtViewExpress = new System.Windows.Forms.TextBox();
//			this.panel2 = new System.Windows.Forms.Panel();
//			this.trvFieldList = new System.Windows.Forms.TreeView();
//			this.splitter1 = new System.Windows.Forms.Splitter();
//			this.lstFields = new System.Windows.Forms.ListBox();
//			this.butSure = new System.Windows.Forms.Button();
//			this.butQuit = new System.Windows.Forms.Button();
//			this.butCompiler = new System.Windows.Forms.Button();
//			this.butAdd = new System.Windows.Forms.Button();
//			this.butBigEqu = new System.Windows.Forms.Button();
//			this.butDiv = new System.Windows.Forms.Button();
//			this.butBig = new System.Windows.Forms.Button();
//			this.butLessEqu = new System.Windows.Forms.Button();
//			this.butMult = new System.Windows.Forms.Button();
//			this.butSub = new System.Windows.Forms.Button();
//			this.butLess = new System.Windows.Forms.Button();
//			this.butEqu = new System.Windows.Forms.Button();
//			this.butNotEqu = new System.Windows.Forms.Button();
//			this.butLeftFit = new System.Windows.Forms.Button();
//			this.butLeftBracket = new System.Windows.Forms.Button();
//			this.butRightFit = new System.Windows.Forms.Button();
//			this.butRightBracket = new System.Windows.Forms.Button();
//			this.panel1.SuspendLayout();
//			this.panel2.SuspendLayout();
//			this.SuspendLayout();
//			// 
//			// statusBar1
//			// 
//			this.statusBar1.Location = new System.Drawing.Point(0, 326);
//			this.statusBar1.Name = "statusBar1";
//			this.statusBar1.Size = new System.Drawing.Size(368, 24);
//			this.statusBar1.TabIndex = 0;
//			this.statusBar1.Text = "statusBar1";
//			// 
//			// panel1
//			// 
//			this.panel1.Controls.Add(this.butCompiler);
//			this.panel1.Controls.Add(this.butQuit);
//			this.panel1.Controls.Add(this.butSure);
//			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
//			this.panel1.Location = new System.Drawing.Point(288, 0);
//			this.panel1.Name = "panel1";
//			this.panel1.Size = new System.Drawing.Size(80, 326);
//			this.panel1.TabIndex = 1;
//			// 
//			// txtViewExpress
//			// 
//			this.txtViewExpress.Dock = System.Windows.Forms.DockStyle.Top;
//			this.txtViewExpress.Location = new System.Drawing.Point(0, 0);
//			this.txtViewExpress.Multiline = true;
//			this.txtViewExpress.Name = "txtViewExpress";
//			this.txtViewExpress.Size = new System.Drawing.Size(288, 64);
//			this.txtViewExpress.TabIndex = 2;
//			this.txtViewExpress.Text = "";
//			// 
//			// panel2
//			// 
//			this.panel2.Controls.Add(this.butRightBracket);
//			this.panel2.Controls.Add(this.butRightFit);
//			this.panel2.Controls.Add(this.butLeftBracket);
//			this.panel2.Controls.Add(this.butLeftFit);
//			this.panel2.Controls.Add(this.butNotEqu);
//			this.panel2.Controls.Add(this.butEqu);
//			this.panel2.Controls.Add(this.butLess);
//			this.panel2.Controls.Add(this.butSub);
//			this.panel2.Controls.Add(this.butMult);
//			this.panel2.Controls.Add(this.butLessEqu);
//			this.panel2.Controls.Add(this.butBig);
//			this.panel2.Controls.Add(this.butDiv);
//			this.panel2.Controls.Add(this.butBigEqu);
//			this.panel2.Controls.Add(this.butAdd);
//			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
//			this.panel2.Location = new System.Drawing.Point(0, 64);
//			this.panel2.Name = "panel2";
//			this.panel2.Size = new System.Drawing.Size(288, 48);
//			this.panel2.TabIndex = 3;
//			// 
//			// trvFieldList
//			// 
//			this.trvFieldList.Dock = System.Windows.Forms.DockStyle.Left;
//			this.trvFieldList.ImageIndex = -1;
//			this.trvFieldList.Location = new System.Drawing.Point(0, 112);
//			this.trvFieldList.Name = "trvFieldList";
//			this.trvFieldList.SelectedImageIndex = -1;
//			this.trvFieldList.Size = new System.Drawing.Size(152, 214);
//			this.trvFieldList.TabIndex = 4;
//			this.trvFieldList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvFieldList_AfterSelect);
//			// 
//			// splitter1
//			// 
//			this.splitter1.Location = new System.Drawing.Point(152, 112);
//			this.splitter1.Name = "splitter1";
//			this.splitter1.Size = new System.Drawing.Size(4, 214);
//			this.splitter1.TabIndex = 5;
//			this.splitter1.TabStop = false;
//			// 
//			// lstFields
//			// 
//			this.lstFields.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.lstFields.ItemHeight = 12;
//			this.lstFields.Location = new System.Drawing.Point(156, 112);
//			this.lstFields.Name = "lstFields";
//			this.lstFields.Size = new System.Drawing.Size(132, 208);
//			this.lstFields.TabIndex = 6;
//			// 
//			// butSure
//			// 
//			this.butSure.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butSure.Location = new System.Drawing.Point(8, 8);
//			this.butSure.Name = "butSure";
//			this.butSure.Size = new System.Drawing.Size(64, 24);
//			this.butSure.TabIndex = 0;
//			this.butSure.Text = "确定(&S)";
//			// 
//			// butQuit
//			// 
//			this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butQuit.Location = new System.Drawing.Point(8, 40);
//			this.butQuit.Name = "butQuit";
//			this.butQuit.Size = new System.Drawing.Size(64, 24);
//			this.butQuit.TabIndex = 1;
//			this.butQuit.Text = "取消(&Q)";
//			// 
//			// butCompiler
//			// 
//			this.butCompiler.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butCompiler.Location = new System.Drawing.Point(8, 88);
//			this.butCompiler.Name = "butCompiler";
//			this.butCompiler.Size = new System.Drawing.Size(64, 24);
//			this.butCompiler.TabIndex = 2;
//			this.butCompiler.Text = "编译(&C)";
//			// 
//			// butAdd
//			// 
//			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butAdd.Location = new System.Drawing.Point(8, 8);
//			this.butAdd.Name = "butAdd";
//			this.butAdd.Size = new System.Drawing.Size(24, 16);
//			this.butAdd.TabIndex = 0;
//			this.butAdd.Text = "+";
//			// 
//			// butBigEqu
//			// 
//			this.butBigEqu.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butBigEqu.Location = new System.Drawing.Point(64, 8);
//			this.butBigEqu.Name = "butBigEqu";
//			this.butBigEqu.Size = new System.Drawing.Size(24, 16);
//			this.butBigEqu.TabIndex = 1;
//			this.butBigEqu.Text = ">=";
//			// 
//			// butDiv
//			// 
//			this.butDiv.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butDiv.Location = new System.Drawing.Point(32, 24);
//			this.butDiv.Name = "butDiv";
//			this.butDiv.Size = new System.Drawing.Size(24, 16);
//			this.butDiv.TabIndex = 2;
//			this.butDiv.Text = "/";
//			// 
//			// butBig
//			// 
//			this.butBig.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butBig.Location = new System.Drawing.Point(64, 24);
//			this.butBig.Name = "butBig";
//			this.butBig.Size = new System.Drawing.Size(24, 16);
//			this.butBig.TabIndex = 3;
//			this.butBig.Text = ">";
//			// 
//			// butLessEqu
//			// 
//			this.butLessEqu.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butLessEqu.Location = new System.Drawing.Point(88, 8);
//			this.butLessEqu.Name = "butLessEqu";
//			this.butLessEqu.Size = new System.Drawing.Size(24, 16);
//			this.butLessEqu.TabIndex = 4;
//			this.butLessEqu.Text = "<=";
//			// 
//			// butMult
//			// 
//			this.butMult.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butMult.Location = new System.Drawing.Point(8, 24);
//			this.butMult.Name = "butMult";
//			this.butMult.Size = new System.Drawing.Size(24, 16);
//			this.butMult.TabIndex = 5;
//			this.butMult.Text = "*";
//			// 
//			// butSub
//			// 
//			this.butSub.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butSub.Location = new System.Drawing.Point(32, 8);
//			this.butSub.Name = "butSub";
//			this.butSub.Size = new System.Drawing.Size(24, 16);
//			this.butSub.TabIndex = 6;
//			this.butSub.Text = "-";
//			// 
//			// butLess
//			// 
//			this.butLess.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butLess.Location = new System.Drawing.Point(88, 24);
//			this.butLess.Name = "butLess";
//			this.butLess.Size = new System.Drawing.Size(24, 16);
//			this.butLess.TabIndex = 7;
//			this.butLess.Text = "<";
//			// 
//			// butEqu
//			// 
//			this.butEqu.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butEqu.Location = new System.Drawing.Point(112, 24);
//			this.butEqu.Name = "butEqu";
//			this.butEqu.Size = new System.Drawing.Size(24, 16);
//			this.butEqu.TabIndex = 8;
//			this.butEqu.Text = "=";
//			// 
//			// butNotEqu
//			// 
//			this.butNotEqu.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butNotEqu.Location = new System.Drawing.Point(112, 8);
//			this.butNotEqu.Name = "butNotEqu";
//			this.butNotEqu.Size = new System.Drawing.Size(24, 16);
//			this.butNotEqu.TabIndex = 9;
//			this.butNotEqu.Text = "<>";
//			// 
//			// butLeftFit
//			// 
//			this.butLeftFit.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butLeftFit.Location = new System.Drawing.Point(144, 24);
//			this.butLeftFit.Name = "butLeftFit";
//			this.butLeftFit.Size = new System.Drawing.Size(24, 16);
//			this.butLeftFit.TabIndex = 10;
//			this.butLeftFit.Text = "[";
//			// 
//			// butLeftBracket
//			// 
//			this.butLeftBracket.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butLeftBracket.Location = new System.Drawing.Point(144, 8);
//			this.butLeftBracket.Name = "butLeftBracket";
//			this.butLeftBracket.Size = new System.Drawing.Size(24, 16);
//			this.butLeftBracket.TabIndex = 11;
//			this.butLeftBracket.Text = "(";
//			// 
//			// butRightFit
//			// 
//			this.butRightFit.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butRightFit.Location = new System.Drawing.Point(168, 24);
//			this.butRightFit.Name = "butRightFit";
//			this.butRightFit.Size = new System.Drawing.Size(24, 16);
//			this.butRightFit.TabIndex = 12;
//			this.butRightFit.Text = "]";
//			// 
//			// butRightBracket
//			// 
//			this.butRightBracket.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.butRightBracket.Location = new System.Drawing.Point(168, 8);
//			this.butRightBracket.Name = "butRightBracket";
//			this.butRightBracket.Size = new System.Drawing.Size(24, 16);
//			this.butRightBracket.TabIndex = 13;
//			this.butRightBracket.Text = ")";
//			// 
//			// FrmEditExpress
//			// 
//			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
//			this.ClientSize = new System.Drawing.Size(368, 350);
//			this.Controls.Add(this.lstFields);
//			this.Controls.Add(this.splitter1);
//			this.Controls.Add(this.trvFieldList);
//			this.Controls.Add(this.panel2);
//			this.Controls.Add(this.txtViewExpress);
//			this.Controls.Add(this.panel1);
//			this.Controls.Add(this.statusBar1);
//			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
//			this.Name = "FrmEditExpress";
//			this.Text = "数据表达式生成器";
//			this.Load += new System.EventHandler(this.FrmEditExpress_Load);
//			this.panel1.ResumeLayout(false);
//			this.panel2.ResumeLayout(false);
//			this.ResumeLayout(false);
//
//		}
//		#endregion
//
//		#endregion 内部自动生产代码...
//
//		#region 变量定义...
//		private DataSet _DsFields = null;
//
//		#endregion 变量定义...
//
//		#region 构造函数...
//		public FrmEditExpress() {
//
//			InitializeComponent();
//
//		}
//		#endregion 构造函数...
//
//		#region 增加ToolText...
//		private void setToolText(){
//			ToolTip tp = new ToolTip();
//			tp.SetToolTip(butAdd,"加法运算");
//			tp.SetToolTip(butSub,"减法运算");
//			tp.SetToolTip(butMult,"乘法运算");
//			tp.SetToolTip(butDiv,"除法运算");
//			tp.SetToolTip(butBigEqu,"大于或者等于运算符号");
//			tp.SetToolTip(butBig,"大于运算符号");
//			tp.SetToolTip(butLessEqu,"小于或者等于运算符号");
//			tp.SetToolTip(butLess,"小于运算符号");
//			tp.SetToolTip(butEqu,"等于运算符号");
//			tp.SetToolTip(butLeftBracket,"左括号");
//			tp.SetToolTip(butRightBracket,"右括号");
//			tp.SetToolTip(butLeftFit,"左中括号");
//			tp.SetToolTip(butRightFit,"右中括号");
//
//			tp.SetToolTip(butCompiler,"表达式预先编译是否成功！");
//		}
//		#endregion 增加ToolText...
//
//		#region 创建表达式的基础数据...
//		//创建树显示相关
//		private void createTreeExpress(){
//			if(trvFieldList.Nodes.Count > 0){
//				trvFieldList.Nodes.Clear();
//			}
//			TreeNode node = new TreeNode("数据库绑定字段");
//			node.Tag = "Fields";
//			trvFieldList.Nodes.Add(node);
//			node = new TreeNode("特殊字段");
//			node.Tag = "Special";
//			trvFieldList.Nodes.Add(node);
//			trvFieldList.Nodes.Add(new TreeNode("函数"));
//			node = new TreeNode("函数");
//			trvFieldList.Nodes.Add(node);
//			addFunction(node);
//
//			trvFieldList.Nodes.Add(node);
//			node = new TreeNode("非绑定字段");
//			trvFieldList.Nodes.Add(node);
//		}
//		//增加数据库绑定字段
//		private void addBindField(TreeNode pNode){
//			lstFields.Items.Clear(); 
//			if(_DsFields!=null && _DsFields.Tables.Count > 0 ){
//				DataTable dt = _DsFields.Tables[0];
//				foreach(DataColumn dc in dt.Columns){
//					string text = dc.Caption.Trim()!=""? dc.Caption : dc.ColumnName ;
//					FieldStruct st = new FieldStruct(dc.ColumnName ,dc.Caption);
//					lstFields.Items.Add(st);
//				}
//			}
//		}
//		//增加系统函数
//		private void addFunction(TreeNode pNode){
//			TreeNode node = new TreeNode("数学");
//			node.Tag = "Math";
//			pNode.Nodes.Add(node);
//
//			 node = new TreeNode("统计");
//			node.Tag = "Stat";
//			pNode.Nodes.Add(node);
//
//			 node = new TreeNode("日期/时间");
//			node.Tag = "Date";
//			pNode.Nodes.Add(node);
//
//			 node = new TreeNode("字符窜");
//			node.Tag = "String";
//			pNode.Nodes.Add(node);
//
//		 node = new TreeNode("检查");
//			node.Tag = "Check";
//			pNode.Nodes.Add(node);
//			
//		}
//		private void addToLstList(TreeNode pNode){
//			if(pNode.Tag==null){return;}
//			string ex = pNode.Tag.ToString();
//			System.Type clsType = null;
//			bool addList = true;
//			switch(ex){
//				case "Math":
//					clsType = System.Type.GetType("System.Math");
//					break;
//				case "String":
//					clsType = System.Type.GetType("System.Math");
//					break;
//				case "Date":
//					clsType = System.Type.GetType("System.DateTime");
//					break;
//				case "Stat":
//					clsType = System.Type.GetType("DIYReport.Express.ExStatistical");
//					break;
//				case "Special":
//					clsType = System.Type.GetType("DIYReport.Express.ExSpecial");
//					break;
//				case "Fields":
//					addBindField(pNode);
//					addList = false;
//					break;
//				default:
//					addList = false;
//					break;
//
//			}
//			lstFields.Items.Clear(); 
//			if(addList){
//				MethodInfo[] infos =   clsType.GetMethods(); 
//				foreach(MethodInfo info in infos){
//					if(info.IsPublic){ 
//						bool hasExist = hasAddStr(info.Name);
//						if(!hasExist){
//							lstFields.Items.Add(new FieldStruct(info.Name ,info.Name));
//						}
//					}
//				}
//			}
//		}
//		//判断某一个函数的名称是否已经在ListBox 控件中存在
//		private bool hasAddStr(string pName){
//			int count = lstFields.Items.Count ;
//			for(int i = 0;i <count;i++){
//				if(lstFields.Items[i].ToString() == pName){
//					return true;
//				}
//			}
//			return false;
//			//foreach( lstFields.Items
//		}
//		#endregion 创建表达式的基础数据...
//
//		#region 界面事件...
//		private void FrmEditExpress_Load(object sender, System.EventArgs e) {
//			setToolText();
//			createTreeExpress();
//		}
//
//		private void trvFieldList_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
//			if(e.Action!=TreeViewAction.Unknown){
//				addToLstList(e.Node); 
//			}
//		}
//		#endregion 界面事件...
//	}
//	
//	#region 字段结构...
//	struct FieldStruct{
//		private string _Name;
//		private string _Description;
//		public FieldStruct(string pName,string pDescription){
//			_Name = pName;
//			_Description = pDescription;
//		}
//
//		#region 覆盖基类的方法...
//		public override string ToString() {
//			return _Description;
//		}
//
//		#endregion 覆盖基类的方法...
//
//		#region Public 属性...
//		public string Name{
//			get{
//				return _Name;
//			}
//			set{
//				_Name = value;
//			}
//		}
//		public string Description{
//			get{
//				return _Description;
//			}
//			set{
//				_Description = value;
//			}
//		}
//		#endregion Public 属性...
//	}
//	#endregion 字段结构...
//}
