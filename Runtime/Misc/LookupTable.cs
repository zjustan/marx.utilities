using System;
using System.Collections.Generic;

namespace Marx.Utilities 
{
    public class LookupTable<TLeft, TRight>
    {
        private List<lookupPair<TLeft, TRight>> items = new();

        public void Add(TLeft left, TRight right)
        {
            foreach (var item in items)
            {
                if (Equals(item.LeftValue, left))
                    throw new Exception($"{typeof(TLeft).Name} on the left already in exists the lookup table");

                if (Equals(item.RightValue, right))
                    throw new Exception($"{typeof(TRight).Name} on the right already exists in the lookup table");
            }

            items.Add(new lookupPair<TLeft, TRight>
            {
                LeftValue = left,
                RightValue = right
            });
        }

        public TLeft Resolve(TRight right)
        {
            foreach (var item in items)
            {
                if (Equals(item.RightValue, right))
                    return item.LeftValue;
            }
            throw new Exception($"{right} does not exist in the lookup table");
        }

        public TRight Resolve(TLeft left)
        {
            foreach (var item in items)
            {
                if (Equals(item.LeftValue, left))
                    return item.RightValue;
            }
            throw new Exception($"{left} does not exist in the lookup table");
        }
        public struct lookupPair<T, U>
        {
            public T LeftValue { get; set; }
            public U RightValue { get; set; }
        }
    }
}
