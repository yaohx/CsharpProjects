//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-12-09
// Description	:	Image Ico 图象编辑处理控件。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Ctls {
    /// <summary>
    /// Image Ico 图象编辑处理控件。
    /// </summary>
    [ToolboxItem(true)]
    public partial class ucIamgeIcoEdit : UserControl, System.ComponentModel.INotifyPropertyChanged {
        private const int CONTROL_WIDTH = 52;
        private const int CONTROL_HEIGHT = 23;
        private const int ICO_IMAGE_SIZE = 16;
        private System.Windows.Forms.ToolTip _ToolTip;
        public ucIamgeIcoEdit() {
            InitializeComponent();

            this.Resize += new EventHandler(ucIamgeIcoEdit_Resize);
            picImageIco.SizeMode = PictureBoxSizeMode.StretchImage;
            _ToolTip = new ToolTip();
            _ToolTip.SetToolTip(picImageIco, "为了减少数据的存储空间,最好选择16*16的图表");
        }

        void ucIamgeIcoEdit_Resize(object sender, EventArgs e) {

            this.Size = new Size(CONTROL_WIDTH, CONTROL_HEIGHT);
        }

        private void butSearchImage_Click(object sender, EventArgs e) {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog(); ;
            fileDialog.Filter = "图标文件 (*.ico)|*.ico";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                try {
                    Image tempImg = System.Drawing.Image.FromFile(fileDialog.FileName);
                    if(tempImg!=null) 
                        tempImg = tempImg.GetThumbnailImage(ICO_IMAGE_SIZE, ICO_IMAGE_SIZE, null, new IntPtr());

                    picImageIco.Image = tempImg;

                    RaisePropertyChanged("ImageData");

                }
                catch {
                    picImageIco.Image = null;

                    MB.WinBase.MessageBoxEx.Show("要显示的图片格式选择不正确，请重新选择!");
                }
            }
        }

        #region 扩展的public 属性...
        /// <summary>
        /// 设置或者获取图片信息。
        /// </summary>
        [Description("设置或者获取图片信息")]
        public System.Drawing.Image Image {
            get {
                return picImageIco.Image;
            }
            set {
                picImageIco.Image = value;
            }
        }
        /// <summary>
        /// 获取或设置图片的数据。
        /// </summary>
        [Browsable(false)]
        public byte[] ImageData {
            get {
                if (picImageIco.Image != null)
                    return MB.Util.MyConvert.Instance.ImageToByte(picImageIco.Image);
                else
                    return null;
            }
            set {
                if (value != null && value.Length > 0) {
                    Image tempImg = MB.Util.MyConvert.Instance.ByteToImage(value);
                    picImageIco.Image = tempImg;
                }
                else {
                    picImageIco.Image = null;
                }
            }
        }

        #endregion 扩展的public 属性...

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
