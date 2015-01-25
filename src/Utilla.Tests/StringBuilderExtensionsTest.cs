using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Utilla.Text;

namespace Utilla.Tests
{
    [TestClass]
    public class StringBuilderExtensionsTest
    {
        [TestMethod]
        public void AppendIf_AppendsWhenConditionIsTrue()
        {
            var sut = new StringBuilder("Hello");
            sut.AppendIf(true, " world");
            Assert.AreEqual("Hello world", sut.ToString());
        }

        [TestMethod]
        public void AppendIf_DoesNotAppendWhenConditionIsFalse()
        {
            var sut = new StringBuilder("Hello");
            sut.AppendIf(false, " world");
            Assert.AreNotEqual("Hello world", sut.ToString());
        }
    }
}
