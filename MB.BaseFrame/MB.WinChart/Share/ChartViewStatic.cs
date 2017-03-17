using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Share
{
	/// <summary>
	/// 报表静态据类.
	/// </summary>
	public static class ChartViewStatic
	{
		#region Readonly variables

		private static readonly string _CHART_NAME = "ChartViewName";

		/// <summary>
		/// 报表名.
		/// </summary>
		public static string CHART_NAME
		{
			get { return ChartViewStatic._CHART_NAME; }
		}

		private static readonly string _CHART_CHARTAREA_NAME = "ChartViewChartArea";

		/// <summary>
		/// <c>ChartArea</c>名.
		/// </summary>
		public static string CHART_CHARTAREA_NAME
		{
			get { return ChartViewStatic._CHART_CHARTAREA_NAME; }
		}

		private static readonly string _CHART_SERIES_NAME = "ChartViewSeries";

		/// <summary>
		/// 分类名.
		/// </summary>
		public static string CHART_SERIES_NAME
		{
			get { return ChartViewStatic._CHART_SERIES_NAME; }
		} 

		private static readonly string _SYSTEM_INT32 = "System.Int32";

		/// <summary>
		/// <c>Int.</c>
		/// </summary>
		public static string SYSTEM_INT32
		{
			get { return ChartViewStatic._SYSTEM_INT32; }
		}

		private static readonly string _SYSTEM_SINGLE = "System.Single";

		/// <summary>
		/// <c>Single.</c>
		/// </summary>
		public static string SYSTEM_SINGLE
		{
			get { return ChartViewStatic._SYSTEM_SINGLE; }
		}

		private static readonly string _SYSTEM_DOUBLE = "System.Double";

		/// <summary>
		/// <c>Double.</c>
		/// </summary>
		public static string SYSTEM_DOUBLE
		{
			get { return ChartViewStatic._SYSTEM_DOUBLE; }
		}

		private static readonly string _SYSTEM_DECIMAL = "System.Decimal";

		/// <summary>
		/// <c>Decimal.</c>
		/// </summary>
		public static string SYSTEM_DECIMAL
		{
			get { return ChartViewStatic._SYSTEM_DECIMAL; }
		}

		private static readonly string _SYSTEM_DATETIME = "System.DateTime";

		/// <summary>
		/// <c>DateTime.</c>
		/// </summary>
		public static string SYSTEM_DATETIME
		{
			get { return ChartViewStatic._SYSTEM_DATETIME; }
		}

		private static readonly string _SYSTEM_STRING = "System.String";

		/// <summary>
		/// <c>String.</c>
		/// </summary>
		public static string SYSTEM_STRING
		{
			get { return ChartViewStatic._SYSTEM_STRING; }
		}

		private static readonly string _EXPLODED_TRUE = "Exploded=true";

		/// <summary>
		/// 饼图扩展属性.
		/// </summary>
		public static string EXPLODED_TRUE
		{
			get { return ChartViewStatic._EXPLODED_TRUE; }
		}

		private static readonly string _XAXIS = "XAxis";

		/// <summary>
		/// X轴.
		/// </summary>
		public static string XAXIS
		{
			get { return ChartViewStatic._XAXIS; }
		}

		private static readonly string _YAXIS = "YAxis";

		/// <summary>
		/// Y轴.
		/// </summary>
		public static string YAXIS
		{
			get { return ChartViewStatic._YAXIS; }
		}

		private static readonly string _ZAXIS = "ZAxis";

		/// <summary>
		/// Z轴.
		/// </summary>
		public static string ZAXIS
		{
			get { return ChartViewStatic._ZAXIS; }
		}

		private static readonly string _SORT_BY_AXISLABEL = "AxisLabel";

		/// <summary>
		/// 排序项.
		/// </summary>
		public static string SORT_BY_AXISLABEL
		{
			get { return ChartViewStatic._SORT_BY_AXISLABEL; }
		} 

		private static readonly string _RESOURCES_FILE_NAME = ".ChartViewResource";

		/// <summary>
		/// 资源文件名.
		/// </summary>
		public static string RESOURCES_FILE_NAME
		{
			get { return ChartViewStatic._RESOURCES_FILE_NAME; }
		}

		private static readonly string _SERIES_DEFAULT_CUSTOMATTRIBUTES = "DrawingStyle=Cylinder";

		/// <summary>
		/// 默认<c>CUSTOMATTRIBUTES</c>.
		/// </summary>
		public static string SERIES_DEFAULT_CUSTOMATTRIBUTES
		{
			get { return ChartViewStatic._SERIES_DEFAULT_CUSTOMATTRIBUTES; }
		}

		private static readonly string _R_BTN_ANALYSIS_NAME = "rBtnAnalysis";

		/// <summary>
		/// 数据分析.
		/// </summary>
		public static string R_BTN_ANALYSIS_NAME
		{
			get { return ChartViewStatic._R_BTN_ANALYSIS_NAME; }
		}

		private static readonly string _R_BTN_COMPARE_NAME = "rBtnCompare";

		/// <summary>
		/// 数据对比.
		/// </summary>
		public static string R_BTN_COMPARE_NAME
		{
			get { return ChartViewStatic._R_BTN_COMPARE_NAME; }
		}

		private static readonly string _R_BTN_DIRECT_NAME = "rBtnDirect";

		/// <summary>
		/// 数据走势.
		/// </summary>
		public static string R_BTN_DIRECT_NAME
		{
			get { return ChartViewStatic._R_BTN_DIRECT_NAME; }
		}

		private static readonly string _DOT = ".";

		/// <summary>
		/// .
		/// </summary>
		public static string DOT
		{
			get { return ChartViewStatic._DOT; }
		}

		private static readonly string _COLON = ":";

		public static string COLON
		{
			get { return ChartViewStatic._COLON; }
		} 

		private static readonly char _SPLITE_ALLOWED_AXES = ',';

		/// <summary>
		/// ,
		/// </summary>
		public static char SPLITE_ALLOWED_AXES
		{
			get { return ChartViewStatic._SPLITE_ALLOWED_AXES; }
		}

		private static readonly string _INT = "Int";

		/// <summary>
		/// <c>int</c>.
		/// </summary>
		public static string INT
		{
			get { return ChartViewStatic._INT; }
		}

		private static readonly string _DECIMAL = "Decimal";

		/// <summary>
		/// <c>Decimal</c>.
		/// </summary>
		public static string DECIMAL
		{
			get { return ChartViewStatic._DECIMAL; }
		}

		private static readonly string _DATE = "Date";

		/// <summary>
		/// <c>Date</c>.
		/// </summary>
		public static string DATE
		{
			get { return ChartViewStatic._DATE; }
		}

		private static readonly string _STRING = "String";

		/// <summary>
		/// <c>string.</c>
		/// </summary>
		public static string STRING
		{
			get { return ChartViewStatic._STRING; }
		}

		private static readonly string _EXPORT_SUFFIX = "*.bmp|*.bmp";

		/// <summary>
		/// 报表导出后缀名.
		/// </summary>
		public static string EXPORT_SUFFIX
		{
			get { return ChartViewStatic._EXPORT_SUFFIX; }
		}

		private static readonly string _LEFT_BRACKET = "(";

		/// <summary>
		/// (
		/// </summary>
		public static string LEFT_BRACKET
		{
			get { return ChartViewStatic._LEFT_BRACKET; }
		}

		private static readonly string _RIGHT_BRACKET = ")";

		/// <summary>
		/// )
		/// </summary>
		public static string RIGHT_BRACKET
		{
			get { return ChartViewStatic._RIGHT_BRACKET; }
		} 


		#endregion

		#region Keys

		private static readonly string _ITEM_PIE_KEY = "String1";

		public static string ITEM_PIE_KEY
		{
			get
			{
				return ChartViewStatic._ITEM_PIE_KEY;
			}
		}

		private static readonly string _ITEM_LINE_KEY = "String2";

		public static string ITEM_LINE_KEY
		{
			get
			{
				return ChartViewStatic._ITEM_LINE_KEY;
			}
		}

		private static readonly string _ITEM_COLUMN_KEY = "String3";

		public static string ITEM_COLUMN_KEY
		{
			get
			{
				return ChartViewStatic._ITEM_COLUMN_KEY;
			}
		}

		private static readonly string _ERROR_FORMAT_KEY = "String4";

		public static string ERROR_FORMAT_KEY
		{
			get { return ChartViewStatic._ERROR_FORMAT_KEY; }
		}

		private static readonly string _EVENT_CHARTVIEW_LOAD_KEY = "String5";

		public static string EVENT_CHARTVIEW_LOAD_KEY
		{
			get { return ChartViewStatic._EVENT_CHARTVIEW_LOAD_KEY; }
		}

		private static readonly string _EVENT_FRM_AFTER_DATA_APPLY_KEY = "String6";

		public static string EVENT_FRM_AFTER_DATA_APPLY_KEY
		{
			get { return ChartViewStatic._EVENT_FRM_AFTER_DATA_APPLY_KEY; }
		}

		private static readonly string _EVENT_BTNOPTION_CLICK_KEY = "String7";

		public static string EVENT_BTNOPTION_CLICK_KEY
		{
			get { return ChartViewStatic._EVENT_BTNOPTION_CLICK_KEY; }
		}

		private static readonly string _EVENT_CHARTCONFIGURE_LOAD_KEY = "String8";

		public static string EVENT_CHARTCONFIGURE_LOAD_KEY
		{
			get { return ChartViewStatic._EVENT_CHARTCONFIGURE_LOAD_KEY; }
		}

		private static readonly string _EVENT_BTN_APPLICATION_CLICK_KEY = "String9";

		public static string EVENT_BTN_APPLICATION_CLICK_KEY
		{
			get { return ChartViewStatic._EVENT_BTN_APPLICATION_CLICK_KEY; }
		}

		private static string _EVENT_BUT_CHOOSE_CLICK_KEY = "String14";

		public static string EVENT_BUT_CHOOSE_CLICK_KEY
		{
			get { return ChartViewStatic._EVENT_BUT_CHOOSE_CLICK_KEY; }
			set { ChartViewStatic._EVENT_BUT_CHOOSE_CLICK_KEY = value; }
		}

		private static readonly string _TEMPLATE_KEY = "String10";

		public static string TEMPLATE_KEY
		{
			get { return ChartViewStatic._TEMPLATE_KEY; }
		}

		private static readonly string _ERROR_CHOOSE_COUNT_KEY = "String11";

		public static string ERROR_CHOOSE_COUNT_KEY
		{
			get { return ChartViewStatic._ERROR_CHOOSE_COUNT_KEY; }
		}

		private static readonly string _ERROR_RBTN_COMPARE_CHOOSE_COUNT_KEY = "String15";

		public static string ERROR_RBTN_COMPARE_CHOOSE_COUNT_KEY
		{
			get { return ChartViewStatic._ERROR_RBTN_COMPARE_CHOOSE_COUNT_KEY; }
		} 

		private static readonly string _ERROR_CHOOSE_YVALUE_COUNT_KEY = "String12";

		public static string ERROR_CHOOSE_YVALUE_COUNT_KEY
		{
			get { return ChartViewStatic._ERROR_CHOOSE_YVALUE_COUNT_KEY; }
		}

		private static readonly string _ERROR_CHOOSE_XZVALUE_ANALYSIS_KEY = "String13";

		public static string ERROR_CHOOSE_XZVALUE_ANALYSIS_KEY
		{
			get { return ChartViewStatic._ERROR_CHOOSE_XZVALUE_ANALYSIS_KEY; }
		}

		private static readonly string _ERROR_RBTN_COMPARE_CHOOSE_XVALUE_KEY = "String16";

		public static string ERROR_RBTN_COMPARE_CHOOSE_XVALUE_KEY
		{
			get { return ChartViewStatic._ERROR_RBTN_COMPARE_CHOOSE_XVALUE_KEY; }
		}

		private static readonly string _ERROR_LEGAL_PAGEINDEX = "String17";

		public static string ERROR_LEGAL_PAGEINDEX
		{
			get { return ChartViewStatic._ERROR_LEGAL_PAGEINDEX; }
		}

		private static readonly string _ERROR_NULL_CLIENTRULE = "String18";

		public static string ERROR_NULL_CLIENTRULE
		{
			get { return ChartViewStatic._ERROR_NULL_CLIENTRULE; }
		}

		private static readonly string _ERROR_NULL_DATASET = "String19";

		public static string ERROR_NULL_DATASET
		{
			get { return ChartViewStatic._ERROR_NULL_DATASET; }
		}

		private static readonly string _PERCENT = "String20";

		public static string PERCENT
		{
			get { return ChartViewStatic._PERCENT; }
		} 

		#endregion

		#region TEMPLATE

		private static readonly string _TEMPLATE_FOLDER = "\\WinChartTemplate";

		/// <summary>
		/// 模板文件夹.
		/// </summary>
		public static string TEMPLATE_FOLDER
		{
			get { return ChartViewStatic._TEMPLATE_FOLDER; }
		}

		private static readonly string _PATH = "\\";
		
		/// <summary>
		/// 路径.
		/// </summary>
		public static string PATH
		{
			get { return ChartViewStatic._PATH; }
		} 

		private static readonly string _SEARCH_END = "*";

		/// <summary>
		/// 模糊查询.
		/// </summary>
		public static string SEARCH_END
		{
			get { return ChartViewStatic._SEARCH_END; }
		}

		private static readonly char _SPLITE_CHART = '&';

		/// <summary>
		/// 文件分隔名.
		/// </summary>
		public static char SPLITE_CHART
		{
			get { return ChartViewStatic._SPLITE_CHART; }
		}

		private static readonly string _XML_SUFFIX = ".xml";

		/// <summary>
		/// .xml
		/// </summary>
		public static string XML_SUFFIX
		{
			get { return ChartViewStatic._XML_SUFFIX; }
		}

		private static readonly string _DATAGRIDVIEW_TEMPLATE_COLUMN_NAME = "模板名";

		/// <summary>
		/// 模板名.
		/// </summary>
		public static string DATAGRIDVIEW_TEMPLATE_COLUMN_NAME
		{
			get { return ChartViewStatic._DATAGRIDVIEW_TEMPLATE_COLUMN_NAME; }
		} 

		#endregion

		#region Const

		/// <summary>
		/// 图表起点X.
		/// </summary>
		public const int CHART_POINT_X = 10;

		/// <summary>
		/// 图表起点Y.
		/// </summary>
		public const int CHART_POINT_Y = 40;

		/// <summary>
		/// 图表大小X.
		/// </summary>
		public const int CHART_SIZE_X = 500;

		/// <summary>
		/// 图表大小Y.
		/// </summary>
		public const int CHART_SIZE_Y = 333;

		/// <summary>
		/// 最大数据选项.
		/// </summary>
		public const int MAX_SELECTED_ITEMS = 3;

		/// <summary>
		/// 最小数据选项.
		/// </summary>
		public const int MIN_SELECTED_ITEMS = 2;

		/// <summary>
		/// 最大分析项.
		/// </summary>
		public const int MAX_ANALYSIS_ITEMS = 2;

		/// <summary>
		/// 最小分析项.
		/// </summary>
		public const int MIN_ANALYSIS_ITEMS = 1;

		/// <summary>
		/// 比较项.
		/// </summary>
		public const int COMPARE_ITEMS = 3;

		/// <summary>
		/// 最大模板数.
		/// </summary>
		public const int MAX_TEMPLATE_NUM = 10;

		/// <summary>
		/// 数值项数.
		/// </summary>
		public const int YVALUE_NUM = 1;

		/// <summary>
		/// 默认X轴坐标数.
		/// </summary>
		public const int DEFAULT_XCOUNTS = 20;

		/// <summary>
		/// X轴坐标数.
		/// </summary>
		public const int XCOUNTS_15 = 15;

		/// <summary>
		/// X轴坐标数.
		/// </summary>
		public const int XCOUNTS_10 = 10;

		/// <summary>
		/// X轴坐标数.
		/// </summary>
		public const int XCOUNTS_25 = 25;

		/// <summary>
		/// X轴坐标数.
		/// </summary>
		public const int XCOUNTS_30 = 30;

		/// <summary>
		/// 导出图表宽.
		/// </summary>
		public const int EXPORT_SIZE_X = 1280;

		/// <summary>
		/// 导出图表高.
		/// </summary>
		public const int EXPORT_SIZE_Y = 1024;

		/// <summary>
		/// 导出图表X坐标.
		/// </summary>
		public const int EXPORT_LOCATE_X = 10;

		/// <summary>
		/// 导出图表Y坐标.
		/// </summary>
		public const int EXPORT_LOCATE_Y = 10;

		/// <summary>
		/// 导出图表内宽.
		/// </summary>
		public const int EXPORT_WIDTH = 1270;

		/// <summary>
		/// 导出图表内高.
		/// </summary>
		public const int EXPORT_HEIGHT = 1014;

		/// <summary>
		/// 百分比小数位精度.
		/// </summary>
		public const int FLOAT_PRECISION = 4;

		/// <summary>
		/// 百分比转化.
		/// </summary>
		public const int CONVERT_PERCENT = 100;

		#region Colors

		public const int COLOR_COUNTS = 36;

		public const int RED = -65536;

		public const int GREEN = -16744448;

		public const int BLUE = -16776961;

		public const int YELLOW = -256;

		public const int SILVER = -4144960;

		public const int LIME = -16711936;

		public const int CYAN = -16711681;

		public const int FUCHSIA = -65281;

		public const int GRAY = -8355712;

		public const int MAROON = -8388608;

		public const int OLIVE = -8355840;

		public const int COLOR_RED_1 = 255;

		public const int COLOR_GREEN_1 = 192;

		public const int COLOR_BLUE_1 = 192;

		public const int COLOR_RED_2 = 255;

		public const int COLOR_GREEN_2 = 224;

		public const int COLOR_BLUE_2 = 192;

		public const int COLOR_RED_3 = 255;

		public const int COLOR_GREEN_3 = 255;

		public const int COLOR_BLUE_3 = 192;

		public const int COLOR_RED_4 = 192;

		public const int COLOR_GREEN_4 = 255;

		public const int COLOR_BLUE_4 = 192;

		public const int COLOR_RED_5 = 192;

		public const int COLOR_GREEN_5 = 255;

		public const int COLOR_BLUE_5 = 255;

		public const int COLOR_RED_6 = 192;

		public const int COLOR_GREEN_6 = 192;

		public const int COLOR_BLUE_6 = 255;

		public const int COLOR_RED_7 = 255;

		public const int COLOR_GREEN_7 = 192;

		public const int COLOR_BLUE_7 = 255;

		public const int COLOR_RED_8 = 224;

		public const int COLOR_GREEN_8 = 224;

		public const int COLOR_BLUE_8 = 224;

		public const int COLOR_RED_9 = 255;

		public const int COLOR_GREEN_9 = 128;

		public const int COLOR_BLUE_9 = 128;

		public const int COLOR_RED_10 = 255;

		public const int COLOR_GREEN_10 = 192;

		public const int COLOR_BLUE_10 = 128;

		public const int COLOR_RED_11 = 255;

		public const int COLOR_GREEN_11 = 255;

		public const int COLOR_BLUE_11 = 128;

		public const int COLOR_RED_12 = 128;

		public const int COLOR_GREEN_12 = 255;

		public const int COLOR_BLUE_12 = 128;

		public const int COLOR_RED_13 = 128;

		public const int COLOR_GREEN_13 = 255;

		public const int COLOR_BLUE_13 = 255;

		public const int COLOR_RED_14 = 128;

		public const int COLOR_GREEN_14 = 128;

		public const int COLOR_BLUE_14 = 255;

		public const int COLOR_RED_15 = 255;

		public const int COLOR_GREEN_15 = 128;

		public const int COLOR_BLUE_15 = 255;

		public const int COLOR_RED_16 = 255;

		public const int COLOR_GREEN_16 = 128;

		public const int COLOR_BLUE_16 = 0;

		public const int COLOR_RED_17 = 192;

		public const int COLOR_GREEN_17 = 0;

		public const int COLOR_BLUE_17 = 0;

		public const int COLOR_RED_18 = 192;

		public const int COLOR_GREEN_18 = 64;

		public const int COLOR_BLUE_18 = 0;

		public const int COLOR_RED_19 = 192;

		public const int COLOR_GREEN_19 = 192;

		public const int COLOR_BLUE_19 = 0;

		public const int COLOR_RED_20 = 0;

		public const int COLOR_GREEN_20 = 192;

		public const int COLOR_BLUE_20 = 0;

		public const int COLOR_RED_21 = 0;

		public const int COLOR_GREEN_21 = 192;

		public const int COLOR_BLUE_21 = 192;

		public const int COLOR_RED_22 = 0;

		public const int COLOR_GREEN_22 = 0;

		public const int COLOR_BLUE_22 = 192;

		public const int COLOR_RED_23 = 192;

		public const int COLOR_GREEN_23 = 0;

		public const int COLOR_BLUE_23 = 192;

		public const int COLOR_RED_24 = 64;

		public const int COLOR_GREEN_24 = 64;

		public const int COLOR_BLUE_24 = 64;

		public const int COLOR_RED_25 = 128;

		public const int COLOR_GREEN_25 = 64;

		public const int COLOR_BLUE_25 = 0;

		public const int BACK_COLOR_RED = 211;

		public const int BACK_COLOR_GREEN = 223;

		public const int BACK_COLOR_BLUE = 240;

		public const int BORDER_LINE_COLOR_RED = 26;

		public const int BORDER_LINE_COLOR_GREEN = 59;

		public const int BORDER_LINE_COLOR_BLUE = 105;

		public const int BORDER_LINE_WIDTH = 2;

		#endregion

		#endregion
	}
}
