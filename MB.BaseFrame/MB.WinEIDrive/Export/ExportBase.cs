using System;
using System.Data;

namespace MB.WinEIDrive.Export {
	/// <summary>
	/// ImportBase 数据导入的基本处理。
	/// </summary>
	public class ExportBase {
		private ExportProgressEventHandler _DataProgress;
		public event ExportProgressEventHandler DataProgress{
			add{
				_DataProgress +=value;
			}
			remove{
				_DataProgress -=value;
			}
		}

		protected void OnProviderProgress(string fieldName,int rowIndex,ref object dataValue) {
			if(_DataProgress != null){
				ExportProgressEventArgs arg = new ExportProgressEventArgs(fieldName,rowIndex,dataValue);
				_DataProgress(this,arg );
				dataValue = arg.NewValue;
			}
		} 

		private ExportColumnsEventHandler _ColumnProgress;
		public event ExportColumnsEventHandler ColumnProgress{
			add{
				_ColumnProgress +=value;
			}
			remove{
				_ColumnProgress -=value;
			}
		}

		protected void OnColumnProgress(ExportColumnsEventArgs arg) {
			if(_ColumnProgress != null){
				_ColumnProgress(this,arg );
			}
		} 

		private object _DataSource;
		public object DataSource{
			get{
				return _DataSource;
			}
			set{
				_DataSource = value;
			}
		}
	}

	#region ExportProgressEventHandler and EventArgs...
	public delegate void ExportProgressEventHandler(object sender, ExportProgressEventArgs e);
	
	public class ExportProgressEventArgs : EventArgs {
		private string _FieldName;
		private int _RowIndex;
		private object _DataValue;
		private object _NewValue;

		public ExportProgressEventArgs(string fieldName,int rowIndex,object dataValue){
			_FieldName = fieldName;
			_DataValue = dataValue;
			_NewValue = dataValue;
			_RowIndex = rowIndex;
		}

		#region public 属性...
		public string FieldName{
			get{
				return _FieldName;
			}
		}
		public object DataValue{
			get{
				return _DataValue;
			}
		}
		public int RowIndex{
			get{
				return _RowIndex;
			}
		}
		public object NewValue{
			get{
				return _NewValue;
			}
			set{
				_NewValue = value;
			}
		}
		#endregion public 属性...
		
	}  
	#endregion ExportProgressEventHandler and EventArgs...

	#region ExportColumnsEventHandler ...
	public delegate void ExportColumnsEventHandler(object sender,ExportColumnsEventArgs e);

	public class ExportColumnsEventArgs : EventArgs{
		private string _FieldName;
		private string _Caption;
		private bool _IsHide;
		private int _ColumnWidth;

		public ExportColumnsEventArgs(string caption,string fieldName,int columnWidth){
			_Caption = caption;
			_FieldName = fieldName;
			_IsHide = false;
			_ColumnWidth = columnWidth;
		}
		public string Caption{
			get{
				return _Caption;
			}
			set{
				_Caption = value;
			}
		}
		public string FieldName{
			get{
				return _FieldName;
			}
			set{
				_FieldName = value;
			}
		}
		public bool IsHide{
			get{
				return _IsHide;
			}
			set{
				_IsHide = value;
			}
		}
		public int ColumnWidth{
			get{
				return _ColumnWidth;
			}
			set{
				_ColumnWidth = value;
			}
		}
	}
	#endregion ExportColumnsEventHandler ...

}
