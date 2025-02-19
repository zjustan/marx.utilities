using System;
using System.Collections.Generic;
using System.Linq;

namespace Marx.Utilities
{
    public static class IEnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> values)
        {
            // If there are no elements in the collection, return the default value of T
            int count = values.Count();
            if (count == 0)
                return default;

            // Guids as well as the hash code for a guid will be unique and thus random        
            int hashCode = Math.Abs(Guid.NewGuid().GetHashCode());
            return values.ElementAt(hashCode % count);
        }

        public static void DistinctAdd<T>(this IList<T> values, T value)
        {
            if (values.Contains(value))
                return;

            values.Add(value);
        }

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> values)
        {
            foreach (T item in values)
            {
                queue.Enqueue(item);
            }
        }

        public static T GetWeightedRandom<T>(this ICollection<T> values) where T : IWeightable
        {
            if (values.Count <= 0)
            {
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