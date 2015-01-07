using System;
using System.Collections.Generic;
using FluentAssertions;
using LinqLibrary;
using NUnit.Framework;

namespace TestLibrary
{
    [TestFixture]
    public class WhereImplementationTests
    {
        [Test]
        public void Where_WithNullSource_ThrowsArgumentNullException()
        {
            List<string> foo = null;
            Assert.Throws<ArgumentNullException>(() => foo.Where(x => true));
        }

        [Test]
        public void Where_WithNullPredicate_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new List<string>().Where((Func<string, bool>)null));
        }

        [Test]
        public void Where_WithValidPredicate_ReturnsFilteredCollection()
        {
            var result = new List<int>();

            foreach (int item in System.Linq.Enumerable.Range(1, 10).Where(x => x % 2 == 0))
            {
                result.Add(item);
            }

            result.Count.Should().Be(5);
        }

        [Test]
        public void Where_ShouldNotEvaluate_UntilCalled()
        {
            var result = System.Linq.Enumerable.Range(1, 10).Where(x =>
            {
                throw new Exception("Didn't work");
            });

            Assert.Pass("Didn't throw exception");
        }

        [Test]
        public void WhereWithIndex_WithNullSource_ThrowsArgumentNullException()
        {
            List<string> foo = null;
            Assert.Throws<ArgumentNullException>(() => foo.Where((x, y) => true));
        }

        [Test]
        public void WhereWithIndex_WithNullPredicate_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new List<string>().Where((Func<string, int, bool>)null));
        }

        [Test]
        public void WhereWithIndex_WithValidPredicate_ReturnsFilteredCollection()
        {
            var result = new List<int>();

            foreach (int item in System.Linq.Enumerable.Range(1, 10).Where((x, y) => x % 2 == 0))
            {
                result.Add(item);
            }

            result.Count.Should().Be(5);
        }

        [Test]
        public void WhereWithIndex_ShouldNotEvaluate_UntilCalled()
        {
            var result = System.Linq.Enumerable.Range(1, 10).Where((x, y) =>
            {
                throw new Exception("Didn't work");
            });

            Assert.Pass("Didn't throw exception");
        }

        [Test]
        public void WhereWithIndex_ShouldPassIndexToPredicate()
        {
            var result = new List<int>();

            foreach (int item in System.Linq.Enumerable.Range(1, 5).Where((x, y) =>
            {
                result.Add(y);
                return true;
            }))
            {

            }

            result.Should().BeEquivalentTo(0, 1, 2, 3, 4);
        }
    }
}
