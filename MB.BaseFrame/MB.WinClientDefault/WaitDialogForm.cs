using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.Common;
using MB.WinBase.IFace;
namespace MB.WinClientDefault {
    /// <summary>
    /// 数据处理等待窗口...
    /// </summary>
    public partial class WaitDialogForm : Form { //DevExpress.XtraBars.Ribbon.RibbonForm  {
       // private const int PROGRESS_BAR_MAX = 100;
        private int _ProcessBarMaxValue;
        private DateTime _ProcessStartTime;
        private string _StatusText;
        private IWaitDialogFormHoster _HosterForm;

        #region 自定义事件...
        private EventHandler _ClickCancel;
        private bool _HasFriendMsg = false;
        public event EventHandler ClickCanceled {
            add {
                _ClickCancel += value;
                butQuit.Enabled = true;
                butQuit.Visible = true;
            }
            remove {
                _ClickCancel -= value;
            }
        }
        private void OnClickCanceled(EventArgs arg) {
            if (_ClickCancel != null) {
                _ClickCancel(this, arg);
            }
        }
        #endregion 自定义事件...

        #region 构造函数...
        /// <summary>
        /// 数据处理等待窗口...
        /// </summary>
        public WaitDialogForm()
            : this(null) {
        }
       /// <summary>
        /// 数据处理等待窗口...
       /// </summary>
        /// <param name="hosterForm">实现 IWaitDialogFormHoster 接口的调用窗口</param>
        public WaitDialogForm(IWaitDialogFormHoster hosterForm) {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            _HosterForm = hosterForm;

            this.ShowInTaskbar = false;
            _ProcessBarMaxValue = 100;
            progressBar1.Maximum = _ProcessBarMaxValue;

            picWaitCursor.Image = Bitmap.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MB.WinClientDefault.Images.wait.gif"));
            this.Load += new EventHandler(WaitDialogForm_Load);

            butQuit.Visible = false;
        }
        #endregion 构造函数...

        #region 控件事件...
        void WaitDialogForm_Load(object sender, EventArgs e) {
            _ProcessStartTime = System.DateTime.Now;
            timer1.Enabled = true;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            Rectangle r = pictureBox1.Bounds;
            ControlPaint.DrawBorder3D(e.Graphics, r, Border3DStyle.RaisedInner);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (progressBar1.Value < _ProcessBarMaxValue)
                progressBar1.Value += 1;
            else
                progressBar1.Value =0;

            DateTime waitTime = System.DateTime.Now;
            _ProcessStartTime = _ProcessStartTime.AddSeconds(0.05);
            TimeSpan t = waitTime.Subtract(_ProcessStartTime);

            labTitle.Text = String.Format("正在处理,请稍待  {0}:{1}:{2}"
                , t.Hours.ToString("00"), t.Minutes.ToString("00"), t.Seconds.ToString("00"));

            labTitle.Refresh(); 
            if (_HosterForm != null && _HosterForm.ProcessState != null){
                if (_HosterForm.ProcessState.Processed)
                    this.Close();
                else {
                    if(_HosterForm.ProcessState.CurrentProcessContent!=null)
                        labContent.Text = _HosterForm.ProcessState.CurrentProcessContent.ToString();
                    labContent.Refresh(); 
                }
            }
        }
        private void butQuit_Click(object sender, EventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
               // timer1.Enabled = false;
                OnClickCanceled(null);
            }
        }
        #endregion 控件事件...

        #region ovrride base method...
        protected override void OnClosing(CancelEventArgs e) {
            try {
                picWaitCursor.Image.Dispose();
                picWaitCursor.Image = null;

            }
            catch { }
            base.OnClosing(e);
        }

        #endregion ovrride base method...


        #region 扩展的public 成员...
        /// <summary>
        /// 显示等待窗口。
        /// 以非模式对话框的形式显示等待窗口。
        /// </summary>
        /// <param name="parent"></param>
        public void ShowWaitForm(IWin32Window parent) {
            if (parent != null)
                this.Show(parent);
            else
                this.Show();

            this.BringToFront();
        }
        /// <summary>
        /// 当前正在处理的内容。
        /// </summary>
        public string StatusText {
            get {
                return _StatusText;
            }
            set {
                _StatusText = value;
            }
        }
        public int ProcessBarMaxValue {
            get {
                return _ProcessBarMaxValue;
            }
            set {
                _ProcessBarMaxValue = value;
            }
        }
        #endregion 扩展的public 成员...

    }
}
