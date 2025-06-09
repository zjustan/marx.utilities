using System;
using System.Collections.Generic;

namespace Marx.Utilities {
    public static class DictionaryExtensions {
        /// <summary>
        /// Adds a new key-value pair to the dictionary if the key does not already exist;
        /// otherwise, updates the value associated with the key.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to modify.</param>
        /// <param name="key">The key to add or update in the dictionary.</param>
        /// <param name="value">The value to associate with the key.</param>
        public static void SetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            if (dictionary.ContainsKey(key)) {
                dictionary[key] = value;
                return;
            }

            dictionary.Add(key, value);
        }

        /// <summary>
        /// Retrieves the value associated with the specified key if it exists in the dictionary;
        /// otherwise, creates a new value using the provided constructor function, adds it to the dictionary,
        /// and returns the newly created value.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to search or modify.</param>
        /// <param name="key">The key whose value to retrieve or create.</param>
        /// <param name="constructor">The function used to initialize a new value if the key does not exist in the dictionary.</param>
        /// <returns>The value associated with the specified key if it exists, or the new value created and added to the dictionary.</returns>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> constructor) {
            if (dictionary.TryGetValue(key, out TValue existing)) {
                return existing;
            }

            TValue newValue = constructor();
            return newValue;
        }

        /// <summary>
        /// Retrieves the value associated with the specified key if it exists in the dictionary;
        /// otherwise, creates a new value using the provided constructor, adds it to the dictionary,
        /// and returns the newly created value.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to retrieve the value from or add the new value to.</param>
        /// <param name="key">The key to look up in the dictionary.</param>
        /// <param name="constructor">A function used to create a new value if the key does not exist in the dictionary.</param>
        /// <returns>The value associated with the specified key, or the newly created value if the key does not exist.</returns>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue value) {
            if (dictionary.TryGetValue(key, out TValue existing)) {
                return existing;
            }

            return value;
        }
    }
}