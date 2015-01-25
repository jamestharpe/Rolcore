//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTest.cs" company="Utilla">
//     Copyright © Utilla Contributors
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
        public void ContainsHtml_ReturnsTrueWhenStringContainsHtml()
        {
            string s = "<a>";
            bool actual = s.ContainsHtml();
            Assert.IsTrue(actual);

            s = "<a href='foobar'>";
            actual = s.ContainsHtml();
            Assert.IsTrue(actual);

            s = "<a href=\"foobar\">";
            actual = s.ContainsHtml();
            Assert.IsTrue(actual);

            s = "<a href=\"foobar\">biz baz";
            actual = s.ContainsHtml();
            Assert.IsTrue(actual);

            s = "<a href=\"foobar\">biz baz</a> bang!";
            actual = s.ContainsHtml();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ToUri_ConvertsStringToUri()
        {
            string expected = "http://www.utilla.com/";
            string actual = expected.ToUri().ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ToUri_RequiresNonEmptyString()
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
