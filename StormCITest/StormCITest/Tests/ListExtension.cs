namespace StormCITest.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class ListExtension
    {
        public static List<T> Portion<T>(this List<T> src, int divider)
        {
            return src.Take(src.Count / divider).ToList();
        }
    }
}
