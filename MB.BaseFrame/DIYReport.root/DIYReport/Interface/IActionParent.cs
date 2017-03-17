//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-1-21
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections ;

namespace DIYReport.Interface
{
	/// <summary>
	/// IActionParent 用户操作的
	/// </summary>
	public interface IActionParent
	{
		 void SetPropertyValue(ref IList pObjList);
		 void Add(IList pObjList);
		 void Remove(IList pObjList);
	}
}
