﻿//-----------------------------------------------------------------------
// <copyright file="CommonExpressions.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Text.RegularExpressions
{
    /// <summary>
    /// Commonly used regular expressions.
    /// </summary>
    public sealed class CommonExpressions
    {
        /// <summary>
        /// A regular expression string that will match most HTML tags.
        /// </summary>
        public const string HtmlTag = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>";
    }
}
