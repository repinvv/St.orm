﻿namespace St.Orm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class HelpfulExtensions
    {
        /// <summary>
        /// This method exists because of weak syntax of "out".
        /// It forces you to write pretty simple operation in 2 lines of code 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            return key != null && dict.TryGetValue(key, out value) ? value : default(TValue);
        }

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defVal)
        {
            TValue value;
            return key != null && dict.TryGetValue(key, out value) ? value : defVal;
        }

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> defValSelector)
        {
            TValue value;
            return key != null && dict.TryGetValue(key, out value) ? value : defValSelector();
        }

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