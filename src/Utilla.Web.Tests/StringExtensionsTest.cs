//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTest.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------

namespace Utilla.Web.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilla.Web;

    /// <summary>
    /// Tests for <see cref="StringExtensions"/>.
    /// </summary>
    [TestClass]
    public class StringExtensionsTest
    {
        #region ContainsHtml Tests

        /// <summary>
        /// A test for ContainsHtml
        /// </summary>
        [TestMethod]
        public void ContainsHtml_ReturnsTrueWhenStringContainsATag()
        {
            Assert.IsTrue("<a>".ContainsHtml());
            Assert.IsTrue("<a href='foobar'>".ContainsHtml());
            Assert.IsTrue("<a href=\"foobar\">".ContainsHtml());
            Assert.IsTrue("<a href=\"foobar\">biz baz".ContainsHtml());
            Assert.IsTrue("<a href=\"foobar\">biz baz</a> bang!".ContainsHtml());
        }

        [TestMethod]
        public void ContainsHtml_ReturnsTrueWhenStringContainsH1Tag()
        {
            Assert.IsTrue("<h1>".ContainsHtml());
            Assert.IsTrue("<h1 id=\"foobar\">".ContainsHtml());
            Assert.IsTrue("<h1/>".ContainsHtml());
            Assert.IsTrue("<h1>Heading 1".ContainsHtml());
            Assert.IsTrue("<h1>Heading 1</h1>".ContainsHtml());
            Assert.IsTrue("<h1>Heading 1</h1> and then some".ContainsHtml());
        }

        [TestMethod]
        public void ContainsHtml_ReturnsFalseWhenStringDoesNotContainHtml()
        {
            Assert.IsFalse("h1".ContainsHtml());
            Assert.IsFalse("h1 id=\"foobar\"".ContainsHtml());
            Assert.IsFalse("h1/".ContainsHtml());
            Assert.IsFalse("h1 Heading 1 /h1".ContainsHtml());
            Assert.IsFalse("A > B and B < C".ContainsHtml());
            Assert.IsFalse("A < B and B > C".ContainsHtml());
        }

        #endregion ContainsHtml Tests

        [TestMethod]
        public void RemoveHtml_RemovesHtml()
        {
            Assert.AreEqual(
                "Hello world!\nClick here.", 
                "<h1>Hello world!</h1>\n<p><a href=\"http://www.jamestharpe.com/\">Click here.</a></p>".RemoveHtml());
        }

        #region ToUri Tests

        [TestMethod, SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
        public void ToUri_ConvertsStringToUri()
        {
            string expected = "http://www.jamestharpe.com/";
            string actual = expected.ToUri().ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException)), SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
        public void ToUri_RequiresNonEmptyString()
        {
            var actual = string.Empty.ToUri().ToString();
            Assert.Fail();
        }

        #endregion ToUri Tests
    }
}