namespace StormGenerator.Models.Pregen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class SwitcheableExtension
    {
        public static List<T> Active<T>(this IEnumerable<T> source) where T : Switcheable
        {
            return source.Where(x => x.Enabled).ToList();
        }

        public static bool AnyActive<T>(this IEnumerable<T> source) where T : Switcheable
        {
            return source.Any(x => x.Enabled);
        }

        public static int ActiveCount<T>(this IEnumerable<T> source) where T : Switcheable
        {
            return source.Count(x => x.Enabled);
        }

        public static int ActiveCount<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : Switcheable
        {
            return source.Where(x => x.Enabled).Count(predicate);
        }
    }
}
