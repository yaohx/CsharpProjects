using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.XWinLib.Share
{
    [DataContract]
    public class GridLayoutInfo
    {
        #region 变量定义
        private string _Name;
        private DateTime _CreateTime;
        #endregion

        #region Public属性

        [DataMember]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        [DataMember]
        public DateTime CreateTime {
            get {
                return _CreateTime;
            }
            set {
                _CreateTime = value;
            }
        }
        #endregion
    }

    [DataContract]
    public class GridLayoutMainInfo
    {
        private string _GridSectionName;
        [DataMember]
        public string GridSectionName
        {
            get
            {
                return _GridSectionName;
            }
            set
            {
                _GridSectionName = value;
            }
        }

        public List<GridLayoutInfo> _GridLayoutList;
        [DataMember]
        public List<GridLayoutInfo> GridLayoutList
        {
            get {
                return _GridLayoutList;
            }
            set {
                _GridLayoutList = value;
            }
        }
    }
}
