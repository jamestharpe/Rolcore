﻿//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Text;
    using System.Text.RegularExpressions;
    using Utilla.IO;

    /// <summary>
    /// Extensions for <see cref="String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The character used instead of '+' for URI safe Base64 encoding.
        /// </summary>
        public const char Base64UriSafePlusAlternative = '-';

        /// <summary>
        /// The character used instead of '/' for URI safe Base64 encoding.
        /// </summary>
        public const char Base64UriSafeSlashAlternative = '_';

        /// <summary>
        /// Determines if the given string is a palindrome (can be read the same forwards and 
        /// backwards).
        /// </summary>
        /// <param name="s">Specifies the string to check.</param>
        /// <returns>True if the string is a palindrome, otherwise false.</returns>
        public static bool IsPalindrome(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return true;
            }

            var notAlphas = new Regex("[^a-z]");
            var alphas = notAlphas.Replace(s.ToLower(), string.Empty);
            var reverseAlphas = alphas.Reverse();
            return string.Equals(alphas, reverseAlphas, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Checks that the string ends with the specified character. If it doesn't then the 
        /// character is appended.
        /// </summary>
        /// <param name="s">Specifies the string to check.</param>
        /// <param name="c">Specifies the character to check for.</param>
        /// <returns>The string, with the trailing character.</returns>
        public static string EnsureTrailing(this string s, char c)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null");
            
            var length = s.Length;

            if (length == 0)
            {
                return c.ToString();
            }

            if (s[length - 1] != c)
            {
                return s + c;
            }
            
            return s;
        }

        /// <summary>
        /// Returns the first part of a string, up to a specified number of characters.
        /// </summary>
        /// <param name="s">The string from which to get the sub string.</param>
        /// <param name="numberOfCharacters">The number of characters to get from the string</param>
        /// <returns>The first part of the string.</returns>
        public static string First(this string s, int numberOfCharacters)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null");
            Contract.Requires<ArgumentOutOfRangeException>(numberOfCharacters >= 0, "numberOfCharacters is less than zero");

            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            else if (s.Length > numberOfCharacters)
            {
                return s.Substring(0, numberOfCharacters);
            }

            return s;
        } // Tested

        /// <summary>
        /// Repeats a string some number of times.
        /// </summary>
        /// <param name="s">The string to be repeated.</param>
        /// <param name="numberOfTimes">The number of times to repeat the string. A value of zero
        /// returns an empty string, a value of one returns the original string, a value of two or
        /// more returns the string repeated that number of times.</param>
        /// <returns><see cref="string"/>.</returns>
        public static string Repeat(this string s, int numberOfTimes)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null");
            Contract.Requires<ArgumentOutOfRangeException>(numberOfTimes >= 0, "numberOfTimes is less than zero");

            if (numberOfTimes == 0)
            {
                return string.Empty; // repeated zero times
            }
            
            var result = new StringBuilder(s);
            for (int i = 1; i < numberOfTimes; i++)
            {
                result.Append(s);
            }

            return result.ToString();
        } // Tested

        /// <summary>
        /// Reverses the given string.
        /// </summary>
        /// <param name="s">Specfies the string to reverse.</param>
        /// <returns>A reversed string.</returns>
        public static string Reverse(this string s)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null");
            var result = s.ToCharArray();
            Array.Reverse(result);
            return new string(result);
        }

        /// <summary>
        /// Converts the string to a <see cref="Uri"/>.
        /// </summary>
        /// <param name="s">Specifies the string to convert.</param>
        /// <param name="baseUri">Optionally specifies the base URI if the specified string is (or might be) relative.</param>
        /// <returns>The Uri represented by the given string.</returns>
        public static Uri ToUri(this string s, Uri baseUri = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(s), "s is null or empty");

            var result = (baseUri == null)
                ? new Uri(s, UriKind.RelativeOrAbsolute) // the constructor will error if it's passed a relative path but not specified as such
                : new Uri(baseUri, s);

            return result;
        }

        /// <summary>
        /// Computes a SHA-1 hash of the specified <see cref="string"/>.
        /// </summary>
        /// <param name="s">The string to hash.</param>
        /// <returns>A SHA-1 hash.</returns>
        public static string ToSHA1String(this string s)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null");
            return s.ToStream().ToSHA1String();
        } // TODO: Test

        #region Base64 Utils
        /// <summary>
        /// Converts the string to a Base64 encoded string.
        /// </summary>
        /// <param name="s">The string to encode</param>
        /// <returns>The Base64 representation of the string.</returns>
        public static string ToBase64String(this string s)
        {
            var result = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
            return result;
        }

        /// <summary>
        /// Converts the string to a URI-safe Base64 encoded string. Read about URL and file safe
        /// base64 encoding at http://tools.ietf.org/html/rfc4648#page-7.
        /// </summary>
        /// <param name="s">The string to encode</param>
        /// <returns>The Base64 representation of the string.</returns>
        public static string ToBase64UriSafeString(this string s)
        {
            var result = new StringBuilder(s.ToBase64String())
                .Replace('+', Base64UriSafePlusAlternative)
                .Replace('/', Base64UriSafeSlashAlternative);
            return result.ToString();
        }

        /// <summary>
        /// Converts the string from a Base64 encoded string to a "regular" string.
        /// </summary>
        /// <param name="s">The string to decode.</param>
        /// <returns>The original string.</returns>
        public static string FromBase64String(this string s)
        {
            var result = Encoding.UTF8.GetString(Convert.FromBase64String(s));
            return result;
        }

        /// <summary>
        /// Converts the string from a Base64 encoded string to a "regular" string.
        /// </summary>
        /// <param name="s">The string to decode.</param>
        /// <returns>The original string.</returns>
        public static string FromBase64UriSafeString(this string s)
        {
            s = new StringBuilder(s)
                .Replace(Base64UriSafePlusAlternative, '+')
                .Replace(Base64UriSafeSlashAlternative, '/')
                .ToString();
            var result = s.FromBase64String();
            return result;
        }
        #endregion Base64 Utils
    }
}
