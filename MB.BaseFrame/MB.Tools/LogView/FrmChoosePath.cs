using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.Tools.LogView
{
    public partial class FrmChoosePath : Form
    {
        #region Private variables

        private static readonly string CONFIG_FILENAME = "\\path.ini";
        private static readonly string CONFIG_SECTION = "Section";
        private static readonly string CONFIG_KEY = "path";

        private static readonly int KEY_NUMBER = 10;
        private string _Current_LogFile_Path = string.Empty;
        private string _Full_CONFIG_File_Name = string.Empty;
        private Form _Form;

        #endregion

        #region Construct

        public FrmChoosePath(Form form)
        {
            InitializeComponent();

            _Full_CONFIG_File_Name = System.Environment.CurrentDirectory + CONFIG_FILENAME;
            bindDbcboPath();
            setDbcboPathSelectedIndex(-1);
            cleanError();
            _Form = form;
        }

        #endregion

        #region Events

        private void OnBtnChooseClick(object sender, EventArgs e)
        {
            try
            {
                DialogResult re = folderBrowserDialog.ShowDialog();
                if (re != DialogResult.OK && re != DialogResult.Yes)
                {
                    return;
                }

                _Current_LogFile_Path = folderBrowserDialog.SelectedPath;

                updateConfigFile(_Current_LogFile_Path);
                bindDbcboPath();

                int index = getSelectIndex(_Current_LogFile_Path);
                setDbcboPathSelectedIndex(index);
            }
            catch (Exception ex)
            {
                showError(ex.Message + " in OnBtnChooseClick");
            }
        }

        private void OnBtnOkClick(object sender, EventArgs e)
        {
            try
            {
                if (dbcboPath.Items.Count > 0 && null != dbcboPath.SelectedItem)
                {
                    _Current_LogFile_Path = dbcboPath.SelectedItem.ToString();
                    updateConfigFile(_Current_LogFile_Path);
                    loadLogView(_Current_LogFile_Path);
                }
            }
            catch (Exception ex)
            {
                showError(ex.Message + " in OnBtnOkClick");
            }
        }

        private void OnBtnExitClick(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                showError(ex.Message + " in OnBtnExitClick");
            }
        }

        private void OnDbcboPathSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _Current_LogFile_Path = dbcboPath.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                showError(ex.Message + " in OnDbcboPathSelectedIndexChanged");
            }
        }

        #endregion

        #region private methods

        private void bindDbcboPath()
        {
            dbcboPath.Items.Clear();

            for (int i = 1; i <= KEY_NUMBER; i++)
            {
                string path = MB.Util.IniFile.ReadString(
                    CONFIG_SECTION, CONFIG_KEY + i.ToString(), null, _Full_CONFIG_File_Name);

                if (!string.IsNullOrEmpty(path))
                {
                    dbcboPath.Items.Add(path);
                }
            }

            
        }

        private int getSelectIndex(string path)
        {
            int index = -1;

            for (int i = 0; i < dbcboPath.Items.Count; i++)
            {
                if (path == dbcboPath.Items[i].ToString())
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private void setDbcboPathSelectedIndex(int index)
        {
            if (-1 == index)
            {
                dbcboPath.SelectedIndex = dbcboPath.Items.Count - 1;
            }
            else
            {
                dbcboPath.SelectedIndex = index;
            }
        }

        private void loadLogView(string path)
        {
            this.Hide();

            MB.Tools.LogView.FrmLogView frm = new MB.Tools.LogView.FrmLogView();
            frm.MdiParent = _Form;
            frm.LoadLogFileTree(path);
            frm.Show();
        }

        private bool checkDuplicate(string path)
        {
            bool result = false;

            string key = string.Empty;
            string loadPath = string.Empty;
            for (int i = 1; i <= KEY_NUMBER; i++)
            {
                key = CONFIG_KEY + i.ToString();
                loadPath = MB.Util.IniFile.ReadString(CONFIG_SECTION, key, null, _Full_CONFIG_File_Name);

                if (!string.IsNullOrEmpty(loadPath))
                {
                    if (path == loadPath)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private int getDuplicateIndex(string path)
        {
            int index = -1;

            string key = string.Empty;
            string loadPath = string.Empty;
            for (int i = 1; i <= KEY_NUMBER; i++)
            {
                key = CONFIG_KEY + i.ToString();
                loadPath = MB.Util.IniFile.ReadString(CONFIG_SECTION, key, null, _Full_CONFIG_File_Name);

                if (!string.IsNullOrEmpty(loadPath))
                {
                    if (path == loadPath)
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }

        private void changeConfigPath(int leftIndex, int rightIndex)
        {
            string leftKey = string.Empty;
            string leftPath = string.Empty;

            leftKey = CONFIG_KEY + leftIndex.ToString();
            leftPath = MB.Util.IniFile.ReadString(CONFIG_SECTION, leftKey, null, _Full_CONFIG_File_Name);

            string rightKey = string.Empty;
            string rightPath = string.Empty;

            rightKey = CONFIG_KEY + rightIndex.ToString();
            rightPath = MB.Util.IniFile.ReadString(CONFIG_SECTION, rightKey, null, _Full_CONFIG_File_Name);

            MB.Util.IniFile.DelKey(CONFIG_SECTION, leftKey, _Full_CONFIG_File_Name);
            MB.Util.IniFile.WriteString(CONFIG_SECTION, leftKey, rightPath, _Full_CONFIG_File_Name);

            MB.Util.IniFile.DelKey(CONFIG_SECTION, rightKey, _Full_CONFIG_File_Name);
            MB.Util.IniFile.WriteString(CONFIG_SECTION, rightKey, leftPath, _Full_CONFIG_File_Name);
        }

        private void updateConfigFile(string path)
        {
            bool duplicate = checkDuplicate(path);
            string oldPath = string.Empty;
            string key = string.Empty;

            key = CONFIG_KEY + KEY_NUMBER.ToString();
            oldPath = MB.Util.IniFile.ReadString(CONFIG_SECTION, key, null, _Full_CONFIG_File_Name);

            if (duplicate)
            {
                int currentIndex = getDuplicateIndex(path);
                int changeIndex = -1;

                if (!string.IsNullOrEmpty(oldPath))
                {
                    changeIndex = KEY_NUMBER;
                }
                else
                {
                    for (int i = 1; i <= KEY_NUMBER; i++)
                    {
                        string currentKey = CONFIG_KEY + i.ToString();
                        string currentValue = MB.Util.IniFile.ReadString(CONFIG_SECTION, currentKey, null, _Full_CONFIG_File_Name);
                        if (string.IsNullOrEmpty(currentValue))
                        {
                            changeIndex = i-1;
                            break;
                        }
                    }
                }

                changeConfigPath(currentIndex, changeIndex);
            }
            else
            {
                if (!string.IsNullOrEmpty(oldPath))
                {
                    for (int i = 2; i <= KEY_NUMBER; i++)
                    {
                        string readKey = CONFIG_KEY + i.ToString();
                        string deleteKey = CONFIG_KEY + (i - 1).ToString();
                        string insteadValue = string.Empty;

                        MB.Util.IniFile.DelKey(CONFIG_SECTION, deleteKey, _Full_CONFIG_File_Name);
                        insteadValue = MB.Util.IniFile.ReadString(CONFIG_SECTION, readKey, null, _Full_CONFIG_File_Name);
                        MB.Util.IniFile.WriteString(CONFIG_SECTION, deleteKey, insteadValue, _Full_CONFIG_File_Name);
                    }

                    MB.Util.IniFile.DelKey(CONFIG_SECTION, key, _Full_CONFIG_File_Name);
                    MB.Util.IniFile.WriteString(CONFIG_SECTION, key, path, _Full_CONFIG_File_Name);
                }
                else
                {
                    for (int i = 1; i <= KEY_NUMBER; i++)
                    {
                        string currentKey = CONFIG_KEY + i.ToString();
                        string currentValue = MB.Util.IniFile.ReadString(CONFIG_SECTION, currentKey, null, _Full_CONFIG_File_Name);
                        if (string.IsNullOrEmpty(currentValue))
                        {
                            MB.Util.IniFile.WriteString(CONFIG_SECTION, currentKey, path, _Full_CONFIG_File_Name);
                            break;
                        }
                    }
                }
            }
        }

        private void showError(string message)
        {
            labError.Text = message;
        }

        private void cleanError()
        {
            labError.Text = string.Empty;
        }

        #endregion
    }
}
