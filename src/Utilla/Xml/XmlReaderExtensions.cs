//-----------------------------------------------------------------------
// <copyright file="XmlReaderExtensions.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Xml
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Extension methods for <see cref="XmlReader"/>.
    /// </summary>
    public static class XmlReaderExtensions
    {
        /// <summary>
        /// Reads the contents of the specified <see cref="XmlReader"/> into a string.
        /// </summary>
        /// <param name="xmlReader">Specifies the <see cref="XmlReader"/> to read.</param>
        /// <returns>A string containing the contents of the specified <see cref="XmlReader"/>.</returns>
        public static string ReadToString(this XmlReader xmlReader)
        {
            Contract.Requires<ArgumentException>(xmlReader != null, "xmlReader is null");

            var result = new StringBuilder();
            while (xmlReader.Read())
            {
                result.Append(xmlReader.ReadOuterXml());
            }

            return result.ToString();
        }
    }
}
