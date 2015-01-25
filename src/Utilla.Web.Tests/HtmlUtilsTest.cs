//-----------------------------------------------------------------------
// <copyright file="HtmlUtilsTest.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilla.Web.UI;

    /// <summary>
    ///This is a test class for HtmlUtilsTest and is intended
    ///to contain all HtmlUtilsTest Unit Tests
    ///</summary>
    [TestClass]
    public class HtmlUtilsTest
    {
        /// <summary>
        ///A test for CreateAttributeRegEx
        ///</summary>
        [TestMethod]
        public void CreateAttributeRegExTest()
        {
            string attributeName = "href"; // TODO: Initialize to an appropriate value
            var actual = HtmlUtils.CreateAttributeRegEx(attributeName);

            const string anchorHrefStandardDblQuote = "<p><a href=\"https://github.com/jamestharpe/Utilla\">Go to Utilla</a></p>";
            var matchValue = actual.Match(anchorHrefStandardDblQuote).Value;
            Assert.AreEqual(" href=\"https://github.com/jamestharpe/Utilla\"", matchValue);

            const string anchorHrefStandardSnglQuote = "<p><a href='https://github.com/jamestharpe/Utilla'>Go to Utilla</a></p>";
            matchValue = actual.Match(anchorHrefStandardSnglQuote).Value;
            Assert.AreEqual(" href='https://github.com/jamestharpe/Utilla'", matchValue);

            const string anchorHrefStandardNoQuote = "<p><a href=https://github.com/jamestharpe/Utilla>Go to Utilla</a></p>";
            matchValue = actual.Match(anchorHrefStandardNoQuote).Value;
            Assert.AreEqual(" href=https://github.com/jamestharpe/Utilla", matchValue);

            const string anchorHrefMismatchDblSnglQuote = "<p><a href=\"https://github.com/jamestharpe/Utilla'>Go to Utilla</a></p>";
            matchValue = actual.Match(anchorHrefMismatchDblSnglQuote).Value;
            Assert.AreEqual(" href=\"https://github.com/jamestharpe/Utilla'", matchValue);

            const string anchorHrefMismatchSnglDblQuote = "<p><a href='https://github.com/jamestharpe/Utilla\">Go to Utilla</a></p>";
            matchValue = actual.Match(anchorHrefMismatchSnglDblQuote).Value;
            Assert.AreEqual(" href='https://github.com/jamestharpe/Utilla\"", matchValue);

            const string anchorHrefOpenDblQuote = "<p><a href=\"https://github.com/jamestharpe/Utilla>Go to Utilla</a></p>";
            matchValue = actual.Match(anchorHrefOpenDblQuote).Value;
            Assert.AreEqual(" href=\"https://github.com/jamestharpe/Utilla", matchValue);

            const string anchorHrefOpenSnglQuote = "<p><a href='https://github.com/jamestharpe/Utilla>Go to Utilla</a></p>";
            matchValue = actual.Match(anchorHrefOpenSnglQuote).Value;
            Assert.AreEqual(" href='https://github.com/jamestharpe/Utilla", matchValue);
        }
    }
}
