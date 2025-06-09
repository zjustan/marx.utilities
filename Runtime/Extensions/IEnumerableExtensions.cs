using System;
using System.Collections.Generic;
using System.Linq;

namespace Marx.Utilities
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Retrieves a random element from an <see cref="IEnumerable{T}"/> collection.
        /// If the collection is empty, the default value for the type <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="values">The input collection from which a random element will be selected.</param>
        /// <returns>A randomly selected element of the input collection, or the default value of <typeparamref name="T"/> if the collection is empty.</returns>
        public static T Random<T>(this IEnumerable<T> values) {
            // If there are no elements in the collection, return the default value of T
            int count = values.Count();
            if (count == 0)
                return default;

            // Guids as well as the hash code for a guid will be unique and thus random        
            int hashCode = Math.Abs(Guid.NewGuid().GetHashCode());
            return values.ElementAt(hashCode % count);
        }

        /// <summary>
        /// Adds a specified value to an <see cref="IList{T}"/> only if it is not already present in the list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="values">The list to which the value will be added if it is not already contained.</param>
        /// <param name="value">The value to potentially add to the list.</param>
        public static void DistinctAdd<T>(this IList<T> values, T value) {
            if (values.Contains(value)) return;

            values.Add(value);
        }

        /// <summary>
        /// Adds multiple elements from an <see cref="IEnumerable{T}"/> collection to a <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the queue and the collection.</typeparam>
        /// <param name="queue">The queue to which the elements will be added.</param>
        /// <param name="values">The collection of elements to enqueue into the queue.</param>
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> values) {
            foreach (T item in values) {
                queue.Enqueue(item);
            }
        }

        /// <summary>
        /// Selects a random element from a collection of objects implementing the <see cref="IWeightable"/> interface,
        /// taking into account the weight of each element for a weighted random distribution.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection, which must implement <see cref="IWeightable"/>.</typeparam>
        /// <param name="values">The collection of weighted elements to select from.</param>
        /// <returns>A randomly selected element from the collection, weighted by the <see cref="IWeightable.Weight"/> property.</returns>
        /// <exception cref="NotImplementedException">Thrown if the collection is empty.</exception>
        public static T GetWeightedRandom<T>(this ICollection<T> values) where T : IWeightable {
            if (values.Count <= 0) {
                throw new NotImplementedException();
            }

            float totalChance = 0;

            foreach (T spawnData in values)
                totalChance += spawnData.Weight;

            float randomValue = UnityEngine.Random.Range(0f, totalChance);
            float cumulativeChance = 0f;

            foreach (T spawnData in values)
            {
                cumulativeChance += spawnData.Weight;

                if (randomValue < cumulativeChance)
                    return spawnData;
            }

            return values.Random();
        }
    }

    public interface IWeightable
    {
        public float Weight{ get; }
    }
}