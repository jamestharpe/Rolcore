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

        [TestMethod]
        public void ToNameValueCollection_ConvertsStringToNameValueCollection()
        {
            var input = "a=b,b=c,a=c,c=d";
            var output = input.ToNameValueCollection(',', '=');
            Assert.AreEqual(3, output.Count);
            Assert.AreEqual("b,c", output["a"]);
            Assert.AreEqual("c", output["b"]);
            Assert.AreEqual("d", output["c"]);
        }

        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void ToNameValueCollection_ThrowsExceptionForBlankEntries()
        {
            var input = "a=b,b=c,,a=c,c=d";
            var output = input.ToNameValueCollection(',', '=');
        }

        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void ToNameValueCollection_ThrowsExceptionForIncorrectlyDelimitedEntries()
        {
            var input = "a=b,b=c,a-c,a=c,c=d";
            var output = input.ToNameValueCollection(',', '=');
        }

        [TestMethod]
        public void IsEquivalentTo_ReturnsTrueForEmptyNameValueCollections()
        {
            var sut1 = new NameValueCollection();
            var sut2 = new NameValueCollection();

            Assert.IsTrue(sut1.IsEquivalentTo(sut2));
        }

        [TestMethod]
        public void IsEquivalentTo_ReturnsTrueForSameNameValueCollections()
        {
            var sut1 = new NameValueCollection();
            var sut2 = sut1;

            Assert.IsTrue(sut1.IsEquivalentTo(sut2));
        }

        [TestMethod]
        public void IsEquivalentTo_ReturnsTrueForEquivalentNameValueCollections()
        {
            var sut1 = (new NameValueCollection()
            {
                { "a", "b" },
                { "c", "d" }
            });

            var sut2 = (new NameValueCollection()
            {
                { "a", "b" },
                { "c", "d" }
            });

            Assert.IsTrue(sut1.IsEquivalentTo(sut2));
        }

        [TestMethod]
        public void IsEquivalentTo_ReturnsFalseForUnEquivalentNameValueCollections()
        {
            var sut1 = (new NameValueCollection()
            {
                { "a", "b" },
                { "c", "d" }
            });

            var sut2 = (new NameValueCollection()
            {
                { "a", "a" },
                { "c", "d" }
            });

            Assert.IsFalse(sut1.IsEquivalentTo(sut2));
        }
    }
}
