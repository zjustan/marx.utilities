using System;
using System.Collections.Generic;

namespace Marx.Utilities
{
    public static class DictionaryExtensions
    {
        public static void SetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return;
            }
            dictionary.Add(key, value);
        }

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> constructor)
        {
            if (dictionary.TryGetValue(key, out TValue existing))
            {
                return existing;
            }
            TValue newValue = constructor();
            return newValue;
        }

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.TryGetValue(key, out TValue existing))
            {
                return existing;
            }
            return value;
        }
    }
}
