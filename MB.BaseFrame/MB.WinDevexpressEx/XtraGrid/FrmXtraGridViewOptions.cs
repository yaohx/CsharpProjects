//---------------------------------------------------------------- 
// Author		:	checndc
// Create date	:	2009-02-17
// Description	:	FrmXtraGridViewOptions XtraGrid 网格选项设置。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Diagnostics ;
using System.Reflection;
using System.Collections;
using System.Collections.Generic; 
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;

using MB.Util;
using MB.XWinLib.DesignEditor;
using DevExpress.XtraGrid;
namespace MB.XWinLib.XtraGrid {
	/// <summary>
	/// FrmXtraGridViewOptions XtraGrid 网格选项设置。
	/// </summary>
	public class FrmXtraGridViewOptions : System.Windows.Forms.Form {
		#region 内部自动生成代码...
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button butQuit;
		private System.Windows.Forms.Button butSure;
        private System.Windows.Forms.TabPage tPageBase;
		private System.Windows.Forms.CheckBox chkAllowCellMerge;
		private System.Windows.Forms.CheckBox chkShowHorzLines;
        private System.Windows.Forms.CheckBox chkShowVertLines;
		private System.Windows.Forms.TabPage tPageConditionStyle;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListBox lstCondition;
		private System.Windows.Forms.Button butAddCondition;
		private System.Windows.Forms.Button butDeleteCondition;
		private System.Windows.Forms.TabControl tabCtlProGrid;
		private System.Windows.Forms.TabPage tPageCondition;
        private System.Windows.Forms.PropertyGrid proGridStyleSetting;
        private Button btnApply;
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSure = new System.Windows.Forms.Button();
            this.butQuit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPageBase = new System.Windows.Forms.TabPage();
            this.chkShowVertLines = new System.Windows.Forms.CheckBox();
            this.chkShowHorzLines = new System.Windows.Forms.CheckBox();
            this.chkAllowCellMerge = new System.Windows.Forms.CheckBox();
            this.tPageConditionStyle = new System.Windows.Forms.TabPage();
            this.tabCtlProGrid = new System.Windows.Forms.TabControl();
            this.tPageCondition = new System.Windows.Forms.TabPage();
            this.proGridStyleSetting = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstCondition = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butDeleteCondition = new System.Windows.Forms.Button();
            this.butAddCondition = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tPageBase.SuspendLayout();
            this.tPageConditionStyle.SuspendLayout();
            this.tabCtlProGrid.SuspendLayout();
            this.tPageCondition.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.butSure);
            this.panel1.Controls.Add(this.butQuit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 40);
            this.panel1.TabIndex = 1;
            // 
            // butSure
            // 
            this.butSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSure.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.butSure.Location = new System.Drawing.Point(164, 8);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(64, 24);
            this.butSure.TabIndex = 1;
            this.butSure.Text = "确定(&S)";
            this.butSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // butQuit
            // 
            this.butQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.butQuit.Location = new System.Drawing.Point(304, 8);
            this.butQuit.Name = "butQuit";
            this.butQuit.Size = new System.Drawing.Size(64, 24);
            this.butQuit.TabIndex = 0;
            this.butQuit.Text = "关闭(&Q)";
            this.butQuit.Click += new System.EventHandler(this.butQuit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPageBase);
            this.tabControl1.Controls.Add(this.tPageConditionStyle);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(376, 406);
            this.tabControl1.TabIndex = 2;
            // 
            // tPageBase
            // 
            this.tPageBase.BackColor = System.Drawing.Color.White;
            this.tPageBase.Controls.Add(this.chkShowVertLines);
            this.tPageBase.Controls.Add(this.chkShowHorzLines);
            this.tPageBase.Controls.Add(this.chkAllowCellMerge);
            this.tPageBase.Location = new System.Drawing.Point(4, 22);
            this.tPageBase.Name = "tPageBase";
            this.tPageBase.Size = new System.Drawing.Size(368, 380);
            this.tPageBase.TabIndex = 0;
            this.tPageBase.Text = "常规";
            this.tPageBase.UseVisualStyleBackColor = true;
            // 
            // chkShowVertLines
            // 
            this.chkShowVertLines.Checked = true;
            this.chkShowVertLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowVertLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkShowVertLines.Location = new System.Drawing.Point(16, 88);
            this.chkShowVertLines.Name = "chkShowVertLines";
            this.chkShowVertLines.Size = new System.Drawing.Size(160, 24);
            this.chkShowVertLines.TabIndex = 2;
            this.chkShowVertLines.Text = "显示垂直网格线";
            // 
            // chkShowHorzLines
            // 
            this.chkShowHorzLines.Checked = true;
            this.chkShowHorzLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowHorzLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkShowHorzLines.Location = new System.Drawing.Point(16, 64);
            this.chkShowHorzLines.Name = "chkShowHorzLines";
            this.chkShowHorzLines.Size = new System.Drawing.Size(176, 16);
            this.chkShowHorzLines.TabIndex = 1;
            this.chkShowHorzLines.Text = "显示水平网格线";
            // 
            // chkAllowCellMerge
            // 
            this.chkAllowCellMerge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkAllowCellMerge.Location = new System.Drawing.Point(16, 32);
            this.chkAllowCellMerge.Name = "chkAllowCellMerge";
            this.chkAllowCellMerge.Size = new System.Drawing.Size(176, 24);
            this.chkAllowCellMerge.TabIndex = 0;
            this.chkAllowCellMerge.Text = "允许相同合并";
            this.chkAllowCellMerge.CheckedChanged += new System.EventHandler(this.chkAllowCellMerge_CheckedChanged);
            // 
            // tPageConditionStyle
            // 
            this.tPageConditionStyle.Controls.Add(this.tabCtlProGrid);
            this.tPageConditionStyle.Controls.Add(this.splitter1);
            this.tPageConditionStyle.Controls.Add(this.groupBox2);
            this.tPageConditionStyle.Location = new System.Drawing.Point(4, 22);
            this.tPageConditionStyle.Name = "tPageConditionStyle";
            this.tPageConditionStyle.Size = new System.Drawing.Size(368, 380);
            this.tPageConditionStyle.TabIndex = 3;
            this.tPageConditionStyle.Text = "条件样式";
            this.tPageConditionStyle.UseVisualStyleBackColor = true;
            // 
            // tabCtlProGrid
            // 
            this.tabCtlProGrid.Controls.Add(this.tPageCondition);
            this.tabCtlProGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlProGrid.Location = new System.Drawing.Point(148, 0);
            this.tabCtlProGrid.Name = "tabCtlProGrid";
            this.tabCtlProGrid.SelectedIndex = 0;
            this.tabCtlProGrid.Size = new System.Drawing.Size(220, 380);
            this.tabCtlProGrid.TabIndex = 2;
            // 
            // tPageCondition
            // 
            this.tPageCondition.Controls.Add(this.proGridStyleSetting);
            this.tPageCondition.Location = new System.Drawing.Point(4, 22);
            this.tPageCondition.Name = "tPageCondition";
            this.tPageCondition.Size = new System.Drawing.Size(212, 354);
            this.tPageCondition.TabIndex = 0;
            this.tPageCondition.Text = "属性";
            // 
            // proGridStyleSetting
            // 
            this.proGridStyleSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proGridStyleSetting.HelpVisible = false;
            this.proGridStyleSetting.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.proGridStyleSetting.Location = new System.Drawing.Point(0, 0);
            this.proGridStyleSetting.Name = "proGridStyleSetting";
            this.proGridStyleSetting.Size = new System.Drawing.Size(212, 354);
            this.proGridStyleSetting.TabIndex = 0;
            this.proGridStyleSetting.ToolbarVisible = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(144, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 380);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.lstCondition);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 380);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条件：";
            // 
            // lstCondition
            // 
            this.lstCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCondition.ItemHeight = 12;
            this.lstCondition.Location = new System.Drawing.Point(3, 48);
            this.lstCondition.Name = "lstCondition";
            this.lstCondition.Size = new System.Drawing.Size(138, 329);
            this.lstCondition.TabIndex = 1;
            this.lstCondition.SelectedIndexChanged += new System.EventHandler(this.lstCondition_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.butDeleteCondition);
            this.panel2.Controls.Add(this.butAddCondition);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(138, 31);
            this.panel2.TabIndex = 0;
            // 
            // butDeleteCondition
            // 
            this.butDeleteCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butDeleteCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butDeleteCondition.Location = new System.Drawing.Point(104, 8);
            this.butDeleteCondition.Name = "butDeleteCondition";
            this.butDeleteCondition.Size = new System.Drawing.Size(24, 20);
            this.butDeleteCondition.TabIndex = 1;
            this.butDeleteCondition.Text = "-";
            this.butDeleteCondition.Click += new System.EventHandler(this.butDeleteCondition_Click);
            // 
            // butAddCondition
            // 
            this.butAddCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAddCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAddCondition.Location = new System.Drawing.Point(72, 8);
            this.butAddCondition.Name = "butAddCondition";
            this.butAddCondition.Size = new System.Drawing.Size(24, 20);
            this.butAddCondition.TabIndex = 0;
            this.butAddCondition.Text = "+";
            this.butAddCondition.Click += new System.EventHandler(this.butAddCondition_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnApply.Location = new System.Drawing.Point(234, 8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(64, 24);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应用(&A)";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // FrmXtraGridViewOptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(376, 446);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmXtraGridViewOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "样式选项";
            this.Load += new System.EventHandler(this.FrmXtraGridViewOptions_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tPageBase.ResumeLayout(false);
            this.tPageConditionStyle.ResumeLayout(false);
            this.tabCtlProGrid.ResumeLayout(false);
            this.tPageCondition.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

        private DevExpress.XtraGrid.Views.Grid.GridView _GridView;
		private GridViewOptionsInfo _CurrentOptionsInfo;
		private string _OptionsCfgFile;//样式存储文件名称
        private string _XmlCfgFileName;//XML 配置文件名称
       
		private const int MAX_SPEED = 400;
		
		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		/// <param name="optionsInfo"></param>
        public FrmXtraGridViewOptions(DevExpress.XtraGrid.Views.Grid.GridView gridView,GridViewOptionsInfo currentOptionsInfo, string optionsCfgFile) {

			InitializeComponent();
			
			_OptionsCfgFile = optionsCfgFile;
			_CurrentOptionsInfo = currentOptionsInfo;

            _GridView = gridView;
			
			setByOptionsInfo();
		}
		#endregion 构造函数...

		#region 界面事件...
		private void butQuit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

        private void btnApply_Click(object sender, EventArgs e)
        {
            createOptionsInfoByCtls();

            if (_CurrentOptionsInfo.StyleConditions == null || _CurrentOptionsInfo.StyleConditions.Count == 0)
            {
                _GridView.FormatConditions.Clear();
            }
            else
            {
                MB.XWinLib.XtraGrid.XtraGridViewHelper.Instance.AddStylesForConditions(_GridView.GridControl, _CurrentOptionsInfo.StyleConditions);
            }
        }

		private void butSure_Click(object sender, System.EventArgs e) {
            this.DialogResult = DialogResult.OK;

			createOptionsInfoByCtls();

            // 当全部删除格式条件时,应该马上清除.XiaoMin
            if (_CurrentOptionsInfo.StyleConditions == null || _CurrentOptionsInfo.StyleConditions.Count == 0)
            {
                _GridView.FormatConditions.Clear();
            }
            else
            {
                MB.XWinLib.XtraGrid.XtraGridViewHelper.Instance.AddStylesForConditions(_GridView.GridControl, _CurrentOptionsInfo.StyleConditions);
            }
            this.Close();
		}

		private void butFont_Click(object sender, System.EventArgs e) {
			System.Windows.Forms.FontDialog fDialog = new FontDialog();
			fDialog.Font = _CurrentOptionsInfo.ForeFont;
			if(fDialog.ShowDialog() ==DialogResult.OK){
				_CurrentOptionsInfo.ForeFont = fDialog.Font;
				
			}
		}
		private void butColor_Click(object sender, System.EventArgs e) {
			System.Windows.Forms.ColorDialog fDialog = new ColorDialog();
			fDialog.Color = _CurrentOptionsInfo.ForeColor;
			if(fDialog.ShowDialog() ==DialogResult.OK){
				_CurrentOptionsInfo.ForeColor = fDialog.Color;
				
			}
		}
		private void chkAutoScroll_CheckedChanged(object sender, System.EventArgs e) {

		}
		#endregion 界面事件...
		
		#region 内部函数处理相关...
		private void setByOptionsInfo(){
			if(_CurrentOptionsInfo==null)
				return;
			chkAllowCellMerge.Checked =  _CurrentOptionsInfo.AllowCellMerge;
			chkShowHorzLines.Checked = _CurrentOptionsInfo.ShowHorzLines;
			chkShowVertLines.Checked = _CurrentOptionsInfo.ShowVertLines;
			foreach(XtraStyleConditionInfo info in  _CurrentOptionsInfo.StyleConditions){
                // 绑定Tag.XiaoMin
                info.Tag = this._GridView;
				lstCondition.Items.Add(info); 
			}
			if(lstCondition.Items.Count > 0)
				lstCondition.SelectedIndex = 0;

		}
		private void createOptionsInfoByCtls(){
			_CurrentOptionsInfo.AllowCellMerge = chkAllowCellMerge.Checked;
			_CurrentOptionsInfo.ShowHorzLines = chkShowHorzLines.Checked;
			_CurrentOptionsInfo.ShowVertLines = chkShowVertLines.Checked;
			GridViewOptionsHelper.Instance.SaveOptionsInfoToXML(_CurrentOptionsInfo,_OptionsCfgFile);
		}

		#endregion 内部函数处理相关...

		private void chkAllowCellMerge_CheckedChanged(object sender, System.EventArgs e) {
		}

		private void lstCondition_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(lstCondition.SelectedIndex >= 0){
				proGridStyleSetting.SelectedObject =  lstCondition.SelectedItem;
			}
		}

		private void butDeleteCondition_Click(object sender, System.EventArgs e) {
			if(lstCondition.SelectedIndex < 0){
				MB.WinBase.MessageBoxEx.Show("请先选择需要删除的条件样式。"); 
				return;
			}
			int cSelIndex = lstCondition.SelectedIndex;
			lstCondition.Items.Remove(lstCondition.SelectedItem);
			_CurrentOptionsInfo.StyleConditions.RemoveAt(cSelIndex);
			
			if(cSelIndex >0){
				lstCondition.SelectedIndex = cSelIndex -1;
			}
			if(cSelIndex==0 && lstCondition.Items.Count > 0)
				lstCondition.SelectedIndex = 0;
		}

		private void butAddCondition_Click(object sender, System.EventArgs e) {
			string cName = getConditionName(_CurrentOptionsInfo.StyleConditions);
            XtraStyleConditionInfo cInfo = new XtraStyleConditionInfo(cName);
            cInfo.Tag = _GridView;
			_CurrentOptionsInfo.StyleConditions.Add(cInfo);
			lstCondition.Items.Add(cInfo);
			lstCondition.SelectedItem = cInfo;
		}

		//获取样式表达的名称
        private string getConditionName(IList<XtraStyleConditionInfo> conditions)
        {
			int i = 0;
			string CNAME = "条件样式:";
			while(true){
				bool exists = false;
                foreach (var info in conditions)
                {
					if(info.Name.Equals(CNAME + i.ToString())){
						exists = true;
						break;
					}
				}
				if(!exists){
					return CNAME + i.ToString();
				}
				i ++;
			}
		}

		private void FrmXtraGridViewOptions_Load(object sender, System.EventArgs e) {
		}

        
	}


	
	#region GridView 选项设置非窗口处理相关...
	/// <summary>
	/// XtraGrid 选择设置接口。
	/// </summary>
	public interface IGridViewOptions{
		GridViewOptionsInfo OptionsInfo{get;set;}
		MB.XWinLib.XtraGrid.GridControlEx GridCtl{get;} 
	}
	
	/// <summary>
	/// 提供Grid View 配置项到XML 文件的存储处理。
	/// </summary>
	public class GridViewOptionsHelper{
		private static readonly string CFG_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"GridViewOprions\" ;
		private static readonly string MAIN_NODE_NAME = "XtraGrid";
        private static readonly string ASSEMBLY_PATH = MB.Util.General.GeApplicationDirectory();  
	   #region Instance...
        private static Object _Obj = new object();
        private static GridViewOptionsHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected GridViewOptionsHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static GridViewOptionsHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new GridViewOptionsHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...
		
		#region public 成员...
		/// <summary>
		/// 把设置的选项存储到XML 文件中。
		/// </summary>
		/// <param name="info"></param>
		/// <param name="xmlFileName"></param>
		public void SaveOptionsInfoToXML(GridViewOptionsInfo info,string xmlFileName){
			StringBuilder xmlSB = new StringBuilder();
			try{
				//描述
				xmlSB.Append("<?xml version='1.0' encoding='utf-8' ?>");
				xmlSB.Append("<!-- Copyright (C) 2009-2010 Chen.Dc Soft (www.mb.com)  -->");
				//xmlSB.Append("<XtraGrid>");
				writeFirstMarker(xmlSB,MAIN_NODE_NAME);
			
				string objXml = objectToXml(info);
				xmlSB.Append(objXml);

				writeLastMarker(xmlSB,MAIN_NODE_NAME); 

				if(!System.IO.Directory.Exists(CFG_FILE_PATH))
					System.IO.Directory.CreateDirectory(CFG_FILE_PATH);
			
				System.Xml.XmlDocument xDoc = new XmlDocument();
			
				xDoc.LoadXml(xmlSB.ToString());
				string fullFileName = CFG_FILE_PATH +  xmlFileName;

				xDoc.Save(fullFileName);
			}
			catch(Exception e){
				MB.Util.TraceEx.Write("把选项数据存储到XML 文件时出错。" + e.Message);
			}

		}
		/// <summary>
		/// 从指定的XML 样式文件中创建ViewOptionsInfo.
		/// </summary>
		/// <param name="xmlFileName"></param>
		/// <returns></returns>
		public GridViewOptionsInfo CreateFromXMLToInfo(string xmlFileName){
			string fullFileName = CFG_FILE_PATH +  xmlFileName;
			GridViewOptionsInfo info = new GridViewOptionsInfo();
			if(System.IO.File.Exists(fullFileName)){
				try{
					System.Xml.XmlDocument xDoc = new XmlDocument();
					xDoc.Load(fullFileName); 
					XmlNode xNode = xDoc.SelectSingleNode( "/" + MAIN_NODE_NAME);
					setObjectProperty(xNode,info);
                }
                catch(Exception e){
                    MB.Util.TraceEx.Write("把配置的XML 文件信息存储为描述的Info 对象时出错：" + e.Message);
                }
			}
			return info;
		}

		#endregion public 成员...

		#region 内部函数处理...
		#region write to xml 文件相关...
		//把Info 对象转换为XML 文件
		private string objectToXml(object obj,params string[] proList){
			StringBuilder sXml = new StringBuilder();
			Type t = obj.GetType();
			//writeFirstMarker(sXml,t.FullName);

			PropertyInfo[] pros = t.GetProperties();
			foreach(PropertyInfo info in pros){
				if(info.IsSpecialName)
					continue;
				if(proList!=null && proList.Length > 0 && Array.IndexOf(proList,info.Name)== -1)
					continue;
				if(!info.CanRead || !info.CanWrite)
					continue;
				Type proType = info.PropertyType;
				// remark:为条件样式加上类型
                object val = info.GetValue(obj, null);
                if (info.Name == "StyleConditions") {
                    StringBuilder sbNodeName = new StringBuilder();
                    sbNodeName.Append(info.Name);

                    sbNodeName.Append("  type=\"System.Collections.Generic.IList\"");
                    writeFirstMarker(sXml, sbNodeName.ToString());
                    processObject(sXml, val);
                }
                else {
                    writeFirstMarker(sXml, info.Name);
                    if (val != null) {
                        switch (proType.Name) {
                            case "Color":
                                sXml.Append(((Color)val).ToArgb().ToString());
                                break;
                            case "Font":

                                processObject(sXml, val, new string[] { "Name", "Size", "Bold", "Underline", "Italic", "Strikeout" });
                                break;
                            //case "IList": //"StyleConditions": edited by Arthur Remark  实现样式表的设定
                            //    processObject(sXml, val);
                            //    break;
                            default:

                                sXml.Append(val.ToString());
                                break;
                        }
                    }
                }
				writeLastMarker(sXml,info.Name);
				 
				 
			}
			//writeLastMarker(sXml,t.FullName);
			return sXml.ToString();
		}
		/// <summary>
		/// Reamark:处理样式表到XML
		/// </summary>
		/// <param name="sXml"></param>
		/// <param name="val"></param>
		private void processObject(StringBuilder sXml,object val)
		{
            List<MB.XWinLib.DesignEditor.XtraStyleConditionInfo> styleList = val as List<MB.XWinLib.DesignEditor.XtraStyleConditionInfo>;
			if(styleList == null  || styleList.Count < 1 ) return;

            string typeName = "\"MB.XWinLib.DesignEditor.XtraStyleConditionInfo\"";
            string assemblyName = "\"MB.XWinLib.Dll\"";
			StringBuilder sbNodeName = new StringBuilder();
			sbNodeName.Append("StyleConditionInfo  type=");				
			sbNodeName.Append(typeName);				
			sbNodeName.Append("  Assembley=");
			sbNodeName.Append(assemblyName);
            foreach (MB.XWinLib.DesignEditor.XtraStyleConditionInfo cInfo in styleList)
			{
				
				writeFirstMarker(sXml,sbNodeName.ToString());				
				processObject(sXml,cInfo,
                    new string[]{"Name","ApplyToRow","BackColor","ColumnName","ColumnCaption","Condition","Expression",
                   "ForeColor","ForeFont","IsByEvent","MarkImage","Value","Value2","ValueDisplayText","Value2DisplayText"});				
                writeLastMarker(sXml,"StyleConditionInfo");
			}
			
		}
		private void processObject(StringBuilder sXml,object val,params string[] proList){
			Type t = val.GetType();
			PropertyInfo[] pros = t.GetProperties();
			foreach(PropertyInfo info in pros){
				if(info.IsSpecialName)
					continue;
				if(proList!=null && proList.Length > 0 && Array.IndexOf(proList,info.Name)== -1)
					continue;
				bool mustDo = (proList!=null && proList.Length > 0 && Array.IndexOf(proList,info.Name)> -1);
				if(!mustDo){
					if(!info.CanRead || !info.CanWrite)
						continue;
				}
				Type proType = info.PropertyType;
				object v = info.GetValue(val,null);
				writeFirstMarker(sXml,info.Name);
                if (proType.Name == "Color") {
                    sXml.Append(((Color)v).ToArgb().ToString() );
                }
                else if (proType.IsEnum) {
                    if (v != null) {
                        string eName = Enum.GetName(info.PropertyType, v);
                        sXml.Append(eName);
                    }
                }
				else if(proType.IsValueType || string.Compare(proType.Name,"String",true)==0)
				{ 
					if(v!=null)
					{ 
						sXml.Append(v.ToString());
					}
				}
				else if(string.Compare(proType.Name,"Image",true)==0 )
				{
                    if(v != null )
					    sXml.Append(MB.Util.MyConvert.Instance.ImageToBase64String(v as System.Drawing.Image));  
				}
				else if(proType.Name == "Font")
				{
					if(v != null)
					processObject(sXml,v,new string[]{"Name","Size","Bold","Underline","Italic","Strikeout"});
				}
				// Remark:判断样式信息
				else if(string.Compare(proType.Name,"bool",true)==0)
				{
					
					if(v!=null)
					{
						sXml.Append(v.ToString());
					}
				}
				// end of added
				else
				{
					Debug.Assert(false,"下级的类型目前先不处理."); 
				}
				writeLastMarker(sXml,info.Name);
			}
		}
		private void writeFirstMarker(StringBuilder sXml,string nodeName){
			sXml.Append("<");
			sXml.Append(nodeName);
			sXml.Append(">");
		}
		private void writeLastMarker(StringBuilder sXml,string nodeName){
			sXml.Append("</");
			sXml.Append(nodeName);
			sXml.Append(">");
		}
		#endregion write to xml 文件相关...
		
		#region read from XML 相关...
		private void setObjectProperty(XmlNode parentNode,object obj){
			Type t = obj.GetType();
			foreach(XmlNode node in parentNode.ChildNodes){
				if(node.NodeType!=System.Xml.XmlNodeType.Element)
					continue;
				PropertyInfo pInfo = t.GetProperty(node.Name);
				if(pInfo==null)
					continue;
				if(pInfo.PropertyType.IsEnum ){
					pInfo.SetValue(obj,Enum.Parse(pInfo.PropertyType,node.InnerText,true),null);
				}
                else if(string.Compare(pInfo.Name,"StyleConditions",true)==0){
                     pInfo.SetValue(obj, createStyleInfoList(node, ""), null);
                }
				else if(string.Compare(pInfo.PropertyType.Name,"string",true)==0){
					pInfo.SetValue(obj, node.InnerText,null);
				}
				else{switch(pInfo.PropertyType.Name){
						 case "Color":
							 pInfo.SetValue(obj, Color.FromArgb(int.Parse(node.InnerText)),null);
							 break;
						 case "Font":
							 pInfo.SetValue(obj, createFont(node),null);
							 break;
						 default:
                            
							 if(pInfo.PropertyType.IsValueType){
								 pInfo.SetValue(obj,Convert.ChangeType(node.InnerText,pInfo.PropertyType),null);
							 }
							 else
								 Debug.Assert(false,"类型" + pInfo.Name + "还没有处理.");
							 break;
					 }
				}
			}
		}
		/// <summary>
		/// 获取设置的样式集合
		/// </summary>
		/// <param name="parentNode"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
        private List<MB.XWinLib.DesignEditor.XtraStyleConditionInfo> createStyleInfoList(XmlNode parentNode, object obj)
		{
            List<MB.XWinLib.DesignEditor.XtraStyleConditionInfo> nodeStylesList = new List<MB.XWinLib.DesignEditor.XtraStyleConditionInfo>();
			try
			{
				foreach(XmlNode node in parentNode.ChildNodes)
				{					
					System.Xml.XmlAttributeCollection nodeAttList = node.Attributes;
                    MB.XWinLib.DesignEditor.XtraStyleConditionInfo newStyleInfo = new XtraStyleConditionInfo();
                    Type t = newStyleInfo.GetType();					
					PropertyInfo[] pInfos  = t.GetProperties();

                    if (newStyleInfo == null) continue;
					foreach(XmlNode xmlNode in node.ChildNodes)
					{							
						PropertyInfo propValue = t.GetProperty(xmlNode.Name);
						if(propValue == null)continue;
						if(string.Compare(propValue.PropertyType.Name,"string",true)==0 )
						{
                            propValue.SetValue(newStyleInfo, xmlNode.InnerText, null);
						}
						else if (propValue.PropertyType.IsEnum && string.Compare(xmlNode.Name,"Condition",true)==0 )
						{
                            propValue.SetValue(newStyleInfo, FromName(xmlNode.InnerText), null);
						}
						else
						{
							switch(propValue.PropertyType.Name)
							{
								case "Color":
                                    // 原来的代码保存的是Argb,读取的是FormName.改成一致使用Argb.XiaoMin
                                    int argb = 0;
                                    if (int.TryParse(xmlNode.InnerText, out argb))
                                    {
                                        propValue.SetValue(newStyleInfo, Color.FromArgb(argb), null);
                                    }
                                    else
                                    {
                                        // 写入日志,颜色转换有误
                                    }               
									break;
								case "Font":
									object f =  createFont(xmlNode);
                                    if(f!=null)
                                        propValue.SetValue(newStyleInfo, f, null);
									break;
								case "Image":
                                    if(!string.IsNullOrEmpty(xmlNode.InnerText))
                                        propValue.SetValue(newStyleInfo, MB.Util.MyConvert.Instance.Base64StringToImage(xmlNode.InnerText), null);
									break;
								default:
									if(propValue.PropertyType.IsValueType)
									{
                                        propValue.SetValue(newStyleInfo, Convert.ChangeType(xmlNode.InnerText, propValue.PropertyType), null);
									}
									else
										Debug.Assert(false,"类型" + xmlNode.Name + "还没有处理.");
									break;	
							}							
						}
					}
                    nodeStylesList.Add(newStyleInfo);
				}
					
			}
			catch(TypeLoadException)
			{

			}
			return nodeStylesList;
             
		}

        /// <summary>
        /// 获取FormatConditionEnum
        /// </summary>
        /// <param name="conditionName">条件名称</param>
        /// <returns>FormatConditionEnum枚举</returns>
        private FormatConditionEnum FromName(string conditionName)
        {
            return (FormatConditionEnum)Enum.Parse(typeof(FormatConditionEnum), conditionName);
        }

		private  Font createFont(XmlNode node){
            XmlNode childNode = getChildNodeByName(node.ChildNodes, "Name");
            if (childNode == null) return null;
            string fontName = childNode.InnerText;
			float fontSize = MB.Util.MyConvert .Instance.ToFloat(getChildNodeByName(node.ChildNodes, "Size").InnerText,2);
			bool bold = bool.Parse(getChildNodeByName(node.ChildNodes,"Bold").InnerText);
			bool underline = bool.Parse(getChildNodeByName(node.ChildNodes,"Underline").InnerText);
			bool italic = bool.Parse(getChildNodeByName(node.ChildNodes,"Italic").InnerText);
			bool strikeout = bool.Parse(getChildNodeByName(node.ChildNodes,"Strikeout").InnerText);
			FontStyle fontType = FontStyle.Regular;
			if(bold){fontType = fontType | FontStyle.Bold ;}
			if(underline){fontType = fontType | FontStyle.Underline ;}
			if(italic){fontType = fontType | FontStyle.Italic ;}
			Font f = new Font(fontName,fontSize);
			Font font = new Font(f,fontType); 
			return font;
			
		}

		private  XmlNode getChildNodeByName(XmlNodeList nodes ,string name){
			foreach(XmlNode node in nodes){
				if(string.Compare(node.Name,name,true)==0)
					return node;
			}
			return null;
		}
		#endregion read from XML 相关...

		#endregion 内部函数处理...
	}


	/// <summary>
	/// 样式描述信息。
	/// </summary>
	public class GridViewOptionsInfo{
		private bool _AllowCellMerge;//判断相同Cell 是否要合并
		private bool _ShowHorzLines;//
		private bool _ShowVertLines;
		private System.Drawing.Font _ForeFont;
		private System.Drawing.Color _ForeColor;
		private bool _AutoScroll;//判断是否启动自动滚动效果
		private bool _InRow;//判断是否在行内滚动
		private int _Speed;//滚动的速度
		private int _Conditions;//条件。

		private List<XtraStyleConditionInfo>  _StyleConditions;
		
		#region 构造函数...
		/// <summary>
		/// 构造函数
		/// </summary>
		public GridViewOptionsInfo(){
			_AllowCellMerge = false;
			_ShowHorzLines = true;
			_ShowVertLines = true;
			_AutoScroll = false;
			_InRow = true;
			_Speed = 200;
			_ForeFont = new Font("宋体",9f);
			_ForeColor = System.Drawing.Color.Black;

            _StyleConditions = new List<XtraStyleConditionInfo>();
		}
		#endregion 构造函数...

		#region public 属性...
		/// <summary>
		/// 判断相同Cell 是否要合并.
		/// </summary>
		public bool AllowCellMerge{
			get{
				return _AllowCellMerge;
			}
			set{
				_AllowCellMerge = value;
			}
		}
		/// <summary>
		/// 显示水平网格线。
		/// </summary> 
		public bool ShowHorzLines{
			get{
				return _ShowHorzLines;
			}
			set{
				_ShowHorzLines = value;
			}
		}
		/// <summary>
		/// 显示垂直网格线。
		/// </summary>
		public bool ShowVertLines{
			get{
				return _ShowVertLines;
			}
			set{
				_ShowVertLines = value;
			}
		}
		/// <summary>
		/// 字体
		/// </summary>
		public System.Drawing.Font ForeFont{
			get{
				return _ForeFont;
			}
			set{
				_ForeFont = value;
			}
		}
		/// <summary>
		/// 字体颜色
		/// </summary>
		public System.Drawing.Color ForeColor{
			get{
				return _ForeColor;
			}
			set{
				_ForeColor = value;
			}
		}
		/// <summary>
		/// 是否启动滚动效果
		/// </summary>
		public bool AutoScroll{
			get{
				return _AutoScroll;
			}
			set{
				_AutoScroll = value;
			}
		}
		/// <summary>
		/// 判断滚动是否在行内执行
		/// </summary>
		public bool InRow{
			get{
				return _InRow;
			}
			set{
				_InRow = value;
			}
		}
		/// <summary>
		/// 滚动速度
		/// </summary>
		public int Speed{
			get{
				return _Speed;
			}
			set{
				_Speed = value;
			}
		}
		/// <summary>
		/// 条件。
		/// </summary>
		public int Conditions{
			get{
				return _Conditions;
			}
			set{
				_Conditions = value;
			}
		}
		/// <summary>
		/// 样式的条件设置。
		/// </summary>
        public List<XtraStyleConditionInfo> StyleConditions {
			get{
				return _StyleConditions;
			}
			set{
				_StyleConditions = value;
			}
		}
		#endregion public 属性...
		

	}
	#endregion GridView 选项设置非窗口处理相关...


}
