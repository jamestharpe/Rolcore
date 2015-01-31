//-----------------------------------------------------------------------
// <copyright file="NameValueCollectionExtensionsTest.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilla.Collections.Specialized;

    [TestClass, SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    public class NameValueCollectionExtensionsTest
    {
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void ToReadOnly_ConvertsToReadOnly()
        {
            var sut = (new NameValueCollection()).ToReadOnly();
            sut.Add("this should", "throw an exception");
            Assert.Fail();
        }

        [TestMethod]
        public void ToReadOnly_PreservesOriginalCollectionValuesValues()
        {
            var sut = (new NameValueCollection()
            {
                { "a", "b" },
                { "c", "d" }
            }).ToReadOnly();

            Assert.AreEqual(2, sut.Count);
            Assert.AreEqual("b", sut["a"]);
            Assert.AreEqual("d", sut["c"]);
        }
    }
}
