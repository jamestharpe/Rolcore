using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilla.Tests
{
    [TestClass]
    public class DateTimeExtensionsTest
    {
        [TestMethod]
        public void IsWithinOneSecondOf_RetrusTrueForEqualValues()
        {
            var dt1 = new DateTime(2015, 01, 25, 6, 49, 55, 327);
            var dt2 = new DateTime(2015, 01, 25, 6, 49, 55, 327);
            Assert.IsTrue(dt1.IsWithinOneSecondOf(dt2));
        }

        [TestMethod]
        public void IsWithinOneSecondOf_RetrusTrueFor999MilisecondsAhead()
        {
            var dt1 = new DateTime(2015, 01, 25, 6, 49, 55, 327);
            var dt2 = dt1.AddMilliseconds(999);
            Assert.IsTrue(dt1.IsWithinOneSecondOf(dt2));
        }

        [TestMethod]
        public void IsWithinOneSecondOf_RetrusTrueFor999MilisecondsBehind()
        {
            var dt1 = new DateTime(2015, 01, 25, 6, 49, 55, 327);
            var dt2 = dt1.AddMilliseconds(-999);
            Assert.IsTrue(dt1.IsWithinOneSecondOf(dt2));
        }

        [TestMethod]
        public void IsWithinOneSecondOf_RetrusFalseForOneSecondAhead()
        {
            var dt1 = new DateTime(2015, 01, 25, 6, 49, 55, 327);
            var dt2 = dt1.AddSeconds(1);
            Assert.IsFalse(dt1.IsWithinOneSecondOf(dt2));
        }

        [TestMethod]
        public void IsWithinOneSecondOf_RetrusFalseForOneSecondBehind()
        {
            var dt1 = new DateTime(2015, 01, 25, 6, 49, 55, 327);
            var dt2 = dt1.AddSeconds(-1);
            Assert.IsFalse(dt1.IsWithinOneSecondOf(dt2));
        }
    }
}
