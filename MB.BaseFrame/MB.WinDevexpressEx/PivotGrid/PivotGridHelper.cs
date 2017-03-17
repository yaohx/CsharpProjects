//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	PivotGridHelper PivotGrid 操作相关的处理函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using MB.Util;
using System.Windows.Forms;
using MB.XWinLib.Share;
using MB.XWinLib.XtraGrid; 
namespace MB.XWinLib.PivotGrid
{
	/// <summary>
	/// PivotGridHelper PivotGrid 操作相关的处理函数。
	/// </summary>
	public class PivotGridHelper {
		private static string LAYOUT_XML_PATH = MB.Util.General.GeApplicationDirectory() + @"UserSetting\";
        //add by aifang 支持多维
        private static readonly string GRID_LAYOUT_FILE_SETTING_FULLNAME = LAYOUT_XML_PATH + "GridLayoutSetting.xml";

        #region Instance...
        private static Object _Obj = new object();
        private static PivotGridHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected PivotGridHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static PivotGridHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new PivotGridHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

		#region public  function...
		/// <summary>
		/// 把数据加载到PivotGrid 控件中。
		/// </summary>
		/// <param name="gridCtl">PivotGrid 控件。</param>
		/// <param name="dataSource">准备加载的数据源。</param>
		/// <param name="baseRule">当前正在处理的业务类。</param>
		public void LoadPivotGridData(DevExpress.XtraPivotGrid.PivotGridControl gridCtl,
			object dataSource,MB.WinBase.IFace.IClientRuleQueryBase baseRule,ColPivotList xmlCfgLst){

            addDataToCtl(gridCtl, dataSource, baseRule, xmlCfgLst);
			RestoreLayout(gridCtl,baseRule);
		}
		#endregion public function...
        /// <summary>
        /// 把XtraGrid中的数据倒出到Excel 中 并打开Excel 进行显示
        /// </summary>
        /// <param name="pDg"></param>
        public void ExportToExcelAndShow(DevExpress.XtraPivotGrid.PivotGridControl xtraGrid) {
            //string temPath = MB.Util.General.GeApplicationDirectory() + @"Temp\";
            //bool b = System.IO.Directory.Exists(temPath);
            //if (!b) {
            //    System.IO.Directory.CreateDirectory(temPath);
            //}
            //string fullPath = temPath + System.Guid.NewGuid().ToString() + ".xls";
            //b = System.IO.File.Exists(fullPath);
            var fileDialog = new System.Windows.Forms.SaveFileDialog();
            fileDialog.Filter = "Excel 文件|.xls";
            fileDialog.ShowDialog();
            var fileFullName = fileDialog.FileName;
            if (string.IsNullOrEmpty(fileFullName)) return;
            try {
                xtraGrid.ExportToXls(fileFullName);
                var b = System.IO.File.Exists(fileFullName);
                //判断该文件是否导出成功，如果是那么直接打开该文件
                if (b) {
                    //System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
                    //Info.FileName = fullPath;
                    ////声明一个程序类
                    //System.Diagnostics.Process Proc;
                    ////启动外部程序
                    //Proc = System.Diagnostics.Process.Start(Info);
                    var dre = MB.WinBase.MessageBoxEx.Question("文件导出成功，是否需要打开文件所在的目录");
                    if (dre == DialogResult.Yes)
                    {
                        System.Diagnostics.ProcessStartInfo explore = new System.Diagnostics.ProcessStartInfo();
                        explore.FileName = "explorer.exe";
                        explore.Arguments = System.IO.Path.GetDirectoryName(fileFullName);
                        System.Diagnostics.Process.Start(explore);
                    }
                }
            }
            catch (Exception e) {
                MB.Util.TraceEx.Write("导出Excel 文件出错！" + e.Message, MB.Util.APPMessageType.SysErrInfo);
                MB.WinBase.MessageBoxEx.Show("导出Excel 文件出错！");
            }
        }
		
