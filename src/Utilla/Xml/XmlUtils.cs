//-----------------------------------------------------------------------
// <copyright file="XmlUtils.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Xml
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Static class containing utility methods for working with XML.
    /// </summary>
    public static class XmlUtils
    {
        /// <summary>
        /// Gets the specified element from the node list.
        /// </summary>
        /// <param name="nodeList">The node list</param>
        /// <param name="elementName">The name of the element to get</param>
        /// <returns>The element's inner text.</returns>
        public static string GetElementFromNodeList(XmlNodeList nodeList, string elementName)
        {
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == elementName)
                {
                    return node.InnerText;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Deserializes the given XML into an object instance.
        /// </summary>
        /// <typeparam name="T">The type of object to be deserialized.</typeparam>
        /// <param name="xml">The XML to deserialize in to an instance of <see cref="T"/>.</param>
        /// <returns>An instance of <see cref="T"/> deserialized from the given XML.</returns>
        public static T DeserializeFromXml<T>(string xml)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(xml), "xml is null or empty");

            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new StringReader(xml))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
