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
using System.Collections.Generic;
using System.Windows.Forms;

namespace DIYReport.UserDIY
{
	#region 比较器...
	class DesignObjSortLeft : IComparer  {

		int IComparer.Compare( Object x, Object y )  {
			DesignControl xObj = x as DesignControl;
			DesignControl yObj = y as DesignControl;
			return( (new CaseInsensitiveComparer()).Compare( xObj.Left ,yObj.Left ) );
		}

	}
	class DesignObjSortTop : IComparer  {

		int IComparer.Compare( Object x, Object y )  {
			DesignControl xObj = x as DesignControl;
			DesignControl yObj = y as DesignControl;
			return( (new CaseInsensitiveComparer()).Compare( xObj.Top  ,yObj.Top ) );
		}

	}
	#endregion 比较器...
	/// <summary>
	/// DesignControlList 用户
	/// </summary>
	public class DesignControlList : ArrayList , DIYReport.Interface.IActionParent  
	{
		private DIYReport.UserDIY.DesignSection  _Section;
		private DIYReport.ReportModel.RptSingleObjList _DataObj;  
		 
		
		#region 构造函数...
		public DesignControlList(DIYReport.UserDIY.DesignSection  pSection ){
			_Section = pSection;
			_DataObj = pSection.DataObj.RptObjList;
			foreach(DIYReport.Interface.IRptSingleObj  obj in _DataObj){
				DesignControl ctl = new DesignControl( obj );
				this.Add(ctl);
			}
		 
		}
		#endregion 构造函数...
		private DIYReport.UndoManager.UndoMgr _UndoMgr{
			get{
				return _Section.SectionList.Report.UndoMgr ; 
			}
		}

		#region public 属性...
		public DIYReport.UserDIY.DesignSection Section{
			get{
				return _Section;
			}
			set{
				_Section = value;
			}
		}

		#endregion public 属性...

		#region Public 方法...

