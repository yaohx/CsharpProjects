using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using System.Xml;
using System.Reflection;
using MB.Util;
using MB.Util.Model;
using MB.Util.XmlConfig;  
namespace WinTestProject {
    public partial class Form1 : Form,MB.WinBase.IFace.IWaitDialogFormHoster   {
        public Form1() {
            InitializeComponent();

            //int orgId = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("A", 10);
            //MB.RuleBase.BaseRule



        }

        private void button1_Click(object sender, EventArgs e) {
            MB.WinClientDefault.DefaultMdiMainForm mainForm = new MB.WinClientDefault.DefaultMdiMainForm("短线产品订货系统", getDefaultModule());
            mainForm.AfterCreateModuleNode += new MB.WinClientDefault.MdiMainFunctionTreeEventHandle(mainForm_AfterCreateModuleNode);
           // mainForm.Show();            
        }

        void mainForm_AfterCreateModuleNode(object sender, MB.WinClientDefault.MdiMainFunctionTreeEventArgs arg) {
            
        }
        private MB.Util.Model.ModuleTreeNodeInfo[] getDefaultModule() {
            MB.Util.Model.ModuleTreeNodeInfo mInfo = new MB.Util.Model.ModuleTreeNodeInfo(1, "系统设置");
            MB.Util.Model.ModuleTreeNodeInfo mInfo1 = new MB.Util.Model.ModuleTreeNodeInfo(1, "商品管理");
            MB.Util.Model.ModuleTreeNodeInfo mInfo2 = new MB.Util.Model.ModuleTreeNodeInfo(1, "征订主题");
            MB.Util.Model.ModuleTreeNodeInfo mInfo3 = new MB.Util.Model.ModuleTreeNodeInfo(1, "征订主题控制");
            MB.Util.Model.ModuleTreeNodeInfo mInfo4 = new MB.Util.Model.ModuleTreeNodeInfo(1, "征订单");
            MB.Util.Model.ModuleTreeNodeInfo mInfo5 = new MB.Util.Model.ModuleTreeNodeInfo(1, "批量修改征订单");
            MB.Util.Model.ModuleTreeNodeInfo mInfo6 = new MB.Util.Model.ModuleTreeNodeInfo(1, "查询分析");
            MB.Util.Model.ModuleTreeNodeInfo mInfo7 = new MB.Util.Model.ModuleTreeNodeInfo(1, "期货合同");

            mInfo.Commands = new List<MB.Util.Model.ModuleCommandInfo>();
            mInfo.Commands.Add(new MB.Util.Model.ModuleCommandInfo("Open", "MyRuleTestUI.UITestRuleClient,MyRuleTestUI.dll", "MB.WinClientDefault.DefaultViewForm,MB.WinClientDefault.dll"));
            mInfo.Commands.Add(new MB.Util.Model.ModuleCommandInfo("AddNew", "MyRuleTestUI.UITestRuleClient,MyRuleTestUI.dll", "MyRuleTestUI.frmEditTestRule,MyRuleTestUI.dll"));
            MB.Util.Model.ModuleTreeNodeInfo[] ms = { mInfo, mInfo1, mInfo2, mInfo3, mInfo4, mInfo5, mInfo6, mInfo7 };
            return ms;
        }

        private void button2_Click(object sender, EventArgs e) {
          //var tt = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits("UITestRule.xml");
        //   MB.WinBase.LayoutXmlConfigHelper t = new MB.WinBase.LayoutXmlConfigHelper();
            //var tt = XmlConfigHelperEx.Instance.CreateEntityList<ColumnEditCfgInfo>("Name", "", "");
           // var t = gettt<string>(99);
            //var tt = test(); 

  
        }
        private Dictionary<string, T> gettt<T>(int i) {
            return new Dictionary<string, T>();
        }

        private void button2_Click_1(object sender, EventArgs e) {
            //frmTestMain frm = new frmTestMain();
            //frm.ShowDialog(); 
           // Form2 frm = new Form2();
           // frm.ShowDialog(); 

            List<MB.Util.Model.QueryParameterInfo> pars = new List<QueryParameterInfo>();
            pars.Add(new QueryParameterInfo("Name", "站三", DataFilterConditions.Equal));
            pars.Add(new QueryParameterInfo("Code", "站三234525", DataFilterConditions.GreaterOrEqual));
            pars.Add(new QueryParameterInfo("Address", "站三2352525", DataFilterConditions.Equal));

            MB.Util.Serializer.QueryParameterXmlSerializer s = new MB.Util.Serializer.QueryParameterXmlSerializer();
            string xml = s.Serializer(pars.ToArray());
            MB.Util.Model.QueryParameterInfo[] pp = s.DeSerializer(xml);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            if (tabControl1.SelectedTab.Equals(tPageImage)) {
                ucImageFileList1.DataSource = ucEditFileExplorer1.FileList;
            }
        }
        MB.WinClientDefault.WaitDialogForm _WaitFrm;
        private void button3_Click(object sender, EventArgs e) {
            _WaitFrm = new MB.WinClientDefault.WaitDialogForm(this);
            _WaitFrm.ClickCanceled += new EventHandler(frm_ClickCanceled);
            _WaitFrm.ShowWaitForm(this);
        }

        void frm_ClickCanceled(object sender, EventArgs e) {
          //  MB.WinClientDefault.QueryFilter.FrmGetObjectDataAssistant assistant =
          //                    new MB.WinClientDefault.QueryFilter.FrmGetObjectDataAssistant(
          //"MB.ERP.CTrans.UIRule.TrTransDtlDgnUIRule,MB.ERP.CTrans.dll",
          //               string.Format("GetDeliverableDgn;{0}", transPlanGenInfo.FROM_ORG_ID), "");
        }


        #region IWaitDialogFormHoster 成员
        private MB.WinBase.Common.WorkWaitDialogArgs _WaitInfo;
        public MB.WinBase.Common.WorkWaitDialogArgs ProcessState {
            get {
                if (_WaitInfo == null)
                    _WaitInfo = new MB.WinBase.Common.WorkWaitDialogArgs(textBox1.Text, checkBox1.Checked);
                else {
                    _WaitInfo.CurrentProcessContent = textBox1.Text;
                    _WaitInfo.Processed = checkBox1.Checked;
                }
                return _WaitInfo;
            }
        }

        #endregion


        ///// <summary>
        ///// 根据指定的数据类和过虑条件获取数据。
        ///// </summary>
        ///// <param name="dataType">需要获取的数据类型</param>
        ///// <param name="xmlFilterParams">XML 格式的查询过滤条件</param>
        ///// <returns>包含多个Item 的 集合系列化后的XML格式</returns>
        //public string GetObjectAsXml(int dataType, string xmlFilterParams) {
        //    return null; 
        //}
        ///// <summary>
        ///// 根据指定的类型保存数据。
        ///// </summary>
        ///// <param name="dataType">需要保存的数据类型</param>
        ///// <param name="xmlEntitys">需要保存的数据实体类系列化后的XML 格式</param>
        ///// <param name="operationType">数据操作的方式</param>
        ///// <returns>返回受影响的行数,-1表示不成功</returns>
        //public int SaveObjectByXml(int dataType, string xmlEntitys, int operationType) {
        //    return 0;
        //}
    }
    [MB.Aop.InjectionManager]
    public class myClass : System.ContextBoundObject {
        public string Name;
        public myClass() {
            Name = "AAA";

            aaaaa();
        }

        public void text() {
            aaaaa();
        }

        private void aaaaa() {
        }
    }
}
