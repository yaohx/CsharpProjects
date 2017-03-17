//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	所有数据对象必须继承的抽象基类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms;

using MB.WinBase.Common;
using System.Collections.Generic;
namespace MB.WinClientDefault.UICommand {
    /// <summary>
    /// command 定义。
    /// </summary>
    public class CommandGroups {
        public static readonly XMenuInfo[]
            FileCommands = { new XMenuInfo(UICommands.Individuality,"个性化设置",11),
                             new XMenuInfo(UICommands.SysSetting,"系统选项设置",12),
                              new XMenuInfo(UICommands.MdiSaveLayout,"保存布局",-1,true,13),
                             new XMenuInfo(UICommands.Exit,"退出",-1,true,14)},

            EditCommands =  {new XMenuInfo(UICommands.AddNew,"新增",-1,false,21,Shortcut.CtrlN) ,
							 new XMenuInfo(UICommands.Open,"打开",-1,false,22,Shortcut.CtrlO),
                             //new XMenuInfo(UICommands.Copy,"复制",-1,false,23,Shortcut.CtrlC),
							 new XMenuInfo(UICommands.Delete,"删除",-1,true,25,Shortcut.CtrlD),
                              new XMenuInfo(UICommands.Save,"保存",-1,true,25,Shortcut.CtrlS)
                             },

            SearchCommands = {new XMenuInfo(UICommands.Query,"查询",-1,true,41),
                              new XMenuInfo(UICommands.PrintPreview,"打印预览",-1,true,42),
                              new XMenuInfo(UICommands.DataExport,"导出",43),
                              new XMenuInfo(UICommands.DataImport,"导入",44),
                              new XMenuInfo(UICommands.Refresh,"刷新",-1,true,45) },

            ToolsCommands = {new XMenuInfo(UICommands.FunctionTree,"功能模块树",61),
                             new XMenuInfo(UICommands.OnlineMessage,"信息互动",-1,true,62),
                             new XMenuInfo(UICommands.Calculator,"计算器",63)},

            HelpCommands =  {new XMenuInfo(UICommands.HelpList,"帮助",-1,true,71),
                             new XMenuInfo(UICommands.About,"关于",72)
                            },
            UIMainGroups = {new XMenuInfo("系统(&F)",FileCommands,1,false),
                            new XMenuInfo("编辑(&E)",EditCommands,2,true),
                            new XMenuInfo("查找/分析(&Q)",SearchCommands,4,true),
                            new XMenuInfo("扩展功能(&M)",null,5,true),
                            new XMenuInfo("工具(&T)",ToolsCommands,6,false),
                            new XMenuInfo("帮助(&H)",HelpCommands,7,false)},
            UIMainCommands;

        static CommandGroups() {
            ArrayList commands = MergeLists(FileCommands, EditCommands, SearchCommands, ToolsCommands, HelpCommands, UIMainGroups);

            UIMainCommands = commands.ToArray(typeof(XMenuInfo)) as XMenuInfo[];
        }

        static ArrayList MergeLists(params IList[] lists) {

            ArrayList baseList = new ArrayList();
            foreach (IList list in lists) baseList.AddRange(list);
            return baseList;


        }
    }
 
    /// <summary>
    /// 用户 主要UI 操作Command.
    /// </summary>
    public class UICommands {


        private static readonly Guid UICommandSet = new Guid("{70020645-4804-4029-8F51-565FCA2271F7}");

        public static readonly CommandID AddNew = new CommandID(UICommandSet, (int)UICommandType.AddNew);
        public static readonly CommandID Edit = new CommandID(UICommandSet, (int)UICommandType.Edit);
        public static readonly CommandID Open = new CommandID(UICommandSet, (int)UICommandType.Open);
        public static readonly CommandID Save = new CommandID(UICommandSet, (int)UICommandType.Save);
        public static readonly CommandID Copy = new CommandID(UICommandSet, (int)UICommandType.Copy);
        public static readonly CommandID Delete = new CommandID(UICommandSet, (int)UICommandType.Delete);
        public static readonly CommandID Submit = new CommandID(UICommandSet, (int)UICommandType.Submit);
        public static readonly CommandID CancelSubmit = new CommandID(UICommandSet, (int)UICommandType.CancelSubmit);

        public static readonly CommandID Print = new CommandID(UICommandSet, (int)UICommandType.Print);
        public static readonly CommandID PrintPreview = new CommandID(UICommandSet, (int)UICommandType.PrintPreview);
        public static readonly CommandID Exit = new CommandID(UICommandSet, (int)UICommandType.Exit);

        public static readonly CommandID SysSetting = new CommandID(UICommandSet, (int)UICommandType.SysSetting);
        public static readonly CommandID Individuality = new CommandID(UICommandSet, (int)UICommandType.Individuality);
        public static readonly CommandID HelpList = new CommandID(UICommandSet, (int)UICommandType.HelpList);
        public static readonly CommandID About = new CommandID(UICommandSet, (int)UICommandType.About);
        public static readonly CommandID OnlineMessage = new CommandID(UICommandSet, (int)UICommandType.OnlineMessage);
        public static readonly CommandID Calculator = new CommandID(UICommandSet, (int)UICommandType.Calculator);
        public static readonly CommandID FunctionTree = new CommandID(UICommandSet, (int)UICommandType.FunctionTree);
        public static readonly CommandID Query = new CommandID(UICommandSet, (int)UICommandType.Query);
        
        public static readonly CommandID SearchTemplet = new CommandID(UICommandSet, (int)UICommandType.SearchTemplet);
        public static readonly CommandID Refresh = new CommandID(UICommandSet, (int)UICommandType.Refresh);
        public static readonly CommandID Aggregation = new CommandID(UICommandSet, (int)UICommandType.Aggregation);
        public static readonly CommandID DataExport = new CommandID(UICommandSet, (int)UICommandType.DataExport);
        public static readonly CommandID DataImport = new CommandID(UICommandSet, (int)UICommandType.DataImport);
        public static readonly CommandID MdiSaveLayout = new CommandID(UICommandSet, (int)UICommandType.MdiSaveLayout);
        public static readonly CommandID DynamicAggregation = new CommandID(UICommandSet, (int)UICommandType.DynamicAggregation);

        public static CommandID[] AllCommands = new CommandID[]{AddNew,Edit,Open,Save,Copy,Delete,Submit,CancelSubmit,
                                                                Print,PrintPreview,Exit,SysSetting,Individuality,
                                                                HelpList,About,OnlineMessage,Calculator,FunctionTree,
                                                                Query,SearchTemplet,Refresh,Aggregation,DataExport,DataImport,DynamicAggregation};
        /// <summary>
        /// 把字符转换为CommandID 的格式。
        /// </summary>
        /// <param name="commandID"></param>
        /// <returns></returns>
        public static CommandID ConvertToCommandID(UICommandType commandType) {
            CommandID cmd = Array.Find<CommandID>(AllCommands, o => o.ID == (int)commandType);
            return cmd;
        }
    }
    
}