		#region Public 方法(位置大小调整相关)...	
		/// <summary>
		/// 把所有选中的控件向左边靠齐（紧挨在一起)
		/// </summary>
		public void DockToLeft(){
			this.Sort(new DesignObjSortLeft());
			int right = 0 ;
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					right = right + ctl.Width  >this.Section.Width ?this.Section.Width  - ctl.Width:right ;
					if(right>0){
						ctl.DataObj.Location = new Point(right,ctl.DataObj.Location.Y);
					}
					right = ctl.DataObj.Rect.Right ;
				}
			}
			ShowFocusHandle(true);
		}
		/// <summary>
		/// 把所有选中的控件向顶端靠齐（紧挨在一起) 
		/// </summary>
		public void DockToTop(){
			this.Sort(new DesignObjSortLeft());
			int bottom = 0 ;
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					bottom = bottom + ctl.Height   >this.Section.Height ?this.Section.Height   - ctl.Height : bottom ;
					if(bottom>0){
						ctl.DataObj.Location = new Point(ctl.DataObj.Location.X,bottom);
					}
					bottom = ctl.DataObj.Rect.Bottom;
				}
			}
			ShowFocusHandle(true);
		}
		#endregion Public 方法(位置大小调整相关)...		

		#region 编辑相关...
		public DesignControl Add(DesignControl pControl){
			pControl.DeControlList = this;
			//增加到要处理的集合中
			base.Add(pControl); 
			//这个是增加到界面上
			_Section.DeControls.Add(pControl);
			return pControl;
		}
		/// <summary>
		/// 把当前选择的控件删除掉
		/// </summary>
		public void RemoveSelectedControl() {
			int i = 0;
			ArrayList unList = new ArrayList();
			while(i < this.Count){ 
				if(this[i]==null){
					break;
				}
				 DesignControl ctl = this[i] as DesignControl;
				if(ctl.IsSelected){
					//unList.Add( ctl.DataObj.Clone());
					removeCtl(ctl);
				}
				else{
					i++;
				}
			}
			//_UndoMgr.Store("移除报表控件",unList,this,DIYReport.UndoManager.ActionType.Remove); 
		}
		private void removeCtl(DesignControl pCtl){
			//判断是否为图片文件并销毁图片
			if(pCtl.DataObj.Type == DIYReport.ReportModel.RptObjType.Image){
				DIYReport.ReportModel.RptObj.RptPictureBox pic = pCtl.DataObj  as DIYReport.ReportModel.RptObj.RptPictureBox;
				if(pic.Image!=null){
					pic.Image.Dispose();
				}
			}
			//删除控件数据集合中的
			_DataObj.Remove(pCtl.DataObj);
			//从Section界面中删除8个热点
			foreach(FocusHandleCTL focus in pCtl.FocusList ){
				_Section.DeControls.Remove(focus);
			}
			pCtl.FocusList.Clear();
			//删除Section 上的控件集合（界面上的控件）
			_Section.DeControls.Remove(pCtl);
			//删除当前集合中的对该对象的引用
			base.Remove (pCtl);
		}
		/// <summary>
		/// 通过鼠标创建控件
		/// </summary>
		/// <param name="pFirst"></param>
		/// <param name="pLast"></param>
		public void CreateControl(DIYReport.ReportModel.RptObjType pType , Point pFirst,Point pLast){
			
			CreateControl(null,true,pType,pFirst,pLast);

			
		}
		public void CreateControl(string pDispText,bool pChangeRect,DIYReport.ReportModel.RptObjType pType , Point pFirst,Point pLast){
			Rectangle rect = PublicFun.ChangeMousePointToRect(pFirst,pLast); 

			Rectangle mousRect = pChangeRect?_Section.RectangleToClient(rect):rect; 

			DIYReport.Interface.IRptSingleObj  data = _DataObj.AddByType(pType,pDispText,_Section.DataObj) ;
			if(data==null)
				return;

			DesignControl ctl = new DesignControl(data );
			ctl.BringToFront();

			data.BeginUpdate(); 
			data.Location = new Point(mousRect.Left ,mousRect.Top );
			data.Size = mousRect.Size;
			data.EndUpdate();
			
			ctl.IsSelected = true;
			ctl.IsMainSelected = true;
			this.Add(ctl);
			//判断是否通过鼠标来创建
			if(pChangeRect){
                //ArrayList unList = new ArrayList();
                //object cUnData = data.Clone();
                //DIYReport.TrackEx.Write(cUnData!=null,"由于程序特殊的需要，该报表对象需要提供Clone() 的方法。");
                //if(cUnData!=null){
                //    unList.Add(cUnData); 
                //    _UndoMgr.Store("新建报表控件",unList,this,DIYReport.UndoManager.ActionType.Add); 
                //}
			}
			DesignEnviroment.CurrentRptObj =  data;
		}

		/// <summary>
		/// 在指定的位置报表打印和浏览需要的控件
		/// </summary>
		/// <param name="pType"></param>
		/// <param name="pObjList"></param>
		/// <param name="pLocation"></param>
		/// <param name="CtlWidth"></param>
		/// <param name="pColCount"></param>
		public void CreateRptObjByList(DIYReport.ReportModel.RptObjType pType ,IList pObjList,Point pLocation,
										Size  pCtlSize,int pColCount){
			int hasCreate = 0;
			int rowCount = 0;
			foreach(string sizeName in pObjList){
				Point firstP;
				Point lastP;
				if(hasCreate > pColCount){
					hasCreate = 0;
					rowCount ++;
				}
				//计算动态创建控件的两个点
				firstP = new Point(pLocation.X + hasCreate * pCtlSize.Width,pLocation.Y + rowCount * pCtlSize.Height  );
				int lastY = pLocation.Y + (rowCount + 1) * pCtlSize.Height;
				//如果Section 的高度不够，需要重新分配
				if(lastY > _Section.DataObj.Height){
					//暂时先不处理自动调整Section 的高度
					//_Section.DataObj.Height = lastY;
				}
				int lastX = pLocation.X + (hasCreate + 1) * pCtlSize.Width;
				lastX = lastX > 0?lastX+1:lastX;
				lastY = lastY > 0?lastY+1:lastY;
				lastP = new Point(lastX,lastY );
				CreateControl(sizeName,false,DIYReport.ReportModel.RptObjType.Text,firstP,lastP);
				hasCreate ++;
			}
			ShowFocusHandle(true);
		}

		#endregion 编辑相关...

		/// <summary>
		/// 通过鼠标选择控件 
		/// </summary>
		/// <param name="pRect"></param>
		public void SelectCtlByMouseRect(Point pFirst,Point pLast){
			//把鼠标选择的坐标转换成从左上到右下的选择方式
			Rectangle rect = PublicFun.ChangeMousePointToRect(pFirst,pLast); 
			SelectCtlByMouseRect(rect);
		}
		 
		/// <summary>
		/// 通过鼠标选择控件 ，
		/// </summary>
		/// <param name="pRect"></param>
		public void SelectCtlByMouseRect(Rectangle pRect){
			Rectangle mousRect = _Section.RectangleToClient(pRect); 
			DesignControl lastSelectCtl = null ;
			foreach(DesignControl ctl in this){
				Rectangle rect = Rectangle.Intersect( mousRect, new Rectangle(ctl.Location,ctl.Size)) ;
				bool b = rect!=Rectangle.Empty ; 
				if(b){
					ctl.IsMainSelected = false;
					ctl.IsSelected = b;
					lastSelectCtl = ctl;
				}
				else{
					if(DesignEnviroment.PressCtrlKey!=true && DesignEnviroment.PressShiftKey!=true){
						ctl.IsSelected = false;	
					}
				}
			}
			if(lastSelectCtl!=null){
				lastSelectCtl.IsMainSelected = true;

				DesignEnviroment.CurrentRptObj =  lastSelectCtl.DataObj; 
			}
		}

		#region 焦点控制相关...
