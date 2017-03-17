namespace MB.WinEIDrive.Excel
{
    using System;
    using System.ComponentModel;

    internal class GemBoxLicense : License
    {
        // Methods
        public GemBoxLicense(string licenseKey)
        {
            this.licenseKey = licenseKey;
        }

        public override void Dispose()
        {
        }

        // Properties
        public override string LicenseKey
        {
            get
            {
                return this.licenseKey;
            }
        }


        // Fields
        private string licenseKey;
    }
}