		/// <summary>
		/// 执行PivotGrid 自定义事件。
		/// </summary>
		/// <param name="containerForm"></param>
		/// <param name="xtraGrid"></param>
		/// <param name="menuType"></param>
		public void HandleClickXtraContextMenu(MB.WinBase.IFace.IForm containerForm,
			PivotGridEx xtraGrid,XtraContextMenuType menuType){
			if(containerForm==null)
				return;
			switch(menuType){
				case XtraContextMenuType.Sort:
					break;
				case XtraContextMenuType.Style:
					break;
				case XtraContextMenuType.SaveDefaultStyle:
                    showFrmGridLayoutManager(containerForm, xtraGrid); //modify by aifang 2012-6-7
                    //SavePivotGridLayout(containerForm, xtraGrid);
                    //xtraGrid.ShowPrintPreview(); 
					break;
				default:
					break;
			}

		}
        //显示Grid状态设置窗体
        private void showFrmGridLayoutManager(MB.WinBase.IFace.IForm containerForm, PivotGridEx xtraGrid)
        {
            frmGridLayoutManager frm = new frmGridLayoutManager(containerForm, xtraGrid);
            frm.ShowDialog();
        }
        public void SavePivotGridLayout(MB.WinBase.IFace.IForm containerForm, PivotGridEx xtraGrid)
        {
            string name = getLayoutXmlSaveName(containerForm.ClientRuleObject, xtraGrid);
            if (!System.IO.Directory.Exists(LAYOUT_XML_PATH))
                System.IO.Directory.CreateDirectory(LAYOUT_XML_PATH);

            xtraGrid.SaveLayoutToXml(LAYOUT_XML_PATH + name);
            //xtraGrid.SaveLayoutToXml(name);
        }
        public void DeletePivotGridLayout(MB.WinBase.IFace.IForm containerForm, PivotGridEx xtraGrid,GridLayoutInfo layoutInfo)
        {
            string name = getLayoutXmlSessionName(containerForm.ClientRuleObject, xtraGrid);
            if (layoutInfo != null) name = name + "~" + layoutInfo.Name + ".xml";

            if (string.IsNullOrEmpty(name)) return;

            if (System.IO.File.Exists(LAYOUT_XML_PATH + name))
            {
                System.IO.File.Delete(LAYOUT_XML_PATH + name);
            }
        }
        public void RestoreLayout(DevExpress.XtraPivotGrid.PivotGridControl gridCtl, MB.WinBase.IFace.IClientRuleQueryBase baseRule) {
			PivotGridEx pivotGrid = gridCtl as PivotGridEx;
			if(pivotGrid==null)
				return;
			string name = getLayoutXmlSaveName(baseRule,pivotGrid);
			if(System.IO.File.Exists(LAYOUT_XML_PATH + name)){
				gridCtl.OptionsLayout.Columns.RemoveOldColumns = true; 
				gridCtl.RestoreLayoutFromXml( LAYOUT_XML_PATH + name );
			}
		}
        public string getLayoutXmlSessionName(MB.WinBase.IFace.IClientRuleQueryBase baseRule, PivotGridEx xtraGrid) {
			if( xtraGrid.ParentForm==null)
				return null;
            return baseRule.GetType().FullName + "~" + xtraGrid.ParentForm.GetType().FullName + "~" + xtraGrid.Name; 			
		}

