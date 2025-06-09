using System;
using System.Collections.Generic;

namespace Marx.Utilities
{
    /// <summary>
    /// Represents a bidirectional lookup table that facilitates mapping between two distinct types of values.
    /// </summary>
    /// <typeparam name="TLeft">The type of the values on the left-hand side of the mapping.</typeparam>
    /// <typeparam name="TRight">The type of the values on the right-hand side of the mapping.</typeparam>
    public class LookupTable<TLeft, TRight> {
        private List<LookupPair<TLeft, TRight>> items = new();

        /// <summary>
        /// Adds a new mapping between the specified left-hand and right-hand values to the lookup table.
        /// </summary>
        /// <param name="left">The left-hand side value to add to the lookup table.</param>
        /// <param name="right">The right-hand side value to add to the lookup table.</param>
        /// <exception cref="Exception">
        /// Thrown if the value of <paramref name="left"/> is already present in the lookup table
        /// or if the value of <paramref name="right"/> is already present in the lookup table.
        /// </exception>
        public void Add(TLeft left, TRight right) {
            foreach (var item in items) {
                if (Equals(item.LeftValue, left)) throw new Exception($"{typeof(TLeft).Name} on the left already in exists the lookup table");

                if (Equals(item.RightValue, right))
                    throw new Exception($"{typeof(TRight).Name} on the right already exists in the lookup table");
            }

            items.Add(new LookupPair<TLeft, TRight>
            {
                LeftValue = left,
                RightValue = right
            });
        }

        /// <summary>
        /// Resolves the left-hand side value associated with the specified right-hand side value in the lookup table.
        /// </summary>
        /// <param name="right">The right-hand side value to resolve.</param>
        /// <returns>The left-hand side value associated with the specified right-hand side value.</returns>
        /// <exception cref="Exception">Thrown if the specified <paramref name="right"/> value does not exist in the lookup table.</exception>
        public TLeft Resolve(TRight right) {
            foreach (var item in items) {
                if (Equals(item.RightValue, right)) return item.LeftValue;
            }
            throw new Exception($"{right} does not exist in the lookup table");
        }

        /// <summary>
        /// Resolves the right-hand side value from the lookup table corresponding to the specified left-hand side value.
        /// </summary>
        /// <param name="left">The left-hand side value to find the mapped right-hand side value for.</param>
        /// <returns>The right-hand side value associated with the specified left-hand side value.</returns>
        /// <exception cref="Exception">
        /// Thrown if the specified <paramref name="left"/> does not exist in the lookup table.
        /// </exception>
        public TRight Resolve(TLeft left) {
            foreach (var item in items) {
                if (Equals(item.LeftValue, left)) return item.RightValue;
            }
            throw new Exception($"{left} does not exist in the lookup table");
        }

        /// <summary>
        /// Attempts to resolve the right-hand side value associated with the specified left-hand side value.
        /// </summary>
        /// <param name="left">The left-hand side value to resolve.</param>
        /// <param name="right">When this method returns, contains the resolved right-hand side value if the resolution is successful; otherwise, the default value for the type of <paramref name="right"/>.</param>
        /// <returns>
        /// True if the resolution is successful and the specified left-hand side value exists in the lookup table; otherwise, false.
        /// </returns>
        public bool TryResolve(TLeft left, out TRight right) {
            right = default;
            foreach (var item in items) {
                if (!Equals(item.LeftValue, left)) continue;
                right = item.RightValue;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to find and retrieve the left-hand side value that corresponds to the specified right-hand side value.
        /// </summary>
        /// <param name="right">The right-hand side value for which to find a matching left-hand side value in the lookup table.</param>
        /// <param name="left">
        /// When this method returns, contains the left-hand side value associated with the specified <paramref name="right"/>,
        /// if such a mapping exists; otherwise, contains the default value for the type of <paramref name="left"/>.
        /// </param>
        /// <returns>
        /// true if a mapping for the specified <paramref name="right"/> was found; otherwise, false.
        /// </returns>
        public bool TryResolve(TRight right, out TLeft left) {
            left = default;
            foreach (var item in items) {
                if (!Equals(item.RightValue, right)) continue;
                left = item.LeftValue;
                return true;
            }

            return false;
        }
        
        private struct LookupPair<T, U>
        {
            public T LeftValue { get; set; }
            public U RightValue { get; set; }
        }
    }
}
