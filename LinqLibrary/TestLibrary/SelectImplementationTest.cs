using System;
using System.Collections.Generic;
using FluentAssertions;
using LinqLibrary;
using NUnit.Framework;

namespace TestLibrary
{
    [TestFixture]
    public class SelectImplementationTests
    {
        [Test]
        public void Select_WithNullSource_ThrowsArgumentNullException()
        {
            List<string> foo = null;
            Assert.Throws<ArgumentNullException>(() => foo.Select(x => x));
        }

        [Test]
        public void Select_WithNullPredicate_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new List<string>().Select((Func<string, bool>)null));
        }

        [Test]
        public void Select_WithValidPredicate_ReturnsFilteredCollection()
        {
            var result = new List<int>();

            foreach (int item in System.Linq.Enumerable.Range(1, 5).Select(x => x * 2))
            {
                result.Add(item);
            }

            result.Should().BeEquivalentTo(2, 4, 6, 8, 10);
        }

        [Test]
        public void Select_ShouldNotEvaluate_UntilCalled()
        {
            System.Linq.Enumerable.Range(1, 10).Select(x =>
            {
                throw new Exception("Didn't work");
                return 1;
            });

            Assert.Pass("Didn't throw exception");
        }

        [Test]
        public void SelectWithIndex_WithNullSource_ThrowsArgumentNullException()
        {
            List<string> foo = null;
            Assert.Throws<ArgumentNullException>(() => foo.Select((x, y) => true));
        }

        [Test]
        public void SelectWithIndex_WithNullPredicate_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new List<string>().Select((Func<string, int, bool>)null));
        }

        [Test]
        public void SelectWithIndex_WithValidPredicate_ReturnsFilteredCollection()
        {
            var result = new List<int>();

            foreach (int item in System.Linq.Enumerable.Range(1, 5).Select((x, y) => x * 2))
            {
                result.Add(item);
            }

            result.Should().BeEquivalentTo(2, 4, 6, 8, 10);
        }

        [Test]
        public void SelectWithIndex_ShouldNotEvaluate_UntilCalled()
        {
            System.Linq.Enumerable.Range(1, 10).Select((x, y) =>
            {
                throw new Exception("Didn't work");
                return 1;
            });

            Assert.Pass("Didn't throw exception");
        }

        [Test]
        public void SelectWithIndex_ShouldPassIndexToPredicate()
        {
            var result = new List<int>();

            foreach (bool item in System.Linq.Enumerable.Range(1, 5).Select((x, y) =>
            {
                result.Add(y);
                return true;
            }))
            {
                Console.WriteLine(item);
            }

            result.Should().BeEquivalentTo(0, 1, 2, 3, 4);
        }

        [Test]
        public void SelectMany_ShouldThrow_WhenSourceIsNull()
        {
            string[] input = null;
            Assert.Throws<ArgumentNullException>(() => input.SelectMany(x => x));
        }

        [Test]
        public void SelectMany_ShouldThrow_WhenCollectionSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new List<string>().SelectMany((Func<string, IEnumerable<char>>)null));
        }

        [Test]
        public void SelectMany_ReturnsEachLetterFromAllStrings()
        {
            var names = new [] { "abc", "def" };
            var result = names.SelectMany(x => x.ToCharArray()).ToList();
            result.Should().BeEquivalentTo('a', 'b', 'c', 'd', 'e', 'f');
        }

        [Test]
        public void SelectManyWithResultSelector_ShouldThrow_WhenSourceIsNull()
        {
            string[] input = null;
            Assert.Throws<ArgumentNullException>(() => input.SelectMany(x => x, (x, y) => false));
        }

        [Test]
        public void SelectManyWithResultSelector_ShouldThrow_WhenCollectionSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new List<string>().SelectMany((Func<string, IEnumerable<char>>)null, (x, y) => false));
        }

        [Test]
        public void SelectMany_ShouldThrow_WhenResultSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new List<string>().SelectMany(x => x, (Func<string, char, object>)null));
        }

        [Test]
        public void SelectMany_WithProjection_ReturnsEachLetterFromAllStrings()
        {
            var names = new[] { "abc", "def" };
            var result = names.SelectMany(x => x, (name, letter) => new { name, letter }).ToList();
            result.Should().BeEquivalentTo(new {name = "abc", letter = 'a'},
                new { name = "abc", letter = 'b' },
                new { name = "abc", letter = 'c' },
                new { name = "def", letter = 'd' },
                new { name = "def", letter = 'e' },
                new { name = "def", letter = 'f' });
        }

        [Test]
        public void SelectMany_DefersExecution()
        {
            var names = new[] { "abc", "def" };
            names.SelectMany(x =>
            {
                Assert.Fail("Did not defer execution");
                return x;
            });

            Assert.Pass("Defered execution");
        }
    }
}
