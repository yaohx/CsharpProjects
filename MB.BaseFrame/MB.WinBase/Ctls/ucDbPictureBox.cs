//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	Nick
// Create date	:	2005-12-02
// Description	:	DBPictureBox 图片的选择和显示。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace MB.WinBase.Ctls
{
    /// <summary>
    /// DBPictureBox 图片的选择和显示。
    /// </summary>
    public class ucDbPictureBox : System.Windows.Forms.UserControl, System.ComponentModel.INotifyPropertyChanged
    {
        #region 内部自动生成代码...

        private System.Windows.Forms.ContextMenu cMenuMain;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuOpen;
        private System.Windows.Forms.MenuItem mnuDelete;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuStretch;
        private System.Windows.Forms.MenuItem menuCenter;
        private System.Windows.Forms.MenuItem menuNormal;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.MenuItem menuPast;
        private MenuItem mItemSaveAs;
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.cMenuMain = new System.Windows.Forms.ContextMenu();
            this.mnuOpen = new System.Windows.Forms.MenuItem();
            this.menuPast = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuNormal = new System.Windows.Forms.MenuItem();
            this.menuStretch = new System.Windows.Forms.MenuItem();
            this.menuCenter = new System.Windows.Forms.MenuItem();
            this.panMain = new System.Windows.Forms.Panel();
            this.picMain = new System.Windows.Forms.PictureBox();
            this.mItemSaveAs = new System.Windows.Forms.MenuItem();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.SuspendLayout();
            // 
            // cMenuMain
            // 
            this.cMenuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOpen,
            this.menuPast,
            this.menuItem2,
            this.mnuDelete,
            this.menuItem1,
            this.menuItem3,
            this.mItemSaveAs});
            // 
            // mnuOpen
            // 
            this.mnuOpen.Index = 0;
            this.mnuOpen.Text = "打开(&O)";
            // 
            // menuPast
            // 
            this.menuPast.Index = 1;
            this.menuPast.Text = "粘贴(&P)";
            this.menuPast.Click += new System.EventHandler(this.menuPast_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "-";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 3;
            this.mnuDelete.Text = "删除(&D)";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 5;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNormal,
            this.menuStretch,
            this.menuCenter});
            this.menuItem3.Text = "图象";
            // 
            // menuNormal
            // 
            this.menuNormal.Index = 0;
            this.menuNormal.Text = "正常";
            this.menuNormal.Click += new System.EventHandler(this.menuNormal_Click);
            // 
            // menuStretch
            // 
            this.menuStretch.Index = 1;
            this.menuStretch.Text = "大小自适应";
            this.menuStretch.Click += new System.EventHandler(this.menuStretch_Click);
            // 
            // menuCenter
            // 
            this.menuCenter.Index = 2;
            this.menuCenter.Text = "图片正中间";
            this.menuCenter.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // panMain
            // 
            this.panMain.AutoScroll = true;
            this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMain.Controls.Add(this.picMain);
            this.panMain.Location = new System.Drawing.Point(16, 8);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(232, 104);
            this.panMain.TabIndex = 0;
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.Color.White;
            this.picMain.Location = new System.Drawing.Point(1, 1);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(32, 24);
            this.picMain.TabIndex = 1;
            this.picMain.TabStop = false;
            // 
            // mItemSaveAs
            // 
            this.mItemSaveAs.Index = 6;
            this.mItemSaveAs.Text = "另存为(&S)";
            this.mItemSaveAs.Click += new System.EventHandler(this.mItemSaveAs_Click);
            // 
            // ucDbPictureBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenu = this.cMenuMain;
            this.Controls.Add(this.panMain);
            this.Name = "ucDbPictureBox";
            this.Size = new System.Drawing.Size(280, 136);
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #endregion 内部自动生成代码...

        #region 变量定义...
        private Form _ViewForm;
        private PictureBox _ViewPicBox;
        private bool _ReadOnly;
        private const int SEP_WIDTH = 5;
        private const int DEFAULT_WIDTH = 24;

        #endregion 变量定义...

        #region 自定义事件...
        private System.EventHandler _ImageChanged;
        public event System.EventHandler ImageChanged {
            add {
                _ImageChanged += value;
            }
            remove {
                _ImageChanged -= value;
            }
        }

        protected virtual void OnImageChanged(System.EventArgs arg) {
            if (_ImageChanged != null) {
                _ImageChanged(this, arg);
            }
        }
        #endregion 自定义事件...

        #region 构造函数...
        public ucDbPictureBox() {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化
            panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            mnuOpen.Click += new EventHandler(mnuOpen_Click);
            mnuDelete.Click += new EventHandler(mnuDelete_Click);

            picMain.BackgroundImageChanged += new EventHandler(picMain_BackgroundImageChanged);
            picMain.DoubleClick += new EventHandler(picMain_DoubleClick);

            iniViewImageForm();

            this.Load += new EventHandler(DBPictureBox_Load);
            picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            cMenuMain.Popup += new EventHandler(cMenuMain_Popup);

            //panMain.BorderStyle  
        }
        #endregion 构造函数...

        #region Public 属性...

        /// <summary>
        /// 设置或获取另存图片时的默认文件名
        /// </summary>
        [Description("指示另存图片时的默认文件名")]
        public string ImageFileName { get; set; }

        /// <summary>
        /// 设置或者获取图片信息。
        /// </summary>
        [Description("指示面板是否有边框。")]
        public new System.Windows.Forms.BorderStyle BorderStyle {
            get {
                return panMain.BorderStyle;
            }
            set {
                panMain.BorderStyle = value;
            }
        }
        /// <summary>
        /// 设置或者获取图片信息。
        /// </summary>
        [Description("设置或者获取图片信息")]
        public System.Drawing.Image Image {
            get {
                return picMain.Image;
            }
            set {
                if (picMain.SizeMode != System.Windows.Forms.PictureBoxSizeMode.AutoSize) {
                    picMain.Size = new Size(this.panMain.Width - SEP_WIDTH, this.panMain.Height - SEP_WIDTH);
                }
                else {

                }
                picMain.Image = value;
                _imageData = null;
                RaisePropertyChanged("ImageData");
            }
        }

        byte[] _imageData = null;

        /// <summary>
        /// 获取或设置图片的数据。
        /// </summary>
        [Browsable(false)]
        public byte[] ImageData {
            get {
                if (picMain.Image != null)
                {
                    if(_imageData==null)
                        _imageData=MB.Util.MyConvert.Instance.ImageToByte(picMain.Image);
                    return _imageData;
                }
                else
                    return null;
            }
            set {
                _imageData = null;

                if (value != null && value.Length > 0) {
                    Image tempImg = MB.Util.MyConvert.Instance.ByteToImage(value);           
                    picMain.Image = tempImg;

                    _imageData = value;
                }
                else {
                    picMain.Image = null;
                }
                
            }
        }
        /// <summary>
        /// 设置控件的只读状态
        /// </summary>
        public bool ReadOnly {
            get {
                return _ReadOnly;
            }
            set {
                _ReadOnly = value;
                if (!_ReadOnly) {
                    this.ContextMenu = cMenuMain;
                }
                else {
                    this.ContextMenu = null;
                }
            }
        }
        /// <summary>
        /// 控件图片框将如何显示图片
        /// </summary>
        [Description("控件图片框将如何显示图片。")]
        public System.Windows.Forms.PictureBoxSizeMode SizeModel {
            get {
                return picMain.SizeMode;
            }
            set {
                picMain.SizeMode = value;
            }
        }
        #endregion Public 属性...

        #region 界面事件...
        // 从本地硬盘中选择图片。
        private void mnuOpen_Click(object sender, EventArgs e) {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog(); ;
            fileDialog.Filter = "图片文件 (*.Jpg)|*.Jpg|图片文件 (*.Bmp)| *.Bmp|All files (*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                try {
                    Image tempImg = System.Drawing.Image.FromFile(fileDialog.FileName);

                    if (picMain.SizeMode != System.Windows.Forms.PictureBoxSizeMode.AutoSize) {
                        picMain.Size = new Size(this.panMain.Width - SEP_WIDTH, this.panMain.Height - SEP_WIDTH);
                    }
                    this.Image = tempImg;
                    OnImageChanged(null);
                   
                }
                catch {
                    this.Image = null;

                    OnImageChanged(null);

                    picMain.Size = new Size(this.panMain.Width - SEP_WIDTH, this.panMain.Height - SEP_WIDTH);
                    MB.WinBase.MessageBoxEx.Show("要显示的图片格式选择不正确，请重新选择!");
                }
            }
        }
        // 清空图片区域的显示。
        private void mnuDelete_Click(object sender, EventArgs e) {
            try {
                this.Image = null;
                //picMain.Size = new Size(24,24); 
            }
            catch (Exception ee) {
                throw new MB.Util.APPException("清除图片信息有误" + ee.Message, Util.APPMessageType.DisplayToUser);
            }
        }

        private void picMain_DoubleClick(object sender, System.EventArgs e) {
            _ViewPicBox.Image = this.Image;
            _ViewForm.Owner = this.ParentForm;
            _ViewForm.Show();

        }

        private void _ViewForm_Closing(object sender, CancelEventArgs e) {
            if (_ViewForm.Equals(sender)) {
                e.Cancel = true;
                _ViewForm.Hide();
                this.ParentForm.BringToFront();
            }
        }


        private void _ViewButClose_Click(object sender, EventArgs e) {
            _ViewForm.Hide();
        }

        private void picMain_BackgroundImageChanged(object sender, EventArgs e) {
            _ViewPicBox.Image = this.Image;
        }

        private void menuItem6_Click(object sender, System.EventArgs e) {
            picMain.Size = new Size(this.panMain.Width - SEP_WIDTH, this.panMain.Height - SEP_WIDTH);
            picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }

        private void menuStretch_Click(object sender, System.EventArgs e) {
            picMain.Size = new Size(this.panMain.Width - SEP_WIDTH, this.panMain.Height - SEP_WIDTH);
            picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }

        private void menuNormal_Click(object sender, System.EventArgs e) {
            picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        }
        private void cMenuMain_Popup(object sender, EventArgs e) {
            object img = getBitmapFromClipboard();
            menuPast.Enabled = img != null;
        }
        private void menuPast_Click(object sender, System.EventArgs e) {
            System.Drawing.Bitmap img = getBitmapFromClipboard();
            if (img == null) return;
            if (picMain.SizeMode != System.Windows.Forms.PictureBoxSizeMode.AutoSize) {
                picMain.Size = new Size(this.panMain.Width - SEP_WIDTH, this.panMain.Height - SEP_WIDTH);
            }
            this.Image = img;
            OnImageChanged(null);
        }
        protected override void OnSizeChanged(EventArgs e) {
            picMain.Size = new Size(panMain.Width - SEP_WIDTH, panMain.Height - SEP_WIDTH);
            base.OnSizeChanged(e);
        }

        private void DBPictureBox_Load(object sender, EventArgs e) {
            picMain.Location = new Point(0, 0);
            picMain.Size = new Size(panMain.Width - SEP_WIDTH, panMain.Height - SEP_WIDTH);
        }
        //从剪贴板中获取图片
        private System.Drawing.Bitmap getBitmapFromClipboard() {
            IDataObject data = Clipboard.GetDataObject();
            object d = data.GetData(typeof(System.Drawing.Bitmap));
            if (d != null)
                return d as System.Drawing.Bitmap;
            else
                return null;

        }

        #endregion 界面事件...

        #region 内部函数处理...
        private void iniViewImageForm() {
            _ViewForm = new Form();
            _ViewPicBox = new PictureBox();

            _ViewForm.Closing += new CancelEventHandler(_ViewForm_Closing);

            this._ViewPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            _ViewForm.MaximizeBox = false;
            _ViewForm.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            _ViewForm.BackColor = System.Drawing.Color.White;
            _ViewForm.ClientSize = new System.Drawing.Size(352, 238);
            _ViewForm.Controls.Add(this._ViewPicBox);

            _ViewForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        }
        //设置控件的Image.
        private void setImage() {

        }
        #endregion 内部函数处理...



        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged 成员

        private void mItemSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Image == null) return;

                SaveFileDialog dialog = new SaveFileDialog();

                if (this.Image.RawFormat.Guid == ImageFormat.Jpeg.Guid)
                    dialog.Filter = "图片文件(*.jpg)|*.jpg";
                else if (this.Image.RawFormat.Guid == ImageFormat.Bmp.Guid)
                    dialog.Filter = "图片文件(*.bmp)|*.bmp";
                else if(this.Image.RawFormat.Guid==ImageFormat.Gif.Guid)
                    dialog.Filter = "图片文件(*.gif)|*.gif";
                else if (this.Image.RawFormat.Guid == ImageFormat.Png.Guid)
                    dialog.Filter = "图片文件(*.png)|*.png";
                else
                    dialog.Filter="All files(*.*)|*.*)";

                if (String.IsNullOrEmpty(this.ImageFileName))
                    dialog.FileName = "未命名";
                else
                    dialog.FileName = ImageFileName;

                //dialog.DefaultExt = "图片文件 (*.Jpg)|*.Jpg| 图片文件 (*.Bmp)| *.Bmp|All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;
                    Image img = this.Image;

                    if (System.IO.File.Exists(filePath))
                    {
                        var result = MessageBoxEx.Question("文件名已存在，是否替换？");
                        if (result == DialogResult.Cancel || result == DialogResult.No) return;

                        System.IO.File.Delete(filePath);
                    }

                    using (FileStream fs = File.Open(filePath,FileMode.OpenOrCreate))
                    {
                        fs.Write(this.ImageData, 0, this.ImageData.Length);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
            finally
            {
 
            }
        }

    }
}
