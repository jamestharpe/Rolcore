//-----------------------------------------------------------------------
// <copyright file="XElementExtensionMethods.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Xml.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Extension methods for <see cref="XElement"/>.
    /// </summary>
    public static class XElementExtensionMethods
    {
        /// <summary>
        /// Converts an <see cref="XElement"/> to an <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="element">Specifies the <see cref="XElement"/> to convert.</param>
        /// <returns>An <see cref="XmlDocument"/>.</returns>
        public static XmlDocument ToXmlDocument(this XElement element)
        {   
            Contract.Requires<ArgumentNullException>(element != null, "xElement is null");

            var result = new XmlDocument();
            result.LoadXml(element.ToString());
            return result;
        } // TODO: Unit test

        /// <summary>
        /// Retrieves the child element value  
        /// </summary>
        /// <param name="parentElement">The parent element which contains the child element required</param>
        /// <param name="childElementName">Name of the child element to be retrieved</param>
        /// <returns>Returns the value of the child element, if it finds or returns null</returns>
        public static string GetChildElementValue(this XElement parentElement, string childElementName)
        {
            Contract.Requires<ArgumentNullException>(parentElement != null, "parentElement is null");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(childElementName), "childElementName is null or empty");
            
            var resultElement = parentElement.Elements().Where(e => e.Name.LocalName == childElementName).FirstOrDefault();
            return (resultElement == null) ? string.Empty : resultElement.Value;
        } // TODO: Unit test

        /// <summary>
        /// Retrieves all the elements by specified name
        /// </summary>
        /// <param name="xmlReader">The XML reader to read from.</param>
        /// <param name="nameOfTheElement">The name of the element go retrieve.</param>
        /// <returns>Returns an IEnumerable collection of elements with specified name</returns>
        public static IEnumerable<XElement> GetAllElementsByNameFromXmlReader(XmlReader xmlReader, string nameOfTheElement)
        {
            Contract.Requires<ArgumentNullException>(xmlReader != null, "xmlReader is null");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(nameOfTheElement), "nameOfTheElement is null or empty");

            return XElement.Load(xmlReader).Descendants().Where(e => e.Name.LocalName == nameOfTheElement);
        }
    }
}
