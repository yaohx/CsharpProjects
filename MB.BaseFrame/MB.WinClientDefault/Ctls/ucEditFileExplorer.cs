//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-08
// Description	:	文件浏览窗口
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace MB.WinClientDefault.Ctls {
    /// <summary>
    /// 文件浏览窗口
    /// </summary>
    [ToolboxItem(true)] 
    public partial class ucEditFileExplorer : UserControl {
        private System.Windows.Forms.ImageList _SmallIconList;
        private System.Windows.Forms.ImageList _IconList;
        private BindingList<FileDataInfo> _FileList;
        private object _DataType;
        private int _NodeID;
        private int _PrevNodeID;
        private bool _IsSingleCreate;
        private FileExplorerType _FileExplorerType;
        private Size _ShrinkImageSize;
        private static readonly string[] IMAGE_FILE_EXTENSION = new string[] { ".BMP", ".GIF", ".JPG" };

        #region 构造函数...
        public ucEditFileExplorer()
            : this( null, -1, -1) {

        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        public ucEditFileExplorer(object dataType, int nodeID, int prevNodeID) {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            lstFileExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            _DataType = dataType;
            _NodeID = nodeID;
            _PrevNodeID = prevNodeID;

            _SmallIconList = new ImageList();
            _IconList = new ImageList();
            _ShrinkImageSize = new Size(32, 32);
            _IconList.ImageSize =  new Size(_ShrinkImageSize.Width * 2,_ShrinkImageSize.Height *2); 

            lstFileExplorer.SmallImageList = _SmallIconList;
            lstFileExplorer.LargeImageList = _IconList;

            mItemSmallicon.Click += new EventHandler(mItemSmallicon_Click);
            mItemLargeIcon.Click += new EventHandler(mItemLargeIcon_Click);
            mItemList.Click += new EventHandler(mItemList_Click);
            mItemDetail.Click += new EventHandler(mItemDetail_Click);
            this.Load += new EventHandler(ucEditFileExplorer_Load);

            _FileList = new BindingList<FileDataInfo>();
        }
        #endregion 构造函数...

        #region 自定义事件处理相关...
        private EditFileExplorerEventHandle _BeforItemInsert;
        public event EditFileExplorerEventHandle BeforItemInsert {
            add {
                _BeforItemInsert += value;
            }
            remove {
                _BeforItemInsert -= value;
            }
        }
        private void onBeforItemInsert(EditFileExplorerEventArgs arg) {
            if (_BeforItemInsert != null)
                _BeforItemInsert(this, arg);
        }

        private EditFileExplorerEventHandle _AfterItemInsert;
        public event EditFileExplorerEventHandle AfterItemInsert {
            add {
                _AfterItemInsert += value; 
            }
            remove {
                _AfterItemInsert -= value;
            }
        }
        private void onAfterItemInsert(EditFileExplorerEventArgs arg ) {
            if (_AfterItemInsert != null)
                _AfterItemInsert(this, arg);
        }

        #endregion 自定义事件处理相关...

        #region 扩展的public 属性...
        /// <summary>
        /// 当前文件的浏览列表。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<FileDataInfo> FileList {
            get {
                return _FileList;
            }
            set {
                _FileList = value;

                createlistitem();
            }
        }

        /// <summary>
        /// 文件浏览类型...
        /// </summary>
        public FileExplorerType FileExplorerType {
            get {
                return _FileExplorerType;
            }
            set {
                _FileExplorerType = value;
            }
        }
        /// <summary>
        /// 缩略图的大小。
        /// </summary>
        public Size ShrinkImageSize {
            get {
                return _ShrinkImageSize;
            }
            set {
                _ShrinkImageSize = value;
                _IconList.ImageSize = _ShrinkImageSize;
            }
        }
        #endregion 扩展的public 属性...

        #region 内部函数处理...

        //初始化文件列表...
        private void createlistitem() {
            _SmallIconList.Images.Clear();
            _IconList.Images.Clear();
            lstFileExplorer.Items.Clear();

            foreach (FileDataInfo info in _FileList) {
                System.Windows.Forms.ListViewItem item = createItemByInfo(info);
                EditFileExplorerEventArgs arg = new EditFileExplorerEventArgs(item, item.Tag as FileDataInfo);
                onBeforItemInsert(arg);

                if (!arg.Cancel) {
                    lstFileExplorer.Items.Add(item);
                    onAfterItemInsert(new EditFileExplorerEventArgs(item, item.Tag as FileDataInfo));
                }
            }
        }
        //根据Info 创建listview 的item
        private System.Windows.Forms.ListViewItem createItemByInfo(FileDataInfo dataInfo) {
            System.Windows.Forms.ListViewItem item = new ListViewItem(dataInfo.Name);
          
            item.SubItems.Add(dataInfo.Type);
            item.SubItems.Add((dataInfo.Size / 1000).ToString() + " KB");
            item.SubItems.Add(dataInfo.CreateDate.ToString("yyyy-MM-dd hh:mm"));
            item.SubItems.Add(dataInfo.Remark);
            item.Tag = dataInfo;

            _SmallIconList.Images.Add(MB.Util.MyConvert.Instance.ByteToImage(dataInfo.SmallFileIcon));

            _IconList.Images.Add(MB.Util.MyConvert.Instance.ByteToImage(dataInfo.LargeFileIcon));
            item.ImageIndex = _SmallIconList.Images.Count - 1;
 
            return item;
        }
        //创建listview 的表头
        private void createFileListHeader() {
            lstFileExplorer.Columns.Add("文件名", 160, HorizontalAlignment.Left);
            lstFileExplorer.Columns.Add("文件类型", 120, HorizontalAlignment.Left);
            lstFileExplorer.Columns.Add("文件大小", 120, HorizontalAlignment.Left);
            lstFileExplorer.Columns.Add("创建时间", 120, HorizontalAlignment.Left);
            lstFileExplorer.Columns.Add("备注", 200, HorizontalAlignment.Left);
        }
        private void addFileToList(string fileFullName) {
            FileInfo fi = new FileInfo(fileFullName);
            int fileLen = (int)fi.Length;
            byte[] datas = new byte[fileLen];
            System.IO.FileStream fileStream = fi.OpenRead();
            fileStream.Read(datas, 0, fileLen);
            fileStream.Close();

            FileDataInfo fileData = new FileDataInfo();
            if (_FileList.Where<FileDataInfo>(o => string.Compare(o.Name , fi.Name,true)==0).Count<FileDataInfo>() > 0 ) {
                return;
            }

            fileData.Name = fi.Name;
            fileData.Size = System.Convert.ToInt32(fi.Length);
            fileData.Type = fi.Extension;
            fileData.CreateDate = System.DateTime.Now;
            fileData.Content = datas;

            fileData.SmallFileIcon = MB.Util.MyConvert.Instance.ImageToByte(IconExtractor.GetFileIcon(fi.FullName, IconSize.Small));
            fileData.LargeFileIcon = MB.Util.MyConvert.Instance.ImageToByte(Image.FromFile(fileFullName).GetThumbnailImage(_ShrinkImageSize.Width, _ShrinkImageSize.Height, null, new IntPtr()));   //MB.Util.MyConvert.Instance.ImageToByte(IconExtractor.GetFileIcon(fi.FullName, IconSize.Large));

            _FileList.Add(fileData);
            System.Windows.Forms.ListViewItem item = createItemByInfo(fileData);
            lstFileExplorer.Items.Add(item);
        }
        //打开目录导入文件。
        private void openPath() {
            FolderDialog dialog = new FolderDialog();
                if (dialog.DisplayDialog() == DialogResult.OK) {
                    string[] files = System.IO.Directory.GetFiles(dialog.Path);
                    
                    foreach (string f in files) {
                        string ext = System.IO.Path.GetExtension(f);
                        //目前先针对图片文件来进行处理
                        if (Array.IndexOf<string>(IMAGE_FILE_EXTENSION, ext.ToUpper()) < 0)
                            continue;

                        addFileToList(f);
                    }
                }
        }

        //选择并增加文件到列表中。
        private void addFile() {
            OpenFileDialog fileDialog = new OpenFileDialog();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("All files(*.*)|*.*|");
            //sb.Append("Office 文档 (*.txt)|*.txt|(*.doc)|*.doc|(*.xls)|*.xls|");
            sb.Append("图象 文档 (*.bmp)|*.bmp|(*.gif)|*.gif|(*.jpg)|*.jpg");
           // sb.Append("压缩文档 (*.rar)|*.rar|(*.zip)|*.zip");

            fileDialog.Filter = sb.ToString();
            fileDialog.FilterIndex = 1;
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (fileDialog.OpenFile() == null) return;
                if (fileDialog.FileNames != null && fileDialog.FileNames.Length > 0) {
                    foreach(string fileName in fileDialog.FileNames)
                        addFileToList(fileName);
                }
                
            }
        }
        //删除选择的行
        private void deleteFile() {
            if (lstFileExplorer.SelectedItems.Count == 0) {
                MB.WinBase.MessageBoxEx.Show("请选择需要删除的电子文件.");
                return;
            }
            while (lstFileExplorer.SelectedItems.Count > 0) {
                System.Windows.Forms.ListViewItem item = lstFileExplorer.SelectedItems[0];
                FileDataInfo dataInfo = item.Tag as FileDataInfo;
                _FileList.Remove(dataInfo); 
                lstFileExplorer.Items.Remove(item);
            }
        }
        //导出电子文件
        private void outputFile() {
            if (lstFileExplorer.SelectedItems.Count == 0) {
                MB.WinBase.MessageBoxEx.Show("请选择需要导出的电子文件.");
                return;
            }
            try {
                FileDataInfo dataInfo = lstFileExplorer.SelectedItems[0].Tag as FileDataInfo;
                System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();

                saveDlg.DefaultExt = dataInfo.Type;
                string fileType = dataInfo.Type;
                saveDlg.Filter = fileType.Remove(0, 1) + " files (*" + fileType + ")|*" + fileType; ;

                if (saveDlg.ShowDialog() == DialogResult.OK) {
                    System.IO.Stream outPut = null;
                    if ((outPut = saveDlg.OpenFile()) != null) {
                        if (outPut.Length > 0) {
                            DialogResult re = MB.WinBase.MessageBoxEx.Question("文件已经存在，是否要覆盖原先的文件?");
                            if (re != DialogResult.Yes) {
                                outPut.Close();
                                return;
                            }
                        }
                        System.IO.BinaryWriter binWrite = new System.IO.BinaryWriter(outPut);
                        binWrite.Write(dataInfo.Content);
                        binWrite.Close();
                        if (outPut != null)
                            outPut.Close();

                        MB.WinBase.MessageBoxEx.Show("文件导出操作成功！");
                    }
                }
            }
            catch (Exception ex) {
                MB.WinBase.MessageBoxEx.Show("文件导出操作不成功！");
                MB.Util.TraceEx.Write(ex.Message);
            }
        }
        //浏览电子文件
        private void previewFile() {
            if (lstFileExplorer.SelectedItems.Count == 0) {
                MB.WinBase.MessageBoxEx.Show("请选择需要导出的电子文件.");
                return;
            }
            try {
                FileDataInfo dataInfo = lstFileExplorer.SelectedItems[0].Tag as FileDataInfo;
                //string tempFile = System.AppDomain.CurrentDomain.BaseDirectory  +     @"temp\" + System.Guid.NewGuid().ToString().Replace('-','1')  +  dataInfo.Type;
                string tempFile = System.AppDomain.CurrentDomain.BaseDirectory + @"temp\" + System.Guid.NewGuid().ToString() + dataInfo.Type;
                System.IO.FileStream myStream = new System.IO.FileStream(tempFile, System.IO.FileMode.CreateNew);
                System.IO.BinaryWriter binWrite = new System.IO.BinaryWriter(myStream);
                binWrite.Write(dataInfo.Content);
                binWrite.Close();
                myStream.Close();

                System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
                Info.FileName = tempFile;//FILE_PATH;
                //声明一个程序类
                System.Diagnostics.Process Proc;
                //启动外部程序
                Proc = System.Diagnostics.Process.Start(Info);
            }
            catch (Exception e) {
                MB.Util.TraceEx.Write("文件浏览有误" + e.Message);
                MB.WinBase.MessageBoxEx.Show("文件浏览有误,可能是找不到对应的打开程序。请重试.");
            }
        }
        #endregion 内部函数处理...

        #region 菜单和工具拦操作事件...
        private void mItemSmallicon_Click(object sender, System.EventArgs e) {
            lstFileExplorer.View = System.Windows.Forms.View.SmallIcon;
        }

        private void mItemDetail_Click(object sender, EventArgs e) {
            lstFileExplorer.View = System.Windows.Forms.View.Details;
        }

        private void mItemList_Click(object sender, EventArgs e) {
            lstFileExplorer.View = System.Windows.Forms.View.List;
        }

        private void mItemLargeIcon_Click(object sender, EventArgs e) {
            lstFileExplorer.View = System.Windows.Forms.View.LargeIcon;
        }

        private void toolBarMain_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
            try {
                if (e.Button.Equals(tButAddNew)) {
                    addFile();
                }
                else if (e.Button.Equals(tButDelete)) {
                    deleteFile();
                }
                else if (e.Button.Equals(tButOutPut)) {
                    outputFile();
                }
                else if (e.Button.Equals(tButPreview)) {
                    previewFile();
                }
                else if (e.Button.Equals(tButOpenPath)) {
                    openPath();
                }
                else if (e.Button.Equals(tButView)) { }
                else {
                    Debug.Assert(false, "");
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);   
            }
        }
       
        private void lstFileExplorer_DoubleClick(object sender, System.EventArgs e) {
            try {
                if (lstFileExplorer.Items.Count > 0)
                    previewFile();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void ucEditFileExplorer_Load(object sender, EventArgs e) {
            createFileListHeader();
        }

        #endregion 菜单和工具拦操作事件...
    }


    #region FileDataInfo...
    /// <summary>
    /// 文件描述信息
    /// </summary>
    [Serializable]
    public class FileDataInfo {
        private int _ID;

        private string _Name;
        private string _Type;
        private int _Size;
        private DateTime _CreateDate;
        private string _Remark;

        private byte[] _SmallFileIcon;
        private byte[] _LargeFileIcon;
        private byte[] _Content;

        /// <summary>
        /// 构造函数...
        /// </summary>
        public FileDataInfo(){

        }

        #region public 属性...
        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        public string Type {
            get {
                return _Type;
            }
            set {
                _Type = value;
            }
        }
        public int Size {
            get {
                return _Size;
            }
            set {
                _Size = value;
            }
        }
        public DateTime CreateDate {
            get {
                return _CreateDate;
            }
            set {
                _CreateDate = value;
            }
        }
        public string Remark {
            get {
                return _Remark;
            }
            set {
                _Remark = value;
            }
        }
        public byte[] SmallFileIcon {
            get {
                return _SmallFileIcon;
            }
            set {
                _SmallFileIcon = value;
            }
        }
        public byte[] LargeFileIcon {
            get {
                return _LargeFileIcon;
            }
            set {
                _LargeFileIcon = value;
            }
        }
        public byte[] Content {
            get {
                return _Content;
            }
            set {
                _Content = value;
            }
        }
        #endregion public 属性...


    }
    #endregion FileDataInfo...

    #region 获取文件图标相关...
    public enum IconSize {
        /// <summary>
        /// 16X16 icon
        /// </summary>
        Small,
        /// <summary>
        /// 32X32 icon
        /// </summary>
        Large
    }
    /// <summary>
    /// Util class to extract icons from files or directories.
    /// </summary>
    class IconExtractor {
        private static readonly string NULL_FILE_ICO = @"MB.WinClientDefault.Images.NullFileType.ico";

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        class Win32 {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
            public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon

            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath,
                uint dwFileAttributes,
                ref SHFILEINFO psfi,
                uint cbSizeFileInfo,
                uint uFlags);
        }

        /// <summary>
        /// Gets the icon asotiated with the filename.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap GetFileIcon(string fileName, IconSize _iconSize) {
            System.Drawing.Icon myIcon = null;
            try {
                IntPtr hImgSmall;    //the handle to the system image list
                SHFILEINFO shinfo = new SHFILEINFO();

                //Use this to get the small Icon
                hImgSmall = Win32.SHGetFileInfo(fileName, 0, ref shinfo,
                    (uint)Marshal.SizeOf(shinfo),
                    Win32.SHGFI_ICON |
                    (_iconSize == IconSize.Small ? Win32.SHGFI_SMALLICON : Win32.SHGFI_LARGEICON));

                //The icon is returned in the hIcon member of the shinfo
                myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
            }
            catch {

                System.Drawing.Bitmap nullType = MB.WinClientDefault.Images.ImageListHelper.Instance.CreateBitmapFromResources(NULL_FILE_ICO);
                return nullType;
            }

            return myIcon.ToBitmap();
        }
    }
    #endregion 获取文件图标相关...

    #region FolderDialog...
    /// <summary>
    /// FolderDialog 选择目录
    /// </summary>
    public class FolderDialog : System.Windows.Forms.Design.FolderNameEditor {
        System.Windows.Forms.Design.FolderNameEditor.FolderBrowser fDialog = new
            System.Windows.Forms.Design.FolderNameEditor.FolderBrowser();
        public FolderDialog() {
        }
        public DialogResult DisplayDialog() {
            return DisplayDialog("请选择一个文件夹");
        }

        public DialogResult DisplayDialog(string description) {
            fDialog.Description = description;
            return fDialog.ShowDialog();
        }
        public string Path {
            get {
                return fDialog.DirectoryPath;
            }
        }
        ~FolderDialog() { //
            fDialog.Dispose();
        }
    }
    #endregion FolderDialog...

    #region FileExplorerType...
    /// <summary>
    /// 文件浏览类型。
    /// </summary>
    public enum FileExplorerType {
        [Description("All files(*.*)|*.*|")]
        ALL,
        [Description("图象 文档 (*.Bmp)|*.bmp|(*.Gif)|*.gif|(*.jpg)|*.jpg|")]
        Image,
        [Description("压缩文档 (*.rar)|*.rar|(*.zip)|*.zip")]
        Zip,
        [Description("Office 文档 (*.txt)|*.txt|(*.doc)|*.doc|(*.xls)|*.xls|")]
        Office
    }
    #endregion FileExplorerType...

    #region  事件类型定义...
    public delegate void EditFileExplorerEventHandle(object sender,EditFileExplorerEventArgs arg);
    public class EditFileExplorerEventArgs {
        private System.Windows.Forms.ListViewItem _Item;
        private FileDataInfo _ItemData;
        private bool _Cancel;

        public EditFileExplorerEventArgs(System.Windows.Forms.ListViewItem item, FileDataInfo itemData) {
            _Item = item;
            _ItemData = itemData;
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ListViewItem Item {
            get {
                return _Item;
            }
            set {
                _Item = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public FileDataInfo ItemData {
            get {
                return _ItemData;
            }
            set {
                _ItemData = value;
            }
        }
        /// <summary>
        /// 是否取消。
        /// </summary>
        public bool Cancel {
            get {
                return _Cancel;
            }
            set {
                _Cancel = value;
            }
        }
    }
    #endregion  事件类型定义...
}
