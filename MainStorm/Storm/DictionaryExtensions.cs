namespace Storm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        /// <summary>
        /// This method exists because of bogus syntax of "out".
        /// It forces you to write bulky code for simple operation
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
    }
}
