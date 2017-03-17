//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	MdiForm 窗口处理项。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinClientDefault.UICommand;
using MB.WinBase.Common; 

namespace MB.WinClientDefault.Menu {
    /// <summary>
    /// XtraMenu 菜单创建公共处理方法。
    /// </summary>
    [MB.Aop.InjectionManager]
    class XtraMenuManager : System.ContextBoundObject {
        private UICommandExecutor _CommandExecutor;
        private DevExpress.XtraBars.BarManager _BarManager;
        private DevExpress.XtraBars.Bar _BarMainMenu;
        private DevExpress.XtraBars.Bar _BarEdit;
        private Dictionary<DevExpress.XtraBars.BarButtonItem, XMenuInfo> _AllCreateButtonItems; 

        #region 构造函数...
       /// <summary>
       /// 
       /// </summary>
       /// <param name="commandExecutor"></param>
       /// <param name="barManager"></param>
       /// <param name="mainMenu"></param>
       /// <param name="barTools"></param>
        public XtraMenuManager(UICommandExecutor commandExecutor, DevExpress.XtraBars.BarManager barManager,
                             DevExpress.XtraBars.Bar mainMenu, DevExpress.XtraBars.Bar barTools) {
            _BarManager = barManager;
            _BarMainMenu = mainMenu;
            _BarEdit = barTools;
            _CommandExecutor = commandExecutor;

            _AllCreateButtonItems = new Dictionary<DevExpress.XtraBars.BarButtonItem, XMenuInfo>();
        }
        #endregion 构造函数...

        #region 菜单显示控制处理相关...
        /// <summary>
        /// MDI 窗口菜单可操作设置。
        /// </summary>
        /// <param name="activeForm"></param>
        public void EnabledToolsMenu(MB.WinBase.IFace.IForm activeForm) {
            
        }
        /// <summary>
        /// 根据创建的命令格式化操作菜单项。
        /// </summary>
        /// <param name="moduleInfo"></param>
        public void RefreshToolsButtonItem(MB.Util.Model.ModuleTreeNodeInfo moduleInfo) {
            foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateButtonItems.Keys) {
                butItem.Enabled = true;
            }
            if (!string.IsNullOrEmpty(moduleInfo.RejectCommands)) {
                string[] rejects = moduleInfo.RejectCommands.Split(',');
                foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateButtonItems.Keys) {
                    XMenuInfo menuInfo = _AllCreateButtonItems[butItem];
                    UICommandType cType = (UICommandType)menuInfo.CommandID.ID;
                    if (Array.IndexOf(rejects, cType.ToString()) >= 0)
                        butItem.Enabled = false;
                }
            }

            MB.WinBase.IFace.IForm activeForm = _CommandExecutor.HostMdiMainForm.GetActiveMdiChildForm();
            if (activeForm == null) return;

            //如果是分析界面的话要屏蔽掉操作功能菜单项。
            MB.WinBase.IFace.IClientRule editRule = activeForm.ClientRuleObject as MB.WinBase.IFace.IClientRule;
            if (editRule != null) return ;

            foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateButtonItems.Keys) {
                XMenuInfo menuInfo = _AllCreateButtonItems[butItem];
                var info = CommandGroups.EditCommands.FirstOrDefault(o=>o.CommandID.Equals(menuInfo.CommandID));
                if (info != null) {
                    butItem.Enabled = false;
                    continue;
                }
                if (menuInfo.CommandID.Equals(UICommands.DataImport)) {
                    butItem.Enabled = false;
                    continue;
                }
            }
        }
        #endregion 菜单显示控制处理相关...

        #region 窗口菜单创建处理相关...
        /// <summary>
        /// 创建默认的菜单。
        /// </summary>
        public void CreateDefaultMenu() {
            _BarManager.Images = Images.ImageListHelper.Instance.CreateXCommandsImage();   
            createMainMenu();
        }

        //创建主菜单。
        //如果还存在多级菜单就修改为递归的方式来实现。
        private void createMainMenu() {
            IOrderedEnumerable<XMenuInfo> menus = CommandGroups.UIMainGroups.OrderBy<XMenuInfo, int>(o => o.Index);
            foreach (XMenuInfo info in menus) {
                DevExpress.XtraBars.BarSubItem subItem = new DevExpress.XtraBars.BarSubItem();
                subItem.Caption = info.Description;
                this._BarManager.Items.Add(subItem);

                this._BarMainMenu.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(subItem));
 
                if (info.Childs == null || info.Childs.Length == 0) continue;

                IOrderedEnumerable<XMenuInfo> childsMenu = info.Childs.OrderBy<XMenuInfo, int>(o => o.Index);
                foreach (XMenuInfo child in childsMenu) {
                    DevExpress.XtraBars.BarButtonItem btnItem = createDefaultButtonItem(child);
                    this._BarManager.Items.Add(btnItem);
                    subItem.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(btnItem, child.BeginGroup));
                    //如果是EditGroup 或者是SearchGroup 需要对应的Item 加上BarEdit 上。
                    if (info.ShowToCommandBar) {
                        bool beginGroup = child.BeginGroup;
                        DevExpress.XtraBars.LinkPersistInfo cmdButton = new DevExpress.XtraBars.LinkPersistInfo(btnItem, beginGroup);
                        btnItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                        cmdButton.UserPaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph; 
                        this._BarEdit.LinksPersistInfo.Add(cmdButton); 
                    }
                }
            }
        }
        //创建一般的button 菜单。
        private DevExpress.XtraBars.BarButtonItem createDefaultButtonItem(XMenuInfo menuInfo) {
            DevExpress.XtraBars.BarButtonItem btnItem = new DevExpress.XtraBars.BarButtonItem();
            btnItem.ImageIndex = menuInfo.ImageIndex;
            btnItem.Caption = menuInfo.Description;
            btnItem.Hint = menuInfo.Description;
            btnItem.Tag = menuInfo;

            if (menuInfo.Shortcut != System.Windows.Forms.Shortcut.None) {
                btnItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(menuInfo.Shortcut);
            }
            btnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(btnItem_ItemClick);

            _AllCreateButtonItems.Add(btnItem, menuInfo);
            return btnItem;
        }
        //
        void btnItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (e.Item.Tag == null) return;

            try {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor()) {
                    XMenuInfo menuInfo = e.Item.Tag as XMenuInfo;
                    _CommandExecutor.ExecCommand(menuInfo.CommandID);
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);   
            }
        }
        #endregion 窗口菜单创建处理相关...
    }
}

