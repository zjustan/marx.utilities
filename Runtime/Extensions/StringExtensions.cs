using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Marx.Utilities
{
    public static class StringExtensions {

        /// <summary>
        /// Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name="str">The string to check for null or emptiness.</param>
        /// <returns>True if the string is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool NotNullOrEmpty(this string str) => !string.IsNullOrEmpty(str);

        /// <summary>
        /// Removes the specified substring from the beginning of the string, if it exists.
        /// </summary>
        /// <param name="str">The string to process.</param>
        /// <param name="remove">The substring to remove from the beginning of the string.</param>
        /// <returns>The modified string with the specified substring removed from the start, or the original string if the substring is not found.</returns>
        public static string RemoveStart(this string str, string remove) {
            int index = str.IndexOf(remove, StringComparison.Ordinal);
            return index < 0 ? str : str.Remove(index, remove.Length);
        }

        /// <summary>
        /// Removes the specified substring from the end of the string, if it exists.
        /// </summary>
        /// <param name="str">The string to process.</param>
        /// <param name="remove">The substring to remove from the end of the string.</param>
        /// <returns>The modified string with the specified substring removed from the end, or the original string if the substring is not found.</returns>
        public static string RemoveEnd(this string str, string remove) {
            if (!str.EndsWith(remove)) return str;
            return str.Remove(str.LastIndexOf(remove, StringComparison.Ordinal));
        }

        /// <summary>
        /// "Camel case string" => "CamelCaseString" 
        /// </summary>
        public static string ToCamelCase(this string message) {
            message = message.Replace("-", " ").Replace("_", " ");
            message = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(message);
            message = message.Replace(" ", "");
            return message;
        }

        /// <summary>
        /// "CamelCaseString" => "Camel Case String"
        /// </summary>
        public static string SplitCamelCase(this string camelCaseString) {
            if (string.IsNullOrEmpty(camelCaseString)) return camelCaseString;

            string camelCase = Regex.Replace(Regex.Replace(camelCaseString, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
            string firstLetter = camelCase[..1].ToUpper();

            if (camelCaseString.Length > 1) {
                string rest = camelCase[1..];

                return firstLetter + rest;
            }

            return firstLetter;
        }

    }

}