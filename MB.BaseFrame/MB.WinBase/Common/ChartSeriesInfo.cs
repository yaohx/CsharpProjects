using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MB.WinBase.Common
{
    public class ChartSeriesInfo
    {
        private string _Name;
        private string _ViewType;
        private string[] _ValueDataMember;
        private string _ArgumentDataMember;
        private bool _LabelsVisible;
        private Color _Color;
        private string _ArgumentScaleType;

        private List<ChartSeriesPointInfo> _SeriesPoints;

        public ChartSeriesInfo(string name, string viewType)
        {
            _Name = name;
            _ViewType = viewType;

            _SeriesPoints = new List<ChartSeriesPointInfo>();
        }

        public string Name { get { return _Name; } set { _Name = value; } }

        public string ViewType
        {
            get { return _ViewType; }
            set { _ViewType = value; }
        }

        public string[] ValueDataMember
        {
            get { return _ValueDataMember; }
            set { _ValueDataMember = value; }
        }

        public string ArgumentDataMember
        {
            get { return _ArgumentDataMember; }
            set { _ArgumentDataMember = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool LabelsVisible
        {
            get { return _LabelsVisible; }
            set { _LabelsVisible = value; }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        /// <summary>
        /// Qualitative
        /// Numerical,
        /// DateTime
        /// Auto
        /// </summary>
        public string ArgumentScaleType
        {
            get { return _ArgumentScaleType; }
            set { _ArgumentScaleType = value; }
        }



        public List<ChartSeriesPointInfo> SeriesPoints
        {
            get { return _SeriesPoints; }
            set { _SeriesPoints = value; }
        }
    }
}
