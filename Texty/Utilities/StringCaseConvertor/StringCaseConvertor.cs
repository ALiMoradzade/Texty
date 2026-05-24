using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Texty.Utilities.StringCaseConvertor
{
    internal static class StringCaseConvertor
    {
        private static char[] separator = new char[] { '-', '_', '.', ' ' };

        public static DialogResult MessageBoxWrongFormat(string caseName)
        {
            var r = MessageBox.Show($"Can't convert this text to {caseName} case",
                                    "Invalid case conversion format",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private static string RemoveSeparator(string text)
        {
            char[] separatorRemoved = text.Where(c => !separator.Contains(c))
                                          .ToArray();
            string separatorRemovedText = string.Concat(separatorRemoved);
            return separatorRemovedText;
        }

        private static string[] SplitBySeparator(string text)
        {
            string[] words = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }

        public static bool IsSplitableBySeparator(string text)
        {
            return text.Any(c => separator.Contains(c));
        }

        public static string[] SplitByUpperCase(string text)
        {
            string[] words = Regex.Matches(text, "[A-Z0-9][a-z0-9]*")
                                  .Cast<Match>()
                                  .Select(match => match.Value)
                                  .Where(match => match.Length > 0)
                                  .ToArray();
            return words;
        }

        public static bool IsSplitableByUpperCase(string text)
        {
            return text.Any(c => char.IsUpper(c));
        }

        public static bool IsSplitable(string text)
        {
            if (IsSplitableBySeparator(text)) return true;
            else if (IsSplitableByUpperCase(text)) return true;
            return false;
        }

        private static string[] SplitAuto(string text)
        {
            if (IsSplitableBySeparator(text)) return SplitBySeparator(text);
            else if (IsSplitableByUpperCase(text)) return SplitByUpperCase(text);
            else throw new FormatException("Invalid case conversion format");
        }

        private static string[] CapitalizeAllWords(string[] words)
        {
            string[] loweredWords = LowerAllWords(words);
            string[] capitalized = loweredWords.Select(word =>
            {
                char firstLetter = word[0];
                string remaining = word.Substring(1);
                return char.ToUpper(firstLetter) + remaining;
            }).ToArray();

            return capitalized;
        }

        private static string[] LowerAllWords(string[] words)
        {
            string[] lowered = words.Select(word => word.ToLower())
                                    .ToArray();
            return lowered;
        }

        private static string[] UpperAllWords(string[] words)
        {
            string[] lowered = words.Select(word => word.ToUpper())
                                    .ToArray();
            return lowered;
        }

        /// <summary>
        /// Invert Case = Toggle Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToInvertCase(string text)
        {
            string separatorRemovedText = RemoveSeparator(text);
            var toggleCase = string.Concat(
                                            separatorRemovedText.Select(c => char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c))
                                          );
            return toggleCase;
        }

        /// <summary>
        /// Laze Case = Lower Flat Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToLazyCase(string text)
        {
            string separatorRemovedText = RemoveSeparator(text);
            string lazyCase = separatorRemovedText.ToLower();
            return lazyCase;
        }

        /// <summary>
        /// Kebab Case = Lower Kebab Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToKebabCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] loweredWords = LowerAllWords(words);
            string addSeparator = string.Join("-", loweredWords);
            return addSeparator;
        }

        /// <summary>
        /// Snake Case = Lower Snake Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToSnakeCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] loweredWords = LowerAllWords(words);
            string addSeparator = string.Join("_", loweredWords);
            return addSeparator;
        }

        /// <summary>
        /// Dot Case = Lower Dot Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToDotCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] loweredWords = LowerAllWords(words);
            string addSeparator = string.Join(".", loweredWords);
            return addSeparator;
        }

        /// <summary>
        /// Space Case = Lower Space Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToSpaceCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] loweredWords = LowerAllWords(words);
            string addSeparator = string.Join(" ", loweredWords);
            return addSeparator;
        }

        public static string ToCamelCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string newText = string.Concat(capitalizedWords);
            return char.ToLower(newText[0]) + newText.Substring(1);
        }

        public static string ToCamelKebabCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join("-", capitalizedWords);
            return char.ToLower(addSeparator[0]) + addSeparator.Substring(1);
        }

        public static string ToCamelSnakeCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join("_", capitalizedWords);
            return char.ToLower(addSeparator[0]) + addSeparator.Substring(1);
        }

        public static string ToCamelDotCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join(".", capitalizedWords);
            return char.ToLower(addSeparator[0]) + addSeparator.Substring(1);
        }

        public static string ToSentenceCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join(" ", capitalizedWords);
            return addSeparator;
        }

        /// <summary>
        /// Pascal Case = Upper Camel Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToPascalCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string newText = string.Concat(capitalizedWords);
            return newText;
        }

        /// <summary>
        /// Train Case = Pascal Kebab Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToTrainCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join("-", capitalizedWords);
            return addSeparator;
        }

        public static string ToPascalSnakeCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join("_", capitalizedWords);
            return addSeparator;
        }

        public static string ToPascalDotCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join(".", capitalizedWords);
            return addSeparator;
        }

        public static string ToTitleCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] capitalizedWords = CapitalizeAllWords(words);
            string addSeparator = string.Join(" ", capitalizedWords);
            return addSeparator;
        }

        /// <summary>
        /// Screaming Case = Upper Flat Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToScreamingCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] upperedWords = UpperAllWords(words);
            string newText = string.Concat(upperedWords);
            return newText;
        }

        /// <summary>
        /// Cobol Case = Screaming Kebab Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToCobolCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] upperedWords = UpperAllWords(words);
            string addSeparator = string.Join("-", upperedWords);
            return addSeparator;
        }

        public static string ToScreamingSnakeCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] upperedWords = UpperAllWords(words);
            string addSeparator = string.Join("_", upperedWords);
            return addSeparator;
        }

        public static string ToScreamingDotCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] upperedWords = UpperAllWords(words);
            string addSeparator = string.Join(".", upperedWords);
            return addSeparator;
        }

        public static string ToUpperSpaceCase(string text)
        {
            string[] words = SplitAuto(text);
            string[] upperedWords = UpperAllWords(words);
            string addSeparator = string.Join(" ", upperedWords);
            return addSeparator;
        }
    }
}