//		/// <summary>
//		/// 通过鼠标得到选择控件下的控件焦点
//		/// </summary>
//		/// <param name="pPoint"></param>
//		/// <returns></returns>
//		public FocusHandle GetFocusByMouse(Point pPoint){
//			foreach(DesignControl ctl in this.Values){
//				bool b = ctl.MouseIsOverFocus(pPoint);
//				if(b){
//					FocusHandleList focus = ctl.FocusList ;
//					FocusHandle hand = focus.GetFocusByPoint(pPoint); 
//					if(hand!=null){
//						return hand;
//					}
//				}
//			}
//			return null;
//		}
		/// <summary>
		/// 根据点得到该点下的控件
		/// </summary>
		/// <param name="pPoint"></param>
		/// <returns></returns>
		public DesignControl GetControlByPoint(Point pPoint){
			foreach(DesignControl ctl in this){
				bool b = ctl.MouseIsOver(pPoint);
				if(b){
					return ctl;
				}
			}
			return null;
		}
		/// <summary>
		/// 当用户点击控件焦点时显示拖动的边框
		/// </summary>
		/// <param name="pFocus"></param>
		/// <param name="pPoint"></param>
		public void DrawDragFrameByFocus(FocusHandleCTL pFocus,Point pFirst,Point pLast){
			int width = pLast.X - pFirst.X ,height = pLast.Y - pFirst.Y ;
			int SEP =1;
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					Rectangle dt = new Rectangle(new Point(ctl.Left + SEP,ctl.Top + SEP) ,ctl.Size);
					dt = ReSetCtlDragFrame(pFocus.FocusType,dt,width,height);
//					//在拖动的时候显示控件
//					if(dt.Left <=0) dt.Location = new Point(0,dt.Top);
//					if(dt.Top <=0) dt.Location = new Point(dt.Left ,0);
//					if(dt.Right >=_Section.Width) dt.Width = _Section.Width - dt.Left ;
//					if(dt.Bottom  >=_Section.Height)dt.Height = _Section.Height -dt.Top ;

					Rectangle rect = _Section.RectangleToScreen(dt);
					GDIHelper.DrawReversibleRect(rect,FrameStyle.Thick);  
				}
			}
		}
		/// <summary>
		/// 当用户点击焦点拖动时候改变控件的大小
		/// </summary>
		/// <param name="pFocus"></param>
		/// <param name="pPoint"></param>
		public void MoveByDragFocus(FocusHandleCTL pFocus,Point pFirst,Point pLast){
			int width = pLast.X - pFirst.X ,height = pLast.Y - pFirst.Y ;
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					Rectangle dt = new Rectangle(new Point(ctl.Left ,ctl.Top) ,ctl.Size);
					dt = ReSetCtlDragFrame(pFocus.FocusType,dt,width,height);
//					if(dt.Left <=0) dt.Location = new Point(0,dt.Top);
//					if(dt.Top <=0) dt.Location = new Point(dt.Left ,0);
//					if(dt.Right >=_Section.Width) dt.Width = _Section.Width - dt.Left ;
//					if(dt.Bottom  >=_Section.Height)dt.Height = _Section.Height -dt.Top ;

					DIYReport.Interface.IRptSingleObj  dataObj =  ctl.DataObj;
					dataObj.BeginUpdate(); 
					dataObj.Location = dt.Location ;
					dataObj.Size = dt.Size ;
					dataObj.EndUpdate();
					 
				}
			}
		}

		#endregion 焦点控制相关...

		/// <summary>
		/// 画拖动的边框
		/// </summary>
		public void DrawReversibleFrame(Point pFirst,Point pLast){
			int width = pLast.X - pFirst.X ;
			int height = pLast.Y - pFirst.Y ;
			int SEP = 1;
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					Rectangle dt = ctl.DisplayRectangle;
					int left = ctl.Left +  width + SEP;
					int top = ctl.Top + height + SEP;
					//限制鼠标的拖动范围
					left = left < 0 ?0:left;
					top = top <0 ? 0:top;
					left = left +ctl.Width > _Section.Width? _Section.Width - ctl.Width : left;
					top = top + ctl.Height  > _Section.Height?_Section.Height - ctl.Height : top;

					dt.Location = new Point(left,top);
					Rectangle rect = _Section.RectangleToScreen(dt);
					GDIHelper.DrawReversibleRect(rect,FrameStyle.Thick);  
				}
			}
		}

		/// <summary>
		///  把选择的控件拖动到指定的位置
		/// </summary>
		/// <param name="pFirst"></param>
		/// <param name="pLast"></param>
		public void MoveToPoint(Point pFirst,Point pLast){
			int width = pLast.X - pFirst.X ;
			int height = pLast.Y - pFirst.Y ;
           // List<DesignControl> unList = new List<DesignControl>();
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					int left = ctl.Left +  width ;
					int top = ctl.Top + height ;
                    
					//unList.Add(ctl.DataObj.WiseClone()); 

					//限制鼠标的拖动范围
					left = left < 0? 0:left;
					top = top < 0? 0:top;
					left = left +ctl.Width > _Section.Width? _Section.Width - ctl.Width : left;
					top = top + ctl.Height  > _Section.Height?_Section.Height - ctl.Height : top;

					ctl.DataObj.Location = new Point(left,top);
                    //ctl.Visible = false;
                    //unList.Add(ctl);
				}
			}
            //foreach (DesignControl ctl in unList)
            //    ctl.Visible = true;
			//_UndoMgr.Store("移动报表控件",unList,this,DIYReport.UndoManager.ActionType.PropertyChange);    

		}
		#region 通过方向键调整位置和大小...
		/// <summary>
		/// 处理方向键按下的时候
		/// </summary>
		/// <param name="pKeyValue"></param>
		public void ProcessArrowKeyDown(int pKeyValue,bool pIsSize){
			if(pIsSize){
				ProcessResizeByKey(pKeyValue);
			}
			else{
				ProcessReLocationByKey(pKeyValue);
			}
		}

		private void ProcessReLocationByKey(int pKeyValue){
			if(pKeyValue ==37 || pKeyValue==38 || pKeyValue==39||pKeyValue==40){
				int mX = 0,mY = 0;
				if(pKeyValue ==37 ){mX = -1;}
				if(pKeyValue ==39 ){mX = +1;}
				if(pKeyValue == 38){mY = -1;}
				if(pKeyValue == 40){mY = 1;}
				foreach(DesignControl ctl in this){
					if(ctl.IsSelected){
						int left = ctl.DataObj.Location.X + mX;
						int top = ctl.DataObj.Location.Y + mY;
						//限制移动动范围 
						left = left < 0 ?0:left;
						top = top <0 ? 0:top;
						left = left +ctl.Width > _Section.Width? _Section.Width - ctl.Width : left;
						top = top + ctl.Height  > _Section.Height?_Section.Height - ctl.Height : top;
						 
						ctl.DataObj.Location = new Point(left,top);		
						ctl.FocusList.ShowFocusHandle(true);
					}
				}
			}
		}
		private void ProcessResizeByKey(int pKeyValue){
			if(pKeyValue ==37 || pKeyValue==38 || pKeyValue==39||pKeyValue==40){
				int mX = 0,mY = 0,width = 0,height = 0;
				if(pKeyValue ==37 ){
					mX = -1;
					width = 1;}
				if(pKeyValue ==39 ){
					mX = 0;
					width = 1;}
				if(pKeyValue == 38){
					mY = -1;
					height = 1;}
				if(pKeyValue == 40){
					mY = 0;
					height = 1;}

				foreach(DesignControl ctl in this){
					if(ctl.IsSelected){
						int left = ctl.DataObj.Location.X + mX;
						int top = ctl.DataObj.Location.Y + mY;
						//限制移动动范围
						left = left < 0 ?0:left;
						top = top <0 ? 0:top;
						left = left +ctl.Width > _Section.Width? _Section.Width - ctl.Width : left;
						top = top + ctl.Height  > _Section.Height?_Section.Height - ctl.Height : top;
						
						ctl.DataObj.BeginUpdate(); 
						ctl.DataObj.Location = new Point(left,top);		
						Size s = ctl.DataObj.Size;
						ctl.DataObj.Size = new Size(s.Width + width,s.Height + height); 
						ctl.DataObj.EndUpdate();
						ctl.FocusList.ShowFocusHandle(true);
					}
				}
			}
		}
		#endregion 通过方向键调整位置和大小...
		/// <summary>
		/// 显示选择控件的热点
		/// </summary>
		/// <param name="pShow"></param>
		public void ShowFocusHandle(bool pShow){
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					ctl.FocusList.ShowFocusHandle(pShow);
				}
			}
		}

		/// <summary>
		/// 设置当前鼠标下的控件 
		/// </summary>
		/// <param name="pCtl"></param>
		public void SetMainSelected(DesignControl pCtl){
			foreach(DesignControl ctl in this){
				ctl.IsMainSelected = false;
			}
			pCtl.IsMainSelected = true;
		}
		/// <summary>
		/// 获取当前选择的设计控件。
		/// </summary>
		/// <returns></returns>
		public IList GetSelectedCtlsData(){
			ArrayList dataLst = new ArrayList();
			foreach(DesignControl ctl in this){
				if(!ctl.IsSelected)
					continue;
				dataLst.Add(ctl.DataObj);
			}
			return dataLst;
		}
		/// <summary>
		/// 得到当前鼠标下的选择的控件
		/// </summary>
		/// <returns></returns>
		public DesignControl GetMainSelectedCtl(){
			foreach(DesignControl ctl in this){
				if(ctl.IsMainSelected){
					return ctl;
				}
			}
			return null;
		}
		/// <summary>
		/// 设置所有的控件为不选择状态
		/// </summary>
		public void SetAllNotSelected(){
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					ctl.FocusList.ShowFocusHandle(false);
					ctl.IsSelected = false;
				}
			}
		}

		/// <summary>
		/// 格式化选择的控件
		/// </summary>
		/// <param name="pCurrentCtl"></param>
		/// <param name="pType"></param>
		public void FormatCtl(FormatCtlType pType){
			DesignControl ctl = GetMainSelectedCtl();
			if(ctl!=null){
				FormatCtl(new Rectangle(ctl.Location,ctl.Size) ,pType);
			}
		}
		public void FormatCtl(Rectangle pRect,FormatCtlType pType){
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					DIYReport.Interface.IRptSingleObj  dataObj = ctl.DataObj;
					switch(pType){
						case FormatCtlType.Left  :
							dataObj.Location = new Point(pRect.Left,ctl.Top)  ;
							break;
						case FormatCtlType.Top :
							dataObj.Location = new Point(ctl.Left, pRect.Top );
							//ctl.Top = pRect.Top ;
							break;
						case FormatCtlType.Right  :
							dataObj.Location = new Point(pRect.Right - ctl.Width ,ctl.Top);
							//ctl.Left  = pRect.Right - ctl.Width  ;
							break;
						case FormatCtlType.Bottom  :
							dataObj.Location = new Point(ctl.Left,pRect.Top + pRect.Height  - ctl.Height);
							//ctl.Top = pRect.Top + pRect.Height  - ctl.Height  ;
							break;
						case FormatCtlType.Width  :
							dataObj.Size = new Size( pRect.Width,ctl.Height);
							//ctl.Width  = pRect.Width  ;
							break;
						case FormatCtlType.Height  :
							dataObj.Size = new Size(ctl.Width , pRect.Height);
							//ctl.Height  = pRect.Height  ;
							break;
						default:
							break;
					}
				}
			}
			ShowFocusHandle(true); 
		}


		/// <summary>
		/// 
		/// </summary>
		public void Invalidate(){
			foreach(DesignControl ctl in this){
				if(ctl.IsSelected){
					ctl.Invalidate(); 
				}
			}
		}
		public Rectangle ReSetCtlDragFrame(HandleType pType , Rectangle pRect,int pWidth,int pHeight){
			int left = pRect.Left ,top = pRect.Top  ;
			Rectangle newRect;
			switch(pType){
				case HandleType.LeftTop :
					newRect = new Rectangle(left + pWidth,top + pHeight ,pRect.Width - pWidth, pRect.Height - pHeight); 
					break;
				case HandleType.MiddleTop :
					newRect = new Rectangle(left,top + pHeight ,pRect.Width , pRect.Height - pHeight); 
					break;
				case HandleType.RightTop :
					newRect = new Rectangle(left, top + pHeight ,pRect.Width  + pWidth , pRect.Height - pHeight); 
					break;
				case HandleType.RightMiddle :
					newRect = new Rectangle(left ,top,pRect.Width  + pWidth   , pRect.Height ); 
					break;
				case HandleType.RightBottom :
					newRect = new Rectangle(left ,top ,pRect.Width  + pWidth  , pRect.Height + pHeight ); 
					break;
				case HandleType.BottomMiddle :
					newRect = new Rectangle(left ,top , pRect.Width  , pRect.Height + pHeight); 
					break;
				case HandleType.LeftBottom :
					newRect = new Rectangle(left +  pWidth ,top , pRect.Width -  pWidth,  pRect.Height + pHeight ); 
					break;
				case HandleType.LeftMiddle  :
					newRect = new Rectangle(left +  pWidth, top, pRect.Width -  pWidth , pRect.Height); 
					break;
				default:
					newRect = pRect;
					break;
			}
			return newRect;
		}
		#endregion Public 方法...

		#region 内部处理函数...

		#endregion 内部处理函数...

		#region 实现 IActionParent 接口的方法 ...
		//实现该接口是为了实现用户操作的 Undo 和Redo 的操作
		public void SetPropertyValue(ref IList pObjList){
			//在改变对象属性的撤消动作中 目前只处理报表对象的位置和大小
			ArrayList proList = new ArrayList();
			foreach(DIYReport.Interface.IRptSingleObj  obj in _DataObj){
				for(int i =0;i < pObjList.Count ; i++){
					DIYReport.Interface.IRptSingleObj utem = pObjList[i] as DIYReport.Interface.IRptSingleObj;
					if(utem!=null && obj.Name == utem.Name){
						proList.Add(obj.WiseClone());
						obj.BeginUpdate();
						obj.Location = utem.Location ;
						obj.Size = utem.Size;
						obj.EndUpdate();
					}
				}
			}
			pObjList = proList;
		}
		// 在撤消操作中，如果碰到删除的操作，需要在撤消时重新增加回来
		public void Add(IList pObjList){
			DesignControl last = null;
			int selectHeight = this.Section.DataObj.Height;
 
			for(int i =0;i < pObjList.Count;i++){
				DIYReport.Interface.IRptSingleObj data =  pObjList[i] as DIYReport.Interface.IRptSingleObj ;
				data = data.Clone() as DIYReport.Interface.IRptSingleObj;

				if(data.Location.Y + data.Size.Height > selectHeight)
					selectHeight = data.Location.Y + data.Size.Height;
				
				DesignControl ctl = new DesignControl( data as DIYReport.Interface.IRptSingleObj );

				data.EndUpdate();

				ctl.IsSelected = true;
				//ctl.IsMainSelected = true;
				this._DataObj.Add(data);
				this.Add(ctl);
				last = ctl;
			}
			if(last!=null){
				SetMainSelected(last);
				ShowFocusHandle(true); 
			}
			if(selectHeight > this.Section.DataObj.Height )
				this.Section.DataObj.Height = selectHeight;
		}
		//在撤消操作中，如果碰到增加的操作，需要在撤消时删除掉
		public void Remove(IList pObjList){
			int i = 0;
			while(i < this.Count){ 
				if(this[i]==null){
					break;
				}
				bool b = false;
				DesignControl ctl = this[i] as DesignControl;
				for(int j =0;j < pObjList.Count;j++){
					DIYReport.Interface.IRptSingleObj data = pObjList[j] as DIYReport.Interface.IRptSingleObj;
					if(data!=null && ctl.DataObj.Name == data.Name){
						removeCtl(ctl);
						b = true;
					}
				}
				 if(!b){
					i++;
				}
			}
		}
		#endregion 实现 IActionParent 接口的方法 ...
	}
}
