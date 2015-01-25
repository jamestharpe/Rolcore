//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTest.cs" company="Utilla">
//     Copyright © Utilla 
// </copyright>
//-----------------------------------------------------------------------

namespace Utilla.Web.Tests
{
    using Utilla.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    
    /// <summary>
    /// Tests for <see cref="StringExtensions"/>.
    /// </summary>
    [TestClass]
    public class StringExtensionsTest
    {
        /// <summary>
        /// A test for ContainsHtml
        /// </summary>
        [TestMethod]
        public void ContainsHtmlPositiveTest()
        {
            string s = "<a>";
            bool actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href='foobar'>";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href=\"foobar\">";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href=\"foobar\">biz baz";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href=\"foobar\">biz baz</a> bang!";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);
        }

        /// <summary>
        ///A test for ToUri
        ///</summary>
        [TestMethod]
        public void ToUriTest()
        {
            string expected = "http://www.utilla.com/";
            string actual = expected.ToUri().ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToUri
        ///</summary>
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ToUriEmptyStringTest()
        {
            string expected = string.Empty;
            string actual = expected.ToUri().ToString();
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContainsHtml
        ///</summary>
        //TODO: negative tests
        //[TestMethod]
        //public void ContainsHtmlNegativeTest()
        //{
        //}
    }
}
