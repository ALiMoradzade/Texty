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

namespace Texty.Tools.Encoding
{
    internal class TextEncodingConverter
    {
        public enum Encoding
        {
            UTF8,
            UTF16,
            UTF32,
        }

        private string text;
        private string utf8;
        private string utf16;
        private string utf32;
        private static string separator = " ";
        private bool isBigEndian;
        private bool bom;

        public string Text
        {
            get => text;
        }
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
        public string Separator
        {
            get => separator;
        }
        public bool IsBigEndian
        {
            get => isBigEndian;
        }
        public bool BOM
        {
            get => bom;
        }

        public TextEncodingConverter(bool isBigEndian = true, bool bom = false)
        {
            this.isBigEndian = isBigEndian;
            this.bom = bom;
        }

        public void Encode(string text)
        {
            this.text = text;
            utf8 = EncodeToUTF8(text);
            utf16 = string.Join(separator, EncodeToUTF16(text, isBigEndian, bom));
            utf32 = string.Join(separator, EncodeToUTF32(text, isBigEndian, bom));
        }

        public void Decode(string code, Encoding codeEncoding)
        {

            if (codeEncoding == Encoding.UTF8)
            {
                text = DecodeToUTF8(code);
            }
            else if (codeEncoding == Encoding.UTF16)
            {
                text = DecodeToUTF16(code, isBigEndian, BOM);
            }
            else if (codeEncoding == Encoding.UTF32)
            {
                text = DecodeToUTF32(code, isBigEndian, BOM);
            }

            Encode(text);
        }

        #region Encode
        private static string EncodeToUTF8(string text)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(text);
            var hex = bytes.Select(number => number.ToString("X2"));
            var hexPrefix = hex.Select(x => $"0x{x}");
            return string.Join(separator, hexPrefix);
        }

        private static string EncodeToUTF16(string text, bool isBigEndian, bool BOM)
        {
            UnicodeEncoding encoding = new UnicodeEncoding(isBigEndian, BOM);
            byte[] bytes = encoding.GetBytes(text);
            var hex = bytes.Select(number => number.ToString("X2"));
            string[][] groupEndian = GroupByIndex(hex.ToArray(), 2);
            var concatEndian = groupEndian.Select(x => string.Concat(x));
            var hexPrefix = concatEndian.Select(x => $"0x{x}");
            return string.Join(separator, hexPrefix);
        }

        private static string EncodeToUTF32(string text, bool isBigEndian, bool BOM)
        {
            UTF32Encoding encoding = new UTF32Encoding(isBigEndian, BOM);
            byte[] bytes = encoding.GetBytes(text);
            var hex = bytes.Select(number => number.ToString("X2"));
            string[][] groupEndian = GroupByIndex(hex.ToArray(), 4);
            var concatEndian = groupEndian.Select(x => string.Concat(x));
            var hexPrefix = concatEndian.Select(x => $"0x{x}");
            return string.Join(separator, hexPrefix);
        }
        #endregion

        #region Decode
        private static string DecodeToUTF8(string hexadecimalCodes)
        {
            string[] splittedUnicodes = hexadecimalCodes.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            string[] removeHexaPrefix = splittedUnicodes.Select(x => x.Replace("0x", "")).ToArray();
            byte[] bytes = removeHexaPrefix.Select(hex => Convert.ToByte(hex, 16)).ToArray();

            UTF8Encoding encoding = new UTF8Encoding();
            string text = encoding.GetString(bytes);
            return text;
        }

        private static string DecodeToUTF16(string hexadecimalCodes, bool isBigEndian, bool BOM = false)
        {
            string[] splittedUnicodes = hexadecimalCodes.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            string[] removeHexaPrefix = splittedUnicodes.Select(x => x.Replace("0x", "")).ToArray();
            string[][] splittedEndianJagged = removeHexaPrefix.Select(x => SplitByString(x, 2)).ToArray();
            string[] splittedEndian = splittedEndianJagged.SelectMany(x => x).ToArray();
            byte[] bytes = splittedEndian.Select(hex => Convert.ToByte(hex, 16)).ToArray();

            UnicodeEncoding encoding = new UnicodeEncoding(isBigEndian, BOM);
            string text = encoding.GetString(bytes);
            return text;
        }

        private static string DecodeToUTF32(string hexadecimalCodes, bool isBigEndian, bool BOM = false)
        {
            string[] splittedUnicodes = hexadecimalCodes.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            string[] removeHexaPrefix = splittedUnicodes.Select(x => x.Replace("0x", "")).ToArray();
            string[][] splittedEndianJagged = removeHexaPrefix.Select(x => SplitByString(x, 2)).ToArray();
            string[] splittedEndian = splittedEndianJagged.SelectMany(x => x).ToArray();
            byte[] bytes = splittedEndian.Select(hex => Convert.ToByte(hex, 16)).ToArray();

            UTF32Encoding encoding = new UTF32Encoding(isBigEndian, BOM);
            string text = encoding.GetString(bytes);
            return text;
        }
        #endregion

        private static string[][] GroupByIndex(string[] hexArray, int count)
        {
            var groupByIndex = hexArray.Select((value, index) => new { value, index })
                                       .GroupBy(endian => endian.index / count);

            var filterByValue = groupByIndex.Select
                                           ( 
                                               groupedElements => groupedElements.Select(endian => endian.value)
                                                                                 .ToArray()
                                           );

            return filterByValue.ToArray();
        }

        private static string[] SplitByString(string hexadecimalCode, int count)
        {
            var sectionRange = Enumerable.Range(0, hexadecimalCode.Length / count);
            
            string[] splittedEndian = sectionRange.Select(index => 
            {
                string s = hexadecimalCode.Substring(index * count, count);
                return s;
            })
                .ToArray();

            return splittedEndian;
        }

        public static bool IsCodeBaseCorrect(string code, Encoding codeEncoding)
        {
            string[] splittedUnicodes = code.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            string[] removeHexaPrefix = splittedUnicodes.Select(x => x.Replace("0x", "")).ToArray();

            if (codeEncoding == Encoding.UTF8)
            {
                return removeHexaPrefix.All(hexa => Regex.IsMatch(hexa, "[a-fA-F0-9]{1,2}"));
            }
            else if (codeEncoding == Encoding.UTF16)
            {
                return removeHexaPrefix.All(hexa => Regex.IsMatch(hexa, "[a-fA-F0-9]{1,4}"));
            }
            else if (codeEncoding == Encoding.UTF32)
            {
                return removeHexaPrefix.All(hexa => Regex.IsMatch(hexa, "[a-fA-F0-9]{1,8}"));
            }
            return false;
        }

        public static bool IsCodeLengthCorrect(string code, Encoding codeEncoding)
        {
            string[] splittedUnicodes = code.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            string[] removeHexaPrefix = splittedUnicodes.Select(x => x.Replace("0x", "")).ToArray();

            return removeHexaPrefix.All(hex => Convert.ToInt32(hex, 16) <= 0x10FFFF);
        }
    }
}
