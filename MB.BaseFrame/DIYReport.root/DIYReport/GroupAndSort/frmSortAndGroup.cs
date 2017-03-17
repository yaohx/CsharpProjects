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

namespace DIYReport.GroupAndSort {
	
	/// <summary>
	/// 分组和排序delegate 声明
	/// </summary>
	public delegate void SortAndGroupEventHandler(object sender,EventArgs e);

	/// <summary>
	/// frmSortAndGroup 报表打印字段分组分排序设置。
	/// </summary>
	public class frmSortAndGroup : System.Windows.Forms.Form {
		#region 内部自动生成代码...
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butGroupSetting;
		private System.Windows.Forms.Button butFinish;
		private System.Windows.Forms.Button butNext;
		private System.Windows.Forms.Button butPrevious;
		private System.Windows.Forms.Button butQuit;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butRemove;
		private System.Windows.Forms.Button butTop;
		private System.Windows.Forms.Button butBottom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cobField1;
		private System.Windows.Forms.ComboBox cobField2;
		private System.Windows.Forms.ComboBox cobField3;
		private System.Windows.Forms.ComboBox cobField4;
		private System.Windows.Forms.Button butSort1;
		private System.Windows.Forms.Button butSort2;
		private System.Windows.Forms.Button butSort3;
		private System.Windows.Forms.Button butSort4;
		private System.Windows.Forms.Panel panGroup;
		private System.Windows.Forms.ListBox lstFields;
		private System.Windows.Forms.ListBox lstGroupFields;
		private System.Windows.Forms.Panel panSort;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ImageList imgLst;
		private System.Windows.Forms.Timer timControl;
		private System.Windows.Forms.Label label9;
		private System.ComponentModel.IContainer components;

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSortAndGroup));
			this.panel1 = new System.Windows.Forms.Panel();
			this.butQuit = new System.Windows.Forms.Button();
			this.butPrevious = new System.Windows.Forms.Button();
			this.butNext = new System.Windows.Forms.Button();
			this.butFinish = new System.Windows.Forms.Button();
			this.panGroup = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.lstFields = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.butBottom = new System.Windows.Forms.Button();
			this.imgLst = new System.Windows.Forms.ImageList(this.components);
			this.butTop = new System.Windows.Forms.Button();
			this.butRemove = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.lstGroupFields = new System.Windows.Forms.ListBox();
			this.butGroupSetting = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panSort = new System.Windows.Forms.Panel();
			this.butSort4 = new System.Windows.Forms.Button();
			this.butSort3 = new System.Windows.Forms.Button();
			this.butSort2 = new System.Windows.Forms.Button();
			this.butSort1 = new System.Windows.Forms.Button();
			this.cobField4 = new System.Windows.Forms.ComboBox();
			this.cobField3 = new System.Windows.Forms.ComboBox();
			this.cobField2 = new System.Windows.Forms.ComboBox();
			this.cobField1 = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.timControl = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panGroup.SuspendLayout();
			this.panSort.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Teal;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.butQuit);
			this.panel1.Controls.Add(this.butPrevious);
			this.panel1.Controls.Add(this.butNext);
			this.panel1.Controls.Add(this.butFinish);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 232);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(378, 40);
			this.panel1.TabIndex = 0;
			// 
			// butQuit
			// 
			this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butQuit.Location = new System.Drawing.Point(58, 8);
			this.butQuit.Name = "butQuit";
			this.butQuit.Size = new System.Drawing.Size(72, 24);
			this.butQuit.TabIndex = 3;
			this.butQuit.Text = "取消(&Q)";
			this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
			// 
			// butPrevious
			// 
			this.butPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrevious.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butPrevious.Location = new System.Drawing.Point(138, 8);
			this.butPrevious.Name = "butPrevious";
			this.butPrevious.Size = new System.Drawing.Size(72, 24);
			this.butPrevious.TabIndex = 2;
			this.butPrevious.Text = "上一步(&B)";
			this.butPrevious.Click += new System.EventHandler(this.butPrevious_Click);
			// 
			// butNext
			// 
			this.butNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butNext.Location = new System.Drawing.Point(218, 8);
			this.butNext.Name = "butNext";
			this.butNext.Size = new System.Drawing.Size(72, 24);
			this.butNext.TabIndex = 1;
			this.butNext.Text = "下一步(&N)";
			this.butNext.Click += new System.EventHandler(this.butNext_Click);
			// 
			// butFinish
			// 
			this.butFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butFinish.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butFinish.Location = new System.Drawing.Point(298, 8);
			this.butFinish.Name = "butFinish";
			this.butFinish.Size = new System.Drawing.Size(72, 24);
			this.butFinish.TabIndex = 0;
			this.butFinish.Text = "完成(&F)";
			this.butFinish.Click += new System.EventHandler(this.butFinish_Click);
			// 
			// panGroup
			// 
			this.panGroup.Controls.Add(this.label9);
			this.panGroup.Controls.Add(this.lstFields);
			this.panGroup.Controls.Add(this.label8);
			this.panGroup.Controls.Add(this.label7);
			this.panGroup.Controls.Add(this.butBottom);
			this.panGroup.Controls.Add(this.butTop);
			this.panGroup.Controls.Add(this.butRemove);
			this.panGroup.Controls.Add(this.butAdd);
			this.panGroup.Controls.Add(this.lstGroupFields);
			this.panGroup.Controls.Add(this.butGroupSetting);
			this.panGroup.Controls.Add(this.label1);
			this.panGroup.Location = new System.Drawing.Point(8, 0);
			this.panGroup.Name = "panGroup";
			this.panGroup.Size = new System.Drawing.Size(368, 232);
			this.panGroup.TabIndex = 1;
			// 
			// label9
			// 
			this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label9.ForeColor = System.Drawing.Color.Maroon;
			this.label9.Location = new System.Drawing.Point(8, 200);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(224, 16);
			this.label9.TabIndex = 10;
			this.label9.Text = "最多只能有4个分组的字段";
			// 
			// lstFields
			// 
			this.lstFields.ItemHeight = 12;
			this.lstFields.Location = new System.Drawing.Point(8, 32);
			this.lstFields.Name = "lstFields";
			this.lstFields.Size = new System.Drawing.Size(144, 160);
			this.lstFields.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.ForeColor = System.Drawing.Color.Navy;
			this.label8.Location = new System.Drawing.Point(8, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 24);
			this.label8.TabIndex = 9;
			this.label8.Text = "报表设计字段：";
			// 
			// label7
			// 
			this.label7.ForeColor = System.Drawing.Color.Navy;
			this.label7.Location = new System.Drawing.Point(208, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(144, 24);
			this.label7.TabIndex = 8;
			this.label7.Text = "选定分组的字段：";
			// 
			// butBottom
			// 
			this.butBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butBottom.ImageIndex = 1;
			this.butBottom.ImageList = this.imgLst;
			this.butBottom.Location = new System.Drawing.Point(168, 168);
			this.butBottom.Name = "butBottom";
			this.butBottom.Size = new System.Drawing.Size(24, 24);
			this.butBottom.TabIndex = 6;
			this.butBottom.Click += new System.EventHandler(this.butBottom_Click);
			// 
			// imgLst
			// 
			this.imgLst.ImageSize = new System.Drawing.Size(16, 16);
			this.imgLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLst.ImageStream")));
			this.imgLst.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// butTop
			// 
			this.butTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butTop.ImageIndex = 0;
			this.butTop.ImageList = this.imgLst;
			this.butTop.Location = new System.Drawing.Point(168, 120);
			this.butTop.Name = "butTop";
			this.butTop.Size = new System.Drawing.Size(24, 24);
			this.butTop.TabIndex = 5;
			this.butTop.Click += new System.EventHandler(this.butTop_Click);
			// 
			// butRemove
			// 
			this.butRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butRemove.Location = new System.Drawing.Point(168, 64);
			this.butRemove.Name = "butRemove";
			this.butRemove.Size = new System.Drawing.Size(24, 24);
			this.butRemove.TabIndex = 4;
			this.butRemove.Text = "<";
			this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
			// 
			// butAdd
			// 
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAdd.Location = new System.Drawing.Point(168, 32);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(24, 24);
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = ">";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// lstGroupFields
			// 
			this.lstGroupFields.ItemHeight = 12;
			this.lstGroupFields.Location = new System.Drawing.Point(208, 32);
			this.lstGroupFields.Name = "lstGroupFields";
			this.lstGroupFields.Size = new System.Drawing.Size(152, 160);
			this.lstGroupFields.TabIndex = 2;
			// 
			// butGroupSetting
			// 
			this.butGroupSetting.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butGroupSetting.Location = new System.Drawing.Point(272, 200);
			this.butGroupSetting.Name = "butGroupSetting";
			this.butGroupSetting.Size = new System.Drawing.Size(88, 24);
			this.butGroupSetting.TabIndex = 0;
			this.butGroupSetting.Text = "分组选项";
			this.butGroupSetting.Click += new System.EventHandler(this.butGroupSetting_Click);
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(160, 152);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "优先级";
			// 
			// panSort
			// 
			this.panSort.Controls.Add(this.butSort4);
			this.panSort.Controls.Add(this.butSort3);
			this.panSort.Controls.Add(this.butSort2);
			this.panSort.Controls.Add(this.butSort1);
			this.panSort.Controls.Add(this.cobField4);
			this.panSort.Controls.Add(this.cobField3);
			this.panSort.Controls.Add(this.cobField2);
			this.panSort.Controls.Add(this.cobField1);
			this.panSort.Controls.Add(this.label6);
			this.panSort.Controls.Add(this.label5);
			this.panSort.Controls.Add(this.label4);
			this.panSort.Controls.Add(this.label3);
			this.panSort.Controls.Add(this.label2);
			this.panSort.Location = new System.Drawing.Point(8, 0);
			this.panSort.Name = "panSort";
			this.panSort.Size = new System.Drawing.Size(368, 232);
			this.panSort.TabIndex = 2;
			// 
			// butSort4
			// 
			this.butSort4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSort4.Location = new System.Drawing.Point(208, 144);
			this.butSort4.Name = "butSort4";
			this.butSort4.Size = new System.Drawing.Size(72, 24);
			this.butSort4.TabIndex = 12;
			this.butSort4.Text = "升序";
			// 
			// butSort3
			// 
			this.butSort3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSort3.Location = new System.Drawing.Point(208, 112);
			this.butSort3.Name = "butSort3";
			this.butSort3.Size = new System.Drawing.Size(72, 24);
			this.butSort3.TabIndex = 11;
			this.butSort3.Text = "升序";
			// 
			// butSort2
			// 
			this.butSort2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSort2.Location = new System.Drawing.Point(208, 80);
			this.butSort2.Name = "butSort2";
			this.butSort2.Size = new System.Drawing.Size(72, 24);
			this.butSort2.TabIndex = 10;
			this.butSort2.Text = "升序";
			// 
			// butSort1
			// 
			this.butSort1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSort1.Location = new System.Drawing.Point(208, 48);
			this.butSort1.Name = "butSort1";
			this.butSort1.Size = new System.Drawing.Size(72, 24);
			this.butSort1.TabIndex = 9;
			this.butSort1.Text = "升序";
			// 
			// cobField4
			// 
			this.cobField4.Location = new System.Drawing.Point(48, 144);
			this.cobField4.Name = "cobField4";
			this.cobField4.Size = new System.Drawing.Size(144, 20);
			this.cobField4.TabIndex = 8;
			// 
			// cobField3
			// 
			this.cobField3.Location = new System.Drawing.Point(48, 112);
			this.cobField3.Name = "cobField3";
			this.cobField3.Size = new System.Drawing.Size(144, 20);
			this.cobField3.TabIndex = 7;
			// 
			// cobField2
			// 
			this.cobField2.Location = new System.Drawing.Point(48, 80);
			this.cobField2.Name = "cobField2";
			this.cobField2.Size = new System.Drawing.Size(144, 20);
			this.cobField2.TabIndex = 6;
			// 
			// cobField1
			// 
			this.cobField1.Location = new System.Drawing.Point(48, 48);
			this.cobField1.Name = "cobField1";
			this.cobField1.Size = new System.Drawing.Size(144, 20);
			this.cobField1.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(8, 152);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 24);
			this.label6.TabIndex = 4;
			this.label6.Text = "4";
			// 
			// label5
			// 
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(8, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 24);
			this.label5.TabIndex = 3;
			this.label5.Text = "3";
			// 
			// label4
			// 
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(8, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 24);
			this.label4.TabIndex = 2;
			this.label4.Text = "2";
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "1";
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(240, 32);
			this.label2.TabIndex = 0;
			this.label2.Text = "最多可以按四个字段对记录排序，既可以升序，也可以降序。";
			// 
			// timControl
			// 
			this.timControl.Enabled = true;
			this.timControl.Tick += new System.EventHandler(this.timControl_Tick);
			// 
			// frmSortAndGroup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(378, 272);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panGroup);
			this.Controls.Add(this.panSort);
			this.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSortAndGroup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "分组与排序向导";
			this.panel1.ResumeLayout(false);
			this.panGroup.ResumeLayout(false);
			this.panSort.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		#region 自定义事件...
		private event SortAndGroupEventHandler _AfterSortAndGroup;
		public event SortAndGroupEventHandler AfterSortAndGroup{
			add{
				_AfterSortAndGroup +=value;
			}
			remove{
				_AfterSortAndGroup -=value;
			}
		}
		private void OnAfterSortAndGroup(EventArgs e){
			if(_AfterSortAndGroup!=null){
				_AfterSortAndGroup(this,e);	
			}
		}
		#endregion 自定义事件...

		#region 变量定义...
		private IList _FieldList;
		private IList _CobSort;
		private IList _ButSort;
		private string ASC_TEXT = "升序";
		private string DESC_TEXT = "降序";
		private const int MAX_SORT_FIELD = 4;

		#endregion 变量定义...

		#region 构造函数...
		public frmSortAndGroup( IList pFieldList ) {
			InitializeComponent();
			_FieldList = pFieldList;
			_CobSort = new ArrayList();
			_ButSort = new ArrayList(); 
			_CobSort.Add(cobField1 );
			_CobSort.Add(cobField2);
			_CobSort.Add(cobField3);
			_CobSort.Add(cobField4);
			
			_ButSort.Add(butSort1);
			_ButSort.Add(butSort2);
			_ButSort.Add(butSort3);
			_ButSort.Add(butSort4);

			for(int i = 0;i< MAX_SORT_FIELD;i++){
				Button but = _ButSort[i] as Button ; 
				but.Click +=new EventHandler(butSort_Click);
			}
			
			(_FieldList as ArrayList).Sort(new FieldSortComparer());  

			addFieldsToList();
		}
		#endregion 构造函数...
 
		#region 界面操作事件处理...
		private void butNext_Click(object sender, System.EventArgs e) {
			clickNext(true);

		}

		private void butPrevious_Click(object sender, System.EventArgs e) {
			clickNext(false);
		}
		#endregion 界面操作事件处理...

		#region 内部函数控制处理...
		//把字段增加到操作控制的界面中
		private void addFieldsToList(){
			//int i = 0;
			foreach(RptFieldInfo info in _FieldList){
				if(info.Description ==null || info.Description.Trim().Length ==0)
					continue;
				if(string.Compare(info.DataType,"Byte[]",true)==0)
					continue;
				if(info.IsGroup){
					lstGroupFields.Items.Add(info); 
				}
				else{
					lstFields.Items.Add(info); 
				}
			}
		}
		//操作显示控制 
		private void clickNext(bool pIsNext){
			if(!pIsNext){
				bool b = checkedSortInfo();
				if(!b){
					return ;
				}
			}
			panGroup.Visible = !pIsNext;
			panSort.Visible = pIsNext;
			
			butPrevious.Enabled = pIsNext;
			butNext.Enabled = !pIsNext;
			if(pIsNext){
				addSortItems();
			}
		}
		//分组字段选择控制 
		private void moveToGroup(){
			int seIndex = lstFields.SelectedIndex;
			if(seIndex > -1){
				RptFieldInfo field = lstFields.SelectedItem as RptFieldInfo;
				field.IsGroup = true;
				field.OrderIndex = lstGroupFields.Items.Count ;
				lstFields.Items.Remove(field); 
				lstGroupFields.Items.Add(field); 

				setSelectedIndex(seIndex,lstFields);
				field.DivideName = "";
				field.IsAscending = true;
			}
		}
		private void moveFromGroup(){
			int seIndex = lstGroupFields.SelectedIndex;
			if(seIndex > -1){
				RptFieldInfo field = lstGroupFields.SelectedItem as RptFieldInfo;
				field.IsGroup = false;
				lstGroupFields.Items.Remove(field); 
				lstFields.Items.Add(field); 
				
				setSelectedIndex(seIndex,lstGroupFields);
			}
		}
		private void setSelectedIndex(int pCurrIndex,ListBox pList){
			if(pList.Items.Count <0){
				return ;
			}
			if(pCurrIndex == pList.Items.Count){
				pList.SelectedIndex = pCurrIndex -1;
			}
			else{
				pList.SelectedIndex = pCurrIndex;
			}
		}
		//先后顺序的调整
		private void sortItem(bool pIsNext){
			int selIndex = lstGroupFields.SelectedIndex;  
			if((!pIsNext &  selIndex < 1) || (pIsNext & selIndex ==  lstGroupFields.Items.Count -1)) {
				return ;
			}
			int insert = pIsNext==false?selIndex-1:selIndex +1;
			RptFieldInfo info = lstGroupFields.SelectedItem as  RptFieldInfo;

			lstGroupFields.Items.RemoveAt(selIndex);  
			lstGroupFields.Items.Insert(insert,info); 
			lstGroupFields.SelectedIndex = insert;
			 
		}
		//增加排序的项
		private void addSortItems(){
			//控制选择升降序的Button
			//int setSortIndex = 0;
			if(panSort.Visible){
				for(int i = 0; i< MAX_SORT_FIELD; i++){
					ComboBox box = _CobSort[i] as  ComboBox;
					box.Items.Clear();
					box.Items.Add("(无)"); 
					int j = 0;
					foreach(object obj in lstGroupFields.Items){
						box.Items.Add(obj);
						if((obj as RptFieldInfo).SetSort && j == i ){
							box.SelectedItem = obj;
							(_ButSort[i] as Button).Text = (obj as RptFieldInfo).IsAscending? ASC_TEXT : DESC_TEXT;
							//setSortIndex ++;
						}
						j++;
					}
				}
			}
		}
		//获取分组字段的排序信息
		private bool getSortInfo(){
			bool b = checkedSortInfo();
			if(!b){
				return false;
			}
			foreach(RptFieldInfo rpt in _FieldList){
				rpt.SetSort = false;
			}
			int orderIndex = 0;
			for(int i = 0; i< MAX_SORT_FIELD; i++){
				ComboBox box = _CobSort[i] as  ComboBox;
				if(box.SelectedIndex > 0){
					RptFieldInfo info = box.SelectedItem as RptFieldInfo;
					if(info.IsGroup){
						Button but = _ButSort[i] as Button; 
						info.SetSort = true;
						info.IsAscending = but.Text == ASC_TEXT;
						info.OrderIndex = orderIndex ++;
					}
				}
			}
			return true;
		}
		//检查分组排序的设置是否正确
		private bool checkedSortInfo(){
			ArrayList fields = new ArrayList();
			foreach(ComboBox box in _CobSort){
				string name = box.Text;
				if(name!="(无)" && name!=""){
					bool doubleSet = fields.Contains(name);
					if(doubleSet){
						MessageBox.Show("无法对相同的字段进行排序，请删除重复的字段设置!","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
						return false;
					}
					fields.Add(name);
				}
			}
			return true;
		}
		#endregion 内部函数控制处理...

		#region 界面控制Button事件...

		private void butAdd_Click(object sender, System.EventArgs e) {
			moveToGroup();
		}

		private void butRemove_Click(object sender, System.EventArgs e) {
			moveFromGroup();
		}

		private void butQuit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void butTop_Click(object sender, System.EventArgs e) {
			sortItem(false);
		}

		private void butBottom_Click(object sender, System.EventArgs e) {
			sortItem(true);
		}
		#endregion 界面控制Button事件...

		private void timControl_Tick(object sender, System.EventArgs e) {
			//控制分组字段的Button
			butAdd.Enabled = (lstFields.SelectedIndex  > -1) & (lstGroupFields.Items.Count < MAX_SORT_FIELD);

			butRemove.Enabled = lstGroupFields.SelectedIndex  > -1 ;
			butTop.Enabled = lstGroupFields.SelectedIndex > 0;
			butBottom.Enabled = lstGroupFields.SelectedIndex < lstGroupFields.Items.Count -1;
		}

		private void butSort_Click(object sender, System.EventArgs e) {
			Button but = sender as Button ;
			if(but.Text == ASC_TEXT){
				but.Text = DESC_TEXT;
			}
			else{
				but.Text = ASC_TEXT;
			}
		}

		private void butGroupSetting_Click(object sender, System.EventArgs e) {
			frmGroupSetting frm = new frmGroupSetting(_FieldList);
			frm.ShowDialog(); 
		}

		private void butFinish_Click(object sender, System.EventArgs e) {
			bool b = getSortInfo();
			if(b){
				int i = 0;
				foreach(RptFieldInfo info in lstGroupFields.Items){
					info.OrderIndex = i++;
				}
				_AfterSortAndGroup(this,e);
				this.Close();
			}
		}
	}
}
