using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Marx.Utilities {

    /// <summary>
    /// Provides utility methods for working with enumerations of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the enumeration. Must be a struct and an enumeration.
    /// </typeparam>
    public static class Enum<T> where T : struct {
        public static bool TryGetDefinedValue<T>(int input, [NotNullWhen(true)] out T? value) where T : struct, IConvertible
        {
            value = default;
            if (!Enum.IsDefined(typeof(T), input)) return false;
            value = (T)Enum.ToObject(typeof(T), input);
            return true;

        }

        public static IEnumerable<T> GetValues() => Enum.GetValues(typeof(T)).Cast<T>();
    }
    
}