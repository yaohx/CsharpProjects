//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-01
// Description	:	代码生成
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.IO;

using MB.Tools.CodeGenerate;
namespace MB.Tools
{
    public partial class frmCodeGenerate : Form
    {
        private static readonly string DEFAULT_SQL = "SELECT * FROM {0} WHERE 1=0";
        private static readonly string DEFAULT_COLUMN_NAME = "Name";
        private static readonly string DEFAULT_COLUMN_NAME_CAPTION = "名称";
        private static readonly string DEFAULT_COLUMN_DESCRIPTION = "Description";
        private static readonly string DEFAULT_COLUMN_DESCRIPTION_CAPTION = "描述";
        private static readonly string DEFAULT_COLUMN_TYPE = "Type";
        private static readonly string DEFAULT_COLUMN_TYPE_CAPTION = "数据类型";
        private static readonly string DEFAULT_GRIDVIEW_NAME = "gridView";
        private static readonly string DATABASE_TYPE = "DatabaseType";
        private static readonly string CodeFolder = "CodeFolder";
        private static readonly string DEFAULT_CodeFolder = "Code";
        private static readonly string DEFAULT_CodeFolderServer = "Server";
        private static readonly string DEFAULT_CodeFolderClient = "UI";
        private DataSet _DataSet;
        private List<TabPage> _HasLoadPage;

        private string GenerateCodeFolder
        {
            get
            {
                var codeFolder = System.Configuration.ConfigurationManager.AppSettings[CodeFolder];
                codeFolder = string.IsNullOrWhiteSpace(codeFolder) ? DEFAULT_CodeFolder : codeFolder;
                var fullCodeFolder = Path.Combine(System.Windows.Forms.Application.StartupPath, codeFolder);
                if (!Directory.Exists(fullCodeFolder))
                {
                    Directory.CreateDirectory(fullCodeFolder);
                }
                return fullCodeFolder;
            }
        }

        private string GenerateCodeFolderServer
        {
            get
            {
                var fullCodeFolder = Path.Combine(GenerateCodeFolder, DEFAULT_CodeFolderServer);
                if (!Directory.Exists(fullCodeFolder))
                {
                    Directory.CreateDirectory(fullCodeFolder);
                }
                return fullCodeFolder;
            }
        }

        private string GenerateCodeFolderClient
        {
            get
            {
                var fullCodeFolder = Path.Combine(GenerateCodeFolder, DEFAULT_CodeFolderClient);
                if (!Directory.Exists(fullCodeFolder))
                {
                    Directory.CreateDirectory(fullCodeFolder);
                }
                return fullCodeFolder;
            }
        }

        public frmCodeGenerate()
        {
            InitializeComponent();

            cobGenerateType.Items.Add("单据主体对象");
            cobGenerateType.Items.Add("单据明细对象");
            cobGenerateType.Items.Add("查询分析");

            cobGenerateType.SelectedIndex = 0;
            _HasLoadPage = new List<TabPage>();

            getUserTable();
            linkLabelServerCodeFolder.Text = GenerateCodeFolderServer;
            linkLabelClientCodeFolder.Text = GenerateCodeFolderClient;

        }

        #region Events

        private void butCodeGenerate_Click(object sender, EventArgs e)
        {

            if (nullValueProvider1.IsNullValue(cobTableName) || nullValueProvider1.IsNullValue(txtEntityName))
            {
                MessageBox.Show("请输入表名称 和 数据实体名称！");
                return;
            }
            //this.Cursor = Cursors.WaitCursor;

            try
            {
                if (null == _DataSet)
                {
                    _DataSet = getData();
                }
                _HasLoadPage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取表结构有误，请检查！" + ex.Message);
            }
            for (int i = 0; i < tabCtlMain.TabPages.Count; i++)
            {
                if (tabCtlMain.TabPages[i].Equals(tPageLanguage))
                    SelectedTabGenerateCode(tPageUIView);
                else
                    SelectedTabGenerateCode(tabCtlMain.TabPages[i]);
            }

            //this.Cursor = Cursors.Default;
        }

