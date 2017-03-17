//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	版本更新处理相关。
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

using MB.Util.Model;
namespace MB.WinClientDefault.VersionAutoUpdate {
    /// <summary>
    /// 版本下载更新等待处理窗口。
    /// </summary>
    public partial class FileDownloadWaitDialog : Form {
        private VersionDownloadHelper _UpdateHelper;
        private IVersionDownload _AutoUpdate;
        private List<VersionUpdateFileInfo> _ForDownloadFiles;
        private MB.WinBase.IFace.IWaitDialogFormHoster _HosterParent;
        #region 自定义事件...
        private EventHandler _ClickCancel;
        private bool _HasFriendMsg = false;
        /// <summary>
        /// 线程等待取消后产生的事件。
        /// </summary>
        public event EventHandler ClickCanceled {
            add {
                _ClickCancel += value;
            }
            remove {
                _ClickCancel -= value;
            }
        }
        private void OnClickCanceled(EventArgs arg) {
            if (_ClickCancel != null) {
                _ClickCancel(this, arg);
            }
        }
        #endregion 自定义事件...
        #region 构造和函数...
        /// <summary>
        ///  版本下载更新等待处理窗口。
        /// </summary>
        /// <param name="autoUpdate"></param>
        /// <param name="forDownloadFiles"></param>
        public FileDownloadWaitDialog(VersionDownloadHelper hosterParent, IVersionDownload autoUpdate, List<VersionUpdateFileInfo> forDownloadFiles) {
            InitializeComponent();

            lsvDownFiles.Dock = DockStyle.Fill;

            _UpdateHelper = hosterParent;
            _HosterParent = hosterParent;
            _AutoUpdate = autoUpdate;
            _ForDownloadFiles = forDownloadFiles;

            this.Load += new EventHandler(VersionUpdateDialog_Load);

     

        }
        #endregion 构造和函数...


        void VersionUpdateDialog_Load(object sender, EventArgs e) {
            lsvDownFiles.Items.Clear();
            lsvDownFiles.Columns.Clear();

            createFileListHeader();

            foreach (var dataInfo in _ForDownloadFiles) {
                System.Windows.Forms.ListViewItem  newItem = createItemByInfo(dataInfo);
                lsvDownFiles.Items.Add(newItem);
            }
            _UpdateHelper.BeginRunWork();
            timer1.Enabled = true;
        }

        #region 内部函数处理...
        //根据Info 创建listview 的item
        private System.Windows.Forms.ListViewItem createItemByInfo(VersionUpdateFileInfo dataInfo) {
            ListViewItem item = new ListViewItem(dataInfo.FileName);
            item.ForeColor = dataInfo.Completed ? Color.Black : Color.Blue;

            item.SubItems.Add(dataInfo.ChildDirectoryName);
            if(dataInfo.FileLength < 1000)
                item.SubItems.Add("1 KB");
            else
                item.SubItems.Add((dataInfo.FileLength / 1000).ToString() + " KB");

            item.SubItems.Add(VersionDownloadHelper.DividendToInt32(dataInfo.HasDownLoad * 100, dataInfo.FileLength).ToString() + "%");
            item.SubItems.Add(dataInfo.Remark);
            item.Tag = dataInfo;

            return item;
        }
     
        //创建listview 的表头
        private void createFileListHeader() {
            lsvDownFiles.Columns.Add("文件名称", 160, HorizontalAlignment.Left);
            lsvDownFiles.Columns.Add("子目录", 120, HorizontalAlignment.Left);
            lsvDownFiles.Columns.Add("文件大小", 120, HorizontalAlignment.Left);
            lsvDownFiles.Columns.Add("已下载", 120, HorizontalAlignment.Left);
            lsvDownFiles.Columns.Add("备注", 200, HorizontalAlignment.Left);
        }
        //根据 文件Info 获取对应的ListViewItem
        private ListViewItem getListViewByData(VersionUpdateFileInfo dataInfo){
            foreach (ListViewItem item in lsvDownFiles.Items) {
                if (object.Equals(item.Tag, dataInfo))
                    return item;
            }
            return null;
        }
        #endregion 内部函数处理...

        private void timer1_Tick(object sender, EventArgs e) {
            if (_HosterParent.ProcessState.CurrentProcessContent != null) {
                VersionUpdateFileInfo dataInfo = _HosterParent.ProcessState.CurrentProcessContent as VersionUpdateFileInfo;
                ListViewItem lstItem = getListViewByData(dataInfo);
                lstItem.EnsureVisible(); 
                if (dataInfo.Completed) {
                    lstItem.ForeColor = Color.Black;
                }
                
               // lstItem.SubItems[3].Text = System.Convert.ToInt32(dataInfo.HasDownLoad * 100 / dataInfo.FileLength).ToString() + "%";
                lstItem.SubItems[3].Text = VersionDownloadHelper.DividendToInt32(dataInfo.HasDownLoad * 100, dataInfo.FileLength).ToString() + "%";
                //if (lstItem.Index > 0) {
                //    lsvDownFiles.Items[lstItem.Index - 1].ForeColor = Color.Black;
                //    lsvDownFiles.Items[lstItem.Index - 1].SubItems[3].Text = "100%";
                //}
            }


            if (_HosterParent.ProcessState.Processed) {
                this.Close();
            }
        }

        private void butCancel_Click(object sender, EventArgs e) {
            DialogResult re = MB.WinBase.MessageBoxEx.Question("版本更新尚未完成，是否取消？");
            if (re != DialogResult.Yes)
                return;

            OnClickCanceled(e);
        }
    }
}
