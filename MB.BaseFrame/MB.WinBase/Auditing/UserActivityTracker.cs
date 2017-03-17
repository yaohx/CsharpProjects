using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace MB.WinBase.Auditing {
    /// <summary>
    /// 跟踪用户对模块的使用情况
    /// </summary>
    public class UserActivityTracker {

        private static string _AUDIT_PATH = AppDomain.CurrentDomain.BaseDirectory + @"Audit\";
        private static string _AUDIT_USER_ACTIVITY_TRACKING_FILE = @"UserAtivityTracking_{0}.uat";
        private static object _SynLock = new object();
        private static UserActivityTracker _Instance;


        static UserActivityTracker() {
            if (_Instance == null) {
                lock (_SynLock) {
                    _Instance = new UserActivityTracker();
                }
            }
        }

        /// <summary>
        /// 单实例
        /// </summary>
        public static UserActivityTracker Instance {
            get { return _Instance; }
        }

        #region 字段和属性

        private Action<string> _PublishArchiveOnlineMessage;
        private Action<List<UserActivity>> _ActionArchiveToOnline;
        private List<UserActivity> _UserActivities;//用户当前操作的活动
        private System.Timers.Timer _TimerToArchive;

        /// <summary>
        /// 需要外面注入方法来保存用户日志到线上
        /// </summary>
        public event Action<List<UserActivity>> ActionArchiveToOnline {
            add { _ActionArchiveToOnline += value; }
            remove { _ActionArchiveToOnline -= value; }
        }

        /// <summary>
        /// 保存日志过程中可以订阅保存的实时日志
        /// </summary>
        public event Action<string> PublishArchiveOnlineMessage {
            add { _PublishArchiveOnlineMessage += value; }
            remove { _PublishArchiveOnlineMessage -= value; }
        }

        #endregion

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private UserActivityTracker() {
            _UserActivities = new List<UserActivity>();
            _PublishArchiveOnlineMessage += new Action<string>(msg => {
                MB.Util.TraceEx.Write(msg);
            });

            //1个小时保存一次用户的操作
            _TimerToArchive = new System.Timers.Timer(TimeSpan.FromHours(1).TotalMilliseconds);
            //_TimerToArchive = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _TimerToArchive.Elapsed += new System.Timers.ElapsedEventHandler((obj, eventArgs) => {
                ArchiveToOnline();
            });
            _TimerToArchive.Start();
        }

        #region 外部函数
        /// <summary>
        /// 往活动列表中加活动
        /// </summary>
        /// <param name="activity"></param>
        public void AddActivityToList(UserActivity activity) {
            activity.ACCESS_DATE = DateTime.Now.Date;
            activity.FIRST_ACCESS_TIME = DateTime.Now;
            activity.LAST_ACCESS_TIME = DateTime.Now;
            MB.Util.Model.SysLoginUserInfo userInfo = AppEnvironmentSetting.Instance.CurrentLoginUserInfo;
            if (userInfo != null) {
                activity.USER_ID = userInfo.USER_ID;
                activity.USER_CODE = userInfo.USER_CODE;
                activity.USER_NAME = userInfo.USER_NAME;
            }

            List<UserActivity> uas = new List<UserActivity>();
            uas.Add(activity);
            mergeActivities(uas);
        }

        /// <summary>
        /// 异步保存用户操作至数据库
        /// </summary>
        public void ArchiveToOnlineAsyn() {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((obj) => {
                ArchiveToOnline();
            } ));
        }

        /// <summary>
        /// 将用户操作保存至数据库
        /// </summary>
        public void ArchiveToOnline() {
            Archive(false);
        }

        /// <summary>
        /// 将用户操作保存至本地或者数据库，按照标志位指示操作
        /// <param name="isOfflineSave">True表示保存到本地, False表示保存到数据库</param>
        /// </summary>
        public void Archive(bool isOfflineSave) {
            publishArchiveMessage("Begin保存用户操作开始...");
            try {
                checkLocalDirectorys();
                loadUserActivities();
            }
            catch (Exception ex) {
                publishArchiveMessage("End加载用户操作出错 ：{0}", ex.ToString());
                return;
            }

            try {
                if (!isOfflineSave)
                    onActionArchiveToOnline();
                else
                    archiveOffline();

                publishArchiveMessage("End保存用户操作成功");
                _UserActivities.Clear();//保存成功，清空列表
            }
            catch (Exception ex) {
                publishArchiveMessage("End保存用户操作出错 ：{0}", ex.ToString());
                archiveOffline();
            }
        }
        #endregion


        #region 内部处理函数

        /// <summary>
        /// 输出在存储用户操作活动的时候的处理消息
        /// </summary>
        private void publishArchiveMessage(string message, params string[] formateString) {
            if (_PublishArchiveOnlineMessage != null) {

                try {
                    message = string.Format("[UserActivityArchiveProcess]" + message, formateString);
                }
                catch (System.FormatException) {
                    //如果Format参数个数不正确，不做format
                }
                _PublishArchiveOnlineMessage(message);
            }
        }

        /// <summary>
        ///  检查用户操作日志目录是否存在
        /// </summary>
        private void checkLocalDirectorys() {
            publishArchiveMessage("检查用户操作日志目录开始...");
            DirectoryInfo dirData = new DirectoryInfo(_AUDIT_PATH);
            if (!dirData.Exists)
                dirData.Create();
            publishArchiveMessage("检查用户操作日志目录结束...");
        }

        /// <summary>
        /// 检查是否要上传操作日志
        /// </summary>
        /// <returns></returns>
        private bool isNeedToUpload() {
            DirectoryInfo dirData = new DirectoryInfo(_AUDIT_PATH);
            FileInfo[] files = dirData.GetFiles("*.uat");
            if (files.Length > 0) {
                return true;
            }

            if (_UserActivities.Count > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 加载本地的用户操作
        /// </summary>
        private void loadUserActivities() {
            publishArchiveMessage("加载用户操作日志开始...");
            DirectoryInfo dirData = new DirectoryInfo(_AUDIT_PATH);
            FileInfo[] files = dirData.GetFiles("*.uat");
            foreach (FileInfo file in files) {
                try {
                    byte[] byteActivities = MB.Util.MyConvert.Instance.FileToByte(file.FullName);
                    MB.Util.Serializer.LightweightTextSerializer lts = new Util.Serializer.LightweightTextSerializer();
                    IList userActivites = lts.DeSerializer(typeof(UserActivity), byteActivities);
                    if (userActivites != null && userActivites.Count > 0) {
                        mergeActivities(userActivites);
                    }
                    file.Delete();
                }
                catch (Exception ex) {
                    publishArchiveMessage("加载用户操作日志失败:{0}", ex.ToString());
                }
            }
            publishArchiveMessage("加载用户操作日志完成");
        }

        /// <summary>
        /// 把存储到本地的用户操作日志，合并到内存中
        /// </summary>
        /// <param name="userActiviites"></param>
        private void mergeActivities(IList userActiviites) {
            foreach (UserActivity ua in userActiviites) {
                Predicate<UserActivity> pred = new Predicate<UserActivity>(a => a.MODULE_CODE == ua.MODULE_CODE
                    && a.USER_CODE == ua.USER_CODE
                    && a.FIRST_ACCESS_TIME.Day == ua.FIRST_ACCESS_TIME.Day);

                bool hasSame = _UserActivities.Exists(pred);
                if (hasSame) {
                    UserActivity exitsingUa = _UserActivities.Find(pred);
                    exitsingUa.MODULE_ACCESS_COUNT += ua.MODULE_ACCESS_COUNT;
                    exitsingUa.LAST_ACCESS_TIME = ua.LAST_ACCESS_TIME;
                }
                else {
                    _UserActivities.Add(ua);
                }

            }
        }

        /// <summary>
        /// 保存到本地
        /// </summary>
        private void archiveOffline() {

            if (_UserActivities.Count <= 0) {
                publishArchiveMessage("保存用户操作Offline成功，但是没有记录");
                return;
            }

            try {
                MB.Util.Serializer.LightweightTextSerializer lts = new Util.Serializer.LightweightTextSerializer();
                byte[] uas = lts.Serializer(typeof(UserActivity), _UserActivities);
                string filePath = string.Format(_AUDIT_PATH + _AUDIT_USER_ACTIVITY_TRACKING_FILE, DateTime.Now.ToString("_yyMMdd_HHmmss"));
                publishArchiveMessage("保存用户操作Offline：{0}", filePath);
                MB.Util.MyConvert.Instance.ByteToFile(filePath, uas);
            }
            catch (Exception ex) {
                publishArchiveMessage("保存用户操作Offline失败：{0}", ex.ToString());
            }

            publishArchiveMessage("保存用户操作Offline成功");
        }

        /// <summary>
        /// 触发保存操作日志到其他存储设备
        /// </summary>
        private void onActionArchiveToOnline() {
            if (_UserActivities.Count <= 0) {
                publishArchiveMessage("保存用户操作Online成功,但是没有记录");
                return;
            }

            if (_ActionArchiveToOnline != null) {
                try {
                    _ActionArchiveToOnline(_UserActivities);
                    publishArchiveMessage("保存用户操作Online成功");
                }
                catch (Exception ex) {
                    publishArchiveMessage("保存用户操作Online失败：{0}", ex.ToString());
                    throw;//重新抛出异常，让外层处理异常
                }
            }
            else
                throw new MB.Util.APPException("没有注册ActionArchiveToOnline,无法保存用户操作Online");
        }


        #endregion


    }


}
