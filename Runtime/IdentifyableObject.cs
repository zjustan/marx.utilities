using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marx.Utilities 
{

    public class IdentifyableObject<T> : ScriptableObject, IEquatable<T> where T : IdentifyableObject<T>
    {
        [field: SerializeField] public SGuid Guid { get; private set; }

        public static T Get(SGuid id)
        {
            Lookup.TryGetValue(id, out var result);
            return result;
        }

        public static IReadOnlyCollection<T> All
        {
            get
            {
                FillCache();
                return LookupCache.Values;
            }
        }

        public static IReadOnlyDictionary<SGuid, T> Lookup
        {
            get
            {
                FillCache();
                return LookupCache;
            }

        }

        private static Dictionary<SGuid, T> LookupCache;

        private static void FillCache()
        {
            LookupCache ??= Resources.LoadAll<T>("").ToDictionary(x => x.Guid, x => x);
        }

        public bool Equals(T other)
        {

            if (other == null)
                return false;

            return Guid.Equals(other.Guid);
        }

        public static implicit operator SGuid(IdentifyableObject<T> @object) => @object.Guid;
    }

}