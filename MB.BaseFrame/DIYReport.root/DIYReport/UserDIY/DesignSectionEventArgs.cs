using System;

namespace DIYReport.UserDIY
{
	/// <summary>
	///  事件委托申明
	///  备注：除非明确的知道需求，最好不要这样设计事件的参赛 
	/// </summary>
	public delegate void DesignSectionEventHandler(object sender,DesignSectionEventArgs  e);
	/// <summary>
	/// DesignSectionEventArgs Design Section 事件定义。
	/// </summary>
	public class DesignSectionEventArgs : System.EventArgs 
	{
		#region 变量定义...
		//设计的Sction 
		private DesignSection _Section;
		//插入Section 在集合中的位置
		private int _InsertPosition;
		#endregion 变量定义...

		#region 构造函数...
		public DesignSectionEventArgs()
		{
		}
		public DesignSectionEventArgs(int pInsertPosition,DesignSection pSection) {
			_InsertPosition = pInsertPosition;
			_Section = pSection;
		}
		#endregion 构造函数...

		#region Public 属性...
		public DesignSection Section{
			get{
				return  _Section;
			}
			set{
				_Section = value;
			}
		}
		public int InsertPosition{
			get{
				return _InsertPosition;
			}
			set{
				_InsertPosition = value;
			}
		}
		#endregion Public 属性...
	}
}
