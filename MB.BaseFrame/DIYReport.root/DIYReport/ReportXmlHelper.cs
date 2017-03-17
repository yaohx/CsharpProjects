//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-12
// Description	:	主要处理报表数据的存储和XML文档之间的转换。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace DIYReport
{
	/// <summary>
	/// ReportXmlHelper 主要处理报表数据的存储和XML文档之间的转换。。
	/// </summary>
	public class ReportXmlHelper
	{

		public static readonly string REPORT_ASSEMBLY = System.AppDomain.CurrentDomain.BaseDirectory  +  @"DIYReportR3.DLL";
		public static readonly string REPORT_ROOT = "DIYReport.ReportModel.RptReport";
		public static readonly string SECTION_LIST_MARKER = "SectionList";
		public static readonly string RPT_OBJ_LIST_MARKER = "RptObjList";
		private static string _CurrentProcessFilePath = "";

		/// <summary>
		/// 打开保存在本地目录下的报表文件
		/// </summary>
		/// <returns></returns>
		public static DIYReport.ReportModel.RptReport Open(){
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
		public static DIYReport.ReportModel.RptReport OpenReport(string pFullPath){
			bool b = System.IO.File.Exists(pFullPath);
			if(b){
				try{
					XmlDocument doc = new XmlDocument();
					doc.Load( pFullPath );
 
					DIYReport.ReportModel.RptReport report = ReportReader.Instance().BuildReport(doc);
					
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
		public static bool Save(DIYReport.ReportModel.RptReport pReport){
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
					doc.LoadXml(ReportWriter.Instance().BuildXMLString(pReport));
					doc.Save( filePath );
					//System.Windows.Forms.MessageBox.Show("报表导出成功！","导出报表操作提示",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);  
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
	}
	/// <summary>
	/// 报表构造XML 字符窜
	/// </summary>
	public class ReportWriter{
		private static ReportWriter _Writer;
		public static ReportWriter Instance(){
			if(_Writer==null)
				_Writer = new ReportWriter();
			return _Writer;
		}
		/// <summary>
		/// 把报表对象转换为
		/// </summary>
		/// <param name="pReport"></param>
		/// <returns></returns>
		public byte[] BuildToXmlBytes(DIYReport.ReportModel.RptReport report){
			string reportStr = BuildXMLString(report);
			return  System.Text.Encoding.UTF8.GetBytes(reportStr); 
		}
		/// <summary>
		///  根据报表对象构建对应的xml 文档字符窜。
		/// </summary>
		/// <param name="pReport"></param>
		/// <returns></returns>
		public string BuildXMLString(DIYReport.ReportModel.RptReport pReport){
			StringBuilder xmlSB = new StringBuilder();
			//描述
			xmlSB.Append("<?xml version='1.0' encoding='utf-8' ?>");
			xmlSB.Append("<!-- 用户自定义智能报表中心生成的报表 -->");
			
			writeFirstMarker(xmlSB, pReport.GetType().FullName); 
			xmlSB.Append( reportString(pReport));
			DIYReport.ReportModel.RptSectionList sectionList = pReport.SectionList ;
			
			writeFirstMarker(xmlSB, ReportXmlHelper.SECTION_LIST_MARKER); 
			foreach(DIYReport.ReportModel.RptSection section in  sectionList){
				writeFirstMarker(xmlSB, section.GetType().FullName); 
				
				xmlSB.Append( sectionString(section));
				writeFirstMarker(xmlSB, ReportXmlHelper.RPT_OBJ_LIST_MARKER); 
				DIYReport.ReportModel.RptSingleObjList rptObjList = section.RptObjList ;
				foreach(DIYReport.Interface.IRptSingleObj   rptObj in rptObjList){
					writeFirstMarker(xmlSB, rptObj.GetType().FullName); 
						xmlSB.Append(objectToXml(rptObj));
					writeLastMarker(xmlSB, rptObj.GetType().FullName);
				}
				writeLastMarker(xmlSB, ReportXmlHelper.RPT_OBJ_LIST_MARKER); 
				writeLastMarker(xmlSB, section.GetType().FullName);
			}
			writeLastMarker(xmlSB, ReportXmlHelper.SECTION_LIST_MARKER); 
			writeLastMarker(xmlSB, pReport.GetType().FullName);
			return xmlSB.ToString();
 
		}

		private string reportString(DIYReport.ReportModel.RptReport pReport){
			return objectToXml(pReport,new string[]{"ID","Name","Text","GroupLayoutType","DetailOrderString","Width","Height",
												   "PaperName","ReportDataWidth","PrintName","IsLandscape","Margins","FillNULLRow","IDEX"});
			
		}
		private string sectionString(DIYReport.ReportModel.RptSection pSection){
			 
			return objectToXml(pSection,new string[]{"Name","SectionType","Height","Width","SectionType","Index","GroupField","BackgroundImage"});
		}
//		private string rptObjString(DIYReport.Interface.IRptSingleObj   pRptObj){
//			return objectToXml(pRptObj);
//		}

		private string objectToXml(object obj,params string[] proList){
			StringBuilder sXml = new StringBuilder();
			Type t = obj.GetType();
			//writeFirstMarker(sXml,t.FullName);

			PropertyInfo[] pros = t.GetProperties();
			foreach(PropertyInfo info in pros){
				if(info.IsSpecialName)
					continue;
				if(proList!=null && proList.Length > 0 && Array.IndexOf(proList,info.Name)== -1)
					continue;
				if(!info.CanRead || !info.CanWrite)
					continue;
				if(string.Compare(info.Name,"Section",true)==0)
					continue;
				Type proType = info.PropertyType;
				writeFirstMarker(sXml,info.Name);
				object val = info.GetValue(obj,null);
				if(val!=null){
					switch(proType.Name){
						case "Color":
							sXml.Append(((Color)val).Name);
							break;
						case "Rectangle":
						case "RectangleF":
							processObject(sXml,val,new string[]{"X","Y","Width","Height"});
							break;
						case "Size":
						case "SizeF":
						case "Point":
						case "PointF":
						case "PaperSize":
						case "Margins":
						case "RptFieldInfo":
							processObject(sXml,val);
							break;
						case "Font":
							processObject(sXml,val,new string[]{"Name","Size","Bold","Underline","Italic","Strikeout"});
							break;
						case "Image":
							sXml.Append(DIYReport.PublicFun.ImageToString(val as Image));
							break;
						default:
							sXml.Append(val.ToString());
							break;
					}
				}
				writeLastMarker(sXml,info.Name);
				 
				 
			}
			//writeLastMarker(sXml,t.FullName);
			return sXml.ToString();
		}
		private void processObject(StringBuilder sXml,object val,params string[] proList){
			Type t = val.GetType();
			PropertyInfo[] pros = t.GetProperties();
			foreach(PropertyInfo info in pros){
				if(info.IsSpecialName)
					continue;
				if(proList!=null && proList.Length > 0 && Array.IndexOf(proList,info.Name)== -1)
					continue;
				bool mustDo = (proList!=null && proList.Length > 0 && Array.IndexOf(proList,info.Name)> -1);
				if(!mustDo){
					if(!info.CanRead || !info.CanWrite)
						continue;
				}
				Type proType = info.PropertyType;
				object v = info.GetValue(val,null);
				if(proType.IsEnum || proType.IsValueType || string.Compare(proType.Name,"String",true)==0){ 
					writeFirstMarker(sXml,info.Name);
					if(v!=null){
						sXml.Append(v.ToString());
					}
					writeLastMarker(sXml,info.Name);
				}
				else{
					Debug.Assert(false,"下级的类型目前先不处理."); 
					//processObject(sXml,v);
				}
			}
		}
		 
		private void writeFirstMarker(StringBuilder sXml,string nodeName){
			sXml.Append("<");
			sXml.Append(nodeName);
			sXml.Append(">");
		}
		private void writeLastMarker(StringBuilder sXml,string nodeName){
			sXml.Append("</");
			sXml.Append(nodeName);
			sXml.Append(">");
		}
	}

	/// <summary>
	/// 报表读取器。
	/// </summary>
	public class ReportReader{
		private static ReportReader _Reader;
		public static ReportReader Instance(){
			if(_Reader==null)
				_Reader = new ReportReader();
			return _Reader;
		}
		/// <summary>
		///  根据xml 文件创建对应的报表对象。
		/// </summary>
		/// <param name="reportData"></param>
		/// <returns></returns>
		public DIYReport.ReportModel.RptReport ReadFromXmlBytes(byte[] reportData){
			string xmlStr = System.Text.Encoding.UTF8.GetString(reportData);
			
			return ReadFromXmlString(xmlStr);

		}
		/// <summary>
		/// 根据xml 文件创建对应的报表对象。
		/// </summary>
		/// <param name="reportData"></param>
		/// <returns></returns>
		public DIYReport.ReportModel.RptReport ReadFromXmlString(string reportData){
			XmlDocument xmlDoc = new XmlDocument();
			bool b = DIYReport.UserDIY.DesignEnviroment.IsUserDesign;
			try{
				DIYReport.UserDIY.DesignEnviroment.IsUserDesign = false;
				xmlDoc.LoadXml(reportData);
			}
			catch(Exception e){
				throw new Exception("需要加载的报表格式有误，请检查。" + e.Message);
			}
			finally{
				DIYReport.UserDIY.DesignEnviroment.IsUserDesign = b;
			}
			return BuildReport(xmlDoc);
		}
		/// <summary>
		/// 根据xml 文件创建对应的报表对象。
		/// </summary>
		/// <param name="xmlDoc"></param>
		/// <returns></returns>
		public  DIYReport.ReportModel.RptReport BuildReport(XmlDocument xmlDoc){
			bool b = DIYReport.UserDIY.DesignEnviroment.IsUserDesign;
			DIYReport.ReportModel.RptReport report = null;
			try{
				DIYReport.UserDIY.DesignEnviroment.IsUserDesign = false;

				XmlNode firstNode = xmlDoc.SelectSingleNode("/" + ReportXmlHelper.REPORT_ROOT);
				TrackEx.Write("正在加载自定义报表组件:" +  ReportXmlHelper.REPORT_ASSEMBLY);
				System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(ReportXmlHelper.REPORT_ASSEMBLY);
				TrackEx.Write("自定义报表组件加载成功！");
				report = new DIYReport.ReportModel.RptReport();
				setObjectProperty(asm,firstNode,report);
				//报表XML文档的结构为： Report/SelectList/Section/RptObjList/RptObj;
				//现在晕了： 下次再把下面的代码修改修改
				foreach(XmlNode reportNode in firstNode.ChildNodes){
					if(string.Compare(reportNode.Name,ReportXmlHelper.SECTION_LIST_MARKER,true)!=0)
						continue;
					foreach(XmlNode sectionNode in reportNode.ChildNodes){
						DIYReport.ReportModel.RptSection rptSection = new DIYReport.ReportModel.RptSection();
						rptSection.Report = report;
						setObjectProperty(asm,sectionNode,rptSection);
						report.SectionList.Add(rptSection);
						foreach(XmlNode sectionProNode in sectionNode.ChildNodes){
							if(string.Compare(sectionProNode.Name,ReportXmlHelper.RPT_OBJ_LIST_MARKER,true)!=0) 
								continue;
							foreach(XmlNode rptObjNode in sectionProNode.ChildNodes){
								object rptObj = asm.CreateInstance(rptObjNode.Name);
								if(rptObj==null)
									continue;
								setObjectProperty(asm,rptObjNode,rptObj);
								DIYReport.Interface.IRptSingleObj rpt = rptObj as DIYReport.Interface.IRptSingleObj;
								if(rpt!=null){
									rptSection.RptObjList.Add(rpt);
									rpt.Section = rptSection;  
								}
								else{
									DIYReport.TrackEx.Write("把类型" + rptObj.GetType().FullName +"转换为DIYReport.Interface.IRptSingleObj为空。" );
								}
							}		
						}
					}
 
				}
				//特殊处理一些属性
                DIYReport.TrackEx.Write("开始执行获取纸张大小");
				report.PaperSize = DIYReport.Print.RptPageSetting.GetSizeByFullInfo(report.PrintDocument,report.PrintName,report.PaperName,report.Width,report.Height);    

			} 
			catch(Exception e){
				throw e;
			}
			finally{
				DIYReport.UserDIY.DesignEnviroment.IsUserDesign = b;
			}
			return report;
		}
		//根据XML 文档的node 设置对象的属性
		private void setObjectProperty(System.Reflection.Assembly asm,XmlNode parentNode,object obj){
			Type t = obj.GetType();
			foreach(XmlNode node in parentNode.ChildNodes){
				if(node.NodeType!=System.Xml.XmlNodeType.Element)
					continue;
				if(string.Compare(node.Name,ReportXmlHelper.RPT_OBJ_LIST_MARKER,true)==0) 
					continue;
				if(string.Compare(node.Name,ReportXmlHelper.SECTION_LIST_MARKER,true)==0)
					continue;
				PropertyInfo pInfo = t.GetProperty(node.Name);
				if(pInfo==null)
					continue;
				if(pInfo.PropertyType.IsEnum ){
					pInfo.SetValue(obj,Enum.Parse(pInfo.PropertyType,node.InnerText,true),null);
				}
				else if(string.Compare(pInfo.PropertyType.Name,"string",true)==0){
					pInfo.SetValue(obj, node.InnerText,null);
				}
				else{switch(pInfo.PropertyType.Name){
						 case "Guid":
							pInfo.SetValue(obj, new Guid(node.InnerText),null);
							 break;
						 case "Color":
							 pInfo.SetValue(obj, Color.FromName(node.InnerText),null);
							 break;
						 case "Rectangle":
						 case "RectangleF":
							 pInfo.SetValue(obj, createObj(asm,node,pInfo.PropertyType,new string[]{"X","Y","Width","Height"}),null);
							 break;
						 case "Size":
						 case "SizeF":
						 case "Point":
						 case "PointF":
						 case "Margins":
						 case "RptFieldInfo":
							 if(node.HasChildNodes)
								pInfo.SetValue(obj, createObj(asm,node,pInfo.PropertyType),null);
							 break;
						 case "Font":
							 pInfo.SetValue(obj, createFont(node),null);
							 break;
						 case "Image":
							 if(node.InnerText!=null && node.InnerText.Length > 0) 
								 pInfo.SetValue(obj,DIYReport.PublicFun.StringToImage(node.InnerText),null);
							 break;
						 default:
							 if(pInfo.PropertyType.IsValueType){
								 pInfo.SetValue(obj,Convert.ChangeType(node.InnerText,pInfo.PropertyType),null);
							 }
							 else
								 Debug.Assert(false,"类型" + pInfo.Name + "还没有处理.");
							 break;
					 }
				}
			}
		}
		//根据Xml node 和类型创建 对应的对象。
		private object createObj(System.Reflection.Assembly asm,XmlNode node,Type proType,params string[] proList){
			object proObj = proType.Assembly.CreateInstance( proType.FullName,true);
			PropertyInfo[] infos =  proObj.GetType().GetProperties();
			foreach(PropertyInfo info in infos){
				if(info.IsSpecialName)
					continue;
				if(!info.CanWrite)
					continue;
				if(proList!=null && proList.Length > 0 && Array.IndexOf(proList,info.Name)==-1)
					continue;
				XmlNode proNode = getChildNodeByName(node.ChildNodes,info.Name);
				if(proNode==null){
					Debug.Assert(false,"属性" + info.Name + "预先没有进行存储处理");
					continue;
				}
				//通过ChildNodes.Count是否等于0来判断innerText 是否为它对应的属性值。
				//if(!proNode.HasChildNodes){
				info.SetValue(proObj,Convert.ChangeType(proNode.InnerText,info.PropertyType),null);
				//}
				//else{
				//	info.SetValue(proObj,createObj(proNode,info.PropertyType),null);
				//}
			}
			return proObj;
		}
		private Font createFont(XmlNode node){
			//XmlNode childNode = node.SelectSingleNode("/Name");
			XmlNode xNode = getChildNodeByName(node.ChildNodes,"Name");
			if(xNode==null)
				return null; 
			string fontName = xNode.InnerText;
			float fontSize = DIYReport.PublicFun.ToFloat(getChildNodeByName(node.ChildNodes, "Size").InnerText);
			bool bold = bool.Parse(getChildNodeByName(node.ChildNodes,"Bold").InnerText);
			bool underline = bool.Parse(getChildNodeByName(node.ChildNodes,"Underline").InnerText);
			bool italic = bool.Parse(getChildNodeByName(node.ChildNodes,"Italic").InnerText);
			bool strikeout = bool.Parse(getChildNodeByName(node.ChildNodes,"Strikeout").InnerText);
			FontStyle fontType = FontStyle.Regular;
			if(bold){fontType = fontType | FontStyle.Bold ;}
			if(underline){fontType = fontType | FontStyle.Underline ;}
			if(italic){fontType = fontType | FontStyle.Italic ;}
			Font f = new Font(fontName,fontSize);
			Font font = new Font(f,fontType); 
			return font;
			
		}
		private XmlNode getChildNodeByName(XmlNodeList nodes ,string name){
			foreach(XmlNode node in nodes){
				if(string.Compare(node.Name,name,true)==0)
					return node;
			}
			return null;
		}
	}
}
