//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-03
// Description	:	XCommands 用户UI 操作的命令集合描述。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms;

namespace DIYReport.UserDIY
{
	/// <summary>
	/// command 定义。
	/// </summary>
	public class CommandGroups {

		public static readonly CommandID[]

			AlignCommands = { StandardCommands.AlignLeft, 
								StandardCommands.AlignTop,
								StandardCommands.AlignRight,
								StandardCommands.AlignBottom,
								StandardCommands.SizeToControlWidth,
								StandardCommands.SizeToControlHeight
							},

			DockCommands =  { UICommands.DockToLeft,
						      UICommands.DockToTop
						     },

			FileCommands =  {UICommands.NewReport,
							   UICommands.OpenFile,
							   UICommands.SaveFile,
							   UICommands.SaveFileAs,
						       UICommands.PageSetup,
							   UICommands.Preview,
							   UICommands.Print,
							   UICommands.SortAndGroup,
							   UICommands.Exit},

 

			EditCommands = {StandardCommands.Undo,
							   StandardCommands.Redo,
							   StandardCommands.Cut,
								StandardCommands.Copy,
							   StandardCommands.Paste,
							   StandardCommands.Delete,
							   StandardCommands.SelectAll,
							   UICommands.ShowProperty
							   
							   
						   },

			UIMainCommands;

		static CommandGroups() {
			ArrayList commands = MergeLists(DockCommands, FileCommands, EditCommands);

			UIMainCommands = commands.ToArray(typeof(CommandID)) as CommandID[]; 
		}
		

		static ArrayList MergeLists(params IList[] lists) {
			ArrayList baseList = new ArrayList();
			foreach(IList list in lists) baseList.AddRange(list);
			return baseList;
		}
	}
	/// <summary>
	/// 字体颜色格式化相关
	/// </summary>
	public class FormattingCommands { 
		public static readonly Guid EnvCommandSet = new Guid("{0E3843CC-9C61-442c-A76C-4F60BC5706CC}");
		public static byte[] FontSizeSet = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

		private const int cmdidFontName = 18;
		private const int cmdidFontSize = 19;
		private const int cmdidBold = 52;
		private const int cmdidItalic = 70; 
		private const int cmdidUnderline = 77;
		private const int cmdidJustifyCenter = 71;
		private const int cmdidJustifyLeft = 73;
		private const int cmdidJustifyRight = 74;
		private const int cmdidJustifyJustify = 72;
		private const int cmdidForeColor = 69;
		private const int cmdidBackColor = 51;

		public static readonly CommandID FontName = new CommandID(EnvCommandSet, cmdidFontName);
		public static readonly CommandID FontSize = new CommandID(EnvCommandSet, cmdidFontSize);
		public static readonly CommandID Bold = new CommandID(EnvCommandSet, cmdidBold);
		public static readonly CommandID Italic = new CommandID(EnvCommandSet, cmdidItalic);
		public static readonly CommandID Underline = new CommandID(EnvCommandSet, cmdidUnderline);
		public static readonly CommandID JustifyCenter = new CommandID(EnvCommandSet, cmdidJustifyCenter);
		public static readonly CommandID JustifyLeft = new CommandID(EnvCommandSet, cmdidJustifyLeft);
		public static readonly CommandID JustifyRight = new CommandID(EnvCommandSet, cmdidJustifyRight);
		public static readonly CommandID JustifyJustify = new CommandID(EnvCommandSet, cmdidJustifyJustify);
		public static readonly CommandID ForeColor = new CommandID(EnvCommandSet, cmdidForeColor);
		public static readonly CommandID BackColor = new CommandID(EnvCommandSet, cmdidBackColor);
	}

	#region 用户 主要UI 操作Command...
	/// <summary>
	/// 用户 主要UI 操作Command.
	/// </summary>
	public class UICommands { 
		private const int cmdidNewReport = 1;
		private const int cmdidOpenFile = 2;
		private const int cmdidSaveFile = 3; 
		private const int cmdidSaveFileAs = 4;
		private const int cmdidPrint = 5;
		private const int cmdidPreview = 6;
		private const int cmdidSortAndGroup = 7;//PageSetup
		private const int cmdidPageSetup = 8;//
		private const int cmdidShowProperty = 9;
		private const int cmdidShowArrowForm = 10;
		private const int cmdidDockToTop = 11;
		private const int cmdidDockToLeft = 12;
		private const int cmdidExit = 13;
//		private const int cmdidUndo = 14;
//		private const int cmdidRedo = 15;
		private const int cmdidControlHandle = 16;
		private const int cmdidSetObjProperty = 17;
		private const int cmdidOutPut = 18;
		private const int cmdidImport = 19;


		 

