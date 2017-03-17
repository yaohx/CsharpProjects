namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    internal class Utilities
    {
        // Methods
        private Utilities()
        {
        }

        internal static ushort BoolToUshort(bool boolValue)
        {
            return (boolValue ? ((ushort) 1) : ((ushort) 0));
        }

        public static string ByteArr2HexStr(byte[] byteArr)
        {
            int num2 = Math.Max((int) ((byteArr.Length * 3) - 1), 1);
            StringBuilder builder1 = new StringBuilder(num2, num2);
            for (int num1 = 0; num1 < byteArr.Length; num1++)
            {
                if (num1 > 0)
                {
                    builder1.Append(' ');
                }
                builder1.AppendFormat("{0:X2}", byteArr[num1]);
            }
            return builder1.ToString();
        }

        public static bool Contains(Array arr, object val)
        {
            return (Array.IndexOf(arr, val) != -1);
        }

        public static object[] ConvertBytesToObjectArray(byte[] bytes)
        {
            object[] objArray1 = new object[bytes.Length];
            Array.Copy(bytes, 0, objArray1, 0, bytes.Length);
            return objArray1;
        }

        public static int GetByteArrLengthFromHexStr(string hexStr)
        {
            return ((hexStr.Length / 3) + 1);
        }

        public static byte[] HexStr2ByteArr(string hexStr)
        {
            int num1 = Utilities.GetByteArrLengthFromHexStr(hexStr);
            byte[] buffer1 = new byte[num1];
            for (int num2 = 0; num2 < num1; num2++)
            {
                buffer1[num2] = byte.Parse(hexStr.Substring(num2 * 3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return buffer1;
        }

        public static bool IsBitSetted(byte sourceByte, byte mask)
        {
            return ((sourceByte & mask) != 0);
        }

        public static string ReadString(bool isUnicode, byte[] rpnBytes, int startIndex, int length)
        {
            string text1 = string.Empty;
            if (isUnicode)
            {
                return Encoding.Unicode.GetString(rpnBytes, startIndex, length * 2);
            }
            return Encoding.ASCII.GetString(rpnBytes, startIndex, length);
        }

        internal static int RotateLeft(int val, byte count)
        {
            uint num1 = Utilities.RotateLeft((uint) val, count);
            return num1.GetHashCode();
        }

        internal static uint RotateLeft(uint val, byte count)
        {
            return ((val << (count & 0x1f)) | (val >> ((0x20 - count) & 0x1f)));
        }

        public static byte SetBit(byte sourceByte, byte mask, bool value)
        {
            sourceByte = (byte) (sourceByte & ~mask);
            if (value)
            {
                sourceByte = (byte) (sourceByte + mask);
            }
            return sourceByte;
        }

        internal static void ToFile(byte[] bytes)
        {
            FileStream stream1 = new FileStream("dump.txt", FileMode.Create);
            stream1.Write(bytes, 0, bytes.Length);
            stream1.Close();
        }

        internal static void ToFile(object[] arr)
        {
            byte[] buffer1 = new byte[arr.Length];
            Array.Copy(arr, 0, buffer1, 0, arr.Length);
            Utilities.ToFile(buffer1);
        }

    }
}

