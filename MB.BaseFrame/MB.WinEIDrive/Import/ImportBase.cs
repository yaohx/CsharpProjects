using System;
using System.Data;

namespace MB.WinEIDrive.Import
{
	/// <summary>
	/// ImportBase 数据导入的基本处理。
	/// </summary>
	public class ImportBase
	{
		private ImportProgressEventHandler _DataProgress;
		public event ImportProgressEventHandler DataProgress{
			add{
				_DataProgress +=value;
			}
			remove{
				_DataProgress -=value;
			}
		}

		protected void OnProviderProgress(string fieldName,ref object dataValue) {
			if(_DataProgress != null){
				ImportProgressEventArgs arg = new ImportProgressEventArgs(fieldName,dataValue);
				_DataProgress(this,arg );
				dataValue = arg.NewValue;
			}
		} 
	}

	#region ImportProgressEventHandler and EventArgs...
	public delegate void ImportProgressEventHandler(object sender, ImportProgressEventArgs e);
	
	public class ImportProgressEventArgs : EventArgs {
		private string _FieldName;
		private object _DataValue;
		private object _NewValue;

		public ImportProgressEventArgs(string fieldName,object dataValue){
			_FieldName = fieldName;
			_DataValue = dataValue;
			_NewValue = dataValue;
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
	#endregion ImportProgressEventHandler and EventArgs...

}
