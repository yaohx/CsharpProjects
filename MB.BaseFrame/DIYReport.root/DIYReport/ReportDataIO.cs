//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-22
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Xml ;
using System.Text ;
using System.Drawing ;
using System.Diagnostics ;
using System.Drawing.Printing;

namespace DIYReport
{
	/// <summary>
	/// ReportDataIO 处理报表的删除、保存以及打开
	/// </summary>
	[Obsolete("过期的报表数据转换器，请使用新的转换器。")] 
	public class ReportDataIO : DIYReport.Interface.IReportDataIO  
	{
		#region 变量定义...
		private readonly string REPORT_FILE_PATH = System.AppDomain.CurrentDomain.BaseDirectory  + @"ReportFile"; 

		private const string REPORT_NODE="/RptReport";
//		private const string SECTION_NODE="/RptReport/RptSection";
//		private const string OBJECT_NODE="/RptReport/RptSection/RptObj";
		private string _CurrentProcessFilePath = "";
		private System.Guid _RptID;
		private string _ReportName = "";
		#endregion 变量定义...

		#region Public 方法...
		/// <summary>
		///  创建一个新的报表
		/// </summary>
		/// <returns></returns>
		public virtual DIYReport.ReportModel.RptReport NewReport(){
			DIYReport.ReportModel.RptReport report =  DIYReport.ReportModel.RptReport.NewReport();
			_RptID = report.ID;
			return report;
		}
		/// <summary>
		/// 创建一个新的报表
		/// </summary>
		/// <param name="pReportName"></param>
		/// <param name="pRptID"></param>
		/// <returns></returns>
		public virtual DIYReport.ReportModel.RptReport NewReport(string pReportName,System.Guid pRptID){
			DIYReport.ReportModel.RptReport report =  DIYReport.ReportModel.RptReport.NewReport(pReportName,pRptID);
			_RptID = report.ID;
			return report;
		}

