//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-08
// Description	:	图象浏览控件。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault.Ctls {
    /// <summary>
    /// 图象浏览控件。
    /// </summary>
    public partial class ucImageFileList : UserControl {

        #region 变量定义...
        private object _DataSource;//包含图象数据的数据集。
        private string _KeyFieldName;//图象对应的键名称。
        private string _ImageFieldName;//图象对应的字段名称。
        private string _RemarkFieldName;//显示标记的字段名称
        private string _RemarkFieldCaption;

        private int _PicHeight;//单个图象的高度。
        private int _PicWidth;//单个图象的宽度。

        //最顶部的间距
        private const int TOP_SEP_HEIGHT = 5;
        //最左边的间距
        private const int LEFT_SEP_WIDTH = 5;
        //图片之间间割的长度
        private const int SETP_LENGTH = 40;
        //得到垂直画能画几个图象。
        private int _CurrentVCount;
        //当前记录的个数 ， // 以后可以修改为
        private int _RowsCount;
        //当前选择的行
        private int _CurrentRowIndex;

        //图象的最小大小
        private const int MIN_SIZE = 32;
        //图象的最大大小
        private const int MAX_SIZE = 1024;
        //每次放大或者缩小的倍数
        private const int SIZE_PERCENT = 20;
        private Dictionary<int,byte[]>  _HtImageData;
        #endregion 变量定义...
		
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucImageFileList() {
            InitializeComponent();

            picMain.Left = 0;
            picMain.Top = 0;

            _PicHeight = 100;
            _PicWidth = 120;

            panMain.Dock = System.Windows.Forms.DockStyle.Fill;
        }


        #region 自定义事件...
        private ImageFileListEventHandler _ImageClick;
        [Description("当用户Click 显示的单个图像时候响应。"), Category("操作")]
        public event ImageFileListEventHandler ImageClick {
            add {
                _ImageClick += value;
            }
            remove {
                _ImageClick -= value;
            }
        }
        protected virtual void OnImageClick(ImageFileListEventArgs arg) {
            if (_ImageClick != null) {
                _ImageClick(this, arg);
            }
        }
        private ImageFileListEventHandler _ImageDoubleClick;
        [Description("当用户DoubleClick 显示的单个图像时候响应。"), Category("操作")]
        public event ImageFileListEventHandler ImageDoubleClick {
            add {
                _ImageDoubleClick += value;
            }
            remove {
                _ImageDoubleClick -= value;
            }
        }
        protected virtual void OnImageDoubleClick(ImageFileListEventArgs arg) {
            if (_ImageDoubleClick != null) {
                _ImageDoubleClick(this, arg);
            }
        }
        #endregion 自定义事件...

        #region 扩展的public 方法...
        /// <summary>
        /// 重新刷新并绘制图象列表。
        /// </summary>
        public void RefreshDataSource() {
            _HtImageData = new Dictionary<int, byte[]>();
            picMain.Invalidate();
        }
        #endregion 扩展的public 方法...

        #region Public 属性...

        [ReadOnly(true), Description("设置或者获取图片控件的数据源,在设计时只读。"), Category("数据")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource {
            get {
                return _DataSource;
            }
            set {
                _DataSource = value;
                RefreshDataSource();
                //drawDataSource();
            }
        }
        [Description("设置或者获取图片数据对应的键值字段名称。"), Category("数据")]
        public string KeyFieldName {
            get {
                return _KeyFieldName;
            }
            set {
                _KeyFieldName = value;
            }
        }
        [Description("设置或者获取图片数据对应的字段名称，根据它控件自动绘制图片。"), Category("数据")]
        public string ImageFieldName {
            get {
                return _ImageFieldName;
            }
            set {
                _ImageFieldName = value;
            }
        }
        [Description("设置或者获取图片数据对应附件说明的字段名称，根据它控件自动绘制图片。"), Category("数据")]
        public string RemarkFieldName {
            get {
                return _RemarkFieldName;
            }
            set {
                _RemarkFieldName = value;
            }
        }
        [Description("设置或者获取图片数据对应附件说明的字段名称描述，根据它控件自动绘制图片。"), Category("数据")]
        public string RemarkFieldCaption {
            get {
                return _RemarkFieldCaption;
            }
            set {
                _RemarkFieldCaption = value;
            }
        }
        [Description("设置或者获取单个图象的显示高度。"), Category("外观")]
        public int DefaultImageHeight {
            get {
                return _PicHeight;
            }
            set {
                _PicHeight = value;
            }
        }
        [Description("设置或者获取单个图象的显示宽度。"), Category("外观")]
        public int DefaultImageWidth {
            get {
                return _PicWidth;
            }
            set {
                _PicWidth = value;
            }
        }
        #endregion Public 属性...

        #region 内部函数处理...
        //在Picture 上 绘制图象数据。
        private void drawDataSource(System.Drawing.Graphics g) {
            if (string.IsNullOrEmpty(_ImageFieldName)) {
                MB.WinBase.MessageBoxEx.Show("图像集合列表没有配置相应的图像字段！");
                return;
            }

            if (_DataSource == null)
                return;
            IList lstData =  _DataSource as IList;
            if (lstData == null)
                throw new MB.Util.APPException("目前只支持数据实体集合类！", MB.Util.APPMessageType.SysErrInfo);
   
            int index = 0;

            Rectangle re = getDestRect(lstData.Count - 1);
            picMain.Height = re.Height;
            picMain.Width = re.Left + re.Width + SETP_LENGTH;
            _RowsCount = lstData.Count;
           
            //以后可以修改为分页显示处理的方式。
            foreach (object o in lstData) {
                object imgData = MB.Util.MyReflection.Instance.InvokePropertyForGet(o, _ImageFieldName);
                if (imgData != null && imgData != System.DBNull.Value) {
                    int hasCode = o.GetHashCode();
                    if (_HtImageData.ContainsKey(hasCode))
                        imgData = _HtImageData[hasCode];
                    else {
                        _HtImageData[hasCode] = (byte[])imgData;

                    }
                }
                drawImage(o, imgData, g, index++);
            }
        }
        //绘制单个图象
        private void drawImage(object  entity, object imgData, Graphics g, int index) {
            Image img = null;
            if (imgData != null || imgData != System.DBNull.Value)
                img = MB.Util.MyConvert.Instance.ByteToImage((Byte[])imgData);

            Rectangle destRect = getDestRect(index);
            Rectangle destImageRect = getRealImageRect(destRect, img.Size); 

            //计算当前图片，如果不在显示的屏幕范围，那么就不需要绘制
            Rectangle clientRect = this.RectangleToScreen(this.Bounds);
            Rectangle srect = picMain.RectangleToScreen(destRect);
            if (!clientRect.Contains(srect) && !clientRect.IntersectsWith(srect))
                return;

            //绘制图象
            if (img != null) {
                Rectangle srcRect = new Rectangle(0, 0, img.Width, img.Height);
                g.DrawImage(img, destImageRect, srcRect, System.Drawing.GraphicsUnit.Pixel);
                img.Dispose();
                GC.SuppressFinalize(img);
            }

            g.DrawRectangle(System.Drawing.Pens.LightGray, destRect);
            //绘制说明
            if (_RemarkFieldName == null || _RemarkFieldName.Length == 0)
                return;
            if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity,_RemarkFieldName))
                return;
            object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, _RemarkFieldName) ;
            string dispStr = string.Empty;
            if (val != null)
                dispStr = val.ToString();

            if (_RemarkFieldCaption != null && _RemarkFieldCaption.Length > 0)
                dispStr = _RemarkFieldCaption + ":" + dispStr;
            Point beg = new Point(destRect.Left, destRect.Top);


            Brush brush = System.Drawing.Brushes.Black;// new SolidBrush(Color.YellowGreen);
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 9.0f, System.Drawing.FontStyle.Regular);
            StringFormat strFormat = new StringFormat();
            strFormat.Trimming = StringTrimming.EllipsisCharacter;
            strFormat.Alignment = StringAlignment.Center;

            Rectangle textRect = new Rectangle(destRect.X, destRect.Y + destRect.Height, destRect.Width, SETP_LENGTH);
            g.DrawString(dispStr, font, brush, textRect, strFormat);

        }
        //根据记录的Index 获取绘制目标图片的rect 
        private Rectangle getDestRect(int index) {
            //得到垂直画能画几个图象。
            int vCount = (panMain.Height - TOP_SEP_HEIGHT) / (_PicHeight + SETP_LENGTH);
            vCount = vCount == 0 ? 1 : vCount;
            _CurrentVCount = vCount;

            int ix = index / vCount;
            int iy = index % vCount;

            int left = ix * (_PicWidth + SETP_LENGTH / 2) + LEFT_SEP_WIDTH;
            int top = iy * (_PicHeight + SETP_LENGTH) + TOP_SEP_HEIGHT;

            return new Rectangle(left, top, _PicWidth, _PicHeight);
        }
        //根据图象的实际大小 和需要绘制的边框大小 计算出需要绘制的图象边框
        private Rectangle getRealImageRect(Rectangle imageBound, Size srcImageSize) {
            //先确认不变的大小（高度或者宽度）
            int width = 0;
            int height = 0;
            if ((imageBound.Height / imageBound.Width) > (srcImageSize.Height / srcImageSize.Width)) {
                width = imageBound.Width;
                height = srcImageSize.Height * width / srcImageSize.Width;
            }
            else {
                height = imageBound.Height;
                width = srcImageSize.Width * height / srcImageSize.Height;
            }
            return new Rectangle(imageBound.X + (imageBound.Width - width) / 2, imageBound.Y + (imageBound.Height - height) / 2, width, height);
        }
        //根据Point 获取 该记录的Index 
        private int getRowIndexByPoint(Point pMouse) {
            for (int i = 0; i < _RowsCount; i++) {
                //变态的处理方法，在损失性能的前提下，可以增加可读性以及实现的简单性。
                Rectangle rect = getDestRect(i);
                if (rect.Contains(pMouse))
                    return i;
            }
            return -1;
        }
        //判断是否重新绘制 ，主要根据当前垂直画能画几个图象的个数是否发生改变来决定。
        private bool checkCanDraw() {
            if (picMain.Image == null)
                return true;
            //得到垂直画能画几个图象。
            int vCount = this.Height / (_PicHeight + SETP_LENGTH);
            vCount = vCount == 0 ? 1 : vCount;
            if (_CurrentVCount != vCount)
                return true;
            return false;

        }
        //获取Image 操作响应的事件参数
        private ImageFileListEventArgs getImageEventArgs(int index) {
            ImageFileListEventArgs arg = new ImageFileListEventArgs();
            if (index < 0)
                return null;
           
            arg.DataRow = (_DataSource as IList)[index];
            if(!string.IsNullOrEmpty(_KeyFieldName))
                arg.KeyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(arg.DataRow, _KeyFieldName);
            return arg;
        }
        //根据index 绘制选择的边框。
        private void drawSelectFrameByIndex(int index) {
            if (index < 0)
                return;
            Rectangle curRect = getDestRect(index);
            curRect = picMain.RectangleToScreen(curRect);
            ControlPaint.DrawReversibleFrame(curRect, Color.YellowGreen, FrameStyle.Thick);
        }
        #endregion 内部函数处理...

        #region 界面操作事件...
        private void ucPictureList_Resize(object sender, System.EventArgs e) {
            bool b = checkCanDraw();
            if (!b)
                return;

            picMain.Height = panMain.Height > _PicHeight + SETP_LENGTH ? panMain.Height - 1 : _PicHeight + SETP_LENGTH;
            picMain.Invalidate();
        }

        private void picMain_Click(object sender, System.EventArgs e) {
            ImageFileListEventArgs arg = getImageEventArgs(_CurrentRowIndex);
            if (arg == null)
                return;
            OnImageClick(arg);
        }
        private void picMain_DoubleClick(object sender, System.EventArgs e) {
            ImageFileListEventArgs arg = getImageEventArgs(_CurrentRowIndex);
            if (arg == null)
                return;
            OnImageDoubleClick(arg);
        }
        private void picMain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            _CurrentRowIndex = getRowIndexByPoint(new Point(e.X, e.Y));
            drawSelectFrameByIndex(_CurrentRowIndex);
        }
        private void picMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
            drawSelectFrameByIndex(_CurrentRowIndex);
        }
        #endregion 界面操作事件...

        private void picMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
            if (MB.Util.General.IsInDesignMode())
                return;

            drawDataSource(e.Graphics);
        }

     
        private void bButZoom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            int zw = _PicWidth + SIZE_PERCENT;
            if (zw < MAX_SIZE && _PicHeight + SIZE_PERCENT < MAX_SIZE) {
                _PicWidth = zw;
                _PicHeight = _PicHeight + SIZE_PERCENT;

                picMain.Invalidate();
            }
        }

        private void bButShrink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            int sw = _PicWidth - SIZE_PERCENT;
            if (sw > MIN_SIZE && _PicHeight - SIZE_PERCENT > MIN_SIZE) {
                _PicWidth = sw;
                _PicHeight = _PicHeight - SIZE_PERCENT;

                picMain.Invalidate();
            }
        }
    }

    #region 自定义事件项...
    /// <summary>
    ///  事件委托申明
    /// </summary>
    public delegate void ImageFileListEventHandler(object sender, ImageFileListEventArgs e);

    /// <summary>
    /// UP4PictureListEventArgs 公共事件定义
    /// </summary>
    public class ImageFileListEventArgs : EventArgs {
        private int _RowHandle;
        private object _KeyValue;
        private object _DataRow;

        public ImageFileListEventArgs() {

        }

        #region Public 属性...
        public int RowHandle {
            get {
                return _RowHandle;
            }
            set {
                _RowHandle = value;
            }
        }
        public object KeyValue {
            get {
                return _KeyValue;
            }
            set {
                _KeyValue = value;
            }
        }
        public object  DataRow {
            get {
                return _DataRow;
            }
            set {
                _DataRow = value;
            }
        }
        #endregion Public 属性...

    }

    #endregion 自定义事件项...
}
