namespace MB.WinEIDrive.Excel
{
    using System;
    using System.ComponentModel;

    internal class GemBoxLicenseProvider : LicenseProvider
    {
        // Methods
        static GemBoxLicenseProvider()
        {
            GemBoxLicenseProvider.rndGen = new Random();
        }

        public GemBoxLicenseProvider()
        {
        }

        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            ExcelFile file1 = instance as ExcelFile;
            if (file1 == null)
            {
                if (allowExceptions)
                {
                    throw new Exception("Internal error: Unrecognized caller.");
                }
                return null;
            }
			//edit by nick 2006-04-27 破解150 行 //设置处理的最大值为10万行
            file1.HashFactorA = 100000;//GemBoxLicenseProvider.rndGen.Next(0x458, 0x3117);
           // file1.HashFactorB = file1.HashFactorA - 150;
			file1.HashFactorB = 0;
            return new GemBoxLicense("Valid");
        }


        // Fields
        private static Random rndGen;
    }
}

