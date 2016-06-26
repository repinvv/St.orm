namespace StormGenerator
{
    using System.Collections.Generic;

    public static class EnumerableExtension
    {
        public static IEnumerable<List<T>> SplitInGroupsBy<T>(this IEnumerable<T> source, int groupSize)
        {
            var collection = source as ICollection<T>;
            if (collection != null)
            {
                var batchCount = (collection.Count / groupSize) + (collection.Count % groupSize == 0 ? 0 : 1);
                groupSize = (collection.Count / batchCount) + (collection.Count % batchCount == 0 ? 0 : 1);
            }

            var list = new List<T>();
            foreach (T item in source)
            {
                list.Add(item);
                if (list.Count >= groupSize)
                {
                    yield return list;
                    list = new List<T>();
                }
            }

            if (list.Count > 0)
            {
                yield return list;
            }
        }
    }
}
