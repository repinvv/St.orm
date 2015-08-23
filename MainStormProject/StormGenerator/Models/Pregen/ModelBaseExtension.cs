namespace StormGenerator.Models.Pregen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class ModelBaseExtension
    {
        public static List<T2> ActiveSelect<T, T2>(this IEnumerable<T> source, Func<T, T2> selector) where T : ModelBase
        {
            return source.Where(x => x.Enabled).Select(selector).ToList();
        }

        public static List<T> Active<T>(this IEnumerable<T> source) where T : ModelBase
        {
            return source.Where(x => x.Enabled).ToList();
        }

        public static List<T> Active<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : ModelBase
        {
            return source.Where(x => x.Enabled).Where(predicate).ToList();
        }

        public static bool ActiveAny<T>(this IEnumerable<T> source) where T : ModelBase
        {
            return source.Any(x => x.Enabled);
        }

        public static bool ActiveAny<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : ModelBase
        {
            return source.Where(x => x.Enabled).Any(predicate);
        }

        public static int ActiveCount<T>(this IEnumerable<T> source) where T : ModelBase
        {
            return source.Count(x => x.Enabled);
        }

        public static int ActiveCount<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : ModelBase
        {
            return source.Where(x => x.Enabled).Count(predicate);
        }
    }
}
