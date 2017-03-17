using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.WinBase.Binding;
using MB.WinBase.Design;
using System.Drawing.Design;
using MB.Util;
using MB.WinBase.Extend.Base;

namespace MB.WinBase.Extend.Ctls
{
    [ToolboxItem(true)]
    public partial class ucComboxRegionHEx : ucComboxRegionBase
    {
        public ucComboxRegionHEx()
        {
            InitializeComponent();

            this.Load += new EventHandler(UserControl_Load);            
        }

        private TableLayoutPanel tableLayoutPanel5;

        #region private components define...

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        protected override void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.myTableLayoutPanelProvider1 = new MB.WinBase.Design.MyTableLayoutPanelProvider(this.components);
            this.lbCity = new System.Windows.Forms.Label();
            this.lbCountry = new System.Windows.Forms.Label();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.lbDistrict = new System.Windows.Forms.Label();
            this.lbProvince = new System.Windows.Forms.Label();
            this.cbDistrict = new System.Windows.Forms.ComboBox();
            this.cbProvince = new System.Windows.Forms.ComboBox();
            this.myDataBindingProvider1 = new MB.WinBase.Binding.MyDataBindingProvider(this.components);
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCity
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbCity, null);
            this.lbCity.Location = new System.Drawing.Point(3, 26);
            this.lbCity.Name = "lbCity";
            this.lbCity.Size = new System.Drawing.Size(135, 28);
            this.lbCity.TabIndex = 2;
            this.lbCity.Text = "城市";
            this.lbCity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCountry
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbCountry, null);
            this.lbCountry.Location = new System.Drawing.Point(3, 0);
            this.lbCountry.Name = "lbCountry";
            this.lbCountry.Size = new System.Drawing.Size(135, 26);
            this.lbCountry.TabIndex = 1;
            this.lbCountry.Text = "国家";
            this.lbCountry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbCountry
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbCountry, null);
            this.cbCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(144, 3);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(194, 20);
            this.cbCountry.TabIndex = 4;
            // 
            // cbCity
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbCity, null);
            this.cbCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(144, 29);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(194, 20);
            this.cbCity.TabIndex = 6;
            // 
            // lbDistrict
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbDistrict, null);
            this.lbDistrict.Location = new System.Drawing.Point(344, 26);
            this.lbDistrict.Name = "lbDistrict";
            this.lbDistrict.Size = new System.Drawing.Size(135, 28);
            this.lbDistrict.TabIndex = 3;
            this.lbDistrict.Text = "区县";
            this.lbDistrict.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbProvince
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbProvince, null);
            this.lbProvince.Location = new System.Drawing.Point(344, 0);
            this.lbProvince.Name = "lbProvince";
            this.lbProvince.Size = new System.Drawing.Size(135, 26);
            this.lbProvince.TabIndex = 1;
            this.lbProvince.Text = "省份";
            this.lbProvince.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDistrict
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbDistrict, null);
            this.cbDistrict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDistrict.FormattingEnabled = true;
            this.cbDistrict.Location = new System.Drawing.Point(485, 29);
            this.cbDistrict.Name = "cbDistrict";
            this.cbDistrict.Size = new System.Drawing.Size(213, 20);
            this.cbDistrict.TabIndex = 7;
            // 
            // cbProvince
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbProvince, null);
            this.cbProvince.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProvince.FormattingEnabled = true;
            this.cbProvince.Location = new System.Drawing.Point(485, 3);
            this.cbProvince.Name = "cbProvince";
            this.cbProvince.Size = new System.Drawing.Size(213, 20);
            this.cbProvince.TabIndex = 5;
            // 
            // myDataBindingProvider1
            // 
            this.myDataBindingProvider1.XmlConfigFile = null;
            // 
            // tableLayoutPanel5
            // 
            this.myTableLayoutPanelProvider1.SetAutoLayoutPanel(this.tableLayoutPanel5, true);
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel5.Controls.Add(this.lbDistrict, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.cbDistrict, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbProvince, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbCity, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.cbProvince, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbCountry, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbCity, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbCountry, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTableLayoutPanelProvider1.SetIsCaption(this.tableLayoutPanel5, false);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(701, 55);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // ucComboxRegionHEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel5);
            this.Name = "ucComboxRegionHEx";
            this.Size = new System.Drawing.Size(701, 55);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        #endregion
    }

}
