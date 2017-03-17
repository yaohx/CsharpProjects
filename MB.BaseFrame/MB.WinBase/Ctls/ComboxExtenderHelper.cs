using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.Util;
namespace MB.WinBase.Ctls {
    /// <summary>
    /// 常用Combox 方法处理。
    /// </summary>
    public class ComboxExtenderHelper {
        private static readonly string COMBOX_SAVE_FILE = AppDomain.CurrentDomain.BaseDirectory + "ComboxSaveSession.ini";
        private static readonly string COMBOX_SAVE_SESSION = "ComboxSaveSession";

        private const int MAX_ITEMS = 5;

        private ComboxExtenderHelper() {
        }

        #region public static function...
        /// <summary>
        /// 把用户选择的项存储起来。
        /// </summary>
        /// <param name="cobBox"></param>
        /// <param name="saveKey"></param>
        public static void SaveSelected(System.Windows.Forms.ComboBox cobBox) {
            string saveKey = getSaveControlID(cobBox);
            IniFile.WriteString(COMBOX_SAVE_SESSION, saveKey, cobBox.SelectedIndex.ToString(), COMBOX_SAVE_FILE);
        }
        /// <summary>
        /// 恢复用户选择的项
        /// </summary>
        /// <param name="cobBox"></param>
        /// <param name="saveKey"></param>
        /// <returns></returns>
        public static bool ResumeSelected(System.Windows.Forms.ComboBox cobBox) {
            string saveKey = getSaveControlID(cobBox);

            string index = IniFile.ReadString(COMBOX_SAVE_SESSION, saveKey, string.Empty, COMBOX_SAVE_FILE);
            try {
                int re = 0;
                if (Int32.TryParse(index, out re))
                    cobBox.SelectedIndex = re;

            }
            catch { }
            return true;
        }
        /// <summary>
        /// 把Combox 的数据项加载到ini文件中
        /// </summary>
        /// <param name="cobBox"></param>
        /// <param name="saveKey"></param>
        /// <param name="currentInputVal"></param>
        public static void SaveToFile(System.Windows.Forms.ComboBox cobBox, string currentInputVal) {
            string saveKey = getSaveControlID(cobBox);
            int count = cobBox.Items.Count;
            int hasAdd = 0;
            string val = string.Empty;
            if (!string.IsNullOrEmpty(currentInputVal)) { //通过这种方式保持最后存储在在第一项
                val = currentInputVal + ";";
                hasAdd += 1;
            }
            for (int i = 0; i < count; i++) {
                string tem = cobBox.Items[i].ToString();
                if (string.Compare(tem, currentInputVal, true) == 0)
                    continue;
                hasAdd++;
                if (hasAdd >= MAX_ITEMS) break;
                val += tem + ";";
            }
            if (val.Length > 0) {
                val = val.Remove(val.Length - 1, 1);
                IniFile.WriteString(COMBOX_SAVE_SESSION, saveKey, val, COMBOX_SAVE_FILE);
            }

           
        }
        /// <summary>
        /// 从保存的文件读取默认设置的值.
        /// </summary>
        /// <param name="cobBox"></param>
        /// <param name="saveKey"></param>
        public static void ReadFromFile(System.Windows.Forms.ComboBox cobBox) {
            string saveKey = getSaveControlID(cobBox);

            string val = IniFile.ReadString(COMBOX_SAVE_SESSION, saveKey, string.Empty, COMBOX_SAVE_FILE);
            if (string.IsNullOrEmpty(val)) return;
            string[] items = val.Split(';');
            cobBox.Items.Clear();
            if (items.Length > 0) {
                foreach (string item in items)
                    cobBox.Items.Add(item);
            }

            if(cobBox.Items.Count > 0) 
                cobBox.SelectedIndex = 0;
        }
        #endregion public static function...

        #region 内部函数处理...
        //判断指定的值是否已经存在 
        private static bool checkIsExists(System.Windows.Forms.ComboBox cobBox, string saveVal) {
            if (cobBox.Items.Count == 0 || string.IsNullOrEmpty(saveVal))
                return false;
            foreach (string item in cobBox.Items) {
                if (string.Compare(item.Trim(), saveVal.Trim(), true) == 0)
                    return true;
            }
            return false;
        }
        #endregion 内部函数处理...


        private static string getSaveControlID(Control ctl) {
            List<string> names = new List<string>();
            getCtlID(ctl, names);
            return names[0] + " " + ctl.Name;
        }

        private static void getCtlID(Control ctl, List<string> names) {
            if (ctl.Parent == null)
                names.Add(ctl.GetType().FullName);
            else
                getCtlID(ctl.Parent, names);
        }
    }
}
