//---------------------------------------------------------------- 
// Copyright 
// All rights reserved. 
// Author		:	Nick
// Create date	:	2005-10-17
// Description	:	扩展RichTextBox 控件，满足Word 文件编辑的需求。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;

namespace MB.WinBase.Ctls
{
	/// <summary>
	/// RichtextBox 控件的模板处理。
	/// </summary>
	public interface IRichTextTemplet{
		void Open(RichTextBoxEx richTextBox);
		void Save(RichTextBoxEx richTextBox);
	}

	#region StampActions
	public enum StampActions {
		EditedBy = 1,
		DateTime = 2,
		Custom = 4
	}
	#endregion

	/// <summary>
	/// 扩展RichTextBox 控件，满足Word 文件编辑的需求。
	/// </summary>
    [ToolboxItem(true)]
    public class RichTextBoxEx : System.Windows.Forms.UserControl, System.ComponentModel.INotifyPropertyChanged
    {
		#region Windows Generated
		private System.Windows.Forms.ToolBar tb1;
		private System.Windows.Forms.ToolBarButton tbbBold;
		private System.Windows.Forms.ToolBarButton tbbItalic;
		private System.Windows.Forms.ToolBarButton tbbUnderline;
		private System.Windows.Forms.ToolBarButton tbbCenter;
		private System.Windows.Forms.ToolBarButton tbbRight;
		private System.Windows.Forms.ToolBarButton tbbStrikeout;
		private System.Windows.Forms.ToolBarButton tbbColor;
		private System.Windows.Forms.ContextMenu cmColors;
		private System.Windows.Forms.MenuItem miBlack;
		private System.Windows.Forms.MenuItem miBlue;
		private System.Windows.Forms.MenuItem miRed;
		private System.Windows.Forms.MenuItem miGreen;
		private System.Windows.Forms.ToolBarButton tbbStamp;
		private System.Windows.Forms.ToolBarButton tbbOpen;
		private System.Windows.Forms.ToolBarButton tbbSave;
		private System.Windows.Forms.ToolBarButton tbbUndo;
		private System.Windows.Forms.ToolBarButton tbbRedo;
		private System.Windows.Forms.ToolBarButton tbbSeparator2;
		private System.Windows.Forms.ToolBarButton tbbSeparator3;
		private System.Windows.Forms.ToolBarButton tbbSeparator4;
		private System.Windows.Forms.ToolBarButton tbbSeparator1;
		private System.Windows.Forms.ToolBarButton tbbLeft;
		private System.Windows.Forms.OpenFileDialog ofd1;
		private System.Windows.Forms.SaveFileDialog sfd1;
		private System.Windows.Forms.ContextMenu cmFonts;
		private System.Windows.Forms.MenuItem miArial;
		private System.Windows.Forms.MenuItem miGaramond;
		private System.Windows.Forms.MenuItem miTahoma;
		private System.Windows.Forms.MenuItem miTimesNewRoman;
		private System.Windows.Forms.MenuItem miVerdana;
		private System.Windows.Forms.ToolBarButton tbbFont;
		private System.Windows.Forms.ToolBarButton tbbFontSize;
		private System.Windows.Forms.ToolBarButton tbbSeparator5;
		private System.Windows.Forms.MenuItem miCourierNew;
		private System.Windows.Forms.MenuItem miMicrosoftSansSerif;
		private System.Windows.Forms.ContextMenu cmFontSizes;
		private System.Windows.Forms.MenuItem mi8;
		private System.Windows.Forms.MenuItem mi9;
		private System.Windows.Forms.MenuItem mi10;
		private System.Windows.Forms.MenuItem mi11;
		private System.Windows.Forms.MenuItem mi12;
		private System.Windows.Forms.MenuItem mi14;
		private System.Windows.Forms.MenuItem mi16;
		private System.Windows.Forms.MenuItem mi18;
		private System.Windows.Forms.MenuItem mi20;
		private System.Windows.Forms.MenuItem mi22;
		private System.Windows.Forms.MenuItem mi24;
		private System.Windows.Forms.MenuItem mi26;
		private System.Windows.Forms.MenuItem mi28;
		private System.Windows.Forms.MenuItem mi36;
		private System.Windows.Forms.MenuItem mi48;
		private System.Windows.Forms.MenuItem mi72;
		private System.Windows.Forms.ToolBarButton tbbImage;
		private System.Windows.Forms.ToolBarButton tbbSheet;
		private System.Windows.Forms.ImageList imgList1;
        private MB.WinBase.Ctls.UIRuler uiRuler1;
        private MB.WinBase.Ctls.UIRuler uiRuler2;
		private System.Windows.Forms.RichTextBox rtb1;
		private System.ComponentModel.IContainer components;



		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextBoxEx));
            this.tb1 = new System.Windows.Forms.ToolBar();
            this.tbbSave = new System.Windows.Forms.ToolBarButton();
            this.tbbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator3 = new System.Windows.Forms.ToolBarButton();
            this.tbbFont = new System.Windows.Forms.ToolBarButton();
            this.cmFonts = new System.Windows.Forms.ContextMenu();
            this.miArial = new System.Windows.Forms.MenuItem();
            this.miCourierNew = new System.Windows.Forms.MenuItem();
            this.miGaramond = new System.Windows.Forms.MenuItem();
            this.miMicrosoftSansSerif = new System.Windows.Forms.MenuItem();
            this.miTahoma = new System.Windows.Forms.MenuItem();
            this.miTimesNewRoman = new System.Windows.Forms.MenuItem();
            this.miVerdana = new System.Windows.Forms.MenuItem();
            this.tbbFontSize = new System.Windows.Forms.ToolBarButton();
            this.cmFontSizes = new System.Windows.Forms.ContextMenu();
            this.mi8 = new System.Windows.Forms.MenuItem();
            this.mi9 = new System.Windows.Forms.MenuItem();
            this.mi10 = new System.Windows.Forms.MenuItem();
            this.mi11 = new System.Windows.Forms.MenuItem();
            this.mi12 = new System.Windows.Forms.MenuItem();
            this.mi14 = new System.Windows.Forms.MenuItem();
            this.mi16 = new System.Windows.Forms.MenuItem();
            this.mi18 = new System.Windows.Forms.MenuItem();
            this.mi20 = new System.Windows.Forms.MenuItem();
            this.mi22 = new System.Windows.Forms.MenuItem();
            this.mi24 = new System.Windows.Forms.MenuItem();
            this.mi26 = new System.Windows.Forms.MenuItem();
            this.mi28 = new System.Windows.Forms.MenuItem();
            this.mi36 = new System.Windows.Forms.MenuItem();
            this.mi48 = new System.Windows.Forms.MenuItem();
            this.mi72 = new System.Windows.Forms.MenuItem();
            this.tbbSeparator5 = new System.Windows.Forms.ToolBarButton();
            this.tbbBold = new System.Windows.Forms.ToolBarButton();
            this.tbbItalic = new System.Windows.Forms.ToolBarButton();
            this.tbbUnderline = new System.Windows.Forms.ToolBarButton();
            this.tbbStrikeout = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator1 = new System.Windows.Forms.ToolBarButton();
            this.tbbLeft = new System.Windows.Forms.ToolBarButton();
            this.tbbCenter = new System.Windows.Forms.ToolBarButton();
            this.tbbRight = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator2 = new System.Windows.Forms.ToolBarButton();
            this.tbbUndo = new System.Windows.Forms.ToolBarButton();
            this.tbbRedo = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator4 = new System.Windows.Forms.ToolBarButton();
            this.tbbStamp = new System.Windows.Forms.ToolBarButton();
            this.tbbColor = new System.Windows.Forms.ToolBarButton();
            this.cmColors = new System.Windows.Forms.ContextMenu();
            this.miBlack = new System.Windows.Forms.MenuItem();
            this.miBlue = new System.Windows.Forms.MenuItem();
            this.miRed = new System.Windows.Forms.MenuItem();
            this.miGreen = new System.Windows.Forms.MenuItem();
            this.tbbImage = new System.Windows.Forms.ToolBarButton();
            this.tbbSheet = new System.Windows.Forms.ToolBarButton();
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.sfd1 = new System.Windows.Forms.SaveFileDialog();
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.uiRuler2 = new MB.WinBase.Ctls.UIRuler();
            this.uiRuler1 = new MB.WinBase.Ctls.UIRuler();
            this.SuspendLayout();
            // 
            // tb1
            // 
            this.tb1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tb1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbSave,
            this.tbbOpen,
            this.tbbSeparator3,
            this.tbbFont,
            this.tbbFontSize,
            this.tbbSeparator5,
            this.tbbBold,
            this.tbbItalic,
            this.tbbUnderline,
            this.tbbStrikeout,
            this.tbbSeparator1,
            this.tbbLeft,
            this.tbbCenter,
            this.tbbRight,
            this.tbbSeparator2,
            this.tbbUndo,
            this.tbbRedo,
            this.tbbSeparator4,
            this.tbbStamp,
            this.tbbColor,
            this.tbbImage,
            this.tbbSheet});
            this.tb1.ButtonSize = new System.Drawing.Size(16, 16);
            this.tb1.Divider = false;
            this.tb1.DropDownArrows = true;
            this.tb1.ImageList = this.imgList1;
            this.tb1.Location = new System.Drawing.Point(0, 0);
            this.tb1.Name = "tb1";
            this.tb1.ShowToolTips = true;
            this.tb1.Size = new System.Drawing.Size(464, 70);
            this.tb1.TabIndex = 0;
            this.tb1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tb1_ButtonClick);
            // 
            // tbbSave
            // 
            this.tbbSave.ImageIndex = 11;
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.ToolTipText = "Save";
            // 
            // tbbOpen
            // 
            this.tbbOpen.ImageIndex = 10;
            this.tbbOpen.Name = "tbbOpen";
            this.tbbOpen.ToolTipText = "Open";
            // 
            // tbbSeparator3
            // 
            this.tbbSeparator3.Name = "tbbSeparator3";
            this.tbbSeparator3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbFont
            // 
            this.tbbFont.DropDownMenu = this.cmFonts;
            this.tbbFont.ImageIndex = 14;
            this.tbbFont.Name = "tbbFont";
            this.tbbFont.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tbbFont.ToolTipText = "Font";
            // 
            // cmFonts
            // 
            this.cmFonts.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miArial,
            this.miCourierNew,
            this.miGaramond,
            this.miMicrosoftSansSerif,
            this.miTahoma,
            this.miTimesNewRoman,
            this.miVerdana});
            // 
            // miArial
            // 
            this.miArial.Index = 0;
            this.miArial.Text = "Arial";
            this.miArial.Click += new System.EventHandler(this.Font_Click);
            // 
            // miCourierNew
            // 
            this.miCourierNew.Index = 1;
            this.miCourierNew.Text = "Courier New";
            this.miCourierNew.Click += new System.EventHandler(this.Font_Click);
            // 
            // miGaramond
            // 
            this.miGaramond.Index = 2;
            this.miGaramond.Text = "Garamond";
            this.miGaramond.Click += new System.EventHandler(this.Font_Click);
            // 
            // miMicrosoftSansSerif
            // 
            this.miMicrosoftSansSerif.Index = 3;
            this.miMicrosoftSansSerif.Text = "Microsoft Sans Serif";
            this.miMicrosoftSansSerif.Click += new System.EventHandler(this.Font_Click);
            // 
            // miTahoma
            // 
            this.miTahoma.Index = 4;
            this.miTahoma.Text = "Tahoma";
            this.miTahoma.Click += new System.EventHandler(this.Font_Click);
            // 
            // miTimesNewRoman
            // 
            this.miTimesNewRoman.Index = 5;
            this.miTimesNewRoman.Text = "Times New Roman";
            this.miTimesNewRoman.Click += new System.EventHandler(this.Font_Click);
            // 
            // miVerdana
            // 
            this.miVerdana.Index = 6;
            this.miVerdana.Text = "Verdana";
            this.miVerdana.Click += new System.EventHandler(this.Font_Click);
            // 
            // tbbFontSize
            // 
            this.tbbFontSize.DropDownMenu = this.cmFontSizes;
            this.tbbFontSize.ImageIndex = 15;
            this.tbbFontSize.Name = "tbbFontSize";
            this.tbbFontSize.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tbbFontSize.ToolTipText = "Font Size";
            // 
            // cmFontSizes
            // 
            this.cmFontSizes.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mi8,
            this.mi9,
            this.mi10,
            this.mi11,
            this.mi12,
            this.mi14,
            this.mi16,
            this.mi18,
            this.mi20,
            this.mi22,
            this.mi24,
            this.mi26,
            this.mi28,
            this.mi36,
            this.mi48,
            this.mi72});
            // 
            // mi8
            // 
            this.mi8.Index = 0;
            this.mi8.Text = "8";
            this.mi8.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi9
            // 
            this.mi9.Index = 1;
            this.mi9.Text = "9";
            this.mi9.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi10
            // 
            this.mi10.Index = 2;
            this.mi10.Text = "10";
            this.mi10.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi11
            // 
            this.mi11.Index = 3;
            this.mi11.Text = "11";
            this.mi11.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi12
            // 
            this.mi12.Index = 4;
            this.mi12.Text = "12";
            this.mi12.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi14
            // 
            this.mi14.Index = 5;
            this.mi14.Text = "14";
            this.mi14.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi16
            // 
            this.mi16.Index = 6;
            this.mi16.Text = "16";
            this.mi16.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi18
            // 
            this.mi18.Index = 7;
            this.mi18.Text = "18";
            this.mi18.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi20
            // 
            this.mi20.Index = 8;
            this.mi20.Text = "20";
            this.mi20.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi22
            // 
            this.mi22.Index = 9;
            this.mi22.Text = "22";
            this.mi22.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi24
            // 
            this.mi24.Index = 10;
            this.mi24.Text = "24";
            this.mi24.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi26
            // 
            this.mi26.Index = 11;
            this.mi26.Text = "26";
            this.mi26.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi28
            // 
            this.mi28.Index = 12;
            this.mi28.Text = "28";
            this.mi28.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi36
            // 
            this.mi36.Index = 13;
            this.mi36.Text = "36";
            this.mi36.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi48
            // 
            this.mi48.Index = 14;
            this.mi48.Text = "48";
            this.mi48.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi72
            // 
            this.mi72.Index = 15;
            this.mi72.Text = "72";
            this.mi72.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // tbbSeparator5
            // 
            this.tbbSeparator5.Name = "tbbSeparator5";
            this.tbbSeparator5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbBold
            // 
            this.tbbBold.ImageIndex = 0;
            this.tbbBold.Name = "tbbBold";
            this.tbbBold.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbBold.ToolTipText = "Bold";
            // 
            // tbbItalic
            // 
            this.tbbItalic.ImageIndex = 1;
            this.tbbItalic.Name = "tbbItalic";
            this.tbbItalic.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbItalic.ToolTipText = "Italic";
            // 
            // tbbUnderline
            // 
            this.tbbUnderline.ImageIndex = 2;
            this.tbbUnderline.Name = "tbbUnderline";
            this.tbbUnderline.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbUnderline.ToolTipText = "Underline";
            // 
            // tbbStrikeout
            // 
            this.tbbStrikeout.ImageIndex = 3;
            this.tbbStrikeout.Name = "tbbStrikeout";
            this.tbbStrikeout.ToolTipText = "Strikeout";
            // 
            // tbbSeparator1
            // 
            this.tbbSeparator1.Name = "tbbSeparator1";
            this.tbbSeparator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbLeft
            // 
            this.tbbLeft.ImageIndex = 4;
            this.tbbLeft.Name = "tbbLeft";
            this.tbbLeft.ToolTipText = "Left";
            // 
            // tbbCenter
            // 
            this.tbbCenter.ImageIndex = 5;
            this.tbbCenter.Name = "tbbCenter";
            this.tbbCenter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbCenter.ToolTipText = "Center";
            // 
            // tbbRight
            // 
            this.tbbRight.ImageIndex = 6;
            this.tbbRight.Name = "tbbRight";
            this.tbbRight.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbRight.ToolTipText = "Right";
            // 
            // tbbSeparator2
            // 
            this.tbbSeparator2.Name = "tbbSeparator2";
            this.tbbSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbUndo
            // 
            this.tbbUndo.ImageIndex = 12;
            this.tbbUndo.Name = "tbbUndo";
            this.tbbUndo.ToolTipText = "Undo";
            // 
            // tbbRedo
            // 
            this.tbbRedo.ImageIndex = 13;
            this.tbbRedo.Name = "tbbRedo";
            this.tbbRedo.ToolTipText = "Redo";
            // 
            // tbbSeparator4
            // 
            this.tbbSeparator4.Name = "tbbSeparator4";
            this.tbbSeparator4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbStamp
            // 
            this.tbbStamp.ImageIndex = 8;
            this.tbbStamp.Name = "tbbStamp";
            this.tbbStamp.ToolTipText = "Edit Stamp";
            // 
            // tbbColor
            // 
            this.tbbColor.DropDownMenu = this.cmColors;
            this.tbbColor.ImageIndex = 7;
            this.tbbColor.Name = "tbbColor";
            this.tbbColor.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tbbColor.ToolTipText = "Color";
            // 
            // cmColors
            // 
            this.cmColors.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miBlack,
            this.miBlue,
            this.miRed,
            this.miGreen});
            // 
            // miBlack
            // 
            this.miBlack.Index = 0;
            this.miBlack.Text = "Black";
            this.miBlack.Click += new System.EventHandler(this.Color_Click);
            // 
            // miBlue
            // 
            this.miBlue.Index = 1;
            this.miBlue.Text = "Blue";
            this.miBlue.Click += new System.EventHandler(this.Color_Click);
            // 
            // miRed
            // 
            this.miRed.Index = 2;
            this.miRed.Text = "Red";
            this.miRed.Click += new System.EventHandler(this.Color_Click);
            // 
            // miGreen
            // 
            this.miGreen.Index = 3;
            this.miGreen.Text = "Green";
            this.miGreen.Click += new System.EventHandler(this.Color_Click);
            // 
            // tbbImage
            // 
            this.tbbImage.ImageIndex = 17;
            this.tbbImage.Name = "tbbImage";
            this.tbbImage.ToolTipText = "image";
            // 
            // tbbSheet
            // 
            this.tbbSheet.ImageIndex = 18;
            this.tbbSheet.Name = "tbbSheet";
            this.tbbSheet.ToolTipText = "sheet";
            // 
            // imgList1
            // 
            this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
            this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList1.Images.SetKeyName(0, "");
            this.imgList1.Images.SetKeyName(1, "");
            this.imgList1.Images.SetKeyName(2, "");
            this.imgList1.Images.SetKeyName(3, "");
            this.imgList1.Images.SetKeyName(4, "");
            this.imgList1.Images.SetKeyName(5, "");
            this.imgList1.Images.SetKeyName(6, "");
            this.imgList1.Images.SetKeyName(7, "");
            this.imgList1.Images.SetKeyName(8, "");
            this.imgList1.Images.SetKeyName(9, "");
            this.imgList1.Images.SetKeyName(10, "");
            this.imgList1.Images.SetKeyName(11, "");
            this.imgList1.Images.SetKeyName(12, "");
            this.imgList1.Images.SetKeyName(13, "");
            this.imgList1.Images.SetKeyName(14, "");
            this.imgList1.Images.SetKeyName(15, "");
            this.imgList1.Images.SetKeyName(16, "");
            this.imgList1.Images.SetKeyName(17, "");
            this.imgList1.Images.SetKeyName(18, "");
            // 
            // ofd1
            // 
            this.ofd1.DefaultExt = "rtf";
            this.ofd1.Filter = "Rich Text Files|*.rtf|Plain Text File|*.txt";
            this.ofd1.Title = "Open File";
            // 
            // sfd1
            // 
            this.sfd1.DefaultExt = "rtf";
            this.sfd1.Filter = "Rich Text File|*.rtf|Plain Text File|*.txt";
            this.sfd1.Title = "Save As";
            // 
            // rtb1
            // 
            this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb1.Location = new System.Drawing.Point(24, 94);
            this.rtb1.Name = "rtb1";
            this.rtb1.Size = new System.Drawing.Size(440, 130);
            this.rtb1.TabIndex = 4;
            this.rtb1.Text = "";
            this.rtb1.TextChanged += new System.EventHandler(this.rtb1_TextChanged);
            // 
            // uiRuler2
            // 
            this.uiRuler2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRuler2.BackgroundImage")));
            this.uiRuler2.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiRuler2.IsHorizontal = false;
            this.uiRuler2.Location = new System.Drawing.Point(0, 94);
            this.uiRuler2.Name = "uiRuler2";
            this.uiRuler2.Size = new System.Drawing.Size(24, 130);
            this.uiRuler2.TabIndex = 3;
            this.uiRuler2.Visible = false;
            // 
            // uiRuler1
            // 
            this.uiRuler1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRuler1.BackgroundImage")));
            this.uiRuler1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiRuler1.IsHorizontal = true;
            this.uiRuler1.Location = new System.Drawing.Point(0, 70);
            this.uiRuler1.Name = "uiRuler1";
            this.uiRuler1.Size = new System.Drawing.Size(464, 24);
            this.uiRuler1.TabIndex = 2;
            this.uiRuler1.Visible = false;
            // 
            // RichTextBoxEx
            // 
            this.Controls.Add(this.rtb1);
            this.Controls.Add(this.uiRuler2);
            this.Controls.Add(this.uiRuler1);
            this.Controls.Add(this.tb1);
            this.Name = "RichTextBoxEx";
            this.Size = new System.Drawing.Size(464, 224);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region private declare ...
		/* RTF HEADER
		 * ----------
		 * 
		 * \rtf[N]  - For text to be considered to be RTF, it must be enclosed in this tag.
		 *      rtf1 is used because the RichTextBox conforms to RTF Specification
		 *      version 1.
		 * \ansi  - The character set.
		 * \ansicpg[N] - Specifies that unicode characters might be embedded. ansicpg1252
		 *      is the default used by Windows.
		 * \deff[N]  - The default font. \deff0 means the default font is the first font
		 *      found.
		 * \deflang[N] - The default language. \deflang1033 specifies US English.
		 * */
		private const string RTF_HEADER = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033";
		/* RTF DOCUMENT AREA
		 * -----------------
		 * 
		 * \viewkind[N] - The type of view or zoom level.  \viewkind4 specifies normal view.
		 * \uc[N]  - The number of bytes corresponding to a Unicode character.
		 * \pard  - Resets to default paragraph properties
		 * \cf[N]  - Foreground color.  \cf1 refers to the color at index 1 in
		 *      the color table
		 * \f[N]  - Font number. \f0 refers to the font at index 0 in the font
		 *      table.
		 * \fs[N]  - Font size in half-points.
		 * */
		private const string RTF_DOCUMENT_PRE = @"\viewkind4\uc1\pard\cf1\f0\fs20";
		private const string RTF_DOCUMENT_POST = @"\cf0\fs17}";
		private string RTF_IMAGE_POST = @"}";

		// The number of hundredths of millimeters (0.01 mm) in an inch
		// For more information, see GetImagePrefix() method.
		private const int HMM_PER_INCH = 2540;

		// The number of twips in an inch
		// For more information, see GetImagePrefix() method.
		private const int TWIPS_PER_INCH = 1440;

		// Ensures that the metafile maintains a 1:1 aspect ratio
		private const int MM_ISOTROPIC = 7;

		// Allows the x-coordinates and y-coordinates of the metafile to be adjusted
		// independently
		private const int MM_ANISOTROPIC = 8;
		// Represents an unknown font family
		private const string FF_UNKNOWN = "UNKNOWN";


		// The horizontal resolution at which the control is being displayed
		private float xDpi;

		// The vertical resolution at which the control is being displayed
		private float yDpi;

		// Dictionary that mapas Framework font families to RTF font families
		private HybridDictionary rtfFontFamily;
		// Dictionary that maps color enums to RTF color codes
		private HybridDictionary rtfColor;

		private bool _RuleSizeVisible = false;
		
		private static IRichTextTemplet _RichTextTemplet;
		#endregion private declare ...

		#region construct function ...
		public RichTextBoxEx() {
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//Update the graphics on the toolbar
			UpdateToolbar();

			// Initialize the dictionary mapping color codes to definitions
			rtfColor = new HybridDictionary();
			rtfColor.Add(RtfColor.Aqua, RtfColorDef.Aqua);
			rtfColor.Add(RtfColor.Black, RtfColorDef.Black);
			rtfColor.Add(RtfColor.Blue, RtfColorDef.Blue);
			rtfColor.Add(RtfColor.Fuchsia, RtfColorDef.Fuchsia);
			rtfColor.Add(RtfColor.Gray, RtfColorDef.Gray);
			rtfColor.Add(RtfColor.Green, RtfColorDef.Green);
			rtfColor.Add(RtfColor.Lime, RtfColorDef.Lime);
			rtfColor.Add(RtfColor.Maroon, RtfColorDef.Maroon);
			rtfColor.Add(RtfColor.Navy, RtfColorDef.Navy);
			rtfColor.Add(RtfColor.Olive, RtfColorDef.Olive);
			rtfColor.Add(RtfColor.Purple, RtfColorDef.Purple);
			rtfColor.Add(RtfColor.Red, RtfColorDef.Red);
			rtfColor.Add(RtfColor.Silver, RtfColorDef.Silver);
			rtfColor.Add(RtfColor.Teal, RtfColorDef.Teal);
			rtfColor.Add(RtfColor.White, RtfColorDef.White);
			rtfColor.Add(RtfColor.Yellow, RtfColorDef.Yellow);
			
			// Initialize the dictionary mapping default Framework font families to
			// RTF font families
			rtfFontFamily = new HybridDictionary();
			rtfFontFamily.Add(FontFamily.GenericMonospace.Name, RtfFontFamilyDef.Modern);
			rtfFontFamily.Add(FontFamily.GenericSansSerif, RtfFontFamilyDef.Swiss);
			rtfFontFamily.Add(FontFamily.GenericSerif, RtfFontFamilyDef.Roman);
			rtfFontFamily.Add(FF_UNKNOWN, RtfFontFamilyDef.Unknown);
			// Get the horizontal and vertical resolutions at which the object is
			// being displayed
			using(Graphics _graphics = this.CreateGraphics()) {
				xDpi = _graphics.DpiX;
				yDpi = _graphics.DpiY;
			}
		}
		#endregion construct function ...

		/// <summary>
		/// 模板处理。
		/// </summary>
		public static IRichTextTemplet RichTextTemplet{
			set{
				_RichTextTemplet = value;
			}
		}
		#region Extend Public function ...
		[Description("设置或者获取控件是否显示设置的尺寸。")] 
		public bool RuleSizeVisible{
			get{
				return _RuleSizeVisible;
			}
			set{
				_RuleSizeVisible = value;
				setRIleSizeV(value);
			}
		}
		private void setRIleSizeV(bool Visible){
			uiRuler1.Visible = Visible;
			uiRuler2.Visible = Visible;
		}
		/// <summary>
		/// 设置或者获取控件RTF格式的数据。
		/// </summary>
		[Description("设置或者获取控件RTF格式的数据。")] 
		public byte[] RtfContent{
			get{
                if (string.IsNullOrEmpty(rtb1.Rtf))
                    return null;
                else
                    return System.Text.UnicodeEncoding.Default.GetBytes(rtb1.Rtf);
				//return rtb1.Rtf;
			}
			set{
                if (value == null || value.Length == 0)
                    rtb1.Rtf = string.Empty;
                else
                    rtb1.Rtf = System.Text.UnicodeEncoding.Default.GetString(value);
			}
		}
		#endregion Extend Public function ...

		#region Stamp Event Stuff
		[Description("Occurs when the stamp button is clicked"), 
		Category("Behavior")]
		public event System.EventHandler Stamp;
        
		/// <summary>
		/// OnStamp event
		/// </summary>
		protected virtual void OnStamp(EventArgs e) {
			if(Stamp != null)
				Stamp(this, e);

			switch(StampAction) {
				case StampActions.EditedBy: {
					StringBuilder stamp = new StringBuilder(""); //holds our stamp text
					if(rtb1.Text.Length > 0) stamp.Append("\r\n\r\n"); //add two lines for space
					stamp.Append("Edited by "); 
					//use the CurrentPrincipal name if one exsist else use windows logon username
					if(Thread.CurrentPrincipal == null || Thread.CurrentPrincipal.Identity == null || Thread.CurrentPrincipal.Identity.Name.Length <= 0)
						stamp.Append(Environment.UserName);
					else
						stamp.Append(Thread.CurrentPrincipal.Identity.Name);
					stamp.Append(" on " + DateTime.Now.ToLongDateString() + "\r\n");
			
					rtb1.SelectionLength = 0; //unselect everything basicly
					rtb1.SelectionStart = rtb1.Text.Length; //start new selection at the end of the text
					rtb1.SelectionColor = this.StampColor; //make the selection blue
					rtb1.SelectionFont = new Font(rtb1.SelectionFont, FontStyle.Bold); //set the selection font and style
					rtb1.AppendText(stamp.ToString()); //add the stamp to the richtextbox
					rtb1.Focus(); //set focus back on the richtextbox
				} break; //end edited by stamp
				case StampActions.DateTime: {
					StringBuilder stamp = new StringBuilder(""); //holds our stamp text
					if(rtb1.Text.Length > 0) stamp.Append("\r\n\r\n"); //add two lines for space
					stamp.Append(DateTime.Now.ToLongDateString() + "\r\n");
					rtb1.SelectionLength = 0; //unselect everything basicly
					rtb1.SelectionStart = rtb1.Text.Length; //start new selection at the end of the text
					rtb1.SelectionColor = this.StampColor; //make the selection blue
					rtb1.SelectionFont = new Font(rtb1.SelectionFont, FontStyle.Bold); //set the selection font and style
					rtb1.AppendText(stamp.ToString()); //add the stamp to the richtextbox
					rtb1.Focus(); //set focus back on the richtextbox
				} break;
			} //end select
		}
		#endregion

		#region Toolbar button click
		/// <summary>
		///     Handler for the toolbar button click event
		/// </summary>
		private void tb1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//Switch based on the tooltip of the button pressed
			//OR: This could be changed to switch on the actual button pressed (e.g. e.Button and the case would be tbbBold)
			switch(e.Button.ToolTipText.ToLower()) {
				case "bold": 
					if(rtb1.SelectionFont != null) {
						//using bitwise Exclusive OR to flip-flop the value
						rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style ^ FontStyle.Bold);
					}
					break;
				case "italic": 
					if(rtb1.SelectionFont != null) {
						//using bitwise Exclusive OR to flip-flop the value
						rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style ^ FontStyle.Italic);
					}
					break;
				case "underline":
					if(rtb1.SelectionFont != null) {
						//using bitwise Exclusive OR to flip-flop the value
						rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style ^ FontStyle.Underline);
					}
					break;
				case "strikeout":
					if(rtb1.SelectionFont != null) {
						//using bitwise Exclusive OR to flip-flop the value
						rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style ^ FontStyle.Strikeout);
					}
					break;
				case "left":
					//change horizontal alignment to left
					rtb1.SelectionAlignment = HorizontalAlignment.Left;
					break;
				case "center":
					//change horizontal alignment to center
					rtb1.SelectionAlignment = HorizontalAlignment.Center;
					break;
				case "right":
					//change horizontal alignment to right
					rtb1.SelectionAlignment = HorizontalAlignment.Right;
					break;
				case "edit stamp":
					OnStamp(new EventArgs()); //send stamp event
					break;
				case "color":
					rtb1.SelectionColor = Color.Black;
                    
					break;
				case "undo":
					rtb1.Undo();
                   
					break;
				case "redo":
					rtb1.Redo();
                    
					break;
				case "open":
					if(_RichTextTemplet==null){
						try {
							if (ofd1.ShowDialog() == DialogResult.OK && ofd1.FileName.Length > 0)
								if(System.IO.Path.GetExtension(ofd1.FileName).ToLower().Equals(".rtf")) 
									rtb1.LoadFile(ofd1.FileName, RichTextBoxStreamType.RichText);
								else
									rtb1.LoadFile(ofd1.FileName, RichTextBoxStreamType.PlainText);
						}
						catch (ArgumentException ae) {
							if(ae.Message == "Invalid file format.")
								MessageBox.Show("There was an error loading the file: " + ofd1.FileName);				
						}
					}
					else{
						_RichTextTemplet.Open(this);
					}
					break;
				case "save":
					if(_RichTextTemplet==null){
						if(sfd1.ShowDialog() == DialogResult.OK && sfd1.FileName.Length > 0)
							if(System.IO.Path.GetExtension(sfd1.FileName).ToLower().Equals(".rtf"))
								rtb1.SaveFile(sfd1.FileName);
							else
								rtb1.SaveFile(sfd1.FileName, RichTextBoxStreamType.PlainText);
					}
					else{
						_RichTextTemplet.Save(this);
					}
					break;
				case "image":
					System.Windows.Forms.OpenFileDialog ofd2 = new OpenFileDialog();
					ofd2.InitialDirectory="D:\\";
					ofd2.Filter="图像文件(*.jpg;*.bmp;*.tif)|*.jpg;*.bmp;*.tif";
					if(ofd2.ShowDialog()==DialogResult.OK) {
						Image img = Image.FromFile(ofd2.FileName);
						//						this.rtb1.CreateGraphics();
						//
						//						Graphics GraphicsMyg = rtb1.CreateGraphics();
						//			
						//						GraphicsMyg.DrawImage(img,0,0);
						//						GraphicsMyg.ResetTransform();
						InsertImage(img);
                        
					}
					break;
				case "sheet":
					frmAddSheet frm = new frmAddSheet(rtb1);
					frm.ShowDialog();
                    
					break;
				default:
					break;
			} //end select
			UpdateToolbar(); //Update the toolbar buttons
          
		}
		#endregion

		#region Update Toolbar
		/// <summary>
		///     Update the toolbar button statuses
		/// </summary>
		public void UpdateToolbar() {
			//This is done incase 2 different fonts are selected at the same time
			//If that is the case there is no selection font so I use the default
			//font instead.
			Font fnt;
			if(rtb1.SelectionFont != null)
				fnt = rtb1.SelectionFont;
			else
				fnt = rtb1.Font;

			//Do all the toolbar button checks
			tbbBold.Pushed		= fnt.Bold; //bold button
			tbbItalic.Pushed	= fnt.Italic; //italic button
			tbbUnderline.Pushed	= fnt.Underline; //underline button
			tbbStrikeout.Pushed	= fnt.Strikeout; //strikeout button
			tbbLeft.Pushed		= (rtb1.SelectionAlignment == HorizontalAlignment.Left); //justify left
			tbbCenter.Pushed	= (rtb1.SelectionAlignment == HorizontalAlignment.Center); //justify center
			tbbRight.Pushed		= (rtb1.SelectionAlignment == HorizontalAlignment.Right); //justify right

			//Check the correct color
			foreach(MenuItem mi in cmColors.MenuItems) {
				mi.Checked = (rtb1.SelectionColor == Color.FromName(mi.Text));
			}

			//Check the correct font
			foreach(MenuItem mi in cmFonts.MenuItems) {
				mi.Checked = (fnt.FontFamily.Name == mi.Text);
			}

			//Check the correct font size
			foreach(MenuItem mi in cmFontSizes.MenuItems) {
				mi.Checked = ((int)fnt.SizeInPoints == float.Parse(mi.Text));
			}
		}
		#endregion

		#region Update Toolbar Seperators
		private void UpdateToolbarSeperators() {
			//Save & Open
			if(!tbbSave.Visible && !tbbOpen.Visible) 
				tbbSeparator3.Visible = false;
			else
				tbbSeparator3.Visible = true;

			//Font & Font Size
			if(!tbbFont.Visible && !tbbFontSize.Visible) 
				tbbSeparator5.Visible = false;
			else
				tbbSeparator5.Visible = true;

			//Bold, Italic, Underline, & Strikeout
			if(!tbbBold.Visible && !tbbItalic.Visible && !tbbUnderline.Visible && !tbbStrikeout.Visible)
				tbbSeparator1.Visible = false;
			else
				tbbSeparator1.Visible = true;

			//Left, Center, & Right
			if(!tbbLeft.Visible && !tbbCenter.Visible && !tbbRight.Visible)
				tbbSeparator2.Visible = false;
			else
				tbbSeparator2.Visible = true;

			//Undo & Redo
			if(!tbbUndo.Visible && !tbbRedo.Visible) 
				tbbSeparator4.Visible = false;
			else
				tbbSeparator4.Visible = true;
		}
		#endregion

		#region RichTextBox Selection Change
		/// <summary>
		///		Change the toolbar buttons when new text is selected
		/// </summary>
		private void rtb1_SelectionChanged(object sender, System.EventArgs e) {
			UpdateToolbar(); //Update the toolbar buttons
		}
		#endregion

		#region Color Click
		/// <summary>
		///     Change the richtextbox color
		/// </summary>
		private void Color_Click(object sender, System.EventArgs e) {
			if(rtb1.SelectionFont != null) {
				//set the richtextbox color based on the name of the menu item
				rtb1.SelectionColor = Color.FromName(((MenuItem)sender).Text);
			}
		}
		#endregion

		#region Font Click
		/// <summary>
		///     Change the richtextbox font
		/// </summary>
		private void Font_Click(object sender, System.EventArgs e) {
			if(rtb1.SelectionFont != null) {
				//set the richtextbox font family based on the name of the menu item
				rtb1.SelectionFont = new Font(((MenuItem)sender).Text, rtb1.SelectionFont.SizeInPoints);
			}
		}
		#endregion

		#region Font Size Click
		/// <summary>
		///     Change the richtextbox font size
		/// </summary>
		private void FontSize_Click(object sender, System.EventArgs e) {
			//set the richtextbox font size based on the name of the menu item
			rtb1.SelectionFont = new Font(rtb1.SelectionFont.FontFamily.Name, float.Parse(((MenuItem)sender).Text));
		}
		#endregion

		#region Link Clicked
		/// <summary>
		/// Starts the default browser if a link is clicked
		/// </summary>
		private void rtb1_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e) {
			System.Diagnostics.Process.Start(e.LinkText);
		}
		#endregion

		#region Public Properties
		/// <summary>
		///     The toolbar that is contained with-in the RichTextBoxExtened control
		/// </summary>
		[Description("The internal toolbar control"),
		Category("Internal Controls")]
		public ToolBar Toolbar {
			get { return tb1; }
		}

		/// <summary>
		///     The RichTextBox that is contained with-in the RichTextBoxExtened control
		/// </summary>
		[Description("The internal richtextbox control"),
		Category("Internal Controls")]
		public RichTextBox RichTextBox {
			get	{ return rtb1; }
		}

		/// <summary>
		///     Show the save button or not
		/// </summary>
		[Description("Show the save button or not"),
		Category("Appearance")]
		public Boolean ShowSave {
			get { return tbbSave.Visible; }
			set { tbbSave.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///    Show the open button or not 
		/// </summary>
		[Description("Show the open button or not"),
		Category("Appearance")]
		public Boolean ShowOpen {
			get { return tbbOpen.Visible; }
			set	{ tbbOpen.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the stamp button or not
		/// </summary>
		[Description("Show the stamp button or not"),
		Category("Appearance")]
		public Boolean ShowStamp {
			get { return tbbStamp.Visible; }
			set { tbbStamp.Visible = value; }
		}
		
		/// <summary>
		///     Show the color button or not
		/// </summary>
		[Description("Show the color button or not"),
		Category("Appearance")]
		public Boolean ShowColors {
			get { return tbbColor.Visible; }
			set { tbbColor.Visible = value; }
		}

		/// <summary>
		///     Show the undo button or not
		/// </summary>
		[Description("Show the undo button or not"),
		Category("Appearance")]
		public Boolean ShowUndo {
			get { return tbbUndo.Visible; }
			set { tbbUndo.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the redo button or not
		/// </summary>
		[Description("Show the redo button or not"),
		Category("Appearance")]
		public Boolean ShowRedo {
			get { return tbbRedo.Visible; }
			set { tbbRedo.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the bold button or not
		/// </summary>
		[Description("Show the bold button or not"),
		Category("Appearance")]
		public Boolean ShowBold {
			get { return tbbBold.Visible; }
			set { tbbBold.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the italic button or not
		/// </summary>
		[Description("Show the italic button or not"),
		Category("Appearance")]
		public Boolean ShowItalic {
			get { return tbbItalic.Visible; }
			set { tbbItalic.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the underline button or not
		/// </summary>
		[Description("Show the underline button or not"),
		Category("Appearance")]
		public Boolean ShowUnderline {
			get { return tbbUnderline.Visible; }
			set { tbbUnderline.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the strikeout button or not
		/// </summary>
		[Description("Show the strikeout button or not"),
		Category("Appearance")]
		public Boolean ShowStrikeout {
			get { return tbbStrikeout.Visible; }
			set { tbbStrikeout.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the left justify button or not
		/// </summary>
		[Description("Show the left justify button or not"),
		Category("Appearance")]
		public Boolean ShowLeftJustify {
			get { return tbbLeft.Visible; }
			set { tbbLeft.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the right justify button or not
		/// </summary>
		[Description("Show the right justify button or not"),
		Category("Appearance")]
		public Boolean ShowRightJustify {
			get { return tbbRight.Visible; }
			set { tbbRight.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the center justify button or not
		/// </summary>
		[Description("Show the center justify button or not"),
		Category("Appearance")]
		public Boolean ShowCenterJustify {
			get { return tbbCenter.Visible; }
			set { tbbCenter.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Determines how the stamp button will respond
		/// </summary>
		StampActions m_StampAction = StampActions.EditedBy;
		[Description("Determines how the stamp button will respond"),
		Category("Behavior")]
		public StampActions StampAction {
			get { return m_StampAction; }
			set { m_StampAction = value; }
		}
		
		/// <summary>
		///     Color of the stamp text
		/// </summary>
		Color m_StampColor = Color.Blue;

		[Description("Color of the stamp text"),
		Category("Appearance")]
		public Color StampColor {
			get { return m_StampColor; }
			set { m_StampColor = value; }
		}
			
		/// <summary>
		///     Show the font button or not
		/// </summary>
		[Description("Show the font button or not"),
		Category("Appearance")]
		public Boolean ShowFont {
			get { return tbbFont.Visible; }
			set { tbbFont.Visible = value; }
		}

		/// <summary>
		///     Show the font size button or not
		/// </summary>
		[Description("Show the font size button or not"),
		Category("Appearance")]
		public Boolean ShowFontSize {
			get { return tbbFontSize.Visible; }
			set { tbbFontSize.Visible = value; }
		}

		/// <summary>
		///     Detect URLs with-in the richtextbox
		/// </summary>
		[Description("Detect URLs with-in the richtextbox"),
		Category("Behavior")]
		public Boolean DetectURLs {
			get { return rtb1.DetectUrls; }
			set { rtb1.DetectUrls = value; }
		}
		#endregion

		#region Insert Image

		/// <summary>
		/// Inserts an image into the RichTextBox.  The image is wrapped in a Windows
		/// Format Metafile, because although Microsoft discourages the use of a WMF,
		/// the RichTextBox (and even MS Word), wraps an image in a WMF before inserting
		/// the image into a document.  The WMF is attached in HEX format (a string of
		/// HEX numbers).
		/// 
		/// The RTF Specification v1.6 says that you should be able to insert bitmaps,
		/// .jpegs, .gifs, .pngs, and Enhanced Metafiles (.emf) directly into an RTF
		/// document without the WMF wrapper. This works fine with MS Word,
		/// however, when you don't wrap images in a WMF, WordPad and
		/// RichTextBoxes simply ignore them.  Both use the riched20.dll or msfted.dll.
		/// </summary>
		/// <remarks>
		/// NOTE: The image is inserted wherever the caret is at the time of the call,
		/// and if any text is selected, that text is replaced.
		/// </remarks>
		/// <param name="_image"></param>
		public void InsertImage(Image _image) {

			StringBuilder _rtf = new StringBuilder();

			// Append the RTF header
			_rtf.Append(RTF_HEADER);

			// Create the font table using the RichTextBox's current font and append
			// it to the RTF string
			_rtf.Append(GetFontTable(this.Font));

			// Create the image control string and append it to the RTF string
			_rtf.Append(GetImagePrefix(_image));

			// Create the Windows Metafile and append its bytes in HEX format
			_rtf.Append(GetRtfImage(_image));

			// Close the RTF image control string
			_rtf.Append(RTF_IMAGE_POST);

			rtb1.SelectedRtf = _rtf.ToString();
		}

		/// <summary>
		/// Creates the RTF control string that describes the image being inserted.
		/// This description (in this case) specifies that the image is an
		/// MM_ANISOTROPIC metafile, meaning that both X and Y axes can be scaled
		/// independently.  The control string also gives the images current dimensions,
		/// and its target dimensions, so if you want to control the size of the
		/// image being inserted, this would be the place to do it. The prefix should
		/// have the form ...
		/// 
		/// {\pict\wmetafile8\picw[A]\pich[B]\picwgoal[C]\pichgoal[D]
		/// 
		/// where ...
		/// 
		/// A = current width of the metafile in hundredths of millimeters (0.01mm)
		///  = Image Width in Inches * Number of (0.01mm) per inch
		///  = (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 2540
		///  = (Image Width in Pixels / Graphics.DpiX) * 2540
		/// 
		/// B = current height of the metafile in hundredths of millimeters (0.01mm)
		///  = Image Height in Inches * Number of (0.01mm) per inch
		///  = (Image Height in Pixels / Graphics Context's Vertical Resolution) * 2540
		///  = (Image Height in Pixels / Graphics.DpiX) * 2540
		/// 
		/// C = target width of the metafile in twips
		///  = Image Width in Inches * Number of twips per inch
		///  = (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 1440
		///  = (Image Width in Pixels / Graphics.DpiX) * 1440
		/// 
		/// D = target height of the metafile in twips
		///  = Image Height in Inches * Number of twips per inch
		///  = (Image Height in Pixels / Graphics Context's Horizontal Resolution) * 1440
		///  = (Image Height in Pixels / Graphics.DpiX) * 1440
		/// 
		/// </summary>
		/// <remarks>
		/// The Graphics Context's resolution is simply the current resolution at which
		/// windows is being displayed.  Normally it's 96 dpi, but instead of assuming
		/// I just added the code.
		/// 
		/// According to Ken Howe at pbdr.com, "Twips are screen-independent units
		/// used to ensure that the placement and proportion of screen elements in
		/// your screen application are the same on all display systems."
		/// 
		/// Units Used
		/// ----------
		/// 1 Twip = 1/20 Point
		/// 1 Point = 1/72 Inch
		/// 1 Twip = 1/1440 Inch
		/// 
		/// 1 Inch = 2.54 cm
		/// 1 Inch = 25.4 mm
		/// 1 Inch = 2540 (0.01)mm
		/// </remarks>
		/// <param name="_image"></param>
		/// <returns></returns>
		private string GetImagePrefix(Image _image) {

			StringBuilder _rtf = new StringBuilder();

			// Calculate the current width of the image in (0.01)mm
			int picw = (int)Math.Round((_image.Width / xDpi) * HMM_PER_INCH);

			// Calculate the current height of the image in (0.01)mm
			int pich = (int)Math.Round((_image.Height / yDpi) * HMM_PER_INCH);

			// Calculate the target width of the image in twips
			int picwgoal = (int)Math.Round((_image.Width / xDpi) * TWIPS_PER_INCH);

			// Calculate the target height of the image in twips
			int pichgoal = (int)Math.Round((_image.Height / yDpi) * TWIPS_PER_INCH);

			// Append values to RTF string
			_rtf.Append(@"{\pict\wmetafile8");
			_rtf.Append(@"\picw");
			_rtf.Append(picw);
			_rtf.Append(@"\pich");
			_rtf.Append(pich);
			_rtf.Append(@"\picwgoal");
			_rtf.Append(picwgoal);
			_rtf.Append(@"\pichgoal");
			_rtf.Append(pichgoal);
			_rtf.Append(" ");

			return _rtf.ToString();
		}

		/// <summary>
		/// Use the EmfToWmfBits function in the GDI+ specification to convert a 
		/// Enhanced Metafile to a Windows Metafile
		/// </summary>
		/// <param name="_hEmf">
		/// A handle to the Enhanced Metafile to be converted
		/// </param>
		/// <param name="_bufferSize">
		/// The size of the buffer used to store the Windows Metafile bits returned
		/// </param>
		/// <param name="_buffer">
		/// An array of bytes used to hold the Windows Metafile bits returned
		/// </param>
		/// <param name="_mappingMode">
		/// The mapping mode of the image.  This control uses MM_ANISOTROPIC.
		/// </param>
		/// <param name="_flags">
		/// Flags used to specify the format of the Windows Metafile returned
		/// </param>
		[DllImportAttribute("gdiplus.dll")]
		private static extern uint GdipEmfToWmfBits (IntPtr _hEmf, uint _bufferSize,
			byte[] _buffer, int _mappingMode, EmfToWmfBitsFlags _flags);


		/// <summary>
		/// Wraps the image in an Enhanced Metafile by drawing the image onto the
		/// graphics context, then converts the Enhanced Metafile to a Windows
		/// Metafile, and finally appends the bits of the Windows Metafile in HEX
		/// to a string and returns the string.
		/// </summary>
		/// <param name="_image"></param>
		/// <returns>
		/// A string containing the bits of a Windows Metafile in HEX
		/// </returns>
		private string GetRtfImage(Image _image) {

			StringBuilder _rtf = null;

			// Used to store the enhanced metafile
			MemoryStream _stream = null;

			// Used to create the metafile and draw the image
			Graphics _graphics = null;

			// The enhanced metafile
			Metafile _metaFile = null;

			// Handle to the device context used to create the metafile
			IntPtr _hdc;

			try {
				_rtf = new StringBuilder();
				_stream = new MemoryStream();

				// Get a graphics context from the RichTextBox
				using(_graphics = this.CreateGraphics()) {

					// Get the device context from the graphics context
					_hdc = _graphics.GetHdc();

					// Create a new Enhanced Metafile from the device context
					_metaFile = new Metafile(_stream, _hdc);

					// Release the device context
					_graphics.ReleaseHdc(_hdc);
				}

				// Get a graphics context from the Enhanced Metafile
				using(_graphics = Graphics.FromImage(_metaFile)) {

					// Draw the image on the Enhanced Metafile
					_graphics.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height));

				}

				// Get the handle of the Enhanced Metafile
				IntPtr _hEmf = _metaFile.GetHenhmetafile();

				// A call to EmfToWmfBits with a null buffer return the size of the
				// buffer need to store the WMF bits.  Use this to get the buffer
				// size.
				uint _bufferSize = GdipEmfToWmfBits(_hEmf, 0, null, MM_ANISOTROPIC,
					EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);

				// Create an array to hold the bits
				byte[] _buffer = new byte[_bufferSize];

				// A call to EmfToWmfBits with a valid buffer copies the bits into the
				// buffer an returns the number of bits in the WMF.  
				uint _convertedSize = GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC,
					EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);

				// Append the bits to the RTF string
				for(int i = 0; i < _buffer.Length; ++i) {
					_rtf.Append(String.Format("{0:X2}", _buffer[i]));
				}

				return _rtf.ToString();
			}
			finally {
				if(_graphics != null)
					_graphics.Dispose();
				if(_metaFile != null)
					_metaFile.Dispose();
				if(_stream != null)
					_stream.Close();
			}
		}
  
		#endregion

		#region RTF Helpers

		/// <summary>
		/// Creates a font table from a font object.  When an Insert or Append 
		/// operation is performed a font is either specified or the default font
		/// is used.  In any case, on any Insert or Append, only one font is used,
		/// thus the font table will always contain a single font.  The font table
		/// should have the form ...
		/// 
		/// {\fonttbl{\f0\[FAMILY]\fcharset0 [FONT_NAME];}
		/// </summary>
		/// <param name="_font"></param>
		/// <returns></returns>
		private string GetFontTable(Font _font) {

			StringBuilder _fontTable = new StringBuilder();

			// Append table control string
			_fontTable.Append(@"{\fonttbl{\f0");
			_fontTable.Append(@"\");
   
			// If the font's family corresponds to an RTF family, append the
			// RTF family name, else, append the RTF for unknown font family.
			if (rtfFontFamily.Contains(_font.FontFamily.Name))
				_fontTable.Append(rtfFontFamily[_font.FontFamily.Name]);
			else
				_fontTable.Append(rtfFontFamily[FF_UNKNOWN]);

			// \fcharset specifies the character set of a font in the font table.
			// 0 is for ANSI.
			_fontTable.Append(@"\fcharset0 ");

			// Append the name of the font
			_fontTable.Append(_font.Name);

			// Close control string
			_fontTable.Append(@";}}");

			return _fontTable.ToString();
		}

		/// <summary>
		/// Creates a font table from the RtfColor structure.  When an Insert or Append
		/// operation is performed, _textColor and _backColor are either specified
		/// or the default is used.  In any case, on any Insert or Append, only three
		/// colors are used.  The default color of the RichTextBox (signified by a
		/// semicolon (;) without a definition), is always the first color (index 0) in
		/// the color table.  The second color is always the text color, and the third
		/// is always the highlight color (color behind the text).  The color table
		/// should have the form ...
		/// 
		/// {\colortbl ;[TEXT_COLOR];[HIGHLIGHT_COLOR];}
		/// 
		/// </summary>
		/// <param name="_textColor"></param>
		/// <param name="_backColor"></param>
		/// <returns></returns>
		private string GetColorTable(RtfColor _textColor, RtfColor _backColor) {

			StringBuilder _colorTable = new StringBuilder();

			// Append color table control string and default font (;)
			_colorTable.Append(@"{\colortbl ;");

			// Append the text color
			_colorTable.Append(rtfColor[_textColor]);
			_colorTable.Append(@";");

			// Append the highlight color
			_colorTable.Append(rtfColor[_backColor]);
			_colorTable.Append(@";}\n");
     
			return _colorTable.ToString();
		}

		/// <summary>
		/// Called by overrided RichTextBox.Rtf accessor.
		/// Removes the null character from the RTF.  This is residue from developing
		/// the control for a specific instant messaging protocol and can be ommitted.
		/// </summary>
		/// <param name="_originalRtf"></param>
		/// <returns>RTF without null character</returns>
		private string RemoveBadChars(string _originalRtf) {   
			return _originalRtf.Replace("\0", "");
		}

		#endregion

		#region My Enums and struct...

		// Specifies the flags/options for the unmanaged call to the GDI+ method
		// Metafile.EmfToWmfBits().
		private enum EmfToWmfBitsFlags {

			// Use the default conversion
			EmfToWmfBitsFlagsDefault = 0x00000000,

			// Embedded the source of the EMF metafiel within the resulting WMF
			// metafile
			EmfToWmfBitsFlagsEmbedEmf = 0x00000001,

			// Place a 22-byte header in the resulting WMF file.  The header is
			// required for the metafile to be considered placeable.
			EmfToWmfBitsFlagsIncludePlaceable = 0x00000002,

			// Don't simulate clipping by using the XOR operator.
			EmfToWmfBitsFlagsNoXORClip = 0x00000004
		};
		// Enum for possible RTF colors
		public enum RtfColor {
			Black, Maroon, Green, Olive, Navy, Purple, Teal, Gray, Silver,
			Red, Lime, Yellow, Blue, Fuchsia, Aqua, White
		}
		// Definitions for colors in an RTF document
		private struct RtfColorDef {
			public const string Black = @"\red0\green0\blue0";
			public const string Maroon = @"\red128\green0\blue0";
			public const string Green = @"\red0\green128\blue0";
			public const string Olive = @"\red128\green128\blue0";
			public const string Navy = @"\red0\green0\blue128";
			public const string Purple = @"\red128\green0\blue128";
			public const string Teal = @"\red0\green128\blue128";
			public const string Gray = @"\red128\green128\blue128";
			public const string Silver = @"\red192\green192\blue192";
			public const string Red = @"\red255\green0\blue0";
			public const string Lime = @"\red0\green255\blue0";
			public const string Yellow = @"\red255\green255\blue0";
			public const string Blue = @"\red0\green0\blue255";
			public const string Fuchsia = @"\red255\green0\blue255";
			public const string Aqua = @"\red0\green255\blue255";
			public const string White = @"\red255\green255\blue255";
		}

		// Control words for RTF font families
		private struct RtfFontFamilyDef {
			public const string Unknown = @"\fnil";
			public const string Roman = @"\froman";
			public const string Swiss = @"\fswiss";
			public const string Modern = @"\fmodern";
			public const string Script = @"\fscript";
			public const string Decor = @"\fdecor";
			public const string Technical = @"\ftech";
			public const string BiDirect = @"\fbidi";
		}
		#endregion My Enums and struct...


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged 成员

        private void rtb1_TextChanged(object sender, EventArgs e) {
            RaisePropertyChanged("RtfContent");
        }

    } //end class
} //end namespace
