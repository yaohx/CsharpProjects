//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-1-21
// Description	:	ActionType 用户设计报表动作的类型
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace DIYReport.UndoManager
{
	/// <summary>
	/// ActionType 用户设计报表动作的类型
	/// </summary>
	public enum ActionType
	{
		PropertyChange,//属性值发生改变
		Add,//编辑 在对应的集合中增加一个对象
		Remove //编辑 在对应的集合删除一个对象
		
	}
}
