using System;
using System.Collections;
using System.Reflection;

namespace DIYReport.UndoManager
{
	/// <summary>
	/// UndoMgr undo/redo操作管理。
	/// </summary>
	public class UndoMgr
	{
		private Stack _RedoStack;
		private Stack _UndoStack;
		public UndoMgr()
		{
			_RedoStack = new Stack();
			_UndoStack = new Stack();

		}
		#region 自定义事件...
		private System.EventHandler _UndoMgrChanged;
		public event System.EventHandler UndoMgrChanged{
			add{
				_UndoMgrChanged +=value;
			}
			remove{
				_UndoMgrChanged -=value;
			}
		}
		private void onUndoMgrCHanged(System.EventArgs arg){
			if(_UndoMgrChanged!=null){
				_UndoMgrChanged(this,arg);
			}
		}
		#endregion 自定义事件...

		#region Public 属性...
		/// <summary>
		/// 判断是否可以进行Undo的操作
		/// </summary>
		public bool CanUndo{
			get{
				return _UndoStack.Count > 0 ;
				}
		}
		/// <summary>
		/// 判断是否可以进行Redo的操作
		/// </summary>
		public bool CanRedo{
			get{
				return _RedoStack.Count > 0;
			}
		}
		#endregion Public 属性...

		#region public 方法...
		/// <summary>
		/// 移除Redo 和Undo stack 中的所有成员
		/// </summary>
		public void Clear(){
			_RedoStack.Clear();
			_UndoStack.Clear();
		}
		/// <summary>
		/// 把用户设计操作的动作存储到Undo 对应的stack 中
		/// </summary>
		/// <param name="pAction"></param>
		public void Store(ActionInfo pAction){
			_UndoStack.Push(pAction);
			onUndoMgrCHanged(null);
		}
		public void Store(string pDescription,IList  pObjList,
			DIYReport.Interface.IActionParent pActionParent,ActionType pActType){

			Store(new ActionInfo(pDescription,pObjList,pActionParent,pActType));
			
			_RedoStack.Clear();
			
		}
		/// <summary>
		/// 撤消操作
		/// </summary>
		public void Undo(){
			if(CanUndo){
				ActionInfo actInfo = _UndoStack.Pop() as ActionInfo ;
				ActionType aType = actInfo.ActionType ;
				switch(aType){
					case ActionType.PropertyChange :
						//setPropertyValue(actInfo.ObjList,actInfo.PropertyList);
						IList  pro = actInfo.ObjList;
						//_RedoStack.Push( actInfo);
						actInfo.ListenerParent.SetPropertyValue( ref pro); 
						_RedoStack.Push( new ActionInfo(actInfo.Description,pro,actInfo.ListenerParent ,actInfo.ActionType));
						break;
					case ActionType.Add :
						_RedoStack.Push( actInfo);
						actInfo.ListenerParent.Remove(actInfo.ObjList);   
						break;
					case ActionType.Remove  :
						_RedoStack.Push( actInfo);
						actInfo.ListenerParent.Add(actInfo.ObjList);   
						break;
					default:
						//Debug 
						break;

				}
				onUndoMgrCHanged(null);
				//_RedoStack.Push( actInfo);
			}
		}
		/// <summary>
		/// 重复操作
		/// </summary>
		public void Redo(){
			if(CanRedo ){
				ActionInfo actInfo = _RedoStack.Pop() as ActionInfo ;
				ActionType aType = actInfo.ActionType ;
				switch(aType){
					case ActionType.PropertyChange :
						//setPropertyValue(actInfo.ObjList,actInfo.PropertyList);
						IList  pro = actInfo.ObjList;
						actInfo.ListenerParent.SetPropertyValue( ref pro ); 
						_UndoStack.Push( new ActionInfo(actInfo.Description,pro,actInfo.ListenerParent ,actInfo.ActionType));
						break;
					case ActionType.Add :
						_UndoStack.Push( actInfo);
						actInfo.ListenerParent.Add (actInfo.ObjList);   
						break;
					case ActionType.Remove  :
						_UndoStack.Push( actInfo);
						actInfo.ListenerParent.Remove(actInfo.ObjList);   
						break;
					default:
						//Debug 
						break;

				}
				onUndoMgrCHanged(null);
			}
		}

		#endregion public 方法...
	}
}
