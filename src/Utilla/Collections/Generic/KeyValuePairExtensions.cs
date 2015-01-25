//-----------------------------------------------------------------------
// <copyright file="KeyValuePairExtensions.cs" company="Utilla">
//     Copyright © Utilla 
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// Extension methods for <see cref="KeyValuePair"/>.
    /// </summary>
    public static class KeyValuePairExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="KeyValuePair"/> to a 
        /// <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="value">Specifies the value to convert.</param>
        /// <returns>A <see cref="NameValueCollection"/> containing the values from the 
        /// <see cref="KeyValuePair{}"/>.</returns>
        public static NameValueCollection ToNameValueCollection(this KeyValuePair<string, string>[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "value is null.");
            }

            var result = new NameValueCollection(value.Length);
            foreach (var item in value)
            {
                result.Add(item.Key, item.Value);
            }

            return result;
        }
    }
}
