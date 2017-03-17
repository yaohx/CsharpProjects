
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
    public partial class ucComboxRegionEx : ucComboxRegionBase
    {
        public ucComboxRegionEx()
        {
            InitializeComponent();

            this.Load += new EventHandler(UserControl_Load);            
        }

        #region private components define...

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbCity = new System.Windows.Forms.Label();
            this.lbProvince = new System.Windows.Forms.Label();
            this.lbCountry = new System.Windows.Forms.Label();
            this.lbDistrict = new System.Windows.Forms.Label();
            this.cbDistrict = new System.Windows.Forms.ComboBox();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.cbProvince = new System.Windows.Forms.ComboBox();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.myTableLayoutPanelProvider1 = new MB.WinBase.Design.MyTableLayoutPanelProvider(this.components);
            this.myDataBindingProvider1 = new MB.WinBase.Binding.MyDataBindingProvider(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCity
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbCity, null);
            this.lbCity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCity.Location = new System.Drawing.Point(3, 26);
            this.lbCity.Name = "lbCity";
            this.lbCity.Size = new System.Drawing.Size(135, 26);
            this.lbCity.TabIndex = 2;
            this.lbCity.Text = "城市";
            this.lbCity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbProvince
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbProvince, null);
            this.lbProvince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProvince.Location = new System.Drawing.Point(3, 52);
            this.lbProvince.Name = "lbProvince";
            this.lbProvince.Size = new System.Drawing.Size(135, 26);
            this.lbProvince.TabIndex = 1;
            this.lbProvince.Text = "省份";
            this.lbProvince.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCountry
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbCountry, null);
            this.lbCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCountry.Location = new System.Drawing.Point(3, 0);
            this.lbCountry.Name = "lbCountry";
            this.lbCountry.Size = new System.Drawing.Size(135, 26);
            this.lbCountry.TabIndex = 1;
            this.lbCountry.Text = "国家";
            this.lbCountry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDistrict
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.lbDistrict, null);
            this.lbDistrict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDistrict.Location = new System.Drawing.Point(3, 78);
            this.lbDistrict.Name = "lbDistrict";
            this.lbDistrict.Size = new System.Drawing.Size(135, 29);
            this.lbDistrict.TabIndex = 3;
            this.lbDistrict.Text = "区县";
            this.lbDistrict.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDistrict
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbDistrict, null);
            this.cbDistrict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDistrict.FormattingEnabled = true;
            this.cbDistrict.Location = new System.Drawing.Point(3, 81);
            this.cbDistrict.Name = "cbDistrict";
            this.cbDistrict.Size = new System.Drawing.Size(179, 20);
            this.cbDistrict.TabIndex = 7;
            // 
            // cbCity
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbCity, null);
            this.cbCity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(3, 55);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(179, 20);
            this.cbCity.TabIndex = 6;
            // 
            // cbProvince
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbProvince, null);
            this.cbProvince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbProvince.FormattingEnabled = true;
            this.cbProvince.Location = new System.Drawing.Point(3, 29);
            this.cbProvince.Name = "cbProvince";
            this.cbProvince.Size = new System.Drawing.Size(179, 20);
            this.cbProvince.TabIndex = 5;
            // 
            // cbCountry
            // 
            this.myDataBindingProvider1.SetAdvanceDataBinding(this.cbCountry, null);
            this.cbCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(3, 3);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(179, 20);
            this.cbCountry.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.myTableLayoutPanelProvider1.SetAutoLayoutPanel(this.tableLayoutPanel1, true);
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.lbCountry, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbCity, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbProvince, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbDistrict, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTableLayoutPanelProvider1.SetIsCaption(this.tableLayoutPanel1, false);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(132, 107);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.myTableLayoutPanelProvider1.SetAutoLayoutPanel(this.tableLayoutPanel2, true);
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.Controls.Add(this.cbCountry, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbProvince, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbCity, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbDistrict, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTableLayoutPanelProvider1.SetIsCaption(this.tableLayoutPanel2, false);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(185, 107);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // myDataBindingProvider1
            // 
            this.myDataBindingProvider1.XmlConfigFile = null;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(321, 107);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 1;
            // 
            // ucComboxRegionEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucComboxRegionEx";
            this.Size = new System.Drawing.Size(321, 107);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private SplitContainer splitContainer1;
        #endregion

    }
}
