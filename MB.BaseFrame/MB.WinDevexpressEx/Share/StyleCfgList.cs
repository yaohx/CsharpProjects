//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	StyleCfgList UI Style 描述的特定集合类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Xml;

namespace MB.XWinLib.Share
{
	/// <summary>
	/// StyleCfgList UI Style 描述的特定集合类。
	/// </summary>
	public class StyleCfgList : System.Collections.Generic.Dictionary<string,StyleCfgInfo>{
		#region 变量定义...
		private static readonly string STYLE_CONFIG_NODE="/AppConfig/Styles/Style";
		public static  string DEFAULT_CFG_NAME = "AppStylesConfig";
		//存储已经获取的样式数据。一般来说，一个应用程序里面只要有一个Style 就可以了。
		private static StyleCfgList _DataStore; 
		#endregion 变量定义...

		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		protected StyleCfgList(string cfgFileName) {
			createFromcfg(cfgFileName);
		}
		#endregion 构造函数...

		#region Public Static 方法...
		/// <summary>
		/// 实例化一个样式的集合类。（增加它是为了实现单根模式）
		/// </summary>
		/// <returns></returns>
		public static StyleCfgList CreateInstance(){
			return CreateInstance(DEFAULT_CFG_NAME);
		}
		/// <summary>
		///  实例化一个样式的集合类。（增加它是为了实现单根模式）
		/// </summary>
		/// <param name="cfgFileName"></param>
		/// <returns></returns>
		public static StyleCfgList CreateInstance(string cfgFileName){
			if(_DataStore==null)
				_DataStore = new StyleCfgList(cfgFileName);
			return _DataStore;
		}

		#endregion Public Static 方法...


		#region public 方法...
		/// <summary>
		/// add
		/// </summary>
		/// <param name="colInfo"></param>
		/// <returns></returns>
		public StyleCfgInfo Add(StyleCfgInfo colInfo){
			base.Add(colInfo.Name,colInfo); 

			return colInfo;
		}
		#endregion public 方法...

		#region 内部函数处理...
		//从配置文件中获取信息。
		private void createFromcfg(string fileName){
            XmlDocument xmlDoc = MB.WinBase.ShareLib.Instance.LoadXmlConfigFile(fileName);
            if (xmlDoc == null)
				return ;
            XmlNodeList nodeList = xmlDoc.SelectNodes(STYLE_CONFIG_NODE);
			if(nodeList==null)
				return ;
			foreach(XmlNode node in nodeList){
				if(node.NodeType != XmlNodeType.Element)
					continue;
				if(node.Attributes["Name"]==null)
					continue;
				string name = node.Attributes["Name"].Value;
				StyleCfgInfo info = new StyleCfgInfo(name);
				
				fillInfo(info,node);

				this.Add(info);
			}
		}
		//获取节点
		private void fillInfo(StyleCfgInfo info,XmlNode xmlNode){
			if(xmlNode.ChildNodes.Count==0)
				return;
			foreach(XmlNode node in xmlNode.ChildNodes){
				if(node.NodeType != XmlNodeType.Element)
					continue;
				string text = node.InnerText.Trim();
				switch(node.Name){
					case "BackColor":
						info.BackColor = System.Drawing.Color.FromName(text) ;
						break;
					case "ForeColor":
						info.ForeColor = System.Drawing.Color.FromName(text) ;
						break;
					case "Font":
						info.Font = getFont(node);
						break;
					default:
						MB.Util.TraceEx.Write("节点" + node.Name + "还没有进行处理，请确定对应的XML文件配置是否正确，请区分大小写。");  
						break;
				}
			}
		}
		//根据配置得到字体
		private System.Drawing.Font getFont(XmlNode fontNode){
			float size = 9f;
			System.Drawing.FontStyle styles = System.Drawing.FontStyle.Regular; 
			
			foreach(XmlNode node in fontNode.ChildNodes){
				if(node.NodeType!= XmlNodeType.Element)
					continue;
				string text = node.InnerText.Trim();
                
				switch(node.Name){
					case "Size":
						size = MB.Util.MyConvert .Instance.ToFloat(text,2);  
						break;
					case "Bold":
						if(MB.Util.MyConvert .Instance.ToBool(text))
							styles = styles | System.Drawing.FontStyle.Bold;
						break;
					case "Italic":
						if(MB.Util.MyConvert .Instance.ToBool(text))
							styles = styles | System.Drawing.FontStyle.Italic;
						break;
					case "Strikeout":
						if(MB.Util.MyConvert .Instance.ToBool(text)) 
							styles = styles | System.Drawing.FontStyle.Strikeout;
						break;
					case "UndeLine":
						if(MB.Util.MyConvert .Instance.ToBool(text))
							styles = styles | System.Drawing.FontStyle.Underline;
						break;
					default:
						MB.Util.TraceEx.Write("节点" + node.Name + "还没有进行处理，请确定对应的XML文件配置是否正确，请区分大小写。");  
						break;
				}

			}
			System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif",size,styles);
			return font;
		}
		#endregion 内部函数处理...
	}
}
