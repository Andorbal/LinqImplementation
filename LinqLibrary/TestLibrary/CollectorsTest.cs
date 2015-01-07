using System;
using System.Collections.Generic;
using FluentAssertions;
using LinqLibrary;
using NUnit.Framework;

namespace TestLibrary
{
    [TestFixture]
    public class CollectorsTest
    {
        [Test]
        public void ToList_WithNullSource_ThrowsArgumentNullException()
        {
            List<string> foo = null;
            Assert.Throws<ArgumentNullException>(() => foo.ToList());
        }

        [Test]
        public void ToList_WithValidSoure_ReturnsCollectionOfAllItems()
        {
            var result = System.Linq.Enumerable.Range(1, 5).ToList();
            result.Should().BeEquivalentTo(1, 2, 3, 4, 5);
        }
    }
}
