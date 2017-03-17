using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// MDI 窗口必须要实现的接口。
    /// </summary>
    public interface  IMdiMainForm {
        /// <summary>
        /// 退出应用程序。
        /// </summary>
        void Exit();
        /// <summary>
        /// 获取当前活动的窗口。
        /// </summary>
        /// <returns></returns>
        IForm GetActiveMdiChildForm();
        /// <summary>
        /// 显示功能模块树。
        /// </summary>
        void ShowFunctionTree();
        /// <summary>
        /// 显示在线消息。
        /// </summary>
        void ShowOnlineMessage();
        /// <summary>
        /// 显示个人设置。
        /// </summary>
        void ShowUserSetting();

        /// <summary>
        /// 显示系统设置。
        /// </summary>
        void ShowApplicationSetting();

        /// <summary>
        /// 保持MDI 布局
        /// </summary>
        void SaveMdiLayput();

        /// <summary>
        /// 验证列.
        /// </summary>
        void ValidatedColumns(IForm activeForm);

        /// <summary>
        /// 验证当前活动编辑的窗口.
        /// </summary>
        /// <param name="activeEditForm"></param>
        void ValidatedEditForm(IForm activeEditForm);
    }
}