		/// <summary>
		/// 打开保存在本地目录下的报表文件
		/// </summary>
		/// <returns></returns>
		public virtual DIYReport.ReportModel.RptReport Open(){
			System.Windows.Forms.OpenFileDialog fileDialog  = new System.Windows.Forms.OpenFileDialog();;
			fileDialog.Filter = "报表文件 (*.rpt)|*.rpt" ;
			fileDialog.FilterIndex = 1;
			fileDialog.RestoreDirectory = true ;
			if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

				string filePath = fileDialog.FileName; 
				return  OpenReport(filePath);
			}
			return null;
		}
		/// <summary>
		///  根据指定的报表文件打开一张报表
		/// </summary>
		/// <param name="pFullPath"></param>
		/// <returns></returns>
		public virtual DIYReport.ReportModel.RptReport OpenReport(string pFullPath){
			bool b = System.IO.File.Exists(pFullPath);
			if(b){
				try{
					XmlDocument doc = new XmlDocument();
					doc.Load( pFullPath );
					XmlNode node = doc.SelectSingleNode(REPORT_NODE);
					_CurrentProcessFilePath = System.IO.Path.GetDirectoryName(pFullPath);
					DIYReport.ReportModel.RptReport report = createReportData(node);
					
					report.RptFilePath = pFullPath;
					return report;
				}
				catch(Exception e){
					Debug.Assert(false,"打开报表出错！",e.Message);
				}
			}
			return null;
		}
		/// <summary>
		/// 保存报表。 (目前把它当做，报表的导出功能来使用)
		/// </summary>
		/// <param name="pReport"></param>
		/// <returns></returns>
		public  bool Save(DIYReport.ReportModel.RptReport pReport){
			System.Windows.Forms.SaveFileDialog fileDialog  = new System.Windows.Forms.SaveFileDialog();;
			fileDialog.Filter = "报表文件 (*.rpt)|*.rpt" ;
			fileDialog.FilterIndex = 1;
			fileDialog.RestoreDirectory = true ;
			if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

				string filePath = fileDialog.FileName; 
				_CurrentProcessFilePath = System.IO.Path.GetDirectoryName(filePath);
				if(filePath==null || filePath.Length == 0){
					return false;
				}
				bool b = System.IO.Directory.Exists( _CurrentProcessFilePath);
				if(!b){
					System.IO.Directory.CreateDirectory( _CurrentProcessFilePath);
				}
				pReport.RptFilePath = filePath;
				XmlDocument doc = new XmlDocument();
				try{
					doc.LoadXml(buildXMLString(pReport));
					doc.Save( filePath );
					System.Windows.Forms.MessageBox.Show("报表导出成功！","导出报表操作提示",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);  
					return true;
				}
				catch(Exception e){
					Debug.Assert(false,"报表存储出错！",e.Message);
					System.Windows.Forms.MessageBox.Show("报表导出失败！","导出报表操作提示",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);  
					return false;
				}
			}
			return false;
		}
		/// <summary>
		/// 把设计的报表存储在系统设置的目录下
		/// </summary>
		/// <param name="pReport"></param>
		/// <returns></returns>
		public virtual bool SaveReportAs(DIYReport.ReportModel.RptReport pReport,string pFullPath){
			return SaveReport(pReport,pFullPath);
		}
		public virtual bool SaveReport(DIYReport.ReportModel.RptReport pReport,string pFullPath){
			_CurrentProcessFilePath = System.IO.Path.GetDirectoryName(pFullPath);
			if(pFullPath==null || pFullPath==""){
				//_CurrentProcessFilePath = REPORT_FILE_PATH;
				return Save(pReport);
			}
			bool b = System.IO.Directory.Exists( _CurrentProcessFilePath);
			if(!b){
				System.IO.Directory.CreateDirectory( _CurrentProcessFilePath);
			}
			XmlDocument doc = new XmlDocument();
			try{
				doc.LoadXml(buildXMLString(pReport));
				doc.Save( pFullPath );
				return true;
			}
			catch(Exception e){
				Debug.Assert(false,"报表存储出错！",e.Message);
				return false;
			}
		}
		#endregion Public 方法...

		#region Build XML File...
		private string buildXMLString(DIYReport.ReportModel.RptReport pReport){
			StringBuilder xmlSB = new StringBuilder();
			//描述
			xmlSB.Append("<?xml version='1.0' encoding='utf-8' ?>");
			xmlSB.Append("<!-- 用户自定义智能报表中心生成的报表 -->");
			xmlSB.Append( reportString(pReport));
			DIYReport.ReportModel.RptSectionList sectionList = pReport.SectionList ;
			foreach(DIYReport.ReportModel.RptSection section in  sectionList){
				xmlSB.Append( sectionString(section));
				DIYReport.ReportModel.RptSingleObjList rptObjList = section.RptObjList ;
				foreach(DIYReport.Interface.IRptSingleObj   rptObj in rptObjList){
					xmlSB.Append(rptObjString(rptObj));
				}
				xmlSB.Append("</RptSection>");
			}
			xmlSB.Append("</RptReport>");
			return xmlSB.ToString();
 
		}

		private string reportString(DIYReport.ReportModel.RptReport pReport){
			StringBuilder xmlSB = new StringBuilder();
			xmlSB.Append("<RptReport ID = '" + pReport.ID.ToString() +"' Name='" + pReport.Name + "'");
			if(pReport.Text!=null && pReport.Text.Trim()!=""){
				xmlSB.Append(" Text ='"+ pReport.Text + "'");
			}
			//add by Nick 2005-01-27  
			xmlSB.Append(" GroupLayoutType ='"+ ((int)pReport.GroupLayoutType).ToString() + "'");
			xmlSB.Append(" DetailOrderString ='"+ pReport.DetailOrderString + "'");

			xmlSB.Append(" Width = '" + pReport.PaperSize.Width.ToString() +"'");
			xmlSB.Append(" Height = '" + pReport.PaperSize.Height.ToString() + "'");
			xmlSB.Append(" PaperName = '" + pReport.PaperSize.PaperName + "'"); 

			xmlSB.Append(" PrintName = '" + pReport.PrintName + "'"); 

			xmlSB.Append(" IsLandscape = '" + pReport.IsLandscape.ToString() + "'");

			xmlSB.Append(" Margins-Left='" + pReport.Margins.Left.ToString() + "'");
			xmlSB.Append(" Margins-Top ='" + pReport.Margins.Top.ToString() + "'");
			xmlSB.Append(" Margins-Right ='" + pReport.Margins.Right.ToString() + "'");
			xmlSB.Append(" Margins-Bottom ='" + pReport.Margins.Bottom.ToString() + "'");

			xmlSB.Append(" FillNULLRow = '" + pReport.FillNULLRow.ToString() +"'"); 
			xmlSB.Append(">");

			return xmlSB.ToString();

		}
		private string sectionString(DIYReport.ReportModel.RptSection pSection){
			StringBuilder xmlSB = new StringBuilder();
			xmlSB.Append("<RptSection Name = '" + pSection.Name + "'");
			xmlSB.Append(" Height ='" + pSection.Height.ToString() + "'");
			xmlSB.Append(" Width ='" + pSection.Width.ToString() + "'");
			xmlSB.Append(" SectionType = '" + ((int)pSection.SectionType).ToString() +"'");
			//add by nick 2005-01-27
			xmlSB.Append(" Index ='" + pSection.Index.ToString() + "'");
			if(pSection.GroupField!=null){
				xmlSB.Append(" GroupField_Name ='" + pSection.GroupField.FieldName + "'");
				xmlSB.Append(" GroupField_IsAscending ='" + pSection.GroupField.IsAscending.ToString() + "'");
				xmlSB.Append(" GroupField_OrderIndex ='" + pSection.GroupField.OrderIndex.ToString() + "'");
				xmlSB.Append(" GroupField_SetSort ='" + pSection.GroupField.SetSort.ToString()  + "'");
				xmlSB.Append(" GroupField_DivideName ='" + pSection.GroupField.DivideName  + "'");
			}
//			xmlSB.Append(" GroupFieldName ='" + pSection.GroupFieldName + "'");
//			xmlSB.Append(" OrderType ='" + pSection.OrderType + "'");
			xmlSB.Append(">");
			return xmlSB.ToString();
		}
		private string rptObjString(DIYReport.Interface.IRptSingleObj   pRptObj){
			StringBuilder xmlSB = new StringBuilder();
			xmlSB.Append("<RptObj Name = '" + pRptObj.Name +  "'");
			xmlSB.Append(" Type = '" + ((int)pRptObj.Type).ToString() + "'");
			xmlSB.Append(" Location-X='" +  pRptObj.Location.X.ToString() +"'");
			xmlSB.Append(" Location-Y='" + pRptObj.Location.Y.ToString() + "'");
			xmlSB.Append(" Size-Width='" + pRptObj.Size.Width.ToString() +"'"); 
			xmlSB.Append(" Size-Height='" + pRptObj.Size.Height.ToString() + "'");
			xmlSB.Append(" LinePound ='" + pRptObj.LinePound.ToString() +"'");
			xmlSB.Append(" LineStyle ='" + ((int)pRptObj.LineStyle).ToString() +"'");
			xmlSB.Append(" BackgroundColor ='" + pRptObj.BackgroundColor.ToArgb().ToString() +"'");
			xmlSB.Append(" ForeColor ='" + pRptObj.ForeColor.ToArgb().ToString() +"'"); 
			switch(pRptObj.Type){
				case DIYReport.ReportModel.RptObjType.Line:   
					DIYReport.ReportModel.RptObj.RptLine obj = pRptObj as  DIYReport.ReportModel.RptObj.RptLine;
					xmlSB.Append(" LineType ='" + ((int)obj.LineType).ToString() + "'"); 
					break;
				case DIYReport.ReportModel.RptObjType.Rect:
					break;
				case DIYReport.ReportModel.RptObjType.Text:
				case DIYReport.ReportModel.RptObjType.Express:
					DIYReport.Interface.IRptTextObj   txt = pRptObj as DIYReport.Interface.IRptTextObj;
					xmlSB.Append(" WordWrap ='" + txt.WordWrap.ToString() +"'"); 
					xmlSB.Append(" Alignment ='" + txt.Alignment.ToString() +"'");
					xmlSB.Append(" ShowFrame ='" + txt.ShowFrame.ToString() +"'");
					xmlSB.Append(" FormatStyle ='" + txt.FormatStyle + "'");

					xmlSB.Append(" Font-Bold='"+  txt.Font.Bold.ToString() +"'"); 
					xmlSB.Append(" Font-FamilyName='"+  txt.Font.FontFamily.Name +"'"); 
					xmlSB.Append(" Font-Underline='"+  txt.Font.Underline.ToString() +"'"); 
					xmlSB.Append(" Font-Italic='"+  txt.Font.Italic.ToString() +"'"); 
					xmlSB.Append(" Font-Strikeout='"+  txt.Font.Strikeout.ToString() +"'");
					xmlSB.Append(" Font-Size='"+  txt.Font.Size.ToString() +"'"); 

					if(txt.Type == DIYReport.ReportModel.RptObjType.Text){
						DIYReport.ReportModel.RptObj.RptLable lab = txt as DIYReport.ReportModel.RptObj.RptLable;
						if(lab.Text!=null && lab.Text.Trim()!=""){
							xmlSB.Append(" Text ='" + lab.Text + "'");
						}
					}
					else{
						DIYReport.ReportModel.RptObj.RptExpressBox box = txt as DIYReport.ReportModel.RptObj.RptExpressBox;
						if(box.DataSource!=null && box.DataSource.Trim()!=""){xmlSB.Append(" DataSource ='" + box.DataSource.Trim() + "'");}
						if(box.FieldName!=null && box.FieldName.Trim()!=""){xmlSB.Append(" FieldName ='" + box.FieldName.Trim() + "'");}
						xmlSB.Append(" ExpressType ='" + ((int)box.ExpressType).ToString() + "'");
					}
					break;
				case DIYReport.ReportModel.RptObjType.Image:
					DIYReport.ReportModel.RptObj.RptPictureBox pic = pRptObj as DIYReport.ReportModel.RptObj.RptPictureBox;
					//等待处理图片
					SaveRptImage(pic);
					break;
				default:
					break;
			}
			xmlSB.Append(">");
			//write end rptObj
			xmlSB.Append("</RptObj>");
			return xmlSB.ToString();
		}
		#endregion Build XML File...

		#region Image Process...
		//把报表中的图片信息存储到报表所在的下一级目录（以报表名为名称）下。
		public virtual void SaveRptImage(DIYReport.ReportModel.RptObj.RptPictureBox pImage){
			//pImage.Section.Report.Name 
			string imagePath = System.IO .Path.GetFileNameWithoutExtension(pImage.Section.Report.ID.ToString());
			string filePath =   _CurrentProcessFilePath + @"\" + imagePath + @"\";
			bool b = System.IO.Directory.Exists( filePath);
			if(!b){
				System.IO.Directory.CreateDirectory( filePath );
			}
			//string tt =pImage.Image..Name ;
			string fileName =   filePath + pImage.Name + ".jpg" ;

			bool fb = System.IO.File.Exists( fileName);
			if(fb){
			}
			try{
			if(pImage.Image!=null){
				pImage.Image.Save(fileName);
			}
			}
			catch{
				//throw e;
			}
		}
		//读报表中的图片信息
		public virtual void ReadRptImage(DIYReport.ReportModel.RptObj.RptPictureBox pImage){
			string imagePath = System.IO .Path.GetFileNameWithoutExtension(_RptID.ToString() );
			string filePath =   _CurrentProcessFilePath + @"\" +  imagePath + @"\" + pImage.Name + ".jpg" ;
			bool b = System.IO.File.Exists(filePath);
			if(b){
				Image im = System.Drawing.Image.FromFile(filePath);
				pImage.Image = im;
			}
		}
		#endregion Image Process...

		#region Read From Rpt XML File...
		//加载RptReport 的数据 
		private DIYReport.ReportModel.RptReport  createReportData(XmlNode pReportNode){
			DIYReport.ReportModel.RptReport report = new DIYReport.ReportModel.RptReport();
			report.Name = pReportNode.Attributes["Name"].Value;
			//存储当前处理的报表的名称
			//nick add 2005-11-28 为了处理导入导出而增加的。
			if(_RptID==Guid.Empty){
				if(pReportNode.Attributes["ID"]!=null){
					string id = pReportNode.Attributes["ID"].Value;
					_RptID = new System.Guid(id);
				}
			}
			report.ID = _RptID;


			_ReportName = report.Name;
			if(pReportNode.Attributes["Text"]!=null){
				report.Text = pReportNode.Attributes["Text"].Value ;
			}
			if(pReportNode.Attributes["DetailOrderString"]!=null){
				report.DetailOrderString = pReportNode.Attributes["DetailOrderString"].Value ;
			}
			if(pReportNode.Attributes["GroupLayoutType"]!=null){
				report.GroupLayoutType  = (DIYReport.ReportModel.GroupLayoutType)PublicFun.ToInt(pReportNode.Attributes["GroupLayoutType"].Value) ;
			}

			int width = PublicFun.ToInt( pReportNode.Attributes["Width"].Value);
			int height = PublicFun.ToInt( pReportNode.Attributes["Height"].Value);
			if(pReportNode.Attributes["PrintName"]!=null){
				report.PrintName = 	pReportNode.Attributes["PrintName"].Value;
			}
			//bool isCustom = false;
			if(pReportNode.Attributes["PaperName"]!=null){
				string pageName =  pReportNode.Attributes["PaperName"].Value;
				
				PaperSize size = DIYReport.Print.RptPageSetting.GetSizeByFullInfo(report.PrintDocument,report.PrintName,pageName,width,height);     //new PaperSize(pageName,width,height);

				if(size!=null){
					if(size.Kind == PaperKind.Custom){ 
						try{
							//pagersize 的大小是不能进行修改的。
							size.Width = width;
							size.Height = height;
						}
						catch{
						}

						//isCustom = true;
					
					}//MyCDC
				
					//				PaperSize size = DIYReport.Print.RptPageSetting.GetPaperSizeByName(report.PrintDocument,"MyCDC");     //new PaperSize(pageName,width,height);
					//				if(size.Kind == PaperKind.Custom){ 
					//					size.Width = 200;
					//					size.Height = 400;
					//				}//MyCDC
				
					report.PaperSize = size;
				}
			}
			//if(report.PrintName==null || report.PrintName.Length ==0)
			report.PrintName = report.PrintDocument.PrinterSettings.PrinterName ;
  
			report.IsLandscape = bool.Parse(pReportNode.Attributes["IsLandscape"].Value);
			report.FillNULLRow = bool.Parse(pReportNode.Attributes["FillNULLRow"].Value);

			int left = PublicFun.ToInt( pReportNode.Attributes["Margins-Left"].Value);
			int top = PublicFun.ToInt( pReportNode.Attributes["Margins-Top"].Value);
			int right = PublicFun.ToInt( pReportNode.Attributes["Margins-Right"].Value);
			int bottom = PublicFun.ToInt( pReportNode.Attributes["Margins-Bottom"].Value);
			Margins m = new Margins(left,right,top,bottom);
			report.Margins = m;
			PageSettings setting = new PageSettings();
			setting.Margins = m;
			setting.Landscape = report.IsLandscape;
			setting.PaperSize = report.PaperSize;

			report.PrintDocument.DefaultPageSettings = setting;
			//report.PrintDocument.PrinterSettings.DefaultPageSettings = setting;   
//			if(isCustom){
//				DIYReport.Print.MyPrinterSettings.ChangeSetting(report.PrintDocument,"Microsoft Office Document Image Writer",System.Convert.ToInt16(width),System.Convert.ToInt16(height));
//			}
			//report.PrintDocument.PrinterSettings.PrinterName =  setting.PrinterSettings.PrinterName;  
			//
			createSection(report,pReportNode.ChildNodes);
			return report;
		}

		//加载RptSection 的数据
		private void createSection(DIYReport.ReportModel.RptReport pReport,XmlNodeList pSectionList){
			foreach(XmlNode node in pSectionList){
				int sectionType = PublicFun.ToInt(node.Attributes["SectionType"].Value); 
				DIYReport.ReportModel.RptSection section = new DIYReport.ReportModel.RptSection((DIYReport.SectionType)sectionType);
				section.Name = node.Attributes["Name"].Value;
				section.Height = PublicFun.ToInt(node.Attributes["Height"].Value);
				section.Width = PublicFun.ToInt(node.Attributes["Width"].Value);
				//add by nick 2005-01-27
				if(node.Attributes["Index"]!=null){
					section.Index = PublicFun.ToInt(node.Attributes["Index"].Value);
				}
//				xmlSB.Append(" GroupField_Name ='" + pSection.GroupField.FieldName + "'");
//				xmlSB.Append(" GroupField_IsAscending ='" + pSection.GroupField.IsAscending.ToString() + "'");
//				xmlSB.Append(" GroupField_OrderIndex ='" + pSection.GroupField.OrderIndex.ToString() + "'");
//				xmlSB.Append(" GroupField_SetSort ='" + pSection.GroupField.SetSort.ToString()  + "'");
//				xmlSB.Append(" GroupField_DivideName ='" + pSection.GroupField.DivideName  + "'");

				if(node.Attributes["GroupField_Name"]!=null){
					string gName = node.Attributes["GroupField_Name"].Value;
					DIYReport.GroupAndSort.RptFieldInfo field = new DIYReport.GroupAndSort.RptFieldInfo(gName);
					if(node.Attributes["GroupField_IsAscending"]!=null){
						field.IsAscending  = bool.Parse(node.Attributes["GroupField_IsAscending"].Value);
					}
					if(node.Attributes["GroupField_OrderIndex"]!=null){
						field.OrderIndex  = int.Parse(node.Attributes["GroupField_OrderIndex"].Value);
					}
					if(node.Attributes["GroupField_SetSort"]!=null){
						field.SetSort  = bool.Parse(node.Attributes["GroupField_SetSort"].Value);
					}
					if(node.Attributes["GroupField_DivideName"]!=null){
						field.DivideName = node.Attributes["GroupField_DivideName"].Value;
					}
					section.GroupField = field;
				}

				pReport.SectionList.Add(section); 
				//增加Section 总的RptObject
				createRptObj(section,node.ChildNodes); 
				section.Report = pReport;
			}
		}
		private void createRptObj(DIYReport.ReportModel.RptSection pSection, XmlNodeList pSectionList){
			foreach(XmlNode node in pSectionList){
				DIYReport.ReportModel.RptSingleObj obj = new DIYReport.ReportModel.RptSingleObj();
				int objType = PublicFun.ToInt(node.Attributes["Type"].Value); 
				DIYReport.ReportModel.RptObjType type = (DIYReport.ReportModel.RptObjType)objType;
				string objName = node.Attributes["Name"].Value;
				DIYReport.Interface.IRptSingleObj singObj = RptObjectHelper.CreateObj(type); 
//				switch(type){
//					case DIYReport.ReportModel.RptObjType.Line:   
//						singObj = new DIYReport.ReportModel.RptObj.RptLine(objName);  
//						break;
//					case DIYReport.ReportModel.RptObjType.Rect:
//						singObj = new DIYReport.ReportModel.RptObj.RptRect(objName);
//						break;
//					case DIYReport.ReportModel.RptObjType.Text:
//						singObj = new DIYReport.ReportModel.RptObj.RptLable(objName);
//						break;
//					case DIYReport.ReportModel.RptObjType.Express:
//						singObj = new DIYReport.ReportModel.RptObj.RptExpressBox(objName);
//						break;
//					case DIYReport.ReportModel.RptObjType.Image:
//						singObj = new DIYReport.ReportModel.RptObj.RptPictureBox(objName);
//						break;
//					
//					default:
//						Debug.Assert("该对象类型" + type.ToString() + "目前还没有处理.");
//						break;
//				}
				fillInfoToRptObj(singObj,node);
				singObj.Section = pSection;
				//把控件增加到Section 中
				pSection.RptObjList.Add( singObj);
			}
		}
		private void fillInfoToRptObj(DIYReport.Interface.IRptSingleObj pRptObj,XmlNode pNode){
			int x = PublicFun.ToInt(pNode.Attributes["Location-X"].Value) ;
			int y = PublicFun.ToInt(pNode.Attributes["Location-Y"].Value) ;
			pRptObj.Location = new Point(x,y);
			int width = PublicFun.ToInt(pNode.Attributes["Size-Width"].Value) ;
			int height = PublicFun.ToInt(pNode.Attributes["Size-Height"].Value) ;
			pRptObj.Size = new Size(width,height); 
			pRptObj.LinePound =  PublicFun.ToInt(pNode.Attributes["LinePound"].Value);
			if(pNode.Attributes["LineStyle"]!=null){
				int lineStyle = PublicFun.ToInt(pNode.Attributes["LineStyle"].Value);
				pRptObj.LineStyle = (System.Drawing.Drawing2D.DashStyle)lineStyle;
			}
			else{
				pRptObj.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid ;
			}
			pRptObj.BackgroundColor = Color.FromArgb(PublicFun.ToInt(pNode.Attributes["BackgroundColor"].Value));
			pRptObj.ForeColor = Color.FromArgb(PublicFun.ToInt(pNode.Attributes["ForeColor"].Value));
			switch(pRptObj.Type){
				case DIYReport.ReportModel.RptObjType.Line:
					DIYReport.ReportModel.RptObj.RptLine line = pRptObj as  DIYReport.ReportModel.RptObj.RptLine;
					if(pNode.Attributes["LineType"]!=null){
						line.LineType = (DIYReport.ReportModel.LineType) int.Parse(pNode.Attributes["LineType"].Value) ;
					}
					break;
				case DIYReport.ReportModel.RptObjType.Text:
				case DIYReport.ReportModel.RptObjType.Express:
					//如果是文本对象，需要加载字体的颜色、形状
					DIYReport.Interface.IRptTextObj   txt = pRptObj as DIYReport.Interface.IRptTextObj;
					txt.WordWrap = bool.Parse(pNode.Attributes["WordWrap"].Value);
					string sLimg = pNode.Attributes["Alignment"].Value;
					//得到字符对齐方式  //由于早期的报表是存储字符的，为了保持兼容性，也采取这种存储方式
					if(sLimg == "Near"){
						txt.Alignment = StringAlignment.Near ;
					}
					else if(sLimg == "Center"){txt.Alignment = StringAlignment.Center ;}
					else{txt.Alignment = StringAlignment.Far;}

					txt.ShowFrame = bool.Parse(pNode.Attributes["ShowFrame"].Value);
					if(pNode.Attributes["FormatStyle"]!=null){
						txt.FormatStyle =  pNode.Attributes["FormatStyle"].Value ;
					}
					string fontName = pNode.Attributes["Font-FamilyName"].Value ;
					float fontSize = PublicFun.ToFloat(pNode.Attributes["Font-Size"].Value) ;
					bool bold = bool.Parse(pNode.Attributes["Font-Bold"].Value);
					bool underline = bool.Parse(pNode.Attributes["Font-Underline"].Value);
					bool italic = bool.Parse(pNode.Attributes["Font-Italic"].Value);
					bool strikeout = bool.Parse(pNode.Attributes["Font-Strikeout"].Value);
					FontStyle fontType = FontStyle.Regular;
					//通过位运算得到字符stype 靠！怎么会采取这种存储方式呢，看来晚上是不能写的太晚~~~~~~~~
					if(bold){fontType = fontType | FontStyle.Bold ;}
					if(underline){fontType = fontType | FontStyle.Underline ;}
					if(italic){fontType = fontType | FontStyle.Italic ;}


					Font f = new Font(fontName,fontSize);
					Font font = new Font(f,fontType); 
					txt.Font = font;
					if(txt.Type == DIYReport.ReportModel.RptObjType.Text){
						if(pNode.Attributes["Text"]!=null){
							DIYReport.ReportModel.RptObj.RptLable lab = txt as DIYReport.ReportModel.RptObj.RptLable;
							lab.Text = pNode.Attributes["Text"].Value ;
						}
					}
					else{
						DIYReport.ReportModel.RptObj.RptExpressBox box = txt as DIYReport.ReportModel.RptObj.RptExpressBox;
						if(pNode.Attributes["ExpressType"]!=null){
							int exType = int.Parse(pNode.Attributes["ExpressType"].Value);
							box.ExpressType = (DIYReport.ReportModel.ExpressType)exType;
						}
						if(pNode.Attributes["DataSource"]!=null){
							box.DataSource = pNode.Attributes["DataSource"].Value;
						}
						if(pNode.Attributes["FieldName"]!=null){
							box.FieldName = pNode.Attributes["FieldName"].Value;
						}

					}
					break;
				case DIYReport.ReportModel.RptObjType.Image :
					DIYReport.ReportModel.RptObj.RptPictureBox pic = pRptObj as DIYReport.ReportModel.RptObj.RptPictureBox;
					//处理图片
					ReadRptImage(pic);
					break;
				default:
					break;
			}
		}

		#endregion Read From Rpt XML File...
	}
}
