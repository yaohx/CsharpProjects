using System;
using System.Collections ; 

namespace DIYReport.UndoManager
{
	/// <summary>
	/// ActionInfo 存储每次操作的详细操作对象
	/// </summary>
	public class ActionInfo
	{
		#region 内部变量定义...
		private string _Description;
		private IList _ObjList;
		private DIYReport.Interface.IActionParent _ListenerParent;
		private ActionType _ActionType;
		#endregion 内部变量定义...

		#region 构造函数...

		#region 覆盖基类的方法...
		public override string ToString() {
			return _Description;
		}

		#endregion 覆盖基类的方法...

		public ActionInfo()
		{
			 
		}
		public ActionInfo(string pDesc,IList pObjList,DIYReport.Interface.IActionParent pListenerParent,
						   ActionType pActionType){
			_Description = pDesc;
			_ObjList = pObjList;
			_ListenerParent = pListenerParent;
			_ActionType = pActionType;

		}
		#endregion 构造函数...

		#region Public 属性...
		public string Description{
			get{
				return _Description;
			}
			set{
				_Description = value;
			}
		}
		public IList ObjList{
			get{
				return _ObjList;
			}
			set{
				_ObjList = value;
			}
		}
		public DIYReport.Interface.IActionParent ListenerParent{
			get{
				return _ListenerParent;
			}
			set{
				_ListenerParent = value;
			}
		}
		public ActionType ActionType{
			get{
				return _ActionType;
			}
			set{
				_ActionType = value;
			}
		}
		#endregion Public 属性...

	}
}
