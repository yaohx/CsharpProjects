using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault.Ctls {
    /// <summary>
    /// 模块评语。
    /// </summary>
    public partial class ucBfModuleComment : UserControl {
        private MB.WinBase.IFace.IViewGridForm _ViewGridForm;
        private MB.WinBase.IFace.IBfModuleCommentClient _CommentClient;
        private MB.WinClientDefault.Common.BfModuleCommentHelper _CommentHelper;
        private List<MB.Util.Model.BfModuleCommentInfo> _LstComments;
        private const int MAX_COMMENT_LENGTH = 4 * 1024 * 1024;
        private const int MIN_COMMENT_LENGTH = 151;
        private static readonly string RTF_LINE = "----------------------------------------------------------------------------------------------- \n";
        private static readonly string COMMENT_APPLICATION_IDENTITY = "MBERP";
        private static readonly string ADMINISTRATOR_USER =  MB.BaseFrame.SOD.ADMINISTRATOR_USER_CODE;

        /// <summary>
        ///  模块发表评语
        /// </summary>
        /// <param name="viewGridForm"></param>
        public ucBfModuleComment(MB.WinBase.IFace.IViewGridForm viewGridForm) {
            InitializeComponent();

           
            cobCommentType.Items.Add(MB.WinClientDefault.Properties.Resources.BfModlueComment_COMMENT_TYPE1);
            cobCommentType.Items.Add(MB.WinClientDefault.Properties.Resources.BfModlueComment_COMMENT_TYPE2);
            cobCommentType.Items.Add(MB.WinClientDefault.Properties.Resources.BfModlueComment_COMMENT_TYPE3);
            cobCommentType.Items.Add(MB.WinClientDefault.Properties.Resources.BfModlueComment_COMMENT_TYPE4);
            cobCommentType.Items.Add(MB.WinClientDefault.Properties.Resources.BfModlueComment_COMMENT_TYPE5);
            cobCommentType.SelectedIndex = 0;

            _ViewGridForm = viewGridForm;

            _LstComments = new List<MB.Util.Model.BfModuleCommentInfo>();
            _CommentHelper = new MB.WinClientDefault.Common.BfModuleCommentHelper();
            _CommentClient = _CommentHelper.CreateCommentClient();

            this.Load += new EventHandler(ucBfModuleComment_Load);
            tButClear.Enabled = string.Compare(ADMINISTRATOR_USER, MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_ID, true) == 0; ;     
        }

        #region 界面操作事件...
        void ucBfModuleComment_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;
            try {
                loadModuleComment();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }


        private void tButRefresh_Click(object sender, EventArgs e) {
            try {
                loadModuleComment();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
        private void butSubmitComment_Click(object sender, EventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this.ParentForm)) {
                try {
                    checkInputData();

                    MB.Util.Model.BfModuleCommentInfo newInfo = createNewCommentInfo();
                    if (_CommentClient == null)
                        throw new MB.Util.APPException("需要配置 IBfModuleCommentClient 客户端");

                    _CommentClient.AddObject(newInfo);
                    appendModuleComment(newInfo);

                    rxtInputComment.Clear();
                    rxtInputComment.Focus();
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                }
            }
        }

        #endregion 界面操作事件...

        #region 内部函数处理...
        private void loadModuleComment() {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this.ParentForm)) {
                if (_CommentClient == null)
                    throw new MB.Util.APPException("需要配置 IBfModuleCommentClient 客户端");

                if (string.IsNullOrEmpty(_ViewGridForm.ClientRuleObject.ModuleTreeNodeInfo.PriID))
                    throw new MB.Util.APPException("该模块对应的权限 PriID 没有配置,请先配置");  

                MB.Util.Common.QueryParameterHelper parHelper = new MB.Util.Common.QueryParameterHelper();
                parHelper.AddParameterInfo("APPLICATION_IDENTITY", COMMENT_APPLICATION_IDENTITY, MB.Util.DataFilterConditions.Equal);
                parHelper.AddParameterInfo("MODULE_IDENTITY", _ViewGridForm.ClientRuleObject.ModuleTreeNodeInfo.PriID, MB.Util.DataFilterConditions.Equal);
                _LstComments = _CommentClient.GetObjects(parHelper.ToArray());

                _LstComments.Sort(new Comparison<MB.Util.Model.BfModuleCommentInfo>(commentCreateDateCompare));

                rxtModuleComment.Clear(); 
                foreach (var commentInfo in _LstComments)
                    appendModuleComment(commentInfo);
            }
            
        }
        private int commentCreateDateCompare(MB.Util.Model.BfModuleCommentInfo x, MB.Util.Model.BfModuleCommentInfo y) {
            return Comparer<DateTime>.Default.Compare(x.CREATE_DATE, y.CREATE_DATE);
        }
        //追加评语
        private void appendModuleComment(MB.Util.Model.BfModuleCommentInfo commentInfo) {
            rxtModuleComment.SelectionStart = 0;
            string rtf = System.Text.Encoding.UTF8.GetString(commentInfo.COMMENT_CONTENT);
            rxtModuleComment.SelectedRtf = rtf;
            rxtModuleComment.SelectionStart = 0;
            rxtModuleComment.SelectionColor = Color.Blue;
            rxtModuleComment.SelectedText = string.Format(MB.WinClientDefault.Properties.Resources.BfModuleComment_COMMENT_TITLE, commentInfo.USER_ID, commentInfo.COMMENT_TYPE, commentInfo.CREATE_DATE.ToString());
            rxtModuleComment.SelectionStart = 0;
            rxtModuleComment.SelectionColor = Color.Black;
            rxtModuleComment.SelectedText = RTF_LINE;
        }

        //核查当前编辑的评语是否合法
        private bool checkInputData() {
            if (rxtInputComment.Rtf.Length > MAX_COMMENT_LENGTH)
                throw new MB.Util.APPException(string.Format(MB.WinClientDefault.Properties.Resources.BfModuleComment_MAX_COMMENT_LENGTH, MAX_COMMENT_LENGTH), MB.Util.APPMessageType.DisplayToUser);
            if (string.IsNullOrEmpty(rxtInputComment.Text) && rxtInputComment.Rtf.Length <= MIN_COMMENT_LENGTH )
                throw new MB.Util.APPException(MB.WinClientDefault.Properties.Resources.BfModuleComment_MIN_COMMENT_CHECK, MB.Util.APPMessageType.DisplayToUser);

            if (string.IsNullOrEmpty(_ViewGridForm.ClientRuleObject.ModuleTreeNodeInfo.PriID))
                throw new MB.Util.APPException("该模块对应的权限 PriID 没有配置,请先配置");  
            return true;
        }
        //发布一个新的评语
        private MB.Util.Model.BfModuleCommentInfo createNewCommentInfo() {
            MB.Util.Model.BfModuleCommentInfo newInfo = new MB.Util.Model.BfModuleCommentInfo();
            newInfo.COMMENT_TYPE = cobCommentType.Text;
            newInfo.USER_ID = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_ID;
            newInfo.COMMENT_CONTENT = System.Text.Encoding.UTF8.GetBytes(rxtInputComment.Rtf);
            newInfo.MODULE_IDENTITY = _ViewGridForm.ClientRuleObject.ModuleTreeNodeInfo.PriID;
            newInfo.APPLICATION_IDENTITY = COMMENT_APPLICATION_IDENTITY;
            newInfo.CREATE_DATE = System.DateTime.Now;
            return newInfo;

        }
        #endregion 内部函数处理...

        private void tButClear_Click(object sender, EventArgs e) {
            DialogResult re = MB.WinBase.MessageBoxEx.Question("是否决定清除当前模板的所有评语");
            if (re != DialogResult.Yes) return;

            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this.ParentForm)) {
                int v = _CommentClient.ClearObject(COMMENT_APPLICATION_IDENTITY, _ViewGridForm.ClientRuleObject.ModuleTreeNodeInfo.PriID);
                if (v > 0)
                    rxtModuleComment.Clear(); 
            }
        }

    }
}
