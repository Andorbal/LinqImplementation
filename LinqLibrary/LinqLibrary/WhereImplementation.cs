using System;
using System.Collections.Generic;

namespace LinqLibrary
{
    public static class WhereImplementation
    {
        /// <summary>
        /// Get elements from source that match the predicate
        /// </summary>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            Preconditions.CheckNotNull(predicate, "predicate");

            return WhereInternal(Preconditions.CheckNotNull(source, "source"), (x, y) => predicate(x));
        }

        /// <summary>
        /// Get elements from source that match the predicate
        /// </summary>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
        {
            return WhereInternal(Preconditions.CheckNotNull(source, "source"), 
                Preconditions.CheckNotNull(predicate, "predicate"));
        }

        public static IEnumerable<T> WhereInternal<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
        {
            int i = 0;
            foreach (T item in source)
            {
                if (predicate(item, i++))
                {
                    yield return item;
                }
            }
        } 
    }
}
