using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common
{
    public class ChartSeriesPointInfo
    {
        private object _Argument;
        private object[] _ValueParams;

        public ChartSeriesPointInfo(object argument,params object[] valueParams)
        {
            _Argument = argument;
            _ValueParams = valueParams;
        }

        public object Argument
        {
            get { return _Argument; }
            set { _Argument = value; }
        }

        public object[] ValueParams
        {
            get { return _ValueParams; }
            set { _ValueParams = value; }
        }
    }
}
