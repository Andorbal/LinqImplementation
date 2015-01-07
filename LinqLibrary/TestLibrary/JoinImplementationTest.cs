using System;
using FluentAssertions;
using LinqLibrary;
using NUnit.Framework;

namespace TestLibrary
{
    [TestFixture]
    public class JoinImplementationTest
    {
        [Test]
        public void Join_WithMatchingKeysOnEachSide_ReturnsJoinedCollection()
        {
            string[] left = {"andy", "april", "bob"};
            string[] right = {"advil", "abs", "dole"};

            var result = left.Join(right, x => x[0], y => y[0], Tuple.Create).ToList();
            result.Count.Should().Be(4);
            result.Should().Contain(Tuple.Create("andy", "advil"));
            result.Should().Contain(Tuple.Create("andy", "abs"));
            result.Should().Contain(Tuple.Create("april", "advil"));
            result.Should().Contain(Tuple.Create("april", "abs"));
        }

        [Test]
        public void Join_WithNoMatchingObjects_ReturnsEmptyResult()
        {
            string[] left = { "andy", "april", "bob" };
            string[] right = { "stuff", "goo", "dole" };

            var result = left.Join(right, x => x[0], y => y[0], Tuple.Create).ToList();
            result.Count.Should().Be(0);
        }

        [Test]
        public void Join_DoesNotStartUntilOperatedOn()
        {
            string[] left = { "andy", "april", "bob" };
            string[] right = { "advil", "abs", "dole" };

            var result = left.Join(right, x => x[0], y => y[0], (x, y) =>
            {
                throw new Exception("Didn't work");
                return Tuple.Create(x, y);
            });
        }
    }
}
