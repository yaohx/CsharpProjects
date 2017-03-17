namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Globalization;

    ///<summary>
    ///Class used for controling number format
    ///</summary>
    internal class NumbersParser
    {
        // Methods
        ///<summary>
        ///Initialize object
        ///</summary>
        static NumbersParser()
        {
            NumbersParser.formatProvider = new NumberFormatInfo();
            NumbersParser.formatProvider.NumberDecimalSeparator = ".";
        }

        public NumbersParser()
        {
        }

        ///<summary>
        ///Determines whether the specified double value is ushort( integer ).
        ///</summary>
        ///<param name="doubleValue">The double value.</param>
        ///<returns>
        ///<c>true</c> if the specified double value is ushort; otherwise, <c>false</c>.
        ///</returns>
        public static bool IsUshort(double doubleValue)
        {
            if (doubleValue < 65535)
            {
                return (Math.Ceiling(doubleValue) == doubleValue);
            }
            return false;
        }

        ///<summary>
        ///Convert string to double.
        ///</summary>
        ///<param name="data">string data.</param>
        ///<returns>double data.</returns>
        public static double StrToDouble(string data)
        {
            double num1;
            double.TryParse(data, NumberStyles.Float, NumbersParser.formatProvider, out num1);
            return num1;
        }

        ///<summary>
        ///Converts string to float.
        ///</summary>
        ///<param name="str">strind data.</param>
        ///<returns>flot data.</returns>
        public static float StrToFloat(string str)
        {
            double num1 = 0;
            double.TryParse(str, NumberStyles.Float, NumbersParser.formatProvider, out num1);
            return (float) num1;
        }

        ///<summary>
        ///Converts string to int.
        ///</summary>
        ///<param name="data">string data.</param>
        ///<returns>int data.</returns>
        public static int StrToInt(string data)
        {
            double num1 = 0;
            double.TryParse(data, NumberStyles.Integer, NumbersParser.formatProvider, out num1);
            return (int) num1;
        }


        // Properties
        ///<summary>
        ///Get number format info instance
        ///</summary>
        public static NumberFormatInfo Provider
        {
            get
            {
                return NumbersParser.formatProvider;
            }
        }


        // Fields
        ///<summary>
        ///Number format for string conversion
        ///</summary>
        private static NumberFormatInfo formatProvider;
    }
}

