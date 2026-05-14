using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Texty.Encoding
{
    internal class TextEncoding
    {
        private string utf8;
        private string utf16;
        private string utf32;
        private static string separator = " ";

        public string UTF8
        {
            get => utf8;
        }
        public string UTF16
        {
            get => utf16;
        }
        public string UTF32
        {
            get => utf32;
        }

        public void Encode(string text)
        {
            utf8 = EncodeToUTF8(text);
            utf16 = EncodeToUTF16(text, false);
            utf32 = EncodeToUTF32(text, false);
        }

        public void Decode(byte[] text)
        {

        }

        #region Encode
        private static string EncodeToUTF8(string text)
        {
            byte[] decimalUTF8 = System.Text.Encoding.UTF8.GetBytes(text);
            string[] stringUTF8 = ConvertDecimalToHexa(decimalUTF8);
            stringUTF8 = AddHexPrefix(stringUTF8);
            return ConvertToString(stringUTF8);
        }

        private static string EncodeToUTF16(string text, bool isLittleEndian)
        {
            byte[] decimalUTF16;
            if (isLittleEndian) decimalUTF16 = System.Text.Encoding.Unicode.GetBytes(text); //LE
            else decimalUTF16 = System.Text.Encoding.BigEndianUnicode.GetBytes(text);       // BE
            
            string[] stringUTF16 = ConvertDecimalToHexa(decimalUTF16);
            stringUTF16 = ConcatEndian(stringUTF16, 2);
            stringUTF16 = AddHexPrefix(stringUTF16);
            return ConvertToString(stringUTF16);
        }

        private static string EncodeToUTF32(string text, bool isLittleEndian)
        {
            byte[] decimalUTF32 = System.Text.Encoding.UTF32.GetBytes(text);        // LE
            if (!isLittleEndian) decimalUTF32 = decimalUTF32.Reverse().ToArray();   // BE

            string[] stringUTF32 = ConvertDecimalToHexa(decimalUTF32);
            stringUTF32 = ConcatEndian(stringUTF32, 4);
            stringUTF32 = stringUTF32.Reverse().ToArray();
            stringUTF32 = AddHexPrefix(stringUTF32);
            return ConvertToString(stringUTF32);
        }
        #endregion

        #region Decode
        private static string DecodeToUTF8(byte[] decimalUTF8)
        {
            return System.Text.Encoding.UTF8.GetString(decimalUTF8);
        }

        private static string DecodeToUTF16(byte[] decimalUTF16, bool isLittleEndian)
        {
            if (isLittleEndian) return System.Text.Encoding.Unicode.GetString(decimalUTF16); //LE
            else return System.Text.Encoding.BigEndianUnicode.GetString(decimalUTF16);       // BE
        }

        private static string DecodeToUTF32(byte[] decimalUTF32, bool isLittleEndian)
        {
            if (isLittleEndian) return System.Text.Encoding.UTF32.GetString(decimalUTF32);      // LE
            else return System.Text.Encoding.UTF32.GetString(decimalUTF32.Reverse().ToArray()); // BE
        }
        #endregion

        private static string[] ConvertDecimalToHexa(byte[] decimalArray)
        {
            var hexaDecimalList = decimalArray.Select(deci =>
            {
                string hex = $"{deci:X2}"; // X uppercaser, x lowercase
                return hex;
            });

            return hexaDecimalList.ToArray();
        }

        private static byte[] ConvertHexaToDecimal(string[] hexaDecimalArray)
        {
            var decimalList = hexaDecimalArray.Select(hexa => Convert.ToByte(hexa, 16));
            return decimalList.ToArray();
        }

        private static string[] ConcatEndian(string[] hexArray, int count)
        {
            var groupByIndex = hexArray.Select((value, index) => new { value, index })
                                        .GroupBy(endian => endian.index / count);
            
            var filterByValue = groupByIndex.Select
                                            (
                                                groupedElements => groupedElements.Select
                                                                                    (
                                                                                        endian => endian.value
                                                                                    )
                                            );

            var concatEndian = filterByValue.Select(endian => string.Concat(endian));
            
            return concatEndian.ToArray();
        }

        private static string[] AddHexPrefix(string[] hexaDecimal)
        {
            return hexaDecimal.Select(x => $"0x{x}").ToArray();
        }

        private static string[] RemoveHexPrefix(string[] hexaDecimal)
        {
            return hexaDecimal.Select(x => x.Replace("0x", "")).ToArray();
        }

        private static string ConvertToString(string[] hexaDecimal)
        {
            return string.Join(separator, hexaDecimal);
        }

        private static string[] ConvertToArray(string hexaDecimal)
        {
            return hexaDecimal.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
