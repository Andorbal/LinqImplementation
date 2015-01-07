using System;
using System.Collections.Generic;

namespace LinqLibrary
{
    public static class SelectImplementation
    {
        /// <summary>
        /// Projects each element from the source
        /// </summary>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> predicate)
        {
            Preconditions.CheckNotNull(predicate, "predicate");

            return SelectInternal(Preconditions.CheckNotNull(source, "source"), (x, y) => predicate(x));
        }

        /// <summary>
        /// Projects each element from the source
        /// </summary>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, 
            Func<TSource, int, TResult> predicate)
        {
            return SelectInternal(Preconditions.CheckNotNull(source, "source"),
                Preconditions.CheckNotNull(predicate, "predicate"));
        }

        /// <summary>
        /// Projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence.
        /// </summary>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, 
            Func<TSource, IEnumerable<TResult>> selector)
        {
            return SelectManyImpl(
                Preconditions.CheckNotNull(source, "source"), 
                Preconditions.CheckNotNull(selector, "collectionSelector"), 
                (x, y) => y);
        }

        /// <summary>
        /// Projects each element of a sequence to an IEnumerable<T>, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein.
        /// </summary>
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector
)
        {
            return SelectManyImpl(
                Preconditions.CheckNotNull(source, "source"), 
                Preconditions.CheckNotNull(collectionSelector, "collectionSelector"),
                Preconditions.CheckNotNull(resultSelector, "resultSelector"));
        }

        public static IEnumerable<TResult> SelectInternal<TSource, TResult>(this IEnumerable<TSource> source, 
            Func<TSource, int, TResult> predicate)
        {
            int i = 0;
            foreach (TSource item in source)
            {
                yield return predicate(item, i++);
            }
        }

        public static IEnumerable<TResult> SelectManyImpl<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (var item in source)
            {
                foreach (var innerItem in collectionSelector(item))
                {
                    yield return resultSelector(item, innerItem);
                }
            }
        }
    }
}