        private void txtTableName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtTableName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cobTableName.Text != nullValueProvider1.MyNullValueTable[cobTableName])
                {

                    txtEntityName.Text = tableNameToEntityName(cobTableName.Text);
                    _DataSet = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("txtTableName_Leave:" + ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTabGenerateCode(tabCtlMain.SelectedTab);
        }

        private void cobTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtEntityName.Text = tableNameToEntityName(cobTableName.Text);

                if (1 == this.tabPageLanguage.SelectedIndex)
                {
                    bindGridControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("cobTableName_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void OnTabPageLanguageSelcetedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (1 == this.tabPageLanguage.SelectedIndex)
                {
                    bindGridControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnTabPageLanguageSelcetedIndexChanged:" + ex.Message);
            }
        }

        private void OnGridControlCustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            try
            {
                updateDataSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnGridControlCustomRowCellEdit:" + ex.Message);
            }
        }

        private void OnButRefurbishClick(object sender, EventArgs e)
        {
            try
            {
                bindGridControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnButRefurbishClick:" + ex.Message);
            }
        }

        private void OnTxtSqlScriptLeave(object sender, EventArgs e)
        {
            try
            {
                if (nullValueProvider1.IsNullValue(cobTableName) || nullValueProvider1.IsNullValue(txtEntityName))
                {
                    MessageBox.Show("请输入表名称 和 数据实体名称！");
                    return;
                }

                _DataSet = getData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnTxtSqlScriptLeave:" + ex.Message);
            }
        }

        #endregion

        #region private methods

        private void getUserTable()
        {
            string dbType = System.Configuration.ConfigurationManager.AppSettings[DATABASE_TYPE].ToString();
            if (string.Compare(dbType, "SqlServer", true) == 0)
            {
                getSqlServerUserTable();
            }
            else
            {
                getOracleUserTable();
            }

        }

        private void getSqlServerUserTable() {
            try {
                string sql = "SELECT name FROM sys.objects WHERE  type in (N'U') ORDER BY name";
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(sql);
                IDataReader reader = db.ExecuteReader(dbCmd);

                while (reader.Read()) {
                    cobTableName.Items.Add(reader[0].ToString());
                }
                reader.Close();

                if (0 != cobTableName.Items.Count) {
                    cobTableName.SelectedIndex = 0;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("配置有误！" + ex.Message);
            }
        }

        private void getOracleUserTable()
        {
            try
            {
                string tableSpace = System.Configuration.ConfigurationManager.AppSettings["TABLESPACE_NAME"].ToString();
                string owner = System.Configuration.ConfigurationManager.AppSettings["OWNER"].ToString();
                string sql = "SELECT TABLE_NAME FROM All_All_Tables WHERE Tablespace_name='{0}' and OWNER='{1}' ORDER BY TABLE_NAME";
                sql = string.Format(sql, tableSpace, owner);
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(sql);
                IDataReader reader = db.ExecuteReader(dbCmd);

                while (reader.Read())
                {
                    cobTableName.Items.Add(reader[0].ToString());
                }
                reader.Close();

                if (0 != cobTableName.Items.Count)
                {
                    cobTableName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置有误！" + ex.Message);
            }
        }


        private DataSet getData()
        {
            string sql = string.Empty;
            if (txtSqlScript.Text != nullValueProvider1.MyNullValueTable[txtSqlScript])
            {
                sql = txtSqlScript.Text;
            }

            if (string.IsNullOrEmpty(sql))
            {
                if (!string.IsNullOrEmpty(cobGenerateType.Text))
                {
                    sql = string.Format(DEFAULT_SQL, cobTableName.Text);
                }
            }

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            
            DataSet dsData = db.ExecuteDataSet(dbCmd);
            //dsData.Tables[0].Columns[0].DataType 
            return dsData;
        }

        private string tableNameToEntityName(string tableName)
        {
            char[] cs = tableName.ToLower().ToCharArray();
            bool toUpper = true;
            for (int i = 0; i < cs.Length; i++)
            {
                if (toUpper)
                    cs[i] = Char.ToUpper(cs[i]);
                if (cs[i] == '_')
                    toUpper = true;
                else
                    toUpper = false;
            }
            string s = new string(cs);
            s = s.Replace("_", "");
            return s;
        }

        private void setRemark()
        {
            if (tabCtlMain.SelectedTab.Equals(tPageUIView))
            {

                txtFileName.Text = "请把它Copy 到 UI 层组件的ConfigFile 目录下(设置属性为“始终复制”);文件名称为：" + txtEntityName.Text + ".xml";
            }
            else if (tabCtlMain.SelectedTab.Equals(tPageServerEntity))
            {

                txtFileName.Text = "请把它Copy 到 服务端组件的Model 目录下;文件名称为：" + txtEntityName.Text + "Info.cs";
            }
            else if (tabCtlMain.SelectedTab.Equals(tPageServerSql))
            {

                txtFileName.Text = "请把它Copy 到 服务端组件的的ConfigFile 目录下(设置属性为“始终复制”);文件名称为：" + txtEntityName.Text + ".xml";
            }
            else if (tabCtlMain.SelectedTab.Equals(tPageServerRule))
            {

                txtFileName.Text = "请把它Copy 到 服务端组件的根 目录下;文件名称为：" + txtEntityName.Text + ".cs";
            }
            else if (tabCtlMain.SelectedTab.Equals(tPageServerInterface))
            {

                txtFileName.Text = "请把它Copy 到 服务端组件的IFace 目录下;文件名称为：I" + txtEntityName.Text + ".cs";
            }
            else if (tabCtlMain.SelectedTab.Equals(tPageEditForm))
            {

                txtFileName.Text = "添加WindowsForm 请把它Copy 到 UI 层 组件的根 目录下;文件名称为：FrmEdit" + txtEntityName.Text + ".cs";
            }
            else if (tabCtlMain.SelectedTab.Equals(tPageUIRule))
            {

                txtFileName.Text = "添加WindowsForm 请把它Copy 到 UI 层 组件的UIRule目录下;文件名称为：" + txtEntityName.Text + ".cs";
            }
            else
            {
                txtFileName.Text = " 中英 文对照表 ";
                return;
            }
        }

        private void updateDataSet()
        {
            DataColumn colName = new DataColumn(DEFAULT_COLUMN_NAME);
            colName.Caption = DEFAULT_COLUMN_NAME_CAPTION;

            DataColumn colDescription = new DataColumn(DEFAULT_COLUMN_DESCRIPTION);
            colDescription.Caption = DEFAULT_COLUMN_DESCRIPTION_CAPTION;

            DataColumn colType = new DataColumn(DEFAULT_COLUMN_TYPE);
            colType.Caption = DEFAULT_COLUMN_TYPE_CAPTION;

            DataTable newDataTable = new DataTable();
            newDataTable.Columns.AddRange(new DataColumn[] { colName, colDescription, colType });


            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                DataRow row = gridView.GetDataRow(i);

                _DataSet.Tables[0].Columns[i].DataType = System.Type.GetType(row[2].ToString());

            }
        }

        private void bindGridControl()
        {
            _DataSet = getData();
            if (null != _DataSet)
            {
                LocalLanguageObject language = new LocalLanguageObject(rxtLanguage);

                DataColumnCollection dataColumnCollections = _DataSet.Tables[0].Columns;

                DataColumn colName = new DataColumn(DEFAULT_COLUMN_NAME);
                colName.Caption = DEFAULT_COLUMN_NAME_CAPTION;

                DataColumn colDescription = new DataColumn(DEFAULT_COLUMN_DESCRIPTION);
                colDescription.Caption = DEFAULT_COLUMN_DESCRIPTION_CAPTION;

                DataColumn colType = new DataColumn(DEFAULT_COLUMN_TYPE);
                colType.Caption = DEFAULT_COLUMN_TYPE_CAPTION;

                DataTable dataTable = new DataTable();
                DataSet dataSet = new DataSet();
                dataTable.Columns.AddRange(new DataColumn[] { colName, colDescription, colType });


                foreach (DataColumn dataColumn in dataColumnCollections)
                {
                    dataTable.Rows.Add(dataColumn.ColumnName, language.GetDescription(dataColumn.ColumnName), dataColumn.DataType);
                }

                dataSet.Tables.Add(dataTable);

                gridControl.DataSource = dataSet.Tables[0].DefaultView;

                string[] items = getDataTypes();

                DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxDataType =
                    new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                repositoryItemComboBoxDataType.Items.AddRange(items);

                gridView.Columns[2].ColumnEdit = repositoryItemComboBoxDataType;

                this.gridView.Name = DEFAULT_GRIDVIEW_NAME;
            }
        }

        private string[] getDataTypes()
        {
            const string SystemString = "System.String";
            const string SystemBoolean = "System.Boolean";
            const string SystemInt16 = "System.Int16";
            const string SystemInt32 = "System.Int32";
            const string SystemInt64 = "System.Int64";
            const string SystemDecimal = "System.Decimal";
            const string SystemByte = "System.Byte[]";
            const string SystemDateTime = "System.DateTime";

            return new string[] {
                SystemString, 
                SystemBoolean, 
                SystemInt16, 
                SystemInt32, 
                SystemInt64, 
                SystemDecimal, 
                SystemByte, 
                SystemDateTime
            };
        }

        private void SelectedTabGenerateCode(TabPage currentTab)
        {
            try
            {
                setRemark();
                if (_DataSet == null || _HasLoadPage.Contains(currentTab)) return;
                RichTextBox currentRichTextBox = null;
                if (currentTab.Equals(tPageUIView))
                {
                    rxtUIXmlView.Clear();
                    LocalLanguageObject language = new LocalLanguageObject(rxtLanguage);
                    UIXmlGenerateHelper uiXml = new UIXmlGenerateHelper(_DataSet.Tables[0].Columns, language);
                    uiXml.GenerateToRichTextBox(rxtUIXmlView);
                    currentRichTextBox = rxtUIXmlView;

                }
                else if (currentTab.Equals(tPageServerEntity))
                {
                    rxtServerEntity.Clear();
                    GenerateEntityModelHelper code = new GenerateEntityModelHelper(cobTableName.Text, txtEntityName.Text, "ID", _DataSet.Tables[0].Columns);
                    code.GenerateToRichTextBox(rxtServerEntity);
                    currentRichTextBox = rxtServerEntity;

                }
                else if (currentTab.Equals(tPageServerSql))
                {
                    rxtServerSqlXml.Clear();
                    ServerXmlSqlGenerate serverSql = new ServerXmlSqlGenerate(cobTableName.Text, _DataSet.Tables[0].Columns);
                    serverSql.GenerateToRichTextBox(rxtServerSqlXml);
                    currentRichTextBox = rxtServerSqlXml;

                }
                else if (currentTab.Equals(tPageServerRule))
                {
                    rxtServerRule.Clear();
                    ServerRuleGenerate serverRule = new ServerRuleGenerate(cobTableName.Text, txtEntityName.Text, "ID");
                    serverRule.GenerateToRichTextBox(rxtServerRule, false);
                    currentRichTextBox = rxtServerRule;

                }
                else if (currentTab.Equals(tPageServerInterface))
                {
                    rxtServerInterface.Clear();
                    ServerRuleGenerate serverRule = new ServerRuleGenerate(cobTableName.Text, txtEntityName.Text, "ID");
                    serverRule.GenerateToRichTextBox(rxtServerInterface, true);
                    currentRichTextBox = rxtServerInterface;

                }
                else if (currentTab.Equals(tPageEditForm))
                {
                    rxtUIEditForm.Clear();
                    UIEditFormGenerateHelper editForm = new UIEditFormGenerateHelper(cobTableName.Text, txtEntityName.Text);
                    editForm.GenerateToRichTextBox(rxtUIEditForm, false);
                    currentRichTextBox = rxtUIEditForm;
                }
                else if (currentTab.Equals(tPageUIRule))
                {
                    rxtUIRule.Clear();
                    UIEditFormGenerateHelper editForm = new UIEditFormGenerateHelper(cobTableName.Text, txtEntityName.Text);
                    editForm.GenerateToRichTextBox(rxtUIRule, true);
                    currentRichTextBox = rxtUIRule;
                }
                else
                {
                    return;
                }
                _HasLoadPage.Add(currentTab);
                SaveCodeFile(currentRichTextBox, currentTab);
            }
            catch (Exception ex)
            {
                MessageBox.Show("tabControl1_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void SaveCodeFile(RichTextBox rxtTab, TabPage currentTab)
        {
            var serverCodePath = GenerateCodeFolderServer;
            var clientCodePath = GenerateCodeFolderClient;

            string fullFileName = null;
            if (currentTab.Equals(tPageUIView))
            {
                fullFileName = string.Format(@"{0}\{1}\{2}{3}", clientCodePath, "ConfigFile", txtEntityName.Text, ".xml");
            }
            else if (currentTab.Equals(tPageServerEntity))
            {
                fullFileName = string.Format(@"{0}\{1}\{2}{3}", serverCodePath, "Model", txtEntityName.Text, "Info.cs");
            }
            else if (currentTab.Equals(tPageServerSql))
            {
                fullFileName = string.Format(@"{0}\{1}\{2}{3}", serverCodePath, "ConfigFile", txtEntityName.Text, ".xml");
            }
            else if (currentTab.Equals(tPageServerRule))
            {
                fullFileName = string.Format(@"{0}\{1}{2}", serverCodePath, txtEntityName.Text, ".cs");
            }
            else if (currentTab.Equals(tPageServerInterface))
            {
                fullFileName = string.Format(@"{0}\{1}\I{2}{3}", serverCodePath, "IFace", txtEntityName.Text, ".cs");
            }
            else if (currentTab.Equals(tPageEditForm))
            {
                fullFileName = string.Format(@"{0}\FrmEdit{1}{2}", clientCodePath, txtEntityName.Text, ".cs");
            }
            else if (currentTab.Equals(tPageUIRule))
            {
                fullFileName = string.Format(@"{0}\{1}\{2}{3}", clientCodePath, "UIRule", txtEntityName.Text, ".cs");
            }
            else
            {
                fullFileName = null;
                return;
            }
            if (!string.IsNullOrWhiteSpace(fullFileName))
            {
                if (!Directory.Exists(Path.GetDirectoryName(fullFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullFileName));
                }
                File.WriteAllText(fullFileName, rxtTab.Text, Encoding.UTF8);
            }
        }
        #endregion

        private void linkLabelGenerateCodeFolderServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(GenerateCodeFolderServer);
        }

        private void linkLabelClientCodeFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(GenerateCodeFolderClient);
        }
    }
}