        //add by aifang
        private GridLayoutInfo GetPivotGridActiveLayout(MB.WinBase.IFace.IClientRuleQueryBase baseRule, PivotGridEx xtraGrid)
        {
            try
            {
                var gridLayoutMainList = new MB.Util.Serializer.DataContractFileSerializer<List<GridLayoutMainInfo>>(GRID_LAYOUT_FILE_SETTING_FULLNAME).Read();
                if (gridLayoutMainList == null) return null;

                string sectionName = getLayoutXmlSessionName(baseRule, xtraGrid);
                var gridLayoutList = gridLayoutMainList.Find(o => o.GridSectionName.Equals(sectionName));
                if (gridLayoutList == null || gridLayoutList.GridLayoutList.Count == 0) return null;

                return gridLayoutList.GridLayoutList.OrderByDescending(o => o.CreateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(ex.Message, Util.APPMessageType.SysErrInfo);
                return null;
            }
        }
        private string getLayoutXmlSaveName(MB.WinBase.IFace.IClientRuleQueryBase baseRule, PivotGridEx xtraGrid)
        {
            try
            {
                if (xtraGrid.ParentForm == null)
                    return null;
                string layoutXmlName = getLayoutXmlSessionName(baseRule, xtraGrid);
                GridLayoutInfo layoutInfo = GetPivotGridActiveLayout(baseRule, xtraGrid);
                //add by aifang 2012-08-29 begin
                //判断状态保存日期是否大于动态列设置日期，如是，则生效，否则不生效。
                DateTime dt = layoutInfo.CreateTime;
                var dynamicSetting = XtraGridDynamicHelper.Instance.GetXtraGridDynamicSettingInfo(baseRule);
                if (dynamicSetting != null)
                {
                    if (dynamicSetting.LastModifyDate.Subtract(dt).TotalMilliseconds > 0) layoutInfo = null;
                }
                //end

                if (layoutInfo == null)
                    return layoutXmlName + ".xml";
                else return layoutXmlName + "~" + layoutInfo.Name + ".xml";
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(ex.Message, Util.APPMessageType.SysErrInfo);
                return null;
            }
        }

		#region 内部函数处理...
		//内部函数处理，把数据加载到pivot grid 控件中。
		private void addDataToCtl(DevExpress.XtraPivotGrid.PivotGridControl gridCtl, object  dataSource
                                        , MB.WinBase.IFace.IClientRuleQueryBase baseRule, ColPivotList xmlCfgLst) {

            Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPros = XtraGridDynamicHelper.Instance.GetDynamicColumns(baseRule);
            //modify by aifang 2012-08-29 动态列加载  Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPros = baseRule.UIRuleXmlConfigInfo.GetDefaultColumns();

			if(colPros==null)
				return;

			//清除历史的设置
			gridCtl.Fields.Clear();
			gridCtl.Groups.Clear();

            List<Dictionary<DevExpress.XtraPivotGrid.PivotGridField, ColPivotInfo>> expressionFieldsList = new List<Dictionary<DevExpress.XtraPivotGrid.PivotGridField, ColPivotInfo>>();

            foreach (MB.WinBase.Common.ColumnPropertyInfo info in colPros.Values) {
				if(!info.IsPivotExpressionSourceColumn && (!info.Visibled || info.VisibleWidth < 1))
					continue;
				var result =setPivotFieldByCfg(xmlCfgLst,gridCtl,info);
                if (result != null)
                    expressionFieldsList.Add(result);
			}

            //在绑定一般列以后再处理非绑定列的表达式，如果在一起处理会发生列的加载先后顺序
            foreach (Dictionary<DevExpress.XtraPivotGrid.PivotGridField, ColPivotInfo> expressionFields in expressionFieldsList)
            {
                foreach (KeyValuePair<DevExpress.XtraPivotGrid.PivotGridField, ColPivotInfo> expressionField in expressionFields)
                {
                    expressionField.Key.UnboundType = DevExpress.Data.UnboundColumnType.Object;

                    string[] sourceColumns = expressionField.Value.ExpressionSourceColumns.Split(',');
                    string[] formattedSourceColumns = new string[sourceColumns.Length];
                    for (int i = 0; i < sourceColumns.Length; i++)
                    {
                        DevExpress.XtraPivotGrid.PivotGridField sourceField = gridCtl.Fields[sourceColumns[i]];
                        if (sourceField == null)
                            throw new MB.Util.APPException(string.Format("ExpressionSourceColumns中的列{0}中Expression中的列不存在，请注意配置中依赖列必需在被依赖列的前面", sourceColumns[i], MB.Util.APPMessageType.SysErrInfo));

                        formattedSourceColumns[i] = sourceField.ExpressionFieldName;
                    }

                    expressionField.Key.UnboundExpression = string.Format(expressionField.Value.Expression, formattedSourceColumns);
                }
            }

			//处理字段帮定的情况。
			processUnitField(xmlCfgLst,gridCtl);
			
			//通过条件增加样式
			addConditionsForStyles(gridCtl,baseRule);

			gridCtl.DataSource = dataSource;
			
		}

		//通过配置的PivotList 设置PivotField 的信息 
        private Dictionary<DevExpress.XtraPivotGrid.PivotGridField, ColPivotInfo> setPivotFieldByCfg(ColPivotList xmlCfgLst, DevExpress.XtraPivotGrid.PivotGridControl gridCtl,
													MB.WinBase.Common.ColumnPropertyInfo fieldInfo){

			if(xmlCfgLst==null)
				return null;
			if(xmlCfgLst.Columns.Count ==0 && !xmlCfgLst.AutoCreatedGridField)
				return null;

			IList<MB.XWinLib.PivotGrid.ColPivotInfo>  infos = xmlCfgLst.GetColPivotInfos(fieldInfo.Name);
			DevExpress.XtraPivotGrid.PivotGridGroup pivotGridGroup = null;
			if(infos.Count > 1 && xmlCfgLst.SameFieldGroup){
				pivotGridGroup = new DevExpress.XtraPivotGrid.PivotGridGroup();
				gridCtl.Groups.Add(pivotGridGroup);
			}
			if(infos.Count== 0){
				createNewPivotField(fieldInfo.Name,fieldInfo.Description,gridCtl);
                //add by aifang 去掉汇总后金额显示格式（原来不管是金额还是数量都显示￥前缀）begin
                if (fieldInfo.DataType.Equals("System.Decimal"))
                {
                    gridCtl.Fields[fieldInfo.Name].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                }
                //add by aifang end
				return null;
			}
			else{

                Dictionary<DevExpress.XtraPivotGrid.PivotGridField, ColPivotInfo> expressionFields = 
                    new Dictionary<DevExpress.XtraPivotGrid.PivotGridField, ColPivotInfo>();

                
				//处理一个字段对应多个分组的情况
                foreach (ColPivotInfo info in infos)
                {
                    DevExpress.XtraPivotGrid.PivotGridField pivField = createNewPivotField(fieldInfo.Name, fieldInfo.Description, gridCtl);
                    //如果定义了Expression则表示该列的值是通过其他列或其他手段推算而出的
                    if (!string.IsNullOrEmpty(info.Expression))
                    {
                        expressionFields.Add(pivField, info);
                    }

                    if (info.Description != null && info.Description.Length > 0)
                        pivField.Caption = info.Description;

                    pivField.Area = info.IniArea;
                    pivField.AreaIndex = info.AreaIndex;
                    pivField.AllowedAreas = info.AllowedAreas;
                    pivField.TopValueCount = info.TopValueCount;
                    pivField.GroupInterval = info.GroupInterval;

                    if (info.SummaryItemType != null && info.SummaryItemType.Length > 0)
                        pivField.SummaryType = (DevExpress.Data.PivotGrid.PivotSummaryType)Enum.Parse(typeof(DevExpress.Data.PivotGrid.PivotSummaryType), info.SummaryItemType);

                    //setDefaultFormatByGroup(pivField);
                    //格式化显示数据。
                    if (info.CellFormat != DevExpress.Utils.FormatInfo.Empty)
                    {
                        pivField.CellFormat.Format = info.CellFormat.Format;
                        pivField.CellFormat.FormatType = info.CellFormat.FormatType;
                        pivField.CellFormat.FormatString = info.CellFormat.FormatString;
                    }
                    if (info.ValueFormat != DevExpress.Utils.FormatInfo.Empty)
                    {
                        pivField.ValueFormat.Format = info.CellFormat.Format;
                        pivField.ValueFormat.FormatType = info.CellFormat.FormatType;
                        pivField.ValueFormat.FormatString = info.CellFormat.FormatString;
                    }
                    //加入字段绑定分组。
                    if (pivotGridGroup != null)
                    {
                        pivotGridGroup.Add(pivField);
                    }
                    //DevExpress.XtraPivotGrid.PivotGroupInterval.DateDayOfWeek

                }
                return expressionFields;
			}
		}
		//处理字段结合绑定的情况
		private void processUnitField(ColPivotList xmlCfgLst,DevExpress.XtraPivotGrid.PivotGridControl gridCtl){
			if(xmlCfgLst==null || xmlCfgLst.FieldGroups==null || xmlCfgLst.FieldGroups.Length ==0)
				return;
			string[] groups = xmlCfgLst.FieldGroups.Split(';');
			foreach(string group in groups){
				string[] fields = group.Split(',');
				DevExpress.XtraPivotGrid.PivotGridGroup pivotGridGroup = new DevExpress.XtraPivotGrid.PivotGridGroup();
				gridCtl.Groups.Add(pivotGridGroup);
				foreach(string field in fields){
					DevExpress.XtraPivotGrid.PivotGridField pivField = gridCtl.Fields[field];
					if(pivField==null)
						continue;
					pivotGridGroup.Add(pivField);
				}
			}

		}
		//根据分组的类型设置默认的分组数据。
//		private static void setDefaultFormatByGroup(DevExpress.XtraPivotGrid.PivotGridField pivField){
//			switch(pivField.GroupInterval){
//				case DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear:
//					pivField.ValueFormat.FormatString = "{0} 年";
//					pivField.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
//					break;
//				case DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter:
//					pivField.ValueFormat.FormatString = "季节 {0}";
//					pivField.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
//					break;
//				case DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth:
//					pivField.ValueFormat.FormatString = "{0} 月";
//					pivField.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
//					break;
//				case DevExpress.XtraPivotGrid.PivotGroupInterval.DateWeekOfYear:
//				case DevExpress.XtraPivotGrid.PivotGroupInterval.DateWeekOfMonth:
//					pivField.ValueFormat.FormatString = "第 {0} 周";
//					pivField.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
//					break;
//				case DevExpress.XtraPivotGrid.PivotGroupInterval.DateDayOfWeek:
//					pivField.ValueFormat.FormatString = "星期 {0}";
//					pivField.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
//					break;
//				case DevExpress.XtraPivotGrid.PivotGroupInterval.DateDay:
//					break;
//				default:
//					break;
//			}
//
//		}
		//创建一个新的PivotGridField 对象。
		private DevExpress.XtraPivotGrid.PivotGridField createNewPivotField(string fieldName,string fieldDesc,DevExpress.XtraPivotGrid.PivotGridControl gridCtl){
			DevExpress.XtraPivotGrid.PivotGridField pivField = new DevExpress.XtraPivotGrid.PivotGridField();
			pivField.Name = "pivCol" + fieldName;
			pivField.FieldName = fieldName;
			pivField.Caption = fieldDesc;
			gridCtl.Fields.Add(pivField); 
			gridCtl.OptionsLayout.StoreAllOptions = true; 
			return pivField;
		}
		//增加网格显示的格式化样式信息。
        private void addConditionsForStyles(DevExpress.XtraPivotGrid.PivotGridControl gridCtl, MB.WinBase.IFace.IClientRuleQueryBase busObj) {			
			if(busObj == null)
				return;


            Dictionary<string, MB.WinBase.Common.StyleConditionInfo> styleConditions = busObj.UIRuleXmlConfigInfo.GetDefaultStyleConditions();
			if(styleConditions==null)return;

			XWinLib.Share.StyleCfgList styleList =  XWinLib.Share.StyleCfgList.CreateInstance();
			if(styleList==null)return;
              
			foreach(MB.WinBase.Common.StyleConditionInfo info in  styleConditions.Values){
				//判断定义的样式是否存在。
				if(!styleList.ContainsKey(info.StyleName)){
					MB.Util.TraceEx.Write(string.Format("样式 {0} 需要在AppStylesConfig.xml 文件中配置,如果已经配置请检查大小写。",info.StyleName),MB.Util.APPMessageType.SysErrInfo);  
					continue;
				}
				DevExpress.XtraPivotGrid.PivotGridField pivField = gridCtl.Fields[info.ColumnName];
				if(pivField==null) {
					MB.Util.TraceEx.Write(string.Format("字段 {0} 的 Style Format condition 列名称的配置，在数据源中找不到对应的列，请检查。",info.ColumnName ),MB.Util.APPMessageType.SysErrInfo);  
					continue;
				}

				XWinLib.Share.StyleCfgInfo styleInfo = styleList[info.StyleName];

				//如果存在DispColName 的话，需要也是处理，在本应该程序中是在GridView的RowCellStyle事件中进行处理。
				if(info.IsByEvent || (info.DispColName!=null && info.DispColName.Length > 0 && info.DispColName!=info.ColumnName))
					continue;
				//创建FormatCondition。  
				if(gridCtl.FormatConditions[info.Name]==null){
					DevExpress.XtraGrid.FormatConditionEnum cc = (DevExpress.XtraGrid.FormatConditionEnum)Enum.Parse( typeof(DevExpress.XtraGrid.FormatConditionEnum),Enum.GetName(typeof(DataFilterConditions),info.Condition));
					DevExpress.XtraPivotGrid.PivotGridStyleFormatCondition styleCondition = new DevExpress.XtraPivotGrid.PivotGridStyleFormatCondition();

					styleCondition.Appearance.BackColor = styleInfo.BackColor;
					styleCondition.Appearance.ForeColor = styleInfo.ForeColor;
					styleCondition.Appearance.Font = styleInfo.Font;

					styleCondition.Appearance.Options.UseBackColor = true;
					styleCondition.Appearance.Options.UseForeColor = true;
					styleCondition.ApplyToCustomTotalCell = false;
					styleCondition.ApplyToGrandTotalCell = false;
					styleCondition.ApplyToTotalCell = false;

					styleCondition.Condition = (DevExpress.XtraGrid.FormatConditionEnum)Enum.Parse( typeof(DevExpress.XtraGrid.FormatConditionEnum),Enum.GetName(typeof(DataFilterConditions),info.Condition));
					styleCondition.Field = pivField;
					styleCondition.Value1 = info.Value ;
					styleCondition.Value2 = info.Value2; 

					gridCtl.FormatConditions.Add(styleCondition);

				}
			}
		}
		
		#endregion 内部函数处理...
	}
}
