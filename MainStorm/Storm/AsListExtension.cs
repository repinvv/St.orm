namespace Storm
{
    using System.Collections.Generic;
    using System.Linq;

    public static class AsListExtension
    {
        /// <summary>
        /// Regular ToList method forces creation of new list object,
        /// AsList will first try to cast it to list, and only then will make new list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<T> AsList<T>(this IEnumerable<T> input)
        {
            return input as List<T> ?? input.ToList();
        }
    }
}
