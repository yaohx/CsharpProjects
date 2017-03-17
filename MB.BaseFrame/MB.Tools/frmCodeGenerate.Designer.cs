namespace MB.Tools {
    partial class frmCodeGenerate {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCodeGenerate));
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabelServerCodeFolder = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.cobTableName = new System.Windows.Forms.ComboBox();
            this.cobGenerateType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butCodeGenerate = new System.Windows.Forms.Button();
            this.txtSqlScript = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.tabCtlMain = new System.Windows.Forms.TabControl();
            this.tPageLanguage = new System.Windows.Forms.TabPage();
            this.tabPageLanguage = new System.Windows.Forms.TabControl();
            this.tPageLanguageText = new System.Windows.Forms.TabPage();
            this.rxtLanguage = new System.Windows.Forms.RichTextBox();
            this.tPageLanguageControl = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.butRefurbish = new System.Windows.Forms.Button();
            this.tPageUIView = new System.Windows.Forms.TabPage();
            this.rxtUIXmlView = new System.Windows.Forms.RichTextBox();
            this.tPageUIRule = new System.Windows.Forms.TabPage();
            this.rxtUIRule = new System.Windows.Forms.RichTextBox();
            this.tPageEditForm = new System.Windows.Forms.TabPage();
            this.rxtUIEditForm = new System.Windows.Forms.RichTextBox();
            this.tPageServerEntity = new System.Windows.Forms.TabPage();
            this.rxtServerEntity = new System.Windows.Forms.RichTextBox();
            this.tPageServerSql = new System.Windows.Forms.TabPage();
            this.rxtServerSqlXml = new System.Windows.Forms.RichTextBox();
            this.tPageServerRule = new System.Windows.Forms.TabPage();
            this.rxtServerRule = new System.Windows.Forms.RichTextBox();
            this.tPageServerInterface = new System.Windows.Forms.TabPage();
            this.rxtServerInterface = new System.Windows.Forms.RichTextBox();
            this.nullValueProvider1 = new MB.Tools.NullValueProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabelClientCodeFolder = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.tabCtlMain.SuspendLayout();
            this.tPageLanguage.SuspendLayout();
            this.tabPageLanguage.SuspendLayout();
            this.tPageLanguageText.SuspendLayout();
            this.tPageLanguageControl.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.tPageUIView.SuspendLayout();
            this.tPageUIRule.SuspendLayout();
            this.tPageEditForm.SuspendLayout();
            this.tPageServerEntity.SuspendLayout();
            this.tPageServerSql.SuspendLayout();
            this.tPageServerRule.SuspendLayout();
            this.tPageServerInterface.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.linkLabelClientCodeFolder);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.linkLabelServerCodeFolder);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cobTableName);
            this.panel1.Controls.Add(this.cobGenerateType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtEntityName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.butCodeGenerate);
            this.panel1.Controls.Add(this.txtSqlScript);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 103);
            this.panel1.TabIndex = 0;
            // 
            // linkLabelServerCodeFolder
            // 
            this.linkLabelServerCodeFolder.AutoSize = true;
            this.linkLabelServerCodeFolder.Location = new System.Drawing.Point(743, 22);
            this.linkLabelServerCodeFolder.Name = "linkLabelServerCodeFolder";
            this.linkLabelServerCodeFolder.Size = new System.Drawing.Size(55, 13);
            this.linkLabelServerCodeFolder.TabIndex = 10;
            this.linkLabelServerCodeFolder.TabStop = true;
            this.linkLabelServerCodeFolder.Text = "linkLabel1";
            this.linkLabelServerCodeFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGenerateCodeFolderServer_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(684, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "生成代码存放路径:";
            // 
            // cobTableName
            // 
            this.cobTableName.ForeColor = System.Drawing.Color.Gray;
            this.cobTableName.FormattingEnabled = true;
            this.cobTableName.Location = new System.Drawing.Point(14, 27);
            this.cobTableName.Name = "cobTableName";
            this.nullValueProvider1.SetNullValueDescription(this.cobTableName, "<<请输入表名称>>");
            this.cobTableName.Size = new System.Drawing.Size(160, 21);
            this.cobTableName.TabIndex = 0;
            this.cobTableName.Text = "<<请输入表名称>>";
            this.cobTableName.SelectedIndexChanged += new System.EventHandler(this.cobTableName_SelectedIndexChanged);
            this.cobTableName.Leave += new System.EventHandler(this.txtTableName_Leave);
            // 
            // cobGenerateType
            // 
            this.cobGenerateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobGenerateType.FormattingEnabled = true;
            this.cobGenerateType.Location = new System.Drawing.Point(393, 27);
            this.cobGenerateType.Name = "cobGenerateType";
            this.cobGenerateType.Size = new System.Drawing.Size(172, 21);
            this.cobGenerateType.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(394, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "生成类型：";
            // 
            // txtEntityName
            // 
            this.txtEntityName.ForeColor = System.Drawing.Color.Gray;
            this.txtEntityName.Location = new System.Drawing.Point(199, 26);
            this.txtEntityName.Name = "txtEntityName";
            this.nullValueProvider1.SetNullValueDescription(this.txtEntityName, "<<请输入数据实体名称>>");
            this.txtEntityName.Size = new System.Drawing.Size(170, 20);
            this.txtEntityName.TabIndex = 1;
            this.txtEntityName.Text = "<<请输入数据实体名称>>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(197, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "实体名称：";
            // 
            // butCodeGenerate
            // 
            this.butCodeGenerate.ForeColor = System.Drawing.Color.Black;
            this.butCodeGenerate.Location = new System.Drawing.Point(596, 3);
            this.butCodeGenerate.Name = "butCodeGenerate";
            this.butCodeGenerate.Size = new System.Drawing.Size(82, 46);
            this.butCodeGenerate.TabIndex = 4;
            this.butCodeGenerate.Text = "执行(&G)";
            this.butCodeGenerate.UseVisualStyleBackColor = true;
            this.butCodeGenerate.Click += new System.EventHandler(this.butCodeGenerate_Click);
            // 
            // txtSqlScript
            // 
            this.txtSqlScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlScript.ForeColor = System.Drawing.Color.Gray;
            this.txtSqlScript.Location = new System.Drawing.Point(14, 68);
            this.txtSqlScript.Multiline = true;
            this.txtSqlScript.Name = "txtSqlScript";
            this.nullValueProvider1.SetNullValueDescription(this.txtSqlScript, "<<请输入SQL语句，如果为空将根据表名称自动拼接SQL语句>>");
            this.txtSqlScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSqlScript.Size = new System.Drawing.Size(919, 29);
            this.txtSqlScript.TabIndex = 3;
            this.txtSqlScript.Text = "<<请输入SQL语句，如果为空将根据表名称自动拼接SQL语句>>";
            this.txtSqlScript.Leave += new System.EventHandler(this.OnTxtSqlScriptLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "SQL 脚本：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "表名称：";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 103);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(937, 4);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // txtFileName
            // 
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFileName.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFileName.ForeColor = System.Drawing.Color.Red;
            this.txtFileName.Location = new System.Drawing.Point(0, 107);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.nullValueProvider1.SetNullValueDescription(this.txtFileName, "");
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(937, 50);
            this.txtFileName.TabIndex = 4;
            // 
            // tabCtlMain
            // 
            this.tabCtlMain.Controls.Add(this.tPageLanguage);
            this.tabCtlMain.Controls.Add(this.tPageUIView);
            this.tabCtlMain.Controls.Add(this.tPageUIRule);
            this.tabCtlMain.Controls.Add(this.tPageEditForm);
            this.tabCtlMain.Controls.Add(this.tPageServerEntity);
            this.tabCtlMain.Controls.Add(this.tPageServerSql);
            this.tabCtlMain.Controls.Add(this.tPageServerRule);
            this.tabCtlMain.Controls.Add(this.tPageServerInterface);
            this.tabCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlMain.Location = new System.Drawing.Point(0, 157);
            this.tabCtlMain.Name = "tabCtlMain";
            this.tabCtlMain.SelectedIndex = 0;
            this.tabCtlMain.Size = new System.Drawing.Size(937, 450);
            this.tabCtlMain.TabIndex = 5;
            this.tabCtlMain.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tPageLanguage
            // 
            this.tPageLanguage.Controls.Add(this.tabPageLanguage);
            this.tPageLanguage.Location = new System.Drawing.Point(4, 22);
            this.tPageLanguage.Name = "tPageLanguage";
            this.tPageLanguage.Size = new System.Drawing.Size(929, 424);
            this.tPageLanguage.TabIndex = 5;
            this.tPageLanguage.Text = "中英文对照表";
            this.tPageLanguage.UseVisualStyleBackColor = true;
            // 
            // tabPageLanguage
            // 
            this.tabPageLanguage.Controls.Add(this.tPageLanguageText);
            this.tabPageLanguage.Controls.Add(this.tPageLanguageControl);
            this.tabPageLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageLanguage.Location = new System.Drawing.Point(0, 0);
            this.tabPageLanguage.Name = "tabPageLanguage";
            this.tabPageLanguage.SelectedIndex = 0;
            this.tabPageLanguage.Size = new System.Drawing.Size(929, 424);
            this.tabPageLanguage.TabIndex = 0;
            this.tabPageLanguage.SelectedIndexChanged += new System.EventHandler(this.OnTabPageLanguageSelcetedIndexChanged);
            // 
            // tPageLanguageText
            // 
            this.tPageLanguageText.Controls.Add(this.rxtLanguage);
            this.tPageLanguageText.Location = new System.Drawing.Point(4, 22);
            this.tPageLanguageText.Name = "tPageLanguageText";
            this.tPageLanguageText.Padding = new System.Windows.Forms.Padding(3);
            this.tPageLanguageText.Size = new System.Drawing.Size(921, 398);
            this.tPageLanguageText.TabIndex = 0;
            this.tPageLanguageText.Text = "对照表";
            this.tPageLanguageText.UseVisualStyleBackColor = true;
            // 
            // rxtLanguage
            // 
            this.rxtLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtLanguage.ForeColor = System.Drawing.Color.Gray;
            this.rxtLanguage.Location = new System.Drawing.Point(3, 3);
            this.rxtLanguage.Name = "rxtLanguage";
            this.nullValueProvider1.SetNullValueDescription(this.rxtLanguage, "<<可以直接从PowerDesigner 中Copy 过来。第一列为中文 第二列为英文，其它没关系>>");
            this.rxtLanguage.Size = new System.Drawing.Size(915, 392);
            this.rxtLanguage.TabIndex = 1;
            this.rxtLanguage.Text = "<<可以直接从PowerDesigner 中Copy 过来。第一列为中文 第二列为英文，其它没关系>>";
            // 
            // tPageLanguageControl
            // 
            this.tPageLanguageControl.Controls.Add(this.panel2);
            this.tPageLanguageControl.Controls.Add(this.butRefurbish);
            this.tPageLanguageControl.Location = new System.Drawing.Point(4, 22);
            this.tPageLanguageControl.Name = "tPageLanguageControl";
            this.tPageLanguageControl.Padding = new System.Windows.Forms.Padding(3);
            this.tPageLanguageControl.Size = new System.Drawing.Size(921, 398);
            this.tPageLanguageControl.TabIndex = 1;
            this.tPageLanguageControl.Text = "数据配制";
            this.tPageLanguageControl.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.gridControl);
            this.panel2.Location = new System.Drawing.Point(6, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(909, 351);
            this.panel2.TabIndex = 5;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(909, 351);
            this.gridControl.TabIndex = 4;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.gridView.OptionsSelection.InvertSelection = true;
            this.gridView.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.OnGridControlCustomRowCellEdit);
            // 
            // butRefurbish
            // 
            this.butRefurbish.Location = new System.Drawing.Point(6, 7);
            this.butRefurbish.Name = "butRefurbish";
            this.butRefurbish.Size = new System.Drawing.Size(75, 25);
            this.butRefurbish.TabIndex = 4;
            this.butRefurbish.Text = "刷新";
            this.butRefurbish.UseVisualStyleBackColor = true;
            this.butRefurbish.Click += new System.EventHandler(this.OnButRefurbishClick);
            // 
            // tPageUIView
            // 
            this.tPageUIView.Controls.Add(this.rxtUIXmlView);
            this.tPageUIView.Location = new System.Drawing.Point(4, 22);
            this.tPageUIView.Name = "tPageUIView";
            this.tPageUIView.Padding = new System.Windows.Forms.Padding(3);
            this.tPageUIView.Size = new System.Drawing.Size(929, 424);
            this.tPageUIView.TabIndex = 0;
            this.tPageUIView.Text = "客户端-XML配置文件";
            this.tPageUIView.UseVisualStyleBackColor = true;
            // 
            // rxtUIXmlView
            // 
            this.rxtUIXmlView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtUIXmlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtUIXmlView.Location = new System.Drawing.Point(3, 3);
            this.rxtUIXmlView.Name = "rxtUIXmlView";
            this.nullValueProvider1.SetNullValueDescription(this.rxtUIXmlView, "");
            this.rxtUIXmlView.ReadOnly = true;
            this.rxtUIXmlView.Size = new System.Drawing.Size(923, 416);
            this.rxtUIXmlView.TabIndex = 0;
            this.rxtUIXmlView.Text = "";
            // 
            // tPageUIRule
            // 
            this.tPageUIRule.Controls.Add(this.rxtUIRule);
            this.tPageUIRule.Location = new System.Drawing.Point(4, 22);
            this.tPageUIRule.Name = "tPageUIRule";
            this.tPageUIRule.Size = new System.Drawing.Size(929, 424);
            this.tPageUIRule.TabIndex = 7;
            this.tPageUIRule.Text = "客户端业务类";
            this.tPageUIRule.UseVisualStyleBackColor = true;
            // 
            // rxtUIRule
            // 
            this.rxtUIRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtUIRule.Location = new System.Drawing.Point(0, 0);
            this.rxtUIRule.Name = "rxtUIRule";
            this.nullValueProvider1.SetNullValueDescription(this.rxtUIRule, "");
            this.rxtUIRule.Size = new System.Drawing.Size(929, 422);
            this.rxtUIRule.TabIndex = 0;
            this.rxtUIRule.Text = "";
            // 
            // tPageEditForm
            // 
            this.tPageEditForm.Controls.Add(this.rxtUIEditForm);
            this.tPageEditForm.Location = new System.Drawing.Point(4, 22);
            this.tPageEditForm.Name = "tPageEditForm";
            this.tPageEditForm.Size = new System.Drawing.Size(929, 424);
            this.tPageEditForm.TabIndex = 6;
            this.tPageEditForm.Text = "客户端-窗口编辑";
            this.tPageEditForm.UseVisualStyleBackColor = true;
            // 
            // rxtUIEditForm
            // 
            this.rxtUIEditForm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtUIEditForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtUIEditForm.Location = new System.Drawing.Point(0, 0);
            this.rxtUIEditForm.Name = "rxtUIEditForm";
            this.nullValueProvider1.SetNullValueDescription(this.rxtUIEditForm, "");
            this.rxtUIEditForm.ReadOnly = true;
            this.rxtUIEditForm.Size = new System.Drawing.Size(929, 423);
            this.rxtUIEditForm.TabIndex = 0;
            this.rxtUIEditForm.Text = "";
            // 
            // tPageServerEntity
            // 
            this.tPageServerEntity.Controls.Add(this.rxtServerEntity);
            this.tPageServerEntity.Location = new System.Drawing.Point(4, 22);
            this.tPageServerEntity.Name = "tPageServerEntity";
            this.tPageServerEntity.Padding = new System.Windows.Forms.Padding(3);
            this.tPageServerEntity.Size = new System.Drawing.Size(929, 424);
            this.tPageServerEntity.TabIndex = 1;
            this.tPageServerEntity.Text = "服务端-数据实体类";
            this.tPageServerEntity.UseVisualStyleBackColor = true;
            // 
            // rxtServerEntity
            // 
            this.rxtServerEntity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtServerEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtServerEntity.Location = new System.Drawing.Point(3, 3);
            this.rxtServerEntity.Name = "rxtServerEntity";
            this.nullValueProvider1.SetNullValueDescription(this.rxtServerEntity, "");
            this.rxtServerEntity.ReadOnly = true;
            this.rxtServerEntity.Size = new System.Drawing.Size(923, 416);
            this.rxtServerEntity.TabIndex = 0;
            this.rxtServerEntity.Text = "";
            // 
            // tPageServerSql
            // 
            this.tPageServerSql.Controls.Add(this.rxtServerSqlXml);
            this.tPageServerSql.Location = new System.Drawing.Point(4, 22);
            this.tPageServerSql.Name = "tPageServerSql";
            this.tPageServerSql.Size = new System.Drawing.Size(929, 424);
            this.tPageServerSql.TabIndex = 2;
            this.tPageServerSql.Text = "服务端-SQL 配置文件";
            this.tPageServerSql.UseVisualStyleBackColor = true;
            // 
            // rxtServerSqlXml
            // 
            this.rxtServerSqlXml.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtServerSqlXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtServerSqlXml.Location = new System.Drawing.Point(0, 0);
            this.rxtServerSqlXml.Name = "rxtServerSqlXml";
            this.nullValueProvider1.SetNullValueDescription(this.rxtServerSqlXml, "");
            this.rxtServerSqlXml.ReadOnly = true;
            this.rxtServerSqlXml.Size = new System.Drawing.Size(929, 423);
            this.rxtServerSqlXml.TabIndex = 0;
            this.rxtServerSqlXml.Text = "";
            // 
            // tPageServerRule
            // 
            this.tPageServerRule.Controls.Add(this.rxtServerRule);
            this.tPageServerRule.Location = new System.Drawing.Point(4, 22);
            this.tPageServerRule.Name = "tPageServerRule";
            this.tPageServerRule.Size = new System.Drawing.Size(929, 424);
            this.tPageServerRule.TabIndex = 3;
            this.tPageServerRule.Text = "服务端-业务类";
            this.tPageServerRule.UseVisualStyleBackColor = true;
            // 
            // rxtServerRule
            // 
            this.rxtServerRule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtServerRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtServerRule.Location = new System.Drawing.Point(0, 0);
            this.rxtServerRule.Name = "rxtServerRule";
            this.nullValueProvider1.SetNullValueDescription(this.rxtServerRule, "");
            this.rxtServerRule.ReadOnly = true;
            this.rxtServerRule.Size = new System.Drawing.Size(929, 423);
            this.rxtServerRule.TabIndex = 0;
            this.rxtServerRule.Text = "";
            // 
            // tPageServerInterface
            // 
            this.tPageServerInterface.Controls.Add(this.rxtServerInterface);
            this.tPageServerInterface.Location = new System.Drawing.Point(4, 22);
            this.tPageServerInterface.Name = "tPageServerInterface";
            this.tPageServerInterface.Size = new System.Drawing.Size(929, 424);
            this.tPageServerInterface.TabIndex = 4;
            this.tPageServerInterface.Text = "服务端-服务接口";
            this.tPageServerInterface.UseVisualStyleBackColor = true;
            // 
            // rxtServerInterface
            // 
            this.rxtServerInterface.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtServerInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rxtServerInterface.Location = new System.Drawing.Point(0, 0);
            this.rxtServerInterface.Name = "rxtServerInterface";
            this.nullValueProvider1.SetNullValueDescription(this.rxtServerInterface, "");
            this.rxtServerInterface.ReadOnly = true;
            this.rxtServerInterface.Size = new System.Drawing.Size(929, 423);
            this.rxtServerInterface.TabIndex = 0;
            this.rxtServerInterface.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(684, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "服务器端:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label7.Location = new System.Drawing.Point(684, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "客 户 端:";
            // 
            // linkLabelClientCodeFolder
            // 
            this.linkLabelClientCodeFolder.AutoSize = true;
            this.linkLabelClientCodeFolder.Location = new System.Drawing.Point(743, 44);
            this.linkLabelClientCodeFolder.Name = "linkLabelClientCodeFolder";
            this.linkLabelClientCodeFolder.Size = new System.Drawing.Size(55, 13);
            this.linkLabelClientCodeFolder.TabIndex = 13;
            this.linkLabelClientCodeFolder.TabStop = true;
            this.linkLabelClientCodeFolder.Text = "linkLabel1";
            this.linkLabelClientCodeFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClientCodeFolder_LinkClicked);
            // 
            // frmCodeGenerate
            // 
            this.AcceptButton = this.butCodeGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 607);
            this.Controls.Add(this.tabCtlMain);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCodeGenerate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本代码生成";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabCtlMain.ResumeLayout(false);
            this.tPageLanguage.ResumeLayout(false);
            this.tabPageLanguage.ResumeLayout(false);
            this.tPageLanguageText.ResumeLayout(false);
            this.tPageLanguageControl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.tPageUIView.ResumeLayout(false);
            this.tPageUIRule.ResumeLayout(false);
            this.tPageEditForm.ResumeLayout(false);
            this.tPageServerEntity.ResumeLayout(false);
            this.tPageServerSql.ResumeLayout(false);
            this.tPageServerRule.ResumeLayout(false);
            this.tPageServerInterface.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSqlScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butCodeGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ComboBox cobGenerateType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobTableName;
        private NullValueProvider nullValueProvider1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TabControl tabCtlMain;
        private System.Windows.Forms.TabPage tPageLanguage;
        private System.Windows.Forms.TabPage tPageUIView;
        private System.Windows.Forms.RichTextBox rxtUIXmlView;
        private System.Windows.Forms.TabPage tPageUIRule;
        private System.Windows.Forms.RichTextBox rxtUIRule;
        private System.Windows.Forms.TabPage tPageEditForm;
        private System.Windows.Forms.RichTextBox rxtUIEditForm;
        private System.Windows.Forms.TabPage tPageServerEntity;
        private System.Windows.Forms.RichTextBox rxtServerEntity;
        private System.Windows.Forms.TabPage tPageServerSql;
        private System.Windows.Forms.RichTextBox rxtServerSqlXml;
        private System.Windows.Forms.TabPage tPageServerRule;
        private System.Windows.Forms.RichTextBox rxtServerRule;
        private System.Windows.Forms.TabPage tPageServerInterface;
        private System.Windows.Forms.RichTextBox rxtServerInterface;
        private System.Windows.Forms.TabControl tabPageLanguage;
        private System.Windows.Forms.TabPage tPageLanguageText;
        private System.Windows.Forms.RichTextBox rxtLanguage;
        private System.Windows.Forms.TabPage tPageLanguageControl;
        private System.Windows.Forms.Button butRefurbish;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabelServerCodeFolder;
        private System.Windows.Forms.LinkLabel linkLabelClientCodeFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}