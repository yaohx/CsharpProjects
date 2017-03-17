//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing ; 
using System.Collections ;
using System.Windows.Forms ;

namespace DIYReport.UserDIY
{
	/// <summary>
	/// FocusHandleList 的摘要说明。
	/// </summary>
	public class FocusHandleList : ArrayList  
	{
		private static int FUDGER = GDIHelper.FUDGER ;
		private static int HALF_FUDGER = GDIHelper.FUDGER /2;
		private DesignControl _DesignCtl;
		public FocusHandleList(DesignControl pControl)
		{
			_DesignCtl = pControl;

			//再增加焦点边框
			//_CtlRect = new Rectangle(rect.X - FUDGER ,rect.Y -FUDGER  ,rect.Width + 2*FUDGER  ,rect.Height + 2*FUDGER  );
			create8Focus();
		}
		#region public 方法...
		/// <summary>
		/// 在多选情况下设置当前选择的控件
		/// </summary>
		/// <param name="pIsMain"></param>
		public void SetMainSelected( bool pIsMain){
			foreach(FocusHandleCTL focus in this){
				focus.SetBackColor(pIsMain? Color.Blue:Color.White) ;
 
			}
		}

//		/// <summary>
//		/// 通过鼠标得到鼠标移动下的热点
//		/// </summary>
//		/// <param name="pPoint"></param>
//		/// <returns></returns>
//		public FocusHandle GetFocusByPoint(Point pPoint){
//			int X = pPoint.X ,Y = pPoint.Y ;
//			foreach(FocusHandle focus in this){
//				Rectangle rect = focus.Rect ;
//				bool b = rect.Contains(pPoint);
//				if(b){
//					return focus;
//				}
//			}
//			return null;
//		}
		#endregion public 方法...

		#region Public 属性...
		public DesignControl DesignCtl {
			get{
				return _DesignCtl;
			}
			set{
				_DesignCtl = value;
			}
		}
		#endregion Public 属性...
		
		#region 内部处理...
		private  void create8Focus(){
			for(int i =0 ;i < 8 ;i ++){
				base.Add( new FocusHandleCTL(this,(HandleType)i));
			}
			DockFocusToCtl();

		}
		/// <summary>
		/// 显示焦点
		/// </summary>
		/// <param name="pShow"></param>
		public void ShowFocusHandle(bool pShow){
			//DockFocusToCtl(pShow);
			foreach(FocusHandleCTL ctl in this) {
				ctl.Visible = pShow;
				if(pShow){
					if(!_DesignCtl.Parent.Controls.Contains(ctl)){
						_DesignCtl.Parent.Controls.Add(ctl);  
					}
					ctl.BringToFront();
				}
			}
		}
		/// <summary>
		/// 把焦点
		/// </summary>
		public void DockFocusToCtl(){
			DockFocusToCtl(false);
		}
		public void DockFocusToCtl(bool pVisible){
			Rectangle rect = _DesignCtl.GetOutFocusRect();
			int RECT_X = rect.X,RECT_Y = rect.Y ,
				RECT_WIDTH = rect.Width ,RECT_HEIGHT = rect.Height;
			foreach(FocusHandleCTL ctl in this){
				HandleType type = ctl.FocusType ;
				ctl.Visible = false;
				switch(type){
					case HandleType.LeftTop://顶端靠左
						ctl.Location = new Point(RECT_X ,RECT_Y); 
						break;
					case HandleType.MiddleTop://顶靠中
						ctl.Location = new Point(RECT_X + RECT_WIDTH / 2 - HALF_FUDGER ,RECT_Y); 
						break;
					case HandleType.RightTop://顶靠右
						ctl.Location = new Point(RECT_X + RECT_WIDTH - FUDGER ,RECT_Y); 
						break;
					case HandleType.RightMiddle://右中间
						ctl.Location = new Point(RECT_X + RECT_WIDTH - FUDGER ,RECT_Y + RECT_HEIGHT/2 -HALF_FUDGER ); 
						break;
					case HandleType.RightBottom://底靠右
						ctl.Location = new Point(RECT_X + RECT_WIDTH - FUDGER ,RECT_Y + RECT_HEIGHT - FUDGER); 
						break;
					case HandleType.BottomMiddle ://底靠中
						ctl.Location = new Point(RECT_X + RECT_WIDTH / 2 - HALF_FUDGER ,RECT_Y + RECT_HEIGHT - FUDGER); 
						break;
					case HandleType.LeftBottom://底靠左
						ctl.Location = new Point(RECT_X ,RECT_Y + RECT_HEIGHT - FUDGER); 
						break;
					case HandleType.LeftMiddle ://左中间
						ctl.Location = new Point(RECT_X ,RECT_Y + RECT_HEIGHT/2 -HALF_FUDGER); 
						break;
				}
				ctl.Visible = pVisible;
			}
		}
		#endregion 内部处理...
	}
}
