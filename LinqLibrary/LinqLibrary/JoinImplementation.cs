using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqLibrary
{
    public static class JoinImplementation
    {
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer, IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            return JoinImpl(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        }

        private static IEnumerable<TResult> JoinImpl<TOuter, TInner, TKey, TResult>(
            IEnumerable<TOuter> outer, IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            IEqualityComparer<TKey> equalityComparer = EqualityComparer<TKey>.Default;
            var lookup = inner.ToLookup(innerKeySelector, equalityComparer);

            foreach (var outerItem in outer)
            {
                var outerKey = outerKeySelector(outerItem);
                foreach (var innerItem in lookup[outerKey])
                {
                    yield return resultSelector(outerItem, innerItem);
                }
            }
        } 
    }
}
