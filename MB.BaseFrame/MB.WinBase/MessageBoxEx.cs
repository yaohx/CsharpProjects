//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	MessageBoxEx 用户提示消息框。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

using System.Windows.Forms;
namespace MB.WinBase {
    /// <summary>
    /// MessageBoxEx 用户提示消息框。
    /// </summary>
    public class MessageBoxEx {
        #region 常量定义...
        private static readonly string MSG_OPERATE_NOTE = "操作提示";
        #endregion 常量定义...

        #region private construct function...
        /// <summary>
        /// 
        /// </summary>
        private MessageBoxEx() { }
        #endregion private construct function...

        #region Public 方法...

        /// <summary>
        /// 显示用户消息。
        /// </summary>
        /// <param name="pMsg"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Show(string message, params string[] paras) {
            string msg = message;
            return Show(msg, MessageBoxIcon.Information, false);
        }
        /// <summary>
        /// 显示用户消息。
        /// </summary>
        /// <param name="pMsg"></param>
        /// <param name="msgIco"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Show(string message, MessageBoxIcon msgIco, params string[] paras) {
            string msg = message;
            return Show(msg, msgIco, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pMsg"></param>
        /// <param name="mustShowByMsgBox"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Show(string message, bool mustShowByMsgBox) {
            return Show(message, MessageBoxIcon.Information, mustShowByMsgBox);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pMsg"></param>
        /// <param name="mustShowByMsgBox">是否必须用MessageBox来显示</param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Show(string message, MessageBoxIcon msgIco, bool mustShowByMsgBox) {
            bool b = false;
            if (!b) {
                return MessageBox.Show(message, MSG_OPERATE_NOTE, MessageBoxButtons.OK, msgIco);
            }
            else {
                return System.Windows.Forms.DialogResult.OK;
            }
        }
        public static System.Windows.Forms.DialogResult Show(IWin32Window pOwner, string message) {
            return Show(pOwner, message, MessageBoxIcon.Information, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pOwner"></param>
        /// <param name="pMsg"></param>
        /// <param name="mustShowByMsgBox">是否必须用MessageBox来显示</param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Show(IWin32Window owner, string message, MessageBoxIcon msgIco, bool mustShowByMsgBox) {
            string txt = MSG_OPERATE_NOTE;
            if (owner.GetType().Name == "Form") {
                Form frm = owner as Form;
                txt = frm.Text;
            }
            bool b = false;
            if (!b) {
                return MessageBox.Show(owner, message, txt, MessageBoxButtons.OK, msgIco);
            }
            else {
                return System.Windows.Forms.DialogResult.OK;
            }
        }
        /// <summary>
        /// 提问消息。
        /// </summary>
        /// <param name="pMsg"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Question(string message, params string[] paras) {
            string msg = message;
            return MessageBox.Show(msg, MSG_OPERATE_NOTE, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pOwner"></param>
        /// <param name="pMsg"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Question(IWin32Window owner, string message) {
            string txt = MSG_OPERATE_NOTE;
            if (owner.GetType().Name == "Form") {
                Form frm = owner as Form;
                txt = frm.Text;
            }
            return MessageBox.Show(owner, message, txt, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion Public 方法...


    }
}
