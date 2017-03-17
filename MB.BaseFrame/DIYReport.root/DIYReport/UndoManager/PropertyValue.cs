using System;

namespace DIYReport.UndoManager
{
	/// <summary>
	/// PropertyValue 的摘要说明。
	/// </summary>
	[Serializable]
	public class PropertyValue {
		private string _PropName;
		private object _PropVal;

		public PropertyValue(string propName, object propVal) {
			_PropName = propName;
			_PropVal = propVal;
		}
		public string PropertyName {
			get {
				return _PropName;
			}
			set {
				_PropName = value;
			}
		}
		public object Value {
			get {
				return _PropVal;
			}
			set {
				_PropVal = value;
			}
		}
	}
}
