//-----------------------------------------------------------------------
// <copyright file="NameValueCollectionExtensions.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Collections.Specialized
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// Provides extension methods related to <see cref="NameValueCollection"/>.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Creates a copy of the current instance that is read-only.
        /// </summary>
        /// <param name="value">Specifies the collection to convert to read-only.</param>
        /// <returns>A read-only instance.</returns>
        public static NameValueCollection ToReadOnly(this NameValueCollection value)
        {
            Contract.Requires<ArgumentNullException>(value != null, "value is null.");

            var result = new NameValueCollectionEx();
            result.Add(value);
            return result.ToReadOnly();
        }

        /// <summary>
        /// Creates a <see cref="NameValueCollection"/> from the specified string and seperator.
        /// </summary>
        /// <param name="value">Specifies the string that is to be converted to a 
        /// <see cref="NameValueCollection"/>.</param>
        /// <param name="listItemSeperator">Specifies the character that separates name-value 
        /// entries.</param>
        /// <param name="keyValueSeperator">Specifies the character that separates the name from 
        /// the value in each name-value pair.</param>
        /// <returns>A <see cref="NameValueCollection"/> representation of the specified string.</returns>
        public static NameValueCollection ToNameValueCollection(this string value, char listItemSeperator, char keyValueSeperator)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(value), "value is null or empty.");

            var result = new NameValueCollection();
            var nameValuePairs = value.Split(new char[] { listItemSeperator });
            for (int i = 0; i < nameValuePairs.Length; i++)
            {
                var nameValue = nameValuePairs[i].Split(new char[] { keyValueSeperator });
                if (nameValue.Length != 2)
                {
                    throw new NotSupportedException("Cannot convert \"" + nameValue + " to name and value.");
                }

                result.Add(nameValue[0].Trim(), nameValue[1].Trim());
            }

            return result;
        }

        /// <summary>
        /// Determines if the <see cref="NameValueCollection"/>s contain exactly the same keys and
        /// values.
        /// </summary>
        /// <param name="value">The first <see cref="NameValueCollection"/> to compare.</param>
        /// <param name="compare">The other <see cref="NameValueCollection"/> to compare.</param>
        /// <returns>True if the <see cref="NameValueCollection"/> instances are equivalent.</returns>
        public static bool IsEquivalentTo(this NameValueCollection value, NameValueCollection compare)
        {
            if (value == compare)
            {
                return true;
            }

            if ((value == null && compare != null) || (value != null && compare == null) || (value.Count != compare.Count))
            {
                return false;
            }

            foreach (string key in value.AllKeys)
            {
                var keyValue = value[key];
                var compareValue = compare[key];
                if (keyValue != compareValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
