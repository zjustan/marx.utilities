using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Marx.Utilities {
    public static class StringExtensions {

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        public static bool NotNullOrEmpty(this string str) => !string.IsNullOrEmpty(str);

        public static string RemoveStart(this string str, string remove) {
            int index = str.IndexOf(remove, StringComparison.Ordinal);
            return index < 0 ? str : str.Remove(index, remove.Length);
        }

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