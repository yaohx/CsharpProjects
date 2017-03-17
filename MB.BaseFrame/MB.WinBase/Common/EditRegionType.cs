using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.WinBase.Common
{
    public class RegionType
    {
        private int _RegionId;
        public int RegionId
        {
            get { return _RegionId; }
            set { _RegionId = value; }
        }

        private int _RegionLevel;
        public int RegionLevel
        {
            get { return _RegionLevel; }
            set { _RegionLevel = value; }
        }

        private string _Region;
        public string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }

        private int _ParentRegionLevel;
        public int ParentRegionLevel
        {
            get { return _ParentRegionLevel; }
            set { _ParentRegionLevel = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RegionEditInfo
    {
        private string _Country;
        private string _Province;
        private string _City;
        private string _District;
        private string _Level;



        public RegionEditInfo()
        { }
        public RegionEditInfo(string country, string province, string city, string district, string level)
        {
            _Country = country;
            _Province = province;
            _City = city;
            _District = district;
            _Level = level;
        }

        [Description("国家")]
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        [Description("省份")]
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        [Description("城市")]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        [Description("地区")]
        public string District
        {
            get { return _District; }
            set { _District = value; }
        }

        [Description("行政级别")]
        public string Level {
            get { return _Level; }
            set { _Level = value; }
        }
    }

}
