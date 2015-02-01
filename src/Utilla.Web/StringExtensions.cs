using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Utilla.Text.RegularExpressions;


namespace Utilla.Web
{
    public static class StringExtensions
    {

        /// <summary>
        /// Returns true if the string contains HTML.
        /// </summary>
        /// <param name="s">The string to check for HTML.</param>
        /// <returns>True if the string contains HTML.</returns>
        public static bool ContainsHtml(this string s)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null");

            var htmlTagMatcher = CommonExpressions.HtmlTag.ToRegEx(RegexOptions.IgnoreCase);
            var htmlTagMatch = htmlTagMatcher.Match(s);
            return htmlTagMatch.Success;
        }

        public static string RemoveHtml(this string s)
        {
            if (s == null)
                throw new ArgumentNullException("s is null.", "s");

            if (s.Length == 0)
                return s;

            var htmlTagMatcher = CommonExpressions.HtmlTag.ToRegEx(RegexOptions.IgnoreCase);
            return htmlTagMatcher.Replace(s, String.Empty);
        }

        public static string UrlDecode(this string s)
        {
            return System.Web.HttpUtility.UrlDecode(s);
        }

        public static string UrlEncode(this string s)
        {
            return System.Web.HttpUtility.UrlEncode(s);
        }
    }
}
