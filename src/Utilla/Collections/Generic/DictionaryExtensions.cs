﻿//-----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// Extension methods for <see cref="Dictionary"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Dictionary{string, string}"/> to a 
        /// <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="value">Specifies the value to convert.</param>
        /// <returns>A <see cref="NameValueCollection"/> containing the items from the dictionary.</returns>
        public static NameValueCollection ToNameValueCollection(this Dictionary<string, string> value)
        {
            Contract.Requires<ArgumentNullException>(value != null, "value is null");

            return value.ToArray().ToNameValueCollection();
        }
    }
}
