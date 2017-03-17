using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Binding {
    /// <summary>
    /// 扩展 系统的BindingSource 满足自动绑定的功能。
    /// </summary>
    public class BindingSourceEx : System.Windows.Forms.BindingSource {
        private Dictionary<string,object> _CurrentItemOldValue;

        /// <summary>
        /// OnPositionChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPositionChanged(EventArgs e) {
            base.OnPositionChanged(e);
            if(CheckExistsCurrentItem())
                _CurrentItemOldValue = MB.Util.MyReflection.Instance.ObjectToPropertyValues(this.Current); 
        }
        /// <summary>
        /// 清除历史存储的数据 和 绑定到Windows 控件的绑定信息。
        /// </summary>
        public void ClearOldValue() {
            if (_CurrentItemOldValue != null)
                _CurrentItemOldValue.Clear();
           
        }
        /// <summary>
        /// 判断是否存在当前项。
        /// </summary>
        /// <returns></returns>
        public bool CheckExistsCurrentItem() {
            try {
                return this.Current != null; 
            }
            catch {
                return false;
            }
        }
        /// <summary>
        /// 结束编辑但不响应事件。
        /// </summary>
        public void EndEditNoRaiseEvent() {
            bool old = this.RaiseListChangedEvents;
            this.RaiseListChangedEvents = false;
            _CurrentItemOldValue = MB.Util.MyReflection.Instance.ObjectToPropertyValues(this.Current); 
            this.EndEdit();
            this.RaiseListChangedEvents = old;
        }
        /// <summary>
        /// 撤消当前已做的更改。
        /// </summary>
        public void CurrentItemRejectChanges() {
            this.RaiseListChangedEvents = false;
            this.EndEdit();
            MB.Util.MyReflection.Instance.SetByPropertyValues(this.Current, _CurrentItemOldValue);
            this.ResetCurrentItem(); 
            this.RaiseListChangedEvents = true;
        }
        /// <summary>
        /// CurrentItemOldValue
        /// </summary>
        public Dictionary<string, object> CurrentItemOldValue {
            get {
                return _CurrentItemOldValue;
            }
            set {
                _CurrentItemOldValue = value;
            }
        }
    }


}
