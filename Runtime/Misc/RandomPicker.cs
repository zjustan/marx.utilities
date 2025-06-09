using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marx.Utilities
{
    /// <summary>
    /// Represents a utility class used to pick random items from a collection of values.
    /// The collection supports ensuring values are not repeated until all items have been picked.
    /// </summary>
    /// <typeparam name="T">The type of item stored in the collection. Must implement IEquatable&lt;T&gt;.</typeparam>
    public class RandomPicker<T> where T : IEquatable<T> {
        
        private readonly IEnumerable<T> source;

        private readonly List<T> unpickedValues;

        public RandomPicker(IEnumerable<T> values)
        {
            source = values;
            unpickedValues = new List<T>(values.Count());
            Fill();
        }
        
        /// <summary>
        /// Gets the most recently picked value from the collection.
        /// The value is updated every time a new item is picked using the <see cref="Pick"/> method.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Picks a random item from the collection, ensuring it does not repeat the most recently picked item.
        /// Refills the collection with all items when all have been picked.
        /// </summary>
        /// <returns>
        /// A randomly selected item of type T from the collection.
        /// </returns>
        public T Pick() {
            if (unpickedValues.Count == 0)
                Fill();

            T newValue;
            int newIndex;

            int limit = 999;
            
            do
            {
                newIndex = UnityEngine.Random.Range(0, unpickedValues.Count);
                newValue = unpickedValues[newIndex];
                
            }
            while (newValue.Equals(Value) || --limit < -1);

            if (limit <= 0)
            {
                Debug.LogError("Limit reached while trying to pick random value");
            }

            unpickedValues.RemoveAt(newIndex);
            return Value = newValue;
        }

        /// <summary>
        /// Picks a random item from the collection that satisfies the specified condition,
        /// ensuring it does not repeat the most recently picked item.
        /// Refills the collection with all items when all have been picked or no items satisfy the condition.
        /// </summary>
        /// <param name="Condition">
        /// A function defining the condition that an item must satisfy to be selected.
        /// </param>
        /// <returns>
        /// A randomly selected item of type T from the collection that meets the specified condition.
        /// </returns>
        public T PickWhere(Func<T, bool> Condition) {
            if (unpickedValues.Count == 0 || !unpickedValues.Any(Condition))
                Fill();

            T newValue;
            int newIndex;

            int limit = 999;
            
            do
            {
                newIndex = UnityEngine.Random.Range(0, unpickedValues.Count);
                newValue = unpickedValues[newIndex];
            }
            while (newValue.Equals(Value) || !Condition(newValue) || --limit < -1);

            
            if (limit <= 0)
            {
                Debug.LogError("Limit reached while trying to pick random value");
            }
            
            unpickedValues.RemoveAt(newIndex);
            return Value = newValue;
        }

        /// <summary>
        /// Picks a specified number of random items from the collection, ensuring they do not repeat the most recently picked items.
        /// Refills the collection with all items when all have been picked.
        /// </summary>
        /// <param name="count">
        /// The number of random items to pick from the collection.
        /// </param>
        /// <returns>
        /// A collection of randomly selected items of type T.
        /// </returns>
        public IEnumerable<T> PickRange(int count) {
            for (int i = 0; i < count; i++) {
                yield return Pick();
            }
        }

        /// <summary>
        /// Removes the specified item from the collection of unpicked values.
        /// </summary>
        /// <param name="item">The item to be removed from the collection.</param>
        public void Remove(T item) {
            unpickedValues.Remove(item);
        }

        private void Fill()
        {
            unpickedValues.Clear();
            unpickedValues.AddRange(source);

        }

    }
}