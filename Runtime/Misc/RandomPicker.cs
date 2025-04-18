using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marx.Utilities
{
    public class RandomPicker<T> where T : IEquatable<T>
    {
        private readonly IEnumerable<T> source;

        private readonly List<T> unpickedValues;

        public RandomPicker(IEnumerable<T> values)
        {
            source = values;
            unpickedValues = new List<T>(values.Count());
            Fill();
        }

        public T Value { get; private set; }

        public T Pick()
        {
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

        public T PickWhere(Func<T, bool> Condition)
        {
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

        public IEnumerable<T> PickRange(int minigameCount)
        {
            for (int i = 0; i < minigameCount; i++)
            {
                yield return Pick();
            }
        }

        public void Remove(T item)
        {
            unpickedValues.Remove(item);
        }

        private void Fill()
        {
            unpickedValues.Clear();
            unpickedValues.AddRange(source);

        }

    }
}