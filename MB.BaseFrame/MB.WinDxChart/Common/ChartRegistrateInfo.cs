using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinDxChart.Common
{
    public class ChartRegistrateInfo
    {
        private string _Name;
        private Type _Type;
        private string _Description;
        private int _ImageIndex;
        private string _CodeFile;
        private string _XmlFile;
        private string _AboutFile;


        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public Type RegistType
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int ImageIndex
        {
            get { return _ImageIndex; }
            set { _ImageIndex = value; }
        }

        public string CodeFile
        {
            get { return _CodeFile; }
            set { _CodeFile = value; }
        }

        public string XmlFile
        {
            get { return _XmlFile; }
            set { _XmlFile = value; }
        }

        public string AboutFile
        {
            get { return _AboutFile; }
            set { _AboutFile = value; }
        }
    }
}
