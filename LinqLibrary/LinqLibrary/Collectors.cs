using System.Collections.Generic;

namespace LinqLibrary
{
    public static class Collectors
    {
        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            Preconditions.CheckNotNull(source, "source");

            var results = new List<T>();
            foreach (var item in source)
            {
                results.Add(item);
            }

            return results;
        }
    }
}
