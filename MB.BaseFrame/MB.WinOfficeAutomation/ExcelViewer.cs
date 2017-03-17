using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;

namespace MB.WinOfficeAutomation {
    /// <summary>
    /// Excel查看控件
    /// </summary>
    public partial class ExcelViewer : Control {

        private const int SWP_FRAMECHANGED = 0x0020;
        private const int SWP_DRAWFRAME = 0x20;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int SWP_NOZORDER = 0x4;
        private const int GWL_STYLE = (-16);
        private const int WS_CAPTION = 0xC00000;
        private const int WS_THICKFRAME = 0x40000;
        private const int WS_SIZEBOX = WS_THICKFRAME;
        private const int SWP_NOACTIVATE = 0x0010;

        private Excel.Application _XsApplication;
        private Process _Process;
        private IntPtr _ExcelHandle;
        private bool _Initialized = false;
        private Excel.Workbooks _Workbooks;
        private Excel.Workbook _Workbook;
        private CultureInfo _ThreadCulture;

        #region win32 Import

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExcelViewer() {
            this.Resize += new EventHandler(ExcelViewer_Resize);
        }

        /// <summary>
        /// 当前的工作薄是否已经被保存
        /// </summary>
        public bool Saved {
            get { return _Workbook.Saved; }
        }

        /// <summary>
        /// 被打开的Excel Application
        /// </summary>
        public Excel.Application Application {
            get { return _XsApplication; }
        }


        /// <summary>
        /// 打开当前的ExcelFile
        /// </summary>
        /// <param name="filename">要打开的全部路径</param>
        public void OpenFile(string filename) {
            if (!_Initialized)
                init();
            _Workbooks = _XsApplication.Workbooks;
            _Workbook = _Workbooks.Open(filename);
        }

        /// <summary>
        /// 关闭当前的Excel
        /// </summary>
        public void CloseExcel() {
            closeExcel();
        }

        /// <summary>
        /// 强制杀除当前Excel
        /// </summary>
        public void KillExcel() {
            if (_Process != null)
                _Process.Kill();
        }

        /// <summary>
        /// 保存当前的工作薄
        /// </summary>
        public void SaveActiveWorkbook() {
            _Workbook.Save();
        }

        /// <summary>
        /// 把当前工作薄保存到文件上
        /// </summary>
        /// <param name="filename">需要保存的文件</param>
        public void SaveActiveWorkbookAs(string filename) {
            _Workbook.SaveAs(filename);
        }

        /// <summary>
        /// 赋值当前工作薄到文件中
        /// </summary>
        /// <param name="filename">指定的文件</param>
        public void SaveCopyOfActiveWorksheetAs(string filename) {
            _Workbook.SaveCopyAs(filename);
        }

        #region 内部函数
        private void ExcelViewer_Resize(object sender, EventArgs e) {
            if (_ExcelHandle != IntPtr.Zero) {
                SetWindowPos(_ExcelHandle, new IntPtr(0), 0, 0, this.Width, this.Height, SWP_NOACTIVATE);
            }
        }

        /// <summary>
        /// 初始化excel
        /// </summary>
        private void init() {
            _ThreadCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");

            _XsApplication = new Excel.Application();
            _XsApplication.WindowState = Excel.XlWindowState.xlNormal;
            _XsApplication.Visible = true;

            _ExcelHandle = new IntPtr(_XsApplication.Hwnd);
            SetParent(_ExcelHandle, this.Handle);
            int lngStyle = GetWindowLong(_ExcelHandle, GWL_STYLE);
            lngStyle = lngStyle ^ WS_CAPTION;
            lngStyle = lngStyle ^ WS_THICKFRAME;
            lngStyle = lngStyle ^ WS_SIZEBOX;
            SetWindowLong(_ExcelHandle, GWL_STYLE, lngStyle);
            SetWindowPos(_ExcelHandle, new IntPtr(0), 0, 0, this.Width, this.Height, SWP_FRAMECHANGED);
            int pid = 0;
            GetWindowThreadProcessId(_ExcelHandle, out pid);
            _Process = Process.GetProcessById(pid);
            _Initialized = true;
        }

        private void closeExcel() {
            try {
                if (_Workbook != null) {
                    _Workbook.Close(false);
                    Marshal.ReleaseComObject(_Workbooks);
                    Marshal.ReleaseComObject(_Workbook);
                }
                if (_XsApplication != null) {
                    _XsApplication.Quit();
                    Marshal.ReleaseComObject(_XsApplication);
                    _Process.WaitForExit();
                }
            }
            catch (Exception ex) { 
                
            }
            finally {
                _Workbook = null;
                _Workbooks = null;
                _XsApplication = null;
                if (_Process != null && !_Process.HasExited)
                    _Process.Kill();
                if (_ThreadCulture !=  null)
                    Thread.CurrentThread.CurrentCulture = _ThreadCulture;
                _Initialized = false;
            }
        }



        protected override void OnHandleDestroyed(EventArgs e) {
            CloseExcel();
            base.OnHandleDestroyed(e);
        }
        #endregion
    }
}