		private static readonly Guid UICommandSet = new Guid("{70020645-4804-4029-8F51-565FCA2271F7}");
		public static readonly CommandID NewReport = new CommandID(UICommandSet, cmdidNewReport);
		public static readonly CommandID OpenFile = new CommandID(UICommandSet, cmdidOpenFile);
		public static readonly CommandID SaveFile = new CommandID(UICommandSet, cmdidSaveFile);
		public static readonly CommandID SaveFileAs = new CommandID(UICommandSet, cmdidSaveFileAs);
		public static readonly CommandID Print = new CommandID(UICommandSet, cmdidPrint);
		public static readonly CommandID Preview = new CommandID(UICommandSet, cmdidPreview);
		public static readonly CommandID SortAndGroup = new CommandID(UICommandSet, cmdidSortAndGroup);
		public static readonly CommandID PageSetup = new CommandID(UICommandSet, cmdidPageSetup);
		public static readonly CommandID ShowProperty = new CommandID(UICommandSet, cmdidShowProperty);
		public static readonly CommandID ShowArrowForm = new CommandID(UICommandSet, cmdidShowArrowForm);
		public static readonly CommandID DockToTop = new CommandID(UICommandSet, cmdidDockToTop);
		public static readonly CommandID Exit = new CommandID(UICommandSet, cmdidExit);
//		public static readonly CommandID Undo = new CommandID(UICommandSet, cmdidUndo);
//		public static readonly CommandID Redo = new CommandID(UICommandSet, cmdidRedo);
		public static readonly CommandID DockToLeft = new CommandID(UICommandSet, cmdidDockToLeft);
		public static readonly CommandID ControlHandle = new CommandID(UICommandSet, cmdidControlHandle);
		public static readonly CommandID SetObjProperty = new CommandID(UICommandSet, cmdidSetObjProperty);
		
		public static readonly CommandID Output = new CommandID(UICommandSet,cmdidOutPut);
		public static readonly CommandID Import = new CommandID(UICommandSet,cmdidImport);

	}
	#endregion 用户 主要UI 操作Command...
	/// <summary>
	/// 报表设计操作对象相关。
	/// </summary>
	public class RptDesignCommands { 
		private const int cmdidInsTopMarginBand = 1;
		private const int cmdidInsBottomMarginBand = 2; 
		private const int cmdidInsReportHeaderBand = 3;
		private const int cmdidInsReportFooterBand = 4;
		private const int cmdidInsPageHeaderBand = 5;
		private const int cmdidInsPageFooterBand = 6;
		private const int cmdidInsGroupHeaderBand = 7;
		private const int cmdidInsGroupFooterBand = 8;
		private const int cmdidInsDetailBand = 9;

		private const int cmdidRptLabel = 10;
		private const int cmdidRptFieldText = 11;
		private const int cmdidRptFieldImage = 12;
		private const int cmdidRptCheckBox = 13;
		private const int cmdidRptPictureBox = 14;
		private const int cmdidRptSubReport = 15;
		private const int cmdidRptBarCode = 16;
		private const int cmdidRptOleObject = 17;
		private const int cmdidRptLine = 18;
		private const int cmdidRptFrame = 19;
		private const int cmdidRptExpressBox = 20;
		private const int cmdidRptHViewSpecFieldBox = 21;
		private const int cmdidRptRichTextBox = 22;


		private const int cmdidRptNone = 99;

		private static readonly Guid bandCommandSet = new Guid("{CF42A4B4-6869-4379-A514-C794B6232D18}");
		public static readonly CommandID InsertTopMarginBand = new CommandID(bandCommandSet, cmdidInsTopMarginBand);
		public static readonly CommandID InsertBottomMarginBand = new CommandID(bandCommandSet, cmdidInsBottomMarginBand);
		public static readonly CommandID InsertReportHeaderBand = new CommandID(bandCommandSet, cmdidInsReportHeaderBand);
		public static readonly CommandID InsertReportFooterBand = new CommandID(bandCommandSet, cmdidInsReportFooterBand);
		public static readonly CommandID InsertPageHeaderBand = new CommandID(bandCommandSet, cmdidInsPageHeaderBand);
		public static readonly CommandID InsertPageFooterBand = new CommandID(bandCommandSet, cmdidInsPageFooterBand);
		public static readonly CommandID InsertGroupHeaderBand = new CommandID(bandCommandSet, cmdidInsGroupHeaderBand);
		public static readonly CommandID InsertGroupFooterBand = new CommandID(bandCommandSet, cmdidInsGroupFooterBand);
		public static readonly CommandID InsertDetailBand = new CommandID(bandCommandSet, cmdidInsDetailBand);

		public static readonly CommandID RptLabel = new CommandID(bandCommandSet, cmdidRptLabel);
		public static readonly CommandID RptFieldText = new CommandID(bandCommandSet, cmdidRptFieldText);
		public static readonly CommandID RptFieldImage = new CommandID(bandCommandSet, cmdidRptFieldImage);
		public static readonly CommandID RptCheckBox = new CommandID(bandCommandSet, cmdidRptCheckBox);
		public static readonly CommandID RptPictureBox = new CommandID(bandCommandSet, cmdidRptPictureBox);
		public static readonly CommandID RptSubReport = new CommandID(bandCommandSet, cmdidRptSubReport);
		public static readonly CommandID RptBarCode = new CommandID(bandCommandSet, cmdidRptBarCode);
		public static readonly CommandID RptOleObject = new CommandID(bandCommandSet, cmdidRptOleObject);
		public static readonly CommandID RptLine = new CommandID(bandCommandSet, cmdidRptLine);
		public static readonly CommandID RptFrame = new CommandID(bandCommandSet, cmdidRptFrame);
		public static readonly CommandID RptExpressBox = new CommandID(bandCommandSet, cmdidRptExpressBox);
		public static readonly CommandID RptHViewSpecFieldBox = new CommandID(bandCommandSet, cmdidRptHViewSpecFieldBox);
		public static readonly CommandID RptRichTextBox = new CommandID(bandCommandSet, cmdidRptRichTextBox);
		public static readonly CommandID RptNone = new CommandID(bandCommandSet, cmdidRptNone);
	}
}
