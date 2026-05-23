using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Texty.Tools.Encoding
{
    internal class CharacterEncodingConverter
    {
        public enum Base
        {
            Binary = 2,
            Octal = 8,
            Decimal = 10,
            Hexadecimal = 16
        }

        private char character;
        private string binaryCode;
        private string octalCode;
        private string decimalCode;
        private string hexadecimalCode;

        public char Character
        {
            get => character;
        }
        public string BinaryCode
        {
            get => binaryCode;
        }
        public string OctalCode
        {
            get => octalCode;
        }
        public string DecimalCode
        {
            get => decimalCode;
        }
        public string HexadecimalCode
        {
            get => hexadecimalCode;
        }

        public void Encode(char character)
        {
            this.character = character;

            int decimalCode = character;
            binaryCode = ConvertToBinaryCode(character);
            octalCode = ConvertToOctalCode(character);
            this.decimalCode = Split(decimalCode.ToString(), 3, ',', true);
            hexadecimalCode = ConvertToHexadecimalCode(character);
        }

        public void Decode(int decimalCide)
        {
            character = Convert.ToChar(decimalCide);
            Encode(character);
        }

        #region Encoder
        private static string ConvertToBinaryCode(int decimalCode)
        {
            string decimalToBinary = Convert.ToString(decimalCode, 2);
            string binaryPadLeft = Padding(decimalToBinary, 4, '0', false);
            string fixFormat = Split(binaryPadLeft, 4, ' ', true);
            return fixFormat;
        }

        private static string ConvertToOctalCode(int decimalCode)
        {
            string decimalToOctal = Convert.ToString(decimalCode, 8);
            string fixFormat = Split(decimalToOctal, 3, ' ', true);
            return fixFormat;
        }

        private static string ConvertToHexadecimalCode(int decimalCode)
        {
            string decimalToHexadecimal = decimalCode.ToString("X2");  // X (uppercase), x (lowercase)
            if (decimalToHexadecimal.Length == 2) return $"0x{decimalToHexadecimal}";

            string hexadecimalPadLeft = Padding(decimalToHexadecimal, 4, '0', false);
            return $"0x{hexadecimalPadLeft}";
        }
        #endregion

        private static string Padding(string text, int separateCount, char paddingChar, bool fromRight)
        {
            int lengthToBeComplete = Convert.ToInt32
                                   (
                                       Math.Ceiling(text.Length / (double)separateCount) * separateCount
                                   );
            
            string paddedString;
            if (fromRight) paddedString = text.PadRight(lengthToBeComplete, paddingChar);
            else paddedString = text.PadLeft(lengthToBeComplete, paddingChar);
            
            return paddedString;
        }

        private static string Split(string text, int separateCount, char separator, bool fromRight)
        {
            string pattern = ".{" + separateCount + "}";
            
            string splitedString;
            if (fromRight)
            {
                string reversed = string.Concat(text.Reverse());
                splitedString = Regex.Replace(reversed, pattern, "$0" + separator);
                if (splitedString.Last() == separator) splitedString = splitedString.Remove(splitedString.Length - 1);
                return string.Concat(splitedString.Reverse());
            }
            else
            {
                splitedString = Regex.Replace(text, pattern, "$0" + separator);
                if (splitedString.Last() == separator) splitedString = splitedString.Remove(splitedString.Length - 1);
            }
            return splitedString;
        }

        public static bool IsCodeBaseCorrect(string code, Base codeBase)
        {
            try
            {
                Convert.ToInt32(code, (int)codeBase);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsCodeLengthCorrect(string code, Base codeBase)
        {
            int decimalCode = Convert.ToInt32(code, (int)codeBase);
            return decimalCode >= char.MinValue && decimalCode <= char.MaxValue;
        }

    }
}
